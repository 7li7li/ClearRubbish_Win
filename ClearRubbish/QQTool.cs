using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ClearRubbish
{
    class QQTool
    {
        /// <summary>
        /// QQ聊天记录文件
        /// </summary>
        static readonly string[] msgFileName = new string[] { "Msg3.0.db", "Msg3.0.db-journal" , "Msg3.0index.db", "Msg3.0index.db-journal" };
        public static void DelQQ(string qqPath)
        {
            foreach (string f in Directory.GetFileSystemEntries(qqPath))
            {
                if (File.Exists(f))
                {
                    if (QQConfig.Instance.DelMsg)
                        FileHelper.DeleteFile(f, Config.Instance.DelFileLog);
                    else
                        FileHelper.DeleteFile(f, Config.Instance.DelFileLog, msgFileName);                  
                }
                else
                {
                    //文件夹
                    string[] str = f.Split('\\');
                    if (!QQConfig.Instance.IgnoreFolder(str[str.Length - 1]))
                    {
                        Console.WriteLine("正在清理:" + f);
                        if (QQConfig.Instance.DelMsg)
                            FileHelper.DeleteDir(f, Config.Instance.DelEmptyFolder, Config.Instance.DelFileLog);
                        else
                            FileHelper.DeleteDir(f, Config.Instance.DelEmptyFolder, Config.Instance.DelFileLog, msgFileName);
                        Console.WriteLine("清理完成");
                        Console.WriteLine();
                    }
                }
            }
        }
        //判断是否是QQ文件夹
        public static bool IsQQFloder(string path)
        {
            string[] str = path.Split('\\');
            if (!IsNumber(str[str.Length - 1]))
                return false;//如果不是QQ号
            DirectoryInfo TheFolder = new DirectoryInfo(path);
            int i = 0;
            //遍历文件夹
            foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
            {
                if (NextFolder.Name == "QQ")
                    i++;
            }
            return i == 1;
        }
        /// <summary>
        /// 判断字符串是否是数字
        /// </summary>
        public static bool IsNumber(string s)
        {
            const string pattern = "^[0-9]*$";
            Regex rx = new Regex(pattern);
            return rx.IsMatch(s);
        }
    }
}
