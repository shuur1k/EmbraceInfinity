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

namespace EmbraceInfinity
{
    /// <summary>
    /// Логика взаимодействия для WindowAccount.xaml
    /// </summary>
    public partial class WindowAccount : Window
    {
        EMTYEntities dataEntities = new EMTYEntities();
        public WindowAccount()
        {
            InitializeComponent();
        }

        public void WindowLoading()
        {
            var user = dataEntities.User;
            var query = from User in user
                        select User;
            panelDate.DataContext = query.ToList(); ;
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            var Acc = WindowAccount.GetWindow(this);
            Acc.Close();
        }
    }
}
