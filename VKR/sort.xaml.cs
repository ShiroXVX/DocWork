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

namespace VKR ////////// УДАЛИТЬ СТРАНИЦУ - НЕ ОТНОСИТСЯ К ПРОЕКТУ
{
    /// <summary>
    /// Логика взаимодействия для sort.xaml
    /// </summary>
    public partial class sort : Page
    {
        public sort()
        {
            InitializeComponent();
        }

        public void Vivod(int[] rt)
          {
            for (int i = 0; i < rt.Length; i++)
            {
                ns.Content += " " + rt[i].ToString();
                if(i== rt.Length - 1)
                {
                    ns.Content +=""+'\n' ;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int[] nsor= {13, 27, 731, 322, 385, 54, 1, 97 };
            double[] nsor2 = {12.5,12.4};
            /*for (int i = 0; i < nsor.Length; i++)
            {
                ns.Content += " "+ nsor[i].ToString();
             }*/
            Vivod(nsor);

            for (int i = 0;i < nsor.Length; i++ )
            {
                for (int j = 0; j < nsor.Length-1; j++)
                {
                    if(nsor[j]>nsor[j+1])
                    {
                        int b = nsor[j];
                        //(nsor[j], nsor[j + 1]) = (nsor[j + 1], nsor[j]);
                        //  nsor[j] = nsor[j] ^ nsor[j + 1];
                        // nsor[j + 1]= nsor[j] ^ nsor[j + 1];
                        // nsor[j] = nsor[j] ^ nsor[j + 1];

                        nsor[j] = nsor[j + 1];
                        nsor[j + 1] = b;
                    }
                }
            }

            /* for (int i = 0; i < nsor.Length; i++)
             {
                 s.Content += " " + nsor[i].ToString();
             }*/
            Vivod(nsor);


        }
    }
}
