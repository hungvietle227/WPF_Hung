using DataAccess.DTO;
using DataAccess.Repository;
using Microsoft.IdentityModel.Tokens;
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
    /// Interaction logic for BookingDetailPage.xaml
    /// </summary>
    public partial class BookingDetailPage : Window
    {
        IBookingDetailRepository bookingDetailRepository = new BookingDetailRepository();
        public string bookingReservationID { get; set; }
        public BookingDetailPage()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var data = bookingDetailRepository.GetBookDetailByBookingReservationID(bookingReservationID);
            var mapData = from test in data
                          select new CustomBookingDetail
                          {
                              RoomId = test.RoomId,

                              ActualPrice = test.ActualPrice,

                              BookingReservationId = test.BookingReservationId,

                              StartDate = test.StartDate,

                              EndDate = test.EndDate,
                          };
            dgData.ItemsSource = mapData.ToList();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text.IsNullOrEmpty())
            {
                var data = bookingDetailRepository.GetBookDetailByBookingReservationID(bookingReservationID);
                var mapData = from test in data
                              select new CustomBookingDetail
                              {
                                  RoomId = test.RoomId,

                                  ActualPrice = test.ActualPrice,

                                  BookingReservationId = test.BookingReservationId,

                                  StartDate = test.StartDate,

                                  EndDate = test.EndDate,
                              };
                dgData.ItemsSource = mapData.ToList();
            }
            else
            {
                var data = bookingDetailRepository.SearchBookingDetail(txtSearch.Text);
                var mapData = from test in data
                              select new CustomBookingDetail
                              {
                                  RoomId = test.RoomId,

                                  ActualPrice = test.ActualPrice,

                                  BookingReservationId = test.BookingReservationId,

                                  StartDate = test.StartDate,

                                  EndDate = test.EndDate,
                              };
                dgData.ItemsSource = mapData.ToList();
            }
        }
    }
}
