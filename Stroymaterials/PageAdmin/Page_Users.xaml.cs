using Stroymaterials.AppData;
using Stroymaterials.PageAuthorization;
using Stroymaterials.PageMenu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace Stroymaterials.PageAdmin
{
    /// <summary>
    /// Логика взаимодействия для Page_Users.xaml
    /// </summary>
    /// 
    public partial class Page_Users : Page
    {
        User users = new User();
        public Page_Users()
        {
            InitializeComponent();
            listview_users.ItemsSource = Entities7.GetContext().User.ToList();
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmmain.Navigate(new PageAddUser(users, false, users));
        }

        private void button_del_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить пользователя?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var userObj1 = listview_users.SelectedItems.Cast<User>().ToList().ElementAt(0);
                if (Flag.flag == userObj1.user_login)
                {
                    MessageBox.Show("Вы не можете удалить себя!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else {
                    try
                    {
                        var userObj = listview_users.SelectedItems.Cast<User>().ToList();
                        Entities7.GetContext().User.RemoveRange(userObj);
                        Entities7.GetContext().SaveChanges();
                        MessageBox.Show("Пользователь успешно удалён!");

                        listview_users.ItemsSource = Entities7.GetContext().User.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                
                
            }
        }

        private void button_edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userObj = listview_users.SelectedItems.Cast<User>().ToList().ElementAt(0);
                User users = new User()
                {
                    id_user = userObj.id_user,
                    user_firstname = userObj.user_firstname,
                    user_middlename = userObj.user_middlename,
                    user_lastname = userObj.user_lastname,
                    user_datebirth = userObj.user_datebirth,
                    user_mail = userObj.user_mail,
                    user_phone = userObj.user_phone,
                    user_login = userObj.user_login,
                    user_password = userObj.user_password,
                    user_role = userObj.user_role
                };
                AppFrame.frmmain.Navigate(new PageAddUser(userObj, true, userObj));
            }
            catch
            {
                MessageBox.Show("Выберите пользователя", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


            

        }

        private void button_materials_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmmain.Navigate(new PageAddMaterials());
        }

        private void button_exit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmmain.Navigate(new PageLogin());
            AppFrame.frmsec.Navigate(new PageName(null));
        }

        private void textbox_search_SelectionChanged(object sender, RoutedEventArgs e)
        {
            List<User> _users = AppConnect.model0db.User.ToList();
            _users = _users.Where(x => x.user_lastname.ToLower().Contains(textbox_search.Text.ToLower())).ToList();
            listview_users.ItemsSource = _users;
        }

        private void button_return_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmmain.Navigate(new PageCatalog());
        }
    }
}
