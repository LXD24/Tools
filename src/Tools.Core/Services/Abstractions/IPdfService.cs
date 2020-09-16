using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Core.Services.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPdfService
    {
        /// <summary>
        /// 图片批量合成PDF
        /// </summary>
        /// <param name="imageSourceList">文件源路径集合</param>
        /// <param name="outPath">PDF输出文件路径</param>
        /// <returns></returns>
        Task ConvertImages2PdfAsync(IEnumerable<string> imageSourceList, string outPath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pdfSource">PDF源路径</param>
        /// <returns></returns>
        Task<Bitmap[]> ExtractBitmapsFromPdfAsync(string pdfSource);


        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="pdfSource">PDF源路径</param>
        /// <param name="newPdfSavePath">新PDF文件保存目录</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        Task EncryptAsync(string pdfSource, string newPdfSavePath, string passWord);
    }
}
