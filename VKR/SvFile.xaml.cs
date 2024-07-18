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
    /// Логика взаимодействия для SvFile.xaml
    /// </summary>
    public partial class SvFile : Page
    {
        DipDocumentEntities bd = new DipDocumentEntities();
        int iddoc;

        private BitmapImage Vivod(byte[] imageSource) 
        {
            MemoryStream memorystream = new MemoryStream();
            memorystream.Write(imageSource, 0, (int)imageSource.Length);
            memorystream.Seek(0, SeekOrigin.Begin);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = memorystream;
            image.EndInit();

            return image;
        }

        void updateSels()
        {
            var tegest = bd.Тег_файла.Where(a => a.Код_документа == iddoc);
            var qwe = bd.Список_тегов;
            var tt = tegest.Join(qwe,
                q => q.Код_тега,
                s => s.Код_тега,
              (q, s) => new
              {
                  s.Наименование_тега
              }
              ).Select(q => q.Наименование_тега).ToList();
            tegg.ItemsSource = tt;

            var ww = bd.Список_тегов.Where(a => !tegest.Select(x => x.Код_тега).ToList().Contains(a.Код_тега)).Select(q => q.Наименование_тега).ToList();
            teggAd.ItemsSource = ww;
        }

        public SvFile(int id)
        {
            InitializeComponent();
            iddoc = id;

            var bqq = bd.Превью.Where(a=> a.Код_документа==id).FirstOrDefault();
            cart.Source = Vivod(bqq.Превью1);
            updateSels();


        }

        private void Cansel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Doc());
        }

        private void AdTeg_Click(object sender, RoutedEventArgs e)
        {
            if(teggAd.SelectedItem!=null)
            {
                int at = bd.Список_тегов.Where(x=> x.Наименование_тега== teggAd.SelectedItem.ToString()).FirstOrDefault().Код_тега;
                Тег_файла tf = new Тег_файла()
                {
                    Код_документа=iddoc,
                    Код_тега=at
                };
                bd.Тег_файла.Add(tf);
                bd.SaveChanges();
                updateSels();
            }
            else
            {
                MessageBox.Show("Выберите тег для добавления");
            }
        }

        private void DelTeg_Click(object sender, RoutedEventArgs e)
        {
            if (tegg.SelectedItem != null)
            {
                int at = bd.Список_тегов.Where(x => x.Наименование_тега == tegg.SelectedItem.ToString()).FirstOrDefault().Код_тега;
                var dtg = bd.Тег_файла.Where(y=> y.Код_тега==at && y.Код_документа==iddoc).FirstOrDefault();
                bd.Тег_файла.Remove(dtg);                
                bd.SaveChanges();
                updateSels();
            }
            else
            {
                MessageBox.Show("Выберите тег для удаления");
            }
        }
    }
}
