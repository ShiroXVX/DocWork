using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace VKR
{
    /// <summary>
    /// Логика взаимодействия для Tips.xaml
    /// </summary>
    public partial class Tips : Page
    {
        DipDocumentEntities bd = new DipDocumentEntities();

        private byte[] ConvertImageToByteArrayPng(BitmapImage imageSource) // перевод .png в байт
        {
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageSource));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                return ms.ToArray();
            }
        }

       /* private BitmapImage Vivod(byte[] imageSource)
        {
            MemoryStream memorystream = new MemoryStream();
            memorystream.Write(imageSource, 0, (int)imageSource.Length);
            memorystream.Seek(0, SeekOrigin.Begin);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = memorystream;
            image.EndInit();
          
            return image;
        }*/

        private byte[] ConvertImageToByteArrayJpeg(BitmapImage imageSource) // перевод .jpg в байт
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageSource));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                return ms.ToArray();
            }
        }
        
        public byte[] b;

        void VivodDg()
        {
            var doc = bd.Тип_документа.ToList().Select(a => new
            { a.Код_типа, a.Название_типа, a.Значёк }

            );
            dg.ItemsSource = doc;
        }

        public Tips()
        {
            InitializeComponent();

            VivodDg();

        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            /* var idq = dg.SelectedCells[0];
             var s = idq.Column.GetCellContent(idq.Item);
             int id = Convert.ToInt32((s as TextBlock).Text);
             NavigationService?.Navigate(new EditSotr(id));*/
            try {
                var idq = dg.SelectedCells[0];
                var s = idq.Column.GetCellContent(idq.Item);
                int id = Convert.ToInt32((s as TextBlock).Text);


                OpenFileDialog op = new OpenFileDialog();
                if ((bool)op.ShowDialog()) //проверка на выбор файла
                {
                    string fName = op.FileName.ToString();

                    if (fName.EndsWith(".png"))
                    {
                        BitmapImage image = new BitmapImage(new Uri(op.FileName));
                        b = ConvertImageToByteArrayPng(image);

                        var us = bd.Тип_документа.Where(u => u.Код_типа == id).FirstOrDefault();
                        us.Значёк = b;
                        bd.SaveChanges();
                       // MessageBox.Show("Картинка добавлена");
                        VivodDg();


                    }
                    else if (fName.EndsWith(".jpeg") || fName.EndsWith(".jpg"))
                    {
                        BitmapImage image = new BitmapImage(new Uri(op.FileName));
                        b = ConvertImageToByteArrayJpeg(image);

                        var us = bd.Тип_документа.Where(u => u.Код_типа == id).FirstOrDefault();
                        us.Значёк = b;
                        bd.SaveChanges();
                       // MessageBox.Show("Картинка добавлена");
                        VivodDg();
                    }
                    else
                        MessageBox.Show("Данный формат не поддерживается");
                }

            }
            catch
            {
                MessageBox.Show("Выберите поле");
            }
        }
    }
}
/*       List<Тип_документа> d = new List<Тип_документа>();


       foreach (var pp in sotr)
       {
           d.Add(new Тип_документа() { Код_типа = pp.Код_типа, Название_типа = pp.Название_типа, Значёк = pp.Значёк });
       }*/
//  voprImage.Source = Vivod(b);
