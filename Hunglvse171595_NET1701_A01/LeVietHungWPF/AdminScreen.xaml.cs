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
using System.Windows.Shapes;

namespace LeVietHungWPF
{
    /// <summary>
    /// Interaction logic for AdminScreen.xaml
    /// </summary>
    public partial class AdminScreen : Window
    {
        public AdminScreen()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ManageCustomer customerForm = new ManageCustomer();
            main.Content = customerForm;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            ManageRoom roomForm = new ManageRoom();
            main.Content = roomForm;
        }
    }
}
