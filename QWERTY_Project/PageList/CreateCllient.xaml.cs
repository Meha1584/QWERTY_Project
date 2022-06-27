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
    /// Логика взаимодействия для CreateCllient.xaml
    /// </summary>
    public partial class CreateCllient : Page
    {
        var1Entities entities;
        public CreateCllient(var1Entities entities)
        {
            InitializeComponent();
            this.entities = entities;
        }

        private void AddClient(object sender, RoutedEventArgs e)
        {
            string name = inputFio.Text;
            string serial = inputSerial.Text;
            decimal phone = Convert.ToDecimal(inputPhone.Text);

            Client client = new Client();
            client.name = name;
            client.serialPass = serial;
            client.phone = phone;
            entities.Client.Add(client);
            entities.SaveChanges();
            NavigationService.Navigate(new CreateOrderPageWithClient(client, entities));
        }
    }
}
