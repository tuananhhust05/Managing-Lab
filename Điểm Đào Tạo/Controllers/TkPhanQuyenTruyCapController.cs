using Điểm_Đào_Tạo.DAO;
using Điểm_Đào_Tạo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Điểm_Đào_Tạo.Controllers
{
    public class TkPhanQuyenTruyCapController : Controller
    {
        private readonly ILogger<TkPhanQuyenTruyCapController> _logger;

        public TkPhanQuyenTruyCapController(ILogger<TkPhanQuyenTruyCapController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        String Banghientai = "tklt";


        [HttpGet]
        public IActionResult QuanLyTaiKhoan(String Attribute = "tklt")
        {
            Attribute = Attribute;
            Bangtaikhoan display_Info = TaoBang(Attribute);

            if (Attribute == "tklt")
            {
                display_Info.Danhsachtaikhoan = DataproviderTKDangNhap.Instance.ExecuteQuery($"SELECT * FROM webnhansu.phanquyentklt");
            }
            else if (Attribute != null)
            {
                display_Info.Danhsachtaikhoan = DataproviderTKDangNhap.Instance.ExecuteQuery($"SELECT * FROM webnhansu.phanquyen{Attribute}");
            }
            return View(display_Info);
        }

        [HttpGet]
        public IActionResult AddPhanQuyen(String Attribute = "diemptlaptrinh")
        {
            Bangtaikhoan data_info = TaoBang(Attribute);
            data_info.Danhsachtaikhoan = DataproviderTKDangNhap.Instance.ExecuteQuery($"SELECT * FROM webnhansu.phanquyen{Attribute}");
            return View(data_info);
        }


        [HttpPost]
        public IActionResult AddPhanQuyen(int LabID, String Attribute = "diemptlaptrinh")
        {
            if (LabID != 0)
            {
                try
                {
                    Attribute = Attribute;
                    List<TKDangNhap> DanhSachTaiKhoan = DataproviderTKDangNhap.Instance.ExecuteQuery($"SELECT * FROM webnhansu.phanquyen{Attribute}");
                    foreach (var tk in DanhSachTaiKhoan)
                    {
                        if (LabID == tk.LabID)
                        {
                            return ErrorView("LabID này đã tồn tại!!");
                        }
                    }


                    List<TKDangNhap> TaiKhoan = DataproviderTKDangNhap.Instance.ExecuteQuery($"SELECT * FROM webnhansu.tkdangnhap where LabID = {LabID}");
                    var MatKhau = TaiKhoan[0].MatKhau;
                    DataproviderTKDangNhap.Instance.ExecuteQuery("SET SQL_SAFE_UPDATES = 0");
                    DataproviderTKDangNhap.Instance.ExecuteQuery($"insert webnhansu.phanquyen{Attribute} values ({LabID}, '{MatKhau}')"); // Cập nhật dữ liệu mới
                }

                catch
                {
                    return ErrorView("Không tìm thấy LabID trong danh sách thành viên!!");
                }

            }
            return AddPhanQuyen(Attribute);
        }

        public IActionResult ErrorView(String Error)
        {
            return View("ErrorView", Error);
        }

        public IActionResult DeletePhanQuyen()
        {
            var reqUrl = Request.HttpContext.Request;
            var urlPath = reqUrl.Path;
            var LastPath = urlPath.ToString().Split('/').Last();
            var Attribute = LastPath.ToString().Split('-').First();
            var CurrentLabID = LastPath.ToString().Split('-').Last();
            DataproviderTKDangNhap.Instance.ExecuteQuery($"delete from webnhansu.phanquyen{Attribute} where LabID = '{CurrentLabID}'");
            // Test: DataProviderTaiKhoan.Instance.ExecuteQuery($"insert webnhansu.phanquyendiemptba values (50002, '{CurrentLabID}')");
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    


        public Bangtaikhoan TaoBang(String Attribute)
        {
            Bangtaikhoan Bang = new Bangtaikhoan();
            Dictionary<String, String> LoaiVaTenBang = new Dictionary<String, String>();
            LoaiVaTenBang.Add("tklt", "Toàn bộ tài khoản");
            LoaiVaTenBang.Add("diemptlaptrinh", "Power Team Lập trình");
            LoaiVaTenBang.Add("diempttudonghoa", "Power Team Tự động hóa và IOM");
            LoaiVaTenBang.Add("diemptcokhi", "Power Team Cơ Khí - Cơ Điện Tử");
            LoaiVaTenBang.Add("diemptba", "Power Team Bussiness Analyst (BA)");
            LoaiVaTenBang.Add("diemptngoaingu", "Power Team Ngoại ngữ");
            LoaiVaTenBang.Add("diemptquantridoanhnghiep", "Power Team Quản trị doanh nghiệp và Marketing");
            LoaiVaTenBang.Add("diemptptbt", "Power Team PTBT");
            LoaiVaTenBang.Add("chutich", "Tài khoản Chủ tịch");
            LoaiVaTenBang.Add("daotao", "Tài khoản Ban Đào Tạo");
            LoaiVaTenBang.Add("nhansu", "Tài khoản Ban Nhân Sự");
            LoaiVaTenBang.Add("ptbt", "Tài khoản quản lý điểm PTBT");

            Bang.LoaiVaTenBang = LoaiVaTenBang;
            Bang.LoaiBang = Attribute;

            try
            {
                Bang.TenBang = LoaiVaTenBang[Attribute];
            }

            catch
            {
                Bang.LoaiBang = "tkdangnhap";
                Bang.TenBang = "Toàn bộ tài khoản";
            }

            return Bang;
        }
    }
}
