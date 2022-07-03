using System;
using System.Collections.Generic;

namespace Điểm_Đào_Tạo.Models
{
    public class Bangtaikhoan
    {
        public Dictionary<String, String> LoaiVaTenBang { get; set; }
        public String LoaiBang { get; set; }
        public String TenBang { get; set; }
        public List<TKDangNhap> Danhsachtaikhoan { get; set; }
    }
}
