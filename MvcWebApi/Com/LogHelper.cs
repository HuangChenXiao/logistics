using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com
{
    public class LogHelper
    {
        //简洁版
        public static void AddLgoToTXT(string logstring)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "operalog.txt";
            if (!System.IO.File.Exists(path))
            {
                FileStream stream = System.IO.File.Create(path);
                stream.Close();
                stream.Dispose();
            }
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(logstring);
            }
        }
        //带自动删除版

        public static void Logtest(string logstring)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "operalog.txt";
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }

                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(logstring);
                }

                long size = 0;

                //获取文件大小
                using (FileStream file = System.IO.File.OpenRead(path))
                {
                    size = file.Length;//文件大小。byte
                }

                //判断日志文件大于2M，自动删除。
                if (size > (1024 * 4 * 512))
                {
                    System.IO.File.Delete(path);
                }
            }
            catch
            {

            }
        }

    }
}
