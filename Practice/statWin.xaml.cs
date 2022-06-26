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
    /// Логика взаимодействия для statWin.xaml
    /// </summary>
    public partial class statWin : Window
    {
        public statWin()
        {
            InitializeComponent();
            ComboShopping.ItemsSource = PavilionsEntities.GetContext().StatisticsShopping().Select(x => x.nameShopping).Distinct().ToList();
            //DGridStat.ItemsSource = PavilionsEntities.GetContext().StatisticsShopping().ToList();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            tanantsWin win = new tanantsWin();
            win.Show();
            this.Close();
        }

        private void ComboShopping_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = ComboShopping.SelectedItem;
            DGridStat.ItemsSource = PavilionsEntities.GetContext().StatisticsShopping().Where(b => b.nameShopping == c.ToString() ).ToList();
        }
    }
}
