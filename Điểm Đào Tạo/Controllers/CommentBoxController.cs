using Điểm_Đào_Tạo.DAO;
using Điểm_Đào_Tạo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Điểm_Đào_Tạo.Controllers
{
    public class CommentBoxController : Controller
    {
        // truyền data
        public IActionResult Index()
        {
            List<CommentBox> TkDangNhaps = DataproviderCommentBox.Instance.ExecuteQuery("SELECT * FROM webnhansu.commentbox");
            // đảo ngược 
            List<CommentBox> TkDangNhaps2 = new List<CommentBox>();
            for (int i = TkDangNhaps.Count - 1; i > -1; i--)
            {
                TkDangNhaps2.Add(TkDangNhaps[i]);
            }
            return View(TkDangNhaps2);
        }




        // viết bình luận 
        [HttpPost]
        public IActionResult WriteComment(string comment)
        {   // set Id 
            List<CommentBox> data_info = DataproviderCommentBox.Instance.ExecuteQuery("SELECT * FROM webnhansu.commentbox");
            List<int> ListID = new List<int>();
            foreach (var chungchi in data_info)
            {
                ListID.Add(chungchi.ID);
            }
            int CurrentID = 1;  // if normal CurrentID=1 
            if (ListID.Count>0)
            {
                CurrentID = (ListID.Max() + 1); // Lấy ID lớn nhất và add vào ID + 1
            }
            // lấy thông tin đăng nhập 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));

            // lấy dữ liệu từ bang chinh 
            var members = DataproviderBangChinh.Instance.ExecuteQuery($"select * from webnhansu.maintable where LabID = {tkdangnhap.LabID}");

            // trèn dữ liệu xuống database 
            var members1 = DataproviderCommentBox.Instance.ExecuteQuery("INSERT INTO webnhansu.commentbox VALUE ("+ CurrentID + ","+tkdangnhap.LabID+",'"+members[0].Ten+ "','" + members[0].Image+ "','" + comment + "','"+DateTime.Now.ToString()+"')");
            return RedirectToAction("Index", "CommentBox");
        }




        // deletecomment
        // chỉ LT hoặc người viết ra được xóa 
        public IActionResult deletecomment()
        {
            // lấy id được truyền vào 
            var reqUrl = Request.HttpContext.Request;
            var urlPath = reqUrl.Path; /// Path: Home/EditLT/ID
            var CurrentID = urlPath.ToString().Split('/').Last(); /// Lấy LabID hiện tại thông qua tên miền

            // lấy comment dưới database lên 
            List<CommentBox> data_info = DataproviderCommentBox.Instance.ExecuteQuery("SELECT * FROM webnhansu.commentbox WHERE `ID`="+ CurrentID + "");

            // Lấy thông tin đăng nhập 
            var tkdangnhap = JsonConvert.DeserializeObject<TKDangNhap>(HttpContext.Session.GetString("DangNhapSession"));

            // kiểm tra xem thằng LabID có trong bảng LT Không 
            List<LT> query_LT = DataproviderLT.Instance.ExecuteQuery($"select * from webnhansu.lt where LabID = {data_info[0].LabID} ");

            if (query_LT.Count > 0)
            {
                List<CommentBox> data_info2 = DataproviderCommentBox.Instance.ExecuteQuery("DELETE FROM webnhansu.commentbox WHERE `ID`=" + CurrentID + "");
            }
            else if(tkdangnhap.LabID== data_info[0].LabID)
            {
                List<CommentBox> data_info3 = DataproviderCommentBox.Instance.ExecuteQuery("DELETE FROM webnhansu.commentbox WHERE `ID`=" + CurrentID + "");
            }
            return RedirectToAction("Index", "CommentBox");
        }


    }
}
