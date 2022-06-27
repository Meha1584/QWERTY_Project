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

namespace QWERTY_Project.PageList
{
    /// <summary>
    /// Логика взаимодействия для CreateOrderPage.xaml
    /// </summary>
    public partial class CreateOrderPage : Page
    {
        var1Entities entities;
        public CreateOrderPage(var1Entities entities)
        {
            InitializeComponent();
            this.entities = entities;
        }

        private void GoCteareOrder(object sender, RoutedEventArgs e)
        {
            
            NavigationService.Navigate(new CheckClientPage(entities));
        }

        private void GoCteareClient(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateCllient(entities));
        }
    }
}
