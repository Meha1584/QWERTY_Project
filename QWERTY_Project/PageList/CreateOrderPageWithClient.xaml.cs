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
    /// Логика взаимодействия для CreateOrderPageWithClient.xaml
    /// </summary>
    public partial class CreateOrderPageWithClient : Page
    {
        var1Entities entities;
        Client client;
        public CreateOrderPageWithClient(Client client, var1Entities entities)
        {
            InitializeComponent();
            this.client = client;
            this.entities = entities;
            outputClient.Content = $"Клиент {client.name}";
            List<string> listTip = new List<string>();
            List<string> listMaster = new List<string>();
            foreach (var item in entities.Type)
            {
                listTip.Add(item.Title);
            }
            choiseTip.ItemsSource = listTip.ToList();

            foreach (var item in entities.Worker)
            {
                if (item.Position1.title.Equals("Мастер"))
                {
                    listMaster.Add(item.FIO);
                }
            }
            choiceMaster.ItemsSource = listMaster;
        }

        private void AddDevice(object sender, RoutedEventArgs e)
        {
            Type type = entities.Type.ToList().Find(x => x.Title.Equals(choiseTip.SelectedItem));
            Worker worker = entities.Worker.ToList().Find(x => x.FIO.Equals(choiceMaster.SelectedItem));

            Device device = new Device();
            device.model = inputModel.Text;
            device.client = client.Num;
            device.complaint = inputCompla.Text;
            device.damage = inputDamage.Text;
            device.master = worker.tabNum;
            device.type = type.id;
            entities.Device.Add(device);
            entities.SaveChanges();
            MessageBox.Show("Заявка оформлена");
            NavigationService.Navigate(new MasterPage(entities));
        }
    }
}
