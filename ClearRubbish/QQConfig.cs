using System;
using System.Collections.Generic;
using System.IO;

namespace ClearRubbish
{
    class QQConfig
    {
        private QQConfig()
        {
            ReadQQConfig();
        }
        private static QQConfig instance;
        public static QQConfig Instance 
        {
            get
            {
                if(instance == null)
                {
                    instance = new QQConfig();
                }
                return instance;
            }
        }
        private bool clearQQ = true;
        private bool delMsg = false;
        private string[] qqPaths = null;
        private string[] noDelFloder = null;//不删除的文件夹名称
        /// <summary>
        /// 清理QQ
        /// </summary>
        public bool ClearQQ { get { return clearQQ; } }
        /// <summary>
        /// 清理Msg
        /// </summary>
        public bool DelMsg { get { return delMsg; } }
        /// <summary>
        /// QQ路径
        /// </summary>
        public string[] QQPaths { get { return qqPaths; } }

        //读取QQ配置
        private void ReadQQConfig()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;//Environment.CurrentDirectory;
            string configPath = Path.Combine(path, "QQConfig.txt");
            if(!File.Exists(configPath))
            {
                Console.WriteLine("未找到"+ configPath);
                return;
            }
            string[] configLines = File.ReadAllLines(configPath);

            List<string> qqPaths = new List<string>();
            for (int i = 0; i < configLines.Length; i++)
            {
                if (configLines[i].StartsWith("//"))
                    continue;//跳过注释行
                if (configLines[i].StartsWith("ClearQQ") && configLines[i].Contains("="))
                {
                    var configValue = configLines[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    //subKey = configValue[0].Trim();
                    clearQQ = configValue[1].Trim() == "true";
                }
                if (configLines[i].StartsWith("DelMsg") && configLines[i].Contains("="))
                {
                    var configValue = configLines[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    //subKey = configValue[0].Trim();
                    delMsg = configValue[1].Trim() == "true";
                }
                if (configLines[i].StartsWith("QQPath") && configLines[i].Contains("="))
                {
                    var configValue = configLines[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    //subKey = configValue[0].Trim();
                    string value = configValue[1].Trim();
                    string personalFloder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    string qqPath = value == "default" ? Path.Combine(personalFloder, "Tencent Files") : value;
                    qqPaths.Add(qqPath);
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
            this.qqPaths = qqPaths.ToArray();
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
