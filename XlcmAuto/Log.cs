using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace XlcmAuto
{
    class Log
    {
        public static String LOG_PATH = "C:\\xlcmlog.log";
        public static StreamWriter sw;

        public static void CreateLogFile()
        {
            if (!File.Exists(LOG_PATH))
            {
                File.Create(LOG_PATH).Close();
            }
            sw = new StreamWriter(LOG_PATH, true);
            sw.AutoFlush = true;
        }

        public static void d(String str)
        {
            sw.WriteLine(str);
        }

        public static void Close()
        {
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    }
}
