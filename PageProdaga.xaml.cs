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
    /// Логика взаимодействия для PageProdaga.xaml
    /// </summary>
    public partial class PageProdaga : Page
    {
        public PageProdaga()
        {
            InitializeComponent();
            HistProdGrid.ItemsSource = TradeBDEntities.GetContext().History_Prod.AsNoTracking().ToList();
            HistProdGrid.ItemsSource = TradeBDEntities.GetContext().History_Prod.ToList();

        }

        private void AddTovBut_Click(object sender, RoutedEventArgs e)
        {
            Nav.NavFrame.Navigate(new PageCRProdag());
        }

        private void ProdagBut_Click(object sender, RoutedEventArgs e)
        {
            Nav.NavFrame.Navigate(new MainPage());
        }

        private void PostavBut_Click(object sender, RoutedEventArgs e)
        {
            Nav.NavFrame.Navigate(new PagePostavka());
        }
    }
}
