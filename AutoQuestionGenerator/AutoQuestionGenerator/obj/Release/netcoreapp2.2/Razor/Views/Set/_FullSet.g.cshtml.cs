#pragma checksum "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6758355639294bcff36218db427c6289d1a5e2b5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Set__FullSet), @"mvc.1.0.view", @"/Views/Set/_FullSet.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Set/_FullSet.cshtml", typeof(AspNetCore.Views_Set__FullSet))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6758355639294bcff36218db427c6289d1a5e2b5", @"/Views/Set/_FullSet.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ab11deb0466ec5e2e96aca1e14ec8a38233b3dfd", @"/Views/_ViewImports.cshtml")]
    public class Views_Set__FullSet : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<QuestionSetViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Set", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Checkworkset", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax", new global::Microsoft.AspNetCore.Html.HtmlString("true"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax-method", new global::Microsoft.AspNetCore.Html.HtmlString("POST"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax-update", new global::Microsoft.AspNetCore.Html.HtmlString("#Question-Form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax-mode", new global::Microsoft.AspNetCore.Html.HtmlString("replace"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("Questions"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("novalidate", new global::Microsoft.AspNetCore.Html.HtmlString("novalidate"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("autocomplete", new global::Microsoft.AspNetCore.Html.HtmlString("off"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(149, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
            BeginContext(153, 2281, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6758355639294bcff36218db427c6289d1a5e2b56950", async() => {
                BeginContext(378, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(385, 36, false);
#line 7 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
Write(Html.HiddenFor(m => m.QuestionSetID));

#line default
#line hidden
                EndContext();
                BeginContext(421, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 8 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
       int correct = 0; 

#line default
#line hidden
                BeginContext(450, 4, true);
                WriteLiteral("    ");
                EndContext();
#line 9 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
     for (int i = 0; i < Model.questions.Length; i++)
    {

#line default
#line hidden
                BeginContext(512, 12, true);
                WriteLiteral("        <div");
                EndContext();
                BeginWriteAttribute("id", " id=\"", 524, "\"", 580, 2);
                WriteAttributeValue("", 529, "Question:", 529, 9, true);
#line 11 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
WriteAttributeValue("", 538, Model.questions[i].question.GetQuestion(), 538, 42, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(581, 21, true);
                WriteLiteral(">\r\n            <p>Q: ");
                EndContext();
                BeginContext(603, 41, false);
#line 12 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
             Write(Model.questions[i].question.GetQuestion());

#line default
#line hidden
                EndContext();
                BeginContext(644, 23, true);
                WriteLiteral(" </p>\r\n            <div");
                EndContext();
                BeginWriteAttribute("id", " id=\"", 667, "\"", 709, 2);
                WriteAttributeValue("", 672, "answer-", 672, 7, true);
#line 13 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
WriteAttributeValue("", 679, Model.questions[i].questionID, 679, 30, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(710, 3, true);
                WriteLiteral(">\r\n");
                EndContext();
#line 14 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
                 if (Model.questions[i].answer.Length == 1)
                {
                    

#line default
#line hidden
                BeginContext(819, 8, true);
                WriteLiteral("Answer: ");
                EndContext();
                BeginContext(835, 200, false);
#line 16 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
                                    Write(Html.TextBoxFor(m => m.questions[i].answer[0], new { style = (Model.questions[i].correct[0] < 0 ? "background:#FF0000" : (Model.questions[i].correct[0] > 0 ? "background:#0F0" : "background:#FFF")) }));

#line default
#line hidden
                EndContext();
                BeginContext(1058, 46, false);
#line 17 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
               Write(Html.HiddenFor(m => m.questions[i].questionID));

#line default
#line hidden
                EndContext();
#line 18 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
                     if (Model.questions[i].correct[0] < 0)
                    {
                        correct++;
                    }

#line default
#line hidden
#line 21 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
                     
                }
                else
                {
                    for (int q = 0; q < Model.questions[i].Boxes.Length; q++)
                    {

                        

#line default
#line hidden
                BeginContext(1444, 27, false);
#line 28 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
                         Write(Model.questions[i].Boxes[q]);

#line default
#line hidden
                EndContext();
                BeginContext(1471, 2, true);
                WriteLiteral(": ");
                EndContext();
                BeginContext(1481, 200, false);
#line 28 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
                                                              Write(Html.TextBoxFor(m => m.questions[i].answer[q], new { style = (Model.questions[i].correct[q] < 0 ? "background:#FF0000" : (Model.questions[i].correct[q] > 0 ? "background:#0F0" : "background:#FFF")) }));

#line default
#line hidden
                EndContext();
                BeginContext(1708, 46, false);
#line 29 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
                   Write(Html.HiddenFor(m => m.questions[i].questionID));

#line default
#line hidden
                EndContext();
#line 29 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
                                                                       

                    }
                    

#line default
#line hidden
#line 32 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
                     if (Model.questions[i].correct.Where(x => x <= 0).Count() == 0)
                    {
                        correct++;
                    }

#line default
#line hidden
#line 35 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
                     
                }

#line default
#line hidden
                BeginContext(1968, 52, true);
                WriteLiteral("            </div>\r\n        </div>\r\n        <br />\r\n");
                EndContext();
#line 40 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
    }

#line default
#line hidden
                BeginContext(2027, 4, true);
                WriteLiteral("    ");
                EndContext();
#line 41 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
     if (correct == Model.questions.Length - 1)
    {

#line default
#line hidden
                BeginContext(2083, 29, true);
                WriteLiteral("        <button type=\"button\"");
                EndContext();
                BeginWriteAttribute("onclick", " onclick=\"", 2112, "\"", 2182, 3);
                WriteAttributeValue("", 2122, "window.location=\'/Set/Complete?setIDt=", 2122, 38, true);
#line 43 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
WriteAttributeValue("", 2160, Model.QuestionSetID, 2160, 20, false);

#line default
#line hidden
                WriteAttributeValue("", 2180, "\';", 2180, 2, true);
                EndWriteAttribute();
                BeginContext(2183, 18, true);
                WriteLiteral(">Review</button>\r\n");
                EndContext();
#line 44 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
    }
    else
    {

#line default
#line hidden
                BeginContext(2225, 100, true);
                WriteLiteral("        <button type=\"submit\" class=\"btn btn-primary\">Submit</button>\r\n        <button type=\"button\"");
                EndContext();
                BeginWriteAttribute("onclick", " onclick=\"", 2325, "\"", 2394, 3);
                WriteAttributeValue("", 2335, "window.location=\'/Set/Complete?setID=", 2335, 37, true);
#line 48 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
WriteAttributeValue("", 2372, Model.QuestionSetID, 2372, 20, false);

#line default
#line hidden
                WriteAttributeValue("", 2392, "\';", 2392, 2, true);
                EndWriteAttribute();
                BeginContext(2395, 25, true);
                WriteLiteral(">Quit & Review</button>\r\n");
                EndContext();
#line 49 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\_FullSet.cshtml"
    }

#line default
#line hidden
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<QuestionSetViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591