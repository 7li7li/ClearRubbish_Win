using System;
using System.IO;

namespace ClearRubbish
{
    class Config
    {
        private Config()
        {
            ReadConfig();
        }
        private static Config instance;
        public static Config Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Config();
                }
                return instance;
            }
        }
        private bool debug = false;
        private bool delFileLog = false;
        private bool delEmptyFolder = false;
        private bool delTemp = true;
        /// <summary>
        /// 测试不删除
        /// </summary>
        public bool Debug { get { return debug; } }
        /// <summary>
        /// 文件删除日志
        /// </summary>
        public bool DelFileLog { get { return delFileLog; } }
        /// <summary>
        /// 删除空文件夹
        /// </summary>
        public bool DelEmptyFolder { get { return delEmptyFolder; } }
        /// <summary>
        /// 删除缓存目录
        /// </summary>
        public bool DelTemp { get { return delTemp; } }


        private void ReadConfig()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;//Environment.CurrentDirectory;
            string configPath = Path.Combine(path, "Config.txt");
            if (!File.Exists(configPath))
            {
                Console.WriteLine("未找到" + configPath);
                return;
            }
            string[] configLines = File.ReadAllLines(configPath);
            for (int i = 0; i < configLines.Length; i++)
            {
                if (configLines[i].StartsWith("//"))
                    continue;//跳过注释行
                if (configLines[i].StartsWith("Debug") && configLines[i].Contains("="))
                {
                    var configValue = configLines[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    //subKey = configValue[0].Trim();
                    debug = configValue[1].Trim() == "true";
                }
                if (configLines[i].StartsWith("DelFileLog") && configLines[i].Contains("="))
                {
                    var configValue = configLines[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    //subKey = configValue[0].Trim();
                    delFileLog = configValue[1].Trim() == "true";
                }
                if (configLines[i].StartsWith("DelEmptyFolder") && configLines[i].Contains("="))
                {
                    var configValue = configLines[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    //subKey = configValue[0].Trim();
                    delEmptyFolder = configValue[1].Trim() == "true";
                }
                if (configLines[i].StartsWith("DelTemp") && configLines[i].Contains("="))
                {
                    var configValue = configLines[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    //subKey = configValue[0].Trim();
                    delTemp = configValue[1].Trim() == "true";
                }
            }
        }
    }
}
