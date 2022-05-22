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
    /// Логика взаимодействия для PageCRPostavsh.xaml
    /// </summary>
    public partial class PageCRPostavsh : Page
    {
        private Postavshik _currentPostavs = new Postavshik();
        public PageCRPostavsh(Postavshik SelectPostav)
        {
            InitializeComponent();
            if (SelectPostav != null)
            {
                _currentPostavs = SelectPostav;
            }
            DataContext = _currentPostavs;
        }

        private void BackBut_Click(object sender, RoutedEventArgs e)
        {
            Nav.NavFrame.GoBack();
        }

        private void AdBut_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentPostavs.postav))
            {
                errors.AppendLine("Введите название");
            }
            if (string.IsNullOrWhiteSpace(_currentPostavs.phone))
            {
                errors.AppendLine("Введите телефон");
            }
            if (string.IsNullOrWhiteSpace(_currentPostavs.adres))
            {
                errors.AppendLine("Выберите адрес");
            }
            if (string.IsNullOrWhiteSpace(_currentPostavs.fio_zav))
            {
                errors.AppendLine("Введите ФИО заведующего");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentPostavs.id_postavsh == 0)
            {
                TradeBDEntities.GetContext().Postavshik.Add(_currentPostavs);
            }
            try
            {
                TradeBDEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Nav.NavFrame.Navigate(new PagePostavshik());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
