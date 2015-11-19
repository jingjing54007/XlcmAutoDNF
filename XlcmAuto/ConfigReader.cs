using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XlcmAuto
{
    class ConfigReader
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        private String path;
        private int skillCount;
        public static String sectionSkill = "skills";
        public static String sectionSystem = "system";
        public static String sectionHotKey = "hotkey";

        public static String keySkillKey = "skill_key_{0:d}";
        public static String keySkillCd = "skill_cd_{0:d}";
        public static String keySkillCount = "skill_count";
        public static String keyPick = "pick";
        public static String keyAgain = "again";
        public static String keyAutomove = "automove";
        public static String keyMedicine = "medicine";
        public static String keyMidRoundWait = "init_round_wait";
        public static String keyInitRoundWait = "mid_round_wait";
        public static String keyWindowTitle = "dnf_window_title";

        public ConfigReader(String filepath)
        {
            path = filepath;
            skillCount = Int32.Parse(IniReadValue(sectionSkill, keySkillCount, "1"));
        }

        public int getSkillCount()
        {
            return skillCount;
        }

        public String getKeyForSkillCd(int index)
        {
            return String.Format(keySkillCd, index);
        }

        public String getKeyForSkillKey(int index)
        {
            return String.Format(keySkillKey, index);
        }

        public void IniWriteValue(string section, string key, string iValue)
        {
            WritePrivateProfileString(section, key, iValue, path);
        }

        public string IniReadValue(string section, string key, string Default = null)
        {
            try
            {
                StringBuilder temp = new StringBuilder(255);
                int i = GetPrivateProfileString(section, key, "", temp, 255, path);
                return temp.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + e.StackTrace);
                return Default;
            }

        }
    }
}
