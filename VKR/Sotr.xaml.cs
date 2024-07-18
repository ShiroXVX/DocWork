using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для Sotr.xaml
    /// </summary>
    public partial class Sotr : Page
    {
        DipDocumentEntities bd = new DipDocumentEntities();

        void Vivod() //процедура вывода данных в датагрид
        {
            var sotr = bd.Сотрудник.ToList();
            var dol = bd.Должность.ToList();
            var otd = bd.Отдел.ToList();
            var itogTable = sotr.Join( //Соединяем таблицы должность и сотрудник
                dol,
                s => s.Код_должности,
                d => d.Код_должности,
                (s, d) => new
                {
                    Код__сотрудника = s.Код_сотрудника,
                    s.Имя,
                    s.Фамилия,
                    s.Отчество,
                    s.Контактный_номер,
                    s.Email,
                    Наименование__должности = d.Наименование_должности,
                    s.Код_отдела
                }
                );

            var itogTable2 = itogTable.Join( //Соединяем таблицы должность и сотрудник
              otd,
              s => s.Код_отдела,
              d => d.Код_отдела,
              (s, d) => new
              {
                  s.Код__сотрудника,
                  s.Имя,
                  s.Фамилия,
                  s.Отчество,
                  Контактный__номер=s.Контактный_номер,
                  s.Email,
                  s.Наименование__должности ,
                  Название__отдела=d.Название_отдела
              }
              );

            dg.ItemsSource = itogTable2; //выводим преобразованную таблицу
           
        }
        public Sotr()
        {
            InitializeComponent();
            Vivod();
        }

        public async void qweAsync()/////////////////////////////////////////////////////////////////////////////// и Loaded на странице
        {
            await Task.Delay(1); // асихронный метод, чтобы выполнить последним
            dg.Columns[0].MaxWidth = 0;// 1 колонка с id становится не видной пользователю
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
              qweAsync();
        }

        private void ad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new EditSotr(-1));
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                if (dg.Columns.Count > 0)//// пофиксить
                {
                    dg.Columns[0].MaxWidth = 0;
                }
                var idq = dg.SelectedCells[0];
                var s = idq.Column.GetCellContent(idq.Item);
                int id = Convert.ToInt32((s as TextBlock).Text);
             
                NavigationService?.Navigate(new EditSotr(id));
            }
            catch 
            {
                MessageBox.Show("Выберите поле");

            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var idq = dg.SelectedCells[0];
                var s = idq.Column.GetCellContent(idq.Item);
                int id5 = Convert.ToInt32((s as TextBlock).Text);
                var dd1 = bd.Сотрудник.Where(d => d.Код_сотрудника == id5).FirstOrDefault();
                var dd1del = bd.Сотрудник.Remove(dd1);
                bd.SaveChanges();
                Vivod();
            }
            catch
            {
                MessageBox.Show("Выберите поле");

            }
        }
    }
}
