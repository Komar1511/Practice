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
using System.Windows.Shapes;

namespace Practice
{
    /// <summary>
    /// Логика взаимодействия для AddEditPageTanant.xaml
    /// </summary>
    public partial class AddEditPageTanant : Window
    {
        private Tanants _currentTanants = new Tanants();
        private int reg = 0;
        int maxid = PavilionsEntities.GetContext().Tanants.Max(u => u.idTanant);
        public AddEditPageTanant(Tanants selectedTanants)
        {
            InitializeComponent();

            if (selectedTanants != null)
            {
                _currentTanants = selectedTanants;
                reg = 1;
            }
            else
            {
                _currentTanants.idTanant = maxid + 1;
            }

            DataContext = _currentTanants;

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentTanants.title.ToString()))
                errors.AppendLine("Укажите название");
            if (string.IsNullOrWhiteSpace(_currentTanants.numberTanant.ToString()))
                errors.AppendLine("Укажите номер телефона");
            if (string.IsNullOrWhiteSpace(_currentTanants.address.ToString()))
                errors.AppendLine("Укажите адрес");
            if (reg == 0) PavilionsEntities.GetContext().Tanants.Add(_currentTanants);

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            try
            {
                PavilionsEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена. Обновите таблицу");
                _currentTanants = new Tanants();
                tanantsWin win1 = new tanantsWin();
                win1.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BtnPrev_Click(object sender, RoutedEventArgs e)
        {

            tanantsWin win1 = new tanantsWin();
            win1.Show();
            this.Close();

        }
    }
}
