#pragma checksum "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\DiemPTBT\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "62be750f129e1a131ec79f812d621de27975b8dc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DiemPTBT_Index), @"mvc.1.0.view", @"/Views/DiemPTBT/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"62be750f129e1a131ec79f812d621de27975b8dc", @"/Views/DiemPTBT/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"efbe8d1151b71cbb94f360cbe4ec20846dadbe63", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_DiemPTBT_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<diemptbt>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "xuatfilexdiemptbt", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "DiemPTBT", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("margin-top:30px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "capnhatdiemptbtbangexcel", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "timkiemdiemptbt", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "sapxepdiemptbt", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "timdiemptbttheokhoang", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "EditDiemPTBTlink", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral(@"  <h1 style=""text-align:center;color:#808080;margin-top:50px"">Bảng Điểm Phát triển bản thân</h1>
<style>
    .container_swap{
  
  width: 1300px;
  margin:auto;
  margin-top: 100px;
  
  }
  
  .div_chinh{
  
  width: 900px;
  
  float: left;
  
  text-align: center;
  margin:auto;
  
  }
  .div_chinhle{
  
  width: 30px;
  
  float: left;
  
  text-align: center;
  margin:auto;
  
  }
  
  .div_le{
  
  width: 70px;
  
  float: left;
  
  text-align: center;
  margin:auto;
  
  }
  .div_phu{
  
  width: 300px;
  
  float: left;
  
  text-align: center;
  margin:auto;
  background-color:#DCDCDC;
  }

</style>
<div class=""container_swap"">
  
    <div class=""div_le"">.</div>
    <div class=""div_phu"">
        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "62be750f129e1a131ec79f812d621de27975b8dc6903", async() => {
                WriteLiteral("Xuất Điểm Phát triển bản thân Excel ");
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
            WriteLiteral("\r\n         <hr  style=\"width:70%;text-align:center;\" />\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "62be750f129e1a131ec79f812d621de27975b8dc8527", async() => {
                WriteLiteral("Cập nhật  Điểm Phát triển bản thân bằng Excel ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        <hr  style=\"width:70%;text-align:center;\" />\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "62be750f129e1a131ec79f812d621de27975b8dc10077", async() => {
                WriteLiteral("Tim kiếm Điểm Phát triển bản thân  ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        <hr  style=\"width:70%;text-align:center;\" />\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "62be750f129e1a131ec79f812d621de27975b8dc11617", async() => {
                WriteLiteral("Sắp xếp  Điểm Phát triển bản thân  ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n         <hr  style=\"width:70%;text-align:center;\" />\r\n         ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "62be750f129e1a131ec79f812d621de27975b8dc13159", async() => {
                WriteLiteral("Tìm Điểm Phát triển bản thân Theo Khoảng ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        <p style=\"line-height:1000px\">.</p>\r\n        <hr  style=\"width:70%;text-align:center;line-height:1000px\" />\r\n    </div>\r\n    <div class=\"div_chinhle\">.</div>\r\n\r\n\r\n    <div class=\"div_chinh\">\r\n");
#nullable restore
#line 75 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\DiemPTBT\Index.cshtml"
      if (Model != null)
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 77 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\DiemPTBT\Index.cshtml"
         if (Model.Count() > 0)
        {  
             

#line default
#line hidden
#nullable disable
            WriteLiteral(@"   <table style=""border-collapse:collapse;width:1000px; margin:auto; border:1px solid black; border-spacing:10px;background-color: #E6E6FA"">
    <thead >
       <tr>
                     <th style=""border:1px solid black;padding:10px;"">LabID </th>
                      <th style=""border:1px solid black;padding:10px;"">Tên </th>
                     <th style=""border:1px solid black;padding:10px;"">Điểm PTBT </th>
                     <th style=""border:1px solid black;padding:10px;"">Chỉnh sửa  </th>
       </tr>
    </thead>
    <tbody>
        
");
#nullable restore
#line 91 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\DiemPTBT\Index.cshtml"
              foreach (var item in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("<tr>\r\n            <td style=\"border:1px solid black; text-align: center;padding:10px;background-color: #B0E0E6\">");
#nullable restore
#line 93 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\DiemPTBT\Index.cshtml"
                                                                                                     Write(item.LabID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td style=\"border:1px solid black;text-align: center;padding:10px;background-color: #B0E0E6\">");
#nullable restore
#line 94 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\DiemPTBT\Index.cshtml"
                                                                                                    Write(item.Ten);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td style=\"border:1px solid black;text-align: center;padding:10px;background-color: #B0E0E6\">");
#nullable restore
#line 95 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\DiemPTBT\Index.cshtml"
                                                                                                    Write(item.DiemPTBT);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "62be750f129e1a131ec79f812d621de27975b8dc17656", async() => {
                WriteLiteral("Edit");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 96 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\DiemPTBT\Index.cshtml"
                                                   WriteLiteral(item.LabID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" </td>\r\n                        \r\n                    \r\n                     </tr>");
#nullable restore
#line 99 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\DiemPTBT\Index.cshtml"
                          }

#line default
#line hidden
#nullable disable
            WriteLiteral("            \r\n       \r\n       \r\n    </tbody>\r\n </table>\r\n");
#nullable restore
#line 105 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\DiemPTBT\Index.cshtml"
}

#line default
#line hidden
#nullable disable
#nullable restore
#line 105 "D:\D\Hệ thống các dự án\Web quản lý nhân sự Lab\Project  tổng\Full Code\Điểm Đào Tạo\Views\DiemPTBT\Index.cshtml"
 }

#line default
#line hidden
#nullable disable
            WriteLiteral("  </div>\r\n </div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<diemptbt>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
