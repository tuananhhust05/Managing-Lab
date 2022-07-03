using MySql.Data.MySqlClient;
using Điểm_Đào_Tạo.Models;
using System.Collections.Generic;

namespace Điểm_Đào_Tạo.DAO
{
    public class DataproviderDuan
    {
        private static DataproviderDuan instance; // tất cả nhứng thằng tạo qua thắng này đều chỉ xuất hiện 1 lần 
        // đóng gói nó -> cú pháp Ctrl + R+ E ,đặt chuột ở ngay thằng instance 
        public static DataproviderDuan Instance
        {
            get { if (instance == null) instance = new DataproviderDuan(); return DataproviderDuan.instance; } // phòng trường hợp thằng instance rỗng 
            private set { DataproviderDuan.instance = value; } // chỉ nội bộ class sử dụng , không đc lấy ra 
        }
        private DataproviderDuan() { }  // cái này để chắc chắn cái Singleton kia là private , lưu ý dấu ngoặc kép đằng sau 



        private string connectionSTR = "server=localhost;user=root;database=webnhansu;port=3306;password=123456"; // kết nối với database new_schema 


        public List<duan> ExecuteQuery(string query, object[] parameter = null) // trả về 1 danh sách abc 
        {
            List<duan> TkDangNhaps = new List<duan>();// tạo danh sách các đối tượng abc 
            MySqlConnection con = new MySqlConnection(connectionSTR);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())  // thằng reader này đọc dữ liệu trong bảng
            {
                duan TkDangNhap = new duan();
                TkDangNhap.tenduan = reader["ten du an"].ToString(); // ép về kiểu nguyên 
                TkDangNhap.maduan = reader["ma du an"].ToString();  // ép về kiểu string
                


                TkDangNhaps.Add(TkDangNhap);
            }
            reader.Close();

            return TkDangNhaps;
        }
    }
}
