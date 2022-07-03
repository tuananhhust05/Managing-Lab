using Điểm_Đào_Tạo.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Điểm_Đào_Tạo.DAO
{
    public class DataproviderBangChinh
    {
        private static DataproviderBangChinh instance; // tất cả nhứng thằng tạo qua thắng này đều chỉ xuất hiện 1 lần 
        // đóng gói nó -> cú pháp Ctrl + R+ E ,đặt chuột ở ngay thằng instance 
        public static DataproviderBangChinh Instance
        {
            get { if (instance == null) instance = new DataproviderBangChinh(); return DataproviderBangChinh.instance; } // phòng trường hợp thằng instance rỗng 
            private set { DataproviderBangChinh.instance = value; } // chỉ nội bộ class sử dụng , không đc lấy ra 
        }
        private DataproviderBangChinh() { }  // cái này để chắc chắn cái Singleton kia là private , lưu ý dấu ngoặc kép đằng sau 



        private string connectionSTR = "server=localhost;user=root;database=webnhansu;port=3306;password=123456"; // kết nối với database new_schema 


        public List<MainTable> ExecuteQuery(string query, object[] parameter = null) // trả về 1 danh sách abc 
        {
            List<MainTable> TkDangNhaps = new List<MainTable>();// tạo danh sách các đối tượng abc 
            MySqlConnection con = new MySqlConnection(connectionSTR);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())  // thằng reader này đọc dữ liệu trong bảng
            {
                MainTable TkDangNhap1 = new MainTable();
                TkDangNhap1.LabID = int.Parse(reader["LabID"].ToString()); // ép về kiểu nguyên 
                TkDangNhap1.Ten = reader["Ten"].ToString();  // ép về kiểu string
                TkDangNhap1.TheHe= reader["TheHe"].ToString(); // ép về kiểu nguyên 
                TkDangNhap1.SDT = reader["SDT"].ToString();
                TkDangNhap1.NganhHoc = reader["NganhHoc"].ToString(); // ép về kiểu nguyên 
                TkDangNhap1.TruongHoc = reader["TruongHoc"].ToString();
                TkDangNhap1.email = reader["email"].ToString();
                TkDangNhap1.QueQuan = reader["QueQuan"].ToString();
                TkDangNhap1.TrangThai= reader["TrangThai"].ToString();
                TkDangNhap1.Image = reader["Image"].ToString();
                // khớp là được 

                TkDangNhaps.Add(TkDangNhap1);
            }
            reader.Close();

            return TkDangNhaps;
        }
    }
}
