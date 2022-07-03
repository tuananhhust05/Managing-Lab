using ClosedXML.Excel;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Điểm_Đào_Tạo.DAO;
using Điểm_Đào_Tạo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Điểm_Đào_Tạon.Controllers
{   
    // làm tất cả ở home ta có thể tạo những biến toàn cục 
    public class QuanLyDuAnController : Controller
    {
       
        // show trang chủ 
        public IActionResult Index()
        {
            string query = "SELECT * FROM webnhansu.`chitietduan`";
            List<ChiTietDuAn> bangdiems = DataproviderChiTietDuAn.Instance.ExecuteQuery(query);
            //  List<ChiTietDuAn> bangdiems1 = DataproviderChiTietDuAn.Instance.ExecuteQuery("DELETE FROM webnhansu.`chitietduan`WHERE `LabID`='a' ");

           

            return View(bangdiems);
           
        }


        // hiênt thị danh sách dự án hiện có 
        public IActionResult hienthidanhsachduan()
        {
            string query = "SELECT * FROM webnhansu.`ds du an`";
            List<duan> bangdiems = DataproviderDuan.Instance.ExecuteQuery(query);
            return View(bangdiems);
        }


        // Thêm dự án bằng ex 
        [HttpGet]
        public IActionResult themduanex()
        {
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            List<TKDangNhap> TkDangNhaps100 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendaotao`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps100.Count > 0)
            {
                
                return View();
            }
            else
            {
                return RedirectToAction("Index", "TkSuper");// trả về trang chủ 
            }
        }
        [HttpPost]
        [System.Obsolete]
        public IActionResult themduanex(IFormFile file, [FromServices] IHostingEnvironment hostingEnviroment)
        {
            string fileName = $"{hostingEnviroment.WebRootPath}\\files\\{file.FileName}";  // địa chỉ để tải file lên => sau đó đọc
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            // lấy số dòng trong bảng để điền ID 
            List<ChiTietDuAn> result11 = DataproviderChiTietDuAn.Instance.ExecuteQuery("SELECT * FROM webnhansu.chitietduan");
            int a = result11.Count; // truyền vào hàm 


            var bangdiems = this.GetStudentList(file.FileName, a);
            string query2 = "INSERT INTO `webnhansu`.`ds du an` VALUES ('" + bangdiems[0].Tenduan + "','1'); ";// tên dự án làm Key luôn => thêm cả 2 luôn 

            // sau nên không sợ trùng 
            List<duan> result2 = DataproviderDuan.Instance.ExecuteQuery(query2); // thêm nhưng chưa tính trùng => Không sao 
                                                                                 //List<ChiTietDuAn> result2 = DataproviderChiTietDuAn.Instance.ExecuteQuery("DELETE FROM `webnhansu`.`ds du an` WHERE `ten du an`='a'");

            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            var chinhsua = DataproviderSaveDataLogin.Instance.ExecuteQuery("INSERT INTO webnhansu.savedataedit VALUES(" + tkdangnhap.LabID + ",'" + DateTime.Now.ToString() + "','Thêm dự án :" + bangdiems[0].Tenduan + "')");

            return View(bangdiems);  // truyền vào danh sách những đối tượng lấy từ file excel 
        }
        private List<ChiTietDuAn> GetStudentList(string fName, int a) // hàm phụ 
        {
            List<ChiTietDuAn> bangdiems = new List<ChiTietDuAn>();
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {

                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {

                    while (reader.Read())
                    {

                        bangdiems.Add(new ChiTietDuAn()// bangdiems đang chứa tất cả dữ liệu từ excel 
                        {

                            LabID = int.Parse(reader.GetValue(0).ToString()),
                            Ten = reader.GetValue(1).ToString(),
                            Tenduan = reader.GetValue(2).ToString(),
                            Chucvu = reader.GetValue(3).ToString(),
                            BatDau = reader.GetValue(4).ToString(),
                            KetThuc = reader.GetValue(5).ToString(),
                            Danhgia = reader.GetValue(6).ToString(),
                        });

                    }

                }
            }
            for (int i = 1; i <= bangdiems.Count; i++)
            {
                bangdiems[i - 1].ID = a + i;
            }
            foreach (var item in bangdiems)  // gồm những phần được cộng vào 
            {
                // cẩn thận không thiếu 
                string query1 = "INSERT INTO webnhansu.chitietduan VALUES (" + item.ID + "," + item.LabID + ",'" + item.Ten + "', '" + item.Tenduan + "', '" + item.Chucvu + "','" + item.BatDau + "','" + item.KetThuc + "', '" + item.Danhgia + "'); ";
                List<ChiTietDuAn> result1 = DataproviderChiTietDuAn.Instance.ExecuteQuery(query1);

            }
            return bangdiems;   // trả về 1 danh sách gồm các phần đã được cập nhật điểm 
        }





        // xóa dự án 





        [HttpGet]
        public IActionResult xoaduan()
        {
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            List<TKDangNhap> TkDangNhaps100 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendaotao`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps100.Count > 0)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Index", "TkSuper");// trả về trang chủ 
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult xoaduan(string IP)
        {

            string query = "DELETE FROM webnhansu.`chitietduan`WHERE `Ten du an`='" + IP + "'";
            List<ChiTietDuAn> bangdiems = DataproviderChiTietDuAn.Instance.ExecuteQuery(query);
            List<ChiTietDuAn> bangdiems2 = DataproviderChiTietDuAn.Instance.ExecuteQuery("DELETE FROM webnhansu.`ds du an`WHERE `ten du an`='" + IP + "'");  // xóa từ 2 nguồn 


            // lưu dữ liệu phiên làm việc 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            var chinhsua = DataproviderSaveDataLogin.Instance.ExecuteQuery("INSERT INTO webnhansu.savedataedit VALUES(" + tkdangnhap.LabID + ",'" + DateTime.Now.ToString() + "','Xóa dự án:" + IP+ "')");

            return View(bangdiems);
        }





        // tìm kiếm dự án theo Lab ID 

        [HttpGet]
        public IActionResult timkiemduanLabID()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult timkiemduanLabID(string IP)
        {

            string query = "SELECT * FROM webnhansu.`chitietduan` WHERE `LabID`='" + IP.ToString() + "'";
            List<ChiTietDuAn> bangdiems = DataproviderChiTietDuAn.Instance.ExecuteQuery(query);


            return View(bangdiems);
        }




        


        // Tìm kiếm bằng excel 
        // chú ý: Get StudentList Không được trùng tên 
        [HttpGet]
        public IActionResult timkiembangex()
        {
            return View();
        }
        [HttpPost]
        [System.Obsolete]
        public IActionResult timkiembangex(IFormFile file, [FromServices] IHostingEnvironment hostingEnviroment)
        {
            string fileName = $"{hostingEnviroment.WebRootPath}\\files\\{file.FileName}";  // địa chỉ để tải file lên => sau đó đọc
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            var bangdiems = this.GetStudentList1(file.FileName);


            // dùng sesion để truyền sang trang xuất file excel những đối tượng lấy từ file excel 
            SoluongSession n = new SoluongSession();
            n.soluong = bangdiems.Count;
            HttpContext.Session.SetString("SoluongSession", JsonConvert.SerializeObject(n));// truyền vào số lượng 
            for (int i = 0; i <= bangdiems.Count - 1; i++)   // dùng vòng for để tạo nhiều session 
            {
                HttpContext.Session.SetString("StudentSession" + i + "", JsonConvert.SerializeObject(bangdiems[i])); // để ý chỗ student Session
            }
            return View(bangdiems);  // truyền vào danh sách những đối tượng lấy từ file excel 
        }
        private List<ChiTietDuAn> GetStudentList1(string fName) // hàm phụ 
        {
            List<ChiTietDuAn> bangdiems = new List<ChiTietDuAn>();
            List<LabID> labid = new List<LabID>();  // lấy danh sách ID 
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        labid.Add(new LabID()// bangdiems đang chứa tất cả dữ liệu từ excel 
                        {
                            ID = reader.GetValue(0).ToString(),

                        });
                    }
                }
            }

            foreach (var item in labid)  // gồm những phần được cộng vào 
            {
                //string query1 = "INSERT INTO webnhansu.chitietduan VALUES ('" + item.LabID + "', '" + item.Tenduan + "', '" + item.Chucvu + "', '" + item.Danhgia + "'); ";
                // List<ChiTietDuAn> result1 = DataproviderChiTietDuAn.Instance.ExecuteQuery(query1);
                List<ChiTietDuAn> result2 = DataproviderChiTietDuAn.Instance.ExecuteQuery("SELECT * FROM webnhansu.chitietduan WHERE `LabID`='" + item.ID + "'");
                foreach (var item2 in result2)
                {
                    bangdiems.Add(item2);
                }
            }
            return bangdiems;   // trả về 1 danh sách gồm các phần đã được cập nhật điểm 
        }

        // xuất danh sách vừa tìm được ra excel 
        public IActionResult xuatketquatimraex() // dùng session lấy thông tin từ trang gốc cái dữ liệu đã tìm được từ file excel up lên 
        {
            List<ChiTietDuAn> bangdiems = new List<ChiTietDuAn>();
            // lấy ra số lượng session

            SoluongSession n = new SoluongSession();
            n = JsonConvert.DeserializeObject<SoluongSession>(HttpContext.Session.GetString("SoluongSession"));

            for (int i = 0; i <= n.soluong - 1; i++)
            {
                var student = JsonConvert.DeserializeObject<ChiTietDuAn>(HttpContext.Session.GetString("StudentSession" + i + ""));// lấy thong tin từ bên kia qua 
                bangdiems.Add(student);
            }// nhét 1 lít đối tượng vào session 



            using (var workbook = new XLWorkbook())
            {  // nhét hết vào workbook
                var worksheet = workbook.Worksheets.Add("Students");
                var currentRow = 1;
                #region Header
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "LabID";
                worksheet.Cell(currentRow, 3).Value = "Họ và tên";
                worksheet.Cell(currentRow, 4).Value = "Tên dự án";
                worksheet.Cell(currentRow, 5).Value = "Chức vụ";
                worksheet.Cell(currentRow, 6).Value = "Bắt đầu";
                worksheet.Cell(currentRow, 7).Value = "Kết thúc";
                worksheet.Cell(currentRow, 8).Value = "Đánh giá ";

                #endregion
                #region Body
                foreach (var student1 in bangdiems)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = student1.ID;
                    worksheet.Cell(currentRow, 2).Value = student1.LabID;
                    worksheet.Cell(currentRow, 3).Value = student1.Ten;
                    worksheet.Cell(currentRow, 4).Value = student1.Tenduan;
                    worksheet.Cell(currentRow, 5).Value = student1.Chucvu;
                    worksheet.Cell(currentRow, 6).Value = student1.BatDau;
                    worksheet.Cell(currentRow, 7).Value = student1.KetThuc;
                    worksheet.Cell(currentRow, 8).Value = student1.Danhgia;

                }
                #endregion
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-oficedocument.spreadsheetml.sheet",
                        "Danhsachthanhvienkemduan.xlsx"
                        );
                }
            }
        }




        // xuất danh sách thành viên theo dự án 
        //1. Chọn dự án 
        [HttpGet]
        public IActionResult hienthidanhsachthanhvientheoduan()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult hienthidanhsachthanhvientheoduan(string IP)
        {

            // string query = "DELETE FROM webnhansu.`chitietduan`WHERE `Ten du an`='" + IP + "'";
            // List<ChiTietDuAn> bangdiems = DataproviderChiTietDuAn.Instance.ExecuteQuery(query);
            // List<ChiTietDuAn> bangdiems2 = DataproviderChiTietDuAn.Instance.ExecuteQuery("DELETE FROM webnhansu.`ds du an`WHERE `ten du an`='" + IP + "'");  // xóa từ 2 nguồn 
            string query = "SELECT * FROM webnhansu.`chitietduan` WHERE `ten du an`='" + IP.ToString() + "'";
            List<ChiTietDuAn> bangdiems = DataproviderChiTietDuAn.Instance.ExecuteQuery(query);

            // chuyển đống bảng điểm lên session  
            if (bangdiems.Count > 0)
            {
                SoluongSession n = new SoluongSession();
                n.soluong = bangdiems.Count;
                HttpContext.Session.SetString("SoluongSession", JsonConvert.SerializeObject(n));// truyền vào số lượng 
                for (int i = 0; i <= bangdiems.Count - 1; i++)   // dùng vòng for để tạo nhiều session 
                {
                    HttpContext.Session.SetString("StudentSession" + i + "", JsonConvert.SerializeObject(bangdiems[i])); // để ý chỗ student Session
                }
            }
            return View(bangdiems);
        }


        // 2. Xuất danh sách dự án vừa chọn ra 
        public IActionResult xuatdanhsachthanhvientheoduanex()
        {
            // lấy từ session dữ liệu để chuẩn bị nhét vào file excel 
            List<ChiTietDuAn> bangdiems = new List<ChiTietDuAn>();
            // lấy ra số lượng session

            SoluongSession n = new SoluongSession();
            n = JsonConvert.DeserializeObject<SoluongSession>(HttpContext.Session.GetString("SoluongSession"));

            for (int i = 0; i <= n.soluong - 1; i++)
            {
                var student = JsonConvert.DeserializeObject<ChiTietDuAn>(HttpContext.Session.GetString("StudentSession" + i + ""));// lấy thong tin từ bên kia qua 
                bangdiems.Add(student);
            }



            using (var workbook = new XLWorkbook())
            {  // nhét hết vào workbook
                var worksheet = workbook.Worksheets.Add("Students");
                var currentRow = 1;
                #region Header
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "LabID";
                worksheet.Cell(currentRow, 3).Value = "Họ và tên";
                worksheet.Cell(currentRow, 4).Value = "Tên dự án";
                worksheet.Cell(currentRow, 5).Value = "Chức vụ";
                worksheet.Cell(currentRow, 6).Value = "Bắt đầu";
                worksheet.Cell(currentRow, 7).Value = "Kết thúc";
                worksheet.Cell(currentRow, 8).Value = "Đánh giá ";

                #endregion
                #region Body
                foreach (var student1 in bangdiems)
                {
                    currentRow++;
                    // chèn câu lệnh tìm chứng chỉ vào đây 
                    worksheet.Cell(currentRow, 1).Value = student1.ID;
                    worksheet.Cell(currentRow, 2).Value = student1.LabID;
                    worksheet.Cell(currentRow, 3).Value = student1.Ten;
                    worksheet.Cell(currentRow, 4).Value = student1.Tenduan;
                    worksheet.Cell(currentRow, 5).Value = student1.Chucvu;
                    worksheet.Cell(currentRow, 6).Value = student1.BatDau;
                    worksheet.Cell(currentRow, 7).Value = student1.KetThuc;
                    worksheet.Cell(currentRow, 8).Value = student1.Danhgia;

                }
                #endregion
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-oficedocument.spreadsheetml.sheet",
                        "Danhsachthanhvienkemduan.xlsx"
                        );
                }
            }
        }


        // -----------edit theo link 
        // edit bằng link 
        [HttpGet]
        // đóng vai trò đưa lên dũ liệu đã lưu 
        public IActionResult EditDuAnlink()
        {   // dùng link để lấy ID 
            /// Lấy ID đang chỉnh sửa => lấy bangừ link 
            var reqUrl = Request.HttpContext.Request;
            var urlPath = reqUrl.Path; /// Path: Home/EditLT/ID
            var CurrentID = urlPath.ToString().Split('/').Last(); /// Lấy LabID hiện tại thông qua tên miền
            var bangchinh = DataproviderChiTietDuAn.Instance.ExecuteQuery($"SELECT * FROM webnhansu.chitietduan where ID = {CurrentID}"); /// Hiển thị người đang chỉnh sửa hiện tại

            // phân quyền 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            List<TKDangNhap> TkDangNhaps100 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendaotao`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps100.Count > 0)
            {

                return View(bangchinh);
            }
            else
            {
                return RedirectToAction("Index", "TkSuper");// trả về trang chủ 
            }
           
        }

        [HttpPost]
        // Lab ID này lấy ở đợt truyền vào dữ liệu lần đầu 
        public IActionResult EditDuAnlink(String ID, String LabID, String Ten, String Tenduan, String ChucVu, String BatDau, String KetThuc, String Danhgia)
        {
            /// Lấy LabID đang chỉnh sửa
            var reqUrl = Request.HttpContext.Request;
            var urlPath = reqUrl.Path;
            var CurrentID = urlPath.ToString().Split('/').Last();
            ///
            // chú ý số, chữ 
            DataproviderChiTietDuAn.Instance.ExecuteQuery($"update webnhansu.chitietduan " +
                                $"set LabID = {LabID},Ten= '{Ten}',`Ten du an` = '{Tenduan}',ChucVu= '{ChucVu}',`BatDau`= '{BatDau.ToString()}',KetThuc= '{KetThuc.ToString()}',DanhGia = '{Danhgia}' " +
                                $"where ID = {ID} ");  // udate dữ liệu 

            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            var chinhsua = DataproviderSaveDataLogin.Instance.ExecuteQuery("INSERT INTO webnhansu.savedataedit VALUES(" + tkdangnhap.LabID + ",'" + DateTime.Now.ToString() + "','Chỉnh sửa ID="+ID+"')");

            return EditDuAnlink();   // gọi vòng lại hàm 
        }
    }
}
