#pragma checksum "/Users/michael/dev/cs526/assignment_3/Assignment3-Code/ImageSharingWIthSecurity/ImageSharingWIthSecurity/Views/Home/Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "45c60503d71074f3c84c71f53fdc4ae784fc3e96"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Error), @"mvc.1.0.view", @"/Views/Home/Error.cshtml")]
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
#line 1 "/Users/michael/dev/cs526/assignment_3/Assignment3-Code/ImageSharingWIthSecurity/ImageSharingWIthSecurity/Views/_ViewImports.cshtml"
using ImageSharingWithSecurity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/michael/dev/cs526/assignment_3/Assignment3-Code/ImageSharingWIthSecurity/ImageSharingWIthSecurity/Views/_ViewImports.cshtml"
using ImageSharingWithSecurity.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"45c60503d71074f3c84c71f53fdc4ae784fc3e96", @"/Views/Home/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2866ac68f4b151f85647edc6a447cdf66bd31a17", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ImageSharingWithSecurity.Models.ErrorViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/Users/michael/dev/cs526/assignment_3/Assignment3-Code/ImageSharingWIthSecurity/ImageSharingWIthSecurity/Views/Home/Error.cshtml"
  
    ViewData["Title"] = "Error";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<h1 class=\"text-danger\">Error.</h1>\r\n<h2 class=\"text-danger\">An error occurred while processing your request.</h2>\r\n\r\n");
#nullable restore
#line 10 "/Users/michael/dev/cs526/assignment_3/Assignment3-Code/ImageSharingWIthSecurity/ImageSharingWIthSecurity/Views/Home/Error.cshtml"
 if (Model.ShowRequestId)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>\r\n        <strong>Request ID:</strong> <code>");
#nullable restore
#line 13 "/Users/michael/dev/cs526/assignment_3/Assignment3-Code/ImageSharingWIthSecurity/ImageSharingWIthSecurity/Views/Home/Error.cshtml"
                                      Write(Model.RequestId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</code>\r\n    </p>\r\n");
#nullable restore
#line 15 "/Users/michael/dev/cs526/assignment_3/Assignment3-Code/ImageSharingWIthSecurity/ImageSharingWIthSecurity/Views/Home/Error.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h3>Development Mode</h3>\r\n<p>\r\n    Error Message: ");
#nullable restore
#line 19 "/Users/michael/dev/cs526/assignment_3/Assignment3-Code/ImageSharingWIthSecurity/ImageSharingWIthSecurity/Views/Home/Error.cshtml"
              Write(Model.ErrId);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
</p>
<p>
    <strong>Development environment should not be enabled in deployed applications</strong>, as it can result in sensitive information from exceptions being displayed to end users. For local debugging, development environment can be enabled by setting the <strong>ASPNETCORE_ENVIRONMENT</strong> environment variable to <strong>Development</strong>, and restarting the application.
</p>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ImageSharingWithSecurity.Models.ErrorViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
