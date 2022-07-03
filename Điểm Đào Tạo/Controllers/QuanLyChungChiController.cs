using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Điểm_Đào_Tạo.Models;
using Điểm_Đào_Tạo.DAO;
using ClosedXML.Excel;
using ExcelDataReader;
using System.IO;
using Newtonsoft.Json;

namespace Điểm_Đào_Tạo.Controllers
{
    public class QuanLyChungChiController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index(List<ChungChi> ChungChi= null)
        {
            // LT này là dữ liệu lấy từ file excel
            ChungChi = ChungChi == null ? new List<ChungChi>() : ChungChi;

            foreach (var chungchi in ChungChi)
            {
                // Tìm kiếm xem chungchi.ID có không 
                List<ChungChi> query_ChungChi = DataproviderChungChi.Instance.ExecuteQuery($"select * from webnhansu.chungchi where ID = {chungchi.ID} ");

                // Không có thì sẽ tạo và ngược lại thì Update
                if (query_ChungChi.Count == 0)
                    DataproviderChungChi.Instance.ExecuteQuery($"insert webnhansu.chungchi (ID,LabID,Ten,TenChungChi) " +
                        $"values({chungchi.ID},{chungchi.LabID},'{chungchi.Ten}','{chungchi.Tenchungchi}') ");
                else
                    DataproviderChungChi.Instance.ExecuteQuery($"update webnhansu.ChungChi " +
                                $"set LabID = {chungchi.LabID},Ten = '{chungchi.Ten}',TenChungChi = '{chungchi.Tenchungchi}' " +
                                $"where ID = {chungchi.ID} ");

            }
            // lưu lại phiên làm việc 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            var chinhsua = DataproviderSaveDataLogin.Instance.ExecuteQuery("INSERT INTO webnhansu.savedataedit VALUES(" + tkdangnhap.LabID + ",'" + DateTime.Now.ToString() + "','Thêm dữ liệu bằng excel vào bảng chứng chỉ')");




            // trang index để show 
            // Trả về View

            List<ChungChi> display_Info = DataproviderChungChi.Instance.ExecuteQuery("select * from webnhansu.chungchi");

            return View(display_Info);
        }

        [HttpPost]
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
            // phan quyền 
            List<ChungChi> a = new List<ChungChi>();
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            List<TKDangNhap> TkDangNhaps100 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendaotao`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            
            
            if (TkDangNhaps100.Count > 0)
            { a = this.GetChungChiList(fileName); }

            // vẫn up load file nhưng dữ liệu chưa chắc đã vào được 
            // Trả về dữ liệu
            return Index(a);
        }

        private List<ChungChi> GetChungChiList(string fileName)
        {
            List<ChungChi> ChungChi = new List<ChungChi>();
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
                        ChungChi chungchi = new ChungChi();

                         // thêm tính cả ID trong file excel 
                        chungchi.ID = Convert.ToInt32(render.GetValue(0));

                        // Kiểm tra điều kiện nếu trường rỗng thì gán mặc định là 0 tránh gây lỗi
                        try
                        {
                            chungchi.LabID = Convert.ToInt32(render.GetValue(1));
                        }
                        catch
                        {
                            chungchi.LabID = 0;
                        }

                        try
                        {
                            chungchi.Ten = render.GetValue(2).ToString();
                        }
                        catch
                        {
                            chungchi.Ten = "";
                        }

                        try
                        {
                            chungchi.Tenchungchi = render.GetValue(3).ToString();
                        }
                        catch
                        {
                            chungchi.Tenchungchi = "";
                        }
                       
                        ChungChi.Add(chungchi);

                    }
                }
            }
            return ChungChi;

        }
        [HttpPost]
        public IActionResult ExportToExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Chứng Chỉ");
                var currentRow = 1;
                // trỏ đến dòng 1 và cột 1 thay giá trị bằng LabID các dòng dưới cx tương tự
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "LabId";
                worksheet.Cell(currentRow, 3).Value = "Họ và Tên";
                worksheet.Cell(currentRow, 4).Value = "Chứng chỉ";


                // Lấy tất cả dữ liệu trong database theo thứ tự tăng dần labID
                List<ChungChi> LT = DataproviderChungChi.Instance.ExecuteQuery("select * from webnhansu.chungchi order by ID ASC");
                foreach (var lt in LT)
                {
                    // Dòng thứ 2 trở đi sẽ đổ dữ liệu từ database vào
                    currentRow += 1;
                    //dòng 2 cột 1 điền lt.ID
                    worksheet.Cell(currentRow, 1).Value = lt.ID;
                    worksheet.Cell(currentRow, 2).Value = lt.LabID;
                    worksheet.Cell(currentRow, 3).Value = lt.Ten;
                    worksheet.Cell(currentRow, 4).Value = lt.Tenchungchi;
                }
                // Trả về dữ liệu dạng xlsx
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ChungChi_Info.xlsx");
                }


            }
        }

        public IActionResult ChungChi()
        {
            string query = "SELECT * FROM webnhansu.ChungChi";
            List<ChungChi> ChungChi = DataproviderChungChi.Instance.ExecuteQuery(query);
            return View(ChungChi);
        }

        [HttpGet]
        public IActionResult FindChungChi(String Attribute = null, String varr = null)
        {
            Attribute = Attribute; /// Trường dữ liệu tìm kiếm
            varr = varr; /// Thông tin cần tìm kiếm trong trường dữ liệu đó
            List<ChungChi> display_Info = null;
            if (Attribute != null)
            {
                display_Info = DataproviderChungChi.Instance.ExecuteQuery($"select * from webnhansu.chungchi where {Attribute} = '{varr}'");
            }

            return View(display_Info);
        }

        [HttpGet]
        public IActionResult AddChungChi()
        {
            List<ChungChi> data_info = DataproviderChungChi.Instance.ExecuteQuery($"SELECT * FROM webnhansu.chungchi");
            List<int> ListID = new List<int>();
            foreach (var chungchi in data_info)
            {
                ListID.Add(chungchi.ID);
            }
            int CurrentID = 1;
            if (ListID != null)
            {
                CurrentID = (ListID.Max()); // Lấy ID lớn nhất
            }
            var ChungChi = DataproviderChungChi.Instance.ExecuteQuery($"SELECT * FROM webnhansu.chungchi where ID = '{CurrentID}'");

            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            List<TKDangNhap> TkDangNhaps100 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendaotao`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps100.Count > 0)
            { return View(ChungChi); }
            else
            {
                return RedirectToAction("Index", "TkSuper");// trả về trang chủ 
            }

           
        }

        [HttpPost]
        public IActionResult AddChungChi(int LabID, String Ten, String TenChungChi)
        {
            List<ChungChi> data_info = DataproviderChungChi.Instance.ExecuteQuery($"SELECT * FROM webnhansu.chungchi");
            List<int> ListID = new List<int>();
            foreach (var chungchi in data_info)
            {
                ListID.Add(chungchi.ID);
            }
            int CurrentID = 1;
            if (ListID != null)
            {
                CurrentID = (ListID.Max() + 1); // Lấy ID lớn nhất và add vào ID + 1
            }
            if (LabID != 0)
            {
                DataproviderChungChi.Instance.ExecuteQuery("SET SQL_SAFE_UPDATES = 0");
                DataproviderChungChi.Instance.ExecuteQuery($"insert webnhansu.chungchi values ({CurrentID.ToString()},{LabID},'{Ten}','{TenChungChi}')"); // Cập nhật dữ liệu mới
            }
            // lưu lại phiên làm việc 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            var chinhsua = DataproviderSaveDataLogin.Instance.ExecuteQuery("INSERT INTO webnhansu.savedataedit VALUES(" + tkdangnhap.LabID + ",'" + DateTime.Now.ToString() + "','thêm ID="+ CurrentID.ToString() + " vào bảng chứng chỉ')");

            return AddChungChi();
        }

        //Home/EditChungChi/LabID
        [HttpGet]
        public IActionResult EditChungChi()
        {
            /// Lấy ID đang chỉnh sửa
            var reqUrl = Request.HttpContext.Request;
            var urlPath = reqUrl.Path; /// Path: Home/EditLT/ID
            var CurrentID = urlPath.ToString().Split('/').Last(); /// Lấy LabID hiện tại thông qua tên miền
            var ChungChi = DataproviderChungChi.Instance.ExecuteQuery($"SELECT * FROM webnhansu.chungchi where ID = '{CurrentID}'"); /// Hiển thị người đang chỉnh sửa hiện tại

           // phân quyền 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            List<TKDangNhap> TkDangNhaps100 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendaotao`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps100.Count > 0)
            { return View(ChungChi); }
            else
            {
                return RedirectToAction("Index", "TkSuper");// trả về trang chủ 
            }
        }

        [HttpPost]
        public IActionResult EditChungChi(String ID, String LabID, String Ten, String TenChungChi)
        {
            /// Lấy LabID đang chỉnh sửa
            var reqUrl = Request.HttpContext.Request;
            var urlPath = reqUrl.Path;
            var CurrentID = urlPath.ToString().Split('/').Last();
            ///

            DataproviderChungChi.Instance.ExecuteQuery($"update webnhansu.ChungChi " +
                                $"set LabID = {LabID},Ten = '{Ten}',TenChungChi = '{TenChungChi}' " +
                                $"where ID = {ID} ");
            // lưu lại phiên làm việc 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            var chinhsua = DataproviderSaveDataLogin.Instance.ExecuteQuery("INSERT INTO webnhansu.savedataedit VALUES(" + tkdangnhap.LabID + ",'" + DateTime.Now.ToString() + "','chỉnh sửa  ID=" + CurrentID.ToString() + "  bảng chứng chỉ')");

            return EditChungChi();
        }



        public IActionResult DeleteChungChi()
        {

            var reqUrl = Request.HttpContext.Request;
            var urlPath = reqUrl.Path;
            var CurrentID = urlPath.ToString().Split('/').Last();


            // phân quyền 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            List<TKDangNhap> TkDangNhaps100 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendaotao`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps100.Count > 0)
            {
                DataproviderChungChi.Instance.ExecuteQuery($"delete from webnhansu.chungchi where ID = '{CurrentID}'");


                // lưu lại phiên làm việc 
                var tkdangnhap2 = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
                var chinhsua = DataproviderSaveDataLogin.Instance.ExecuteQuery("INSERT INTO webnhansu.savedataedit VALUES(" + tkdangnhap2.LabID + ",'" + DateTime.Now.ToString() + "','xóa  ID=" + CurrentID.ToString() + "  bảng chứng chỉ')");

                return View(); }
            else
            {
                return RedirectToAction("Index", "TkSuper");// trả về trang chủ 
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

       
    }
}
