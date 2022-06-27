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
    /// Логика взаимодействия для CreateRepairPage.xaml
    /// </summary>
    public partial class CreateRepairPage : Page
    {
        var1Entities entities;
        Device device;
        List<Equipment> equipments;
        public CreateRepairPage(Device device, var1Entities entities)
        {
            InitializeComponent();
            this.device = device;
            this.entities = entities;
            titleEq.Content = device.model;
            var listValueOfParts = entities.DevicePart.Join(entities.SpecificationsOfPart,
                x => x.id,
                y => y.idPart,
                (x, y) => new
                {
                    id = x.id,
                    title = x.title,
                    count = x.count,
                    cost = x.cost,
                    value = y.value,
                    idS = y.idScpecification
                });

            equipments = listValueOfParts.Join(entities.Specifications,
                x => x.idS,
                y => y.id,
                (x, y) => new Equipment()
                {
                    id = x.id,
                    title = x.title,
                    count = x.count,
                    cost = x.cost,
                    value = x.value,
                    idS = x.idS,
                    Title = y.Title
                }).ToList();

            listOborudovanie.ItemsSource = equipments.ToList();
        }

        private void Finish(object sender, RoutedEventArgs e)
        {
            string comment = inputComment.Text;
            int count = Convert.ToInt32(inputCount.Text);
            if (listOborudovanie.SelectedItems.Count != 0)
            {
                Equipment equipment = listOborudovanie.SelectedItem as Equipment;
                DevicePart devicePart = entities.DevicePart.ToList().Find(x => x.title.Equals(equipment.title));
                if (devicePart != null)
                {
                    Repair repair = new Repair();
                    repair.device = device.id;
                    repair.status = 2;
                    repair.comment = inputComment.Text;
                    repair.cost = count * devicePart.cost;
                    entities.Repair.Add(repair);
                    entities.SaveChanges();
                    PartsToRepair partsToRepair = new PartsToRepair();
                    partsToRepair.idpart = devicePart.id;
                    partsToRepair.idRepair = repair.id;
                    partsToRepair.count = count;
                    entities.PartsToRepair.Add(partsToRepair);
                    entities.SaveChanges();
                    MessageBox.Show("Описание ремонта завершено");
                    NavigationService.Navigate(new EquipmentPage(entities));
                }
            }
            else
            {
                MessageBox.Show("Выберете оборудование");
            }
        }
    }
}
