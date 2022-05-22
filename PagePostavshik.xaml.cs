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
    /// Логика взаимодействия для PagePostavshik.xaml
    /// </summary>
    public partial class PagePostavshik : Page
    {
        public PagePostavshik()
        {
            InitializeComponent();
            PostavGrid.ItemsSource = TradeBDEntities.GetContext().Postavshik.AsNoTracking().ToList();
            PostavGrid.ItemsSource = TradeBDEntities.GetContext().Postavshik.ToList();

        }

        private void EditButtPost_Click(object sender, RoutedEventArgs e)
        {
            Nav.NavFrame.Navigate(new PageCRPostavsh((sender as Button).DataContext as Postavshik));
        }

        private void HistPostButt_Click(object sender, RoutedEventArgs e)
        {
            Nav.NavFrame.Navigate(new PagePostavka());
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Nav.NavFrame.Navigate(new PageCRPostavsh(null));
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            var RemoveK = PostavGrid.SelectedItems.Cast<Postavshik>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {RemoveK.Count()} записей?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    TradeBDEntities.GetContext().Postavshik.RemoveRange(RemoveK);
                    TradeBDEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    PostavGrid.ItemsSource = TradeBDEntities.GetContext().Tovar.AsNoTracking().ToList();
                    PostavGrid.ItemsSource = TradeBDEntities.GetContext().Tovar.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
