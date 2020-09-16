using AduSkin.Controls.Metro;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Tools.Helper;
using Tools.Models;
using Tools.ViewModel.ModuleViewModel;

namespace Tools.Controls
{
    /// <summary>
    /// Image2Pdf.xaml 的交互逻辑
    /// </summary>
    public partial class Image2Pdf
    {
        #region 表格拖拽

        private Image2PdfViewModel _vm;

        public Image2Pdf()
        {
            InitializeComponent();

            //vm = new Image2PdfViewModel();
            //this.DataContext = vm;

            _vm = (Image2PdfViewModel)this.DataContext;
            this.dg.PreviewMouseLeftButtonDown += dgEmployee_PreviewMouseLeftButtonDown;
            this.dg.MouseMove += dgEmployee_MouseMove;
            this.dg.Drop += dgEmployee_Drop;

            this.dg.DragOver += dg_DragOver;
            this.dg.DragLeave += dg_DragLeave;
        }


        void dg_DragLeave(object sender, System.Windows.DragEventArgs e)
        {
            Popup1.IsOpen = false;
        }

        /// <summary>
        /// 拖动时，对插入行进行着色处理。1，排序拖动；2，外部文件拖放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dg_DragOver(object sender, System.Windows.DragEventArgs e)
        {
            var index = UIHelper.GetDataGridItemCurrentRowIndex(e.GetPosition, dg);
            for (var i = 0; i < dg.Items.Count; i++)
            {
                var r = dg.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                if (r == null) continue;
                if (i != index)
                {
                    r.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    r.BorderThickness = new Thickness(0, 0, 0, 0);
                }
                else
                {
                    r.BorderBrush = new SolidColorBrush(Color.FromRgb(32, 164, 230));
                    var th = new Thickness(0, 0, 0, 0);

                    if (index > _vm.PrevRowIndex) th = new Thickness(0, 0, 0, 1);
                    else if (index < _vm.PrevRowIndex) th = new Thickness(0, 1, 0, 0);
                    r.BorderThickness = th;
                }
            }

            if (!Popup1.IsOpen)
            {
                Popup1.IsOpen = true;
            }
            var popupSize = new Size(Popup1.ActualWidth + 110, Popup1.ActualHeight + 10);
            var p = e.GetPosition(this);
            p.X += 10;
            p.Y += 10;
            Popup1.PlacementRectangle = new Rect(p, popupSize);
            // if (row != null) dg.SelectedItem = row.Item;
        }


        /// <summary>
        /// Defines the Drop Position based upon the index.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgEmployee_Drop(object sender, System.Windows.DragEventArgs e)
        {
            this.Popup1.IsOpen = false;
            var index = UIHelper.GetDataGridItemCurrentRowIndex(e.GetPosition, dg);

            if (index < 0 || index == _vm.PrevRowIndex) return;
            ((DataGridRow)dg.ItemContainerGenerator.ContainerFromIndex(index)).BorderThickness =
                new Thickness(0, 0, 0, 0);


            if (index >= dg.Items.Count - 1)
            {
                index = dg.Items.Count - 1;
            }

            _vm.Images.RemoveAt(_vm.PrevRowIndex);
            _vm.Images.Insert(index, _vm.SelectedItem);
            _vm.PrevRowIndex = -1;
        }

        private void dgEmployee_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_vm.PrevRowIndex < 0) return;
            if (dg.Items.Count <= _vm.PrevRowIndex) return;
            var selectedEmp = dg.Items[_vm.PrevRowIndex] as ImageInfo;
            var dragDropEffects = System.Windows.DragDropEffects.Move;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (selectedEmp == null) return;

                _vm.SelectedItem = selectedEmp;
                if (DragDrop.DoDragDrop(dg, selectedEmp, dragDropEffects)
                    != System.Windows.DragDropEffects.None)
                {
                    dg.SelectedItem = selectedEmp;
                }
            }
        }

        void dgEmployee_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            _vm.PrevRowIndex = UIHelper.GetDataGridItemCurrentRowIndex(e.GetPosition, dg);

            if (_vm.PrevRowIndex < 0) return;
            dg.SelectedIndex = _vm.PrevRowIndex;

            //ImageInfo selectedEmp = dg.Items[prevRowIndex] as ImageInfo;
            //if (selectedEmp == null) return;
            //System.Windows.DragDropEffects dragdropeffects = System.Windows.DragDropEffects.Move;
            //this.vm.SelectedItem = selectedEmp;


            //if (DragDrop.DoDragDrop(dg, selectedEmp, dragdropeffects)
            //                    != System.Windows.DragDropEffects.None)
            //{
            //    dg.SelectedItem = selectedEmp;
            //}
        }

        #endregion

        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is AduFlatButton button)
            {
                _vm.RemoveImage(button.CommandParameter?.ToString());
                if (_vm.Images.Count <= 0) _vm.PrevRowIndex = -1;
            }
        }
    }
}
