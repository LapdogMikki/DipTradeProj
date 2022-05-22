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
    /// Логика взаимодействия для PageHisChanges.xaml
    /// </summary>
    public partial class PageHisChanges : Page
    {
        public PageHisChanges()
        {
            InitializeComponent();
            HisChanGrid.ItemsSource = TradeBDEntities.GetContext().HistChangePrice.AsNoTracking().ToList();
            HisChanGrid.ItemsSource = TradeBDEntities.GetContext().HistChangePrice.ToList();

        }

        private void Backbut_Click(object sender, RoutedEventArgs e)
        {
            Nav.NavFrame.GoBack();
        }
    }
}
