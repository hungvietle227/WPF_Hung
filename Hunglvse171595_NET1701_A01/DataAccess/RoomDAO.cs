using BusinessObject.Models;
using DataAccess.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RoomDAO
    {
        private static RoomDAO? instance;
        private static readonly object instanceLook = new object();
        public RoomDAO() { }
        public static RoomDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new RoomDAO();
                    }
                }
                return instance;
            }
        }
        public IEnumerable<RoomInformation> GetAllRoom()
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                return db.RoomInformations.Include(r => r.RoomType).ToList();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public RoomInformation? GetRoomInfoByID(string id)
        {
            int.TryParse(id, out var ID);
            using var db = new FuminiHotelManagementContext();
            RoomInformation? roomInfo = db.RoomInformations.FirstOrDefault(c => c.RoomId == ID);
            return roomInfo;
        }
        public bool UpdateRoom(RoomInformation room )
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.RoomInformations.Update(room);
                var result = db.SaveChanges();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CreateRoom(RoomInformation room)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.RoomInformations.Add(room);
                var result = db.SaveChanges();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
