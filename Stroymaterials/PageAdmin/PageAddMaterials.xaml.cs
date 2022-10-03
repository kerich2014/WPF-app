using Stroymaterials.AppData;
using Stroymaterials.PageAuthorization;
using Stroymaterials.PageMenu;
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

namespace Stroymaterials.PageAdmin
{
    /// <summary>
    /// Логика взаимодействия для PageAddMaterials.xaml
    /// </summary>
    public partial class PageAddMaterials : Page
    {
        Spare material = new Spare();
        public PageAddMaterials()
        {
            InitializeComponent();
            if (Flag.role == 2) 
            {
                button_users.Visibility = Visibility.Hidden;
            }
            listview_materials.ItemsSource = Entities7.GetContext().Spare.ToList();
        }

        private void button_edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var materialObj = listview_materials.SelectedItems.Cast<Spare>().ToList().ElementAt(0);
                Spare spare = new Spare()
                {
                    spare_name = materialObj.spare_name,
                    spare_count = materialObj.spare_count,
                    spare_category = materialObj.spare_category,
                    spare_price = materialObj.spare_price,
                    spare_provider = materialObj.spare_provider,
                    spare_maker = materialObj.spare_maker,
                    spare_available = materialObj.spare_available,
                    spare_description = materialObj.spare_description,
                    spare_photo = materialObj.spare_photo
                };
                var materialObj2 = listview_materials.SelectedItems.Cast<Spare>().ToList();
                try
                {
                    AppFrame.frmmain.Navigate(new PageCreateMaterials(materialObj, true, materialObj));
                    //StorymaterialsEntities1.GetContext().Spare.RemoveRange(materialObj2);
                    Entities7.GetContext().SaveChanges();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }
            catch
            {
                MessageBox.Show("Ошибка! Выберите запчасть", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }




        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmmain.Navigate(new PageCreateMaterials(material, false, material));
        }

        private void button_del_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить запчасть?", "Предупреждение",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var userObj = listview_materials.SelectedItems.Cast<Spare>().ToList();
                try
                {
                    Entities7.GetContext().Spare.RemoveRange(userObj);
                    Entities7.GetContext().SaveChanges();
                    MessageBox.Show("Запчасть успешно удалена!");

                    listview_materials.ItemsSource = Entities7.GetContext().Spare.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void button_users_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmmain.Navigate(new Page_Users());
        }

        private void button_exit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmmain.Navigate(new PageLogin());
            AppFrame.frmsec.Navigate(new PageName(null));
        }
        

        private void textbox_search_SelectionChanged(object sender, RoutedEventArgs e)
        {
            List<Spare> _materials = AppConnect.model0db.Spare.ToList();
            _materials = _materials.Where(x => x.spare_name.ToLower().Contains(textbox_search.Text.ToLower())).ToList();
            listview_materials.ItemsSource = _materials;
        }

        private void button_return_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmmain.Navigate(new PageCatalog());
        }
    }
}
