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
    /// Interaction logic for frmRoom.xaml
    /// </summary>
    public partial class frmRoom : Window
    {
        public int ID { get; set; }
        IRoomRepository roomRepository = new RoomRepository();
        public frmRoom()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ID != 0)
            {
                var room = roomRepository.GetRoomInfoByID(ID.ToString());
                if (room != null)
                {
                    txtDescription.Text = room.RoomDetailDescription;
                    txtMaxCapacity.Text = room.RoomMaxCapacity.ToString();
                    txtRoomNumber.Text = room.RoomNumber;
                    txtRoomPrice.Text = room.RoomPricePerDay.ToString();
                    List<string> statusOptions = new List<string> { "Active", "Inactive" };
                    cboRoomStatus.ItemsSource = statusOptions;
                    int statusValueFromDatabase = (int)room.RoomStatus;
                    cboRoomStatus.SelectedItem = statusValueFromDatabase == 1 ? "Active" : "Inactive";

                    //room type
                    List<string> roomType = new List<string> { "Standard room", "Suite", "Deluxe room", "Executive room", "Family Room", "Connecting Room", "Penthouse Suite", "Bungalow" };
                    cboRoomType.ItemsSource = roomType;
                    int roomTypeValueFromDatabase = (int)room.RoomTypeId;
                    if (roomTypeValueFromDatabase >= 1 && roomTypeValueFromDatabase <= roomType.Count)
                    {
                        cboRoomType.SelectedItem = roomType[roomTypeValueFromDatabase - 1];
                    }
                    btnCreate.IsEnabled = false;
                }
            }
            else
            {
                btnCreate.IsEnabled = true;
                btnUpdate.IsEnabled = false;
                List<string> statusOptions = new List<string> { "Active", "Inactive" };
                cboRoomStatus.ItemsSource = statusOptions;
                cboRoomStatus.SelectedValue = "Active";
                List<string> roomType = new List<string> { "Standard room", "Suite", "Deluxe room", "Executive room", "Family Room", "Connecting Room", "Penthouse Suite", "Bungalow" };
                cboRoomType.ItemsSource = roomType;
                cboRoomStatus.SelectedValue = "Standard room";
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            List<string> roomType = new List<string> { "Standard room", "Suite", "Deluxe room", "Executive room", "Family Room", "Connecting Room", "Penthouse Suite", "Bungalow" };
            var room = new RoomInformation();
            room.RoomDetailDescription = txtDescription.Text.Trim();
            var capacity = txtMaxCapacity.Text.Trim();
            int.TryParse(capacity, out var maxCapacity);
            room.RoomMaxCapacity = maxCapacity;
            room.RoomNumber = txtRoomNumber.Text.Trim();
            var price = txtRoomPrice.Text.Trim();
            int.TryParse(price, out var roomPrice);
            room.RoomPricePerDay = roomPrice;
            room.RoomStatus = cboRoomStatus.SelectedItem.ToString() == "Active" ? (byte)1 : (byte)0;
            string selectedRoomType = cboRoomType.SelectedItem.ToString();
            int selectedIndex = roomType.IndexOf(selectedRoomType);
            if (selectedIndex >= 0)
            {
                room.RoomTypeId = selectedIndex + 1;
            }
            var result = roomRepository.CreateRoom(room);
            if (result)
            {
                MessageBox.Show("Create successfully");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Something went wrong when created");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            List<string> roomType = new List<string> { "Standard room", "Suite", "Deluxe room", "Executive room", "Family Room", "Connecting Room", "Penthouse Suite", "Bungalow" };
            var room = roomRepository.GetRoomInfoByID(ID.ToString());
            if (room != null)
            {
                room.RoomDetailDescription = txtDescription.Text.Trim();
                var capacity = txtMaxCapacity.Text.Trim();
                int.TryParse(capacity, out var maxCapacity);
                room.RoomMaxCapacity = maxCapacity;
                room.RoomNumber = txtRoomNumber.Text.Trim();
                var price = txtRoomPrice.Text.Trim();
                int.TryParse(price, out var roomPrice);
                room.RoomPricePerDay = roomPrice;
                room.RoomStatus = cboRoomStatus.SelectedItem.ToString() == "Active" ? (byte)1 : (byte)0;
                string selectedRoomType = cboRoomType.SelectedItem.ToString();
                int selectedIndex = roomType.IndexOf(selectedRoomType);
                if (selectedIndex >= 0)
                {
                    room.RoomTypeId = selectedIndex + 1;
                }
            }
            var result = roomRepository.UpdateRoom(room);
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
}
