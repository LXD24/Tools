using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using AduSkin.Controls;
using AduSkin.Controls.Metro;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Tools.Core.Services.Abstractions;
using Tools.Helper;
using Tools.Models;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Tools.ViewModel.ModuleViewModel
{
    /// <summary>
    /// 图片转PDF视图模型
    /// </summary>
    public class Image2PdfViewModel : ViewModelBase
    {
        /// <summary>
        /// PDF操作服务
        /// </summary>
        private readonly IPdfService _pdfService;

        public Image2PdfViewModel(IPdfService pdfService)
        {
            _pdfService = pdfService; //SimpleIoc.Default.GetInstance<IPdfService>();
            Images = new ObservableCollection<ImageInfo>();
        }


        #region 字段&属性

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
                BtnContent = value ? "开始执行" : "正在执行中...";
                Set(ref _btnEnabled, value);
            }
        }

        /// <summary>
        /// 开始执行按钮文本信息
        /// </summary>
        private string _btnContent = "开始执行";

        /// <summary>
        /// 开始执行按钮文本信息
        /// </summary>
        public string BtnContent
        {
            get => _btnContent;
            set => Set(ref _btnContent, value);
        }

        /// <summary>
        /// 图片
        /// </summary>
        public ObservableCollection<ImageInfo> Images { get; set; }

        /// <summary>
        /// 当前选中
        /// </summary>
        private ImageInfo _selectedItem;

        public ImageInfo SelectedItem
        {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public int PrevRowIndex { get; set; } = -1;



        #endregion

        #region 命令

        /// <summary>
        /// 选择图片文件
        /// </summary>
        public ICommand SelectImagesCommand => new RelayCommand<string>((e) =>
        {
            var dialog = new OpenFileDialog {Multiselect = true, Filter = "图片|*.jpg;*.jpeg;*.bmp;*.png;*.gif"};
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var hs = Images.Select(t => t.Path).ToHashSet();
                foreach (var item in dialog.FileNames)
                {
                    if (!hs.Contains(item))
                    {
                        Images.Add(new ImageInfo
                        {
                            Path = item,
                            Name = Path.GetFileNameWithoutExtension(item)
                        });
                        hs.Add(item);
                    }
                }
            }
        });

        /// <summary>
        /// 清空图片列表命令
        /// </summary>
        public ICommand ClearImagesCommand => new RelayCommand<string>((e) =>
        {
            Images.Clear();
            SelectedItem = null;
            PrevRowIndex = -1;
        });


        /// <summary>
        /// 编辑导出目录
        /// </summary>
        public ICommand ExecuteCommand => new RelayCommand<string>(async (e) =>
        {
            BtnEnabled = false;
            try
            {

                if (!Images.Any()) return;
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "pdf文件|*.pdf",
                    FilterIndex = 0,
                    RestoreDirectory = true,
                    CheckPathExists = true,
                    FileName = DateTime.Now.ToString("yyyyMMddHHmmss")
                };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var outPath = saveFileDialog.FileName;
                    var imagePaths = Images.Select(t => t.Path).ToList();
                    await _pdfService.ConvertImages2PdfAsync(imagePaths, outPath);

                    var result = AduMessageBox.ShowYesNo(
                        $"执行已完成,文件已保存为{outPath}",
                        "执行已完成",
                        "打开文件夹",
                        "知道了",
                        MessageBoxImage.Exclamation);
                    if (result == MessageBoxResult.Yes)
                    {
                       FileHelper.OpenExplorerAndSelect(outPath);
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

        #region 普通函数

        public void RemoveImage(string path)
        {
            var item = Images.FirstOrDefault(t => t.Path == path);
            if (item != null)
            {
                Images.Remove(item);
                if (SelectedItem != null && SelectedItem.Path == item.Path)
                    SelectedItem = null;
            }
        }

        #endregion
    }
}
