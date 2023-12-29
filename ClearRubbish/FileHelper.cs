using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClearRubbish
{
    class FileHelper
    {
        #region 直接删除指定目录下的所有文件及文件夹(保留目录)
        public static void DeleteDir(string folderName, bool delEmptyFolder, bool log)
        {
            try
            {
                //去除文件夹和子文件的只读属性
                //去除文件夹的只读属性
                System.IO.DirectoryInfo fileInfo = new DirectoryInfo(folderName);
                fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;

                //去除文件的只读属性
                System.IO.File.SetAttributes(folderName, System.IO.FileAttributes.Normal);

                //判断文件夹是否还存在
                if (Directory.Exists(folderName))
                {
                    foreach (string f in Directory.GetFileSystemEntries(folderName))
                    {
                        if (File.Exists(f))
                        {
                            //如果有子文件删除文件
                            DeleteFile(f, log);
                        }
                        else
                        {
                            //循环递归删除子文件夹
                            DeleteDir(f, delEmptyFolder, log);
                        }
                    }

                    //删除空文件夹
                    if (delEmptyFolder)
                    {
                        if (!Config.Instance.Debug)
                            Directory.Delete(folderName);
                        if (log)
                        Console.WriteLine("删除空文件夹:" + folderName);
                    }
                }

            }
            catch (Exception ex) // 异常处理
            {
                if (log)
                    Console.WriteLine(ex.Message.ToString());// 异常信息
            }
        }
        public static void DeleteDir(string folderName,bool delEmptyFolder,bool log,string[] IgnoreFileName)
        {
            try
            {
                //去除文件夹和子文件的只读属性
                //去除文件夹的只读属性
                System.IO.DirectoryInfo fileInfo = new DirectoryInfo(folderName);
                fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;

                //去除文件的只读属性
                System.IO.File.SetAttributes(folderName, System.IO.FileAttributes.Normal);

                //判断文件夹是否还存在
                if (Directory.Exists(folderName))
                {
                    foreach (string f in Directory.GetFileSystemEntries(folderName))
                    {
                        if (File.Exists(f))
                        {
                            //如果有子文件删除文件
                            DeleteFile(f, log, IgnoreFileName);
                        }
                        else
                        {
                            //循环递归删除子文件夹
                            DeleteDir(f, delEmptyFolder,log, IgnoreFileName);
                        }
                    }

                    //删除空文件夹
                    if (delEmptyFolder)
                    {
                        if (!Config.Instance.Debug)
                            Directory.Delete(folderName);
                        if (log)
                            Console.WriteLine("删除空文件夹:" + folderName);
                    }
                }

            }
            catch (Exception ex) // 异常处理
            {
                if (log)
                    Console.WriteLine(ex.Message.ToString());// 异常信息
            }
        }
        #endregion
        public static void DeleteFile(string path,bool log)
        {
            //如果有子文件删除文件
            if (!Config.Instance.Debug)
                File.Delete(path);
            if (log)
                Console.WriteLine("删除文件:" + path);
        }
        public static void DeleteFile(string path,bool log,string[] IgnoreFileName)
        {
            string filename = System.IO.Path.GetFileName(path);//文件名  “Default.aspx”
            if (IgnoreFile(IgnoreFileName, filename))
                return;
            if (!Config.Instance.Debug)
                File.Delete(path);
            if (log)
                Console.WriteLine("删除文件:" + path);
        }
        /// <summary>
        /// 忽略文件
        /// </summary>

        public static bool IgnoreFile(string[] fileNames,string file)
        {
            if (fileNames == null)
                return false;
            for (int i = 0; i < fileNames.Length; i++)
            {
                if (fileNames[i] == file)
                    return true;
            }
            return false;
        }
    }

}
