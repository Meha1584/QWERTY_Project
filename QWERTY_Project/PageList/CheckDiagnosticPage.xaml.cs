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
    /// Логика взаимодействия для CheckDiagnosticPage.xaml
    /// </summary>
    public partial class CheckDiagnosticPage : Page
    {
        List<DeviceCheck> devices;
        var1Entities entities;
        public CheckDiagnosticPage(var1Entities entities)
        {
            InitializeComponent();
            this.entities = entities;
            var tempList = entities.Device.Join(entities.Type,
                x => x.type,
                y => y.id,
                (x, y) => new
                {
                    id = x.id,
                    tip = y.Title,
                    model = x.model,
                    damage = x.damage,
                    compaint = x.complaint,
                    master = x.master
                }).ToList();

            devices = tempList.Join(entities.Worker.ToList(),
                x => x.master,
                y => y.tabNum,
                (x, y) => new DeviceCheck()
                {
                    id = x.id,
                    tip = x.tip,
                    model = x.model,
                    damage = x.damage,
                    compaint = x.compaint,
                    master = y.FIO,
                }).ToList();

            checkDiagnosticGrid.ItemsSource = devices;
            checkDiagnostic.Items.Add("Все");
            checkDiagnostic.SelectedItem = "Все";
            checkDiagnostic.Items.Add("На диагностике");
            checkDiagnostic.Items.Add("В ремонте");

            
        }

        private void chechDiagnosticBox(object sender, SelectionChangedEventArgs e)
        {
            var list = entities.FirstDiagnostic.Where(x => x.device == x.Device1.id).ToList() ;
            var listRepair = entities.Repair.Where(x => x.device == x.Device1.id).ToList();
            List<DeviceCheck> deviceChecks = new List<DeviceCheck>();
            List<DeviceCheck> deviceOffDiagnost = new List<DeviceCheck>();
            foreach (var item in list)
            {
                foreach (var item2 in devices)
                {
                    
                    if (item.device == item2.id)
                    {
                        deviceChecks.Add(item2);
                    }
                }
            }
            foreach (var item in listRepair)
            {
                foreach (var item2 in devices)
                {
                    if (item.device == item2.id)
                    {
                        deviceOffDiagnost.Add(item2);
                    }
                }
            }
            foreach (var item in deviceOffDiagnost)
            {
                foreach (var item2 in deviceChecks.ToList())
                {
                    if (item.id == item2.id)
                    {
                        deviceChecks.Remove(item2);
                    }
                }
            }
            if (checkDiagnostic.SelectedItem.Equals("Все"))
            {
                checkDiagnosticGrid.ItemsSource = devices;
            }
            else
            {
                if (checkDiagnostic.SelectedItem.Equals("На диагностике"))
                {
                    
                    checkDiagnosticGrid.ItemsSource = deviceChecks;
                }
                else
                {
                    checkDiagnosticGrid.ItemsSource = deviceOffDiagnost;
                }
            }
        }
    }
}

