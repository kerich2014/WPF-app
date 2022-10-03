using Stroymaterials.AppData;
using Stroymaterials.PageAdmin;
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
namespace Stroymaterials.PageAuthorization
{
    /// <summary>
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    
    
    public partial class PageLogin : Page
    {
        User users = new User();
        public int role;
        public bool countFail = true;
        Captcha captcha = new CaptchaNamespace.Captcha();
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        public PageLogin()
        {
            InitializeComponent();

            login_label.Focus();
            frame_captcha.Height = 0;
            
            
        }

        private void timerTick(object sender, EventArgs e) => button_enter.IsEnabled = true;

        private void Timer()
        {

            button_enter.IsEnabled = false;
            countFail = false;

            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Start();
        }

        private void button_enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userOdj = AppConnect.model0db.User.FirstOrDefault(x => x.user_login == login_label.Text.ToString()
                && x.user_password == password_label.Password.ToString());
                if (countFail && userOdj == null)
                {
                    frame_captcha.Height = 112;
                    AppFrame.frameCaptcha = frame_captcha;
                    frame_captcha.Navigate(new Captcha());
                    MessageBox.Show("Вы неправильно ввели данные! Введите капчу и повторите через 10 секунд!", "Ошибка при авторизации!", MessageBoxButton.OK, MessageBoxImage.Information);
                    Timer();

                    return;
                }

                if (userOdj == null)
                {
                    frame_captcha.Height = 112;
                    AppFrame.frameCaptcha = frame_captcha;
                    frame_captcha.Navigate(new Captcha());
                    MessageBox.Show("Вы неправильно ввели данные! Введите капчу и повторите через 10 секунд!", "Ошибка при авторизации!", MessageBoxButton.OK, MessageBoxImage.Information);
                    Timer();
                    return;
                }
                if (!countFail && userOdj != null)
                {
                    if (captcha.Access(countFail)) countFail = true;
                    else
                    {
                        captcha.wrongAccess();
                        Timer();
                        return;
                    }
                }
                else
                {
                    Flag.role = userOdj.user_role;
                    switch (Flag.role)
                    {
                        
                        case 1:
                            Flag.flag = userOdj.user_login;
                            AppFrame.frmmain.Navigate(new PageCatalog());
                            AppFrame.frmsec.Navigate(new PageName(Flag.flag));
                            
                            break;
                        case 2:
                            Flag.flag = userOdj.user_login;
                            AppFrame.frmmain.Navigate(new PageCatalog());
                            AppFrame.frmsec.Navigate(new PageName(Flag.flag));
                            break;
                        case 3:
                            Flag.flag= userOdj.user_login;
                            AppFrame.frmmain.Navigate(new PageCatalog());
                            AppFrame.frmsec.Navigate(new PageName(Flag.flag));
                            break;
                        default:
                            MessageBox.Show("Данные не обнаружены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                            break;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("" + Ex.Message.ToString() + "", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        private void button_registration_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmmain.Navigate(new PageRegistration());
        }

        private void button_guest_Click(object sender, RoutedEventArgs e)
        {
            Flag.flag = "Гость";
            Flag.role = 4;
            AppFrame.frmmain.Navigate(new PageCatalog());
            AppFrame.frmsec.Navigate(new PageName(Flag.flag));
        }
    }
}
