#pragma checksum "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\User\Lost.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1058c04e413e492e3b51457790b42a1369885a5b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Lost), @"mvc.1.0.view", @"/Views/User/Lost.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/User/Lost.cshtml", typeof(AspNetCore.Views_User_Lost))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1058c04e413e492e3b51457790b42a1369885a5b", @"/Views/User/Lost.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ab11deb0466ec5e2e96aca1e14ec8a38233b3dfd", @"/Views/_ViewImports.cshtml")]
    public class Views_User_Lost : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AutoQuestionGenerator.DatabaseModels.Users>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\User\Lost.cshtml"
  
    ViewData["Title"] = "Lost";

#line default
#line hidden
            BeginContext(91, 25, true);
            WriteLiteral("\r\n<h2>Lost</h2>\r\n<br />\r\n");
            EndContext();
#line 8 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\User\Lost.cshtml"
 using (Html.BeginForm())
{
    

#line default
#line hidden
            BeginContext(156, 10, true);
            WriteLiteral("Username: ");
            EndContext();
            BeginContext(167, 32, false);
#line 10 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\User\Lost.cshtml"
               Write(Html.TextBoxFor(m => m.Username));

#line default
#line hidden
            EndContext();
            BeginContext(206, 14, true);
            WriteLiteral("<br /><br />\r\n");
            EndContext();
            BeginContext(230, 10, true);
            WriteLiteral("Password: ");
            EndContext();
            BeginContext(241, 59, false);
#line 11 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\User\Lost.cshtml"
               Write(Html.TextBoxFor(m => m.Password, new { type = "password" }));

#line default
#line hidden
            EndContext();
            BeginContext(307, 69, true);
            WriteLiteral("\r\n    <button type=\"submit\" class=\"=btn btn-primary\">Login</button>\r\n");
            EndContext();
#line 13 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\User\Lost.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AutoQuestionGenerator.DatabaseModels.Users> Html { get; private set; }
    }
}
#pragma warning restore 1591
