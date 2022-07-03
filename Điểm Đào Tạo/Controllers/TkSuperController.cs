using ClosedXML.Excel;
using Điểm_Đào_Tạo.DAO;
using Điểm_Đào_Tạo.Models;
using Điểm_Đào_Tạo.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Điểm_Đào_Tạo.Controllers
{
    public class TkSuperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        // quản lý view của phần quản lý điểm 
        public IActionResult quanlydiem()
        {
            return View();
        }


        // show thông tin các bảng 
        public IActionResult showthongtincacbang()
        {
            FullBangViewModel result = new FullBangViewModel();
            // nạp dữ liệu cho các bảng quan trọng 

            // bảng chitiet dự án 
           
            result.chitietduan = DataproviderChiTietDuAn.Instance.ExecuteQuery("SELECT * FROM webnhansu.`chitietduan`");


            // bảng chunchi 
            result.chungchi = DataproviderChungChi.Instance.ExecuteQuery("SELECT * FROM webnhansu.`chungchi`");

            // bảng diemptba
            result.diemptba = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.`diemptba`");
            // bảng diemptbt 
            result.ptbt = Dataproviderdiemptbt.Instance.ExecuteQuery("SELECT * FROM webnhansu.`diemptbt`");
            // bảng Pt cơ khí 
            result.diemptcokhi = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.`diemptcokhi`");
            // ptlaptrinh 
            result.diemptlaptrinh = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.`diemptlaptrinh`");
            // pt ngoại ngữ 
            result.diemptngoaingu = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.`diemptngoaingu`");
            // pt ptbt 
            result.diemptptbt = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.`diemptptbt`");
            // pt quantri 
            result.diemptquantridoanhnghiep = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.`diemptquantridoanhnghiep`");
            // pt tu dong hoa 
            result.diempttudonghoa = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.`diempttudonghoa`");
            // diem ythuc 
            result.ythuc = Dataproviderdiemythuc.Instance.ExecuteQuery("SELECT * FROM webnhansu.`diemythuc`");
            // ds su an 
            result.dsduan = DataproviderDuan.Instance.ExecuteQuery("SELECT * FROM webnhansu.`ds du an`");
            // quản lý LT 
            result.leader = DataproviderLT.Instance.ExecuteQuery("SELECT * FROM webnhansu.`lt`");
            // maintable
            result.bangchinh = DataproviderBangChinh.Instance.ExecuteQuery("SELECT * FROM webnhansu.`maintable`");
            // phân quyền lt 
            result.phanquyenlt = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM webnhansu.`phanquyentklt`");
            // tài khoản đăng nhập 
            result.taikhoan = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM webnhansu.`tkdangnhap`");


            // lấy thông tin 
            var tk = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            List<TKDangNhap> Tk = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM `webnhansu`.`phanquyenchutich`  WHERE `MatKhau`='" + tk.MatKhau.ToString() + "'AND `LabID`=" + tk.LabID + "");
            if (Tk.Count > 0)
            { return View(result); }
            else
            {
                return RedirectToAction("Index", "TkSuper");// trả về trang chủ 
            }
          
        }




        // tạo databaseclone 

        public IActionResult databaseclone()
            // chọn đúng kiêu dữ liệu cho từng bảng 
        {
           
            // xóa rồi nạp 
            List<ChiTietDuAn> TkDangNhaps1 = DataproviderChiTietDuAn.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.chitietduan");
            List<ChungChi> TkDangNhaps2 = DataproviderChungChi.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.chungchi");
            List<DiemDaoTao> TkDangNhaps3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.diemptba");
            List<diemptbt> TkDangNhaps4 = Dataproviderdiemptbt.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.diemptbt");
            List<DiemDaoTao> TkDangNhaps5 = Dataproviderdiemdaotao.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.diemptcokhi ");
            List<DiemDaoTao> TkDangNhaps6 = Dataproviderdiemdaotao.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.diemptlaptrinh");
            List<DiemDaoTao> TkDangNhaps7 = Dataproviderdiemdaotao.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.diemptngoaingu");
            List<DiemDaoTao> TkDangNhaps8 = Dataproviderdiemdaotao.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.diemptptbt");
            List<DiemDaoTao> TkDangNhaps9 = Dataproviderdiemdaotao.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.diemptquantridoanhnghiep");
            List<DiemDaoTao> TkDangNhaps10 = Dataproviderdiemdaotao.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.diempttudonghoa");
            List<diemythuc> TkDangNhaps11 = Dataproviderdiemythuc.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.diemythuc ");
            List<duan> TkDangNhaps12 = DataproviderDuan.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.`ds du an` ");
            List<LT> TkDangNhaps13 = DataproviderLT.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.lt ");
            List<MainTable> TkDangNhaps14 = DataproviderBangChinh.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.maintable ");
            List<TKDangNhap> TkDangNhaps15 = DataproviderTKDangNhap.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.phanquyenadmin ");
            List<TKDangNhap> TkDangNhaps16 = DataproviderTKDangNhap.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.phanquyenchutich");
            List<TKDangNhap> TkDangNhaps17 = DataproviderTKDangNhap.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.phanquyendaotao");
            List<TKDangNhap> TkDangNhaps18 = DataproviderTKDangNhap.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.phanquyendiemptba");
            List<TKDangNhap> TkDangNhaps19 = DataproviderTKDangNhap.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.phanquyendiemptcokhi");
            List<TKDangNhap> TkDangNhaps20 = DataproviderTKDangNhap.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.phanquyendiemptlaptrinh");
            List<TKDangNhap> TkDangNhaps21 = DataproviderTKDangNhap.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.phanquyendiempttudonghoa");
            List<TKDangNhap> TkDangNhaps22 = DataproviderTKDangNhap.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.phanquyendiemptptbt");
            List<TKDangNhap> TkDangNhaps23 = DataproviderTKDangNhap.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.phanquyendiemptngoaingu");
            List<TKDangNhap> TkDangNhaps24 = DataproviderTKDangNhap.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.phanquyennhansu");
            List<TKDangNhap> TkDangNhaps25 = DataproviderTKDangNhap.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.phanquyenptbt");
            List<TKDangNhap> TkDangNhaps26 = DataproviderTKDangNhap.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.phanquyentklt");
            List<TKDangNhap> TkDangNhaps27 = DataproviderTKDangNhap.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.tkdangnhap ");
            List<TKDangNhap> TkDangNhaps28 = DataproviderTKDangNhap.Instance.ExecuteQuery("DELETE FROM  webnhansuclone.phanquyendiemptquantridoanhnghiep");


            // nạp 
            List<ChiTietDuAn> TkDangNhap1 = DataproviderChiTietDuAn.Instance.ExecuteQuery("INSERT INTO webnhansuclone.chitietduan SELECT * FROM webnhansu.chitietduan");
            List<ChungChi> TkDangNhap2 = DataproviderChungChi.Instance.ExecuteQuery("INSERT INTO webnhansuclone.chungchi SELECT * FROM webnhansu.chungchi");
            List<DiemDaoTao> TkDangNhap3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("INSERT INTO webnhansuclone.diemptba SELECT * FROM webnhansu.diemptba");
            List<diemptbt> TkDangNhap4 = Dataproviderdiemptbt.Instance.ExecuteQuery("INSERT INTO webnhansuclone.diemptbt SELECT * FROM webnhansu.diemptbt");
            List<DiemDaoTao> TkDangNhap5 = Dataproviderdiemdaotao.Instance.ExecuteQuery("INSERT INTO webnhansuclone.diemptcokhi SELECT * FROM webnhansu.diemptcokhi ");
            List<DiemDaoTao> TkDangNhap6 = Dataproviderdiemdaotao.Instance.ExecuteQuery("INSERT INTO webnhansuclone.diemptlaptrinh SELECT * FROM webnhansu.diemptlaptrinh");
            List<DiemDaoTao> TkDangNhap7 = Dataproviderdiemdaotao.Instance.ExecuteQuery("INSERT INTO webnhansuclone.diemptngoaingu SELECT * FROM webnhansu.diemptngoaingu");
            List<DiemDaoTao> TkDangNhap8 = Dataproviderdiemdaotao.Instance.ExecuteQuery("INSERT INTO webnhansuclone.diemptptbt SELECT * FROM webnhansu.diemptptbt");
            List<DiemDaoTao> TkDangNhap9 = Dataproviderdiemdaotao.Instance.ExecuteQuery("INSERT INTO webnhansuclone.diemptquantridoanhnghiep SELECT * FROM webnhansu.diemptquantridoanhnghiep");
            List<DiemDaoTao> TkDangNhap10 = Dataproviderdiemdaotao.Instance.ExecuteQuery("INSERT INTO webnhansuclone.diempttudonghoa SELECT * FROM webnhansu.diempttudonghoa");
            List<diemythuc> TkDangNhap11 = Dataproviderdiemythuc.Instance.ExecuteQuery("INSERT INTO webnhansuclone.diemythuc SELECT * FROM webnhansu.diemythuc");
            List<duan> TkDangNhap12 = DataproviderDuan.Instance.ExecuteQuery("INSERT INTO webnhansuclone.`ds du an` SELECT * FROM webnhansu.`ds du an`");
            List<LT> TkDangNhap13 = DataproviderLT.Instance.ExecuteQuery("INSERT INTO webnhansuclone.lt SELECT * FROM webnhansu.lt");

            List<MainTable> TkDangNhap14 = DataproviderBangChinh.Instance.ExecuteQuery("INSERT INTO webnhansuclone.maintable SELECT * FROM webnhansu.maintable");
            List<TKDangNhap> TkDangNhap15 = DataproviderTKDangNhap.Instance.ExecuteQuery("INSERT INTO webnhansuclone.phanquyenadmin SELECT * FROM webnhansu.phanquyenadmin");
            List<TKDangNhap> TkDangNhap16 = DataproviderTKDangNhap.Instance.ExecuteQuery("INSERT INTO webnhansuclone.phanquyenchutich SELECT * FROM webnhansu.phanquyenchutich");
            List<TKDangNhap> TkDangNhap17 = DataproviderTKDangNhap.Instance.ExecuteQuery("INSERT INTO webnhansuclone.phanquyendaotao SELECT * FROM webnhansu.phanquyendaotao");
            List<TKDangNhap> TkDangNhap18 = DataproviderTKDangNhap.Instance.ExecuteQuery("INSERT INTO webnhansuclone.phanquyendiemptba SELECT * FROM webnhansu.phanquyendiemptba");
            List<TKDangNhap> TkDangNhap19 = DataproviderTKDangNhap.Instance.ExecuteQuery("INSERT INTO webnhansuclone.phanquyendiemptcokhi SELECT * FROM webnhansu.phanquyendiemptcokhi");
            List<TKDangNhap> TkDangNhap20 = DataproviderTKDangNhap.Instance.ExecuteQuery("INSERT INTO webnhansuclone.phanquyendiemptlaptrinh SELECT * FROM webnhansu.phanquyendiemptlaptrinh");
            List<TKDangNhap> TkDangNhap21 = DataproviderTKDangNhap.Instance.ExecuteQuery("INSERT INTO webnhansuclone.phanquyendiempttudonghoa SELECT * FROM webnhansu.phanquyendiempttudonghoa");
            List<TKDangNhap> TkDangNhap22 = DataproviderTKDangNhap.Instance.ExecuteQuery("INSERT INTO webnhansuclone.phanquyendiemptptbt SELECT * FROM webnhansu.phanquyendiemptptbt");
            List<TKDangNhap> TkDangNhap23 = DataproviderTKDangNhap.Instance.ExecuteQuery("INSERT INTO webnhansuclone.phanquyendiemptngoaingu SELECT * FROM webnhansu.phanquyendiemptngoaingu");
            List<TKDangNhap> TkDangNhap24 = DataproviderTKDangNhap.Instance.ExecuteQuery("INSERT INTO webnhansuclone.phanquyennhansu SELECT * FROM webnhansu.phanquyennhansu");
            List<TKDangNhap> TkDangNhap25 = DataproviderTKDangNhap.Instance.ExecuteQuery("INSERT INTO webnhansuclone.phanquyenptbt SELECT * FROM webnhansu.phanquyenptbt");
            List<TKDangNhap> TkDangNhap26 = DataproviderTKDangNhap.Instance.ExecuteQuery("INSERT INTO webnhansuclone.phanquyentklt SELECT * FROM webnhansu.phanquyentklt");
            List<TKDangNhap> TkDangNhap27 = DataproviderTKDangNhap.Instance.ExecuteQuery("INSERT INTO webnhansuclone.tkdangnhap SELECT * FROM webnhansu.tkdangnhap");
            List<TKDangNhap> TkDangNhap28 = DataproviderTKDangNhap.Instance.ExecuteQuery("INSERT INTO webnhansuclone.phanquyendiemptquantridoanhnghiep SELECT * FROM webnhansu.phanquyendiemptquantridoanhnghiep");

            // check 
            // xóa rồi nạp 
            List<ChiTietDuAn> TkDangNha1 = DataproviderChiTietDuAn.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.chitietduan");
            List<ChungChi> TkDangNha2 = DataproviderChungChi.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.chungchi");
            List<DiemDaoTao> TkDangNha3 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.diemptba");
            List<diemptbt> TkDangNha4 = Dataproviderdiemptbt.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.diemptbt");
            List<DiemDaoTao> TkDangNha5 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.diemptcokhi ");
            List<DiemDaoTao> TkDangNha6 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.diemptlaptrinh");
            List<DiemDaoTao> TkDangNha7 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.diemptngoaingu");
            List<DiemDaoTao> TkDangNha8 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.diemptptbt");
            List<DiemDaoTao> TkDangNha9 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.diemptquantridoanhnghiep");
            List<DiemDaoTao> TkDangNha10 = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.diempttudonghoa");
            List<diemythuc> TkDangNha11 = Dataproviderdiemythuc.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.diemythuc ");
            List<duan> TkDangNha12 = DataproviderDuan.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.`ds du an` ");
            List<LT> TkDangNha13 = DataproviderLT.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.lt ");
            List<MainTable> TkDangNha14 = DataproviderBangChinh.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.maintable ");
            List<TKDangNhap> TkDangNha15 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.phanquyenadmin ");
            List<TKDangNhap> TkDangNha16 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.phanquyenchutich");
            List<TKDangNhap> TkDangNha17 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.phanquyendaotao");
            List<TKDangNhap> TkDangNha18 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.phanquyendiemptba");
            List<TKDangNhap> TkDangNha19 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.phanquyendiemptcokhi");
            List<TKDangNhap> TkDangNha20 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.phanquyendiemptlaptrinh");
            List<TKDangNhap> TkDangNha21 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.phanquyendiempttudonghoa");
            List<TKDangNhap> TkDangNha22 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.phanquyendiemptptbt");
            List<TKDangNhap> TkDangNha23 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.phanquyendiemptngoaingu");
            List<TKDangNhap> TkDangNha24 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.phanquyennhansu");
            List<TKDangNhap> TkDangNha25 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.phanquyenptbt");
            List<TKDangNhap> TkDangNha26 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.phanquyentklt");
            List<TKDangNhap> TkDangNha27 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.tkdangnhap ");
            List<TKDangNhap> TkDangNha28 = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT* FROM  webnhansuclone.phanquyendiemptquantridoanhnghiep");
            // lưu lại dữ liệu phiên đăng nhập 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));
            var chinhsua = DataproviderSaveDataLogin.Instance.ExecuteQuery("INSERT INTO webnhansu.savedataedit VALUES(" + tkdangnhap.LabID + ",'" + DateTime.Now.ToString() + "','Tạo Database clone')");

            // điều kiện 
            if ((TkDangNha1.Count>0)&& (TkDangNha2. Count > 0) && (TkDangNha3.Count > 0) && (TkDangNha4.Count > 0) && (TkDangNha5.Count > 0) && (TkDangNha6.Count > 0) && (TkDangNha7.Count > 0) && (TkDangNha8.Count > 0) && (TkDangNha9.Count > 0) && (TkDangNha10.Count > 0) && (TkDangNha11.Count > 0) && (TkDangNha12.Count > 0) && (TkDangNha13.Count > 0) && (TkDangNha14.Count > 0) && (TkDangNha15.Count > 0) && (TkDangNha16.Count > 0) && (TkDangNha17.Count > 0) && (TkDangNha18.Count > 0) && (TkDangNha19.Count > 0) && (TkDangNha20.Count > 0) && (TkDangNha21.Count > 0) && (TkDangNha22.Count > 0) && (TkDangNha23.Count > 0) && (TkDangNha24.Count > 0) && (TkDangNha25.Count > 0) && (TkDangNha26.Count > 0) && (TkDangNha27.Count > 0) && (TkDangNha28.Count > 0) )
            {
                ViewBag.Name = "Tạo thành công";
            }
            else
            {
                ViewBag.Name = "Tạo thất bại ";
            }
            return View();
        }

        // show thông tin các bảng 
        public IActionResult xuatexcel()
        {
            FullBangViewModel result = new FullBangViewModel();
            // nạp dữ liệu cho các bảng quan trọng 

            // bảng chitiet dự án 

            result.chitietduan = DataproviderChiTietDuAn.Instance.ExecuteQuery("SELECT * FROM webnhansu.`chitietduan`");//
            // bảng chunchi 
            result.chungchi = DataproviderChungChi.Instance.ExecuteQuery("SELECT * FROM webnhansu.`chungchi`");//

            // bảng diemptba
            result.diemptba = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.`diemptba`");
            // bảng diemptbt 
            result.ptbt = Dataproviderdiemptbt.Instance.ExecuteQuery("SELECT * FROM webnhansu.`diemptbt`");
            // bảng Pt cơ khí 
            result.diemptcokhi = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.`diemptcokhi`");
            // ptlaptrinh 
            result.diemptlaptrinh = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.`diemptlaptrinh`");
            // pt ngoại ngữ 
            result.diemptngoaingu = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.`diemptngoaingu`");
            // pt ptbt 
            result.diemptptbt = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.`diemptptbt`");
            // pt quantri 
            result.diemptquantridoanhnghiep = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.`diemptquantridoanhnghiep`");
            // pt tu dong hoa 
            result.diempttudonghoa = Dataproviderdiemdaotao.Instance.ExecuteQuery("SELECT * FROM webnhansu.`diempttudonghoa`");
            // diem ythuc 
            result.ythuc = Dataproviderdiemythuc.Instance.ExecuteQuery("SELECT * FROM webnhansu.`diemythuc`");
            // ds su an 
            result.dsduan = DataproviderDuan.Instance.ExecuteQuery("SELECT * FROM webnhansu.`ds du an`");
            // quản lý LT 
            result.leader = DataproviderLT.Instance.ExecuteQuery("SELECT * FROM webnhansu.`lt`");
            // maintable
            result.bangchinh = DataproviderBangChinh.Instance.ExecuteQuery("SELECT * FROM webnhansu.`maintable`");//
            // phân quyền lt 
            result.phanquyenlt = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM webnhansu.`phanquyentklt`");
            // tài khoản đăng nhập 
            result.taikhoan = DataproviderTKDangNhap.Instance.ExecuteQuery("SELECT * FROM webnhansu.`tkdangnhap`");


            // xuất ra excel thôi 
            using (var workbook = new XLWorkbook())
            {
                // bảng chi tiết dự án 
                var worksheet1 = workbook.Worksheets.Add("ThongTinDuAn");
                var currentRow1 = 1;
               
                worksheet1.Cell(currentRow1, 1).Value = "ID";
                worksheet1.Cell(currentRow1, 2).Value = "LabID";
                worksheet1.Cell(currentRow1, 3).Value = "Tên";
                worksheet1.Cell(currentRow1, 4).Value = "Tên dự án";
                worksheet1.Cell(currentRow1, 5).Value = "Chức vụ";
                worksheet1.Cell(currentRow1, 6).Value = "Bắt đầu";
                worksheet1.Cell(currentRow1, 7).Value = "Kết thúc";
                worksheet1.Cell(currentRow1, 8).Value = "Đánh giá";

               
                foreach (var student1 in result.chitietduan)
                {
                    currentRow1++;
                    // chèn câu lệnh tìm chứng chỉ vào đây 
                    worksheet1.Cell(currentRow1, 1).Value = student1.ID;
                    worksheet1.Cell(currentRow1, 2).Value = student1.LabID;
                    worksheet1.Cell(currentRow1, 3).Value = student1.Ten;
                    worksheet1.Cell(currentRow1, 4).Value = student1.Tenduan;
                    worksheet1.Cell(currentRow1, 5).Value = student1.Chucvu;
                    worksheet1.Cell(currentRow1, 6).Value = student1.BatDau;
                    worksheet1.Cell(currentRow1, 7).Value = student1.KetThuc;
                    worksheet1.Cell(currentRow1, 8).Value = student1.Danhgia;

                }



                // bảng chi tiết dự án 
                var worksheet2 = workbook.Worksheets.Add("ChungChi");
                var currentRow2 = 1;
               
                worksheet2.Cell(currentRow2, 1).Value = "ID";
                worksheet2.Cell(currentRow2, 2).Value = "LabID";
                worksheet2.Cell(currentRow2, 3).Value = "Tên";
                worksheet2.Cell(currentRow2, 4).Value = "Tên Chứng Chỉ";
             

                
                foreach (var student1 in result.chungchi)
                {
                    currentRow2++;
                    // chèn câu lệnh tìm chứng chỉ vào đây 
                    worksheet2.Cell(currentRow2, 1).Value = student1.ID;
                    worksheet2.Cell(currentRow2, 2).Value = student1.LabID;
                    worksheet2.Cell(currentRow2, 3).Value = student1.Ten;
                    worksheet2.Cell(currentRow2, 4).Value = student1.Tenchungchi;
                    
                }




                // bảng chính 
                // bảng chi tiết dự án 
                var worksheet3 = workbook.Worksheets.Add("MainTable");
                var currentRow3 = 1;
                
                worksheet3.Cell(currentRow3, 1).Value = "LabID";
                worksheet3.Cell(currentRow3, 2).Value = "Tên";
                worksheet3.Cell(currentRow3, 3).Value = "Thế hệ";
                worksheet3.Cell(currentRow3, 4).Value = "SDT";
                worksheet3.Cell(currentRow3, 5).Value = "Ngành học";
                worksheet3.Cell(currentRow3, 6).Value = "Trường học";
                worksheet3.Cell(currentRow3, 7).Value = "email";
                worksheet3.Cell(currentRow3, 8).Value = "Quê quán ";
                worksheet3.Cell(currentRow3, 9).Value = "Trạng thái hoạt động ";
                
                foreach (var student1 in result.bangchinh)
                {
                    currentRow3++;
                    // chèn câu lệnh tìm chứng chỉ vào đây 
                    worksheet3.Cell(currentRow3, 1).Value = student1.LabID;
                    worksheet3.Cell(currentRow3, 2).Value = student1.Ten;
                    worksheet3.Cell(currentRow3, 3).Value = student1.TheHe;
                    worksheet3.Cell(currentRow3, 4).Value = student1.SDT;
                    worksheet3.Cell(currentRow3, 5).Value = student1.NganhHoc;
                    worksheet3.Cell(currentRow3, 6).Value = student1.TruongHoc;
                    worksheet3.Cell(currentRow3, 7).Value = student1.email;
                    worksheet3.Cell(currentRow3, 8).Value = student1.QueQuan;
                    worksheet3.Cell(currentRow3, 9).Value = student1.TrangThai;
                }


                // lt
                var worksheet4 = workbook.Worksheets.Add("LT");
                var currentRow4 = 1;
                // trỏ đến dòng 1 và cột 1 thay giá trị bằng LabID các dòng dưới cx tương tự
                worksheet4.Cell(currentRow4, 1).Value = "ID";
                worksheet4.Cell(currentRow4, 2).Value = "LabId";
                worksheet4.Cell(currentRow4, 3).Value = "Họ và Tên";
                worksheet4.Cell(currentRow4, 4).Value = "Chức Vụ";
                worksheet4.Cell(currentRow4, 5).Value = "Bắt đầu";
                worksheet4.Cell(currentRow4, 6).Value = "Kết thúc";
                worksheet4.Cell(currentRow4, 7).Value = "Đánh giá";

                
                foreach (var lt in result.leader)
                {
                    // Dòng thứ 2 trở đi sẽ đổ dữ liệu từ database vào
                    currentRow4 += 1;
                    //dòng 2 cột 1 điền lt.ID
                    worksheet4.Cell(currentRow4, 1).Value = lt.ID;
                    worksheet4.Cell(currentRow4, 2).Value = lt.LabID;
                    worksheet4.Cell(currentRow4, 3).Value = lt.Ten;
                    worksheet4.Cell(currentRow4, 4).Value = lt.ChucVu;
                    worksheet4.Cell(currentRow4, 5).Value = lt.BatDau;
                    worksheet4.Cell(currentRow4, 6).Value = lt.KetThuc;
                    worksheet4.Cell(currentRow4, 7).Value = lt.DanhGia;
                }



                // điểm PTBT 
                var worksheet5 = workbook.Worksheets.Add("DiemPTBT");
                var currentRow5= 1;
                
                worksheet5.Cell(currentRow5, 1).Value = "LabID";
                worksheet5.Cell(currentRow5, 2).Value = "Họ và tên";
                worksheet5.Cell(currentRow5, 3).Value = "Điểm PTBT";


               
                foreach (var student1 in result.ptbt)
                {
                    currentRow5++;
                    // chèn câu lệnh tìm chứng chỉ vào đây 
                    worksheet5.Cell(currentRow5, 1).Value = student1.LabID;
                    worksheet5.Cell(currentRow5, 2).Value = student1.Ten;
                    worksheet5.Cell(currentRow5, 3).Value = student1.DiemPTBT;
                }


                // điểm ý thức 
                var worksheet6 = workbook.Worksheets.Add("DiemYThuc");
                var currentRow6 = 1;

                worksheet6.Cell(currentRow6, 1).Value = "LabID";
                worksheet6.Cell(currentRow6, 2).Value = "Họ và tên";
                worksheet6.Cell(currentRow6, 3).Value = "Điểm Ý Thức";



                foreach (var student1 in result.ythuc)
                {
                    currentRow6++;
                    // chèn câu lệnh tìm chứng chỉ vào đây 
                    worksheet6.Cell(currentRow6, 1).Value = student1.LabID;
                    worksheet6.Cell(currentRow6, 2).Value = student1.Ten;
                    worksheet6.Cell(currentRow6, 3).Value = student1.DiemYThuc;
                }



                // PT Lập Trình 
                var worksheet7 = workbook.Worksheets.Add("DiemPTLapTrinh");
                var currentRow7 = 1;

                worksheet7.Cell(currentRow7, 1).Value = "LabID";
                worksheet7.Cell(currentRow7, 2).Value = "Họ và tên";
                worksheet7.Cell(currentRow7, 3).Value = "Điểm";



                foreach (var student1 in result.diemptlaptrinh)
                {
                    currentRow7++;
                    // chèn câu lệnh tìm chứng chỉ vào đây 
                    worksheet7.Cell(currentRow7, 1).Value = student1.LabID;
                    worksheet7.Cell(currentRow7, 2).Value = student1.Ten;
                    worksheet7.Cell(currentRow7, 3).Value = student1.Diem;
                }



                // PT BA 
                var worksheet8= workbook.Worksheets.Add("DiemPTBA");
                var currentRow8 = 1;

                worksheet8.Cell(currentRow8, 1).Value = "LabID";
                worksheet8.Cell(currentRow8, 2).Value = "Họ và tên";
                worksheet8.Cell(currentRow8, 3).Value = "Điểm";



                foreach (var student1 in result.diemptba)
                {
                    currentRow8++;
                    // chèn câu lệnh tìm chứng chỉ vào đây 
                    worksheet8.Cell(currentRow8, 1).Value = student1.LabID;
                    worksheet8.Cell(currentRow8, 2).Value = student1.Ten;
                    worksheet8.Cell(currentRow8, 3).Value = student1.Diem;
                }


                //PT Co khi 
              
                var worksheet9 = workbook.Worksheets.Add("DiemCoKhi");
                var currentRow9 = 1;

                worksheet9.Cell(currentRow9, 1).Value = "LabID";
                worksheet9.Cell(currentRow9, 2).Value = "Họ và tên";
                worksheet9.Cell(currentRow9, 3).Value = "Điểm";



                foreach (var student1 in result.diemptcokhi)
                {
                    currentRow9++;
                    // chèn câu lệnh tìm chứng chỉ vào đây 
                    worksheet9.Cell(currentRow9, 1).Value = student1.LabID;
                    worksheet9.Cell(currentRow9, 2).Value = student1.Ten;
                    worksheet9.Cell(currentRow9, 3).Value = student1.Diem;
                }




                // PT ptbt 
                var worksheet10 = workbook.Worksheets.Add("DiemPT PTBT ");
                var currentRow10 = 1;

                worksheet10.Cell(currentRow10, 1).Value = "LabID";
                worksheet10.Cell(currentRow10, 2).Value = "Họ và tên";
                worksheet10.Cell(currentRow10, 3).Value = "Điểm";



                foreach (var student1 in result.diemptptbt)
                {
                    currentRow10++;
                    // chèn câu lệnh tìm chứng chỉ vào đây 
                    worksheet10.Cell(currentRow10, 1).Value = student1.LabID;
                    worksheet10.Cell(currentRow10, 2).Value = student1.Ten;
                    worksheet10.Cell(currentRow10, 3).Value = student1.Diem;
                }



                // pt quản trị 
                var worksheet11= workbook.Worksheets.Add("DiemPT quản trị doanh nghiệp ");
                var currentRow11= 1;

                worksheet11.Cell(currentRow11, 1).Value = "LabID";
                worksheet11.Cell(currentRow11, 2).Value = "Họ và tên";
                worksheet11.Cell(currentRow11, 3).Value = "Điểm";



                foreach (var student1 in result.diemptquantridoanhnghiep)
                {
                    currentRow11++;
                    // chèn câu lệnh tìm chứng chỉ vào đây 
                    worksheet11.Cell(currentRow11, 1).Value = student1.LabID;
                    worksheet11.Cell(currentRow11, 2).Value = student1.Ten;
                    worksheet11.Cell(currentRow11, 3).Value = student1.Diem;
                }


                // pt tudong hoa 
                var worksheet12 = workbook.Worksheets.Add("DiemPT tự động hóa");
                var currentRow12 = 1;

                worksheet12.Cell(currentRow12, 1).Value = "LabID";
                worksheet12.Cell(currentRow12, 2).Value = "Họ và tên";
                worksheet12.Cell(currentRow12, 3).Value = "Điểm";



                foreach (var student1 in result.diempttudonghoa)
                {
                    currentRow12++;
                    // chèn câu lệnh tìm chứng chỉ vào đây 
                    worksheet12.Cell(currentRow12, 1).Value = student1.LabID;
                    worksheet12.Cell(currentRow12, 2).Value = student1.Ten;
                    worksheet12.Cell(currentRow12, 3).Value = student1.Diem;
                }




                // pt ngoại ngữ 
                var worksheet13 = workbook.Worksheets.Add("Diem PT tự động hóa");
                var currentRow13= 1;

                worksheet13.Cell(currentRow13, 1).Value = "LabID";
                worksheet13.Cell(currentRow13, 2).Value = "Họ và tên";
                worksheet13.Cell(currentRow13, 3).Value = "Điểm";



                foreach (var student1 in result.diemptngoaingu)
                {
                    currentRow13++;
                    // chèn câu lệnh tìm chứng chỉ vào đây 
                    worksheet13.Cell(currentRow13, 1).Value = student1.LabID;
                    worksheet13.Cell(currentRow13, 2).Value = student1.Ten;
                    worksheet13.Cell(currentRow13, 3).Value = student1.Diem;
                }


                // pt lập trình 
                var worksheet14 = workbook.Worksheets.Add("Diem PT lập trình");
                var currentRow14 = 1;

                worksheet14.Cell(currentRow14, 1).Value = "LabID";
                worksheet14.Cell(currentRow14, 2).Value = "Họ và tên";
                worksheet14.Cell(currentRow14, 3).Value = "Điểm";



                foreach (var student1 in result.diemptlaptrinh)
                {
                    currentRow14++;
                    // chèn câu lệnh tìm chứng chỉ vào đây 
                    worksheet14.Cell(currentRow14, 1).Value = student1.LabID;
                    worksheet14.Cell(currentRow14, 2).Value = student1.Ten;
                    worksheet14.Cell(currentRow14, 3).Value = student1.Diem;
                }



                ////////////////////////////////////////////////////////////////

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



    }
}
