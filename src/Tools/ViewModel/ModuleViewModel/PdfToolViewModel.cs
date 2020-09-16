using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using Tools.Controls;
using Tools.Models;

namespace Tools.ViewModel.ModuleViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class PdfToolViewModel : ViewModelBase
    {
        public PdfToolViewModel()
        {
            AllControl = new List<ControlModel>()
            {
                new ControlModel("图片合成PDF", typeof(Image2Pdf)),
                new ControlModel("提取PDF图片", typeof(ExtractPdfImage)),
            };
            ViewSource.Source = _allControl;
            ViewSource.View.Culture = new System.Globalization.CultureInfo("zh-CN");
            //_viewSource.View.SortDescriptions.Add(new SortDescription(nameof(ControlModel.Title), ListSortDirection.Ascending));
        }

        private IEnumerable<ControlModel> _allControl;
        /// <summary>
        /// 所有控件
        /// </summary>
        public IEnumerable<ControlModel> AllControl
        {
            get => _allControl;
            set => Set(ref _allControl, value);
        }

        private CollectionViewSource _viewSource = new CollectionViewSource();
        /// <summary>
        /// 所有控件
        /// </summary>
        public CollectionViewSource ViewSource
        {
            get => _viewSource;
            set => Set(ref _viewSource, value);
        }


        private ControlModel _currentShowControl;
        /// <summary>
        /// 当前显示控件
        /// </summary>
        public ControlModel CurrentShowControl
        {
            get => _currentShowControl;
            set
            {
                Set(ref _currentShowControl, value);
                RaisePropertyChanged(nameof(ControlModel.Content));
                RaisePropertyChanged(nameof(ControlModel.Title));
            }
        }

        /// <summary>
        /// 控件显示
        /// </summary>
        private UserControl _content;

        public UserControl Content
        {
            get
            {
                if (CurrentShowControl == null)
                    return null;
                return (UserControl) Activator.CreateInstance(CurrentShowControl.Content);
            }
            set => Set(ref _content, value);
        }

        /// <summary>
        /// 标题
        /// </summary>
        private string _title;
        public string Title
        {
            get => CurrentShowControl?.Title;
            set => Set(ref _title, value);
        }
    }
}
