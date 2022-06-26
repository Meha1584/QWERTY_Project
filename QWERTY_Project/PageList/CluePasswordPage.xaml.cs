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

namespace QWERTY_Project
{
    /// <summary>
    /// Логика взаимодействия для CluePasswordPage.xaml
    /// </summary>
    public partial class CluePasswordPage : Page
    {
        var1Entities entities;
        public CluePasswordPage()
        {
            InitializeComponent();
            entities = new var1Entities();
        }

        private void Proverka(object sender, RoutedEventArgs e)
        {
            int login = Convert.ToInt32(inputLogin.Text);
            string fio = inputfio.Text;
            string position = inputPosition.Text;
            if (entities.Worker.Any(x => x.tabNum == login))
            {
                Worker work = entities.Worker.ToList().Find(x => x.tabNum == login);
                if (work.FIO.Equals(fio))
                {
                    if (work.Position1.title.Equals(position))
                    {
                        MessageBox.Show($"Ваш пароль {work.password}");
                        NavigationService.GoBack();
                    }
                    else
                    {
                        MessageBox.Show("Неправильные данные");
                    }
                }
                else
                {
                    MessageBox.Show("Неправильные данные");
                }
            }
            else
            {
                MessageBox.Show("Неправильные данные");
            }
        }
    }
}
