using Điểm_Đào_Tạo.DAO;
using Điểm_Đào_Tạo.Models;
using ClosedXML.Excel;

using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Điểm_Đào_Tạo.Controllers
{
    public class QuanLyBangChinhController : Controller
    {
        private readonly IWebHostEnvironment _iweb;

        //contructor 
        public QuanLyBangChinhController(IWebHostEnvironment iweb)
        {
            _iweb = iweb;
        }
        // xuất ra màn hình danh sách 

        //1. Lấy dữ liệu từ các bảng 
        public List<memberInfo> Getdata(int LabID = 0)
        {



            // Tạo danh sách bảng chính
            List<MainTable> members = new List<MainTable>();

            // Tạo danh sách thông tin ta cần hiển thị. Class này sẽ tự tính nghĩa trong models
            List<memberInfo> memberInfo = new List<memberInfo>();

            // Tạo danh sách chứng chỉ
            List<ChungChi> chungchi_list = new List<ChungChi>();

            // Tạo dánh sách chi tiết dự an
            List<ChiTietDuAn> chitietduan_list = new List<ChiTietDuAn>();

            // Tạo danh sách LT
            List<LT> LT_list = new List<LT>();

            // Lấy thông tin toàn bộ các user ra
            if (LabID != 0)
                members = DataproviderBangChinh.Instance.ExecuteQuery($"select * from webnhansu.maintable where LabID = {LabID}");
            else
                members = DataproviderBangChinh.Instance.ExecuteQuery("select * from webnhansu.maintable");
            foreach (var member in members)
            {
                // Tạo đối tượng member1 từ class memberInfo
                memberInfo member1 = new memberInfo();

                // Lấy tất cả các chứng chỉ của 1 member
                chungchi_list = DataproviderChungChi.Instance.ExecuteQuery($"select * from webnhansu.chungchi where LabID = {member.LabID}");


                // Lấy tât cả các dự án của 1 member
                chitietduan_list = DataproviderChiTietDuAn.Instance.ExecuteQuery($"select * from webnhansu.chitietduan where LabID = {member.LabID}");


                // Lấy tất cả danh sách chưc vụ của 1 member
                LT_list = DataproviderLT.Instance.ExecuteQuery($"select * from webnhansu.lt where LabID = {member.LabID} ");  // lưu ý tên table 


                // Gán thông tin
                member1.LabID = member.LabID;
                member1.Ten = member.Ten;
                member1.TheHe = member.TheHe;
                member1.SDT = member.SDT;
                member1.NganhHoc = member.NganhHoc;
                member1.TruongHoc = member.TruongHoc;
                member1.email = member.email;
                member1.QueQuan = member.QueQuan;
                member1.TrangThai = member.TrangThai;
                member1.Image = member.Image;
                member1.Chungchi = "";
                // Ghép chuỗi tên các chứng chỉ
                foreach (var chungchi in chungchi_list)
                {
                    member1.Chungchi += chungchi.Tenchungchi + ", ";
                }
                // Bỏ đi ", " ở cuối chuỗi
               // member1.Chungchi = member1.Chungchi.Substring(0, member1.Chungchi.Length - 2);

                member1.Duan = "";
                // Ghép chuỗi tên các dự án
                foreach (var chitietduan in chitietduan_list)
                {
                    member1.Duan += chitietduan.Tenduan + ", ";
                }
                // Bỏ đi ", " ở cuối chuỗi
                //member1.Duan = member1.Duan.Substring(0, member1.Duan.Length - 2);

                member1.ChucVu = "";
                // Ghép chuỗi tên các chức vụ
                foreach (var LT in LT_list)
                {
                    member1.ChucVu += LT.ChucVu + ", ";
                }
                // Bỏ đi ", " ở cuối chuỗi
                //member1.ChucVu = member1.ChucVu.Substring(0, member1.ChucVu.Length - 2);


                // Thêm vào danh sách
                memberInfo.Add(member1);
            }
            return memberInfo;
        }
         // trả về màn hình 
        public IActionResult Index()
        {

            List<memberInfo> memberInfo = Getdata();
            return View(memberInfo);
        }






        //------------------------------------------------------------------------------------
        // xuất dữ liệu bảng chính ra excel 
        public IActionResult ExportDataShowToExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("LT");
                var currentRow = 1;
                // trỏ đến dòng 1 và cột 1 thay giá trị bằng LabID các dòng dưới cx tương tự
                worksheet.Cell(currentRow, 1).Value = "LabId";
                worksheet.Cell(currentRow, 2).Value = "Họ và Tên";
                worksheet.Cell(currentRow, 3).Value = "Thế hệ";
                worksheet.Cell(currentRow, 4).Value = "Số điện thoại";
                worksheet.Cell(currentRow, 5).Value = "Ngành học";
                worksheet.Cell(currentRow, 6).Value = "Email";
                worksheet.Cell(currentRow, 7).Value = "Quê quán";
                worksheet.Cell(currentRow, 8).Value = "Trạng thái";
                worksheet.Cell(currentRow, 9).Value = "Chức vụ";
                worksheet.Cell(currentRow, 10).Value = "Chứng chỉ";
                worksheet.Cell(currentRow, 11).Value = "Dự án";


                // Lấy dữ liệu trong database qua Hàm getdata
                List<memberInfo> memberInfo = Getdata();
                foreach (var member in memberInfo)
                {
                    // Dòng thứ 2 trở đi sẽ đổ dữ liệu từ database vào
                    currentRow += 1;
                    //dòng 2 cột 1 điền ID của user
                    worksheet.Cell(currentRow, 1).Value = member.LabID;
                    worksheet.Cell(currentRow, 2).Value = member.Ten;
                    worksheet.Cell(currentRow, 3).Value = member.TheHe;
                    worksheet.Cell(currentRow, 4).Value = member.SDT;
                    worksheet.Cell(currentRow, 5).Value = member.NganhHoc;
                    worksheet.Cell(currentRow, 6).Value = member.email;
                    worksheet.Cell(currentRow, 7).Value = member.QueQuan;
                    worksheet.Cell(currentRow, 8).Value = member.TrangThai;
                    worksheet.Cell(currentRow, 9).Value = member.ChucVu;
                    worksheet.Cell(currentRow, 10).Value = member.Chungchi;
                    worksheet.Cell(currentRow, 11).Value = member.Duan;
                }
                // Trả về dữ liệu dạng xlsx
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "member_Info.xlsx");
                }


            }
        }

        //----------------------------------------------------------------
        //Tìm kiếm dữ liệu 
        public IActionResult searchMemberInfo(string LabID)
        {

            List<memberInfo> memberInfo = Getdata(Convert.ToInt32(LabID));
            return View(memberInfo);
        }


        //----------------------------------------------------------------------------------------------------------
        // Thêm TK bằng file excel 
        [HttpGet]
        public IActionResult themtkbangex()
        {
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            List<TKDangNhap> TkDangNhaps1 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyennhansu`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps1.Count > 0)
            { return View(); }
            else
            {
                return RedirectToAction("Index", "TkSuper");// trả về trang chủ 
            }
           
        }
        [HttpPost]
        [System.Obsolete]
        public IActionResult themtkbangex(IFormFile file, [FromServices] IHostingEnvironment hostingEnviroment)
        {
            string fileName = $"{hostingEnviroment.WebRootPath}\\files\\{file.FileName}";  // địa chỉ để tải file lên => sau đó đọc
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            var bangdiems = this.GetStudentList1(file.FileName);


            return View(bangdiems);  // truyền vào danh sách những đối tượng lấy từ file excel 
        }
        private List<NhanDuLieuVaoBangChinhEx> GetStudentList1(string fName) // hàm phụ 
        {
            List<NhanDuLieuVaoBangChinhEx> bangdiems = new List<NhanDuLieuVaoBangChinhEx>();  // lấy dữ liệu từ file excel 
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())// nếu còn có thể đọc 
                    {
                        NhanDuLieuVaoBangChinhEx a = new NhanDuLieuVaoBangChinhEx();
                        a.LabID = int.Parse(reader.GetValue(0).ToString());
                        a.Ten = reader.GetValue(1).ToString();
                        a.TheHe = reader.GetValue(2).ToString();
                        a.SDT = reader.GetValue(3).ToString();
                        a.NganhHoc = reader.GetValue(4).ToString();
                        a.TruongHoc = reader.GetValue(5).ToString();
                        a.email = reader.GetValue(6).ToString();
                        a.QueQuan = reader.GetValue(7).ToString();
                        a.PowerTeam = reader.GetValue(8).ToString();
                        a.ChungChi = reader.GetValue(9).ToString();
                        a.TrangThai = reader.GetValue(10).ToString();
                        bangdiems.Add(a);
                    }
                }
            }
            foreach (var item in bangdiems)
            {   // thêm dữ liệu vào bảng chính 
                List<MainTable> bangdiems2 = DataproviderBangChinh.Instance.ExecuteQuery("INSERT INTO  `webnhansu`.`maintable`  VALUES(" + item.LabID + ",'" + item.Ten + "','" + item.TheHe + "','" + item.SDT + "','" + item.NganhHoc + "','" + item.TruongHoc + "','" + item.email + "','" + item.QueQuan + "','" + item.TrangThai + "');");// xóa thằng cũ 

                // insert data to bang y thuc 
                List<diemythuc> bangdiemythuc = Dataproviderdiemythuc.Instance.ExecuteQuery("INSERT INTO  webnhansu.diemythuc VALUES(" + item.LabID + ",'" + item.Ten + "'," + 0 + ")");

                // insert data to bang ptbt 
                List<diemptbt> bangdiemptbt = Dataproviderdiemptbt.Instance.ExecuteQuery("INSERT INTO  webnhansu.diemptbt VALUES(" + item.LabID + ",'" + item.Ten + "'," + 0 + ")");

                // automatic insert data to tkdangnhap tavble 
                List<TKDangNhap> DangNhapTaiKhoanUser  = DataproviderTKDangNhap.Instance.ExecuteQuery("INSERT INTO  webnhansu.tkdangnhap VALUES(" + item.LabID + ",'1')");
                // thêm vào điểm PT 
                if (item.PowerTeam == "LT")
                {
                    List<DiemDaoTao> bangdiems3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("INSERT INTO  webnhansu.diemptlaptrinh VALUES(" + item.LabID + ",'" + item.Ten + "'," + 0 + ")");// thêm điểm là 0
                }
                else if (item.PowerTeam == "TDH")
                {
                    List<DiemDaoTao> bangdiems3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("INSERT INTO  webnhansu.diempttudonghoa VALUES(" + item.LabID + ",'" + item.Ten + "'," + 0 + ")");
                }
                else if (item.PowerTeam == "BA")
                {
                    List<DiemDaoTao> bangdiems3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("INSERT INTO  webnhansu.diemptba VALUES(" + item.LabID + ",'" + item.Ten + "'," + 0 + ")");// thêm điểm là 0
                }
                else if (item.PowerTeam == "NN")
                {
                    List<DiemDaoTao> bangdiems3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("INSERT INTO  webnhansu.diemptngoaingu VALUES(" + item.LabID + ",'" + item.Ten + "'," + 0 + ")");
                }
                else if (item.PowerTeam == "QTDN")
                {
                    List<DiemDaoTao> bangdiems3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("INSERT INTO  webnhansu.diemptquantridoanhnghiep VALUES(" + item.LabID + ",'" + item.Ten + "'," + 0 + ")");// thêm điểm là 0
                }
                else if (item.PowerTeam == "PTBT")
                {
                    List<DiemDaoTao> bangdiems3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("INSERT INTO  webnhansu.diemptptbt VALUES(" + item.LabID + ",'" + item.Ten + "'," + 0 + ")");
                }
                else
                {
                    List<DiemDaoTao> bangdiems3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("INSERT INTO  webnhansu.diemptcokhi VALUES(" + item.LabID + ",'" + item.Ten + "'," + 0 + ")");
                }

                // thêm dữ liệu vào bảng chứng chỉ 

                string[] ChungChiList = item.ChungChi.ToString().Split(",");
                // lấy số lượng đối tượng bảng chứng chỉ 
                List<ChungChi> chungchis = DataproviderChungChi.Instance.ExecuteQuery("SELECT * FROM webnhansu.chungchi");
                int a = chungchis.Count;
                for (int i = 1; i <= ChungChiList.Count(); i++)
                {
                    List<ChungChi> chungchis2 = DataproviderChungChi.Instance.ExecuteQuery("INSERT  INTO webnhansu.chungchi VALUES(" + (a + i) + "," + item.LabID + ",'" + item.Ten + "','" + ChungChiList[i - 1] + "')");
                }
            }

            // lưu phiên làm việc 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            var chinhsua = DataproviderSaveDataLogin.Instance.ExecuteQuery("INSERT INTO webnhansu.savedataedit VALUES(" + tkdangnhap.LabID + ",'" + DateTime.Now.ToString() + "','Thêm Tài Khoản bảng Chính')");

            return bangdiems;

            // trả về danh sách điểm sau khi được cộng, điểm cộng thì có ở file excel r 
        }




      
       







        // cập nhật trạng thái hoạt động bằng excel 
        // phương thức GET2 
        [HttpGet]
        public IActionResult capnhattrangthai()
        {
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            List<TKDangNhap> TkDangNhaps1 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyennhansu`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps1.Count > 0)
            { return View(); }
            else
            {
                return RedirectToAction("Index", "TkSuper");// trả về trang chủ 
            }
        }
        [HttpPost]
        [System.Obsolete]
        public IActionResult capnhattrangthai(IFormFile file, [FromServices] IHostingEnvironment hostingEnviroment)
        {
            string fileName = $"{hostingEnviroment.WebRootPath}\\files\\{file.FileName}";  // địa chỉ để tải file lên => sau đó đọc
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            var bangdiems = this.GetStudentList2(file.FileName);


            return View(bangdiems);  // truyền vào danh sách những đối tượng lấy từ file excel 
        }
        private List<NhanDuLieuVaoBangChinhEx> GetStudentList2(string fName) // hàm phụ 
        {
            List<NhanDuLieuVaoBangChinhEx> bangdiems = new List<NhanDuLieuVaoBangChinhEx>();  // lấy dữ liệu từ file excel 
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())// nếu còn có thể đọc 
                    {
                        NhanDuLieuVaoBangChinhEx a = new NhanDuLieuVaoBangChinhEx();
                        // nhận khuyết dữ liệu 
                        a.LabID = int.Parse(reader.GetValue(0).ToString());

                        a.TrangThai = reader.GetValue(1).ToString();
                        bangdiems.Add(a);
                    }
                }
            }
            List<MainTable> final = new List<MainTable>();
            foreach (var item in bangdiems)
            {
                // chọn để lưu dữ liệu lại 

                List<MainTable> bangdiems1 = DataproviderBangChinh.Instance.ExecuteQuery("SELECT *FROM webnhansu.`maintable`WHERE `LabID`=" + item.LabID + "");

                // xóa dữ liệu cũ 

                string query2 = "DELETE FROM webnhansu.`maintable`WHERE `LabID`=" + item.LabID + "";
                List<MainTable> bangdiems2 = DataproviderBangChinh.Instance.ExecuteQuery(query2);
                //
                // thêm vào dữ liệu mới 
                // null do lẫn những ông trước 
                List<MainTable> bangdiems3 = DataproviderBangChinh.Instance.ExecuteQuery("INSERT INTO  `webnhansu`.`maintable`  VALUES(" + item.LabID + ",'" + bangdiems1[0].Ten + "','" + bangdiems1[0].TheHe + "','" + bangdiems1[0].SDT + "','" + bangdiems1[0].NganhHoc + "','" + bangdiems1[0].TruongHoc + "','" + bangdiems1[0].email + "','" + bangdiems1[0].QueQuan + "','" + item.TrangThai + "');");

                // thêm những đối tượng còn thiếu vào 
                string query4 = "SELECT *FROM webnhansu.`maintable`WHERE `LabID`=" + item.LabID + "";
                List<MainTable> bangdiems4 = DataproviderBangChinh.Instance.ExecuteQuery(query4);
                foreach (var item2 in bangdiems4)
                {
                    final.Add(item2);
                }
            }
            bangdiems.RemoveRange(0, bangdiems.Count); // xóa hết phần tử của Bảng điếm 
            foreach (var item in final)
            {
                NhanDuLieuVaoBangChinhEx b = new NhanDuLieuVaoBangChinhEx();
                b.LabID = item.LabID;
                b.Ten = item.Ten;
                b.TheHe = item.TheHe;
                b.SDT = item.SDT;
                b.NganhHoc = item.NganhHoc;
                b.TruongHoc = item.TruongHoc;
                b.email = item.email;
                b.QueQuan = item.QueQuan;
                b.TrangThai = item.TrangThai;
                bangdiems.Add(b);
            }
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            var chinhsua = DataproviderSaveDataLogin.Instance.ExecuteQuery("INSERT INTO webnhansu.savedataedit VALUES(" + tkdangnhap.LabID + ",'" + DateTime.Now.ToString() + "','Chỉnh sửa trạng thái hoạt động ')");

            return bangdiems;

            // trả về danh sách điểm sau khi được cộng, điểm cộng thì có ở file excel r
        }







        // danh sach thanh vien hoat dong 
        //1. tìm 
        public IActionResult danhsachthanhvienhoatdong()
        {
            string query = "SELECT * FROM webnhansu.maintable WHERE `TrangThai`='Active'";
            List<MainTable> bangdiems = DataproviderBangChinh.Instance.ExecuteQuery(query);
            return View(bangdiems);
        }

        // xuất excel 
        public IActionResult xuatdanhsachhoatdongex()
        {

            string query = "SELECT * FROM webnhansu.maintable WHERE `TrangThai`='Active'";
            List<MainTable> bangdiems = DataproviderBangChinh.Instance.ExecuteQuery(query);


            using (var workbook = new XLWorkbook())
            {  // nhét hết vào workbook
                var worksheet = workbook.Worksheets.Add("Students");
                var currentRow = 1;
                #region Header
                worksheet.Cell(currentRow, 1).Value = "LabID";
                worksheet.Cell(currentRow, 2).Value = "Tên";
                worksheet.Cell(currentRow, 3).Value = "Thế hệ";
                worksheet.Cell(currentRow, 4).Value = "SDT";
                worksheet.Cell(currentRow, 5).Value = "Ngành học";
                worksheet.Cell(currentRow, 6).Value = "Trường học";
                worksheet.Cell(currentRow, 7).Value = "email";
                worksheet.Cell(currentRow, 8).Value = "Quê quán ";
                worksheet.Cell(currentRow, 9).Value = "Trạng thái hoạt động ";
                #endregion
                #region Body
                foreach (var student1 in bangdiems)
                {
                    currentRow++;
                    // chèn câu lệnh tìm chứng chỉ vào đây 
                    worksheet.Cell(currentRow, 1).Value = student1.LabID;
                    worksheet.Cell(currentRow, 2).Value = student1.Ten;
                    worksheet.Cell(currentRow, 3).Value = student1.TheHe;
                    worksheet.Cell(currentRow, 4).Value = student1.SDT;
                    worksheet.Cell(currentRow, 5).Value = student1.NganhHoc;
                    worksheet.Cell(currentRow, 6).Value = student1.TruongHoc;
                    worksheet.Cell(currentRow, 7).Value = student1.email;
                    worksheet.Cell(currentRow, 8).Value = student1.QueQuan;
                    worksheet.Cell(currentRow, 9).Value = student1.TrangThai;
                }
                #endregion
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-oficedocument.spreadsheetml.sheet",
                        "Danhsachthanhvienhoatdong.xlsx"
                        );
                }
            }
        }





        // Xuất ra file excel danh sách theo đặc điểm 
        [HttpGet]
        public IActionResult XuatDanhSachTheoDacDiemBangChinh()  // dùng get hiển thị thông tin cũ 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult XuatDanhSachTheoDacDiemBangChinh(string TheHe, string TrangThai)
        {
            // thiết kế các trường hợp cho query 
            // thiết kế chuỗi luôn đúng để luôn dùng thằng and 
            // không điền gì thì ra tất 

            string b = "";
            string c = "";
            if (TheHe != null)
            {
                b = "AND `TheHe`='" + TheHe + "'";
            }

            if (TrangThai != null)
            {
                c = "AND `TrangThai`= '" + TrangThai + "'";
            }


            string a = "SELECT* FROM webnhansu.maintable WHERE `LabID`!=0  " + b + " " + c + "";
            List<MainTable> bangdiems = DataproviderBangChinh.Instance.ExecuteQuery(a);

            // xuất ra excel thôi 
            using (var workbook = new XLWorkbook())
            {  // nhét hết vào workbook
                var worksheet = workbook.Worksheets.Add("Students");
                var currentRow = 1;
                #region Header
                worksheet.Cell(currentRow, 1).Value = "LabID";
                worksheet.Cell(currentRow, 2).Value = "Tên";
                worksheet.Cell(currentRow, 3).Value = "Thế hệ";
                worksheet.Cell(currentRow, 4).Value = "SDT";
                worksheet.Cell(currentRow, 5).Value = "Ngành học";
                worksheet.Cell(currentRow, 6).Value = "Trường học";
                worksheet.Cell(currentRow, 7).Value = "email";
                worksheet.Cell(currentRow, 8).Value = "Quê quán ";
                worksheet.Cell(currentRow, 9).Value = "Trạng thái hoạt động ";
                #endregion
                #region Body
                foreach (var student1 in bangdiems)
                {
                    currentRow++;
                    // chèn câu lệnh tìm chứng chỉ vào đây 
                    worksheet.Cell(currentRow, 1).Value = student1.LabID;
                    worksheet.Cell(currentRow, 2).Value = student1.Ten;
                    worksheet.Cell(currentRow, 3).Value = student1.TheHe;
                    worksheet.Cell(currentRow, 4).Value = student1.SDT;
                    worksheet.Cell(currentRow, 5).Value = student1.NganhHoc;
                    worksheet.Cell(currentRow, 6).Value = student1.TruongHoc;
                    worksheet.Cell(currentRow, 7).Value = student1.email;
                    worksheet.Cell(currentRow, 8).Value = student1.QueQuan;
                    worksheet.Cell(currentRow, 9).Value = student1.TrangThai;
                }






                #endregion
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-oficedocument.spreadsheetml.sheet",
                        "Danhsachthanhvientheodacdiem.xlsx"
                        );
                }
            }
        }



        // edit bằng link 
        [HttpGet]
        // đóng vai trò đưa lên dũ liệu đã lưu 
        public IActionResult EditBangChinhlink()
        {   // dùng link để lấy ID 
            /// Lấy ID đang chỉnh sửa => lấy bangừ link 
            var reqUrl = Request.HttpContext.Request;
            var urlPath = reqUrl.Path; /// Path: Home/EditLT/ID
            var CurrentID = urlPath.ToString().Split('/').Last(); /// Lấy LabID hiện tại thông qua tên miền
            var bangchinh = DataproviderBangChinh.Instance.ExecuteQuery($"SELECT * FROM webnhansu.maintable where LabID = '{CurrentID}'"); /// Hiển thị người đang chỉnh sửa hiện tại



            // phân quyền 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            List<TKDangNhap> TkDangNhaps1 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyennhansu`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps1.Count > 0)
            { return View(bangchinh); }
            else
            {
                return RedirectToAction("Index", "TkSuper");// trả về trang chủ 
            }

            
        }

        [HttpPost]
        // Lab ID này lấy ở đợt truyền vào dữ liệu lần đầu 
        public IActionResult EditBangChinhlink(String LabID, String Ten, String TheHe, String SDT, String NganhHoc, String TruongHoc, String email, String QueQuan, String TrangThai)
        {
            /// Lấy LabID đang chỉnh sửa
            var reqUrl = Request.HttpContext.Request;
            var urlPath = reqUrl.Path;
            var CurrentID = urlPath.ToString().Split('/').Last();
            ///

            DataproviderBangChinh.Instance.ExecuteQuery($"update webnhansu.maintable " +
                                $"set Ten = '{Ten}',TheHe= '{TheHe}',SDT= '{SDT}',NganhHoc= '{NganhHoc}',TruongHoc= '{TruongHoc}',email= '{email}',QueQuan='{QueQuan}',TrangThai= '{TrangThai}' " +
                                $"where LabID = {LabID} ");  // udate dữ liệu 

            // lưu lại dữ liệu về phiên làm việc 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            var chinhsua = DataproviderSaveDataLogin.Instance.ExecuteQuery("INSERT INTO webnhansu.savedataedit VALUES("+tkdangnhap.LabID+",'"+ DateTime.Now.ToString()+ "','"+"Chinh sua thong tin bảng chính cửa LabID="+LabID+"')");
            return EditBangChinhlink();   // gọi vòng lại hàm 
        }





        // xóa hình ảnh 
        [HttpGet]
        public IActionResult DeleteImage()
        {
            return View();
        }
        [HttpPost]   // đọi một thời gian mới xóa 
        public IActionResult DeleteImage([FromServices] IWebHostEnvironment hostingEnvironment)
        {
            var reqUrl = Request.HttpContext.Request;
            var urlPath = reqUrl.Path; /// Path: Home/EditLT/ID
            var CurrentID = urlPath.ToString().Split('/').Last(); /// Lấy LabID hiện tại thông qua tên miền

            
            var members = DataproviderBangChinh.Instance.ExecuteQuery($"select * from webnhansu.maintable where LabID = {int.Parse(CurrentID)}");
            if (members[0].Image != null)  // xóa ảnh thôi link vẫn để thế để hiển thị khi ảnh nạp vào 
            {
                string imgdel = $"{hostingEnvironment.WebRootPath}{members[0].Image}";
                imgdel = Path.Combine(_iweb.WebRootPath, "files", imgdel);
                FileInfo fi = new FileInfo(imgdel);
                if (fi != null)
                {
                    System.IO.File.Delete(imgdel);
                    fi.Delete();
                }
            }
            return RedirectToAction("Index", "QuanLyBangChinh");
        }
    }
}
