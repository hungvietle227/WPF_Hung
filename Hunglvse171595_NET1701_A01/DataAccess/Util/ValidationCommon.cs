using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Util
{
    public static class ValidationCommon
    {
        public static (bool isValid, string message) ValidateCustomer(string fullName, string telePhone, string email, DateTime birthDay, string status, string password)
        {
            if (fullName.IsNullOrEmpty())
            {
                return (false, "Full name cannot be blank");
            }
            if (telePhone.IsNullOrEmpty())
            {
                return (false, "Telephone cannot be blank");
            }
            if (birthDay == null)
            {
                return (false, "Birthday cannot be null");
            }
            if (status == null)
            {
                return (false, "Status cannot be null");
            }
            if (email.IsNullOrEmpty())
            {
                return (false, "Email cannot be blank");
            }
            if (password.IsNullOrEmpty())
            {
                return (false, "Password cannot be null");
            }
            if (email.Trim() == " ")
            {
                return (false, "Email cannot contain whitespace in between");
            }
            if (password.Trim() == " ")
            {
                return (false, "Password cannot contain whitespace in between");
            }
            return (true, "All field validate");
        }

        public static (bool isValid, string message) ValidateRoom(string roomNumber, string description, string maxCapacity, string roomType, string status, string price)
        {
            if (roomNumber.IsNullOrEmpty())
            {
                return (false, "Room number cannot be blank");
            }
            if (description.IsNullOrEmpty())
            {
                return (false, "Description cannot be blank");
            }
            if (maxCapacity.IsNullOrEmpty())
            {
                return (false, "Max Capacity cannot be blank");
            }
            if (roomType.IsNullOrEmpty())
            {
                return (false, "Room type cannot be null");
            }
            if (status.IsNullOrEmpty())
            {
                return (false, "Status cannot be null");
            }
            if (price.IsNullOrEmpty())
            {
                return (false, "Price cannot be null");
            }
            return (true, "All field validate");
        }
    }
}
