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
        /// <summary>
        /// Самое начало
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик сабытия нажатия на кнопку LogIn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogInClick(object sender, RoutedEventArgs e)
        {
            //Шаблон валидация пароля
            Regex regex = new Regex("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z]{6,}");

            //Проверка валидации пароля
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

       /// <summary>
       /// Обработчик события нажатия на кнопку Sing Up
       /// При нажатии на кнопку открывается новое окно с формой регистраци
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void SignUpClick(object sender, RoutedEventArgs e)
        {
            WindowSignUp SignUp = new WindowSignUp();
            SignUp.Show();

        }
    }
}
