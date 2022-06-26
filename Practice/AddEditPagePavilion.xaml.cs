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
    /// Логика взаимодействия для AddEditPagePavilion.xaml
    /// </summary>
    public partial class AddEditPagePavilion : Window
    {
        private Pavilions _currentPavilions = new Pavilions();
        private int reg = 0;
        private Shoppings curS;
        public AddEditPagePavilion(Pavilions selectedPavilions, Shoppings shoppings)
        {
            InitializeComponent();
            curS = shoppings;
            ComboStatusEdit.ItemsSource = PavilionsEntities.GetContext().Pavilions.Select(x => x.status).Distinct().ToList();


            if (selectedPavilions != null)
            {
                _currentPavilions = selectedPavilions;
                reg = 1;
            }

            DataContext = _currentPavilions;

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentPavilions.floor.ToString()))
                errors.AppendLine("Укажите название");
            if (string.IsNullOrWhiteSpace(_currentPavilions.idPavilion.ToString()))
                errors.AppendLine("Укажите город");
            if (string.IsNullOrWhiteSpace(_currentPavilions.status.ToString()))
                errors.AppendLine("Укажите количество павильонов");
            if (string.IsNullOrWhiteSpace(_currentPavilions.square.ToString()))
                errors.AppendLine("Укажите стоимость тц");
            if (string.IsNullOrWhiteSpace(_currentPavilions.coefficientPavilion.ToString()))
                errors.AppendLine("Укажите коэф.добав.стоим.");
            if (_currentPavilions.coefficientPavilion < 0.1)
                errors.AppendLine("Коэф.добав.стоим. должен быть больше или равен 0.1");
            if (string.IsNullOrWhiteSpace(_currentPavilions.priceSquare.ToString()))
                errors.AppendLine("Укажите этажность");
            if (reg == 0) PavilionsEntities.GetContext().Pavilions.Add(_currentPavilions);

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            try
            {
                PavilionsEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена. Обновите таблицу");
                _currentPavilions = new Pavilions();
                Shopping win1 = new Shopping();
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

            PavilionsWin win = new PavilionsWin(curS);
            win.Show();
            this.Close();

        }
    }
}
