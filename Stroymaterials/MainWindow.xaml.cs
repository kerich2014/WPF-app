using Stroymaterials.AppData;
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

namespace Stroymaterials
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Flag.flag = "";
            InitializeComponent();
            AppConnect.model0db = new Entities7();
            AppFrame.frmmain = main_frame;
            AppFrame.frmsec = frame_name;
            frame_name.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            main_frame.Navigate(new PageLogin());
            frame_name.Navigate(new PageName(null));

        }

    }
}
