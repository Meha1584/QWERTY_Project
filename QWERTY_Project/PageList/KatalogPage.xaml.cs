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
    /// Логика взаимодействия для KatalogPage.xaml
    /// </summary>
    public partial class KatalogPage : Page
    {
        var1Entities entities;
        public KatalogPage(var1Entities entities)
        {
            InitializeComponent();
            this.entities = entities;
            ListEquipment.ItemsSource = entities.DevicePart.ToList();
        }

        private void GoEquip(object sender, MouseButtonEventArgs e)
        {
            DevicePart device = (sender as Grid).DataContext as DevicePart;
            NavigationService.Navigate(new checkEqipPage(device, entities));
        }
    }
}
