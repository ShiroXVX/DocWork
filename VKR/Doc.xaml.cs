using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.IO.Compression;
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
using Word = Microsoft.Office.Interop.Word;
using Exel = Microsoft.Office.Interop.Excel;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Spire.Pdf;
using System.Drawing.Imaging;
using Microsoft.Office.Interop.Word;
using Spire.Pdf.Graphics;
using System.Drawing;
using Spire.Xls;
using Spire.Doc;
using Spire.Doc.Documents;


//using Microsoft.Office.Interop.Excel;

namespace VKR
{
    /// <summary>
    /// Логика взаимодействия для Doc.xaml
    /// </summary>
    public partial class Doc : System.Windows.Controls.Page
    {
        DipDocumentEntities bd = new DipDocumentEntities();
        Object wrap = Word.WdFindWrap.wdFindContinue;
        Object missing = Type.Missing;
        int idb; //переменная id загружаемого файла
     

        public void Vivod() // вывод файлов в датагрид
        {
            var docu = bd.Документ.ToList();
            var tipDoc = bd.Тип_документа.ToList();
            var itogTable = docu.Join( //Соединяем таблицы Документ и Тип_документа
                tipDoc,
                s => s.Код_типа,
                d => d.Код_типа,
                (s, d) => new
                {
                    s.Код_документа,
                    d.Значёк,
                    s.Название_документа
                }
                );
            dg.ItemsSource = itogTable;
        }
        public Doc()
        {
            InitializeComponent();
             Vivod();

            var rr = bd.Список_тегов.Select(t=>t.Наименование_тега).ToList();
            rr.Insert(0,"");
            tit.ItemsSource = rr;

            allteg.ItemsSource = rr;
        }



            public void TgW(string sl) // нахождение определённого тега в word на вход подаётся расположение файла
        {
            var spt = bd.Список_тегов.ToList();
            int kt = 0;
            var app = new Word.Application();
            app.Documents.Open(sl);
            Word.Find find = app.Selection.Find;
            foreach (var t in spt)
            {
               // MessageBox.Show(t.Наименование_тега);
                find.Text = t.Наименование_тега;

               if(find.Execute(FindText: missing,
                    MatchCase: false,
                    MatchWholeWord: false,
                    MatchWildcards: false,
                    MatchSoundsLike: false,
                    MatchAllWordForms: false,
                    Forward: false,
                    Wrap: wrap,
                    Format: false,
                    ReplaceWith: missing, Replace: missing
                    ))
                {
                    Тег_файла doc = new Тег_файла()
                    {

                        Код_документа = idb,
                        Код_тега = t.Код_тега
                    };
                    bd.Тег_файла.Add(doc);
                    bd.SaveChanges();
                    kt++;
                   // MessageBox.Show(t.Наименование_тега+"  asdasdsadasdsadsasadasd");
                }
              //  else { MessageBox.Show("xy"); }
            }
            if (kt == 0)
            {
                Тег_файла doc = new Тег_файла()
                {

                    Код_документа = idb,
                    Код_тега = 1
                };
                bd.Тег_файла.Add(doc);
                bd.SaveChanges();
            }
            app.ActiveDocument.Close();
            app.Quit();
        }

        //dd//////////////////////////////////////////////////////////////////////////////////
        public void TgEX(string sl1) // нахождение определённого тега в exel на вход подаётся расположение файла
        {
            string sl = sl1;
            var spt = bd.Список_тегов.ToList();
            int kt = 0;// проверка на наличие тегов

            try
            {
                var app = new Exel.Application();
                    var wr = app.Workbooks.Open(sl);
                
                    var wrk = wr.ActiveSheet;

                    int rc = wrk.UsedRange.Rows.Count;
                    int cc = wrk.UsedRange.Columns.Count;

                foreach (var t in spt)
                {
                   

                    for (int row = 1; row <= rc; row++)
                    {
                        for (int col = 1; col <= cc; col++)
                        {
                            Exel.Range cell = wrk.Cells[row, col];
                            string cv = cell.Value2?.ToString();

                            if (!string.IsNullOrEmpty(cv) && cv.Equals(t.Наименование_тега, StringComparison.OrdinalIgnoreCase))
                            {
                                Тег_файла doc = new Тег_файла()
                                {

                                    Код_документа = idb,
                                    Код_тега = t.Код_тега
                                };
                                bd.Тег_файла.Add(doc);
                                bd.SaveChanges();
                                kt++;
                               // MessageBox.Show(t.Наименование_тега + "  asdasdsadasdsadsasadasd");
                            }

                        }
                    }
                   }
                if (kt == 0)
                {
                    Тег_файла doc = new Тег_файла()
                    {

                        Код_документа = idb,
                        Код_тега = 1
                    };
                    bd.Тег_файла.Add(doc);
                    bd.SaveChanges();
                }
                wr.Close();
                app.Quit();

            }
            catch { }
        }
        //xl//////////////////////////////////////////////////////////////////////////////////////////////

        //ppppppddddffffffffffff//////////////////////////////////////////////////////////////////////////////////
        public void TgPd(string sl1) // нахождение определённого тега в word на вход подаётся расположение файла
        {
            string sl = sl1;
            var spt = bd.Список_тегов.ToList();
            int kt = 0;// проверка на наличие тегов

            try
            {


                foreach (var t in spt)
                {

                    PdfReader pdfr = new PdfReader(sl);
                    for (int p = 1; p <= pdfr.NumberOfPages; p++)
                    {
                        string pagText = PdfTextExtractor.GetTextFromPage(pdfr, p);
                       // MessageBox.Show(pagText);
                        if (pagText.Contains(t.Наименование_тега))
                        {
                            Тег_файла doc = new Тег_файла()
                            {

                                Код_документа = idb,
                                Код_тега = t.Код_тега
                            };
                            bd.Тег_файла.Add(doc);
                            bd.SaveChanges();
                            kt++;
                           // MessageBox.Show(t.Наименование_тега + "  asdasdsadasdsadsasadasd"); 
                        }
                    }
                    pdfr.Close(); //// проверить
                }
                if (kt == 0)
                {
                    Тег_файла doc = new Тег_файла()
                    {

                        Код_документа = idb,
                        Код_тега = 1
                    };
                    bd.Тег_файла.Add(doc);
                    bd.SaveChanges();
                }
            }
            catch { }
        }
        //ppppppddddffffffffffff//////////////////////////////////////////////////////////////////////////////////////////////

        public static byte[] LoadCompressFile(string FileName) //получение масива в бит виде
        {
            FileStream file = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] buffer = new byte[file.Length];
            file.Read(buffer, 0, buffer.Length);
            file.Close();
            

            return buffer;
        }

        private void zag_Click(object sender, RoutedEventArgs e)/////////////////////////// КНОПКА Найти
        {
            string pt= pet.Text,
                teg1=tit.Text;
            var pft = bd.Документ.ToList();
            var tipDoc = bd.Тип_документа.ToList();
            var tf = bd.Тег_файла.ToList();

            var itogTable = pft.Join( //Соединяем таблицы должность и сотрудник
                   tipDoc,
                   s => s.Код_типа,
                   d => d.Код_типа,
                   (s, d) => new
                   {
                       s.Код_документа,
                       d.Значёк,
                       s.Название_документа
                   }
                   );
            if (rpo.IsChecked != true)
            {
                
                if (teg1 == "" && pt != "")
                {
                    var itogTable2 = itogTable.Where(a => a.Название_документа.ToLower().Contains(pt.ToLower()));
                    dg.ItemsSource = itogTable2;
                }
                else if (teg1 != "" && pt == "")
                {
                    int idteg = bd.Список_тегов.Where(a => a.Наименование_тега == teg1).ToList().FirstOrDefault().Код_тега;
                    var itogTable3 = itogTable.Join(
                        tf,
                        s => s.Код_документа,
                       d => d.Код_документа,
                       (s, d) => new
                       {
                           s.Код_документа,
                           s.Значёк,
                           s.Название_документа,
                           d.Код_тега

                       }
                        ).Where(q => q.Код_тега == idteg).Distinct();
                    dg.ItemsSource = itogTable3;

                }
                else if (teg1 != "" && pt != "")
                {
                    int idteg = bd.Список_тегов.Where(a => a.Наименование_тега == teg1).ToList().FirstOrDefault().Код_тега;
                    var itogTable3 = itogTable.Join(
                        tf,
                        s => s.Код_документа,
                       d => d.Код_документа,
                       (s, d) => new
                       {
                           s.Код_документа,
                           s.Значёк,
                           s.Название_документа,
                           d.Код_тега

                       }
                        ).Where(q => q.Код_тега == idteg).Distinct();

                    var itogTable4 = itogTable3.Where(a => a.Название_документа.ToLower().Contains(pt.ToLower()));

                    dg.ItemsSource = itogTable4;
                }
                else
                {
                    Vivod();
                }
            }
            else if (rpo.IsChecked == true)
            {
                if (allteg.SelectedItem == null && pt != "")
                {
                    var itogTable2 = itogTable.Where(a => a.Название_документа.ToLower().Contains(pt.ToLower()));
                    dg.ItemsSource = itogTable2;
                }
                else if (allteg.SelectedItem != null && pt == "")
                {
                    var itogTable3 = bd.Документ.Join(
                       bd.Тип_документа,
                       s => s.Код_типа,
                      d => d.Код_типа,
                      (s, d) => new
                      {
                          s.Код_документа,
                          s.Название_документа,
                          d.Значёк
                      }
                       ).ToList();
                    var cd2 = tf.Select(x => x.Код_документа);
                    var cd3 = tf;
                    foreach (var tg5 in allteg.SelectedItems)
                    {

                        int idteg = bd.Список_тегов.Where(a => a.Наименование_тега == tg5.ToString()).ToList().Select(q => q.Код_тега).FirstOrDefault(); // id тега
                        var tf1 = tf.Where(a => a.Код_тега == idteg).Select(x => x.Код_документа).ToList(); // id фалов с данным тегом

                        cd3 = cd3.Where(a => tf1.Contains(a.Код_документа)).ToList();//id файлов со всеми тегами                         
                        ;
                    }
                    var rrr = itogTable3.Where(a => cd3.Select(x => x.Код_документа).Contains(a.Код_документа)).Distinct();
                    dg.ItemsSource = rrr;
                }
                else if (allteg.SelectedItem != null && pt != "")
                {
                    // var ww = bd.Список_тегов.Where(a => !tegest.Select(x => x.Код_тега).ToList().Contains(a.Код_тега)).Select(q => q.Наименование_тега).ToList();                   
                    var itogTable3 = bd.Документ.Join(
                        bd.Тип_документа,
                        s => s.Код_типа,
                       d => d.Код_типа,
                       (s, d) => new
                       {
                           s.Код_документа,
                           s.Название_документа,
                           d.Значёк
                       }
                        ).ToList();
                    var cd2 = tf.Select(x => x.Код_документа);
                    var cd3 = tf;
                    foreach (var tg5 in allteg.SelectedItems)
                    {

                        int idteg = bd.Список_тегов.Where(a => a.Наименование_тега == tg5.ToString()).ToList().Select(q => q.Код_тега).FirstOrDefault(); // id тега
                        var tf1 = tf.Where(a => a.Код_тега == idteg).Select(x => x.Код_документа).ToList(); // id фалов с данным тегом

                        cd3 = cd3.Where(a => tf1.Contains(a.Код_документа)).ToList();//id файлов со всеми тегами                         
                        
                    }
                    var rrr = itogTable3.Where(a => cd3.Select(x => x.Код_документа).Contains(a.Код_документа)).Distinct();


                    var itogTable4 = rrr.Where(a => a.Название_документа.ToLower().Contains(pt.ToLower()));

                    dg.ItemsSource = rrr;
                }
                else
                {
                    Vivod();
                }
            }
            
          /*  OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            string sl = openFileDialog1.FileName;
            var spt = bd.Список_тегов.ToList();

            PdfReader pdfr = new PdfReader(sl);
            for(int p=1; p <= pdfr.NumberOfPages; p++)
            {
                string pagText = PdfTextExtractor.GetTextFromPage(pdfr, p);
                MessageBox.Show(pagText);
                if (pagText.Contains("Руководитель"))
                {
                    MessageBox.Show("eeeeexxxx555555555555");
                }
            }*/



        }

        private void ad_Click(object sender, RoutedEventArgs e)
        {

          //  try
            {

                OpenFileDialog op = new OpenFileDialog();
                if ((bool)op.ShowDialog()) //проверка на выбор файла
                {
                    string fName = op.FileName.ToString();                  
                    string[] fn = fName.Split('\\');  //название документа с .
                    string[] faN = fn[fn.Length - 1].Split('.'); // название и тип файла
                    var idtip1 = bd.Тип_документа.ToList().Where(a => a.Название_типа == "." + faN[faN.Length - 1].ToString()).FirstOrDefault();
                    int idtip = idtip1.Код_типа;

                    byte[] ppd;// массив байто для сохранения картинки превью

                    var idm = bd.Документ.Max(p => p.Код_документа).ToString(); // получаем id файла
                    idb = Convert.ToInt32(idm) + 1;
                    Документ doc = new Документ()
                    {

                        Код_документа = idb,
                        Код_типа = idtip,
                        Код_раздела = 1,
                        Документ1 = LoadCompressFile(fName),
                        Название_документа = faN[0].ToString()

                    };
                    bd.Документ.Add(doc);
                    bd.SaveChanges();

                    if (fName.EndsWith(".doc") || fName.EndsWith(".docx"))
                    {
                        TgW(fName);

                        Spire.Doc.Document ddoc = new Spire.Doc.Document();
                        ddoc.LoadFromFile(fName);
                        System.Drawing.Image image = ddoc.SaveToImages(0, Spire.Doc.Documents.ImageType.Bitmap);
                        ppd = ImageToByteArray(image);
                        ddoc.Close();
                        Превью doc1 = new Превью()
                        {
                            Код_документа = idb,
                            Превью1 = ppd
                        };
                        bd.Превью.Add(doc1);
                        bd.SaveChanges();
                        // MessageBox.Show("yra");
                    }

                    else if (fName.EndsWith(".pdf"))
                    {
                        TgPd(fName);// загрузка тегов
                        MessageBox.Show(fName);
                        Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
                        pdf.LoadFromFile(fName);                        
                        System.Drawing.Image image = pdf.SaveAsImage(0, PdfImageType.Bitmap, 500, 500);                  
                        ppd = ImageToByteArray(image); // преобразование в двоичный код
                        pdf.Close();
                        Превью doc1 = new Превью()
                        {
                            Код_документа = idb,
                            Превью1 = ppd
                        };
                        bd.Превью.Add(doc1);
                        bd.SaveChanges();
                        


                    }
                    else if (fName.EndsWith(".xlsx") || fName.EndsWith(".xls"))
                    {
                        TgEX(fName);

                        Workbook workbook = new Workbook();
                        workbook.LoadFromFile(fName);
                        //System.Drawing.Image image = workbook.Worksheets[0].SaveToImage(1, 1, 19, 10);
                       // ppd = ImageToByteArray(image);
                       // Превью doc1 = new Превью()
                       // {
                       // //    Код_документа = idb,
                       //     Превью1 = ppd
                      //  };
                      //  bd.Превью.Add(doc1);
                       // bd.SaveChanges();
                        //MessageBox.Show("eeeeexxxx");
                    }
                    else
                        MessageBox.Show("Данный формат не поддерживается");
                }
            }
           // catch { }




            /* OpenFileDialog openFileDialog1 = new OpenFileDialog();
             openFileDialog1.ShowDialog();
             pet.Text = openFileDialog1.FileName;// выбор файла (его пути)*/
            Vivod();


        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {

            try
            {

            var idq = dg.SelectedCells[0];
                var s = idq.Column.GetCellContent(idq.Item);
                int id5 = Convert.ToInt32((s as TextBlock).Text);
                NavigationService?.Navigate(new SvFile(id5));
                

            }
            catch
            {
                MessageBox.Show("Выберите поле");

            }


        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                var idq = dg.SelectedCells[0];
                var s = idq.Column.GetCellContent(idq.Item);
                int id5 = Convert.ToInt32((s as TextBlock).Text);
                var dd1 = bd.Документ.Where(d=> d.Код_документа==id5).FirstOrDefault();
                var dd1del = bd.Документ.Remove(dd1);
                bd.SaveChanges();
                Vivod();
            }
            catch
            {
                MessageBox.Show("Выберите поле");

            }
        }

        private BitmapImage SourceToBitmap(BitmapSource source)//////

        {
            using (MemoryStream ms = new MemoryStream())
            {

                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(source));
                encoder.Save(ms);
                var image = new BitmapImage();
               // image.BeginInit();
                image.StreamSource = ms;
               // image.EndInit();

                // bmp = new WriteableBitmap(ms); 
                return image;

            }
            
        }   



        public byte[] ImageToByteArray(System.Drawing.Image imageIn) //преобразование в массив бит
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }

        private void zagryz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var idq = dg.SelectedCells[0];
                var s = idq.Column.GetCellContent(idq.Item);
                int id5 = Convert.ToInt32((s as TextBlock).Text);


                var tp1 = bd.Тип_документа;

                var dd5 = bd.Документ.Join(tp1,
                   qw => qw.Код_типа,
                   d => d.Код_типа,
                   (qw, d) => new
                   {
                       qw.Код_документа,
                       d.Название_типа,
                       qw.Название_документа,
                       qw.Документ1
                   }
                   ).Where(d => d.Код_документа == id5).FirstOrDefault();

             int ids=  Convert.ToInt32(App.Current.Properties["sotr"].ToString());/////////
                string strpod = bd.Профиль.Where(x => x.Код_сотрудника == ids).FirstOrDefault().Путь_загрузки;
                string imeF = dd5.Название_документа+dd5.Название_типа;
                string filePath = System.IO.Path.Combine(strpod, imeF);// путь и название файла, в названии указывается тип файла doc xlsx pdf и тд
                File.WriteAllBytes(filePath, dd5.Документ1);//загрузка файла*/
            }
            catch
            {
                MessageBox.Show("Выберите документ!");

            }
        }

        private void rpo_Checked(object sender, RoutedEventArgs e)
        {
            allteg.Visibility = Visibility.Visible;
            tit.Visibility= Visibility.Collapsed;
        }

        private void rpo_Unchecked(object sender, RoutedEventArgs e)
        {
            allteg.Visibility = Visibility.Collapsed;
            tit.Visibility = Visibility.Visible;
        }
    }
}
