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

namespace Stroymaterials.PageAuthorization
{
    /// <summary>
    /// Логика взаимодействия для PageName.xaml
    /// </summary>
    public partial class PageName : Page
    {
      
        public PageName(string username)
        {
            InitializeComponent();
            if (username == null)
            {
                label_info.Visibility = Visibility.Hidden;
                text_info.Content = "";
            }
            else
            {
                label_info.Visibility = Visibility.Visible;
                text_info.Content = username;
            }
            
        }
    }
}
