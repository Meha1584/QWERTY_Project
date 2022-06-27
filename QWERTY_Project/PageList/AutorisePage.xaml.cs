using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AutorisePage.xaml
    /// </summary>
    public partial class AutorisePage : Page
    {
        Window Window2;
        var1Entities entities;
        public AutorisePage(var1Entities _context, Window mainWindow)
        {
            InitializeComponent();
            this.entities = _context;
            this.Window2 = mainWindow;
            
        }
        int count = 0;
        private void CheckEmployeer(object sender, RoutedEventArgs e)
        {
            int login = Convert.ToInt32(inputLogin.Text);
            string password = inputPassword.Password;
            
            if (entities.Worker.Any(x=> x.tabNum == login))
            {
                Worker worker = entities.Worker.ToList().Find(x=> x.tabNum == login);
                if (worker.password.Equals(password))
                {
                    MessageBox.Show($"Здравствуйте, {worker.FIO}");
                    MainManuPage window = new MainManuPage(worker);
                    window.Closed += Window_Closed1;
                    Window2.Hide();
                    window.ShowDialog();
                }
                else
                {
                    count++;
                    MessageBox.Show($"Неверные входные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                count++;
                MessageBox.Show($"Неверные входные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (count >= 3)
            {
                clueButton.Visibility = Visibility.Visible;
            }
        }

        private void Window_Closed1(object sender, EventArgs e)
        {
            Window2.Close();
        }

        private void goClueButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CluePasswordPage());
        }
    }
}
