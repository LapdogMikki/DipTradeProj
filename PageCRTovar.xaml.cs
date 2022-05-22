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
    /// Логика взаимодействия для PageCRTovar.xaml
    /// </summary>
    public partial class PageCRTovar : Page
    {
        private Tovar _currentTovar = new Tovar();
        private HistChangePrice _change;
        public PageCRTovar(Tovar selectTov)
        {
            InitializeComponent();
            if (selectTov != null)
            {
                _change = new HistChangePrice();
                _currentTovar = selectTov;
                var a = _currentTovar.price;
                _change.old_price = a;
                for (int i = 0; i < EdIzm.Items.Count; i++)
                {
                    EdIzm.SelectedIndex = i;
                    if (EdIzm.Text.ToString() == _currentTovar.ed_izm.ToString())
                    {
                        break;
                    }
                }
                
            }
            DataContext = _currentTovar;

        }

        private void CancelBut_Click(object sender, RoutedEventArgs e)
        {
            Nav.NavFrame.Navigate(new MainPage());
        }

        private void DateButt_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentTovar.name))
            {
                errors.AppendLine("Введите название");
            }
            if (string.IsNullOrWhiteSpace(_currentTovar.price.ToString()))
            {
                errors.AppendLine("Введите цену");
            }
            if (EdIzm.SelectedIndex == -1)
            {
                errors.AppendLine("Выберите единицу измерения");
            }
            if (string.IsNullOrWhiteSpace(_currentTovar.kolvo_sklad.ToString()))
            {
                errors.AppendLine("Введите количество на складе");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            _currentTovar.ed_izm = EdIzm.Text.ToString();
            if (_currentTovar.id_tovar == 0)
            {
                TradeBDEntities.GetContext().Tovar.Add(_currentTovar);
            }
            else if (_currentTovar.id_tovar != 0)
            {
                _change.id_tovar = _currentTovar.id_tovar;
                _change.date_change = DateTime.Today;
                _change.new_price = _currentTovar.price;
                TradeBDEntities.GetContext().HistChangePrice.Add(_change);
            }
            try
            {
                TradeBDEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Nav.NavFrame.Navigate(new MainPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

     
    }
}
