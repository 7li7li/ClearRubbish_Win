using System;
using System.Collections.Generic;
using System.IO;

namespace ClearRubbish
{
    class LjPath
    {
        private LjPath()
        {
            LoadPath();
        }
        private static LjPath instance;
        public static LjPath Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LjPath();
                }
                return instance;
            }
        }
        public string[] Paths { get; private set; }
        private void LoadPath()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;//Environment.CurrentDirectory;
            string pathStr = Path.Combine(path, "Path.txt");
            if (!File.Exists(pathStr))
            {
                Console.WriteLine("未找到" + pathStr);
                return;
            }
            string[] lines = File.ReadAllLines(pathStr);
            List<string> paths = new List<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("//"))
                    continue;
                paths.Add(lines[i]);
            }
            Paths = paths.ToArray();
        }
    }
}
