using ClosedXML.Excel;
using Điểm_Đào_Tạo.DAO;
using Điểm_Đào_Tạo.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Điểm_Đào_Tạo.Controllers
{
    public class QuanLyLTController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }



        public IActionResult LT()
        {
            string query = "SELECT * FROM webnhansu.LT";
            List<LT> lt = DataproviderLT.Instance.ExecuteQuery(query);
            return View(lt);
        }

        // Index dùng để show dữ liệu up lên 
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index(List<LT> LT = null)
        {
            // LT này là dữ liệu lấy từ file excel
            LT = LT == null ? new List<LT>() : LT;

            foreach (var lt in LT)
            {
                // Tìm kiếm xem lt.ID có không 
                List<LT> query_LT = DataproviderLT.Instance.ExecuteQuery($"select * from webnhansu.LT where ID = {lt.ID} ");

                // Không có thì sẽ tạo và ngược lại thì Update
                if (query_LT.Count == 0)
                    DataproviderLT.Instance.ExecuteQuery($"insert webnhansu.LT (ID,LabId,Ten,ChucVu,BatDau,KetThuc,DanhGia) " +
                        $"values({lt.ID},{lt.LabID},'{lt.Ten}','{lt.ChucVu}','{lt.BatDau}','{lt.KetThuc}','{lt.DanhGia}') ");
                else
                    DataproviderLT.Instance.ExecuteQuery($"update webnhansu.LT " +
                                $"set LabID = {lt.LabID},Ten = '{lt.Ten}',ChucVu = '{lt.ChucVu}',BatDau = '{lt.BatDau}',KetThuc = '{lt.KetThuc}',DanhGia = '{lt.DanhGia}' " +
                                $"where ID = {lt.ID} ");
                // lưu lại phiên làm việc 
                var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
                var chinhsua = DataproviderSaveDataLogin.Instance.ExecuteQuery("INSERT INTO webnhansu.savedataedit VALUES(" + tkdangnhap.LabID + ",'" + DateTime.Now.ToString() + "','Thêm dữ liệu bảng LT bằng file excel')");


            }


            // Trả về View

            List<LT> display_LT_Info = DataproviderLT.Instance.ExecuteQuery("select * from webnhansu.LT");

            return View(display_LT_Info);
        }
        [HttpPost]
        // đọc  file up lên 
        public IActionResult Index(IFormFile file, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            // Tạo đường dẫn
            string fileName = $"{hostingEnvironment.WebRootPath}/files/{file.FileName}";
            while (System.IO.File.Exists(fileName))
            {
                fileName = fileName.Substring(0, fileName.Length - 5); // Bỏ đi đuôi xlsx
                Random rand = new Random();
                int number2 = rand.Next(1, 100000);
                fileName += number2; // Chèn số vào
                fileName += ".xlsx"; // Chèn đuôi vào

            }

            // Dẩy file vào thư mục
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            // Gọi đến hàm đọc file gửi thằng đường dẫn file ta vừa lưu vào để đọc luôn
            // phân quyền 
            List<LT> a = new List<LT>();
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            List<TKDangNhap> TkDangNhaps100 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyenchutich`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            // điều kiện thỏa mãn 
            if (TkDangNhaps100.Count > 0)
            { a = this.GetLTList(fileName);
                return Index(a);
            }
            else
            {
                return RedirectToAction("Index", "TkSuper");// trả về trang chủ 
            }
            // vẫn up load file nhưng dữ liệu chưa chắc đã vào được 
            // Trả về dữ liệu
            
        }

        private List<LT> GetLTList(string fileName)
        {
            List<LT> LT = new List<LT>();
            // Lấy ra file mà muôn đọc
            // fName là tên file được gửi vào
            // file sẽ được lưu trong thư mục wwwroot/files/...  
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                // Đọc dữ liệu từ file
                using (var render = ExcelReaderFactory.CreateReader(stream))
                {
                    render.Read();
                    while (render.Read())
                    {
                        LT lt = new LT();


                        lt.ID = Convert.ToInt32(render.GetValue(0));

                        // Kiểm tra điều kiện nếu trường rỗng thì gán mặc định là 0 tránh gây lỗi
                        try
                        {
                            lt.LabID = Convert.ToInt32(render.GetValue(1));
                        }
                        catch
                        {
                            lt.LabID = 0;
                        }

                        try
                        {
                            lt.Ten = render.GetValue(2).ToString();
                        }
                        catch
                        {
                            lt.Ten = "";
                        }

                        try
                        {
                            lt.ChucVu = render.GetValue(3).ToString();
                        }
                        catch
                        {
                            lt.ChucVu = "";
                        }
                        try
                        {
                            lt.BatDau = render.GetValue(4).ToString().Substring(0, 10);
                        }
                        catch
                        {
                            lt.BatDau = "";
                        }
                        try
                        {
                            lt.KetThuc = render.GetValue(5).ToString().Substring(0, 10);
                        }
                        catch
                        {
                            lt.KetThuc = "";
                        }

                        try
                        {
                            lt.DanhGia = render.GetValue(6).ToString();
                        }
                        catch
                        {
                            lt.DanhGia = "";
                        }

                        LT.Add(lt);

                    }
                }
            }
            return LT;

        }
        [HttpPost]
        public IActionResult ExportToExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("LT");
                var currentRow = 1;
                // trỏ đến dòng 1 và cột 1 thay giá trị bằng LabID các dòng dưới cx tương tự
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "LabId";
                worksheet.Cell(currentRow, 3).Value = "Họ và Tên";
                worksheet.Cell(currentRow, 4).Value = "Chức Vụ";
                worksheet.Cell(currentRow, 5).Value = "Bắt đầu";
                worksheet.Cell(currentRow, 6).Value = "Kết thúc";
                worksheet.Cell(currentRow, 7).Value = "Đánh giá";

                // Lấy tất cả dữ liệu trong database theo thứ tự tăng dần labID
                List<LT> LT = DataproviderLT.Instance.ExecuteQuery("select * from webnhansu.LT order by ID ASC");
                foreach (var lt in LT)
                {
                    // Dòng thứ 2 trở đi sẽ đổ dữ liệu từ database vào
                    currentRow += 1;
                    //dòng 2 cột 1 điền lt.ID
                    worksheet.Cell(currentRow, 1).Value = lt.ID;
                    worksheet.Cell(currentRow, 2).Value = lt.LabID;
                    worksheet.Cell(currentRow, 3).Value = lt.Ten;
                    worksheet.Cell(currentRow, 4).Value = lt.ChucVu;
                    worksheet.Cell(currentRow, 5).Value = lt.BatDau;
                    worksheet.Cell(currentRow, 6).Value = lt.KetThuc;
                    worksheet.Cell(currentRow, 7).Value = lt.DanhGia;
                }
                // Trả về dữ liệu dạng xlsx
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LT_Info.xlsx");
                }


            }
        }

        [HttpGet]
        public IActionResult Find(String Attribute = null, String varr = null)
        { 

            // để cái attribute sẵn trong ô text 
            Attribute = Attribute; /// Trường dữ liệu tìm kiếm
            varr = varr; /// Thông tin cần tìm kiếm trong trường dữ liệu đó
            List<LT> display_LT_Info = null;
            if (Attribute != null)
            {
                display_LT_Info = DataproviderLT.Instance.ExecuteQuery($"select * from webnhansu.lt where {Attribute} = '{varr}'");
            }

            return View(display_LT_Info);
        }



        //Home/EditLT/LabID
        [HttpGet]
        public IActionResult EditLT()
        {
            /// Lấy ID đang chỉnh sửa
            var reqUrl = Request.HttpContext.Request;
            var urlPath = reqUrl.Path; /// Path: Home/EditLT/ID
            var CurrentID = urlPath.ToString().Split('/').Last(); /// Lấy LabID hiện tại thông qua tên miền
            var lt = DataproviderLT.Instance.ExecuteQuery($"SELECT * FROM webnhansu.LT where ID = '{CurrentID}'"); /// Hiển thị người đang chỉnh sửa hiện tại

            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            List<TKDangNhap> TkDangNhaps100 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyenchutich`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            // điều kiện thỏa mãn 
            if (TkDangNhaps100.Count > 0)
            { return View(lt); }

            else
            {
                return RedirectToAction("Index", "TkSuper");// trả về trang chủ 
            }
        }

        [HttpPost]
        public IActionResult EditLT(String ID, String LabID, String Ten, String ChucVu, String BatDau, String KetThuc, String DanhGia = "Khong")
        {
            /// Lấy LabID đang chỉnh sửa
            var reqUrl = Request.HttpContext.Request;
            var urlPath = reqUrl.Path;
            var CurrentID = urlPath.ToString().Split('/').Last();
            ///

            DataproviderLT.Instance.ExecuteQuery($"delete from webnhansu.lt where ID = '{CurrentID}'"); /// Xóa dữ liệu hiện có

            DataproviderLT.Instance.ExecuteQuery("SET SQL_SAFE_UPDATES = 0");
            DataproviderLT.Instance.ExecuteQuery($"insert into lt values ('{ID}','{LabID}','{Ten}','{ChucVu}','{BatDau}','{KetThuc}','{DanhGia}')"); // Cập nhật dữ liệu mới

            // lưu lại phiên làm việc 
            // lưu lại phiên làm việc 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            var chinhsua = DataproviderSaveDataLogin.Instance.ExecuteQuery("INSERT INTO webnhansu.savedataedit VALUES(" + tkdangnhap.LabID + ",'" + DateTime.Now.ToString() + "','chỉnh sửa ID=" + CurrentID.ToString() + "  bảng LT ')");

            return EditLT();
        }

        public IActionResult DeleteLT()
        {

            var reqUrl = Request.HttpContext.Request;
            var urlPath = reqUrl.Path;
            var CurrentID = urlPath.ToString().Split('/').Last();


            // phân quyền 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            List<TKDangNhap> TkDangNhaps100 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyenchutich`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            // điều kiện thỏa mãn 
            if (TkDangNhaps100.Count > 0)
            {
                DataproviderLT.Instance.ExecuteQuery($"delete from webnhansu.lt where ID = '{CurrentID}'");
                // lưu lại phiên làm việc 
                var tkdangnhap2 = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
                var chinhsua = DataproviderSaveDataLogin.Instance.ExecuteQuery("INSERT INTO webnhansu.savedataedit VALUES(" + tkdangnhap2.LabID + ",'" + DateTime.Now.ToString() + "','xóa ID=" + CurrentID.ToString() + " vào bảng LT')");

                return View();
            }

            else
            {
                return RedirectToAction("Index", "TkSuper");// trả về trang chủ 
            }
           

        }

       

    }
}