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
    /// Логика взаимодействия для LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        string _login, _password;
        public static int index = 1;
        public LogWindow()
        {
            InitializeComponent();
        }
        int maxattempts = 3;

        private void LoginText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            Employees User = null;
            Employees User2 = null;

            _login = LoginText.Text;
            _password = PasswordText.Password;
            User = PavilionsEntities.GetContext().Employees.Where(b => b.passwordEmployee == _password && b.loginEmployee == _login && b.roleEmployee != "Менеджер А").FirstOrDefault();
            User2 = PavilionsEntities.GetContext().Employees.Where(b => b.passwordEmployee == _password && b.loginEmployee == _login && b.roleEmployee == "Менеджер А").FirstOrDefault();

            
            if (User != null)
            {
                MessageBox.Show("Вход выполнен");
                index = User.idEmployee;
                MainWindow win2 = new MainWindow();
                win2.Show();
                this.Close();
            }
            if (User2 != null)
            {
                MessageBox.Show("Вы вошли как Менеджер аренды");
                index = User2.idEmployee;
                tanantsWin win = new tanantsWin();
                win.Show();
                this.Close();
            }
            if (User == null && User2 == null)
            {
                MessageBox.Show("Введите корректные данные или создайте новую учетную запись");
                if (maxattempts <= 1)
                {
                    Capcha win1 = new Capcha();
                    win1.Show();
                    this.Close();
                }
                else maxattempts--;
            }
        }
    }
}

