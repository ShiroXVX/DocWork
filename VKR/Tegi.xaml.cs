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

namespace VKR
{
    /// <summary>
    /// Логика взаимодействия для Tegi.xaml
    /// </summary>
    public partial class Tegi : Page
    {
        DipDocumentEntities bd = new DipDocumentEntities();
        public Tegi()
        {
            InitializeComponent();
            var tegs = bd.Список_тегов.ToList().Select(a=> new {Код__тега=a.Код_тега,Наименование__тега=a.Наименование_тега });
            dg.ItemsSource = tegs;
        }

        private void Ad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new EdTagi(-1));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var idq = dg.SelectedCells[0];
                var s = idq.Column.GetCellContent(idq.Item);
                int id = Convert.ToInt32((s as TextBlock).Text);

                NavigationService?.Navigate(new EdTagi(id));
            }
            catch {
                MessageBox.Show("Выберите поле");
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var idq = dg.SelectedCells[0];
                var s = idq.Column.GetCellContent(idq.Item);
                int id = Convert.ToInt32((s as TextBlock).Text);
                if (id == 1)
                {
                    MessageBox.Show("Данный тег нелязя удалить!");
                }
                else
                {
                    var dteg = bd.Список_тегов.Where(w => w.Код_тега == id).FirstOrDefault();
                    bd.Список_тегов.Remove(dteg);
                    bd.SaveChanges();
                    var tegs = bd.Список_тегов.ToList().Select(a => new { Код__тега = a.Код_тега, Наименование__тега = a.Наименование_тега });
                    dg.ItemsSource = tegs;
                }
                
            }
            catch
            {
                MessageBox.Show("Выберите поле");
            }
        }
    }
}
