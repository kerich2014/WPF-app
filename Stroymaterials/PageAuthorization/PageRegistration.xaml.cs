using Stroymaterials.AppData;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Stroymaterials.PageAuthorization
{
    /// <summary>
    /// Логика взаимодействия для PageRegistration.xaml
    /// </summary>
    public partial class PageRegistration : Page
    {
        string cond = @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)";
        public PageRegistration()
        {
            InitializeComponent();
            label_firstname.Focus();
            
        }

        int error = 0;

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmmain.Navigate(new PageLogin());
        }

        private void label_password_rep_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (label_password.Password != label_password_rep.Password)
            {
                button_create.IsEnabled = false;
                label_password_rep.Background = Brushes.LightCoral;
                label_password_rep.BorderBrush = Brushes.Red;
            }
            else
            {
                button_create.IsEnabled = true;
                label_password_rep.Background = Brushes.LightGreen;
                label_password_rep.BorderBrush = Brushes.Green;
            }
            if (label_password_rep.Password != null)
                error++;

        }

        private void button_create_Click(object sender, RoutedEventArgs e)
        {
            if (AppConnect.model0db.User.Count(x => x.user_login == label_login.Text) > 0)
            {
                MessageBox.Show("Пользователь с таким логином есть!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if(label_password_rep.Password != label_password.Password)
            {
                MessageBox.Show("Пароль введен неверно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if(label_login.Text == null || label_password.Password == null || label_password_rep.Password == null || label_phone.Text == null || label_mail.Text == null || label_firstname.Text == null || label_lastname.Text == null || label_middlename.Text == null || label_datebirth.SelectedDate == null)
            {
                MessageBox.Show($"Не все поля заполнены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information); ;
                return;
            }
            try
            {
                User userObj = new User()
                {
                    user_firstname = label_firstname.Text,
                    user_middlename = label_middlename.Text,
                    user_lastname = label_lastname.Text,
                    user_datebirth =  label_datebirth.SelectedDate.Value,
                    user_mail = label_mail.Text,
                    user_phone = label_phone.Text,
                    user_login = label_login.Text,
                    user_password = label_password.Password,
                    user_role = 1
                };
                AppConnect.model0db.User.Add(userObj);
                AppConnect.model0db.SaveChanges();
                MessageBox.Show("Данные успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.frmmain.Navigate(new PageLogin());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавление данных!  "+ ex.Message, "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        

       
        //проверка вводимых данных
        private void label_phone_LostFocus(object sender, RoutedEventArgs e)
        {

        }



        private void label_phone_SelectionChanged(object sender, RoutedEventArgs e)
        {
            label_phone.Text = Regex.Replace(label_phone.Text, "[^0-9+]", "");
            if (label_phone.Text != null)
                error++;

        }

        private void label_datebirth_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (label_datebirth.SelectedDate > DateTime.Now.AddYears(-18) || label_datebirth.SelectedDate < DateTime.Now.AddYears(-99))
            {
                MessageBox.Show("Регистрироваться могут только люди страше 18 лет и младше 99", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                if (label_datebirth.SelectedDate != null)
                    error++;
            }
        }

        private void label_firstname_SelectionChanged_1(object sender, RoutedEventArgs e)
        {
            label_firstname.Text = Regex.Replace(label_firstname.Text, "[^a-zA-zА-Яа-я0-9]", "");
            if (label_firstname.Text != null)
                error++;
        }

        private void label_lastname_SelectionChanged_1(object sender, RoutedEventArgs e)
        {
            label_lastname.Text = Regex.Replace(label_lastname.Text, "[^a-zA-zА-Яа-я0-9]", "");
            if (label_lastname.Text != null)
                error++;
        }

        private void label_middlename_SelectionChanged_1(object sender, RoutedEventArgs e)
        {
            label_middlename.Text = Regex.Replace(label_middlename.Text, "[^a-zA-zА-Яа-я0-9]", "");
            if (label_middlename.Text != null)
                error++;
        }

        private void label_password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (label_password.Password != null)
                error++;
        }

        private void label_login_SelectionChanged(object sender, RoutedEventArgs e)
        {
            label_login.Text = Regex.Replace(label_login.Text, "[^a-zA-zА-Яа-я0-9]", "");
            if (label_login.Text != null)
                error++;
        }

        private void label_mail_SelectionChanged(object sender, RoutedEventArgs e)
        {
            label_mail.Text = Regex.Replace(label_mail.Text, "[^a-zA-zА-Яа-я0-9@$.]", "");
            if (label_mail.Text != null)
                error++;
        }
    }
}
