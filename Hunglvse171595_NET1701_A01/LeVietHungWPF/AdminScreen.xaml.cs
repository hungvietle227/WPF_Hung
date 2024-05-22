using AutoMapper;
using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Repository;
using Microsoft.IdentityModel.Tokens;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WPF.Utilities.Mappers;

namespace LeVietHungWPF
{
    /// <summary>
    /// Interaction logic for AdminScreen.xaml
    /// </summary>
    public partial class AdminScreen : Window
    {
        private IMapper mapper; // Khai báo IMapper ở đây
        ICustomerRepository customerRepository = new CustomerRepository();
        IRoomRepository roomRepository = new RoomRepository();
        private int customerId;
        public AdminScreen()
        {
            InitializeComponent();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomerMappingProfile>();
            });
            mapper = config.CreateMapper();
        }

        private void Customer_Click(object sender, RoutedEventArgs e)
        {
            var data = customerRepository.GetAllCustomer();
            var mapData = data.Select(c => mapper.Map<CustomCustomer>(c)).ToList();
            dgData.ItemsSource = mapData;
            menuCustomer.IsChecked = true;
            menuRoom.IsChecked = false;
        }

        private void Roome_Click(object sender, RoutedEventArgs e)
        {
            var data = roomRepository.GetAllRoom();
            menuCustomer.IsChecked = false;
            menuRoom.IsChecked = true;
            dgData.ItemsSource = data;
            customerId = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCustomer();
        }

        public void LoadCustomer()
        {
            dgData.ItemsSource = null;
            var data = customerRepository.GetAllCustomer();
            var mapData = data.Select(c => mapper.Map<CustomCustomer>(c)).ToList();
            dgData.ItemsSource = mapData;
            menuCustomer.IsChecked = true;
            menuRoom.IsChecked = false;
        }

        private void dgData_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Cach 1:

            //DataGrid dataGrid = sender as DataGrid;
            //// Kiểm tra xem có hàng nào được chọn không
            //if (dataGrid.SelectedItem != null)
            //{
            //    // Lấy hàng được chọn
            //    Customer selectedItem = (Customer)dataGrid.SelectedItem;
            //    // Lấy thuộc tính ID của hàng được chọn
            //    int id = selectedItem.CustomerId;

            //MessageBox.Show(e.OriginalSource.ToString());
            if (e.OriginalSource is DataGridColumnHeader)
            {
                // Nếu đúng, không làm gì cả và thoát khỏi sự kiện
                return;
            }
            if (dgData.SelectedItem != null)
            {
                if (dgData.ItemsSource is List<CustomCustomer>)
                {
                    // Đang hiển thị danh sách khách hàng, xử lý cho khách hàng
                    CustomCustomer selectedCustomer = (CustomCustomer)dgData.SelectedItem;
                    int id = selectedCustomer.CustomerId;
                    // In ID ra MessageBox hoặc một cơ chế thông báo khác
                    frmCustomer frmCustomer = new frmCustomer();
                    frmCustomer.ID = id;
                    var result = frmCustomer.ShowDialog();
                    if (result == true)
                    {
                        LoadCustomer();
                    }
                }
                else if (dgData.ItemsSource is List<RoomInformation>)
                {
                    // Đang hiển thị danh sách phòng, xử lý cho phòng
                    RoomInformation selectedRoom = (RoomInformation)dgData.SelectedItem;
                    int id = selectedRoom.RoomId;
                    // In ID ra MessageBox hoặc một cơ chế thông báo khác
                    frmRoom frmRoom = new frmRoom();
                    frmRoom.ID = id;
                    var result = frmRoom.ShowDialog();
                    //if (result == true)
                    //{
                    //    LoadCustomer();
                    //}
                }
            }
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.ItemsSource is List<CustomCustomer>)
            {
                if (dgData.SelectedItem != null)
                {
                    CustomCustomer selectedCustomer = (CustomCustomer)dgData.SelectedItem;
                    customerId = selectedCustomer.CustomerId;
                }
            }
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginScreen = new LoginWindow();
            loginScreen.Show();
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (customerId.ToString().IsNullOrEmpty() || customerId == 0)
            {
                MessageBox.Show("Please pick member to update");
                return;
            }
            else
            {
                frmCustomer frmCustomer = new frmCustomer();
                frmCustomer.ID = customerId;
                var result = frmCustomer.ShowDialog();
                if (result == true)
                {
                    LoadCustomer();
                }
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            frmCustomer frmCustomer = new frmCustomer();
            var result = frmCustomer.ShowDialog();
            if (result == true)
            {
                LoadCustomer();
            }
        }
    }
}
