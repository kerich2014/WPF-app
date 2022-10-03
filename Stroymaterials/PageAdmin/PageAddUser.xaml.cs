using Stroymaterials.AppData;
using Stroymaterials.PageAuthorization;
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

namespace Stroymaterials.PageAdmin
{
    /// <summary>
    /// Логика взаимодействия для PageAddUser.xaml
    /// </summary>
    public partial class PageAddUser : Page
    {
        private User person = new User(); 
        bool shouldUpdate;
        User user;
        User newUser;
        User updateUser;
        string cond = @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)";
        
        //public PageAddUser()
        //{
        //    InitializeComponent();
        //    FindFilterUsersRole();
        //    label_firstname.Focus();
        //}
        public PageAddUser(User user, bool shouldUpdate, User updateuser = null)
        {
            this.shouldUpdate = shouldUpdate;
            this.user = user;
            this.updateUser = updateuser;
            



            InitializeComponent();
            foreach (var roles in AppConnect.model0db.Role.ToList())
            {
                combobox_roles.Items.Add(roles.roles_name);
            }
            
            if (person != null)
            {
                
            }
            else
            {
                combobox_roles.SelectedIndex = 0;
            }
            


            if (!shouldUpdate) {
                text_roles.Visibility = Visibility.Hidden;
                combobox_roles.Visibility = Visibility.Hidden;
            }
            label_firstname.Focus();

            if (shouldUpdate)
            {
                label_firstname.Text = updateUser.user_firstname;
                label_lastname.Text = updateUser.user_lastname;
                label_middlename.Text = updateUser.user_middlename;
                label_datebirth.Text = updateUser.user_datebirth.ToString("dd/MM/yyyy");
                label_mail.Text = updateUser.user_mail;
                label_phone.Text = updateUser.user_phone;
                label_login.Text = updateUser.user_login;
                label_password.Password = updateUser.user_password;
                label_password_rep.Password = updateUser.user_password;
                label_role.Content = updateUser.user_role;
                
            }
            else 
            {
                
                combobox_roles.SelectedIndex = 0;
                newUser = new User();
            }

        }
        
        

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmmain.Navigate(new Page_Users());
        }


        private void button_create_Click(object sender, RoutedEventArgs e)
        {
            if (shouldUpdate)
            {
                localUpdateUsers(user);
                updateUsers();
                MessageBox.Show("Изменения сохранены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.frmmain.Navigate(new Page_Users());
            }
            else
            {
                localUpdateUsers(newUser);
                addUser();
                MessageBox.Show("Пользователь добавлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.frmmain.Navigate(new PageLogin());
                AppFrame.frmsec.Navigate(new PageName(Flag.flag));
            }
            
        }

        private void addUser()
        {
            Entities7.GetContext().User.Add(newUser);
            Entities7.GetContext().SaveChanges();
        }
        private void localUpdateUsers(User user)
        {
            user.user_firstname = label_firstname.Text;
            user.user_middlename = label_middlename.Text;
            user.user_lastname = label_lastname.Text;
            user.user_phone = label_phone.Text;
            user.user_mail = label_mail.Text;
            user.user_datebirth = label_datebirth.SelectedDate.Value;
            user.user_login = label_login.Text;
            user.user_password = label_password.Password;
            if (!shouldUpdate)
            {
                user.user_role = 1;
            }
            else
            {
                FindFilterRoleUser();
            }
        }
        private void updateUsers()
        {
            User updatedUser = Entities7.GetContext().User.Where(x => x.id_user == updateUser.id_user).SingleOrDefault();
            updatedUser.user_firstname = updateUser.user_firstname;
            updatedUser.user_middlename = updateUser.user_middlename;
            updatedUser.user_lastname = updateUser.user_lastname;
            updatedUser.user_phone = updateUser.user_phone;
            updatedUser.user_mail = updateUser.user_mail;
            updatedUser.user_datebirth = updateUser.user_datebirth;
            updatedUser.user_login = updateUser.user_login;
            updatedUser.user_password = updateUser.user_password;
        }


        // ----------------------------------------Заполнение Комбобоксов------------------------------

        private void FindFilterRoleUser()
        {
            var _roles = AppConnect.model0db.Role.FirstOrDefault(x => x.roles_name == combobox_roles.SelectedItem.ToString());
            if (_roles == null)
            {
                MessageBox.Show("Выберите элемент", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                user.user_role = _roles.id_role;
            }

        }
        //private void FindFilterUsersRole()
        //{
        //    var typerole = AppConnect.model0db.Roles.FirstOrDefault(x => x.id_roles == user.users_role);
        //    combobox_roles.ItemsSource = typerole.roles_name;

        //}
        // -------------------------------------------------------------------------------



        // ----------------------------------------Валидация------------------------------
        private void label_phone_LostFocus(object sender, RoutedEventArgs e)
        {
            if (label_phone.Text.Length != 11 && !label_phone.Text.StartsWith("7") && !string.IsNullOrEmpty(label_phone.Text))
            {
                button_create.IsEnabled = false;
                label_phone.Background = Brushes.LightCoral;
                label_phone.BorderBrush = Brushes.Red;
            }
            else
            {
                button_create.IsEnabled = true;
                label_phone.Background = Brushes.LightGreen;
                label_phone.BorderBrush = Brushes.Green;
            }
        }
        private void label_mail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(label_mail.Text.ToString(), cond))
            {
                button_create.IsEnabled = false;
                label_mail.Background = Brushes.LightCoral;
                label_mail.BorderBrush = Brushes.Red;
            }
            else
            {
                button_create.IsEnabled = true;
                label_mail.Background = Brushes.LightGreen;
                label_mail.BorderBrush = Brushes.Green;
            }
        }

        private void label_password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (label_password.Password.Length < 4 && !Regex.IsMatch(label_password.Password, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {

                label_password.Background = Brushes.LightCoral;
                label_password.BorderBrush = Brushes.Red;
                button_create.IsEnabled = false;

            }
            else
            {
                button_create.IsEnabled = true;
                label_password.Background = Brushes.LightGreen;
                label_password.BorderBrush = Brushes.Green;
                text_password.Content = "";
            }
        }

        private void label_datebirth_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void label_firstname_SelectionChanged(object sender, RoutedEventArgs e)
        {
            label_firstname.Text = Regex.Replace(label_firstname.Text, "[^a-zA-zА-Яа-я]", "");
        }

        private void label_lastname_SelectionChanged(object sender, RoutedEventArgs e)
        {
            label_lastname.Text = Regex.Replace(label_lastname.Text, "[^a-zA-zА-Яа-я]", "");
        }

        private void label_middlename_SelectionChanged(object sender, RoutedEventArgs e)
        {
            label_middlename.Text = Regex.Replace(label_middlename.Text, "[^a-zA-zА-Яа-я]", "");
        }
        private void label_phone_SelectionChanged(object sender, RoutedEventArgs e)
        {
            label_phone.Text = Regex.Replace(label_phone.Text, "[^0-9+]", "");

        }

        private void label_datebirth_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (label_datebirth.SelectedDate > DateTime.Now.AddYears(-18) || label_datebirth.SelectedDate < DateTime.Now.AddYears(-99))
            {
                MessageBox.Show("Регистрироваться могут только люди страше 18 лет и младше 99", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                button_create.IsEnabled = false;
            }
        }

        private void label_firstname_SelectionChanged_1(object sender, RoutedEventArgs e)
        {
            label_firstname.Text = Regex.Replace(label_firstname.Text, "[^a-zA-zА-Яа-я]", "");
        }

        private void label_lastname_SelectionChanged_1(object sender, RoutedEventArgs e)
        {
            label_lastname.Text = Regex.Replace(label_lastname.Text, "[^a-zA-zА-Яа-я]", "");
        }

        private void label_middlename_SelectionChanged_1(object sender, RoutedEventArgs e)
        {
            label_middlename.Text = Regex.Replace(label_middlename.Text, "[^a-zA-zА-Яа-я]", "");
        }



        // -------------------------------------------------------------------------------
    }

}
