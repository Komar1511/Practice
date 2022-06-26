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
    /// Логика взаимодействия для tanantsWin.xaml
    /// </summary>
    public partial class tanantsWin : Window
    {
        public tanantsWin()
        {
            InitializeComponent();
            DGridTanants.ItemsSource = PavilionsEntities.GetContext().Tanants.ToList();
        }
        private void titleFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text != "")
            {
                var filteredList = PavilionsEntities.GetContext().Tanants.Where(b => b.title.ToLower().Contains(tb.Text.ToLower())).ToList(); //Получаем список по введенному тексту в TextBox(Поиск) 
                DGridTanants.ItemsSource = null; //Обнуляем список 
                DGridTanants.ItemsSource = filteredList; //Обновляем список 
            }
            else
            {
                DGridTanants.ItemsSource = PavilionsEntities.GetContext().Tanants.ToList(); //Первоначальный список 
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            LogWindow win = new LogWindow();
            win.Show();
            this.Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditPageTanant win = new AddEditPageTanant(null);
            win.Show();
            this.Close();
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var upd = DGridTanants.SelectedItems.Cast<Tanants>().FirstOrDefault();
            AddEditPageTanant win = new AddEditPageTanant(upd);
            win.Show();
            this.Close();
        }

        private void BtnStat_click(object sender, RoutedEventArgs e)
        {
            statWin win = new statWin();
            win.Show();
            this.Close();
        }
    }
}
