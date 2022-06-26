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
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        var1Entities entities;
        List<Client> clientList;
        public ClientPage(var1Entities entities)
        {
            InitializeComponent();
            this.entities = entities;
            clientList = entities.Client.ToList();
            listClient.ItemsSource = clientList;
        }

        private void FindFIO(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(inputFIO.Text))
            {
                var list = clientList.Where(x=> x.name.ToLower().Contains(inputFIO.Text.ToLower()));
                listClient.ItemsSource = list;
                listClient.Items.Refresh();
            }
            else
            {
                listClient.ItemsSource = clientList;
                listClient.Items.Refresh();
            }
        }

        private void SaveMaster(object sender, RoutedEventArgs e)
        {
            Client client = listClient.SelectedItem as Client;

            if (client != null)
            {
                client.name = client.name;
                client.Num = client.Num;
                client.phone = client.phone;
                entities.Entry(client).State = System.Data.Entity.EntityState.Modified;
            }
            entities.SaveChanges();
        }
    }
}
