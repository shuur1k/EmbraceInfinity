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
using System.Collections.ObjectModel;

namespace EmbraceInfinity
{
    /// <summary>
    /// Логика взаимодействия для WindowSignUp.xaml
    /// </summary>
    public partial class WindowSignUp : Window
    {
        EMTYEntities dataEntities = new EMTYEntities();
        public WindowSignUp()
        {
            InitializeComponent();
        }

       /// <summary>
       /// Обработчик события нажатия на кнопку Sugn Up
       /// 
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void ButtonSignUpClick(object sender, RoutedEventArgs e)
        {

            User user = new User();
            user.Login = TextBoxLiginReg.Text;
            user.Password = TextBoxPasswordReg.Password;
            dataEntities.User.Add(user);
            dataEntities.SaveChanges();
            var users = dataEntities.User;
            var query = from User in users
                        select User;

        }
    }
}