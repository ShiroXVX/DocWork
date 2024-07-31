using Microsoft.Win32;
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

using System.Windows.Forms;

namespace VKR
{
    /// <summary>
    /// Логика взаимодействия для Profil.xaml
    /// </summary>
    public partial class Profil : Page
    {
        DipDocumentEntities bd = new DipDocumentEntities();
        int id1 = Convert.ToInt32(App.Current.Properties["sotr"].ToString());
        int pr1;
        public Profil()
        {
            InitializeComponent();
            // вывод информации об авторизованном пользователе
            var st = bd.Сотрудник.Join(bd.Профиль,  
                s => s.Код_сотрудника,
                b => b.Код_сотрудника,
                (s, b) => new
                {
                    s.Код_сотрудника,
                    b.Код_профиля,
                    s.Имя,
                    s.Фамилия,
                    s.Отчество,
                    s.Контактный_номер,
                    s.Email,
                    b.Путь_загрузки,
                    b.Пароль
                }
              ).Where(q=> q.Код_сотрудника==id1).FirstOrDefault();
            fam.Content =st.Фамилия;
            im.Content = st.Имя;
            otch.Content = st.Отчество;
            tel.Content = st.Контактный_номер;
            dol.Content = st.Email;
            zg.Content = st.Путь_загрузки;
            pr1 = st.Код_профиля;
        }

        private void del_Click(object sender, RoutedEventArgs e) //выбор пути скачивания документов
        {
  

             var fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                zg.Content = fb.SelectedPath;
                var pr = bd.Профиль.Where(a=> a.Код_профиля==pr1).FirstOrDefault();
                pr.Путь_загрузки = fb.SelectedPath;
                bd.SaveChanges();
            }
        }

        private void Par_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
