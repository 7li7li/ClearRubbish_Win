using System;
using System.IO;

namespace ClearRubbish
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Config.Instance.DelTemp)
            {
                Console.WriteLine("删除临时目录");
                Console.WriteLine();
                ClearTemp();
            }
            else
                Console.WriteLine("不删除临时目录");
            //清理辣鸡
            Console.WriteLine("清理自定义目录");
            Console.WriteLine();
            ClearOtherFolder(LjPath.Instance.Paths);

            Console.WriteLine("自定义目录清理完成");
            Console.WriteLine();
            //清理QQ
            if (QQConfig.Instance.ClearQQ)
            {
                Console.WriteLine("清理QQ");
                Console.WriteLine();
                ClearQQ(QQConfig.Instance.QQPaths);
            }
            else
                Console.WriteLine("不清理QQ");
            //清理微信
            if (WeChatConfig.Instance.ClearWeChat)
            {
                Console.WriteLine("清理微信");
                Console.WriteLine();
                ClearWeChat(WeChatConfig.Instance.WeChatPaths);
            }
            else
                Console.WriteLine("不清理微信");

            Console.WriteLine("微信清理完成");
            Console.WriteLine("全部清理已完成,按回车键退出"); 
            Console.ReadKey();
        }
        //清理临时目录AppData\Local\Temp
        private static void ClearTemp()
        {
            string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string tempPath = Path.Combine(localAppDataPath, "Temp");
            FileHelper.DeleteDir(tempPath, Config.Instance.DelEmptyFolder,Config.Instance.DelFileLog);
        }

        //删除自定义的文件夹
        private static void ClearOtherFolder(string[] paths)
        {
            if (paths == null)
                return;
            for (int i = 0; i < paths.Length; i++)
            {
                Console.WriteLine("正在清理:" + paths[i]);
                FileHelper.DeleteDir(paths[i], Config.Instance.DelEmptyFolder, Config.Instance.DelFileLog);
                Console.WriteLine("清理完成");
                Console.WriteLine();
            }   
        }
        //清理QQ
        private static void ClearQQ(string[] qqPaths)
        {
            if(qqPaths == null)
            {
                Console.WriteLine("qq路径为空，请检查配置文件");
                Console.ReadKey();
                return;
            }
            for (int i = 0; i < qqPaths.Length; i++)
            {
                if (!QQTool.IsQQFloder(qqPaths[i]))
                {
                    Console.WriteLine("该路径不是QQ文件夹路径:" + qqPaths[i]);
                    Console.WriteLine("寻找子目录");
                    //遍历下一级目录
                    DirectoryInfo theFolder = new DirectoryInfo(qqPaths[i]);
                    foreach (DirectoryInfo nextFolder in theFolder.GetDirectories())
                    {
                        if (QQTool.IsQQFloder(nextFolder.FullName))
                        {//如果是qq目录
                            Console.WriteLine("找到QQ路径:" + nextFolder.FullName);
                            Console.WriteLine("开始清理");
                            Console.WriteLine();
                            QQTool.DelQQ(nextFolder.FullName);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("QQ路径:" + qqPaths[i]);
                    Console.WriteLine("开始清理");
                    Console.WriteLine();
                    WeChatTool.DelWeChat(qqPaths[i]);
                }
            }
        }
        //清理微信
        private static void ClearWeChat(string[] weChatPaths)
        {
            if (weChatPaths == null)
            {
                Console.WriteLine("微信路径为空，请检查配置文件");
                Console.ReadKey();
                return;
            }
            for (int i = 0; i < weChatPaths.Length; i++)
            {
                if (!WeChatTool.IsWeChatFloder(weChatPaths[i]))
                {
                    Console.WriteLine("该路径不是微信文件夹路径:" + weChatPaths[i]);
                    Console.WriteLine("寻找子目录");
                    //遍历下一级目录
                    DirectoryInfo theFolder = new DirectoryInfo(weChatPaths[i]);
                    foreach (DirectoryInfo nextFolder in theFolder.GetDirectories())
                    {
                        if (WeChatTool.IsWeChatFloder(nextFolder.FullName))
                        {//如果是微信目录
                            Console.WriteLine("找到微信路径:" + nextFolder.FullName);
                            Console.WriteLine("开始清理");
                            Console.WriteLine();
                            WeChatTool.DelWeChat(nextFolder.FullName);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("微信路径:" + weChatPaths[i]);
                    Console.WriteLine("开始清理");
                    Console.WriteLine();
                    WeChatTool.DelWeChat(weChatPaths[i]);
                }
            }
        }
    }
}
