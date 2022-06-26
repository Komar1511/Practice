using System;
using Microsoft.Win32;
using System.Windows;
using System.Linq;
using System.IO;

namespace Practice
{
    /// <summary> 
    /// Логика взаимодействия для MainWindow.xaml 
    /// </summary> 
    public partial class Photo : Window
    {
        public Photo()
        {
            InitializeComponent();
        }
        private void BtnPhoto_Click(object sender, RoutedEventArgs e)
        {
            string path = "C:\\Users\\komar1511\\Desktop\\Practice\\Sotrudniki";
            var photos = Directory.EnumerateFiles(path);
            using (PavilionsEntities context = new PavilionsEntities())
            {
                foreach (var photo in photos)
                {
                    string s = photo.Substring(photo.LastIndexOf('\\') + 1).Split(' ')[0];
                    var employ = context.Employees.Where(x => x.surnameEmployee == s).FirstOrDefault();
                    if (employ != null)
                        employ.photo = File.ReadAllBytes(photo);
                }
                context.SaveChanges();
            }
            var path2 = "C:\\Users\\komar1511\\Desktop\\Practice\\Image ТЦ";
            var photos2 = Directory.EnumerateFiles(path2);
            using (PavilionsEntities context = new PavilionsEntities())
            {
                foreach (var photo in photos2)
                {
                    string s = photo.Substring(photo.LastIndexOf('\\') + 1);
                    string s2 = s.Substring(0, s.Length - 4);
                    var employ = context.Shoppings.Where(x => x.nameShopping == s2).FirstOrDefault();
                    if (employ != null)
                        employ.image = File.ReadAllBytes(photo);
                }
                context.SaveChanges();
            }
            MessageBox.Show("Попытка загрузки завершена");
        }

            private void BtnBack_Click(object sender, RoutedEventArgs e)
            {
                MainWindow win1 = new MainWindow();
                win1.Show();
                this.Close();
            }
    }
}
