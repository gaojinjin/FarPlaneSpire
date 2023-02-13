using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FSFile {
    /*
     * 文件系统，用来读取和保存
    */
    public class FileManager : Singleton<FileManager>
    {
        /*
         * 根据文件名来读取Resource中的文本数据
        */
        public string ReadResourceText(string file) {
            try {

                string value = ((TextAsset)Resources.Load(file)).text;
                return value;
            } catch (System.NullReferenceException e) {
                Debug.Log(e.Message);
                return null;
            }
        }

        /*
         * 读取指定路径的文本数据
        */
        public string ReadTextFile(string path) {
            // 首先判断文件是否存在
            if (IsFileExists(path)) {
                return File.ReadAllText(path);
            }

            return null;
        }

        /*
         * 传入路径和要保存的字符
        */
        public void SaveTextToFile(string path, string value) {
            // 找到当前路径
            FileInfo file = new FileInfo(path);
            // 判断有没有文件，有则打开，没有则创建后打开
            StreamWriter stream = file.CreateText();
            // 将字符串存进文件中
            stream.WriteLine(value);
            // 释放资源
            stream.Close();
            stream.Dispose();
        }

        /*
         * 传入要保存的文件夹，文件和文本
        */
        public void SaveText(string dir, string file, string value) {
            // 判断是否存在该文件
            if (!IsDirectorExists(dir))
                Directory.CreateDirectory(dir);

            SaveTextToFile(dir + "/" + file, value);
        }

        /*
         * 删除指定路径的文件
        */
        public void RemoveFile(string path) {
            // 查看文件是否存在
            if (IsFileExists(path)) {
                // 将其删除
                FileInfo file = new FileInfo(path);
                file.Delete();
            }
        }

        /*
         * 删除指定路径的文件夹
        */
        public void RemoveDirector(string path) {
            // 查看文件夹
            if (IsDirectorExists(path)) {
                // 将文件夹删除
                DirectoryInfo directory = new DirectoryInfo(path);
                directory.Delete();
            }
        }

        /*
         * 检查文件夹下的文件
         * 传入路径和要传入的文件类型
        */
        public ArrayList CheckFilesInDirector(string path, string type) {
            // 判断文件夹是否存在
            if (IsDirectorExists(path)) {
                DirectoryInfo info = new DirectoryInfo(path);
                FileInfo[] files = info.GetFiles("*." + type, SearchOption.TopDirectoryOnly);

                // 将文件名放入数组中保存返回
                ArrayList names = new ArrayList();

                // 遍历文件夹，输出内容
                foreach (FileInfo file in files)
                    names.Add(file.Name.Substring(0, file.Name.Length - (1 + type.Length)));

                return names;
            }

            return null;
        }

        /*
         * 判断指定路径的文件是否存在
        */
        public bool IsFileExists(string path) {
            return File.Exists(path);
        }

        /*
         * 判断文件夹是否存在
        */
        public bool IsDirectorExists(string path) {
            return Directory.Exists(path);
        }
    }
}
