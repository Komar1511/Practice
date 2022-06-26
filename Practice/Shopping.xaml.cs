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

namespace Practice
{
    /// <summary>
    /// Логика взаимодействия для Shopping.xaml
    /// </summary>
    public partial class Shopping : Window
    {
        public Shopping()
        {
            InitializeComponent();
            DGridShopping.ItemsSource = PavilionsEntities.GetContext().Shoppings.OrderBy(x => x.city).ThenBy(x => x.statusShopping).Where(x => x.statusShopping != "Удален" && x.coefficientShopping > 0.1).ToList();
            ComboCity.ItemsSource = PavilionsEntities.GetContext().Shoppings.Select(x => x.city).Distinct().ToList();
            ComboStatus.ItemsSource = PavilionsEntities.GetContext().Shoppings.Where(x => x.statusShopping != "Удален").Select(x => x.statusShopping).Distinct().ToList();
            
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var upd = DGridShopping.SelectedItems.Cast<Shoppings>().FirstOrDefault();
            AddEditPageShopping win = new AddEditPageShopping(upd);
            win.Show();
            this.Close();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var ShoppingsForRemoving = DGridShopping.SelectedItems.Cast<Shoppings>().ToList();
            if (MessageBox.Show("Вы точно хотите удалить эти(-от) ТЦ", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                { 
                  ShoppingsForRemoving.ForEach(x => x.statusShopping = "Удален");
                    PavilionsEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены!");
                    DGridShopping.ItemsSource = PavilionsEntities.GetContext().Shoppings.Where(x => x.statusShopping != "Удален" && x.coefficientShopping > 0.1).ToList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void ComboCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = ComboCity.SelectedItem;
            List<Shoppings> SearchType = null;
            SearchType = PavilionsEntities.GetContext().Shoppings.Where(b => b.city == c.ToString() && b.statusShopping != "Удален" && b.coefficientShopping > 0.1).ToList();
            DGridShopping.ItemsSource = SearchType; 
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win1 = new MainWindow();
            win1.Show();
            this.Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditPageShopping win = new AddEditPageShopping(null);
            win.Show();
            this.Close();
        }


        private void ComboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = ComboStatus.SelectedItem;
            List<Shoppings> SearchType = null;
            SearchType = PavilionsEntities.GetContext().Shoppings.Where(b => b.statusShopping == c.ToString() && b.statusShopping != "Удален" && b.coefficientShopping > 0.1).ToList();
            DGridShopping.ItemsSource = SearchType;
        }
    }
}
