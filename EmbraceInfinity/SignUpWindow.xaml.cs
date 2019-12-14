using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EmbraceInfinity
{
    /// <summary>
    /// Логика взаимодействия для SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        EMTYEntities dataEntities = new EMTYEntities();

        public SignUpWindow()
        {
            InitializeComponent();
        }

        private void ButtonSignUpClick(object sender, RoutedEventArgs e)
        {
            Regex regexPass = new Regex("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z]{6,}");
            Regex regexEmail = new Regex("/@/");

            if (regexPass.IsMatch(TextBoxPasswordReg.Password))
            {
                LabelAnswer.Content = "Лёгкий пароль, Пример:Fg78Td62";
            }

            else if (regexEmail.IsMatch(TextBoxEmailReg.Text))
            {
                LabelAnswer.Content = "Не паравильно введена почта";
            }

            else if (TextBoxPasswordReg.Password != TextBoxConfirmPassword.Password)
            {
                LabelAnswer.Content = "Повторный пароль не совпадает";
            }

            else if (TextBoxPasswordReg.Password == TextBoxConfirmPassword.Password)
            {
                User user = new User();
                user.Login = TextBoxLiginReg.Text;
                user.Password = TextBoxPasswordReg.Password;
                user.Email = TextBoxEmailReg.Text;
                user.TotalID = 3;
                dataEntities.User.Add(user);
                dataEntities.SaveChanges();
                var users = dataEntities.User;
                var query = from User in users
                            select User;

                WindowUser windowUser = new WindowUser();
                windowUser.Show();
                var ThisWindow = SignUpWindow.GetWindow(this);
                ThisWindow.Close();
            }
           
            
        }

        private void LogInClick(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            var thisWindow = SignUpWindow.GetWindow(this);
            thisWindow.Close();
        }
    }
}
