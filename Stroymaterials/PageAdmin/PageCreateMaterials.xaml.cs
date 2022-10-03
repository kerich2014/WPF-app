using Microsoft.Win32;
using Stroymaterials.AppData;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для PageCreateMaterials.xaml
    /// </summary>
    public partial class PageCreateMaterials : Page
    {
        private Spare _materials = new Spare();
        bool shouldUpdate;
        Spare material;
        Spare newMaterial;
        Spare updateMaterial;


        public PageCreateMaterials(Spare material, bool shouldUpdate, Spare updatematerial = null) 
        {
            this.shouldUpdate = shouldUpdate;
            this.material = material;
            this.updateMaterial = updatematerial;



            InitializeComponent();



            text_name.Focus();
            foreach (var item in AppConnect.model0db.Category.ToList())
            {
                combobox_category.Items.Add(item.category_name);
            }
            foreach (var item in AppConnect.model0db.Provider.ToList())
            {
                combobox_provider.Items.Add(item.provider_name);
            }
            foreach (var item in AppConnect.model0db.Maker.ToList())
            {
                combobox_makers.Items.Add(item.maker_name);
            }

            _materials = material;
            


            if (material == null)
            {
                combobox_category.SelectedIndex = 0;
                combobox_provider.SelectedIndex = 0;
                combobox_makers.SelectedIndex = 0;
            }
            

            if (shouldUpdate)
            {
                text_name.Text = updateMaterial.spare_name;
                text_price.Text = updateMaterial.spare_price.ToString();
                text_count.Text = updateMaterial.spare_count.ToString();
                text_description.Text = updateMaterial.spare_description;
                FindFilterMatCategory();
                FindFilterMatMaker();
                FindFilterMatProvider();


                FindFilterCategoryMat();
                FindFilterMakerMat();
                FindFilterProviderMat();
            }
            else
            {
                
                newMaterial = new Spare();
            }

                

        }

        // ----------------------------------------------------Заполнение комбобоксов-----------------------------------------
        
        private void FindFilterMatProvider()
        {
            var typeProvider = AppConnect.model0db.Provider.FirstOrDefault(x => x.id_provider == _materials.spare_provider);
            combobox_provider.SelectedItem = typeProvider.provider_name;
        }

        private void FindFilterMatMaker()
        {
            var typeMaker = AppConnect.model0db.Maker.FirstOrDefault(x => x.id_maker == _materials.spare_maker);
            combobox_makers.SelectedItem = typeMaker.maker_name;
        }

        private void FindFilterMatCategory()
        {
            var typeCategory = AppConnect.model0db.Category.FirstOrDefault(x => x.id_category == _materials.spare_category);
            combobox_category.Text = typeCategory.category_name;
        }

        private void FindFilterCategoryMat()
        {
            var _category = AppConnect.model0db.Category.FirstOrDefault(x => x.category_name == combobox_category.SelectedItem.ToString());
            if (_category == null)
            {
                MessageBox.Show("Выберите элемент", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _materials.spare_category = _category.id_category;
            }
        }

        private void FindFilterProviderMat()
        {
            var _provider = AppConnect.model0db.Provider.FirstOrDefault(x => x.provider_name == combobox_provider.SelectedItem.ToString());
            if (_provider == null)
            {
                MessageBox.Show("Выберите элемент", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _materials.spare_provider = _provider.id_provider;
            }
        }
        private void FindFilterMakerMat()
        {
            var _maker = AppConnect.model0db.Maker.FirstOrDefault(x => x.maker_name == combobox_makers.SelectedItem.ToString());
            if (_maker == null)
            {
                MessageBox.Show("Выберите элемент", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _materials.spare_maker = _maker.id_maker;
            }
        }
        //--------------------------------------------------------------------------------------------------------------------

        //-----------------------------------------------События--------------------------------------------------------------

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmmain.Navigate(new PageAddMaterials());

        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            FindFilterCategoryMat();
            FindFilterMakerMat();
            FindFilterProviderMat();


            if (shouldUpdate)
            {
                localUpdateMaterials(material);
                updateMaterials();
                MessageBox.Show("Изменения сохранены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.frmmain.Navigate(new PageAddMaterials());
            }
            else
            {
                localUpdateMaterials(newMaterial);
                addMaterial();
                MessageBox.Show("Запчасть добавлена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.frmmain.Navigate(new PageAddMaterials());
            }
        }

        private void addMaterial()
        {   
            if(k==0)
            {
                _materials.spare_photo = "img/glush.jpg";
                AppConnect.model0db.SaveChanges();
                DataContext = null;
                DataContext = _materials;
            }
            Entities7.GetContext().Spare.Add(newMaterial);
            Entities7.GetContext().SaveChanges();
        }

        private void updateMaterials()
        {
            Spare updatedMaterial = Entities7.GetContext().Spare.Where(x => x.id_spare == updateMaterial.id_spare).SingleOrDefault();
            updatedMaterial.spare_name = updateMaterial.spare_name;
            updatedMaterial.spare_count = updateMaterial.spare_count;
            updatedMaterial.spare_price = updateMaterial.spare_price;
            updatedMaterial.spare_category = _materials.spare_category;
            updatedMaterial.spare_maker = _materials.spare_maker;
            updatedMaterial.spare_provider = _materials.spare_provider;
            updatedMaterial.spare_photo = _materials.spare_photo;
        }

        private void localUpdateMaterials(Spare material)
        {
            material.spare_name = text_name.Text;
            material.spare_count = Convert.ToInt32(text_count.Text);
            material.spare_price = Convert.ToInt32(text_price.Text);
            material.spare_description = text_description.Text;
            material.spare_category = _materials.spare_category;
            material.spare_maker = _materials.spare_maker;
            material.spare_provider = _materials.spare_provider;
            material.spare_photo = _materials.spare_photo;
        }

        int k = 0;

        private void button_add_image_Click(object sender, RoutedEventArgs e)
        {
            k++;
            OpenFileDialog dialog = new OpenFileDialog();


            dialog.ShowDialog();

            try
            {
                string directory;
               // directory = dialog.FileName.Substring(dialog.FileName.LastIndexOf('\\'), dialog.FileName.Length - dialog.FileName.Substring(0, dialog.FileName.LastIndexOf('\\')).Length);

                //File.Copy(dialog.FileName, System.AppDomain.CurrentDomain.BaseDirectory + "img/" + directory);
                directory = System.IO.Path.GetFileName(dialog.FileName);
                _materials.spare_photo = "img/" + dialog.SafeFileName;
                AppConnect.model0db.SaveChanges();
                DataContext = null;
                DataContext = _materials;

            }
            catch
            {
                _materials.spare_photo = "img/glush.jpg";
                AppConnect.model0db.SaveChanges();
                DataContext = null;
                DataContext = _materials;
            }
        }

        
        


        private void text_price_SelectionChanged(object sender, RoutedEventArgs e)
        {
            text_price.Text = Regex.Replace(text_price.Text, "[^0-9.]", "");
        }

        private void text_count_SelectionChanged(object sender, RoutedEventArgs e)
        {
            text_count.Text = Regex.Replace(text_count.Text, "[^0-9]", "");
        }

        private void text_count_SelectionChanged_1(object sender, RoutedEventArgs e)
        {
            text_count.Text = Regex.Replace(text_count.Text, "[^0-9]", "");
        }

        private void text_price_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void text_price_SelectionChanged_1(object sender, RoutedEventArgs e)
        {
            text_price.Text = Regex.Replace(text_price.Text, "[^0-9.]", "");
        }



        //--------------------------------------------------------------------------------------------------------------------
    }
}
