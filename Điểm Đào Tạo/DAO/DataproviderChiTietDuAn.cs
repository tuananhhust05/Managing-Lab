using MySql.Data.MySqlClient;
using Điểm_Đào_Tạo.Models;
using System.Collections.Generic;

namespace Điểm_Đào_Tạo.DAO
{
    public class DataproviderChiTietDuAn
    {
        private static DataproviderChiTietDuAn instance; // tất cả nhứng thằng tạo qua thắng này đều chỉ xuất hiện 1 lần 
        // đóng gói nó -> cú pháp Ctrl + R+ E ,đặt chuột ở ngay thằng instance 
        public static DataproviderChiTietDuAn Instance
        {
            get { if (instance == null) instance = new DataproviderChiTietDuAn(); return DataproviderChiTietDuAn.instance; } // phòng trường hợp thằng instance rỗng 
            private set { DataproviderChiTietDuAn.instance = value; } // chỉ nội bộ class sử dụng , không đc lấy ra 
        }
        private DataproviderChiTietDuAn() { }  // cái này để chắc chắn cái Singleton kia là private , lưu ý dấu ngoặc kép đằng sau 



        private string connectionSTR = "server=localhost;user=root;database=webnhansu;port=3306;password=123456"; // kết nối với database new_schema 


        public List<ChiTietDuAn> ExecuteQuery(string query, object[] parameter = null) // trả về 1 danh sách abc 
        {
            List<ChiTietDuAn> TkDangNhaps = new List<ChiTietDuAn>();// tạo danh sách các đối tượng abc 
            MySqlConnection con = new MySqlConnection(connectionSTR);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())  // thằng reader này đọc dữ liệu trong bảng
            {
                ChiTietDuAn TkDangNhap = new ChiTietDuAn();
                TkDangNhap.ID = int.Parse(reader["ID"].ToString());
                TkDangNhap.LabID = int.Parse( reader["LabID"].ToString()); // ép về kiểu nguyên 
                TkDangNhap.Ten = reader["Ten"].ToString();
                TkDangNhap.Tenduan = reader["Ten du an"].ToString();  // ép về kiểu string 
                TkDangNhap.Chucvu = reader["ChucVu"].ToString(); // ép về kiểu nguyên 
                TkDangNhap.BatDau = reader["BatDau"].ToString();
                TkDangNhap.KetThuc = reader["KetThuc"].ToString();
                TkDangNhap.Danhgia = reader["DanhGia"].ToString();  // ép về ki
                // khớp là được 

                TkDangNhaps.Add(TkDangNhap);
            }
            reader.Close();

            return TkDangNhaps;
        }
    }
}
