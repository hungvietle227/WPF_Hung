using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BookingDetailDAO
    {
        private static BookingDetailDAO? instance;
        private static readonly object instanceLook = new object();
        public BookingDetailDAO() { }
        public static BookingDetailDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new BookingDetailDAO();
                    }
                }
                return instance;
            }
        }
        public List<BookingDetail>? GetBookDetailByBookingReservationID(string id)
        {
            int.TryParse(id, out var ID);
            using var db = new FuminiHotelManagementContext();
            List<BookingDetail>? bookingDetail = db.BookingDetails.Where(c => c.BookingReservationId == ID).ToList();
            return bookingDetail;
        }
    }
}
