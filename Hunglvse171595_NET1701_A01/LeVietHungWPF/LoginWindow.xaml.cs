using DataAccess.Enums;
using DataAccess.Repository;
using DataAccess.Util;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LeVietHungWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        ICustomerRepository customerRepository = new CustomerRepository();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = ValidationLogin.ValidateData(txtEmail.Text, txtPassword.Password);
                if (result.isValid)
                {
                    Role login = customerRepository.CheckLogin(txtEmail.Text, txtPassword.Password);
                    if (login == Role.Admin)
                    {
                        AdminScreen screen = new AdminScreen();
                        this.Close();
                        screen.ShowDialog();
                    }
                    else if (login == Role.Customer)
                    {
                        frmCustomer frmCustomer = new frmCustomer();
                        this.Close();
                        frmCustomer.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("User not found");
                    }
                }
                else
                {
                    MessageBox.Show(result.message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            frmRegister frmRegister = new frmRegister();
            frmRegister.Show();
        }
    }
}