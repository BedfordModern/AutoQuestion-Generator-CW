#pragma checksum "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bf02fb5966c67ed6031a39a13731554aac7d2d08"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Work_Later), @"mvc.1.0.view", @"/Views/Work/Later.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Work/Later.cshtml", typeof(AspNetCore.Views_Work_Later))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bf02fb5966c67ed6031a39a13731554aac7d2d08", @"/Views/Work/Later.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ab11deb0466ec5e2e96aca1e14ec8a38233b3dfd", @"/Views/_ViewImports.cshtml")]
    public class Views_Work_Later : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
  
    ViewData["Title"] = "Later";

#line default
#line hidden
            BeginContext(43, 828, true);
            WriteLiteral(@"<style>
    #workTbl {
        font-family: ""Trebuchet MS"", Arial, Helvetica, sans-serif;
        border-collapse: collapse;
        width: 100%;
    }

        #workTbl td, #workTbl th {
            border: 1px solid #ddd;
            padding: 3px;
        }

        #workTbl tr:nth-child(odd) {
            background-color: #9d9393
        }

        #workTbl tr:hover {
            background-color: #ddd;
        }

        #workTbl th {
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: left;
            background-color: #0d6fa8;
            color: white;
            cursor: pointer;
        }

            #workTbl th:hover {
                background-color: #0d49a8;
            }
</style>

<h2>Worksets you've made and saved for later</h2>
");
            EndContext();
#line 40 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
 if (Model.Length > 0)
{

#line default
#line hidden
            BeginContext(898, 421, true);
            WriteLiteral(@"    <table id=""workTbl"">

        <tr>
            <th onclick=""sortTable(0, 'workTbl')"">Set Name </th>
            <th onclick=""sortTable(1, 'workTbl')"">Time Left </th>
            <th onclick=""sortTable(2, 'workTbl')"">Answering Type </th>
            <th onclick=""sortTable(3, 'workTbl')"">Date Set </th>
            <th onclick=""sortTable(4, 'workTbl')"">Date Due </th>
            <th>View</th>
        </tr>
");
            EndContext();
#line 52 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
         foreach (var set in Model)
        {

#line default
#line hidden
            BeginContext(1367, 38, true);
            WriteLiteral("            <tr>\r\n                <td>");
            EndContext();
            BeginContext(1406, 15, false);
#line 55 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
               Write(set.WorksetName);

#line default
#line hidden
            EndContext();
            BeginContext(1421, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(1450, 34, false);
#line 56 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
                Write((set.Date_Due - DateTime.Now).Days);

#line default
#line hidden
            EndContext();
            BeginContext(1485, 32, true);
            WriteLiteral(" Days</td>\r\n                <td>");
            EndContext();
            BeginContext(1518, 11, false);
#line 57 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
               Write(set.SetType);

#line default
#line hidden
            EndContext();
            BeginContext(1529, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(1557, 35, false);
#line 58 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
               Write(set.Date_Set.ToString("dd-MM-yyyy"));

#line default
#line hidden
            EndContext();
            BeginContext(1592, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(1620, 35, false);
#line 59 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
               Write(set.Date_Due.ToString("dd-MM-yyyy"));

#line default
#line hidden
            EndContext();
            BeginContext(1655, 49, true);
            WriteLiteral("</td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1704, 52, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bf02fb5966c67ed6031a39a13731554aac7d2d087918", async() => {
                BeginContext(1747, 5, true);
                WriteLiteral("Print");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "href", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1713, "~/Set/Print?setID=", 1713, 18, true);
#line 61 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
AddHtmlAttributeValue("", 1731, set.WorksetID, 1731, 14, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1756, 22, true);
            WriteLiteral("\r\n                    ");
            EndContext();
            BeginContext(1778, 54, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bf02fb5966c67ed6031a39a13731554aac7d2d089719", async() => {
                BeginContext(1821, 7, true);
                WriteLiteral("Details");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "href", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1787, "~/Work/Workset?id=", 1787, 18, true);
#line 62 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
AddHtmlAttributeValue("", 1805, set.WorksetID, 1805, 14, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1832, 22, true);
            WriteLiteral("\r\n                    ");
            EndContext();
            BeginContext(1854, 51, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bf02fb5966c67ed6031a39a13731554aac7d2d0811522", async() => {
                BeginContext(1897, 4, true);
                WriteLiteral("Edit");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "href", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1863, "~/Set/Build?setID=", 1863, 18, true);
#line 63 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
AddHtmlAttributeValue("", 1881, set.WorksetID, 1881, 14, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1905, 44, true);
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
            EndContext();
#line 66 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
        }

#line default
#line hidden
            BeginContext(1960, 14, true);
            WriteLiteral("    </table>\r\n");
            EndContext();
#line 68 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
}
else
{

#line default
#line hidden
            BeginContext(1986, 47, true);
            WriteLiteral("    <h4>You have set no work for anyone.</h4>\r\n");
            EndContext();
#line 72 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
}

#line default
#line hidden
            BeginContext(2036, 2, true);
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
