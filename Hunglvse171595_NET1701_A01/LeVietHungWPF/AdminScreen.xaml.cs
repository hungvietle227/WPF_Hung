using AutoMapper;
using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Repository;
using Microsoft.IdentityModel.Tokens;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
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
        IBookingReservationRepository bookRepository = new BookingReservationRepository();
        private int customerId;
        private int roomId;
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
            btnCreate.Visibility = Visibility.Visible;
            btnDelete.Visibility = Visibility.Visible;
            btnUpdate.Visibility = Visibility.Visible;
            var data = customerRepository.GetAllCustomer();
            var mapData = data.Select(c => mapper.Map<CustomCustomer>(c)).ToList();
            dgData.ItemsSource = mapData;
            menuCustomer.IsChecked = true;
            menuRoom.IsChecked = false;
            menuReport.IsChecked = false;
            roomId = 0;
            btnFilter.Visibility = Visibility.Collapsed;
            groupBox.Visibility = Visibility.Visible;
        }

        private void Roome_Click(object sender, RoutedEventArgs e)
        {
            btnCreate.Visibility = Visibility.Visible;
            btnDelete.Visibility = Visibility.Visible;
            btnUpdate.Visibility = Visibility.Visible;
            var data = roomRepository.GetAllRoom();
            var mapData = from test in data
                          select new CustomeRoom
                          {
                              RoomId = test.RoomId,

                              RoomNumber = test.RoomNumber,

                              RoomDetailDescription = test.RoomDetailDescription,

                              RoomMaxCapacity = test.RoomMaxCapacity,

                              RoomTypeName = MapRoomName(test.RoomTypeId),

                              RoomStatus = MapStatus(test.RoomStatus),

                              RoomPricePerDay = test.RoomPricePerDay
                          };
            menuCustomer.IsChecked = false;
            menuRoom.IsChecked = true;
            menuReport.IsChecked = false;
            dgData.ItemsSource = mapData.ToList();
            customerId = 0;
            btnFilter.Visibility = Visibility.Collapsed;
            groupBox.Visibility = Visibility.Visible;
        }
        private string MapStatus(byte? roomStatus)
        {
            if (roomStatus == null)
            {
                return null;
            }
            else if (roomStatus == 0)
            {
                return "InActive";
            }
            else if (roomStatus == 1)
            {
                return "Active";
            }
            else
            {
                throw new InvalidOperationException("Invalid roomStatus value");
            }
        }

        private string MapRoomName(int roomTypeId)
        {
            if (roomTypeId == 1)
            {
                return "Standard room";
            }
            else if (roomTypeId == 2)
            {
                return "Suite";
            }
            else if (roomTypeId == 3)
            {
                return "Deluxe room";
            }
            else if (roomTypeId == 4)
            {
                return "Executive room";
            }
            else if (roomTypeId == 5)
            {
                return "Family Room";
            }
            else if (roomTypeId == 6)
            {
                return "Connecting Room";
            }
            else if (roomTypeId == 7)
            {
                return "Penthouse Suite";
            }
            else if (roomTypeId == 8)
            {
                return "Bungalow";
            }
            else
            {
                throw new InvalidOperationException("Invalid RoomName value");
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCustomer();
            btnFilter.Visibility = Visibility.Collapsed;
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
        public void LoadRoom()
        {
            var data = roomRepository.GetAllRoom();
            var mapData = from test in data
                          select new CustomeRoom
                          {
                              RoomId = test.RoomId,

                              RoomNumber = test.RoomNumber,

                              RoomDetailDescription = test.RoomDetailDescription,

                              RoomMaxCapacity = test.RoomMaxCapacity,

                              RoomTypeName = MapRoomName(test.RoomTypeId),

                              RoomStatus = MapStatus(test.RoomStatus),

                              RoomPricePerDay = test.RoomPricePerDay
                          };
            dgData.ItemsSource = mapData.ToList();
            menuCustomer.IsChecked = false;
            menuRoom.IsChecked = true;
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
                else if (dgData.ItemsSource is List<CustomeRoom>)
                {
                    // Đang hiển thị danh sách phòng, xử lý cho phòng
                    CustomeRoom selectedRoom = (CustomeRoom)dgData.SelectedItem;
                    int id = selectedRoom.RoomId;
                    // In ID ra MessageBox hoặc một cơ chế thông báo khác
                    frmRoom frmRoom = new frmRoom();
                    frmRoom.ID = id;
                    var result = frmRoom.ShowDialog();
                    if (result == true)
                    {
                        LoadRoom();
                    }
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
            else if (dgData.ItemsSource is List<CustomeRoom>)
            {
                if (dgData.SelectedItem != null)
                {
                    CustomeRoom selectedRoom = (CustomeRoom)dgData.SelectedItem;
                    roomId = selectedRoom.RoomId;
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
            if ((customerId.ToString().IsNullOrEmpty() || customerId == 0) && menuCustomer.IsChecked == true)
            {
                System.Windows.MessageBox.Show("Please pick member to update");
                return;
            }
            if ((roomId.ToString().IsNullOrEmpty() || roomId == 0) && menuRoom.IsChecked == true)
            {
                System.Windows.MessageBox.Show("Please pick room to update");
                return;
            }
            if (dgData.ItemsSource is List<CustomCustomer>)
            {
                frmCustomer frmCustomer = new frmCustomer();
                frmCustomer.ID = customerId;
                var result = frmCustomer.ShowDialog();
                if (result == true)
                {
                    LoadCustomer();
                }
            }
            if (dgData.ItemsSource is List<CustomeRoom>)
            {
                // In ID ra MessageBox hoặc một cơ chế thông báo khác
                frmRoom frmRoom = new frmRoom();
                frmRoom.ID = roomId;
                var result = frmRoom.ShowDialog();
                if (result == true)
                {
                    LoadRoom();
                }
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {

            if (menuCustomer.IsChecked)
            {
                frmCustomer frmCustomer = new frmCustomer();
                var result = frmCustomer.ShowDialog();
                if (result == true)
                {
                    LoadCustomer();
                }
            }
            else if (menuRoom.IsChecked)
            {
                frmRoom frmRoom = new frmRoom();
                var result = frmRoom.ShowDialog();
                if (result == true)
                {
                    LoadRoom();
                }
            }
        }

        private void menuReport_Click(object sender, RoutedEventArgs e)
        {
            btnCreate.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;
            btnUpdate.Visibility = Visibility.Collapsed;
            btnFilter.Visibility = Visibility.Visible;
            groupBox.Visibility = Visibility.Collapsed;
            dgData.ItemsSource = null;
            var data = bookRepository.GetAllBookingReservation();
            var mapData = from test in data
                          select new CustomBookingReservation
                          {
                              BookingDate = test.BookingDate,

                              BookingReservationId = test.BookingReservationId,

                              BookingStatus = MapStatus(test.BookingStatus),

                              CustomerId = test.CustomerId,

                              TotalPrice = test.TotalPrice
                          };
            dgData.ItemsSource = mapData.ToList();
            menuCustomer.IsChecked = false;
            menuRoom.IsChecked = false;
            menuReport.IsChecked = true;
            roomId = 0;
            customerId = 0;
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            StatisticPage statisticPage = new StatisticPage();
            statisticPage.ShowDialog();

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (menuCustomer.IsChecked)
            {
                if (txtSearch.Text.IsNullOrEmpty())
                {
                    dgData.ItemsSource = null;
                    var data = customerRepository.GetAllCustomer();
                    var mapData = data.Select(c => mapper.Map<CustomCustomer>(c)).ToList();
                    dgData.ItemsSource = mapData;
                }
                else
                {
                    dgData.ItemsSource = null;
                    var data = customerRepository.SearchCustomer(txtSearch.Text);
                    var mapData = data.Select(c => mapper.Map<CustomCustomer>(c)).ToList();
                    dgData.ItemsSource = mapData;
                }
            }
            if (menuRoom.IsChecked)
            {
                if (txtSearch.Text.IsNullOrEmpty())
                {
                    dgData.ItemsSource = null;
                    var data = roomRepository.GetAllRoom();
                    var mapData = from test in data
                                  select new CustomeRoom
                                  {
                                      RoomId = test.RoomId,

                                      RoomNumber = test.RoomNumber,

                                      RoomDetailDescription = test.RoomDetailDescription,

                                      RoomMaxCapacity = test.RoomMaxCapacity,

                                      RoomTypeName = MapRoomName(test.RoomTypeId),

                                      RoomStatus = MapStatus(test.RoomStatus),

                                      RoomPricePerDay = test.RoomPricePerDay
                                  };
                    dgData.ItemsSource = mapData.ToList();
                }
                else
                {
                    dgData.ItemsSource = null;
                    var data = roomRepository.SearchRoom(txtSearch.Text);
                    var mapData = from test in data
                                  select new CustomeRoom
                                  {
                                      RoomId = test.RoomId,

                                      RoomNumber = test.RoomNumber,

                                      RoomDetailDescription = test.RoomDetailDescription,

                                      RoomMaxCapacity = test.RoomMaxCapacity,

                                      RoomTypeName = MapRoomName(test.RoomTypeId),

                                      RoomStatus = MapStatus(test.RoomStatus),

                                      RoomPricePerDay = test.RoomPricePerDay
                                  };
                    dgData.ItemsSource = mapData.ToList();
                }
            }
            if (menuReport.IsChecked)
            {
                if (txtSearch.Text.IsNullOrEmpty())
                {
                    dgData.ItemsSource = null;
                    var data = bookRepository.GetAllBookingReservation();
                    var mapData = from test in data
                                  select new CustomBookingReservation
                                  {
                                      BookingDate = test.BookingDate,

                                      BookingReservationId = test.BookingReservationId,

                                      BookingStatus = MapStatus(test.BookingStatus),

                                      CustomerId = test.CustomerId,

                                      TotalPrice = test.TotalPrice
                                  };
                    dgData.ItemsSource = mapData.ToList();
                }
                else
                {
                    dgData.ItemsSource = null;
                    var data = bookRepository.SearchBookingReservation(txtSearch.Text);
                    var mapData = from test in data
                                  select new CustomBookingReservation
                                  {
                                      BookingDate = test.BookingDate,

                                      BookingReservationId = test.BookingReservationId,

                                      BookingStatus = MapStatus(test.BookingStatus),

                                      CustomerId = test.CustomerId,

                                      TotalPrice = test.TotalPrice
                                  };
                    dgData.ItemsSource = mapData.ToList();
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (menuCustomer.IsChecked)
            {
                if (customerId == 0)
                {
                    System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("Please pick member to delete");
                    return;
                }
                else
                {
                    System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("Do you want to delete this member?", "Confirm Delete", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);
                    if (result == System.Windows.MessageBoxResult.Yes)
                    {
                        var deleteSuccess = customerRepository.DeleteCustomer(customerId);
                        if (deleteSuccess == false)
                        {
                            System.Windows.MessageBox.Show("Delete fail");
                        }
                        LoadCustomer();
                    }
                    else
                    {
                        return;
                    }
                }
            }

            if (menuRoom.IsChecked)
            {
                if (roomId == 0)
                {
                    System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("Please pick room to delete");
                    return;
                }
                else
                {
                    System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("Do you want to delete this room?", "Confirm Delete", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);
                    if (result == System.Windows.MessageBoxResult.Yes)
                    {
                        var deleteSuccess = roomRepository.DeleteRoom(roomId);
                        if (deleteSuccess == false)
                        {
                            System.Windows.MessageBox.Show("Delete fail");
                        }
                        LoadRoom();
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
    }
}
