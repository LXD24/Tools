using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Tools.Helper
{
    public class FileHelper
    {
        /// <summary>
        /// 在资源浏览器打开文件夹
        /// </summary>
        /// <param name="folderPath"></param>
        public static void OpenExplorer(string folderPath)
        {
            Process.Start("explorer.exe", folderPath);
        }

        /// <summary>
        ///  在资源浏览器打开文件所在文件夹并选中文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void OpenExplorerAndSelect(string filePath)
        {
            Process.Start("explorer.exe", "/select," + filePath);
        }


        /// <summary>
        ///  在资源浏览器打开文件所在文件夹并选中文件
        /// </summary>
        /// <param name="filePaths"></param>
        public static void OpenExplorerAndSelect(string[] filePaths)
        {
            Process.Start("explorer.exe", "/select," + string.Join(',', filePaths));
        }
    }
}
