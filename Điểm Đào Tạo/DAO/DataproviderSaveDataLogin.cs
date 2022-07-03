using Điểm_Đào_Tạo.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Điểm_Đào_Tạo.DAO
{
    public class DataproviderSaveDataLogin
    {
        private static DataproviderSaveDataLogin instance; // tất cả nhứng thằng tạo qua thắng này đều chỉ xuất hiện 1 lần 
        // đóng gói nó -> cú pháp Ctrl + R+ E ,đặt chuột ở ngay thằng instance 
        public static DataproviderSaveDataLogin Instance
        {
            get { if (instance == null) instance = new DataproviderSaveDataLogin(); return DataproviderSaveDataLogin.instance; } // phòng trường hợp thằng instance rỗng 
            private set { DataproviderSaveDataLogin.instance = value; } // chỉ nội bộ class sử dụng , không đc lấy ra 
        }
        private DataproviderSaveDataLogin() { }  // cái này để chắc chắn cái Singleton kia là private , lưu ý dấu ngoặc kép đằng sau 



        private string connectionSTR = "server=localhost;user=root;database=webnhansu;port=3306;password=123456"; // kết nối với database new_schema 


        public List<SaveDataLogin> ExecuteQuery(string query, object[] parameter = null) // trả về 1 danh sách abc 
        {
            List<SaveDataLogin> TkDangNhaps = new List<SaveDataLogin>();// tạo danh sách các đối tượng abc 
            MySqlConnection con = new MySqlConnection(connectionSTR);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())  
            {
                SaveDataLogin TkDangNhap1 = new SaveDataLogin();
                TkDangNhap1.LabID = int.Parse(reader["LabID"].ToString()); // ép về kiểu nguyên 
                TkDangNhap1.Time = reader["Time"].ToString();  // ép về kiểu string
                TkDangNhap1.Query = reader["Query"].ToString(); // ép về kiểu nguyên 
               

                TkDangNhaps.Add(TkDangNhap1);
            }
            reader.Close();

            return TkDangNhaps;
        }
    }
}
