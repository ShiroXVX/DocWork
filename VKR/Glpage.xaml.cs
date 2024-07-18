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
    /// Логика взаимодействия для Glpage.xaml
    /// </summary>
    public partial class Glpage : Page
    {
        Sotr ss = new Sotr();
        public Glpage()
        {
            InitializeComponent();

            App.Current.Properties["ro"] = "Администратор";///// 
            App.Current.Properties["sotr"] = 1;/////////////////

            int id1 = Convert.ToInt32(App.Current.Properties["sotr"].ToString());
            string rolName= App.Current.Properties["ro"].ToString();
            switch (rolName)
            {
                case "Cотрудник":
                    teg.Visibility = Visibility.Collapsed;
                  
                    tip.Visibility = Visibility.Collapsed;
                    sot.Visibility = Visibility.Collapsed;
                    break;
                case "Глава отдела":
                    teg.Visibility = Visibility.Collapsed;
                    sot.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Properties["ro"] = "";
            App.Current.Properties["sotr"] = 0;
            NavigationService?.Navigate(new Vxod());
        }

        private void pr_Click(object sender, RoutedEventArgs e)
        {
            Fr.Navigate(new Profil());

        }

        private void d_Click(object sender, RoutedEventArgs e)
        {
            Fr.Navigate(new Doc()); 
        }

        private void teg_Click(object sender, RoutedEventArgs e)
        {
            Fr.Navigate(new Tegi());
        }

       

        private void sot_Click(object sender, RoutedEventArgs e)
        {
            Sotr ssss = new Sotr();
            Fr.Navigate(ssss);
        }

        private void tip_Click(object sender, RoutedEventArgs e)
        {
            Fr.Navigate(new Tips());
        }
    }
}
