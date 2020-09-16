using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// 提取PDF图片视图模型
    /// </summary>
    public class ExtractPdfImageViewModel : ViewModelBase
    {
        /// <summary>
        /// PDF操作服务
        /// </summary>
        private readonly IPdfService _pdfService;

        public ExtractPdfImageViewModel(IPdfService pdfService)
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
                BtnContent = value ? "选择文件" : "正在处理中...";
                Set(ref _btnEnabled, value);
            }
        }

        /// <summary>
        /// 开始执行按钮文本信息
        /// </summary>
        private string _btnContent = "选择文件";

        /// <summary>
        /// 开始执行按钮文本信息
        /// </summary>
        public string BtnContent
        {
            get => _btnContent;
            set => Set(ref _btnContent, value);
        }


        #endregion

        #region 命令
        /// <summary>
        /// 选择图片文件
        /// </summary>
        public ICommand SelectPdfCommand => new RelayCommand<string>(async (e) =>
        {
            BtnEnabled = false;
            try
            {
                var pdfDialog = new OpenFileDialog {Multiselect = false, Filter = "PDF文件|*.pdf;"};
                if (pdfDialog.ShowDialog() == DialogResult.OK)
                {
                    _selectPdfFullFileName = pdfDialog.FileName;

                    var folderBrowserDialog = new FolderBrowserDialog
                    {
                        Description = "请选择图片将要保存到的文件夹", ShowNewFolderButton = true, UseDescriptionForTitle = true,
                        SelectedPath = Path.GetDirectoryName(_selectPdfFullFileName)
                    };

                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        _saveFolderPath = folderBrowserDialog.SelectedPath;

                        var imageList = await _pdfService.ExtractBitmapsFromPdfAsync(_selectPdfFullFileName);
                        var imageNameList = new string[imageList.Length];
                        for (var i = 0; i < imageList.Length; i++)
                        {
                            var newFileName = $"{Path.GetFileNameWithoutExtension(_selectPdfFullFileName)}_{i + 1}.jpg";
                            var combinePath = Path.Combine(_saveFolderPath, newFileName);
                            imageList[i].Save(combinePath);
                            imageNameList[i] = combinePath;
                        }

                        var result = AduMessageBox.ShowYesNo(
                            $"执行已完成,文件已保存到{_saveFolderPath}",
                            "执行已完成",
                            "打开文件夹",
                            "知道了",
                            MessageBoxImage.Exclamation);
                        if (result == MessageBoxResult.Yes)
                        {
                            FileHelper.OpenExplorerAndSelect(imageNameList);
                        }
                    }
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
    }
}
