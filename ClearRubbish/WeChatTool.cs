using System;
using System.IO;

namespace ClearRubbish
{
    class WeChatTool
    { 
        public static void DelWeChat(string weChatPath)
        {
            DirectoryInfo TheFolder2 = new DirectoryInfo(weChatPath);
            foreach (DirectoryInfo NextFolder2 in TheFolder2.GetDirectories())
            {
                if (!WeChatConfig.Instance.IgnoreFolder(NextFolder2.Name))
                {
                    Console.WriteLine("正在清理:" + NextFolder2.Name);          
                    FileHelper.DeleteDir(NextFolder2.FullName, Config.Instance.DelEmptyFolder, Config.Instance.DelFileLog);
                    Console.WriteLine("清理完成");
                    Console.WriteLine();
                }
            }
        }
        //判断是否是微信文件夹
        public static bool IsWeChatFloder(string path)
        {
            //Console.WriteLine(path);
            DirectoryInfo TheFolder = new DirectoryInfo(path);
            int i = 0;
            //遍历文件夹
            foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
            {
                if (NextFolder.Name == "FileStorage")
                    i++;
                if (NextFolder.Name == "Msg")
                    i++;
                if (NextFolder.Name == "config")
                    i++;
            }
            return i == 3;
        }
    }
}
