using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class RoomRepository : IRoomRepository
    {
        public bool CreateRoom(RoomInformation room)
        {
            return RoomDAO.Instance.CreateRoom(room);
        }

        public IEnumerable<RoomInformation> GetAllRoom()
        {
            return RoomDAO.Instance.GetAllRoom();
        }

        public RoomInformation? GetRoomInfoByID(string id)
        {
            return RoomDAO.Instance.GetRoomInfoByID(id);
        }

        public bool UpdateRoom(RoomInformation room)
        {
            return RoomDAO.Instance.UpdateRoom(room);
        }
    }
}
