#pragma checksum "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\Complete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d1dd7cbab51db8891bf5d2cf281fd51bb23bdeb9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Set_Complete), @"mvc.1.0.view", @"/Views/Set/Complete.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Set/Complete.cshtml", typeof(AspNetCore.Views_Set_Complete))]
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
#line 2 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\Complete.cshtml"
using static AutoQuestionGenerator.Helper.Extentions;

#line default
#line hidden
#line 3 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\Complete.cshtml"
using AutoQuestionGenerator.Models.Statistics;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d1dd7cbab51db8891bf5d2cf281fd51bb23bdeb9", @"/Views/Set/Complete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ab11deb0466ec5e2e96aca1e14ec8a38233b3dfd", @"/Views/_ViewImports.cshtml")]
    public class Views_Set_Complete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CompleteQuestionViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 4 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\Complete.cshtml"
  
    ViewData["Title"] = "Complete";

#line default
#line hidden
#line 7 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\Complete.cshtml"
   var list =
          from question in Model.Question
          group question by question.Type.TypeID into QGroup
          select new PercentageModel {
              Current = QGroup.Where(x => x.AnsweredCorrent > 0).Count(),
              Total = QGroup.Count()
          };

    var questionTypes =
        (from question in Model.Question
         select question.Type.Type_Name).ToList();
    questionTypes = questionTypes.Unique().ToList();
            

#line default
#line hidden
            BeginContext(658, 341, true);
            WriteLiteral(@"
<h2>Complete</h2>
<div style=""max-width: 50%; max-height:30%"">
    <canvas id=""CorrectByType"" width=""400"" height=""400""></canvas>
</div>
<script>
    var ctx = document.getElementById(""CorrectByType"");
            var myChart = new Chart(ctx, {

                type: 'bar',
                data: {

                    labels: [");
            EndContext();
#line 32 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\Complete.cshtml"
                              foreach (var item in questionTypes)
            {

                        

#line default
#line hidden
            BeginContext(1084, 1, true);
            WriteLiteral("\'");
            EndContext();
            BeginContext(1086, 4, false);
#line 35 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\Complete.cshtml"
                          Write(item);

#line default
#line hidden
            EndContext();
            BeginContext(1090, 2, true);
            WriteLiteral("\',");
            EndContext();
#line 35 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\Complete.cshtml"
                                             
            }

#line default
#line hidden
            BeginContext(1114, 97, true);
            WriteLiteral("],\r\n            datasets: [{\r\n            label: \'% of Answers Correct\',\r\n                data: [");
            EndContext();
            BeginContext(1212, 52, false);
#line 39 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\Complete.cshtml"
                  Write(list.Select(x => x.Percentage).ToList().Connect(","));

#line default
#line hidden
            EndContext();
            BeginContext(1264, 971, true);
            WriteLiteral(@",2,3,4,5],
                backgroundColor: [
                    'rgba(255, 99, 132, 0.4)',
                    'rgba(54, 162, 235, 0.4)',
                    'rgba(255, 206, 86, 0.4)',
                    'rgba(75, 192, 192, 0.4)',
                    'rgba(153, 102, 255, 0.4)',
                    'rgba(255, 159, 64, 0.4)'
                ],
                borderColor: [
                    'rgba(255,99,132,1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CompleteQuestionViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
