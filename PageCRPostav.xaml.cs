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
    /// Логика взаимодействия для PageCRPostav.xaml
    /// </summary>
    public partial class PageCRPostav : Page
    {
        private Postavka _postCon = new Postavka();
        public PageCRPostav()
        {
            InitializeComponent();
            PostavkaGrid.ItemsSource = TradeBDEntities.GetContext().Postavka.AsNoTracking().ToList();
            PostavkaGrid.ItemsSource = TradeBDEntities.GetContext().Postavka.ToList();
            using (TradeBDEntities db = new TradeBDEntities())
            {
                TovarCombo.ItemsSource = db.Tovar.Select(p => p.name).ToList();
                PostavCombo.ItemsSource = db.Postavshik.Select(p => p.postav).ToList();
            }
            Kolvo.Text = Convert.ToString(0);
            CancEdBut.Visibility = Visibility.Hidden;
            if (PostavkaGrid.Items.Count > 0)
            {
                using (TradeBDEntities db = new TradeBDEntities())
                {
                    var Rash = db.Postavka.Select(p => new { p.rowid, p.price }).ToList();
                    double q = 0;
                    for (int i = 0; i < PostavkaGrid.Items.Count - 1; i++)
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
            if (PostavCombo.SelectedIndex == -1)
            {
                err.AppendLine("Выберите поставщика");
            }

            using (TradeBDEntities db = new TradeBDEntities())
            {
                string que = TovarCombo.Text.ToString();
                var qd = db.Tovar.Select(p => new { p.id_tovar, p.zakup_price, p.name, p.kolvo_sklad }).Where(p => p.name.StartsWith(que)).ToList();
                var _tovar = TradeBDEntities.GetContext().Tovar.Find(qd[0].id_tovar);
                _postCon.id_tovar = qd[0].id_tovar;
                string que2 = PostavCombo.Text.ToString();
                var qp = db.Postavshik.Select(p=>new { p.id_postavsh, p.postav }).Where(p => p.postav.StartsWith(que2)).ToList();
                _postCon.id_postav = qp[0].id_postavsh;
            }
            if (err.Length > 0)
            {
                MessageBox.Show(err.ToString());
                return;
            }
            _postCon.kolvo = Convert.ToDouble(Kolvo.Text);
            _postCon.price = Convert.ToDouble(ItogPrc.Content);
            _postCon.date_postav = DateTime.Today;
            _postCon.trader = "Куликова Д.И";
            _postCon.postav = "Голиков Ю.А";
            if (_postCon.rowid == 0)
            {
                TradeBDEntities.GetContext().Postavka.Add(_postCon);
            }
            try
            {
                TradeBDEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                PostavkaGrid.ItemsSource = TradeBDEntities.GetContext().Postavka.AsNoTracking().ToList();
                PostavkaGrid.ItemsSource = TradeBDEntities.GetContext().Postavka.ToList();
                Kolvo.Text = Convert.ToString(0);
                TovarCombo.SelectedIndex = -1;
                if (PostavkaGrid.Items.Count > 0)
                {
                    using (TradeBDEntities db = new TradeBDEntities())
                    {
                        var Rash = db.Postavka.Select(p => new { p.rowid, p.price }).ToList();
                        double q = 0;
                        for (int i = 0; i < PostavkaGrid.Items.Count - 1; i++)
                        {
                            q = q + Rash[i].price;
                        }
                        ItogPrice.Content = q.ToString();
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            _postCon = new Postavka();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show($"Поставка отменяется?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    TradeBDEntities.GetContext().Postavka.RemoveRange(TradeBDEntities.GetContext().Postavka);
                    TradeBDEntities.GetContext().SaveChanges();
                    MessageBox.Show("Поставка отменена");
                    PostavkaGrid.ItemsSource = TradeBDEntities.GetContext().Postavka.AsNoTracking().ToList();
                    PostavkaGrid.ItemsSource = TradeBDEntities.GetContext().Postavka.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            Nav.NavFrame.Navigate(new PagePostavka());
        }
        private void TovarCombo_DropDownClosed(object sender, EventArgs e)
        {
            using (TradeBDEntities db = new TradeBDEntities())
            {
                string que = TovarCombo.Text.ToString();
                var qd = db.Tovar.Select(p => new { p.id_tovar, p.zakup_price, p.name }).Where(p => p.name.Contains(que)).ToList();
                OnePrc.Content = qd[0].zakup_price.ToString();
                ItogPrc.Content = (Convert.ToDouble(OnePrc.Content) * Convert.ToDouble(Kolvo.Text)).ToString();
            }
        }
        private void CancEdBut_Click(object sender, RoutedEventArgs e)
        {
            _postCon = new Postavka();
            Kolvo.Text = Convert.ToString(0);
            TovarCombo.SelectedIndex = -1;
            CancEdBut.Visibility = Visibility.Hidden;
        }
        private void EditButtPst_Click(object sender, RoutedEventArgs e)
        {
            _postCon = (sender as Button).DataContext as Postavka;
            Kolvo.Text = Convert.ToString(_postCon.kolvo);
            for (int i = 0; i < TovarCombo.Items.Count; i++)
            {
                TovarCombo.SelectedIndex = i;
               if (TovarCombo.Text.ToString() == _postCon.Tovar.name)
                {
                    break;
                }
               
            }
            for (int i = 0; i < PostavCombo.Items.Count; i++)
            {
                PostavCombo.SelectedIndex = i;
                if (PostavCombo.Text.ToString() == _postCon.Postavshik.postav)
                {
                    break;
                }

            }
            using (TradeBDEntities db = new TradeBDEntities())
            {
                string que = TovarCombo.Text.ToString();
                var qd = db.Tovar.Select(p => new { p.id_tovar, p.zakup_price, p.name }).Where(p => p.name.Contains(que)).ToList();
                OnePrc.Content = qd[0].zakup_price.ToString();
                ItogPrc.Content = (Convert.ToDouble(OnePrc.Content) * Convert.ToDouble(Kolvo.Text)).ToString();
            }
            CancEdBut.Visibility = Visibility.Visible;
        }
        private void CancPokBut_Click(object sender, RoutedEventArgs e)
        {
            var RemoveK = PostavkaGrid.SelectedItems.Cast<Postavka>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {RemoveK.Count()} поставки?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    TradeBDEntities.GetContext().Postavka.RemoveRange(RemoveK);
                    TradeBDEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    PostavkaGrid.ItemsSource = TradeBDEntities.GetContext().Postavka.AsNoTracking().ToList();
                    PostavkaGrid.ItemsSource = TradeBDEntities.GetContext().Postavka.ToList();
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
                Hist_Postavka _his_post = new Hist_Postavka();
                var ListProd = db.Postavka.Select(p => p).ToList();
                int i = 0;
                do
                {
                    var _tovar = TradeBDEntities.GetContext().Tovar.Find(ListProd[i].id_tovar);
                    _tovar.kolvo_sklad = _tovar.kolvo_sklad + ListProd[i].kolvo;
                    _his_post.id_postav = ListProd[i].id_postav;
                    _his_post.id_tovar = ListProd[i].id_tovar;
                    _his_post.date_postav = ListProd[i].date_postav;
                    _his_post.kolvo = ListProd[i].kolvo;
                    _his_post.price = ListProd[i].price;
                    _his_post.trader = ListProd[i].trader;
                    _his_post.postav = ListProd[i].postav;
                    TradeBDEntities.GetContext().Hist_Postavka.Add(_his_post);

                    i++;
                    try
                    {
                        TradeBDEntities.GetContext().SaveChanges();
                        _his_post = new Hist_Postavka();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }


                } while (i < ListProd.Count);
            }
            try
            {
                TradeBDEntities.GetContext().Postavka.RemoveRange(TradeBDEntities.GetContext().Postavka);
                TradeBDEntities.GetContext().SaveChanges();
                MessageBox.Show("Поставка оплачена");
                PostavkaGrid.ItemsSource = TradeBDEntities.GetContext().Postavka.AsNoTracking().ToList();
                PostavkaGrid.ItemsSource = TradeBDEntities.GetContext().Postavka.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            Nav.NavFrame.Navigate(new PagePostavka());
        }

        private void Kolvo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
    


