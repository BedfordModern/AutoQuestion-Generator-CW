#pragma checksum "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\User\Login.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "241f30529f3ce3ba429d0cf299df70b5f63631ab"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Login), @"mvc.1.0.view", @"/Views/User/Login.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/User/Login.cshtml", typeof(AspNetCore.Views_User_Login))]
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
#line 1 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\_ViewImports.cshtml"
using AutoQuestionGenerator;

#line default
#line hidden
#line 2 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\_ViewImports.cshtml"
using AutoQuestionGenerator.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"241f30529f3ce3ba429d0cf299df70b5f63631ab", @"/Views/User/Login.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ab11deb0466ec5e2e96aca1e14ec8a38233b3dfd", @"/Views/_ViewImports.cshtml")]
    public class Views_User_Login : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LoginViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\User\Login.cshtml"
  
    ViewData["Title"] = "Login";

#line default
#line hidden
            BeginContext(64, 34, true);
            WriteLiteral("\r\n<h2>Login</h2>\r\n<br />\r\n<br />\r\n");
            EndContext();
#line 9 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\User\Login.cshtml"
 using (Html.BeginForm())
{

#line default
#line hidden
            BeginContext(128, 142, true);
            WriteLiteral("    <div class=\"row\">\r\n        <div class=\"col-md-1\">\r\n            Organisation:\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n            ");
            EndContext();
            BeginContext(271, 69, false);
#line 16 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\User\Login.cshtml"
       Write(Html.TextBoxFor(x => x.Organisation, new { @class = "form-control" }));

#line default
#line hidden
            EndContext();
            BeginContext(340, 150, true);
            WriteLiteral("\r\n        </div>\r\n    </div><br />\r\n    <div class=\"row\">\r\n        <div class=\"col-md-1\">\r\n            Username:\r\n        </div><div class=\"col-md-3\">");
            EndContext();
            BeginContext(491, 65, false);
#line 22 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\User\Login.cshtml"
                               Write(Html.TextBoxFor(m => m.Username, new { @class = "form-control" }));

#line default
#line hidden
            EndContext();
            BeginContext(556, 148, true);
            WriteLiteral("</div>\r\n    </div>\r\n    <div class=\"row\">\r\n        <div class=\"col-md-1\">\r\n            Password:\r\n        </div><div class=\"col-md-3\">\r\n            ");
            EndContext();
            BeginContext(705, 84, false);
#line 28 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\User\Login.cshtml"
       Write(Html.TextBoxFor(m => m.Password, new { type = "password", @class = "form-control" }));

#line default
#line hidden
            EndContext();
            BeginContext(789, 36, true);
            WriteLiteral("\r\n        </div>\r\n    </div><br />\r\n");
            EndContext();
            BeginContext(827, 67, true);
            WriteLiteral("    <button type=\"submit\" class=\"=btn btn-primary\">Login</button>\r\n");
            EndContext();
#line 33 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\User\Login.cshtml"
    if (Model != null && !string.IsNullOrEmpty(Model.Error))
    {

#line default
#line hidden
            BeginContext(963, 45, true);
            WriteLiteral("        <br />\r\n        <p style=\"color:red\">");
            EndContext();
            BeginContext(1009, 11, false);
#line 36 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\User\Login.cshtml"
                        Write(Model.Error);

#line default
#line hidden
            EndContext();
            BeginContext(1020, 6, true);
            WriteLiteral("</p>\r\n");
            EndContext();
#line 37 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\User\Login.cshtml"
    }
}

#line default
#line hidden
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LoginViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
