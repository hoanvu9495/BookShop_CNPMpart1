using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.DAL;
using BookShop.Entities;
using System.Windows.Forms;

namespace BookShop.BLL
{
    public class KhuyenMaiBLL
    {
        DBConnection db;
        public KhuyenMaiBLL()
        {
            db = new DBConnection();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataGridView getAllSach()
        {
            DBConnection db = new DBConnection();
            DataGridView data = new DataGridView();
            List<SachDAL> lst = db.SACHes.Select(n => new SachDAL
            {
                ID = n.ID,
                TEN = n.TEN,
                ID_NXB = n.ID_NXB,
            }).ToList();
            data.DataSource = lst;
            return data;
        }
        /// <summary>
        /// Lấy tên sách đổ vào comboBox
        /// </summary>
        /// <returns></returns>
        public static ComboBox getAllTenSach()
        {
            DBConnection db= new DBConnection();
            ComboBox cbx = new ComboBox();
            var item = db.SACHes.Select(n => new {n.ID,n.TEN }).ToList();
            cbx.DataSource = item;
            return cbx;
        }
        /// <summary>
        /// Lấy mã khuyến mại cho khuyến mại mới
        /// 
        /// </summary>
        /// <returns></returns>
        public static int getMaKM() 
        {
            DBConnection db= new DBConnection();

            var id = db.KHUYENMAIs.OrderByDescending(n => n.ID).First();
            return ++id.ID;
        }


        /// <summary>
        /// Load toàn bộ khuyến mại đổ lên dataGridView
        /// </summary>
        /// <returns></returns>
        public static DataGridView getAllKM() 
        {
            DBConnection db = new DBConnection();
            DataGridView data = new DataGridView();
            var item = db.KHUYENMAIs.Where(n=>n.ISDELETE!=true).ToList();
            data.DataSource = item;
            return data;
        }


        /// <summary>
        /// Thêm khuyến mại vào CSDL
        /// </summary>
        /// <param name="maKm">Mã km</param>
        /// <param name="tenKm">Tên khuyến mại</param>
        /// <param name="ngBd">Ngày bắt đầu khuyến mại</param>
        /// <param name="ngKt">Ngày kết thúc khuyến mại</param>
        public void add(KHUYENMAI km)
        {
            db.KHUYENMAIs.Add(km);
            db.SaveChanges();
        }


        /// <summary>
        /// Xóa một khuyến mại
        /// </summary>
        /// <param name="makm"></param>
        public void deleteKM(int makm)
        {
            var item = db.CHITIETKMs.Where(n => n.ID_KM == makm).ToList();
            foreach (CHITIETKM a in item){
                a.ISDELETE= true;
                db.SaveChanges();
            }
            var km = db.KHUYENMAIs.Find(makm);
            if (km!=null)
            {
                km.ISDELETE = true;
                db.SaveChanges();
            }  
        }
        
    }
}
