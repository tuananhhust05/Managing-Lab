using Microsoft.AspNetCore.Mvc;
using Điểm_Đào_Tạo.DAO;
using Điểm_Đào_Tạo.Models;
using System.Collections.Generic;
using ClosedXML.Excel;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using ExcelDataReader;
using System;
using System.Linq;
using Newtonsoft.Json;

namespace Điểm_Đào_Tạo.Controllers
{
    public class DiemYThucController : Controller
    {
        public IActionResult Index()
        {
            string query = "SELECT * FROM webnhansu.diemythuc ";
            List<diemythuc> bangdiems = Dataproviderdiemythuc.Instance.ExecuteQuery(query);
            return View(bangdiems);
        }







        // xuất file excel điểm ý thức 
        public IActionResult xuatfileexdiemythuc()
        {
            string query = "SELECT * FROM webnhansu.diemythuc ";
            List<diemythuc> bangdiems = Dataproviderdiemythuc.Instance.ExecuteQuery(query);
            using (var workbook = new XLWorkbook())
            {  // nhét hết vào workbook
                var worksheet = workbook.Worksheets.Add("Students");
                var currentRow = 1;
                #region Header
                worksheet.Cell(currentRow, 1).Value = "LabID";
                worksheet.Cell(currentRow, 2).Value = "Họ và tên";
                worksheet.Cell(currentRow, 3).Value = "Điểm ý thức";


                #endregion
                #region Body
                foreach (var student1 in bangdiems)
                {
                    currentRow++;
                    // chèn câu lệnh tìm chứng chỉ vào đây 
                    worksheet.Cell(currentRow, 1).Value = student1.LabID;
                    worksheet.Cell(currentRow, 2).Value = student1.Ten;
                    worksheet.Cell(currentRow, 3).Value = student1.DiemYThuc;


                }
                #endregion
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-oficedocument.spreadsheetml.sheet",
                        "BangDiemYThuc.xlsx"
                        );
                }
            }
        }







        // update data to diemythuc by excel file 
        [HttpGet]
        // chặn ngay từ phần get đẩy view lên 
        public IActionResult capnhatdiemythucbangex()
        {
            // phân quyền 
            // lấy dữ liệu đăng nhập từ session 
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
        public IActionResult capnhatdiemythucbangex(IFormFile file, [FromServices] IHostingEnvironment hostingEnviroment)
        {
            string fileName = $"{hostingEnviroment.WebRootPath}\\files\\{file.FileName}";  // địa chỉ để tải file lên => sau đó đọc
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            var bangdiems = this.GetStudentList(file.FileName);

            // lưu dữ liệu phiên làm việc 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            var chinhsua = DataproviderSaveDataLogin.Instance.ExecuteQuery("INSERT INTO webnhansu.savedataedit VALUES(" + tkdangnhap.LabID + ",'" + DateTime.Now.ToString() + "','Cập nhật điểm ý thức bằng excel')");

            return View(bangdiems);  // truyền vào danh sách những đối tượng lấy từ file excel 
        }
        private List<diemythuc> GetStudentList(string fName) // hàm phụ 
        {
            List<diemythuc> bangdiems = new List<diemythuc>();  // lấy dữ liệu từ file excel 
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        bangdiems.Add(new diemythuc()// bangdiems đang chứa tất cả dữ liệu từ excel 
                        {
                            LabID = int.Parse(reader.GetValue(0).ToString()),
                            Ten = reader.GetValue(1).ToString(),
                            DiemYThuc = int.Parse(reader.GetValue(2).ToString()),

                        });
                    }
                }
            }

            foreach (var item in bangdiems)  // thao tác cộng điểm trong này luôn 
            {
                List<diemythuc> bangdiems1 = Dataproviderdiemythuc.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemythuc WHERE `LabID`=" + item.LabID + ""); // chọn , là 1 list 
                if (bangdiems1.Count > 0)  // phòng exception 
                {
                    List<diemythuc> bangdiems2 = Dataproviderdiemythuc.Instance.ExecuteQuery("DELETE FROM webnhansu.diemythuc WHERE `LabID`=" + item.LabID + "");// xóa thằng cũ 
                    int a = 0;
                    a = bangdiems1[0].DiemYThuc + item.DiemYThuc;
                    List<diemythuc> bangdiems3 = Dataproviderdiemythuc.Instance.ExecuteQuery("INSERT INTO  webnhansu.diemythuc VALUES(" + bangdiems1[0].LabID + ",'" + bangdiems1[0].Ten + "'," + a + ")");
                }

            };
            // trả về danh sách điểm sau khi được cộng 
            List<diemythuc> bangdiemcapnhat = new List<diemythuc>();
            foreach (var item in bangdiems)
            {
                List<diemythuc> bangdiems1 = Dataproviderdiemythuc.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemythuc WHERE `LabID`=" + item.LabID + "");
                foreach (var item1 in bangdiems1)
                {
                    bangdiemcapnhat.Add(item1);  // add từng thằng 
                }

            }

            return bangdiemcapnhat;   // trả về danh sách điểm sau khi được cộng, điểm cộng thì có ở file excel r 
        }





        // TIM KIEM 

        //UP
        public IActionResult timkiemdiemythuc()
        {
            return View();
        }
        // ID 
        public IActionResult timkiemdiemythucID()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult timkiemdiemythucID(string ID)
        {

            string query = "SELECT * FROM webnhansu.diemythuc WHERE `LabID`=" + ID + "";
            List<diemythuc> TkDangNhaps = Dataproviderdiemythuc.Instance.ExecuteQuery(query);

            return View(TkDangNhaps);
        }
        // Ten
        public IActionResult timkiemdiemythucTen()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult timkiemdiemythucTen(string ten)
        {

            string query = "SELECT * FROM webnhansu.diemythuc WHERE `Ten`='" + ten + "'";
            List<diemythuc> TkDangNhaps = Dataproviderdiemythuc.Instance.ExecuteQuery(query);

            return View(TkDangNhaps);
        }





      



        // Sorting Diem Y Thuc 
        public IActionResult sapxepdiemythuc()
        {
            string query = "SELECT * FROM webnhansu.diemythuc ";  // danh sách cuối cùng 
            List<diemythuc> final = new List<diemythuc>();// danh sách cuối cùng
            List<diemythuc> bangdiems = Dataproviderdiemythuc.Instance.ExecuteQuery(query);
            List<int> danhsachdiem = new List<int>();
            foreach (var item in bangdiems)
            {
                danhsachdiem.Add(item.DiemYThuc);
            };
            for (int i = 0; i < danhsachdiem.Count; i++)  // sắp xếp điểm từ lớn đến bé 
            {
                for (int j = i + 1; j < danhsachdiem.Count; j++)
                {
                    if (danhsachdiem[j] > danhsachdiem[i])
                    {
                        int temp = i;
                        i = j;
                        j = temp;
                    }
                }
            }
            foreach (var item in danhsachdiem)  // có danh sahs điểm r thì nạp vào 
            {
                List<diemythuc> bangdiems1 = Dataproviderdiemythuc.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemythuc WHERE `DiemYThuc`=" + item + " ");
                foreach (var item1 in bangdiems1)
                {
                    final.Add(item1);
                }
            }
            return View(final);
        }





        // Find Diem Y Thuc Theo Khoang 
        [HttpGet]
        public IActionResult timdiemythuctheokhoang()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult timdiemythuctheokhoang(string tren, string duoi)
        {

            string query = "SELECT * FROM webnhansu.diemythuc";
            List<diemythuc> TkDangNhaps = Dataproviderdiemythuc.Instance.ExecuteQuery(query);
            List<int> danhsachdiem = new List<int>();// khởi tạo 


            foreach (var item in TkDangNhaps)
            {
                if ((item.DiemYThuc < int.Parse(tren)) && (item.DiemYThuc > int.Parse(duoi)))
                { danhsachdiem.Add(item.DiemYThuc); }
            };
            List<diemythuc> final = new List<diemythuc>();


            foreach (var item in danhsachdiem)  // có danh sahs điểm r thì nạp vào 
            {
                List<diemythuc> bangdiems1 = Dataproviderdiemythuc.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemythuc WHERE `DiemYThuc`=" + item + " ");
                foreach (var item1 in bangdiems1)
                {
                    final.Add(item1);
                }
            }
            return View(final);
        }


        // edit 
        [HttpGet]
        // đóng vai trò đưa lên dũ liệu đã lưu 
        public IActionResult EditDiemYThuclink()
        {   // dùng link để lấy ID 
            /// Lấy ID đang chỉnh sửa => lấy bangừ link 
            var reqUrl = Request.HttpContext.Request;
            var urlPath = reqUrl.Path; /// Path: Home/EditLT/ID
            var CurrentID = urlPath.ToString().Split('/').Last(); /// Lấy LabID hiện tại thông qua tên miền
            var diemythuc = Dataproviderdiemythuc.Instance.ExecuteQuery($"SELECT * FROM webnhansu.diemythuc where LabID = '{CurrentID}'"); /// Hiển thị người đang chỉnh sửa hiện tại

            // phân quyền 

            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            List<TKDangNhap> TkDangNhaps1 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyennhansu`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps1.Count > 0)
            { return View(diemythuc); }
            else
            {
                return RedirectToAction("Index", "TkSuper");// trả về trang chủ 
            }
        }

        [HttpPost]
        public IActionResult EditDiemYThuclink(String LabID, String Ten, String DiemYThuc)
        {
            /// Lấy LabID đang chỉnh sửa
            var reqUrl = Request.HttpContext.Request;
            var urlPath = reqUrl.Path;
            var CurrentID = urlPath.ToString().Split('/').Last();
            ///

            Dataproviderdiemptbt.Instance.ExecuteQuery($"update webnhansu.diemythuc " +
                                $"set Ten = '{Ten}',DiemYThuc= '{DiemYThuc}' " +
                                $"where LabID = {LabID} ");  // udate dữ liệu 
                                                             // lưu phiên làm việc 
                                                           
            // lưu dữ liệu phiên làm việc 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            var chinhsua = DataproviderSaveDataLogin.Instance.ExecuteQuery("INSERT INTO webnhansu.savedataedit VALUES(" + tkdangnhap.LabID + ",'" + DateTime.Now.ToString() + "','Chỉnh sửa điểm ý thức thủ công LabID="+LabID+"')");

            return EditDiemYThuclink();   // gọi vòng lại hàm 
        }




        // chuyển trang 
        public IActionResult diemythuc() // action
        {

            return RedirectToAction("Index", "DiemYThuc");
        }
    }
}
