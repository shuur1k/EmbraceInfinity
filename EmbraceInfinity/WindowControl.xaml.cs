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
    /// Логика взаимодействия для WindowControl.xaml
    /// </summary>
    public partial class WindowControl : Window
    {
        EMTYEntities dataEntities = new EMTYEntities();
        public WindowControl()
        {
           InitializeComponent();
        }

        public void LoadingWindow(object sender, RoutedEventArgs e)
        {
            var worker = dataEntities.Workers;
            var query = from Workers in worker
                        select Workers;
            DataGridCliesnt.ItemsSource = query.ToList();
        }
        private void ButtonFeacher_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

      
       

      
    }
}
