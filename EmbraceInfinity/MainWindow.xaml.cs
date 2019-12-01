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
using System.Text.RegularExpressions;

namespace EmbraceInfinity
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LogInClick(object sender, RoutedEventArgs e)
        {
            Regex regex = new Regex("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z]{6,}");

            if (regex.IsMatch(TextBoxPassword.Password))
            {
                EMTYEntities db = new EMTYEntities();
                var user = db.User.AsNoTracking().FirstOrDefault(u => u.Login == TextBoxLigin.Text && u.Password == TextBoxPassword.Password);
                    if (user == null)
                    {
                        LabelAnswer.Content = "Пользователь не найден";
                    }
                    else
                    {
                        LabelAnswer.Content = "Пользователь найден";
                        WindowControl windowControl = new WindowControl();
                        WindowUser windowUser = new WindowUser();
                        if (user.Login == "Admin")
                        {
                            Hide();
                            windowControl.Show();
                        }
                        else
                        {
                            Hide();
                            windowUser.Show();
                        }
                    }
            }
            else if (string.IsNullOrEmpty(TextBoxLigin.Text) || string.IsNullOrEmpty(TextBoxPassword.Password))
            {
                LabelAnswer.Content = "Введите логин или пароль";
            }
            else LabelAnswer.Content = "Неправильно введёт логин или пароль";
        }
    }
}
