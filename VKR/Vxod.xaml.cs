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
    /// Логика взаимодействия для Vxod.xaml
    /// </summary>
    public partial class Vxod : Page
    {
        DipDocumentEntities bd = new DipDocumentEntities();
        public Vxod()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();//закрытие программы
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (Log.Text!="" && Password.Password.ToString()!="") //авторизация
            {

                    var user = bd.Профиль.AsNoTracking().FirstOrDefault(u => u.Логин == Log.Text && u.Пароль == Password.Password.ToString());
                if (user != null)
                {
                    var rol = bd.Роль.AsNoTracking().FirstOrDefault(r => r.Код_роли == user.Код_роли);
                    App.Current.Properties["ro"] = rol.Наименование_роли; //переменная которая проверяется на каждой странице при переходе
                    App.Current.Properties["sotr"] = user.Код_сотрудника; // код сотрудника в системе
                    MessageBox.Show(App.Current.Properties["ro"].ToString());
                    NavigationService?.Navigate(new Glpage());
                   
                }
                else {
                    MessageBox.Show("Неправильно введён логин или пароль");
                }


                
            }
            else
            {
                MessageBox.Show("Введите логин и пароль!");
            }
        }
    }
}
