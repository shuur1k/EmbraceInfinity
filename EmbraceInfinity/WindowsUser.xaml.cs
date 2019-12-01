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
    public partial class WindowUser : Window
    {
        public WindowUser()
        {
           InitializeComponent();
        }

        private void ButtonFeacher_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCoursorMenu(index);

            switch (index)
            {
                case 0:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new UserControlInicio());
                    break;
                //case 1:
                //    GridPrincipal.Children.Clear();
                //    GridPrincipal.Children.Add(new UserControlClients());
                //    break;
                default:
                    break;

            }
        }
        private void MoveCoursorMenu(int index)
        {
            TransitioningContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, (70 + (60 * index)), 0, 0);

        }
    }
}
