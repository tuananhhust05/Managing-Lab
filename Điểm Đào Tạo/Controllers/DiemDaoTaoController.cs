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
using System.IO;
using System.Linq;

namespace Điểm_Đào_Tạo.Controllers
{
    public class DiemDaoTaoController : Controller
    {
        public IActionResult Index()
        {
            List<DiemDaoTao> bangdiems = new List<DiemDaoTao>(); // trùm cuối 
            // quét và thêm  
            List<DiemDaoTao> bangdiems1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptba");
            foreach (var item in bangdiems1)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> bangdiems2 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptcokhi");
            foreach (var item in bangdiems2)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> bangdiems3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptlaptrinh");
            foreach (var item in bangdiems3)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> bangdiems4 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptngoaingu");
            foreach (var item in bangdiems4)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> bangdiems5 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptptbt");
            foreach (var item in bangdiems5)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> bangdiems6 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptquantridoanhnghiep");
            foreach (var item in bangdiems6)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> bangdiems7 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diempttudonghoa");
            foreach (var item in bangdiems7)
            {
                bangdiems.Add(item);
            }
            return View(bangdiems);
        }




        // Trưởng PT 

        //1. Show các chức năng 
        public IActionResult truongpt()
        {
            return View();
        }
        //2. Show danh sách thành viên PT 
        public IActionResult showdanhsachPT()
        {
            // lấy thông tin đăng nhập từ session 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));// lấy thong tin từ bên kia qua 

            List<TKDangNhap> TkDangNhaps1 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendiemptba`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps1.Count > 0)
            {
                List<DiemDaoTao> TkDangNhaps11 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`diemptba`");
                return View(TkDangNhaps11);
            }
            List<TKDangNhap> TkDangNhaps2 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendiemptquantridoanhnghiep`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps2.Count > 0)
            {
                List<DiemDaoTao> TkDangNhaps22 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`diemptquantridoanhnghiep`");
                return View(TkDangNhaps22);
            }

            List<TKDangNhap> TkDangNhaps3 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendiemptngoaingu`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps3.Count > 0)
            {
                List<DiemDaoTao> TkDangNhaps33 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`diemptngoaingu`");
                return View(TkDangNhaps33);
            }


            List<TKDangNhap> TkDangNhaps4 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendiemptptbt`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps4.Count > 0)
            {
                List<DiemDaoTao> TkDangNhaps44 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`diemptptbt`");
                return View(TkDangNhaps44);
            }


            List<TKDangNhap> TkDangNhaps5 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendiemptcokhi`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps5.Count > 0)
            {
                List<DiemDaoTao> TkDangNhaps55 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`diemptcokhi`");
                return View(TkDangNhaps55);
            }
            List<TKDangNhap> TkDangNhaps6 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendiempttudonghoa`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps6.Count > 0)
            {
                List<DiemDaoTao> TkDangNhaps66 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`diempttudonghoa`");
                return View(TkDangNhaps66);
            }

            List<TKDangNhap> TkDangNhaps7 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendiemptlaptrinh`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps7.Count > 0)
            {
                List<DiemDaoTao> TkDangNhaps77 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`diemptlaptrinh`");
                return View(TkDangNhaps77);
            }
            return View();
        }

        //3. Cập nhật điểm bằng excel 
        [HttpGet]
        public IActionResult capnhatdiemptbangex()
        {
            return View();
        }
        [HttpPost]
        [System.Obsolete]
        public IActionResult capnhatdiemptbangex(IFormFile file, [FromServices] IHostingEnvironment hostingEnviroment)
        {
            string fileName = $"{hostingEnviroment.WebRootPath}\\files\\{file.FileName}";  // địa chỉ để tải file lên => sau đó đọc
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            var bangdiems = this.GetStudentList1(file.FileName);

            // lưu dữ liệu phiên làm việc 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            var chinhsua = DataproviderSaveDataLogin.Instance.ExecuteQuery("INSERT INTO webnhansu.savedataedit VALUES(" + tkdangnhap.LabID + ",'" + DateTime.Now.ToString() + "','Cập nhật điểm đào tạo bằng excel')");

            return View(bangdiems);  // truyền vào danh sách những đối tượng lấy từ file excel 
        }
        private List<DiemDaoTao> GetStudentList1(string fName) // hàm phụ 
        {
            List<DiemDaoTao> bangdiems = new List<DiemDaoTao>();  // lấy dữ liệu từ file excel 
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        bangdiems.Add(new DiemDaoTao()// bangdiems đang chứa tất cả dữ liệu từ excel 
                        {
                            LabID = int.Parse(reader.GetValue(0).ToString()),
                            Ten = reader.GetValue(1).ToString(),
                            Diem = int.Parse(reader.GetValue(2).ToString()),

                        });
                    }
                }
            }
            // lấy dữ liệu từ session 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));// lấy thong tin từ bên kia qua 




            // đối chiếu dữ liệu trong bảng phân quyền và sử lý 
            List<TKDangNhap> TkDangNhaps1 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendiemptba`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps1.Count > 0)  // thỏa mãn thì bắt tay vào sửa và trả về list sau khi sửa 
            {
                // sửa 
                foreach (var item in bangdiems)  // thao tác cộng điểm trong này luôn 
                {
                    List<DiemDaoTao> bangdiems1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptba WHERE `LabID`=" + item.LabID + ""); // chọn , là 1 list 
                    if (bangdiems1.Count > 0)  // phòng exception 
                    {
                        List<DiemDaoTao> bangdiems2 = Dataproviderdiemdaotao.Instance.ExecuteQuery("DELETE FROM webnhansu.diemptba WHERE `LabID`=" + item.LabID + "");// xóa thằng cũ 
                        int a = 0;
                        a = bangdiems1[0].Diem + item.Diem;
                        List<DiemDaoTao> bangdiems3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("INSERT INTO  webnhansu.diemptba VALUES(" + bangdiems1[0].LabID + ",'" + bangdiems1[0].Ten + "'," + a + ")");
                    }

                };
                // trả về màn hình những thằng đã sửa 
                List<DiemDaoTao> bangdiemcapnhat1 = new List<DiemDaoTao>();
                foreach (var item in bangdiems)
                {
                    List<DiemDaoTao> bangdiems1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptba WHERE `LabID`=" + item.LabID + "");
                    foreach (var item1 in bangdiems1)
                    {
                        bangdiemcapnhat1.Add(item1);  // add từng thằng 
                    }

                }

                return bangdiemcapnhat1;
            }






            List<TKDangNhap> TkDangNhaps2 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendiemptquantridoanhnghiep`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps2.Count > 0)
            {
                // sửa 
                foreach (var item in bangdiems)  // thao tác cộng điểm trong này luôn 
                {
                    List<DiemDaoTao> bangdiems1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptquantridoanhnghiep WHERE `LabID`=" + item.LabID + ""); // chọn , là 1 list 
                    if (bangdiems1.Count > 0)  // phòng exception 
                    {
                        List<DiemDaoTao> bangdiems2 = Dataproviderdiemdaotao.Instance.ExecuteQuery("DELETE FROM webnhansu.diemptquantridoanhnghiep WHERE `LabID`=" + item.LabID + "");// xóa thằng cũ 
                        int a = 0;
                        a = bangdiems1[0].Diem + item.Diem;
                        List<DiemDaoTao> bangdiems3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("INSERT INTO  webnhansu.diemptquantridoanhnghiep VALUES(" + bangdiems1[0].LabID + ",'" + bangdiems1[0].Ten + "'," + a + ")");
                    }

                };
                // trả về màn hình những thằng đã sửa 
                List<DiemDaoTao> bangdiemcapnhat1 = new List<DiemDaoTao>();
                foreach (var item in bangdiems)
                {
                    List<DiemDaoTao> bangdiems1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptquantridoanhnghiep WHERE `LabID`=" + item.LabID + "");
                    foreach (var item1 in bangdiems1)
                    {
                        bangdiemcapnhat1.Add(item1);  // add từng thằng 
                    }

                }

                return bangdiemcapnhat1;
            }

            List<TKDangNhap> TkDangNhaps3 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendiemptngoaingu`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps3.Count > 0)
            {
                // sửa 
                foreach (var item in bangdiems)  // thao tác cộng điểm trong này luôn 
                {
                    List<DiemDaoTao> bangdiems1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptngoaingu WHERE `LabID`=" + item.LabID + ""); // chọn , là 1 list 
                    if (bangdiems1.Count > 0)  // phòng exception 
                    {
                        List<DiemDaoTao> bangdiems2 = Dataproviderdiemdaotao.Instance.ExecuteQuery("DELETE FROM webnhansu.diemptngoaingu WHERE `LabID`=" + item.LabID + "");// xóa thằng cũ 
                        int a = 0;
                        a = bangdiems1[0].Diem + item.Diem;
                        List<DiemDaoTao> bangdiems3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("INSERT INTO  webnhansu.diemptngoaingu VALUES(" + bangdiems1[0].LabID + ",'" + bangdiems1[0].Ten + "'," + a + ")");
                    }

                };
                // trả về màn hình những thằng đã sửa 
                List<DiemDaoTao> bangdiemcapnhat1 = new List<DiemDaoTao>();
                foreach (var item in bangdiems)
                {
                    List<DiemDaoTao> bangdiems1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptngoaingu WHERE `LabID`=" + item.LabID + "");
                    foreach (var item1 in bangdiems1)
                    {
                        bangdiemcapnhat1.Add(item1);  // add từng thằng 
                    }

                }

                return bangdiemcapnhat1;
            }


            List<TKDangNhap> TkDangNhaps4 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendiemptptbt`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps4.Count > 0)
            {
                // sửa 
                foreach (var item in bangdiems)  // thao tác cộng điểm trong này luôn 
                {
                    List<DiemDaoTao> bangdiems1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptptbt WHERE `LabID`=" + item.LabID + ""); // chọn , là 1 list 
                    if (bangdiems1.Count > 0)  // phòng exception 
                    {
                        List<DiemDaoTao> bangdiems2 = Dataproviderdiemdaotao.Instance.ExecuteQuery("DELETE FROM webnhansu.diemptptbt WHERE `LabID`=" + item.LabID + "");// xóa thằng cũ 
                        int a = 0;
                        a = bangdiems1[0].Diem + item.Diem;
                        List<DiemDaoTao> bangdiems3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("INSERT INTO  webnhansu.diemptptbt VALUES(" + bangdiems1[0].LabID + ",'" + bangdiems1[0].Ten + "'," + a + ")");
                    }

                };
                // trả về màn hình những thằng đã sửa 
                List<DiemDaoTao> bangdiemcapnhat1 = new List<DiemDaoTao>();
                foreach (var item in bangdiems)
                {
                    List<DiemDaoTao> bangdiems1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptptbt WHERE `LabID`=" + item.LabID + "");
                    foreach (var item1 in bangdiems1)
                    {
                        bangdiemcapnhat1.Add(item1);  // add từng thằng 
                    }

                }

                return bangdiemcapnhat1;
            }


            List<TKDangNhap> TkDangNhaps5 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendiemptcokhi`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps5.Count > 0)
            {
                // sửa 
                foreach (var item in bangdiems)  // thao tác cộng điểm trong này luôn 
                {
                    List<DiemDaoTao> bangdiems1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptcokhi WHERE `LabID`=" + item.LabID + ""); // chọn , là 1 list 
                    if (bangdiems1.Count > 0)  // phòng exception 
                    {
                        List<DiemDaoTao> bangdiems2 = Dataproviderdiemdaotao.Instance.ExecuteQuery("DELETE FROM webnhansu.diemptcokhi WHERE `LabID`=" + item.LabID + "");// xóa thằng cũ 
                        int a = 0;
                        a = bangdiems1[0].Diem + item.Diem;
                        List<DiemDaoTao> bangdiems3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("INSERT INTO  webnhansu.diemptcokhi VALUES(" + bangdiems1[0].LabID + ",'" + bangdiems1[0].Ten + "'," + a + ")");
                    }

                };
                // trả về màn hình những thằng đã sửa 
                List<DiemDaoTao> bangdiemcapnhat1 = new List<DiemDaoTao>();
                foreach (var item in bangdiems)
                {
                    List<DiemDaoTao> bangdiems1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptcokhi WHERE `LabID`=" + item.LabID + "");
                    foreach (var item1 in bangdiems1)
                    {
                        bangdiemcapnhat1.Add(item1);  // add từng thằng 
                    }

                }

                return bangdiemcapnhat1;
            }
            List<TKDangNhap> TkDangNhaps6 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendiempttudonghoa`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps6.Count > 0)
            {
                // sửa 
                foreach (var item in bangdiems)  // thao tác cộng điểm trong này luôn 
                {
                    List<DiemDaoTao> bangdiems1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diempttudonghoa WHERE `LabID`=" + item.LabID + ""); // chọn , là 1 list 
                    if (bangdiems1.Count > 0)  // phòng exception 
                    {
                        List<DiemDaoTao> bangdiems2 = Dataproviderdiemdaotao.Instance.ExecuteQuery("DELETE FROM webnhansu.diempttudonghoa WHERE `LabID`=" + item.LabID + "");// xóa thằng cũ 
                        int a = 0;
                        a = bangdiems1[0].Diem + item.Diem;
                        List<DiemDaoTao> bangdiems3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("INSERT INTO  webnhansu.diempttudonghoa VALUES(" + bangdiems1[0].LabID + ",'" + bangdiems1[0].Ten + "'," + a + ")");
                    }

                };
                // trả về màn hình những thằng đã sửa 
                List<DiemDaoTao> bangdiemcapnhat1 = new List<DiemDaoTao>();
                foreach (var item in bangdiems)
                {
                    List<DiemDaoTao> bangdiems1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diempttudonghoa WHERE `LabID`=" + item.LabID + "");
                    foreach (var item1 in bangdiems1)
                    {
                        bangdiemcapnhat1.Add(item1);  // add từng thằng 
                    }

                }

                return bangdiemcapnhat1;
            }

            List<TKDangNhap> TkDangNhaps7 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendiemptlaptrinh`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps7.Count > 0)
            {
                // sửa 
                foreach (var item in bangdiems)  // thao tác cộng điểm trong này luôn 
                {
                    List<DiemDaoTao> bangdiems1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptlaptrinh WHERE `LabID`=" + item.LabID + ""); // chọn , là 1 list 
                    if (bangdiems1.Count > 0)  // phòng exception 
                    {
                        List<DiemDaoTao> bangdiems2 = Dataproviderdiemdaotao.Instance.ExecuteQuery("DELETE FROM webnhansu.diemptlaptrinh WHERE `LabID`=" + item.LabID + "");// xóa thằng cũ 
                        int a = 0;
                        a = bangdiems1[0].Diem + item.Diem;
                        List<DiemDaoTao> bangdiems3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("INSERT INTO  webnhansu.diemptlaptrinh VALUES(" + bangdiems1[0].LabID + ",'" + bangdiems1[0].Ten + "'," + a + ")");
                    }

                };
                // trả về màn hình những thằng đã sửa 
                List<DiemDaoTao> bangdiemcapnhat1 = new List<DiemDaoTao>();
                foreach (var item in bangdiems)
                {
                    List<DiemDaoTao> bangdiems1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptlaptrinh WHERE `LabID`=" + item.LabID + "");
                    foreach (var item1 in bangdiems1)
                    {
                        bangdiemcapnhat1.Add(item1);  // add từng thằng 
                    }

                }

                return bangdiemcapnhat1;
            }

            return bangdiems;

            // trả về danh sách điểm sau khi được cộng, điểm cộng thì có ở file excel r 
        }





        // Trưởng Ban Đào Tạo 
        //1. Xuất file excel danh sách PT 
        [HttpGet]
        public IActionResult xuatthanhvienPT()  // dùng get hiển thị thông tin cũ 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult xuatthanhvienPT(string PowerTeam)
        {
            // thiết kế các trường hợp cho query 
          


            string a = "SELECT* FROM webnhansu."+PowerTeam+" ";
            List<DiemDaoTao> bangdiems = Dataproviderdiemdaotao.Instance.ExecuteQuery(a);

            // xuất ra excel thôi 
            using (var workbook = new XLWorkbook())
            {  // nhét hết vào workbook
                var worksheet = workbook.Worksheets.Add("Students");
                var currentRow = 1;
                #region Header
                worksheet.Cell(currentRow, 1).Value = "LabID";
                worksheet.Cell(currentRow, 2).Value = "Tên";
                worksheet.Cell(currentRow, 3).Value = "Điểm";
              
                #endregion
                #region Body
                foreach (var student1 in bangdiems)
                {
                    currentRow++;
                    // chèn câu lệnh tìm chứng chỉ vào đây 
                    worksheet.Cell(currentRow, 1).Value = student1.LabID;
                    worksheet.Cell(currentRow, 2).Value = student1.Ten;
                    worksheet.Cell(currentRow, 3).Value = student1.Diem;
                   
                }
                #endregion
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-oficedocument.spreadsheetml.sheet",
                        "Danhsachthanhvien.xlsx"
                        );
                }
            }
        }

        //2.show trưởng ban dào tạo 
        public IActionResult truongbandaotao()
        {
            List<DiemDaoTao> bangdiems = new List<DiemDaoTao>(); // trùm cuối 
            // quét và thêm  
            List<DiemDaoTao> bangdiems1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptba");
            foreach (var item in bangdiems1)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> bangdiems2 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptcokhi");
            foreach (var item in bangdiems2)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> bangdiems3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptlaptrinh");
            foreach (var item in bangdiems3)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> bangdiems4 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptngoaingu");
            foreach (var item in bangdiems4)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> bangdiems5 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptptbt");
            foreach (var item in bangdiems5)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> bangdiems6 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptquantridoanhnghiep");
            foreach (var item in bangdiems6)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> bangdiems7 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diempttudonghoa");
            foreach (var item in bangdiems7)
            {
                bangdiems.Add(item);
            }
            return View(bangdiems);
        }


        // xuất toàn bộ danh sách điểm dào tạo ra file excel 
        public IActionResult xuatfulldanhsachdiemdaotao()
        {
            List<DiemDaoTao> bangdiems = new List<DiemDaoTao>(); // trùm cuối 
            // quét và thêm 
            // lấy dữ liệu
            List<DiemDaoTao> bangdiems1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptba");
            foreach (var item in bangdiems1)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> bangdiems2 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptcokhi");
            foreach (var item in bangdiems2)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> bangdiems3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptlaptrinh");
            foreach (var item in bangdiems3)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> bangdiems4 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptngoaingu");
            foreach (var item in bangdiems4)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> bangdiems5 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptptbt");
            foreach (var item in bangdiems5)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> bangdiems6 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptquantridoanhnghiep");
            foreach (var item in bangdiems6)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> bangdiems7 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diempttudonghoa");
            foreach (var item in bangdiems7)
            {
                bangdiems.Add(item);
            }
            using (var workbook = new XLWorkbook())
            {  // nhét hết vào workbook
                var worksheet = workbook.Worksheets.Add("Students");
                var currentRow = 1;
                #region Header
                worksheet.Cell(currentRow, 1).Value = "LabID";
                worksheet.Cell(currentRow, 2).Value = "Họ và tên";
                worksheet.Cell(currentRow, 3).Value = "Điểm Đào Tạo";


                #endregion
                #region Body
                foreach (var student1 in bangdiems)
                {
                    currentRow++;
                    // chèn câu lệnh tìm chứng chỉ vào đây 
                    worksheet.Cell(currentRow, 1).Value = student1.LabID;
                    worksheet.Cell(currentRow, 2).Value = student1.Ten;
                    worksheet.Cell(currentRow, 3).Value = student1.Diem;


                }
                #endregion
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-oficedocument.spreadsheetml.sheet",
                        "BangDiemDaoTao.xlsx"
                        );
                }
            }
        }






        // tìm kiếm theo trường dữ liệu 
        [HttpGet]
        public IActionResult timkiemdiemdaotaotheoID()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult timkiemdiemdaotaotheoID(string Truong, string Infor)
        {
            List<DiemDaoTao> bangdiems = new List<DiemDaoTao>(); // trùm cuối


            // quét trên 7 bảng 
            if (Truong == "LabID")
            {
                List<DiemDaoTao> TkDangNhaps1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptlaptrinh WHERE `"+ Truong + "`=" + Infor + "");
                foreach (var item in TkDangNhaps1)
                {
                    bangdiems.Add(item);
                }
                List<DiemDaoTao> TkDangNhaps2 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diempttudonghoa WHERE `" + Truong + "`=" + Infor + "");
                foreach (var item in TkDangNhaps2)
                {
                    bangdiems.Add(item);
                }
                List<DiemDaoTao> TkDangNhaps3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptcokhi WHERE `" + Truong + "`=" + Infor + "");
                foreach (var item in TkDangNhaps3)
                {
                    bangdiems.Add(item);
                }
                List<DiemDaoTao> TkDangNhaps4 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptba WHERE `" + Truong + "`=" + Infor + "");
                foreach (var item in TkDangNhaps4)
                {
                    bangdiems.Add(item);
                }
                List<DiemDaoTao> TkDangNhaps5 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptquantridoanhnghiep WHERE `" + Truong + "`=" + Infor + "");
                foreach (var item in TkDangNhaps5)
                {
                    bangdiems.Add(item);
                }
                List<DiemDaoTao> TkDangNhaps6 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptngoaingu WHERE `" + Truong + "`=" + Infor + "");
                foreach (var item in TkDangNhaps6)
                {
                    bangdiems.Add(item);
                }
                List<DiemDaoTao> TkDangNhaps7 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptptbt WHERE `" + Truong + "`=" + Infor + "");
                foreach (var item in TkDangNhaps7)
                {
                    bangdiems.Add(item);
                }
            }


            // quét trên 7 bảng 
            // khác nhau mỗi kiểu ký tự string hay số
            if (Truong == "Ten")
            {
                List<DiemDaoTao> TkDangNhaps1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptlaptrinh WHERE `" + Truong + "`='" + Infor + "'");
                foreach (var item in TkDangNhaps1)
                {
                    bangdiems.Add(item);
                }
                List<DiemDaoTao> TkDangNhaps2 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diempttudonghoa WHERE `" + Truong + "`='" + Infor + "'");
                foreach (var item in TkDangNhaps2)
                {
                    bangdiems.Add(item);
                }
                List<DiemDaoTao> TkDangNhaps3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptcokhi WHERE `" + Truong + "`='" + Infor + "'");
                foreach (var item in TkDangNhaps3)
                {
                    bangdiems.Add(item);
                }
                List<DiemDaoTao> TkDangNhaps4 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptba WHERE `" + Truong + "`='" + Infor + "'");
                foreach (var item in TkDangNhaps4)
                {
                    bangdiems.Add(item);
                }
                List<DiemDaoTao> TkDangNhaps5 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptquantridoanhnghiep WHERE `" + Truong + "`='" + Infor + "'");
                foreach (var item in TkDangNhaps5)
                {
                    bangdiems.Add(item);
                }
                List<DiemDaoTao> TkDangNhaps6 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptngoaingu WHERE `" + Truong + "`='" + Infor + "'");
                foreach (var item in TkDangNhaps6)
                {
                    bangdiems.Add(item);
                }
                List<DiemDaoTao> TkDangNhaps7 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptptbt WHERE `" + Truong + "`='" + Infor + "'");
                foreach (var item in TkDangNhaps7)
                {
                    bangdiems.Add(item);
                }
            }





            return View(bangdiems);
        }



        // Xác định PT trực thuộc của ID 
        [HttpGet]
        public IActionResult xacdinhPT()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult xacdinhPT(string ID)
        {

            List<SignalChar> a = new List<SignalChar>();
            List<DiemDaoTao> TkDangNhaps1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptlaptrinh WHERE `LabID`=" + ID + "");
            if (TkDangNhaps1.Count > 0)
            {
                SignalChar x = new SignalChar();
                x.signal = "Power Team Lập Trình ";
                a.Add(x);
                return View(a);
            }
            List<DiemDaoTao> TkDangNhaps2 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diempttudonghoa WHERE `LabID`=" + ID + "");
            if (TkDangNhaps2.Count > 0)
            {
                SignalChar x = new SignalChar();
                x.signal = "Power Team  Tự động hóa ";
                a.Add(x);
                return View(a);
            }
            List<DiemDaoTao> TkDangNhaps3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptcokhi WHERE `LabID`=" + ID + "");
            if (TkDangNhaps3.Count > 0)
            {
                SignalChar x = new SignalChar();
                x.signal = "Power Team  Cơ Khí";
                a.Add(x);
                return View(a);
            }
            List<DiemDaoTao> TkDangNhaps4 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptba WHERE `LabID`=" + ID + "");
            if (TkDangNhaps4.Count > 0)
            {
                SignalChar x = new SignalChar();
                x.signal = "Power Team  BA";
                a.Add(x);
                return View(a);
            }
            List<DiemDaoTao> TkDangNhaps5 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptquantridoanhnghiep WHERE `LabID`=" + ID + "");
            if (TkDangNhaps5.Count > 0)
            {
                SignalChar x = new SignalChar();
                x.signal = "Power Team  Quản trị doanh nghiệp và Markerting";
                a.Add(x);
                return View(a);
            }
            List<DiemDaoTao> TkDangNhaps6 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptngoaingu WHERE `LabID`=" + ID + "");
            if (TkDangNhaps6.Count > 0)
            {
                SignalChar x = new SignalChar();
                x.signal = "Power Team  Ngoại Ngữ  ";
                a.Add(x);
                return View(a);
            }
            List<DiemDaoTao> TkDangNhaps7 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptptbt WHERE `LabID`=" + ID + "");
            if (TkDangNhaps7.Count > 0)
            {
                SignalChar x = new SignalChar();
                x.signal = "Power Team  Phát Triển Bản Thân";
                a.Add(x);
                return View(a);
            }


            SignalChar x1 = new SignalChar();
            x1.signal = "Không thuộc PT nào";
            a.Add(x1);
            return View(a);
        }





        ///------------------------------------------------------------
        ///EDIT DieMDaoTao
        [HttpGet]
        // đóng vai trò đưa lên dũ liệu đã lưu 
        public IActionResult EditDiemDaoTaolink()
        {  
            /// Lấy ID đang chỉnh sửa
            var reqUrl = Request.HttpContext.Request;
            var urlPath = reqUrl.Path; /// Path: Home/EditLT/ID
            var Infor = urlPath.ToString().Split('/').Last(); /// Lấy LabID hiện tại thông qua tên miền


            List<DiemDaoTao> bangdiems = new List<DiemDaoTao>();
            List<DiemDaoTao> TkDangNhaps1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptlaptrinh WHERE `LabID`=" + Infor.ToString() + "");
            foreach (var item in TkDangNhaps1)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> TkDangNhaps2 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diempttudonghoa WHERE `LabID`=" + Infor.ToString() + "");
            foreach (var item in TkDangNhaps2)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> TkDangNhaps3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptcokhi WHERE `LabID`=" + Infor.ToString() + "");
            foreach (var item in TkDangNhaps3)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> TkDangNhaps4 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptba WHERE `LabID`=" + Infor.ToString() + "");
            foreach (var item in TkDangNhaps4)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> TkDangNhaps5 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptquantridoanhnghiep WHERE `LabID`=" + Infor.ToString() + "");
            foreach (var item in TkDangNhaps5)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> TkDangNhaps6 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptngoaingu WHERE `LabID`=" + Infor.ToString() + "");
            foreach (var item in TkDangNhaps6)
            {
                bangdiems.Add(item);
            }
            List<DiemDaoTao> TkDangNhaps7 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptptbt WHERE `LabID`=" + Infor.ToString() + "");
            foreach (var item in TkDangNhaps7)
            {
                bangdiems.Add(item);
            }

            // phân quyền 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            List<TKDangNhap> TkDangNhaps100 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyendaotao`  WHERE `MatKhau`='" + tkdangnhap.MatKhau.ToString() + "'AND `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps100.Count > 0)
            { return View(bangdiems); }
            else
            {
                return RedirectToAction("Index", "TkSuper");// trả về trang chủ 
            }
        }

        [HttpPost]
        public IActionResult EditDiemDaoTaolink(String LabID, String Ten, String Diem)
        {
            /// Lấy LabID đang chỉnh sửa
            var reqUrl = Request.HttpContext.Request;
            var urlPath = reqUrl.Path;
            var CurrentID = urlPath.ToString().Split('/').Last();
            ///

            // quét 7 bảng 
            List<DiemDaoTao> TkDangNhaps1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptlaptrinh WHERE `LabID`=" + LabID + "");
            if (TkDangNhaps1.Count > 0)
            {
                Dataproviderdiemptbt.Instance.ExecuteQuery($"update webnhansu.diemptlaptrinh " +
                               $"set Ten = '{Ten}',Diem= '{Diem}' " +
                               $"where LabID = {LabID} ");  // udate dữ liệu 
            }
            List<DiemDaoTao> TkDangNhaps2 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diempttudonghoa WHERE `LabID`=" + LabID + "");
            if (TkDangNhaps2.Count > 0)
            {
                Dataproviderdiemptbt.Instance.ExecuteQuery($"update webnhansu.diempttudonghoa " +
                              $"set Ten = '{Ten}',Diem= '{Diem}' " +
                              $"where LabID = {LabID} ");  // udate dữ liệu 
            }
            List<DiemDaoTao> TkDangNhaps3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptcokhi WHERE `LabID`=" + LabID + "");
            if (TkDangNhaps3.Count > 0)
            {
                Dataproviderdiemptbt.Instance.ExecuteQuery($"update webnhansu.diemptcokhi " +
                              $"set Ten = '{Ten}',Diem= '{Diem}' " +
                              $"where LabID = {LabID} ");  // udate dữ liệu 
            }
            List<DiemDaoTao> TkDangNhaps4 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptba WHERE `LabID`=" + LabID + "");
            if (TkDangNhaps4.Count > 0)
            {
                Dataproviderdiemptbt.Instance.ExecuteQuery($"update webnhansu.diemptba " +
                             $"set Ten = '{Ten}',Diem= '{Diem}' " +
                             $"where LabID = {LabID} ");  // udate dữ liệu 
            }
            List<DiemDaoTao> TkDangNhaps5 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptquantridoanhnghiep WHERE `LabID`=" + LabID + "");
            if (TkDangNhaps5.Count > 0)
            {
                Dataproviderdiemptbt.Instance.ExecuteQuery($"update webnhansu.diemptquantridoanhnghiep " +
                             $"set Ten = '{Ten}',Diem= '{Diem}' " +
                             $"where LabID = {LabID} ");  // udate dữ liệu 
            }
            List<DiemDaoTao> TkDangNhaps6 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptngoaingu WHERE `LabID`=" + LabID + "");
            if (TkDangNhaps6.Count > 0)
            {
                Dataproviderdiemptbt.Instance.ExecuteQuery($"update webnhansu.diemptngoaingu " +
                             $"set Ten = '{Ten}',Diem= '{Diem}' " +
                             $"where LabID = {LabID} ");  // udate dữ liệu 
            }
            List<DiemDaoTao> TkDangNhaps7 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptptbt WHERE `LabID`=" + LabID + "");
            if (TkDangNhaps7.Count > 0)
            {
                Dataproviderdiemptbt.Instance.ExecuteQuery($"update webnhansu.diemptptbt " +
                              $"set Ten = '{Ten}',Diem= '{Diem}' " +
                              $"where LabID = {LabID} ");  // udate dữ liệu 
            }



            // lưu phiên làm việc 
            // lưu dữ liệu phiên làm việc 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            var chinhsua = DataproviderSaveDataLogin.Instance.ExecuteQuery("INSERT INTO webnhansu.savedataedit VALUES(" + tkdangnhap.LabID + ",'" + DateTime.Now.ToString() + "','Chỉnh sửa điểm đào tạo thủ công LabID="+ LabID+ "')");


            // update xong gọi lại thằng LabID được chọn 

            return EditDiemDaoTaolink();   // gọi vòng lại hàm 
        }

        // chuyển trang 
        public IActionResult diemdaotao() // action
        {

            return RedirectToAction("Index", "DiemDaoTao");
        }
    }
}
