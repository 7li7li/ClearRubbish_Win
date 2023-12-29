using System;
using System.Collections.Generic;
using System.IO;

namespace ClearRubbish
{
    class WeChatConfig
    {
        private WeChatConfig()
        {
            ReadWeChatConfig();
        }
        private static WeChatConfig instance;
        public static WeChatConfig Instance 
        {
            get
            {
                if(instance == null)
                {
                    instance = new WeChatConfig();
                }
                return instance;
            }
        }
        private bool clearWeChat = false;
        private string[] weChatPaths = null;
        private string[] noDelFloder = null;//不删除的文件夹名称
        /// <summary>
        /// 清理微信
        /// </summary>
        public bool ClearWeChat { get { return clearWeChat; } }
        /// <summary>
        /// 微信路径
        /// </summary>
        public string[] WeChatPaths { get { return weChatPaths; } }

        //读取微信配置
        private void ReadWeChatConfig()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;//Environment.CurrentDirectory;
            string configPath = Path.Combine(path, "WeChatConfig.txt");
            if(!File.Exists(configPath))
            {
                Console.WriteLine("未找到"+ configPath);
                return;
            }
            string[] configLines = File.ReadAllLines(configPath);

            List<string> weChatPaths = new List<string>();
            for (int i = 0; i < configLines.Length; i++)
            {
                if (configLines[i].StartsWith("//"))
                    continue;//跳过注释行
                if (configLines[i].StartsWith("ClearWeChat") && configLines[i].Contains("="))
                {
                    var configValue = configLines[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    //subKey = configValue[0].Trim();
                    clearWeChat = configValue[1].Trim() == "true";
                }
                if (configLines[i].StartsWith("WeChatPath") && configLines[i].Contains("="))
                {
                    var configValue = configLines[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    //subKey = configValue[0].Trim();
                    string value = configValue[1].Trim();
                    string personalFloder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    string weChatPath = value == "default" ? Path.Combine(personalFloder, "WeChat Files") : value;
                    weChatPaths.Add(weChatPath);
                }
                if (configLines[i].StartsWith("IgnoreFolder") && configLines[i].Contains("="))
                {
                    var configValue = configLines[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    //subKey = configValue[0].Trim();
                    string value = configValue[1].Trim();
                    var floderNames=value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    noDelFloder = floderNames;
                }
            }
            this.weChatPaths = weChatPaths.ToArray();
        }
        /// <summary>
        /// 忽略文件夹
        /// </summary>
        /// <param name="folder">文件夹名称</param>
        /// <returns>true忽略</returns>
        public bool IgnoreFolder(string folder)
        {
            for (int i = 0; i < noDelFloder.Length; i++)
            {
                if (noDelFloder[i] == folder)
                    return true;
            }
            return false;
        }
    }
}
