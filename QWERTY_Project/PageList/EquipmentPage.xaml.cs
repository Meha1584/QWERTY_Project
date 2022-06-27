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
    /// Логика взаимодействия для EquipmentPage.xaml
    /// </summary>
    public partial class EquipmentPage : Page
    {
        var1Entities entities;
        List<Equipment> equipments;
        public EquipmentPage(var1Entities entities)
        {
            InitializeComponent();
            this.entities = entities;
            //объединение таблиц
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

            foreach (var item in entities.Specifications)
            {
                checktip.Items.Add(item.Title);
            }
            checktip.Items.Add("Все");
            checktip.SelectedItem = "Все";
        }

        private void FindModel(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(inputModel.Text))
            {
                var list = equipments.Where(x=> x.title.ToLower().Contains(inputModel.Text.ToLower()));
                listOborudovanie.ItemsSource = list;
                listOborudovanie.Items.Refresh();
            }
            else
            {
                listOborudovanie.ItemsSource = equipments;
                listOborudovanie.Items.Refresh();
            }
        }

        private void FilterTip(object sender, SelectionChangedEventArgs e)
        {
            if (checktip.SelectedItem.Equals("Все"))
            {
                var list = equipments.ToList();
                listOborudovanie.ItemsSource = list;
                listOborudovanie.Items.Refresh();
            }
            else
            {
                var list = equipments.Where(x => x.Title.Equals(checktip.SelectedItem));
                listOborudovanie.ItemsSource = list;
                listOborudovanie.Items.Refresh();
            }
        }
    }
}
