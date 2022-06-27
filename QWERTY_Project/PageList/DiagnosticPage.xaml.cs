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
    /// Логика взаимодействия для DiagnosticPage.xaml
    /// </summary>
    public partial class DiagnosticPage : Page
    {
        var1Entities entities;
        Worker worker;
        Device device;
        bool flag;
        public DiagnosticPage(var1Entities entities, Worker worker)
        {
            InitializeComponent();
            this.entities = entities;
            this.worker = worker;
            
            
            foreach (var item in entities.Device)
            {
                if (!entities.FirstDiagnostic.Any(x=> x.Device1.id == x.device))
                {
                    device = item;

                    nameEq.Content = $"Оборудование {device.model}";
                    if (item.master == worker.tabNum)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                else
                {
                    flag = false;
                }
                
            }

            

            if (flag)
            {
                grid.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("У вас нет заказов на диагностику");
                grid.Visibility = Visibility.Hidden;
                nameCheck.Visibility = Visibility.Visible;
            }

            //if ()
            //{
            //    MessageBox.Show("НЕ диагностик");
            //}
        }

        private void GoRepair(object sender, RoutedEventArgs e)
        {
            FirstDiagnostic firstDiagnostic = new FirstDiagnostic();
            string diagnostic = inputDiagn.Text;
            firstDiagnostic.device = device.id;
            firstDiagnostic.description = diagnostic;
            entities.FirstDiagnostic.Add(firstDiagnostic);
            entities.SaveChanges();
            MessageBox.Show("Устройство отправлено на ремонт");
            NavigationService.Navigate(new CreateRepairPage(device, entities));
        }

        private void ReturnDeviceForClient(object sender, RoutedEventArgs e)
        {
            FirstDiagnostic firstDiagnostic = new FirstDiagnostic();
            string diagnostic = inputDiagn.Text;
            firstDiagnostic.device = device.id;
            firstDiagnostic.description = diagnostic;
            entities.FirstDiagnostic.Add(firstDiagnostic);
            entities.SaveChanges();
            MessageBox.Show("Устройство возвращно клиенту");
            NavigationService.Navigate(new EquipmentPage(entities));
        }
    }
}
