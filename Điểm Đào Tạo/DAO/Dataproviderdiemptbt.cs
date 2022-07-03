using MySql.Data.MySqlClient;
using Điểm_Đào_Tạo.Models;
using System.Collections.Generic;

namespace Điểm_Đào_Tạo.DAO
{
    public class Dataproviderdiemptbt
    {
        private static Dataproviderdiemptbt instance; // tất cả nhứng thằng tạo qua thắng này đều chỉ xuất hiện 1 lần 
        // đóng gói nó -> cú pháp Ctrl + R+ E ,đặt chuột ở ngay thằng instance 
        public static Dataproviderdiemptbt Instance
        {
            get { if (instance == null) instance = new Dataproviderdiemptbt(); return Dataproviderdiemptbt.instance; } // phòng trường hợp thằng instance rỗng 
            private set { Dataproviderdiemptbt.instance = value; } // chỉ nội bộ class sử dụng , không đc lấy ra 
        }
        private Dataproviderdiemptbt() { }  // cái này để chắc chắn cái Singleton kia là private , lưu ý dấu ngoặc kép đằng sau 



        private string connectionSTR = "server=localhost;user=root;database=webnhansu;port=3306;password=123456"; // kết nối với database new_schema 


        public List<diemptbt> ExecuteQuery(string query, object[] parameter = null) // trả về 1 danh sách abc 
        {
            List<diemptbt> TkDangNhaps = new List<diemptbt>();// tạo danh sách các đối tượng abc 
            MySqlConnection con = new MySqlConnection(connectionSTR);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())  // thằng reader này đọc dữ liệu trong bảng
            {
                diemptbt TkDangNhap = new diemptbt();
                TkDangNhap.LabID = int.Parse(reader["LabID"].ToString()); // ép về kiểu nguyên 
                TkDangNhap.Ten = reader["Ten"].ToString();  // ép về kiểu string
                TkDangNhap.DiemPTBT = int.Parse(reader["DiemPTBT"].ToString()); // ép về kiểu nguyên 

                // khớp là được 

                TkDangNhaps.Add(TkDangNhap);
            }
            reader.Close();

            return TkDangNhaps;
        }
    }
}
