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
using WpfApp2.Componens;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {       

        public MainWindow()
        {
            InitializeComponent();
            //List<Client> ClinetZapros = BdConect.db.Client.ToList();
            //ClientGrid.ItemsSource = ClinetZapros;
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //string name = NameTb.Text.Trim();
            //string Surname = SurnameTb.Text.Trim();
            //var clints = BdConect.db.Client.FirstOrDefault(x => x.Name == name && x.Surname == Surname);
            //if (clints == null)
            //{
            //    BdConect.db.Client.Add(new Client
            //    {
            //        Name = name,
            //        Surname = Surname
            //    });

            //   MessageBox.Show("Добавленно");
            //}
            //BdConect.db.SaveChanges();
            //List<Client> ClinetZapros = BdConect.db.Client.ToList();
            //ClientGrid.ItemsSource = ClinetZapros;
           
            var client2 = new Client();
            client2.Name = NameTb.Text.Trim();
            client2.Surname = SurnameTb.Text.Trim();
            client2.Patronymic = PatronymicTb.Text.Trim();
            await NetManager.Post("api/Client/Add", client2); 
            ClientGrid.Items.Refresh();
            refresh();

        }

        public class Client 
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Patronymic { get; set; }
        } 
        private async void refresh() 
        {
            var user = await  NetManager.Get<List<Client>>("api/Client/GetAllClient");
             ClientGrid.ItemsSource = user;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var client = (sender as Button).DataContext as Client;
            await NetManager.Put<Client>($"api/Client/Edit/{client.Id}",client);

        }

        private async void DeletBtn_Click(object sender, RoutedEventArgs e)
        {
            var client = (sender as Button).DataContext as Client;
            await NetManager.Delete($"api/Client/Delet/{client.Name}", client);
        }
    }
}
