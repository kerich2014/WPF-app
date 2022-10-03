using Stroymaterials.AppData;
using Stroymaterials.PageAdmin;
using Stroymaterials.PageAuthorization;
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

namespace Stroymaterials.PageMenu
{
    /// <summary>
    /// Логика взаимодействия для PageCatalog.xaml
    /// </summary>
    public partial class PageCatalog : Page
    {

        Spare[] FindMaterials()
        {
            List<Spare> spare = AppConnect.model0db.Spare.ToList();
            var materialsAll = spare;
            if (textbox_search != null)
            {
                spare = spare.Where(x => x.spare_name.ToLower().Contains(textbox_search.Text.ToLower())).ToList();
                switch (combosort_price.SelectedIndex)
                {
                    case 1:
                        spare = spare.OrderBy(x => x.spare_price).ToList();
                        break;
                    case 2:
                        spare = spare.OrderByDescending(x => x.spare_price).ToList();
                        break;
                }
            }

            if (combosort_maker.SelectedIndex > 0)
            {
                spare = spare.Where(x => x.Maker.maker_name == combosort_maker.SelectedItem.ToString()).ToList();
            }

            if (spare.Count > 0)
            {
                label_material_list.Text = "Найдено " + spare.Count.ToString() + " из " + materialsAll.Count.ToString();
            }
            else
            {
                label_material_list.Text = "Элементы не найдены";
            }

            return spare.ToArray();
        }

        private void SetPriceMaterials()
        {
            combosort_price.Items.Add("< Нет >");
            combosort_price.Items.Add("Стоимость по возрастанию");
            combosort_price.Items.Add("Стоимость по убыванию");
            combosort_price.SelectedIndex = 0;
        }

        private void SetMakerMaterials()
        {
            combosort_maker.Items.Add("Производитель");
            foreach (var makers in AppConnect.model0db.Maker) 
            {
                combosort_maker.Items.Add(makers.maker_name);
            }
            combosort_maker.SelectedIndex = 0;
        }
        public PageCatalog()
        {
            //Application.Current.MainWindow.Title = "hjkhk";
            //foreach (FrameworkElement txt in Application.Current.Windows)
            //{
            //    if (txt is Label)
            //         Window.Background = new SolidColorBrush(Colors.Red);

            //         если окно - объект TaskWindow
            //        if (txt.Name == "text_welcome")
            //            MessageBox.Show("dfgd");
            //}
            InitializeComponent();
            SetPriceMaterials();
            SetMakerMaterials();
            listbox_catalog.ItemsSource = FindMaterials();
            switch (Flag.role)
            {
                case 1:
                    button_adminpanel.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    button_adminpanel.Visibility = Visibility.Visible;
                    break;
                case 3:
                    button_adminpanel.Visibility = Visibility.Visible;
                    break;
                case 4:
                    button_adminpanel.Visibility = Visibility.Hidden;
                    button_exit.Content = "Войти";
                    break;

            }
        }

        private void grid_catalog_Loaded(object sender, RoutedEventArgs e)
        {
            var CurrentMaterials = Entities7.GetContext().Spare.ToList();
            listbox_catalog.ItemsSource = CurrentMaterials;
        }

        private void button_adminpanel_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmmain.Navigate(new PageMenuAdmin());
        }

        private void button_exit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmmain.Navigate(new PageLogin());
            AppFrame.frmsec.Navigate(new PageName(null));
        }

        private void combosort_price_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listbox_catalog.ItemsSource = FindMaterials();
        }

        private void textbox_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            listbox_catalog.ItemsSource = FindMaterials();
        }

        private void combosort_maker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listbox_catalog.ItemsSource = FindMaterials();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AppConnect.model0db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            listbox_catalog.ItemsSource = FindMaterials();
        }

        
    }
    }


