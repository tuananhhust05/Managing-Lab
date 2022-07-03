using Điểm_Đào_Tạo.DAO;
using Điểm_Đào_Tạo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Điểm_Đào_Tạo.Controllers
{
    public class TkUserController : Controller
    {
        private readonly IWebHostEnvironment _iweb;

        //contructor 
        public TkUserController(IWebHostEnvironment iweb)
        {
            _iweb = iweb;
        }
        public IActionResult Index()
        {

            // tạo ra đôi tượng kết quả 
            HienThiThongTinNguoiDungTkUser result = new HienThiThongTinNguoiDungTkUser();
            List<HienThiThongTinNguoiDungTkUser> results = new List<HienThiThongTinNguoiDungTkUser>();
            // lấy thông tin từ session 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
         
            



        // xác định các thông tin từ bảng chính  
        var members = DataproviderBangChinh.Instance.ExecuteQuery($"select * from webnhansu.maintable where LabID = {tkdangnhap.LabID}");
            result.LabID = members[0].LabID;
            result.Ten = members[0].Ten;
            result.TheHe = members[0].TheHe;
            result.SDT = members[0].SDT;
            result.NganhHoc = members[0].NganhHoc;
            result.TruongHoc = members[0].TruongHoc;
            result.email = members[0].email;
            result.QueQuan = members[0].QueQuan;
            result.TrangThai = members[0].TrangThai;
            result.Image = members[0].Image;
            // view bag 
            ViewBag.Name = "Xin chào" +" "+ result.Ten;

            // xác đinh PT , tích hợp điểm luôn  
            List<DiemDaoTao> TkDangNhaps1 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptlaptrinh WHERE `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps1.Count > 0)
            {
                SignalChar x = new SignalChar();
                x.signal = "Power Team Lập Trình ";
                result.PowerTeam = x.signal;
                result.Diem = TkDangNhaps1[0].Diem;
            }
            List<DiemDaoTao> TkDangNhaps2 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diempttudonghoa WHERE `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps2.Count > 0)
            {
                SignalChar x = new SignalChar();
                x.signal = "Power Team  Tự động hóa ";
                result.PowerTeam = x.signal;
                result.Diem = TkDangNhaps2[0].Diem;
            }
            List<DiemDaoTao> TkDangNhaps3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptcokhi WHERE `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps3.Count > 0)
            {
                SignalChar x = new SignalChar();
                x.signal = "Power Team  Cơ Khí";
                result.PowerTeam = x.signal;
                result.Diem = TkDangNhaps3[0].Diem;
            }
            List<DiemDaoTao> TkDangNhaps4 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptba WHERE `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps4.Count > 0)
            {
                SignalChar x = new SignalChar();
                x.signal = "Power Team  BA";
                result.PowerTeam = x.signal;
                result.Diem = TkDangNhaps4[0].Diem;
            }
            List<DiemDaoTao> TkDangNhaps5 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptquantridoanhnghiep WHERE `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps5.Count > 0)
            {
                SignalChar x = new SignalChar();
                x.signal = "Power Team  Quản trị doanh nghiệp và Markerting";
                result.PowerTeam = x.signal;
                result.Diem = TkDangNhaps5[0].Diem;
            }
            List<DiemDaoTao> TkDangNhaps6 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptngoaingu WHERE `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps6.Count > 0)
            {
                SignalChar x = new SignalChar();
                x.signal = "Power Team  Ngoại Ngữ  ";
                result.PowerTeam = x.signal;
                result.Diem = TkDangNhaps6[0].Diem;
            }
            List<DiemDaoTao> TkDangNhaps7 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptptbt WHERE `LabID`=" + tkdangnhap.LabID + "");
            if (TkDangNhaps7.Count > 0)
            {
                SignalChar x = new SignalChar();
                x.signal = "Power Team  Phát Triển Bản Thân";
                result.PowerTeam = x.signal;
                result.Diem = TkDangNhaps7[0].Diem;
            }
            // xác định điểm ý thức và điểm PTBT 
            List<diemptbt> TkDangNhaps8 = Dataproviderdiemptbt.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemptbt WHERE `LabID`=" + tkdangnhap.LabID + "");
            result.DiemPTBT = TkDangNhaps8[0].DiemPTBT;
            List<diemythuc> TkDangNhaps9 = Dataproviderdiemythuc.Instance.ExecuteQuery("SELECT * FROM webnhansu.diemythuc WHERE `LabID`=" + tkdangnhap.LabID + "");
            result.DiemPTBT = TkDangNhaps9[0].DiemYThuc;


            // chưngs chỉ 
            List<ChungChi> query_ChungChi = DataproviderChungChi.Instance.ExecuteQuery($"select * from webnhansu.chungchi where LabID = {tkdangnhap.LabID} ");
            string chungchi = query_ChungChi[0].Tenchungchi;
            for (int i=1;i< query_ChungChi.Count;i++)
            {
                chungchi = chungchi + "," + query_ChungChi[1].Tenchungchi;
            }
            result.Chungchi = chungchi;


            results.Add(result);
            return View(results);
        }


        // edit 
        [HttpGet]
        // đóng vai trò đưa lên dũ liệu đã lưu 
        public IActionResult Editthongtinlink()
        {   // dùng link để lấy ID 
            /// Lấy ID đang chỉnh sửa => lấy bangừ link 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            var bangchinh = DataproviderBangChinh.Instance.ExecuteQuery($"SELECT * FROM webnhansu.maintable where LabID = '{tkdangnhap.LabID}'"); /// Hiển thị người đang chỉnh sửa hiện tại



            
             return View(bangchinh); 
           

        }

        [HttpPost]
        // Lab ID này lấy ở đợt truyền vào dữ liệu lần đầu 
        public IActionResult Editthongtinlink( String Ten,String SDT, String NganhHoc, String TruongHoc, String email, String QueQuan)
        {
            // chúng ta không lấy ở ngoài mà ta tự tạo ra ở trong 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            var bangchinh = DataproviderBangChinh.Instance.ExecuteQuery($"SELECT * FROM webnhansu.maintable where LabID = '{tkdangnhap.LabID}'"); /// Hiển thị người đang chỉnh sửa hiện tại


            DataproviderBangChinh.Instance.ExecuteQuery($"update webnhansu.maintable " +
                                $"set Ten = '{Ten}',TheHe= '{bangchinh[0].TheHe}',SDT= '{SDT}',NganhHoc= '{NganhHoc}',TruongHoc= '{TruongHoc}',email= '{email}',QueQuan='{QueQuan}',TrangThai= '{bangchinh[0].TrangThai}' " +
                                $"where LabID = {tkdangnhap.LabID} ");  // udate dữ liệu 

            return Editthongtinlink();   // gọi vòng lại hàm 
        }


        public IActionResult thanhtichduan()
        {
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            string query = "SELECT * FROM webnhansu.`chitietduan` WHERE LabID ="+tkdangnhap.LabID+"";
            List<ChiTietDuAn> bangdiems = DataproviderChiTietDuAn.Instance.ExecuteQuery(query);
            return View(bangdiems);
        }

        public IActionResult thanhtichleaderteam()
        {
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            string query = "SELECT * FROM webnhansu.`lt` WHERE LabID =" + tkdangnhap.LabID + "";
            List<LT> bangdiems = DataproviderLT.Instance.ExecuteQuery(query);
            return View(bangdiems);
        }




        // triển khai sau khi đối tượng đăng nhập 
        [HttpGet]
        public IActionResult suamatkhau()
        {
            return View();
        }

        [HttpPost]
        public IActionResult suamatkhau( string MatKhauMoi,string XacNhanMatKhau )
        {
            // lấy thông tin đăng nhập 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            //chen tài khoản mới 
            SignalChar a = new SignalChar();
            List<SignalChar> result  = new List<SignalChar>();
            if (MatKhauMoi== XacNhanMatKhau)
            {
                DataproviderBangChinh.Instance.ExecuteQuery($"update webnhansu.tkdangnhap " +
                                $"set MatKhau = '{MatKhauMoi}' " +
                                $"where LabID = {tkdangnhap.LabID} ");  // udate dữ liệu
                a.signal = "Sửa thành công";
                result.Add(a);
                return View(result);
            }
            else {
                a.signal = "Sửa không thành công";
                result.Add(a);
                return View(result);
            }
          
        }




        // upload ảnh 
        [HttpGet]
        public IActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadImage(IFormFile file, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            var members = DataproviderBangChinh.Instance.ExecuteQuery($"select * from webnhansu.maintable where LabID = {tkdangnhap.LabID}");


            // upload và lưu ảnh vào hosting ; chức năng cập nhật thay thế ảnh trong hosting 
            // Tạo đường dẫn
            string fileName = $"{hostingEnvironment.WebRootPath}/files/{file.FileName}";
            //if (System.IO.File.Exists(fileName))
            //{
            fileName = fileName.Substring(0, fileName.Length - file.FileName.Length); // Bỏ đi tên cũ 
            fileName = fileName + tkdangnhap.LabID.ToString() + ".jpg";



            if (members[0].Image == null)
            {
                // Dẩy file vào thư mục
                using (FileStream fileStream = System.IO.File.Create(fileName))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }
            else  //nếu có rồi  thì delete trước rồi đẩy 
            {
                string imgdel = fileName;
                imgdel = Path.Combine(_iweb.WebRootPath, "files", imgdel);
                FileInfo fi = new FileInfo(imgdel);
                if (fi != null)
                {
                    System.IO.File.Delete(imgdel);
                    fi.Delete();
                }

                using (FileStream fileStream = System.IO.File.Create(fileName))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }

            }



            // cập nhật database 
            //Trình chiếu có biến thì không có ~ , thao tác để lưu đường link hợp lý 
            fileName = "/files/" + tkdangnhap.LabID.ToString() + ".jpg";
             DataproviderBangChinh.Instance.ExecuteQuery($"update webnhansu.maintable " +
                               $"set Image = '{fileName}' " +
                               $"where LabID = {tkdangnhap.LabID} ");  // udate dữ liệu 


            return RedirectToAction("Index", "TkUser");
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
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            var members = DataproviderBangChinh.Instance.ExecuteQuery($"select * from webnhansu.maintable where LabID = {tkdangnhap.LabID}");
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
            return RedirectToAction("Index", "TkUser");
        }
    }
}
