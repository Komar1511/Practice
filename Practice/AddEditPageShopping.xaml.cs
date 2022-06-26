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
using System.IO;
using Microsoft.Win32;

namespace Practice
{
    /// <summary>
    /// Логика взаимодействия для AddEditPageShopping.xaml
    /// </summary>
    public partial class AddEditPageShopping : Window
    {
        private Shoppings _currentShoppings = new Shoppings();
        private int reg = 0;
        int maxid = PavilionsEntities.GetContext().Shoppings.Max(u => u.idShopping);
        public AddEditPageShopping(Shoppings selectedShoppings)
        {
            InitializeComponent();
            ComboStatusEdit.ItemsSource = PavilionsEntities.GetContext().Shoppings.Where(x => x.statusShopping != "Удален").Select(x => x.statusShopping).Distinct().ToList();

            if (selectedShoppings != null)
            {
                _currentShoppings = selectedShoppings;
                reg = 1;
            }
            else
            {
                _currentShoppings.idShopping = maxid + 1;
            }

            DataContext = _currentShoppings;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files | *.BMP;*.JPG;*.PNG";
            fileDialog.InitialDirectory = @"C:\Users\komar1511\Desktop\Practice\Image ТЦ";

            fileDialog.Title = "Выбор фото ТЦ";

            if (fileDialog.ShowDialog() == true)
            {

                _currentShoppings.image = File.ReadAllBytes(fileDialog.FileName);
            }
            MessageBox.Show(" Файл выбран ");
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentShoppings.nameShopping.ToString()))
                errors.AppendLine("Укажите название");
            if (string.IsNullOrWhiteSpace(_currentShoppings.city.ToString()))
                errors.AppendLine("Укажите город");
            if (string.IsNullOrWhiteSpace(_currentShoppings.countPavilions.ToString()))
                errors.AppendLine("Укажите количество павильонов");
            if (string.IsNullOrWhiteSpace(_currentShoppings.priceShopping.ToString()))
                errors.AppendLine("Укажите стоимость тц");
            if (string.IsNullOrWhiteSpace(_currentShoppings.coefficientShopping.ToString()))
                errors.AppendLine("Укажите коэф.добав.стоим.");
            if (string.IsNullOrWhiteSpace(_currentShoppings.countFloor.ToString()))
                errors.AppendLine("Укажите этажность");
            if (reg == 0) PavilionsEntities.GetContext().Shoppings.Add(_currentShoppings);

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            try
            {
                PavilionsEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена. Обновите таблицу");
                _currentShoppings = new Shoppings();
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
            Shopping win = new Shopping();
            win.Show();
            this.Close();

        }
            private void BtnPavilions_Click(object sender, RoutedEventArgs e)
            {
            var currentShopping = _currentShoppings;
            PavilionsWin win = new PavilionsWin(currentShopping);
               win.Show();
               this.Close();
            }
        }
    }
