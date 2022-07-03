using Điểm_Đào_Tạo.DAO;
using Điểm_Đào_Tạo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Điểm_Đào_Tạo.Controllers
{
    public class ChonTkDangNhapController : Controller
    {
        private readonly IWebHostEnvironment _iweb;

        //contructor 
        public ChonTkDangNhapController(IWebHostEnvironment iweb)
        {
            _iweb = iweb;
        }

        public IActionResult Index()
        {
           
            return View();
        }

        // tài khoản User 
        [HttpGet]
        public IActionResult tkuser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult tkuser(string IP, string pw)
        {

            string query = "SELECT * FROM `webnhansu`.`tkdangnhap`  WHERE `MatKhau`='" + pw.ToString() + "'AND `LabID`=" + int.Parse(IP) + "";
            List<TKDangNhap> TkDangNhaps = DataproviderTKDangNhap.Instance.ExecuteQuery(query);
            if (TkDangNhaps.Count() > 0)  // dữ liệu chưa chắc đã rống đâu nha 
            {

                // lưu dữ liệu đang nhập lên session 
                // set the value into session key 
                HttpContext.Session.SetString("DangNhapSession", JsonConvert.SerializeObject(TkDangNhaps[0]));// set Student Session thành 1 JsonConvert 

                return RedirectToAction("Index", "TkUser");
            }
            else{ return View(); }
          
        }



        // tài khoản super 
        [HttpGet]
        public IActionResult tksuper()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult tksuper(string IP, string pw)
        {

            string query = "SELECT * FROM `webnhansu`.`phanquyentklt`  WHERE `MatKhau`='" + pw.ToString() + "'AND `LabID`=" + int.Parse(IP) + "";
            List<TKDangNhap> TkDangNhaps = DataproviderTKDangNhap.Instance.ExecuteQuery(query);
            if (TkDangNhaps.Count() > 0)  // dữ liệu chưa chắc đã rống đâu nha 
            {

                // lưu dữ liệu đang nhập lên session 
                // set the value into session key 
                HttpContext.Session.SetString("DangNhapSession", JsonConvert.SerializeObject(TkDangNhaps[0]));// set Student Session thành 1 JsonConvert 

                return RedirectToAction("Index", "TkSuper");
            }
            return View();
        }




        // tài khoản chỉnh sửa quyền truy cập 
        [HttpGet]
        public IActionResult tkchinhsuaquyentruycap()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult tkchinhsuaquyentruycap(string IP, string pw)
        {

            string query = "SELECT * FROM `webnhansu`.`phanquyenadmin`  WHERE `MatKhau`='" + pw.ToString() + "'AND `LabID`=" + int.Parse(IP) + "";
            List<TKDangNhap> TkDangNhaps = DataproviderTKDangNhap.Instance.ExecuteQuery(query);
            if (TkDangNhaps.Count() > 0)  // dữ liệu chưa chắc đã rống đâu nha 
            {

                // lưu dữ liệu đang nhập lên session 
                // set the value into session key 
                HttpContext.Session.SetString("DangNhapSession", JsonConvert.SerializeObject(TkDangNhaps[0]));// set Student Session thành 1 JsonConvert 

                return RedirectToAction("QuanLyTaiKhoan", "TkPhanQuyenTruyCap");
            }
            return View();
        }
    }
}
