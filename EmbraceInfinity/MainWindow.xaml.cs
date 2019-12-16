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

        private void LogInClick(object sender, RoutedEventArgs e)
        {
            WindowControl windowControl = new WindowControl();
            WindowUser windowUser = new WindowUser();
            WindowWorkers windowWorkers = new WindowWorkers();

            EMTYEntities db = new EMTYEntities();
            var worker = db.Workers.AsNoTracking().FirstOrDefault(u=>u.Email==TextBoxLigin.Text && u.Password==TextBoxPassword.Password);
            var user = db.User.AsNoTracking().FirstOrDefault(u => u.Login == TextBoxLigin.Text && u.Password == TextBoxPassword.Password);
            if (user == null&&worker==null)
            {
                LabelAnswer.Content = "Пользователь не найден";
            }

            else if (string.IsNullOrEmpty(TextBoxLigin.Text) || string.IsNullOrEmpty(TextBoxPassword.Password))
            {
                LabelAnswer.Content = "Введите логин и пароль";
            }
            try
            {
                if (TextBoxLigin.Text == user.Login)
                {
                    var myWindow = MainWindow.GetWindow(this);
                    myWindow.Close();
                    windowUser.Show();
                }
            }
            catch
            {
                try
                {
                    if (TextBoxLigin.Text == worker.Email && worker.TitleID == 2)
                    {
                        var myWindow = MainWindow.GetWindow(this);
                        myWindow.Close();
                        windowWorkers.Show();
                    }

                    else if (TextBoxLigin.Text == worker.Email && worker.TitleID == 1)
                    {
                        var myWindow = MainWindow.GetWindow(this);
                        myWindow.Close();
                        windowControl.Show();
                    }
                }
                catch
                {
                    LabelAnswer.Content = "Неправильно введёт логин или пароль";
                }
            }
            
           

           
           
        }

        private void SignUpClick(object sender, RoutedEventArgs e)
        {
            
            SignUpWindow Click = new SignUpWindow();
            Click.Show();
            var myWindow = MainWindow.GetWindow(this);
                        myWindow.Close();
        }
    }
}
