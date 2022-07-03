using Điểm_Đào_Tạo.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Điểm_Đào_Tạo.DAO
{
    public class DataproviderTKDangNhap
    {
        private static DataproviderTKDangNhap instance; // tất cả nhứng thằng tạo qua thắng này đều chỉ xuất hiện 1 lần 
        // đóng gói nó -> cú pháp Ctrl + R+ E ,đặt chuột ở ngay thằng instance 
        public static DataproviderTKDangNhap Instance
        {
            get { if (instance == null) instance = new DataproviderTKDangNhap(); return DataproviderTKDangNhap.instance; } // phòng trường hợp thằng instance rỗng 
            private set { DataproviderTKDangNhap.instance = value; } // chỉ nội bộ class sử dụng , không đc lấy ra 
        }
        private DataproviderTKDangNhap() { }  // cái này để chắc chắn cái Singleton kia là private , lưu ý dấu ngoặc kép đằng sau 



        private string connectionSTR = "server=localhost;user=root;database=webnhansu;port=3306;password=123456"; // kết nối với database new_schema 


        public List<TKDangNhap> ExecuteQuery(string query, object[] parameter = null) // trả về 1 danh sách abc 
        {
            List<TKDangNhap> TkDangNhaps = new List<TKDangNhap>();// tạo danh sách các đối tượng abc 
            MySqlConnection con = new MySqlConnection(connectionSTR);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())  // thằng reader này đọc dữ liệu trong bảng
            {
                TKDangNhap TkDangNhap1 = new TKDangNhap();
                TkDangNhap1.LabID = int.Parse(reader["LabID"].ToString()); // ép về kiểu nguyên 
                TkDangNhap1.MatKhau = reader["MatKhau"].ToString();  // ép về kiểu string
                

                // khớp là được 

                TkDangNhaps.Add(TkDangNhap1);
            }
            reader.Close();

            return TkDangNhaps;
        }
    }
}
