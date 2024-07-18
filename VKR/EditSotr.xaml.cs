using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для EditSotr.xaml
    /// </summary>
    public partial class EditSotr : Page
    {
        DipDocumentEntities bd = new DipDocumentEntities();
        public int idp;
        public EditSotr(int idt1)
        {
            InitializeComponent();

            //
           /// roler.Text ="asdasdasdsad";
             //
             idp = idt1;

            var dol = bd.Должность.ToList();
            var nd = dol.Select(j => j.Наименование_должности).Distinct().ToList();
            dol1.ItemsSource = nd;
            var otd = bd.Отдел.ToList();
            var ntd = otd.Select(j => j.Название_отдела).Distinct().ToList();
            otdel.ItemsSource = ntd;
            var rl = bd.Роль.Select(j => j.Наименование_роли).Distinct().ToList();
            roler.ItemsSource = rl;

            if (idp > 0)
            {
                var sotry = bd.Сотрудник.Where(u => u.Код_сотрудника == idp).FirstOrDefault();
                var prof1 = bd.Профиль.Where(u => u.Код_сотрудника == idp).FirstOrDefault();
                var dolg = bd.Должность.Where(u => u.Код_должности == sotry.Код_должности).FirstOrDefault();
                var ot1 = bd.Отдел.Where(u => u.Код_отдела == sotry.Код_отдела).FirstOrDefault();

                var rl1 = bd.Роль.Where(u => u.Код_роли == prof1.Код_роли).FirstOrDefault();
                fam.Text=sotry.Фамилия;
                im.Text = sotry.Имя;
                otch.Text = sotry.Отчество;
                tel.Text = sotry.Контактный_номер;
                ema.Text = sotry.Email;
                dol1.Text = dolg.Наименование_должности;
                otdel.Text = ot1.Название_отдела;
                Ezmen.Content = "Изменить";
                roler.Text = rl1.Наименование_роли;


            }
        }

        private void Cansel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Sotr());
        }

        private void Ezmen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string t = tel.Text.ToString();
                // Regex regex = new Regex(@"[0-9]+"); // исправить регулярное выражение
                bool a = false, c = false;
                c = Regex.IsMatch(t, @"^\+[0-9]{11}$");// если есть знак + в начале
                a = Regex.IsMatch(t, @"^[0-9]{11}$");// проверка на номер телефона
                
                if (fam.Text != "" && im.Text != "" && (a || c) && ema.Text != "" && otdel.Text!="" && dol1.Text != "" )
                {
                   
                    var idd = bd.Должность.Where(p => p.Наименование_должности == dol1.Text).FirstOrDefault();
                    var idot = bd.Отдел.Where(p => p.Название_отдела == otdel.Text).FirstOrDefault();

                    //    MessageBox.Show(Convert.ToInt32(id.Код_должности).ToString());
                    if (idp < 0)
                    {

                        Сотрудник sotr = new Сотрудник()
                        {

                            Фамилия = fam.Text,
                            Имя = im.Text,
                            Отчество = otch.Text,
                            Контактный_номер = tel.Text,
                            Email = ema.Text,
                            Код_должности = Convert.ToInt32(idd.Код_должности),
                            Код_отдела = Convert.ToInt32(idot.Код_отдела)

                        };
                        bd.Сотрудник.Add(sotr);
                        bd.SaveChanges();

                        int ksd = bd.Сотрудник.Max(sot=> sot.Код_сотрудника);
                        int kr = bd.Роль.Where(k=> k.Наименование_роли== roler.Text).FirstOrDefault().Код_роли;
                        Профиль profi = new Профиль()
                        {
                            Код_сотрудника = ksd,
                            Логин = ema.Text,
                            Пароль = "12345",
                            Код_роли = kr
                        };
                        bd.Профиль.Add(profi);
                        bd.SaveChanges();
                        MessageBox.Show("Сотрудник добавлен" + ksd);
              
                        NavigationService?.Navigate(new Sotr());
                    }
                    else
                    {
                        var us = bd.Сотрудник.Where(u => u.Код_сотрудника == idp).FirstOrDefault();

                        us.Фамилия = fam.Text;
                        us.Имя = im.Text;
                        us.Отчество = otch.Text;
                        us.Контактный_номер = tel.Text;
                        us.Email = ema.Text;
                        us.Код_должности = Convert.ToInt32(idd.Код_должности);
                        us.Код_отдела = Convert.ToInt32(idot.Код_отдела);
                        
                        bd.SaveChanges();
                        MessageBox.Show("Сотрудник Изменён");
                        NavigationService?.Navigate(new Sotr());
                    }
                }
            }
            catch
            {
                MessageBox.Show("Проверьте правильность данных");
            }
        }
    }
}
