#pragma checksum "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a26b9e6b421dc8ace8d03ece3898d44afe0d5e81"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_TkUser_Index), @"mvc.1.0.view", @"/Views/TkUser/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\_ViewImports.cshtml"
using Điểm_Đào_Tạo;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\_ViewImports.cshtml"
using Điểm_Đào_Tạo.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a26b9e6b421dc8ace8d03ece3898d44afe0d5e81", @"/Views/TkUser/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"efbe8d1151b71cbb94f360cbe4ec20846dadbe63", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_TkUser_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<HienThiThongTinNguoiDungTkUser>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
  
   
    Layout = "~/Views/Shared/layoutTkUser.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
 <style>

#tendangnhap{
    text-align: center;
    margin-left: 450px;
}
.form-group{
   
    
    margin-top:10px;

}
.btn btn-default{
    color: aqua;
}
.formlogin{
    margin-top:100px;
    margin-left:450px;}
.abc{
margin-top:200px;
margin-left:450px;
}
.giua{
color:black;
margin-left:20px}
.abc1{
    width: 400px;
    height:30px;
}
</style>
<h1 style=""text-align:center;margin-top:100px;"">");
#nullable restore
#line 37 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
                                           Write(ViewBag.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h1>\r\n\r\n\r\n     <!-- show dữ liệu-->\r\n");
#nullable restore
#line 41 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
      if(Model!=null){
     

#line default
#line hidden
#nullable disable
#nullable restore
#line 42 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
      if(Model.Count() > 0)
 {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 44 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
         foreach (var member in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <hr />\r\n           <table style=\"width:800px;margin:auto;margin-top:100px\">\r\n    <tr> \r\n        <td><img");
            BeginWriteAttribute("src", " src=\"", 816, "\"", 835, 1);
#nullable restore
#line 49 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
WriteAttributeValue("", 822, member.Image, 822, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"height:200px;width:200px\"/></td>\r\n        \r\n    </tr>\r\n    <tr> \r\n        <td>LabID:</td>\r\n        <td>");
#nullable restore
#line 54 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
       Write(member.LabID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n    </tr>\r\n    <tr>\r\n        <td>Họ và tên:</td>\r\n        <td>");
#nullable restore
#line 58 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
       Write(member.Ten);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n    </tr>\r\n    <tr>\r\n        <td>Thế hệ:</td>\r\n        <td>");
#nullable restore
#line 62 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
       Write(member.TheHe);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n    </tr>\r\n    <tr>\r\n        <td>Số điện thoại:</td>\r\n        <td>");
#nullable restore
#line 66 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
       Write(member.SDT);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n    </tr>\r\n    <tr>\r\n        <td>Ngành học:</td>\r\n        <td>");
#nullable restore
#line 70 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
       Write(member.NganhHoc);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n    </tr>\r\n    <tr>\r\n        <td>Trường học:</td>\r\n        <td>");
#nullable restore
#line 74 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
       Write(member.TruongHoc);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n    </tr>\r\n    <tr>\r\n        <td>Email:</td>\r\n        <td>");
#nullable restore
#line 78 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
       Write(member.email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n    </tr>\r\n    <tr>\r\n        <td>Quê quán:</td>\r\n        <td>");
#nullable restore
#line 82 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
       Write(member.QueQuan);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n    </tr>\r\n    <tr>\r\n        <td>Chứng chỉ:</td>\r\n        <td>");
#nullable restore
#line 86 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
       Write(member.Chungchi);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n    </tr>\r\n    <tr>\r\n        <td>PowerTeam:</td>\r\n        <td>");
#nullable restore
#line 90 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
       Write(member.PowerTeam);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n    </tr>\r\n    <tr>\r\n        <td>Điểm Đào Tạo:</td>\r\n        <td>");
#nullable restore
#line 94 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
       Write(member.Diem);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n    </tr>\r\n    <tr>\r\n        <td>Điêm Phát Triển Bản Thân:</td>\r\n        <td>");
#nullable restore
#line 98 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
       Write(member.DiemPTBT);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n    </tr>\r\n    <tr>\r\n        <td>Điểm Ý Thức:</td>\r\n        <td>");
#nullable restore
#line 102 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
       Write(member.DiemYThuc);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n    </tr>\r\n</table>\r\n");
#nullable restore
#line 105 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 105 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
         
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 106 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\TkUser\Index.cshtml"
     
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    ");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<HienThiThongTinNguoiDungTkUser>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
