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
    /// Логика взаимодействия для CheckClientPage.xaml
    /// </summary>
    public partial class CheckClientPage : Page
    {
        var1Entities entities;
        public CheckClientPage(var1Entities entities)
        {
            InitializeComponent();
            this.entities = entities;
        }

        private void CheckClient(object sender, RoutedEventArgs e)
        {
            string serial = inputSerial.Text;
            if (entities.Client.Any(x=> x.serialPass.Equals(serial)))
            {
                MessageBox.Show("Клиент найден");
                Client client = entities.Client.ToList().Find(x => x.serialPass.Equals(serial));
                NavigationService.Navigate(new PageList.CreateOrderPageWithClient(client, entities));
            }
        }
    }
}
