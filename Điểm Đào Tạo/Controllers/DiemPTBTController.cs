using Microsoft.AspNetCore.Mvc;
using Điểm_Đào_Tạo.DAO;
using Điểm_Đào_Tạo.Models;
using System.Collections.Generic;
using ClosedXML.Excel;
using System.IO;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System;
using Newtonsoft.Json;

namespace Điểm_Đào_Tạo.Controllers
{
    public class DiemPTBTController : Controller
    {   // Show 
        public IActionResult Index()
        {
            string query = "SELECT * FROM webnhansu.diemptbt ";
            List<diemptbt> bangdiems = Dataproviderdiemptbt.Instance.ExecuteQuery(query);
            return View(bangdiems);
        }



        // Export excel Dptbt 
        public IActionResult xuatfilexdiemptbt()
        {
            string query = "SELECT * FROM webnhansu.diemptbt ";
            List<diemptbt> bangdiems = Dataproviderdiemptbt.Instance.ExecuteQuery(query);
            using (var workbook = new XLWorkbook())
            {  // nhét hết vào workbook
                var worksheet = workbook.Worksheets.Add("Students");
                var currentRow = 1;
                #region Header
                worksheet.Cell(currentRow, 1).Value = "LabID";
                worksheet.Cell(currentRow, 2).Value = "Họ và tên";
                worksheet.Cell(currentRow, 3).Value = "Điểm PTBT";


                #endregion
                #region Body
                foreach (var student1 in bangdiems)
                {
                    currentRow++;
                    // chèn câu lệnh tìm chứng chỉ vào đây 
                    worksheet.Cell(currentRow, 1).Value = student1.LabID;
                    worksheet.Cell(currentRow, 2).Value = student1.Ten;
                    worksheet.Cell(currentRow, 3).Value = student1.DiemPTBT;


                }
                #endregion
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-oficedocument.spreadsheetml.sheet",
                        "BangDiemPTBT.xlsx"
                        );
                }
            }
        }



        // Update data from excel 
        [HttpGet]
        public IActionResult capnhatdiemptbtbangexcel()
        {
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            List<TKDangNhap> TkDangNhaps1 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyenptbt`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps1.Count > 0)
            { return View(); }
            else
            {
                return RedirectToAction("Index", "TkSuper");// trả về trang chủ 
            }
        }
        [HttpPost]
        [System.Obsolete]
        public IActionResult capnhatdiemptbtbangexcel(IFormFile file, [FromServices] IHostingEnvironment hostingEnviroment)

        // phần tạo file trong wwwroot rất quan trọng 

        {
            string fileName = $"{hostingEnviroment.WebRootPath}\\files\\{file.FileName}";  // địa chỉ để tải file lên => sau đó đọc
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            var bangdiems = this.GetStudentList2(file.FileName);

            // lưu dữ liệu phiên làm việc 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            var chinhsua = DataproviderSaveDataLogin.Instance.ExecuteQuery("INSERT INTO webnhansu.savedataedit VALUES(" + tkdangnhap.LabID + ",'" + DateTime.Now.ToString() + "','Cập nhật điểm PTBT bằng excel')");

            return View(bangdiems);  // truyền vào danh sách những đối tượng lấy từ file excel 
        }
        private List<diemptbt> GetStudentList2(string fName) // hàm phụ 
        {
            List<diemptbt> bangdiems = new List<diemptbt>();  // lấy dữ liệu từ file excel 
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        bangdiems.Add(new diemptbt()// bangdiems đang chứa tất cả dữ liệu từ excel 
                        {
                            LabID = int.Parse(reader.GetValue(0).ToString()),
                            Ten = reader.GetValue(1).ToString(),
                            DiemPTBT = int.Parse(reader.GetValue(2).ToString()),

                        });
                    }
                }
            }

            foreach (var item in bangdiems)  // thao tác cộng điểm trong này luôn 
            {
                List<diemptbt> bangdiems1 = Dataproviderdiemptbt.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptbt WHERE `LabID`=" + item.LabID + ""); // chọn , là 1 list 
                if (bangdiems1.Count > 0)  // phòng exception 
                {
                    List<diemptbt> bangdiems2 = Dataproviderdiemptbt.Instance.ExecuteQuery("DELETE FROM webnhansu.diemptbt WHERE `LabID`=" + item.LabID + "");// xóa thằng cũ 
                    int a = 0;
                    a = bangdiems1[0].DiemPTBT + item.DiemPTBT;
                    List<diemptbt> bangdiems3 = Dataproviderdiemptbt.Instance.ExecuteQuery("INSERT INTO  webnhansu.diemptbt VALUES(" + bangdiems1[0].LabID + ",'" + bangdiems1[0].Ten + "'," + a + ")");
                }

            };
            // trả về danh sách điểm sau khi được cộng 
            List<diemptbt> bangdiemcapnhat = new List<diemptbt>();
            foreach (var item in bangdiems)
            {
                List<diemptbt> bangdiems1 = Dataproviderdiemptbt.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptbt WHERE `LabID`=" + item.LabID + "");
                foreach (var item1 in bangdiems1)
                {
                    bangdiemcapnhat.Add(item1);  // add từng thằng 
                }

            }


           
            return bangdiemcapnhat;   // trả về danh sách điểm sau khi được cộng, điểm cộng thì có ở file excel r 
        }




        // Find 
        public IActionResult timkiemdiemptbt()
        {
            return View();
        }
        // id
        [HttpGet]
        public IActionResult timkiemdiemptbtID()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult timkiemdiemptbtID(string ID)
        {

            string query = "SELECT * FROM webnhansu.diemptbt WHERE `LabID`=" + ID + "";
            List<diemptbt> TkDangNhaps = Dataproviderdiemptbt.Instance.ExecuteQuery(query);

            return View(TkDangNhaps);
        }


        // Ten
        [HttpGet]
        public IActionResult timkiemdiemptbtTen()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult timkiemdiemptbtTen(string ten)
        {

            string query = "SELECT * FROM webnhansu.diemptbt WHERE `Ten`='" + ten + "'";
            List<diemptbt> TkDangNhaps = Dataproviderdiemptbt.Instance.ExecuteQuery(query);

            return View(TkDangNhaps);
        }





       





        // sắp xếp điểm ptbt 
        public IActionResult sapxepdiemptbt()
        {
            string query = "SELECT * FROM webnhansu.diemptbt ";  // danh sách cuối cùng 
            List<diemptbt> final = new List<diemptbt>();// danh sách cuối cùng
            List<diemptbt> bangdiems = Dataproviderdiemptbt.Instance.ExecuteQuery(query);
            List<int> danhsachdiem = new List<int>();
            foreach (var item in bangdiems)
            {
                danhsachdiem.Add(item.DiemPTBT);
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
                List<diemptbt> bangdiems1 = Dataproviderdiemptbt.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptbt WHERE `DiemPTBT`=" + item + " ");
                foreach (var item1 in bangdiems1)
                {
                    final.Add(item1);
                }
            }
            return View(final);
        }







        // tìm kiếm điểm ptbt theo khoảng 
        public IActionResult timdiemptbttheokhoang()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult timdiemptbttheokhoang(string tren, string duoi)
        {

            string query = "SELECT * FROM webnhansu.diemptbt";
            List<diemptbt> TkDangNhaps = Dataproviderdiemptbt.Instance.ExecuteQuery(query);
            List<int> danhsachdiem = new List<int>();// khởi tạo 


            foreach (var item in TkDangNhaps)
            {
                if ((item.DiemPTBT < int.Parse(tren)) && (item.DiemPTBT > int.Parse(duoi)))
                { danhsachdiem.Add(item.DiemPTBT); }
            };
            List<diemptbt> final = new List<diemptbt>();


            foreach (var item in danhsachdiem)  // có danh sahs điểm r thì nạp vào 
            {
                List<diemptbt> bangdiems1 = Dataproviderdiemptbt.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptbt WHERE `DiemPTBT`=" + item + " ");
                foreach (var item1 in bangdiems1)
                {
                    final.Add(item1);
                }
            }
            return View(final);
        }



        // edit theo link 
        // controller 
        // cấu trúc thuật toán rất hay 
        [HttpGet]
        // đóng vai trò đưa lên dũ liệu đã lưu 
        public IActionResult EditDiemPTBTlink()
        {
            /// Lấy ID đang chỉnh sửa
            var reqUrl = Request.HttpContext.Request;
            var urlPath = reqUrl.Path; /// Path: Home/EditLT/ID
            var CurrentID = urlPath.ToString().Split('/').Last(); /// Lấy LabID hiện tại thông qua tên miền
            var diemPTBT = Dataproviderdiemptbt.Instance.ExecuteQuery($"SELECT * FROM webnhansu.diemptbt where LabID = '{CurrentID}'"); /// Hiển thị người đang chỉnh sửa hiện tại


            // phân quyền 

            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            List<TKDangNhap> TkDangNhaps1 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyenptbt`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps1.Count > 0)
            { return View(diemPTBT); }
            else
            {
                return RedirectToAction("Index", "TkSuper");// trả về trang chủ 
            }
        }

        [HttpPost]
        public IActionResult EditDiemPTBTlink( String LabID, String Ten,String DiemPTBT)
        {
            /// Lấy LabID đang chỉnh sửa
            var reqUrl = Request.HttpContext.Request;
            var urlPath = reqUrl.Path;
            var CurrentID = urlPath.ToString().Split('/').Last();
            ///

            Dataproviderdiemptbt.Instance.ExecuteQuery($"update webnhansu.diemptbt " +
                                $"set Ten = '{Ten}',DiemPTBT= '{DiemPTBT}' " +
                                $"where LabID = {LabID} ");  // udate dữ liệu 
                                                             // lưu dữ liệu phiên làm việc 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            var chinhsua = DataproviderSaveDataLogin.Instance.ExecuteQuery("INSERT INTO webnhansu.savedataedit VALUES(" + tkdangnhap.LabID + ",'" + DateTime.Now.ToString() + "','Edit điểm PTBT thủ công LabID="+LabID+"')");

            return EditDiemPTBTlink();   // gọi vòng lại hàm 
        }



        public IActionResult diemptbt() // action
        {

            return RedirectToAction("Index", "DiemPTBT");
        }
    }
}
