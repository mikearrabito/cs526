#pragma checksum "/Users/michael/dev/school/cs526/assignment_3/Assignment3-Code/ImageSharingWIthSecurity/ImageSharingWIthSecurity/Views/Images/Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "238b14d68a29a01203cb43bf9c777c8edb1a58fa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Images_Details), @"mvc.1.0.view", @"/Views/Images/Details.cshtml")]
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
#line 1 "/Users/michael/dev/school/cs526/assignment_3/Assignment3-Code/ImageSharingWIthSecurity/ImageSharingWIthSecurity/Views/_ViewImports.cshtml"
using ImageSharingWithSecurity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/michael/dev/school/cs526/assignment_3/Assignment3-Code/ImageSharingWIthSecurity/ImageSharingWIthSecurity/Views/_ViewImports.cshtml"
using ImageSharingWithSecurity.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/Users/michael/dev/school/cs526/assignment_3/Assignment3-Code/ImageSharingWIthSecurity/ImageSharingWIthSecurity/Views/Images/Details.cshtml"
using ImageSharingWithSecurity.Controllers;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"238b14d68a29a01203cb43bf9c777c8edb1a58fa", @"/Views/Images/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2866ac68f4b151f85647edc6a447cdf66bd31a17", @"/Views/_ViewImports.cshtml")]
    public class Views_Images_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ImageSharingWithSecurity.Models.ImageView>
    {
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 5 "/Users/michael/dev/school/cs526/assignment_3/Assignment3-Code/ImageSharingWIthSecurity/ImageSharingWIthSecurity/Views/Images/Details.cshtml"
  
    ViewBag.Title = "Image Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2>");
#nullable restore
#line 9 "/Users/michael/dev/school/cs526/assignment_3/Assignment3-Code/ImageSharingWIthSecurity/ImageSharingWIthSecurity/Views/Images/Details.cshtml"
Write(ViewBag.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n\r\n<p>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "238b14d68a29a01203cb43bf9c777c8edb1a58fa4154", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 186, "~/", 186, 2, true);
#nullable restore
#line 11 "/Users/michael/dev/school/cs526/assignment_3/Assignment3-Code/ImageSharingWIthSecurity/ImageSharingWIthSecurity/Views/Images/Details.cshtml"
AddHtmlAttributeValue("", 188, ImagesController.imageContextPath(Model.Id), 188, 46, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</p>\r\n\r\n<p>Image id: ");
#nullable restore
#line 13 "/Users/michael/dev/school/cs526/assignment_3/Assignment3-Code/ImageSharingWIthSecurity/ImageSharingWIthSecurity/Views/Images/Details.cshtml"
        Write(Model.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n\r\n<p>Tag: ");
#nullable restore
#line 15 "/Users/michael/dev/school/cs526/assignment_3/Assignment3-Code/ImageSharingWIthSecurity/ImageSharingWIthSecurity/Views/Images/Details.cshtml"
   Write(Model.TagName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n\r\n<p>Caption: ");
#nullable restore
#line 17 "/Users/michael/dev/school/cs526/assignment_3/Assignment3-Code/ImageSharingWIthSecurity/ImageSharingWIthSecurity/Views/Images/Details.cshtml"
       Write(Model.Caption);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n\r\n<p>Description: <br />");
#nullable restore
#line 19 "/Users/michael/dev/school/cs526/assignment_3/Assignment3-Code/ImageSharingWIthSecurity/ImageSharingWIthSecurity/Views/Images/Details.cshtml"
                 Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n\r\n<p>Date taken: ");
#nullable restore
#line 21 "/Users/michael/dev/school/cs526/assignment_3/Assignment3-Code/ImageSharingWIthSecurity/ImageSharingWIthSecurity/Views/Images/Details.cshtml"
          Write(Model.DateTaken.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n\r\n<p>Uploader: ");
#nullable restore
#line 23 "/Users/michael/dev/school/cs526/assignment_3/Assignment3-Code/ImageSharingWIthSecurity/ImageSharingWIthSecurity/Views/Images/Details.cshtml"
        Write(Model.Username);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ImageSharingWithSecurity.Models.ImageView> Html { get; private set; }
    }
}
#pragma warning restore 1591
