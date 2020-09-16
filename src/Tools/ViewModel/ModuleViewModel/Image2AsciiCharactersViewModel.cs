using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using AduSkin.Controls;
using AduSkin.Controls.Metro;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Tools.Core.Services.Abstractions;
using Tools.Helper;

namespace Tools.ViewModel.ModuleViewModel
{
    /// <summary>
    /// 图片转ASCii字符视图模型
    /// </summary>
    public class Image2AsciiCharactersViewModel : ViewModelBase
    {
        /// <summary>
        /// PDF操作服务
        /// </summary>
        private readonly IPdfService _pdfService;

        public Image2AsciiCharactersViewModel(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }


        #region 属性&字段

        /// <summary>
        /// 选中PDF的全路径
        /// </summary>
        private string _selectPdfFullFileName;

        /// <summary>
        /// 保存文件夹路径
        /// </summary>
        private string _saveFolderPath;


        /// <summary>
        /// 按钮是否可用
        /// </summary>
        private bool _btnEnabled = true;

        /// <summary>
        /// 按钮是否可用
        /// </summary>
        public bool BtnEnabled
        {
            get => _btnEnabled;
            set
            {
                BtnContent = value ? "选择图片" : "正在转换中...";
                Set(ref _btnEnabled, value);
            }
        }

        /// <summary>
        /// 开始执行按钮文本信息
        /// </summary>
        private string _btnContent = "选择图片";

        /// <summary>
        /// 开始执行按钮文本信息
        /// </summary>
        public string BtnContent
        {
            get => _btnContent;
            set => Set(ref _btnContent, value);
        }


        /// <summary>
        /// 开始执行按钮文本信息
        /// </summary>
        private string _generateResult = "选择图片";

        /// <summary>
        /// 开始执行按钮文本信息
        /// </summary>
        public string GenerateResult
        {
            get => _generateResult;
            set => Set(ref _generateResult, value);
        }


        #endregion

        #region 命令
        /// <summary>
        /// 选择图片文件
        /// </summary>
        public ICommand SelectImageCommand => new RelayCommand<string>(async (e) =>
        {
            BtnEnabled = false;
            try
            {
                var pdfDialog = new OpenFileDialog {Multiselect = false, Filter = "图片|*.jpg;*.jpeg;*.bmp;*.png;"};
                if (pdfDialog.ShowDialog() == DialogResult.OK)
                {
                    _selectPdfFullFileName = pdfDialog.FileName;

                    var image = File.ReadAllBytes(_selectPdfFullFileName);
                    var ms1 = new MemoryStream(image);
                    var bm = (Bitmap) Image.FromStream(ms1);
                    var wh = bm.Width / bm.Height;
                    GenerateResult = ConvertToChar(bm, 1, 1);
                }
            }
            catch (Exception exception)
            {
                NoticeManager.NotifiactionShow.AddNotifiaction(new NotifiactionModel()
                {
                    Title = "出问题啦！",
                    Content = exception.Message,
                    NotifiactionType = EnumPromptType.Error
                });
            }
            finally
            {
                BtnEnabled = true;
            }
        });
        #endregion
        
        /// <summary>
        /// 将图片转换为字符画
        /// </summary>
        /// <param name="bitmap">Bitmap类型的对象</param>
        /// <param name="wAddNum">宽度缩小倍数（如果输入3，则以1/3倍的宽度显示）</param>
        /// <param name="hAddNum">高度缩小倍数（如果输入3，则以1/3倍的高度显示）</param>
        public static string ConvertToChar(Bitmap bitmap, int wAddNum, int hAddNum)
        {
            StringBuilder sb = new StringBuilder();
            String replaceChar = "@*#$%XB0H?OC7>+v=~^:_-'`. ";
            for (int i = 0; i < bitmap.Height; i += hAddNum)
            {
                for (int j = 0; j < bitmap.Width; j += wAddNum)
                {
                    //获取当前点的Color对象
                    Color c = bitmap.GetPixel(j, i);
                    //计算转化过灰度图之后的rgb值（套用已有的计算公式就行）
                    int rgb = (int) (c.R * 0.299 + c.G * 0.587 + c.B * 0.114);
                    //计算出replaceChar中要替换字符的index
                    //所以根据当前灰度所占总rgb的比例(rgb值最大为255，为了防止超出索引界限所以/256.0)
                    //（肯定是小于1的小数）乘以总共要替换字符的字符数，获取当前灰度程度在字符串中的复杂程度
                    int index = (int) (rgb / 256.0 * replaceChar.Length);
                    //追加进入sb
                    sb.Append(replaceChar[index]);
                }

                //添加换行
                sb.Append("\r\n");
            }

            return sb.ToString();
        }
    }
}
