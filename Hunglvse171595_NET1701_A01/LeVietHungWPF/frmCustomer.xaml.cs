using BusinessObject.Models;
using DataAccess.Repository;
using DataAccess.Util;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for frmCustomer.xaml
    /// </summary>
    public partial class frmCustomer : Window
    {
        public int ID { get; set; }
        public bool isMember { get; set; }

        ICustomerRepository customerRepository = new CustomerRepository();
        public frmCustomer()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ID != 0)
            {
                if (isMember)
                {
                    cboStatus.IsEnabled = false;
                    txtEmailAddress.IsEnabled = false;
                }
                var customer = customerRepository.GetCustomerByID(ID.ToString());
                if (customer != null)
                {
                    txtEmailAddress.Text = customer.EmailAddress;
                    txtFullName.Text = customer.CustomerFullName;
                    txtPassword.Text = customer.Password;
                    txtTelephone.Text = customer.Telephone;
                    DateOnly? customerBirthday = customer.CustomerBirthday;
                    if (customerBirthday.HasValue)
                    {
                        DateTime dateTime = new DateTime(customerBirthday.Value.Year, customerBirthday.Value.Month, customerBirthday.Value.Day);
                        datePicker.SelectedDate = dateTime;
                    }
                    else
                    {
                        datePicker.SelectedDate = null;
                    }
                    List<string> statusOptions = new List<string> { "Active", "Inactive" };
                    cboStatus.ItemsSource = statusOptions;
                    int statusValueFromDatabase = (int)customer.CustomerStatus;
                    cboStatus.SelectedItem = statusValueFromDatabase == 1 ? "Active" : "Inactive";
                    btnCreate.IsEnabled = false;
                }
            }
            else
            {
                btnCreate.IsEnabled = true;
                btnUpdate.IsEnabled = false;
                List<string> statusOptions = new List<string> { "Active", "Inactive" };
                cboStatus.ItemsSource = statusOptions;
                cboStatus.SelectedValue = "Active";
                datePicker.SelectedDate = DateTime.Now;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var validate = ValidationCommon.ValidateCustomer(txtFullName.Text, txtTelephone.Text, txtEmailAddress.Text, datePicker.SelectedDate!.Value, cboStatus.SelectedItem.ToString(), txtPassword.Text);
            if (validate.isValid == false)
            {
                MessageBox.Show(validate.message);
                return;
            }
            else
            {
                var customer = customerRepository.GetCustomerByID(ID.ToString());
                if (customer != null)
                {
                    customer.EmailAddress = txtEmailAddress.Text;
                    customer.Telephone = txtTelephone.Text;
                    customer.CustomerFullName = txtFullName.Text;
                    customer.Password = txtPassword.Text;
                    DateTime selectedDate = datePicker.SelectedDate!.Value;
                    System.DateOnly dateOnly = new System.DateOnly(selectedDate.Year, selectedDate.Month, selectedDate.Day);
                    customer.CustomerBirthday = dateOnly;
                    customer.CustomerStatus = cboStatus.SelectedItem.ToString() == "Active" ? (byte)1 : (byte)0;
                }
                var result = customerRepository.UpdateCustomer(customer);
                if (result)
                {
                    MessageBox.Show("Update successfully");
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Something went wrong when updated");
                }
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var validate = ValidationCommon.ValidateCustomer(txtFullName.Text, txtTelephone.Text, txtEmailAddress.Text, datePicker.SelectedDate!.Value, cboStatus.SelectedItem.ToString(), txtPassword.Text);
            if (validate.isValid == false)
            {
                MessageBox.Show(validate.message);
                return;
            }
            else
            {
                Customer customer = new Customer();
                customer.Telephone = txtTelephone.Text.Trim();
                customer.CustomerFullName = txtFullName.Text.Trim();
                customer.CustomerStatus = cboStatus.SelectedItem.ToString() == "Active" ? (byte)1 : (byte)0;
                DateTime selectedDate = datePicker.SelectedDate!.Value;
                System.DateOnly dateOnly = new System.DateOnly(selectedDate.Year, selectedDate.Month, selectedDate.Day);
                customer.CustomerBirthday = dateOnly;
                customer.EmailAddress = txtEmailAddress.Text.Trim();
                customer.Password = txtPassword.Text.Trim();
                var result = customerRepository.CreateCustomer(customer);
                if (result)
                {
                    MessageBox.Show("Create successfully");
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Something went wrong when create");
                }
            }

        }
    }
}
