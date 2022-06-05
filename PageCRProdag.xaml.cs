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
    /// Логика взаимодействия для PageCRProdag.xaml
    /// </summary>
    public partial class PageCRProdag : Page
    {
        private Prodazh _prodCon = new Prodazh();
        public PageCRProdag()
        {
            InitializeComponent();
            ProdagaGrid.ItemsSource = TradeBDEntities.GetContext().Prodazh.AsNoTracking().ToList();
            ProdagaGrid.ItemsSource = TradeBDEntities.GetContext().Prodazh.ToList();
            using (TradeBDEntities db = new TradeBDEntities())
            {
                TovarCombo.ItemsSource = db.Tovar.Select(p => p.name).ToList();
            }
            Kolvo.Text = Convert.ToString(0);
            CancEdBut.Visibility = Visibility.Hidden;
            if (ProdagaGrid.Items.Count > 0)
            {
                using (TradeBDEntities db = new TradeBDEntities())
                {
                    var Rash = db.Prodazh.Select(p => new { p.rowid, p.price }).ToList();
                    double q = 0;
                    for (int i = 0; i < ProdagaGrid.Items.Count - 1; i++)
                    {
                        q = q + Rash[i].price;
                    }
                    ItogPrice.Content = q.ToString();
                }
            }
        }



        private void Kolvo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Kolvo.Text.Length != 0)
            {
                ItogPrc.Content = (Convert.ToDouble(OnePrc.Content) * Convert.ToDouble(Kolvo.Text)).ToString();
            }
        }

        private void Kolvo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void AdButt_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder err = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Kolvo.Text))
            {
                err.AppendLine("Введите количество");
            }
            if (TovarCombo.SelectedIndex == -1)
            {
                err.AppendLine("Выберите товар");
            }
            
            using (TradeBDEntities db = new TradeBDEntities())
            {
                string que = TovarCombo.Text.ToString();
                var qd = db.Tovar.Select(p => new { p.id_tovar, p.price, p.name,p.kolvo_sklad}).Where(p => p.name.StartsWith(que)).ToList();
                var _tovar = TradeBDEntities.GetContext().Tovar.Find(qd[0].id_tovar);
                if (Convert.ToDouble(Kolvo.Text) <= qd[0].kolvo_sklad)
                {
                    _prodCon.kolvo = Convert.ToDouble(Kolvo.Text);
                }
                else if (Convert.ToDouble(Kolvo.Text)> qd[0].kolvo_sklad)
                {
                    err.AppendLine("Товара " + qd[0].name + " на складе недостаточно");
                }
                _prodCon.id_tovar = qd[0].id_tovar;
            }
            if (err.Length > 0)
            {
                MessageBox.Show(err.ToString());
                return;
            }
            _prodCon.kolvo = Convert.ToDouble(Kolvo.Text);
            _prodCon.price = Convert.ToDouble(ItogPrc.Content);
            _prodCon.date_prod = DateTime.Now.Date;
            _prodCon.trader = "Куликова Д.И";
            if (_prodCon.rowid == 0)
            {
                TradeBDEntities.GetContext().Prodazh.Add(_prodCon);
            }
            try
            {
                TradeBDEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                ProdagaGrid.ItemsSource = TradeBDEntities.GetContext().Prodazh.AsNoTracking().ToList();
                ProdagaGrid.ItemsSource = TradeBDEntities.GetContext().Prodazh.ToList();
                Kolvo.Text = Convert.ToString(0);
                TovarCombo.SelectedIndex = -1;
                if (ProdagaGrid.Items.Count > 0)
                {
                    using (TradeBDEntities db = new TradeBDEntities())
                    {
                        var Rash = db.Prodazh.Select(p => new { p.rowid, p.price }).ToList();
                        double q = 0;
                        for (int i = 0; i < ProdagaGrid.Items.Count - 1; i++)
                        {
                            q = q + Rash[i].price;
                        }
                        ItogPrice.Content = q.ToString();
                    }
                }
                _prodCon = new Prodazh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show($"Клиент точно хочет отменить покупку?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    TradeBDEntities.GetContext().Prodazh.RemoveRange(TradeBDEntities.GetContext().Prodazh);
                    TradeBDEntities.GetContext().SaveChanges();
                    MessageBox.Show("Покупка отменена");
                    ProdagaGrid.ItemsSource = TradeBDEntities.GetContext().Prodazh.AsNoTracking().ToList();
                    ProdagaGrid.ItemsSource = TradeBDEntities.GetContext().Prodazh.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            Nav.NavFrame.Navigate(new PageProdaga());
        }

        private void TovarCombo_DropDownClosed(object sender, EventArgs e)
        {
            using (TradeBDEntities db = new TradeBDEntities())
            {
                string que = TovarCombo.Text.ToString();
                var qd = db.Tovar.Select(p => new { p.id_tovar, p.price, p.name }).Where(p => p.name.Contains(que)).ToList();
                OnePrc.Content = qd[0].price.ToString();
                ItogPrc.Content = (Convert.ToDouble(OnePrc.Content) * Convert.ToDouble(Kolvo.Text)).ToString();
            }
        }

        private void CancEdBut_Click(object sender, RoutedEventArgs e)
        {
            _prodCon = new Prodazh();
            Kolvo.Text = Convert.ToString(0);
            TovarCombo.SelectedIndex = -1;
            CancEdBut.Visibility = Visibility.Hidden;
        }

        private void EditButtPrd_Click(object sender, RoutedEventArgs e)
        {
            _prodCon = (sender as Button).DataContext as Prodazh;
            Kolvo.Text = Convert.ToString(_prodCon.kolvo);
            for (int i = 0; i < TovarCombo.Items.Count; i++)
            {
                TovarCombo.SelectedIndex = i;
                if (TovarCombo.Text.ToString() == _prodCon.Tovar.name)
                {
                    break;
                }
            }
            using (TradeBDEntities db = new TradeBDEntities())
            {
                string que = TovarCombo.Text.ToString();
                var qd = db.Tovar.Select(p => new { p.id_tovar, p.price, p.name }).Where(p => p.name.Contains(que)).ToList();
                OnePrc.Content = qd[0].price.ToString();
                ItogPrc.Content = (Convert.ToDouble(OnePrc.Content) * Convert.ToDouble(Kolvo.Text)).ToString();
            }
            CancEdBut.Visibility = Visibility.Visible;
        }

        private void CancPokBut_Click(object sender, RoutedEventArgs e)
        {
            var RemoveK = ProdagaGrid.SelectedItems.Cast<Prodazh>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {RemoveK.Count()} покупки?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    TradeBDEntities.GetContext().Prodazh.RemoveRange(RemoveK);
                    TradeBDEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    ProdagaGrid.ItemsSource = TradeBDEntities.GetContext().Prodazh.AsNoTracking().ToList();
                    ProdagaGrid.ItemsSource = TradeBDEntities.GetContext().Prodazh.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void OformButt_Click(object sender, RoutedEventArgs e)
        {
            using (TradeBDEntities db = new TradeBDEntities())
            {
                History_Prod _his_prod = new History_Prod();
                var ListProd = db.Prodazh.Select(p => p).ToList();
                int i = 0;
                do
                {
                    var _tovar = TradeBDEntities.GetContext().Tovar.Find(ListProd[i].id_tovar);
                    _tovar.kolvo_sklad = _tovar.kolvo_sklad - ListProd[i].kolvo;
                    
                    _his_prod.id_tovar = ListProd[i].id_tovar;
                    _his_prod.kolvo = ListProd[i].kolvo;
                    _his_prod.price = ListProd[i].price;
                    _his_prod.trader = ListProd[i].trader;
                    _his_prod.date_prod = ListProd[i].date_prod;
                    TradeBDEntities.GetContext().History_Prod.Add(_his_prod);
                    
                    i++;
                    try
                    {
                        TradeBDEntities.GetContext().SaveChanges();
                        _his_prod = new History_Prod();
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }


                } while (i < ListProd.Count);
            }
            try
            {
                TradeBDEntities.GetContext().Prodazh.RemoveRange(TradeBDEntities.GetContext().Prodazh);
                TradeBDEntities.GetContext().SaveChanges();
                MessageBox.Show("Покупка оплачена");
                ProdagaGrid.ItemsSource = TradeBDEntities.GetContext().Prodazh.AsNoTracking().ToList();
                ProdagaGrid.ItemsSource = TradeBDEntities.GetContext().Prodazh.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            Nav.NavFrame.Navigate(new PageProdaga());
        }

        private void CheckBut_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
    }

