#pragma checksum "K:\dev\projects\MVCGrid.Net Core Example\Views\Example\Basic.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "ac2b32371a8970d4da30363266a7e3e03c3701980dc5fb572f3c84ce59f0e16e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Example_Basic), @"mvc.1.0.razor-page", @"/Views/Example/Basic.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Views/Example/Basic.cshtml", typeof(AspNetCore.Views_Example_Basic), null)]
namespace AspNetCore
{
    #line default
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 5 "K:\dev\projects\MVCGrid.Net Core Example\Views\Example\Basic.cshtml"
 using MVCGrid.NetCore.Web

    ;
    #line default
    #line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"ac2b32371a8970d4da30363266a7e3e03c3701980dc5fb572f3c84ce59f0e16e", @"/Views/Example/Basic.cshtml")]
    public class Views_Example_Basic : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(100, 434, true);
            WriteLiteral(@"
<link rel=""stylesheet"" href=""https://stackpath.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css"" integrity=""sha384-HSMxcRTRxnN+Bdg0JdbxYKrThecOKuH5zCYotlSAcp1+c8xmyTe9GYg1l9a69psu"" crossorigin=""anonymous"">
<link rel=""stylesheet"" href=""https://stackpath.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap-theme.min.css"" integrity=""sha384-6pzBo3FDv/PJ8r2KRkGHifhEocL+1X2rVCTTkUfGk7/0pbek5mMa1upzvWbrUbOZ"" crossorigin=""anonymous"">
");
            EndContext();
            BeginContext(742, 227, true);
            WriteLiteral("<script src=\"https://code.jquery.com/jquery-3.4.1.min.js\"\r\n        integrity=\"sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=\"\r\n        crossorigin=\"anonymous\"></script>\r\n<script src=\"/MVCGrid.js\"></script>\r\n\r\n<div>\r\n\r\n    ");
            EndContext();
            BeginContext(970, 31, false);
            Write(
#line 17 "K:\dev\projects\MVCGrid.Net Core Example\Views\Example\Basic.cshtml"
     Html.MVCGridNetCore("TestGrid")

#line default
#line hidden
            );
            EndContext();
            BeginContext(1001, 10, true);
            WriteLiteral("\r\n\r\n</div>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MVCGrid.Net_Core_Example.Views.Example.IndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<MVCGrid.Net_Core_Example.Views.Example.IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<MVCGrid.Net_Core_Example.Views.Example.IndexModel>)PageContext?.ViewData;
        public MVCGrid.Net_Core_Example.Views.Example.IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
