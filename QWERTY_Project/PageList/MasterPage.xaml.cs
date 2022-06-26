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
    /// Логика взаимодействия для MasterPage.xaml
    /// </summary>
    public partial class MasterPage : Page
    {
        var1Entities entities;
        List<Worker> listMasterEntiti;
        List<WorkerListClass> workerListClasses;
        public MasterPage(var1Entities entities)
        {
            InitializeComponent();
            this.entities = entities;
            listMasterEntiti = entities.Worker.Where(x=> x.Position1.title.Equals("Мастер")).ToList();
            workerListClasses = new List<WorkerListClass>();
            foreach (var item in listMasterEntiti)
            {
                var status = entities.Repair.ToList().Where(x => x.Device1.master == item.tabNum && x.Status1.Title.Equals("в ремонте")).ToList();
                if (status.Count != 0)
                {
                    workerListClasses.Add(new WorkerListClass() { id = item.tabNum, fio = item.FIO, oklad = item.oklad, procent = item.percentToRepair, status = "Работает" });
                }
                else
                {
                    workerListClasses.Add(new WorkerListClass() { id = item.tabNum, fio = item.FIO, oklad = item.oklad, procent = item.percentToRepair, status = "Свободен" });
                }
                listWorker.ItemsSource = workerListClasses.ToList();
            }
            checkStatus.Items.Add("Все");
            checkStatus.Items.Add("Работает");
            checkStatus.Items.Add("Свободен");
            checkStatus.SelectedItem = "Все";

        }


        private void AddMaster(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddMasterPage(entities));
        }

        private void SaveMaster(object sender, RoutedEventArgs e)
        {

            WorkerListClass workerClass = listWorker.SelectedItem as WorkerListClass;

            Worker worker = entities.Worker.Where(x => x.tabNum == workerClass.id).FirstOrDefault();
            if (worker != null)
            {
                worker.tabNum = workerClass.id;
                worker.oklad = workerClass.oklad;
                worker.percentToRepair = workerClass.procent;
                worker.FIO = workerClass.fio;
                worker.position = worker.position;
                entities.Entry(worker).State = System.Data.Entity.EntityState.Modified;
            }
            entities.SaveChanges();

        }

        private void DeletMaster(object sender, RoutedEventArgs e)
        {
            Worker worker = listWorker.SelectedItem as Worker;

            entities.Worker.Remove(worker);
            entities.SaveChanges();
            listMasterEntiti.Remove(worker);
            listWorker.ItemsSource = listMasterEntiti;
            listWorker.Items.Refresh();
        }

        private void FindFIO(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(inputFIO.Text))
            {
                var list = workerListClasses.Where(x=> x.fio.ToLower().Contains(inputFIO.Text.ToLower())).ToList();
                listWorker.ItemsSource = list;
                listWorker.Items.Refresh();
            }
            else
            {
                listWorker.ItemsSource = workerListClasses;
                listWorker.Items.Refresh();
            }
        }

        private void FilterStatus(object sender, SelectionChangedEventArgs e)
        {
            if (checkStatus.SelectedItem.Equals("Все"))
            {
                var list = workerListClasses.ToList();
                listWorker.ItemsSource = list;
                listWorker.Items.Refresh();
            }
            else
            {
                var list = workerListClasses.Where(x=> x.status.Equals(checkStatus.SelectedItem));
                listWorker.ItemsSource = list;
                listWorker.Items.Refresh();
            }
            
        }
    }
}
