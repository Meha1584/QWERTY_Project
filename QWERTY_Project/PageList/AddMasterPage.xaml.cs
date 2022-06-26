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
    /// Логика взаимодействия для AddMasterPage.xaml
    /// </summary>
    public partial class AddMasterPage : Page
    {
        var1Entities entities;
        public AddMasterPage(var1Entities entities)
        {
            InitializeComponent();
            this.entities = entities;
        }

        //Добавление 
        private void AddMaster(object sender, RoutedEventArgs e)
        {
           
            int login = Convert.ToInt32(inputTabNum.Text);
            string fio = inputFIO.Text;
            int oklad = Convert.ToInt32(inputOklad.Text);
            decimal procent = Convert.ToDecimal(inputProcent.Text);
            string password = inputPass.Text;

            Worker worker = new Worker();
            worker.tabNum = login;
            worker.password = password;
            worker.percentToRepair = procent;
            worker.position = 2;
            worker.oklad = oklad;
            worker.FIO = fio;
            entities.Worker.Add(worker);
            entities.SaveChanges();
            NavigationService.Navigate(new MasterPage(entities));
            
        }
    }
}
