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

namespace DipTradeProj
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            TovarGrid.ItemsSource = TradeBDEntities.GetContext().Tovar.AsNoTracking().ToList();
            TovarGrid.ItemsSource = TradeBDEntities.GetContext().Tovar.ToList();
        }

        private void AddTovBut_Click(object sender, RoutedEventArgs e)
        {
            Nav.NavFrame.Navigate(new PageCRTovar(null));
        }

        private void EditButtTov_Click(object sender, RoutedEventArgs e)
        {
            Nav.NavFrame.Navigate(new PageCRTovar((sender as Button).DataContext as Tovar));
        }

        private void PostavBut_Click(object sender, RoutedEventArgs e)
        {
            Nav.NavFrame.Navigate(new PagePostavka());
        }

        private void ProdagBut_Click(object sender, RoutedEventArgs e)
        {
            Nav.NavFrame.Navigate(new PageProdaga());
        }

        private void RemoveBut_Click(object sender, RoutedEventArgs e)
        {
            var RemoveK = TovarGrid.SelectedItems.Cast<Tovar>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {RemoveK.Count()} записей?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    TradeBDEntities.GetContext().Tovar.RemoveRange(RemoveK);
                    TradeBDEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    TovarGrid.ItemsSource = TradeBDEntities.GetContext().Tovar.AsNoTracking().ToList();
                    TovarGrid.ItemsSource = TradeBDEntities.GetContext().Tovar.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void HistButt_Click(object sender, RoutedEventArgs e)
        {
            Nav.NavFrame.Navigate(new PageHisChanges());
        }
    }
}
