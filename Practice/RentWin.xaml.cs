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
    /// Логика взаимодействия для RentWin.xaml
    /// </summary>
    public partial class RentWin : Window
    {
        private Pavilions pavilion;
        int Tant_ID;
        public List<Tanants> tanantsCollection { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }

        public RentWin(Pavilions selectedPavilon)

        {
            InitializeComponent();
            Start = DateTime.Today;
            Stop = DateTime.Today;
            tanantsCollection = PavilionsEntities.GetContext().Tanants.ToList();
            ComboTanants.ItemsSource = PavilionsEntities.GetContext().Tanants.Select(x => x.title).Distinct().ToList();
            pavilion = selectedPavilon;
            DataContext = pavilion;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Start <= Stop && Start >= DateTime.Today)
            {

                Start = StartPick.SelectedDate.GetValueOrDefault();
                bool stat = Start == DateTime.Today;
                Stop = EndPick.SelectedDate.GetValueOrDefault();
                Tant_ID = PavilionsEntities.GetContext().Tanants.Where(x => x.title == ComboTanants.Text).Select(x => x.idTanant).FirstOrDefault();
                try
                {
                    PavilionsEntities.GetContext().rentPavilion(Tant_ID, LogWindow.index, pavilion.idPavilion, pavilion.idShopping, Start, Stop);
                    MessageBox.Show(stat ? "Арендовано" : "Забронировано");
                }
                catch
                {
                    MessageBox.Show("Вероятно вы пытаетесь арендовать уже арендованный павильон");
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            PavilionsWin win = new PavilionsWin(null);
            win.Show();
            this.Close();
        }
    }
}