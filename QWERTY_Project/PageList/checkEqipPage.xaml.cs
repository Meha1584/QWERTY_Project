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
    /// Логика взаимодействия для checkEqipPage.xaml
    /// </summary>
    public partial class checkEqipPage : Page
    {
        var1Entities entities;
        DevicePart device;
        List<Equipment> equipments;
        public checkEqipPage(DevicePart device, var1Entities entities)
        {
            InitializeComponent();
            this.entities = entities;
            this.device = device;
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

            Equipment equipment = equipments.ToList().Find(x => x.id == device.id);
            title.Content = device.title;
            count.Content = device.count;
            cost.Content = device.cost;
            value.Content = equipment.value;
            tip.Content = equipment.Title;
        }
    }
}
