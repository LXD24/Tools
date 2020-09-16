/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:AduChat"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Tools.Core.Services.Abstractions;
using Tools.Core.Services.Implements;
using Tools.ViewModel.ModuleViewModel;

namespace Tools.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<Image2PdfViewModel>();
            SimpleIoc.Default.Register<ExtractPdfImageViewModel>();
            SimpleIoc.Default.Register<Image2AsciiCharactersViewModel>();

            SimpleIoc.Default.Register<IPdfService, TextPdfService>();
        }

        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();

        public Image2PdfViewModel Image2Pdf => SimpleIoc.Default.GetInstance<Image2PdfViewModel>();

        public ExtractPdfImageViewModel ExtractPdfImage => SimpleIoc.Default.GetInstance<ExtractPdfImageViewModel>();

        public Image2AsciiCharactersViewModel Image2AsciiCharacters => SimpleIoc.Default.GetInstance<Image2AsciiCharactersViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}