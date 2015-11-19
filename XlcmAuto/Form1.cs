using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace XlcmAuto
{
    public partial class Form1 : Form
    {
        private ConfigReader config;
        private int skill_count;

        private String[] skill_keycodes;
        private int[] skill_cds;
        private long[] skill_lastusage;
        private int round_index;
        private int init_round_wait;
        private int mid_round_wait;
        private String pickKey;
        private String automoveKey;
        private String againKey;
        private String windowTitle;

        private int stageCheckIdle = 3000;

        private IntPtr windowHwnd;
        private int gameStage = 0; //-1 finished, 1 in progress, 2 round finished, 3 wait

        System.Timers.Timer gameCheckTimer;
        Thread skillThread;

        public Form1()
        {
            InitializeComponent();
            skill_keycodes = new String[6];
            skill_lastusage = new long[6];
            skill_cds = new int[6];
        }

        private void readConfig()
        {
            skill_count = config.getSkillCount();
            init_round_wait = Int32.Parse(config.IniReadValue(ConfigReader.sectionSystem, ConfigReader.keyInitRoundWait, "2000"));
            mid_round_wait = Int32.Parse(config.IniReadValue(ConfigReader.sectionSystem, ConfigReader.keyMidRoundWait, "2000"));
            for (int i = 1; i <= skill_count; i++)
            {
                String keyCap = config.IniReadValue(ConfigReader.sectionSkill, config.getKeyForSkillKey(i));
                if(keyCap != null)
                {
                    skill_keycodes[i - 1] = (keyCap);
                    String val = config.IniReadValue(ConfigReader.sectionSkill, config.getKeyForSkillCd(i), "10");
                    skill_cds[i - 1] = Int32.Parse(val);
                    skill_lastusage[i - 1] = 0;
                }
            }
            pickKey = config.IniReadValue(ConfigReader.sectionHotKey, ConfigReader.keyPick, "X");
            automoveKey = config.IniReadValue(ConfigReader.sectionHotKey, ConfigReader.keyAutomove, "0");
            againKey = config.IniReadValue(ConfigReader.sectionHotKey, ConfigReader.keyAgain, "F10");
            windowTitle = config.IniReadValue(ConfigReader.sectionSystem, ConfigReader.keyWindowTitle, "DNF");
        }

        private Keys getKeyCode(String keyCap)
        {
            Keys key;
            Enum.TryParse(keyCap, out key);
            return key;
        }

        private void waitMidAnim()
        {
            Thread.Sleep(mid_round_wait);
        }

        private void autoDNF()
        {
            enterQL();
            while (gameStage != -1)
            {
                waitInitAnim();
                skillLoop();
                waitMidAnim();
                pickUp();
                continueGame();
            }
#if DEBUG
            Log.d(String.Format("Auto DNF Ended\n"));
#endif
        }

        private void continueGame()
        {
            if (gameStage == -1)
            {
#if DEBUG
                Log.d(String.Format("Back to town\n"));
#endif
                SendKeys.SendWait("F12");
            }
            else if (gameStage == 2)
            {
#if DEBUG
                Log.d(String.Format("Challege Again\n"));
#endif
                SendKeys.SendWait(againKey);
            }
        }

        private int windowPrepare()
        {
            windowHwnd = WindowsUtil.FindWindow(null, windowTitle);
            if (windowHwnd == null || windowHwnd.ToInt32() == 0)
            {
                MessageBox.Show("没有找到有效的DNF窗口", "错误");
                return -1;
            }

            WindowsUtil.SetForegroundWindow(windowHwnd);
#if DEBUG
            Log.d(String.Format("dnf window title = [{0}] and hwnd = [{1}]\n", windowTitle, windowHwnd));
#endif
            return 0;
        }

        private void pickUp()
        {
#if DEBUG
            Log.d(String.Format("begin picking items for 15\n"));
#endif
            SendKeys.SendWait(automoveKey);
            for (int i = 0; i < 15; i++)
            {
                SendKeys.SendWait(pickKey);
            }
        }

        private void skillLoop()
        {
            while (gameStage == 1)
            {
                for (int i = 0; i < skill_count; i++ )
                {
                    if (gameStage != 1)
                    {
                        break;
                    }
#if DEBUG
                    Log.d(String.Format("send skill {0} and wait for 1000ms \n", skill_keycodes[i]));
#endif
                    SendKeys.SendWait(skill_keycodes[i]);
                    Thread.Sleep(1000);
                }
            }
#if DEBUG
            Log.d(String.Format("skill loop stopped"));
#endif
        }

        private void enterQL()
        {
            SendKeys.SendWait(" ");
#if DEBUG
            Log.d("send key {Space} to enter qinglong\n");
#endif
        }

        private void waitInitAnim()
        {
#if DEBUG
            Log.d(String.Format("wait {0:d}ms for entering ql\n", init_round_wait));
#endif
            Thread.Sleep(init_round_wait);
        }

        private void checkStage(object source, System.Timers.ElapsedEventArgs e)
        {
            Color cpl = WindowsUtil.GetPointColor(321, 554);
            Color ccont = WindowsUtil.GetPointColor(652, 69);
            Color cenemyhp = WindowsUtil.GetPointColor(490, 70);

#if DEBUG
            Log.d(String.Format("Check game stage:\n"));
            Log.d(String.Format("疲劳条左端颜色{0} {1} {2}\n", cpl.R, cpl.G, cpl.B));
            Log.d(String.Format("再次挑战区域颜色{0} {1} {2}\n", ccont.R, ccont.G, ccont.B));
            Log.d(String.Format("怪物血条左端颜色{0} {1} {2}\n", cenemyhp.R, cenemyhp.G, cenemyhp.B));
#endif

            int r_pl_empty = 2;
            int g_pl_empty = 2;
            int b_pl_empty = 2;
            int r_cont_showed = 35;
            int g_cont_showed = 1;
            int b_cont_showed = 36;

            if (WindowsUtil.ColorEqual(cpl, r_pl_empty, g_pl_empty, b_pl_empty))
            {
                gameStage = -1;
            }
            else
            {
                if (WindowsUtil.ColorEqual(ccont, r_cont_showed, g_cont_showed, b_cont_showed))
                {
                    gameStage = 2;
                }
                else
                {
                    if (WindowsUtil.ColorEqual(cenemyhp, r_pl_empty, g_pl_empty, b_pl_empty))
                    {
                        gameStage = 3;
                    }
                    else
                    {
                        gameStage = 1;
                    }
                }
            }
#if DEBUG
            Log.d(String.Format("Check game stage result:{0}\n", gameStage));
#endif
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (config != null)
            {
                Log.CreateLogFile();
                int w = windowPrepare();
                if (w == -1)
                {
                    return;
                }

                //MessageBox.Show(String.Format("载入了{0:d}个技能设置", skill_count));
                skillThread = new Thread(new ThreadStart(autoDNF));
                skillThread.IsBackground = true;
                skillThread.Start();

#if DEBUG
                Log.d("skill thread started\n");
#endif

                gameCheckTimer = new System.Timers.Timer(stageCheckIdle);   //实例化Timer类，设置间隔时间为10000毫秒；   
                gameCheckTimer.Elapsed += new System.Timers.ElapsedEventHandler(checkStage); //到达时间的时候执行事件；   
                gameCheckTimer.AutoReset = true;   //设置是执行一次（false）还是一直执行(true)；   
                gameCheckTimer.Enabled = true;     //是否执行System.Timers.Timer.Elapsed事件；
                gameCheckTimer.Start();

#if DEBUG
                Log.d("checker thread started\n");
#endif
            }
            else
            {
                MessageBox.Show("请先载入一个有效的配置文件", "错误");
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            if (gameCheckTimer != null)
            {
                gameCheckTimer.Stop();
            }
            gameStage = -1;
            if (skillThread != null && skillThread.ThreadState == ThreadState.Running)
            {
                skillThread.Abort();
            }
        }

        private void configOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "自动打怪配置(*.ini)|*.ini";
            ofd.ShowDialog();
            String strOpenFileName = ofd.FileName;
            if (strOpenFileName == null || strOpenFileName == "")
            {
                return;
            }
            configFilePath.Text = strOpenFileName;
            config = new ConfigReader(strOpenFileName);
            readConfig();
        }
    }
}
