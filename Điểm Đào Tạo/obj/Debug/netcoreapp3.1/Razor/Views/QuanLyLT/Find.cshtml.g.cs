#pragma checksum "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\QuanLyLT\Find.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "eb136cb7620ef472db887a24ffbcc37ace80d2b7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_QuanLyLT_Find), @"mvc.1.0.view", @"/Views/QuanLyLT/Find.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eb136cb7620ef472db887a24ffbcc37ace80d2b7", @"/Views/QuanLyLT/Find.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"efbe8d1151b71cbb94f360cbe4ec20846dadbe63", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_QuanLyLT_Find : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<LT>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "TkSuper", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btnlogin"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("text-decoration: none;color:white"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\QuanLyLT\Find.cshtml"
 if (Model != null)
{
    if (Model.Count() > 0)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <h1 style=""text-align:center;color:#808080"">Danh sách thông tin tìm được </h1>
        <table class=""table table-bordered"" style=""text-align:center;margin-top:50px;margin-left:auto;margin-right:auto"" width=""60%"" border=""1"" cellspacing=""0"">
            <tr>
                <th>LabID</th>
                <th>Tên</th>
                <th>Chức vụ</th>
                <th>Bắt đầu</th>
                <th>Kết thúc</th>
                <th>Đánh giá</th>
            </tr>
");
#nullable restore
#line 17 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\QuanLyLT\Find.cshtml"
             foreach (var d in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>");
#nullable restore
#line 20 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\QuanLyLT\Find.cshtml"
                   Write(d.LabID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 21 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\QuanLyLT\Find.cshtml"
                   Write(d.Ten);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 22 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\QuanLyLT\Find.cshtml"
                   Write(d.ChucVu);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 23 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\QuanLyLT\Find.cshtml"
                   Write(d.BatDau);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 24 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\QuanLyLT\Find.cshtml"
                   Write(d.KetThuc);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 25 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\QuanLyLT\Find.cshtml"
                   Write(d.DanhGia);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n");
#nullable restore
#line 27 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\QuanLyLT\Find.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </table>\r\n");
#nullable restore
#line 29 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\QuanLyLT\Find.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <h1 style=\"text-align:center;color:#808080\">Không có thông tin </h1>\r\n");
#nullable restore
#line 33 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\QuanLyLT\Find.cshtml"
    }
}

#line default
#line hidden
#nullable disable
            WriteLiteral("<button style=\"text-align:center;margin-top:25px;margin-left:600px;background-color:#6495ED;width:300px;height:50px;color:white;border-radius: 4px;\">  \r\n         ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "eb136cb7620ef472db887a24ffbcc37ace80d2b78407", async() => {
                WriteLiteral("HomePage");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n     </button>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<LT>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
