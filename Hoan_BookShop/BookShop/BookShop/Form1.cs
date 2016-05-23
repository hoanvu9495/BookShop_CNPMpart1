using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookShop.BLL;
using BookShop.Entities;

namespace BookShop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //dgv_AllHD.DataSource = HoaDonBLL.getAllSach().DataSource;
            dtp_NgayHD.Value = DateTime.Now;
            //txt_MaHD.Text = HoaDonBLL.getMaHD().ToString();
            dgv_AllChuDe.DataSource = ChuDeBLL.getChuDe();
        }

        private void tpe_sale_Click(object sender, EventArgs e)
        {
            //dgv_AllHD.DataSource = HoaDonBLL.getAllSach().DataSource;
            dtp_NgayHD.Value = DateTime.Now;
            //txt_MaHD.Text = HoaDonBLL.getMaHD().ToString();
        }

        private void btn_ThemChuDe_Click(object sender, EventArgs e)
        {
            ChuDeBLL cd = new ChuDeBLL();
            if (cd.add(txt_MaChuDe.Text, txt_TenChuDe.Text))
            {
                MessageBox.Show("Thêm thành công");
                loadDataChuDe();
            }
            else
            {
                MessageBox.Show("Không thêm được");
            }

        }

        //Load dataGridView dgv_AllChuDe
        private void loadDataChuDe()
        {
            dgv_AllChuDe.DataSource = ChuDeBLL.getChuDe().DataSource;
            txt_MaChuDe.Text = ChuDeBLL.getMa().ToString();
            txt_TenChuDe.ResetText();
            ckbx_ChuDe.ResetText();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// sự kiện khi tabPage được gọi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tpe_ChuDe_Paint(object sender, PaintEventArgs e)
        {
            loadDataChuDe();
        }


        /// <summary>
        /// thay đổi chủ đề
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SuaChuDe_Click(object sender, EventArgs e)
        {
            int ma = int.Parse(txt_MaChuDe.Text);
            bool check = ckbx_ChuDe.Checked;
            ChuDeBLL.edit(ma, txt_TenChuDe.Text, check);
            loadDataChuDe();
            btn_ThemChuDe.Enabled = false;
            btn_SuaChuDe.Enabled = false;
            btn_XoaChuDe.Enabled = false;
        }
        /// <summary>
        /// Hiển thị thông tin chủ đề lên textBox để chỉnh sửa, double click vào row cần sửa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        int rowCurent = 0;
        private void dgv_AllChuDe_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            rowCurent = e.RowIndex;

            if (rowCurent >= 1 && rowCurent <= dgv_AllChuDe.RowCount)
            {
                txt_MaChuDe.Text = dgv_AllChuDe.Rows[rowCurent].Cells[0].Value.ToString();
                txt_TenChuDe.Text = dgv_AllChuDe.Rows[rowCurent].Cells[1].Value.ToString();
                ckbx_ChuDe.Checked = Convert.ToBoolean(dgv_AllChuDe.Rows[rowCurent].Cells[2].Value.ToString());
                btn_ThemChuDe.Enabled = false;
                btn_SuaChuDe.Enabled = true;
                btn_XoaChuDe.Enabled = true;
            }

        }
        /// <summary>
        /// nút "tất cả" hiển thị tất cả chủ đề
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AllChuDe_Click(object sender, EventArgs e)
        {
            loadDataChuDe();
            btn_ThemChuDe.Enabled = true;
            btn_SuaChuDe.Enabled = false;
            btn_XoaChuDe.Enabled = false;
        }
        /// <summary>
        /// trả về ID chủ đề tiếp theo để thêm mới, các textBox khác để trống để thêm mới chủ đề
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ResetChuDe_Click(object sender, EventArgs e)
        {
            loadDataChuDe();
            btn_SuaChuDe.Enabled = false;
            btn_ThemChuDe.Enabled = true;
            btn_ResetChuDe.Enabled = false;
        }

        private void btn_XoaChuDe_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txt_MaChuDe.Text.ToString());
            if (MessageBox.Show("Bạn muốn xóa bản ghi này", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    ChuDeBLL.delete(id);

                }
                catch (Exception)
                {
                    MessageBox.Show("Không xóa được");
                }

            }
            btn_XoaChuDe.Enabled = false;
            loadDataChuDe();

        }


        //Khuyến mại
        private void btn_AllKM_Click(object sender, EventArgs e)
        {
            loadKM();
        }

        public void loadKM()
        {
            cbx_SachKM.DataSource = KhuyenMaiBLL.getAllTenSach().DataSource;
            cbx_SachKM.ValueMember = "ID";
            cbx_SachKM.DisplayMember = "TEN";
            txt_MaKM.Text = KhuyenMaiBLL.getMaKM().ToString();
            dgv_AllKM.DataSource = KhuyenMaiBLL.getAllKM().DataSource;
            dgv_CTKM.DataSource = null;
        }
        private void tcl_Home_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //List chi tiết khuyến mại chờ
        public List<CHITIETKM> listCTKM = new List<CHITIETKM>();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                KHUYENMAI km = new KHUYENMAI();
                km.ID = int.Parse(txt_MaKM.Text);
                km.NGAYBATDAU = dtp_NgBatDauKM.Value;
                km.NGAYKETTHUC = dtp_NgKetThucKM.Value;
                km.ISDELETE = false;
                km.TENKM = txt_TenKM.Text;
                KhuyenMaiBLL newKM = new KhuyenMaiBLL();
                newKM.add(km);
                ChiTietKMBLL newCtkm = new ChiTietKMBLL();
                newCtkm.add(listCTKM);
                MessageBox.Show("Cập nhật thành công");
                txt_TenKM.ResetText();
                dtp_NgBatDauKM.ResetText();
                dtp_NgKetThucKM.ResetText();
                dgv_AllKM.DataSource = null;
                loadKM();
            }
            catch (Exception)
            {

                MessageBox.Show("Không thêm được");
            }

        }
        /// <summary>
        /// Thêm sách khuyến mại vào listCTKM chờ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ThemSachKM_Click(object sender, EventArgs e)
        {
            CHITIETKM ctkm = new CHITIETKM();
            ctkm.ID_SACH = int.Parse(cbx_SachKM.SelectedValue.ToString());
            ctkm.PTKM = Int32.Parse(txt_PtKM.Text.ToString());
            ctkm.ID_KM = int.Parse(txt_MaKM.Text);
            ctkm.ISDELETE = false;
            listCTKM.Add(ctkm);
            txt_PtKM.ResetText();
            cbx_SachKM.ResetText();
            txt_TenKM.Enabled = false;
            dgv_CTKM.DataSource = null;
            dgv_CTKM.DataSource = listCTKM;

        }
        //Mã khuyến mại khi double Click vào dgv_AllKM
        public int Makm;
        public void loadCTKM()
        {
            dgv_CTKM.DataSource = null;
            dgv_CTKM.DataSource = ChiTietKMBLL.getCTKM(Makm).DataSource;
        }

        private void dgv_AllSach_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            int id = int.Parse(dgv_AllKM.Rows[e.RowIndex].Cells[0].Value.ToString());
            Makm = id;
            txt_MaKM.Text = id.ToString();
            txt_TenKM.Text = dgv_AllKM.Rows[e.RowIndex].Cells[1].Value.ToString();
            dtp_NgBatDauKM.Value = DateTime.Parse(dgv_AllKM.Rows[e.RowIndex].Cells[2].Value.ToString());
            dtp_NgKetThucKM.Value = DateTime.Parse(dgv_AllKM.Rows[e.RowIndex].Cells[3].Value.ToString());
            dgv_CTKM.DataSource = ChiTietKMBLL.getCTKM(id).DataSource;
            btn_ThemSachKM.Enabled = false;

        }
        /// <summary>
        /// Xóa các textBox thêm mới
        /// Cài lại dgv_CTKM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_NhapLaiCTKM_Click(object sender, EventArgs e)
        {
            txt_MaKM.Text = KhuyenMaiBLL.getMaKM().ToString();
            txt_TenKM.ResetText();
            dtp_NgBatDauKM.ResetText();
            dtp_NgKetThucKM.ResetText();
            dgv_CTKM.DataSource = null;
            btn_XoaCTKM.Enabled = false;
        }
        //Chỉ số dòng đang chọn trong dgv_CTKM
        public int indexRowCTKM;
        /// <summary>
        /// Sự kiện khi nháy double vào row trong CTKM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_CTKM_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRowCTKM = e.RowIndex;
            cbx_SachKM.SelectedValue = dgv_CTKM.Rows[indexRowCTKM].Cells[1].Value;
            txt_PtKM.Text = dgv_CTKM.Rows[indexRowCTKM].Cells[2].Value.ToString();
        }
        /// <summary>
        /// Sửa phần trăm khuyến mại cho sách trong khuyến mại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            btn_ThemSachKM.Enabled = false;
            ChiTietKMBLL ctkm = new ChiTietKMBLL();
            int makm = int.Parse(txt_MaKM.Text);
            int maSach = int.Parse(dgv_CTKM.Rows[indexRowCTKM].Cells[1].Value.ToString());
            int pTKM = int.Parse(txt_PtKM.Text.ToString());
            ctkm.editCTKM(makm, maSach, pTKM);
            loadCTKM();
        }

        /// <summary>
        /// Xóa khuyến mại sách trong chi tiết hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_XoaCTKM_Click(object sender, EventArgs e)
        {
            ChiTietKMBLL ctkm = new ChiTietKMBLL();
            int maSach = int.Parse(dgv_CTKM.Rows[indexRowCTKM].Cells["ID_Sach"].Value.ToString());
            ctkm.deleteCTKM(Makm, maSach);
            loadCTKM();
        }
        /// <summary>
        /// Xóa khuyến mại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_XoaKM_Click(object sender, EventArgs e)
        {
            KhuyenMaiBLL km = new KhuyenMaiBLL();
            km.deleteKM(Makm);
            loadKM();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Sửa thông tin chi tiết hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Sua_Click(object sender, EventArgs e)
        {
                int mahd = int.Parse(txt_MaHD.Text);
                int maSach = int.Parse(cbx_Sach.SelectedValue.ToString());
                int sl = int.Parse(txt_SLSach.Text);
            if (listCTHD == null)
            {
                
                ChiTietHDBLL.edit(mahd, maSach, sl);
            }
            else
            {
                //var item = (from a in listCTHD where a.ID_SACH == maSach select a).SingleOrDefault();

              listCTHD.Where(n => n.ID_SACH == maSach).SingleOrDefault().SOLUONG=sl;
                

            }
            loadCTHD();
        }
        public int MaHD;
        /// <summary>
        /// load chi tiết hóa đơn
        /// </summary>
        public void loadCTHD()
        {
            if (listCTHD != null)
            {
                dgv_CTHD.DataSource = null;
                dgv_CTHD.DataSource = listCTHD;
            }
            else
            {
                dgv_CTHD.DataSource = null;
                dgv_CTHD.DataSource = ChiTietHDBLL.getCTHDByID(MaHD);
            }
            txt_TongTienHD.Text = HoaDonBLL.tongTienHD(listCTHD).ToString();
        }
        /// <summary>
        /// Load thông tin liên quan đến hóa đơn lên tabpage
        /// </summary>
        public void loadHD()
        {
            //Đổ dữ liệu lên combobox
            cbx_Sach.DataSource = SachBLL.getSach();
            cbx_Sach.DisplayMember = "TEN";
            cbx_Sach.ValueMember = "ID";
            dgv_AllHD.DataSource = HoaDonBLL.getAllHD();
            txt_MaHD.Text = HoaDonBLL.getMaHD().ToString();
            btn_SuaCTHD.Enabled = false;
            btn_Xoa.Enabled = false;
            btn_SuaCTHD.Enabled = false;
            //if (txt_SLSach.Text.Count()==0||cbx_Sach.SelectedValue==null)
            //{
            //    btn_ThemSachHD.Enabled = false;
            //}
            //else
            //{
            //    btn_ThemSachHD.Enabled = true;
            //}
            MaHD = int.Parse(txt_MaHD.Text);
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_all_Click(object sender, EventArgs e)
        {
            loadHD();
        }

        //List chi tiêt hóa đơn chờ cập nhật
        List<CHITIETHOADON> listCTHD = new List<CHITIETHOADON>();
        /// <summary>
        /// Thêm sách vào hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ThemSachHD_Click(object sender, EventArgs e)
        {
            CHITIETHOADON cthd = new CHITIETHOADON();
            cthd.ID_HD = int.Parse(txt_MaHD.Text);
            cthd.ID_SACH = int.Parse(cbx_Sach.SelectedValue.ToString());
            cthd.SOLUONG = int.Parse(txt_SLSach.Text);
            cthd.GIA = SachBLL.getSachID(int.Parse(cbx_Sach.SelectedValue.ToString())).GIABAN;
            cthd.PTKM = ChiTietKMBLL.getPTKMIDSach(int.Parse(cbx_Sach.SelectedValue.ToString()));
            cthd.ISDELETE = false;
            listCTHD.Add(cthd);
            dgv_CTHD.DataSource = null;
            dgv_CTHD.DataSource = listCTHD;

            txt_SLSach.ResetText();
            cbx_Sach.ResetText();
            loadCTHD();
        }

        //Mã rows dgv_CTHD đang chọn
        public int indexRowCTHD;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_CTHD_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            indexRowCTHD = e.RowIndex;
            cbx_Sach.SelectedValue = int.Parse(dgv_CTHD.Rows[indexRowCTHD].Cells[1].Value.ToString());
            txt_SLSach.Text = dgv_CTHD.Rows[indexRowCTHD].Cells[2].Value.ToString();
            btn_ThemSachHD.Enabled = false;
            btn_SuaCTHD.Enabled = true;
            btn_XoaCTHD.Enabled = true;
        }

        private void btn_XoaCTHD_Click(object sender, EventArgs e)
        {
            int maSach= int.Parse(dgv_CTHD.Rows[indexRowCTHD].Cells[1].Value.ToString());
            if (listCTHD!=null)
            {
                var item=listCTHD.Where(n => n.ID_SACH == maSach).SingleOrDefault();
                listCTHD.Remove(item);
            }
            else
            {
                ChiTietHDBLL.deleteCTHD(MaHD, maSach);
            }
            loadCTHD();
        }

        private void btn_Huy_Click(object sender, EventArgs e)
        {
            cbx_Sach.ResetText();
            txt_SLSach.ResetText();
            btn_SuaCTHD.Enabled = false;
            btn_ThemSachHD.Enabled= true;
        }
    }
}
