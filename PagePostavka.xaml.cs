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
    /// Логика взаимодействия для PagePostavka.xaml
    /// </summary>
    public partial class PagePostavka : Page
    {
        public PagePostavka()
        {
            InitializeComponent();
            PostavkGrid.ItemsSource = TradeBDEntities.GetContext().Hist_Postavka.AsNoTracking().ToList();
            PostavkGrid.ItemsSource = TradeBDEntities.GetContext().Hist_Postavka.ToList();
        }

        private void ProdagBut_Click(object sender, RoutedEventArgs e)
        {
            Nav.NavFrame.Navigate(new PageProdaga());
        }

        private void TovarBut_Click(object sender, RoutedEventArgs e)
        {
            Nav.NavFrame.Navigate(new MainPage());
        }

        private void PostavshButt_Click(object sender, RoutedEventArgs e)
        {
            Nav.NavFrame.Navigate(new PagePostavshik());
        }

        private void AddPostBut_Click(object sender, RoutedEventArgs e)
        {
            Nav.NavFrame.Navigate(new PageCRPostav());
        }
    }
}
