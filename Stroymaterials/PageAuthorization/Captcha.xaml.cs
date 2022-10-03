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
    /// Логика взаимодействия для Captcha.xaml
    /// </summary>
    public partial class Captcha : Page
    {
        private string text = String.Empty;
        public Captcha()
        {
            InitializeComponent();
            
        }
        
        private void ReloadCaptcha()
        {
            Random rnd = new Random();
            text = "";
            string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM"; // qwertyuiopasdfghjklzxcvbnm
            for (int i = 0; i < 4; i++)
                text += ALF[rnd.Next(ALF.Length)];
            label_captcha.Content = text;
        }


        public bool Access(bool countFail)
        {
            //text_capctha.Text = label_captcha.Content.ToString();
            if (text_capctha.Text == label_captcha.Content.ToString())
                return true;
            else
            {
                return false;
            }

        }
        public void wrongAccess()
        {
            MessageBox.Show("Неверная капча!", "Ошибка капчи!", MessageBoxButton.OK, MessageBoxImage.Information);
            ReloadCaptcha();
        }

        private void button_generate_Click(object sender, RoutedEventArgs e)
        {
            ReloadCaptcha();
        }
    }
}
