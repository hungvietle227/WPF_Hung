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
    /// Interaction logic for StatisticPage.xaml
    /// </summary>
    public partial class StatisticPage : Window
    {
        IBookingReservationRepository bookRepository = new BookingReservationRepository();
        public StatisticPage()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            startDatePicker.SelectedDate = DateTime.Now;
            endDatePicker.SelectedDate = DateTime.Now;
        }

        private void btnStatistic_Click(object sender, RoutedEventArgs e)
        {
            var startDate = startDatePicker.SelectedDate ?? DateTime.Today;
            var endDate = endDatePicker.SelectedDate ?? DateTime.Today;
            var data = bookRepository.GetAllBookingReservationByDate(startDate, endDate);
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
        private string MapStatus(byte? roomStatus)
        {
            if (roomStatus == null)
            {
                return null;
            }
            else if (roomStatus == 0)
            {
                return "Active";
            }
            else if (roomStatus == 1)
            {
                return "Inactive";
            }
            else
            {
                throw new InvalidOperationException("Invalid roomStatus value");
            }
        }
    }
}
