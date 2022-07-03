using Điểm_Đào_Tạo.Models;
using System.Collections.Generic;

namespace Điểm_Đào_Tạo.ViewModels
{
    public class FullBangViewModel
    {   // mỗi bảng 1 thằng 
        public List<ChiTietDuAn> chitietduan { get; set; }
        public List<ChungChi> chungchi { get; set; }
        public List<DiemDaoTao> diemptba { get; set; }
        public List<DiemDaoTao> diemptcokhi { get; set; }
        public List<DiemDaoTao> diemptlaptrinh { get; set; }
        public List<DiemDaoTao> diemptngoaingu  { get; set; }
        public List<DiemDaoTao> diemptptbt { get; set; }
        public List<DiemDaoTao> diempttudonghoa { get; set; }
        public List<DiemDaoTao> diemptquantridoanhnghiep { get; set; }
       
        public List<diemptbt>ptbt { get; set; }
        public List<diemythuc> ythuc { get; set; }
        public List<duan> dsduan { get; set; }
        public List<MainTable> bangchinh { get; set; }
        public List<TKDangNhap> taikhoan { get; set; }
        public List<LT> leader { get; set; }
        public List<TKDangNhap> phanquyenlt { get; set; }

    }
}
