using Điểm_Đào_Tạo.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Điểm_Đào_Tạo.DAO
{
    public class DataproviderCommentBox
    {
        private static DataproviderCommentBox instance; // tất cả nhứng thằng tạo qua thắng này đều chỉ xuất hiện 1 lần 
        // đóng gói nó -> cú pháp Ctrl + R+ E ,đặt chuột ở ngay thằng instance 
        public static DataproviderCommentBox Instance
        {
            get { if (instance == null) instance = new DataproviderCommentBox(); return DataproviderCommentBox.instance; } // phòng trường hợp thằng instance rỗng 
            private set { DataproviderCommentBox.instance = value; } // chỉ nội bộ class sử dụng , không đc lấy ra 
        }
        private DataproviderCommentBox() { }  // cái này để chắc chắn cái Singleton kia là private , lưu ý dấu ngoặc kép đằng sau 



        private string connectionSTR = "server=localhost;user=root;database=webnhansu;port=3306;password=123456"; // kết nối với database new_schema 


        public List<CommentBox> ExecuteQuery(string query, object[] parameter = null) // trả về 1 danh sách abc 
        {
            List<CommentBox> TkDangNhaps = new List<CommentBox>();// tạo danh sách các đối tượng abc 
            MySqlConnection con = new MySqlConnection(connectionSTR);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())  // thằng reader này đọc dữ liệu trong bảng
            {
                CommentBox TkDangNhap = new CommentBox();
                TkDangNhap.ID = int.Parse(reader["ID"].ToString());
                TkDangNhap.LabID = int.Parse(reader["LabID"].ToString()); // ép về kiểu nguyên 
                TkDangNhap.Ten = reader["Ten"].ToString();
                TkDangNhap.Image = reader["Image"].ToString();  // ép về kiểu string 
                TkDangNhap.Comment = reader["Comment"].ToString(); // ép về kiểu nguyên 
                TkDangNhap.Time = reader["Time"].ToString();
              
                // khớp là được 

                TkDangNhaps.Add(TkDangNhap);
            }
            reader.Close();

            return TkDangNhaps;
        }
    }
}
