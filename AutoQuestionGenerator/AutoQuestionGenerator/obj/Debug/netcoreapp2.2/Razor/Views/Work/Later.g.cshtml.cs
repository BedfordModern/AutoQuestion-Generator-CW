#pragma checksum "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "914a5bc061033007aa78d5f10bf8e45595b0c9e5"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"914a5bc061033007aa78d5f10bf8e45595b0c9e5", @"/Views/Work/Later.cshtml")]
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
            BeginContext(898, 450, true);
            WriteLiteral(@"    <table id=""workTbl"">

        <tr>
            <th onclick=""sortTable(0, 'workTbl')"">Set Name </th>
            <th onclick=""sortTable(1, 'workTbl')"">Time Left </th>
            <th onclick=""sortTable(2, 'workTbl')"">Answering Type </th>
            <th onclick=""sortTable(3, 'workTbl')"">Date Set </th>
            <th onclick=""sortTable(4, 'workTbl')"">Date Due </th>
            <th>View</th>
            <th>Prints</th>
        </tr>
");
            EndContext();
#line 53 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
         foreach (var set in Model)
        {

#line default
#line hidden
            BeginContext(1396, 38, true);
            WriteLiteral("            <tr>\r\n                <td>");
            EndContext();
            BeginContext(1435, 15, false);
#line 56 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
               Write(set.WorksetName);

#line default
#line hidden
            EndContext();
            BeginContext(1450, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(1479, 34, false);
#line 57 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
                Write((set.Date_Due - DateTime.Now).Days);

#line default
#line hidden
            EndContext();
            BeginContext(1514, 32, true);
            WriteLiteral(" Days</td>\r\n                <td>");
            EndContext();
            BeginContext(1547, 11, false);
#line 58 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
               Write(set.SetType);

#line default
#line hidden
            EndContext();
            BeginContext(1558, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(1586, 35, false);
#line 59 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
               Write(set.Date_Set.ToString("dd-MM-yyyy"));

#line default
#line hidden
            EndContext();
            BeginContext(1621, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(1649, 35, false);
#line 60 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
               Write(set.Date_Due.ToString("dd-MM-yyyy"));

#line default
#line hidden
            EndContext();
            BeginContext(1684, 49, true);
            WriteLiteral("</td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1733, 55, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "914a5bc061033007aa78d5f10bf8e45595b0c9e57947", async() => {
                BeginContext(1778, 6, true);
                WriteLiteral("Assign");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "href", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1742, "~/Work/Assign?setID=", 1742, 20, true);
#line 62 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
AddHtmlAttributeValue("", 1762, set.WorksetID, 1762, 14, false);

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
            BeginContext(1788, 22, true);
            WriteLiteral("\r\n                    ");
            EndContext();
            BeginContext(1810, 54, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "914a5bc061033007aa78d5f10bf8e45595b0c9e59751", async() => {
                BeginContext(1853, 7, true);
                WriteLiteral("Details");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "href", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1819, "~/Work/Workset?id=", 1819, 18, true);
#line 63 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
AddHtmlAttributeValue("", 1837, set.WorksetID, 1837, 14, false);

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
            BeginContext(1864, 22, true);
            WriteLiteral("\r\n                    ");
            EndContext();
            BeginContext(1886, 51, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "914a5bc061033007aa78d5f10bf8e45595b0c9e511554", async() => {
                BeginContext(1929, 4, true);
                WriteLiteral("Edit");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "href", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1895, "~/Set/Build?setID=", 1895, 18, true);
#line 64 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
AddHtmlAttributeValue("", 1913, set.WorksetID, 1913, 14, false);

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
            BeginContext(1937, 22, true);
            WriteLiteral("\r\n                    ");
            EndContext();
            BeginContext(1959, 54, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "914a5bc061033007aa78d5f10bf8e45595b0c9e513355", async() => {
                BeginContext(2003, 6, true);
                WriteLiteral("Delete");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "href", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1968, "~/Set/Delete?setID=", 1968, 19, true);
#line 65 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
AddHtmlAttributeValue("", 1987, set.WorksetID, 1987, 14, false);

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
            BeginContext(2013, 74, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    <button");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 2087, "\"", 2143, 3);
            WriteAttributeValue("", 2097, "window.open(\'/Set/Print?setID=", 2097, 30, true);
#line 68 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
WriteAttributeValue("", 2127, set.WorksetID, 2127, 14, false);

#line default
#line hidden
            WriteAttributeValue("", 2141, "\')", 2141, 2, true);
            EndWriteAttribute();
            BeginContext(2144, 79, true);
            WriteLiteral(">Print Questions & Answers</button>\r\n                </td>\r\n            </tr>\r\n");
            EndContext();
#line 71 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
        }

#line default
#line hidden
            BeginContext(2234, 14, true);
            WriteLiteral("    </table>\r\n");
            EndContext();
#line 73 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
}
else
{

#line default
#line hidden
            BeginContext(2260, 47, true);
            WriteLiteral("    <h4>You have set no work for anyone.</h4>\r\n");
            EndContext();
#line 77 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Work\Later.cshtml"
}

#line default
#line hidden
            BeginContext(2310, 2, true);
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
