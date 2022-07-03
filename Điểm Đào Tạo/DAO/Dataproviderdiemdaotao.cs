using Điểm_Đào_Tạo.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Điểm_Đào_Tạo.DAO
{
    public class Dataproviderdiemdaotao
    {
        private static Dataproviderdiemdaotao instance; // tất cả nhứng thằng tạo qua thắng này đều chỉ xuất hiện 1 lần 
        // đóng gói nó -> cú pháp Ctrl + R+ E ,đặt chuột ở ngay thằng instance 
        public static Dataproviderdiemdaotao Instance
        {
            get { if (instance == null) instance = new Dataproviderdiemdaotao(); return Dataproviderdiemdaotao.instance; } // phòng trường hợp thằng instance rỗng 
            private set { Dataproviderdiemdaotao.instance = value; } // chỉ nội bộ class sử dụng , không đc lấy ra 
        }
        private Dataproviderdiemdaotao() { }  // cái này để chắc chắn cái Singleton kia là private , lưu ý dấu ngoặc kép đằng sau 



        private string connectionSTR = "server=localhost;user=root;database=webnhansu;port=3306;password=123456"; // kết nối với database new_schema 


        public List<DiemDaoTao> ExecuteQuery(string query, object[] parameter = null) // trả về 1 danh sách abc 
        {
            List<DiemDaoTao> TkDangNhaps = new List<DiemDaoTao>();// tạo danh sách các đối tượng abc 
            MySqlConnection con = new MySqlConnection(connectionSTR);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())  // thằng reader này đọc dữ liệu trong bảng
            {
                DiemDaoTao TkDangNhap = new DiemDaoTao();
                TkDangNhap.LabID = int.Parse(reader["LabID"].ToString()); // ép về kiểu nguyên 
                TkDangNhap.Ten = reader["Ten"].ToString();  // ép về kiểu string
                TkDangNhap.Diem = int.Parse(reader["Diem"].ToString()); // ép về kiểu nguyên 

                // khớp là được 

                TkDangNhaps.Add(TkDangNhap);
            }
            reader.Close();

            return TkDangNhaps;
        }
    }
}
