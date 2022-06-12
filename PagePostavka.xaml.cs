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
using FastReport;
using FastReport.Data;
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
            TovarQCombo.ItemsSource = TradeBDEntities.GetContext().Tovar.Select(p => p.name).ToList();
            PostavQCombo.ItemsSource = TradeBDEntities.GetContext().Postavshik.Select(p => p.postav).ToList();

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

        private void CancButt_Click(object sender, RoutedEventArgs e)
        {
            PostavkGrid.ItemsSource = TradeBDEntities.GetContext().Hist_Postavka.AsNoTracking().ToList();
            PostavkGrid.ItemsSource = TradeBDEntities.GetContext().Hist_Postavka.ToList();
        }

        private void PriceQCheck_Checked(object sender, RoutedEventArgs e)
        {
            l1pq.Visibility = Visibility.Visible;
            Price1Box.Visibility = Visibility.Visible;
            l2pq.Visibility = Visibility.Visible;
            Price2Box.Visibility = Visibility.Visible;
        }

        private void PriceQCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            l1pq.Visibility = Visibility.Hidden;
            Price1Box.Visibility = Visibility.Hidden;
            l2pq.Visibility = Visibility.Hidden;
            Price2Box.Visibility = Visibility.Hidden;
        }

        private void KolQCheck_Checked(object sender, RoutedEventArgs e)
        {
            l1kq.Visibility = Visibility.Visible;
            Kol1Box.Visibility = Visibility.Visible;
            l2kq.Visibility = Visibility.Visible;
            Kol2Box.Visibility = Visibility.Visible;
        }

        private void KolQCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            l1kq.Visibility = Visibility.Hidden;
            Kol1Box.Visibility = Visibility.Hidden;
            l2kq.Visibility = Visibility.Hidden;
            Kol2Box.Visibility = Visibility.Hidden;
        }

        private void PostavshQCheck_Checked(object sender, RoutedEventArgs e)
        {
            PostavQCombo.Visibility = Visibility.Visible;
        }
        private void PostavshQCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            PostavQCombo.Visibility = Visibility.Hidden;
        }

        private void TovarQCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            TovarQCombo.Visibility = Visibility.Hidden;
        }

        private void TovarQCheck_Checked(object sender, RoutedEventArgs e)
        {
            TovarQCombo.Visibility = Visibility.Visible;
        }

        private void Querybut_Click(object sender, RoutedEventArgs e)
        {
            using (TradeBDEntities db = new TradeBDEntities())
            {
                var query = db.Hist_Postavka.Include("Tovar").Include("Postavshik").Select(p=>p).AsQueryable();
                if (DateQCheck.IsChecked == true)
                {
                    if (DateQ1P.SelectedDate == null || DateQ2P.SelectedDate == null)
                    {
                        MessageBox.Show("Выберите дату");
                        return;
                    }
                    

                    query = query.Where(p => p.date_postav >= DateQ1P.SelectedDate && p.date_postav <= DateQ2P.SelectedDate);
                }
                    if (TovarQCheck.IsChecked == true)
                    {
                        if (TovarQCombo.SelectedIndex == -1)
                    {
                        MessageBox.Show("Выберите товар");
                        return;
                    }
                        string qtst = TovarQCombo.Text.ToString();
                        var qtov = db.Tovar.Where(p => p.name.Contains(qtst)).ToList();
                        int idt = qtov[0].id_tovar;
                        query = query.Where(p => p.id_tovar == idt);
                    }
                    if (PostavshQCheck.IsChecked == true)
                {
                    if (PostavQCombo.SelectedIndex == -1)
                    {
                        MessageBox.Show("Выберите поставщика");
                        return;
                    }
                    string qtpst = PostavQCombo.Text;
                    var qtpt = db.Postavshik.Where(p => p.postav.Contains(qtpst)).ToList();
                    int idp = qtpt[0].id_postavsh;
                    query = query.Where(p => p.id_postav == idp);
                }
                    if (KolQCheck.IsChecked == true)
                {
                    if (String.IsNullOrWhiteSpace(Kol1Box.Text) || (String.IsNullOrWhiteSpace(Kol2Box.Text)))
                    {
                        MessageBox.Show("Введите количество");
                        return;
                    }
                    var K1 = Convert.ToDouble(Kol1Box.Text);
                    var K2 = Convert.ToDouble(Kol2Box.Text);
                    query = query.Where(p => p.kolvo >= K1 && p.kolvo <= K2);
                }
                if (PriceQCheck.IsChecked == true)
                {
                    if (String.IsNullOrWhiteSpace(Price1Box.Text) || (String.IsNullOrWhiteSpace(Price2Box.Text)))
                    {
                        MessageBox.Show("Введите количество");
                        return;
                    }
                    var P1 = Convert.ToDouble(Price1Box.Text);
                    var P2 = Convert.ToDouble(Price2Box.Text);
                    query = query.Where(p => p.price>= P1 && p.price <= P2);
                }
                    PostavkGrid.ItemsSource = query.ToList();

                }
            }
        

        private void DateQCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            lbd1.Visibility = Visibility.Hidden;
            DateQ1P.Visibility = Visibility.Hidden;
            lbd2.Visibility = Visibility.Hidden;
            DateQ2P.Visibility = Visibility.Hidden;
        }

        private void DateQCheck_Checked(object sender, RoutedEventArgs e)
        {
            lbd1.Visibility = Visibility.Visible;
            DateQ1P.Visibility = Visibility.Visible;
            lbd2.Visibility = Visibility.Visible;
            DateQ2P.Visibility = Visibility.Visible;
        }

        private void Kol1Box_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void Kol2Box_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void Price1Box_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void Price2Box_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void ReportBut_Click(object sender, RoutedEventArgs e)
        {
            using (Report report = new Report()){
                try
                {
                    report.Load("histprod.frx");
                    report.Prepare();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
               
            }
        }
    }
}
