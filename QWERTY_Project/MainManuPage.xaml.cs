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
using System.Windows.Shapes;

namespace QWERTY_Project
{
    /// <summary>
    /// Логика взаимодействия для MainManuPage.xaml
    /// </summary>
    public partial class MainManuPage : Window
    {
        var1Entities entities;
        Worker worker;
        public MainManuPage(Worker worker)
        {
            InitializeComponent();
            this.worker = worker;
            entities = new var1Entities();
            frame.NavigationService.Navigate(new MasterPage(entities));
            this.WindowState = WindowState.Maximized;
        }

        private void GoMaster(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new MasterPage(entities));
        }

        private void GoClient(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PageList.ClientPage(entities));
        }

        private void GoZakaz(object sender, RoutedEventArgs e)
        {

        }

        private void GoOborud(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PageList.EquipmentPage(entities));
        }

        private void GoKatalog(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PageList.KatalogPage(entities));
        }
    }
}
