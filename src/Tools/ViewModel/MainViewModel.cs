using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using Tools.Views;

namespace Tools.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {

        }

        private SolidColorBrush _mainBackground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        /// <summary>
        /// 属性.
        /// </summary>
        public SolidColorBrush MainBackground
        {
            get => _mainBackground;
            set => Set(ref _mainBackground, value);
        }

        private UserControl _pdfTool = new PdfTool();
        public UserControl PdfTool
        {
            get => _pdfTool;
            set => Set(ref _pdfTool, value);
        }


        private UserControl _imageTool = new ImageTool();
        public UserControl ImageTool
        {
            get => _imageTool;
            set => Set(ref _imageTool, value);
        }
    }
}
