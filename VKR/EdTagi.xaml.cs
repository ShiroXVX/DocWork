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
    /// Логика взаимодействия для EdTagi.xaml
    /// </summary>
    public partial class EdTagi : Page
    {
        DipDocumentEntities bd = new DipDocumentEntities();
        int id;
        public EdTagi(int idt)
        {
            InitializeComponent();
            id = idt;
            if (idt > 0)
            {
                var t = bd.Список_тегов.Where(a=> a.Код_тега==idt).FirstOrDefault();
                teg.Text = t.Наименование_тега.ToString();
                Ezmen.Content = "Изменить";
            }
        }

        private void Ezmen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (teg.Text != "")
                {
                    if (id < 0)
                    {
                        Список_тегов tegi = new Список_тегов()
                        {
                            Наименование_тега = teg.Text
                        };
                        bd.Список_тегов.Add(tegi);
                        bd.SaveChanges();

                        NavigationService?.Navigate(new Tegi());
                    }
                    else
                    {
                        var t = bd.Список_тегов.Where(a => a.Код_тега == id).FirstOrDefault();
                        t.Наименование_тега = teg.Text;
                        bd.SaveChanges();
                        NavigationService?.Navigate(new Tegi());
                    }
                }
                else
                {
                    MessageBox.Show("Заполните поля");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cansel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Tegi());
        }
    }
}
