using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
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
        EMTYEntities dataEntities = new EMTYEntities();//переменная через которую обращаюсь к базе данных
        ObservableCollection<Workers> ListEmployee = new ObservableCollection<Workers>();//переменная коллекции которая хранит содержимое таблицы Workers

        public object Response { get; private set; }


        /// <summary>
        /// Начальный метод работы программы
        /// </summary>
        public WindowControl()
        {
            InitializeComponent();

            //Начальное значение доступа к кнопкам на вкладке работники
            save.IsEnabled = false;
            edit.IsEnabled = true;
            Undo.IsEnabled = false;
            serch.IsEnabled = true;
            add.IsEnabled = true;
            remove.IsEnabled = true;

            saveUser.IsEnabled = false;
            editUser.IsEnabled = true;
            UndoUser.IsEnabled = false;
            serchUser.IsEnabled = true;
            addUser.IsEnabled = true;
            removeUser.IsEnabled = true;

        }

        /// <summary>
        /// Метод который при загрузке страницы отображает 
        /// данные из базы данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LoadingWindow(object sender, RoutedEventArgs e)
        {
            TableWorker(sender, e);
            TableUsers(sender, e);
        }

        /// <summary>
        /// Метод который загружает данные из БД
        /// в таблицу Работников
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TableWorker(object sender, RoutedEventArgs e)
        {
            var worker = dataEntities.Workers;
            var query = from Workers in worker
                        select Workers;
            DataGridCliesnt.ItemsSource = query.ToList();
        }

        public void TableUsers(object sender, RoutedEventArgs e)
        {
            var user = dataEntities.User;
            var query = from User in user
                        select User;
            DataGridUser.ItemsSource = query.ToList();
        }

        /// <summary>
        /// Кнопка закрытия программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonFeacher_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Метод позволяющий перетаскивать окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку добавить
        /// которая позволяет добавить сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddClick(object sender, RoutedEventArgs e)
        {
            save.IsEnabled = true;
            edit.IsEnabled = false;
            Undo.IsEnabled = true;
            serch.IsEnabled = false;
            add.IsEnabled = false;
            remove.IsEnabled = false;
            Workers employee = new Workers();
            try
            {
                employee.ID = dataEntities.Workers.Count() + 1;
                employee.Surname = "не задано";
                employee.Name = "не задано";
                employee.Patronumic = "не задано";
                employee.Telephone = "0";
                employee.BirthDate = DateTime.Parse("2001-12-12");
                employee.Email = "не задано";
                employee.TitleID = 0;
                employee.Password = "";
                dataEntities.Workers.Add(employee);
                dataEntities.SaveChanges();
                DataGridCliesnt.BeginEdit();
                TableWorker(sender, e);
            }
            catch
            {
                MessageBox.Show("Сначала сохраните изменения");
            }
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку редактировать
        /// позволяющаая редактировать данные о работнике
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditClick(object sender, RoutedEventArgs e)
        {
            save.IsEnabled = true;
            edit.IsEnabled = false;
            Undo.IsEnabled = true;
            serch.IsEnabled = false;
            add.IsEnabled = false;
            remove.IsEnabled = false;

            DataGridCliesnt.IsReadOnly = false;
            DataGridCliesnt.BeginEdit();
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку Поиска
        /// Позволяещее осуществлять поиск по фамилии и должности
        /// становится видимым поле, где можно задать параметры поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Serch_Click(object sender, RoutedEventArgs e)
        {
            //  BorderFind.Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку сохранить
        /// сохраняет все вснесённые изменения в базу данных
        /// и отображает все изменения о работниках
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            save.IsEnabled = false;
            edit.IsEnabled = true;
            Undo.IsEnabled = false;
            serch.IsEnabled = true;
            add.IsEnabled = true;
            remove.IsEnabled = true;

            dataEntities.SaveChanges();
            DataGridCliesnt.IsReadOnly = true;
            TableWorker(sender, e);
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку удалить
        /// который удаляет выбранного сотрудника из базы данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            Workers emp = DataGridCliesnt.SelectedItem as Workers;
            if (emp != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить сотрудника?", "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    dataEntities.Workers.Remove(emp);
                    DataGridCliesnt.SelectedIndex = DataGridCliesnt.SelectedIndex == 0 ? 1 : DataGridCliesnt.SelectedIndex - 1;
                    ListEmployee.Remove(emp);
                    dataEntities.SaveChanges();
                    TableWorker(sender, e);
                }
                else
                {
                    MessageBox.Show("Выберите строку для удаления");
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку добавить
        /// которая позволяет добавить пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddClickUser(object sender, RoutedEventArgs e)
        {
            saveUser.IsEnabled = true;
            editUser.IsEnabled = false;
            UndoUser.IsEnabled = true;
            serchUser.IsEnabled = false;
            addUser.IsEnabled = false;
            removeUser.IsEnabled = false;
            User employee = new User();
            try
            {
                employee.ID = dataEntities.User.Count() + 1;
                employee.Login = "не задано";
                employee.Password = "не задано";
                employee.Email = "не задано";
                employee.Location = "";
                employee.TotalID = 3;
                dataEntities.User.Add(employee);
                dataEntities.SaveChanges();
                DataGridUser.BeginEdit();
                TableUsers(sender, e);
            }
            catch
            {
                MessageBox.Show("Сначала сохраните изменения");
            }
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку редактировать
        /// позволяющаая редактировать данные о позволяет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditClickUser(object sender, RoutedEventArgs e)
        {
            saveUser.IsEnabled = true;
            editUser.IsEnabled = false;
            UndoUser.IsEnabled = true;
            serchUser.IsEnabled = false;
            addUser.IsEnabled = false;
            removeUser.IsEnabled = false;

            DataGridUser.IsReadOnly = false;
            DataGridUser.BeginEdit();
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку сохранить
        /// сохраняет все вснесённые изменения в базу данных
        /// и отображает все изменения о пользователях
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveClickUser(object sender, RoutedEventArgs e)
        {
            saveUser.IsEnabled = false;
            editUser.IsEnabled = true;
            UndoUser.IsEnabled = false;
            serchUser.IsEnabled = true;
            addUser.IsEnabled = true;
            removeUser.IsEnabled = true;

            dataEntities.SaveChanges();
            DataGridUser.IsReadOnly = true;
            TableUsers(sender, e);
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку удалить
        /// который удаляет выбранного пользователя из базы данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteClickUser(object sender, RoutedEventArgs e)
        {
            User emp = DataGridUser.SelectedItem as User;
            if (emp != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить сотрудника?", "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    dataEntities.User.Remove(emp);
                    DataGridUser.SelectedIndex = DataGridUser.SelectedIndex == 0 ? 1 : DataGridUser.SelectedIndex - 1;
                   // ListEmployee.Remove(emp);
                    dataEntities.SaveChanges();
                    TableUsers(sender, e);
                }
                else
                {
                    MessageBox.Show("Выберите строку для удаления");
                }
            }
        }
    }
}
