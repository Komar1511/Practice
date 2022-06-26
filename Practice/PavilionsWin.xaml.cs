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
    /// Логика взаимодействия для Pavilions.xaml
    /// </summary>
    public partial class PavilionsWin : Window
    {
        string _name;
        string _name2;
        int idShop = 0;
        public PavilionsWin(Shoppings currentShopping)
        {
            InitializeComponent();
            idShop = currentShopping.idShopping;
            DGridPavilions.ItemsSource = PavilionsEntities.GetContext().Pavilions.Where(x => x.coefficientPavilion > 0.1 && x.idShopping == idShop).ToList();
            ComboFloor.ItemsSource = PavilionsEntities.GetContext().Pavilions.Select(x => x.floor).Distinct().ToList();
            ComboStatus.ItemsSource = PavilionsEntities.GetContext().Pavilions.Select(x => x.status).Distinct().ToList();

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            AddEditPageShopping win = new AddEditPageShopping(PavilionsEntities.GetContext().Shoppings.Find(idShop));
            win.Show();
            this.Close();
        }

        private void ComboFloor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = ComboFloor.SelectedItem;
            List<Pavilions> SearchType = null;
            SearchType = PavilionsEntities.GetContext().Pavilions.Where(b => b.floor.ToString() == c.ToString() && b.coefficientPavilion > 0.1 && b.idShopping == idShop).ToList();
            DGridPavilions.ItemsSource = SearchType;
        }

        private void ComboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = ComboStatus.SelectedItem;
            List<Pavilions> SearchType = null;
            SearchType = PavilionsEntities.GetContext().Pavilions.Where(b => b.status.ToString() == c.ToString() && b.coefficientPavilion > 0.1 && b.idShopping == idShop).ToList();
            DGridPavilions.ItemsSource = SearchType;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditPagePavilion win = new AddEditPagePavilion(null, PavilionsEntities.GetContext().Shoppings.Find(idShop));
            win.Show();
            this.Close();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var PavilionsForRemoving = DGridPavilions.SelectedItems.Cast<Pavilions>().ToList();
            if (MessageBox.Show("Вы точно хотите удалить эти(-от) ТЦ", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    PavilionsForRemoving.ForEach(x => x.status = "Удален");
                    PavilionsEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены!");
                    DGridPavilions.ItemsSource = PavilionsEntities.GetContext().Pavilions.Where(b => b.coefficientPavilion > 0.1).ToList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var upd = DGridPavilions.SelectedItems.Cast<Pavilions>().FirstOrDefault();
            AddEditPagePavilion win = new AddEditPagePavilion(upd, PavilionsEntities.GetContext().Shoppings.Find(idShop));
            win.Show();
            this.Close();
        }


        private void SquareTextTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            _name = SquareTextFrom.Text;
            _name2 = SquareTextTo.Text;
            double num1 = 0;
            double.TryParse(_name, out num1);
            double num2 = 0;
            double.TryParse(_name2, out num2);
            
                DGridPavilions.ItemsSource = PavilionsEntities.GetContext().Pavilions.Where(b => b.square > num1 && b.square < num2 && b.idShopping == idShop && b.coefficientPavilion > 0.1).ToList();
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            _name = SquareTextFrom.Text;
            _name2 = SquareTextTo.Text;
            double num1 = 0;
            double.TryParse(_name, out num1);
            double num2 = 0;
            double.TryParse(_name2, out num2);
            var c = ComboStatus.SelectedItem;
            var a = ComboFloor.SelectedItem;


            // ТУТ ДОЛЖНЫ БЫТЬ IF НА ВСЕ УСЛОВИЯ НИЖЕ
            try
            {
                if (c == null)
                    DGridPavilions.ItemsSource = PavilionsEntities.GetContext().Pavilions.Where(b => b.square > num1 && b.square < num2 && b.idShopping == idShop
                    && b.coefficientPavilion > 0.1 && b.floor.ToString() == a.ToString()).ToList();
                if (a == null && num1.ToString() != null || num2.ToString() != null)
                    DGridPavilions.ItemsSource = PavilionsEntities.GetContext().Pavilions.Where(b => b.square > num1 && b.square < num2 && b.idShopping == idShop
                    && b.coefficientPavilion > 0.1 && b.status.ToString() == c.ToString()).ToList();
                if (num1 == 0 && num2 == 0)
                    DGridPavilions.ItemsSource = PavilionsEntities.GetContext().Pavilions.Where(b => b.idShopping == idShop && b.floor.ToString() == a.ToString()
                    && b.coefficientPavilion > 0.1 && b.status.ToString() == c.ToString()).ToList();
                if (num1.ToString() == null && num2.ToString() == null)
                    DGridPavilions.ItemsSource = PavilionsEntities.GetContext().Pavilions.Where(b => b.idShopping == idShop && b.floor.ToString() == a.ToString()
                    && b.coefficientPavilion > 0.1 && b.status.ToString() == c.ToString()).ToList();
                if (num1 != 0 || num2 != 0 && c != null && a != null )
                    DGridPavilions.ItemsSource = PavilionsEntities.GetContext().Pavilions.Where(b => b.square > num1 && b.square < num2 && b.idShopping == idShop && b.floor.ToString() == a.ToString()
                    && b.coefficientPavilion > 0.1 && b.status.ToString() == c.ToString()).ToList();
                if (a == null && c != null && num1.ToString() != null && num2.ToString() != null)
                    DGridPavilions.ItemsSource = PavilionsEntities.GetContext().Pavilions.Where(b => b.square > num1 && b.square < num2 && b.idShopping == idShop
                    && b.coefficientPavilion > 0.1 && b.status.ToString() == c.ToString()).ToList();
                if (a == null && num1 != 0 && num2 != 0)
                    DGridPavilions.ItemsSource = PavilionsEntities.GetContext().Pavilions.Where(b => b.square > num1 && b.square < num2 && b.idShopping == idShop
                    && b.coefficientPavilion > 0.1 && b.status.ToString() == c.ToString()).ToList();

            }
            catch
            {
            }
        }

        private void Rent_Click(object sender, RoutedEventArgs e)
        {
            var rnt = DGridPavilions.SelectedItems.Cast<Pavilions>().FirstOrDefault();
            RentWin win = new RentWin(rnt);
            win.Show();
            this.Close();
        }
    }
}
