using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Repository;
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
    /// Interaction logic for MemberScreen.xaml
    /// </summary>
    public partial class MemberScreen : Window
    {
        IBookingReservationRepository bookingReservationRepository = new BookingReservationRepository();
        public int ID { get; set; }
        public int bookingReservationID { get; set; }
        public MemberScreen()
        {
            InitializeComponent();
        }

        private void btnManageProfile_Click(object sender, RoutedEventArgs e)
        {
            frmCustomer frmCustomer = new frmCustomer();
            frmCustomer.ID = ID;
            var result = frmCustomer.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ID != 0)
            {
                var data = bookingReservationRepository.GetBookingReservationByCustomerID(ID.ToString());
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

        private void btnBookingDetail_Click(object sender, RoutedEventArgs e)
        {
            if (dgData.SelectedItem == null)
            {
                MessageBox.Show("Please select item to view detail");
            }
            else
            {
                BookingDetailPage page = new BookingDetailPage();
                page.bookingReservationID = bookingReservationID.ToString();
                page.ShowDialog();
            }
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem != null)
            {
                CustomBookingReservation selectedRoom = (CustomBookingReservation)dgData.SelectedItem;
                bookingReservationID = selectedRoom.BookingReservationId;
            }
        }
    }
}
