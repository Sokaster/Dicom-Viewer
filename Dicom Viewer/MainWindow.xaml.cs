using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;
using System.Drawing;
using FellowOakDicom.Serialization;
using System.IO;
using Dicom.Imaging;
using Dicom.Imaging.Render;
using Dicom;

namespace Dicom_Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        // Базовый обработчик для кнопки "Open DICOM Image"
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "DICOM Files (*.dcm)|*.dcm";

            bool? isOpenFileDialogSucess = openFileDialog.ShowDialog();
            if (isOpenFileDialogSucess == true)
            {
                ReadDicomFile(openFileDialog.FileName);
            }
        }

        public void ReadDicomFile(string filePath)
        {
            DicomFile dicomFile = DicomFile.Open(filePath);

            var dicomImage = new DicomImage(dicomFile.Dataset);
            System.Drawing.Bitmap bmp = dicomImage.RenderImage(0).AsClonedBitmap();

            // Convert bitmap to bitmapsource
            var bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                bmp.GetHbitmap(),
                                IntPtr.Zero,
                                Int32Rect.Empty,
                                BitmapSizeOptions.FromEmptyOptions());

            // set the image control source to display the image
            System.Windows.Controls.Image myImage = (System.Windows.Controls.Image)FindName("DicomImage");
            myImage.Source = bitmapSource;
        }
    }
}
