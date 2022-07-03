using Điểm_Đào_Tạo.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Điểm_Đào_Tạo.DAO
{
    public class DataproviderChungChi
    {
        private static DataproviderChungChi instance; // tất cả nhứng thằng tạo qua thắng này đều chỉ xuất hiện 1 lần 
        // đóng gói nó -> cú pháp Ctrl + R+ E ,đặt chuột ở ngay thằng instance 
        public static DataproviderChungChi Instance
        {
            get { if (instance == null) instance = new DataproviderChungChi(); return DataproviderChungChi.instance; } // phòng trường hợp thằng instance rỗng 
            private set { DataproviderChungChi.instance = value; } // chỉ nội bộ class sử dụng , không đc lấy ra 
        }
        private DataproviderChungChi() { }  // cái này để chắc chắn cái Singleton kia là private , lưu ý dấu ngoặc kép đằng sau 



        private string connectionSTR = "server=localhost;user=root;database=webnhansu;port=3306;password=123456"; // kết nối với database new_schema 


        public List<ChungChi> ExecuteQuery(string query, object[] parameter = null) // trả về 1 danh sách abc 
        {
            List<ChungChi> TkDangNhaps = new List<ChungChi>();// tạo danh sách các đối tượng abc 
            MySqlConnection con = new MySqlConnection(connectionSTR);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())  // thằng reader này đọc dữ liệu trong bảng
            {
                ChungChi TkDangNhap1 = new ChungChi();
                TkDangNhap1.ID = int.Parse(reader["ID"].ToString());
                TkDangNhap1.LabID = int.Parse(reader["LabID"].ToString()); // ép về kiểu nguyên 
                TkDangNhap1.Ten = reader["Ten"].ToString();  // ép về kiểu string
                TkDangNhap1.Tenchungchi = reader["Tenchungchi"].ToString(); // ép về kiểu nguyên 
               

                // khớp là được 

                TkDangNhaps.Add(TkDangNhap1);
            }
            reader.Close();

            return TkDangNhaps;
        }
    }
}
