#pragma checksum "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\Complete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ee4f444823d5df814b399a47a669c8dd41f58f05"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ee4f444823d5df814b399a47a669c8dd41f58f05", @"/Views/Set/Complete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ab11deb0466ec5e2e96aca1e14ec8a38233b3dfd", @"/Views/_ViewImports.cshtml")]
    public class Views_Set_Complete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CompleteQuestionViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\Complete.cshtml"
  
    ViewData["Title"] = "Complete";

#line default
#line hidden
            BeginContext(133, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(308, 1078, true);
            WriteLiteral(@"

<h2>Complete</h2>
<div style=""max-width: 50%; max-height:150px"">
    <canvas id=""CorrectPer"" width=""400"" height=""400""></canvas>
</div>

<div style=""max-width: 50%; max-height:30%"">
    <canvas id=""CorrectByType"" width=""400"" height=""400""></canvas>
</div>
<script>
    var options = {
        maintainAspectRatio: false,
        spanGaps: false,
        elements: {
            line: {
                tension: 0.4
            }
        },
        plugins: {
            filler: {
                propagate: false
            }
        },
        scales: {
            xAxes: [{
                ticks: {
                    autoSkip: false,
                    maxRotation: 0
                }
            }],
            yAxes: [{
                ticks: {
                    beginAtZero: true
                }
            }]
        }
    };
    new Chart('CorrectPer', {
        type: 'horizontalBar',
        data: {
            labels: ['Percentage'],
            datasets: [{");
            WriteLiteral("\r\n                label: \'%\',\r\n                data: [");
            EndContext();
            BeginContext(1387, 27, false);
#line 55 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\Complete.cshtml"
                  Write(Model.Percentage.Percentage);

#line default
#line hidden
            EndContext();
            BeginContext(1414, 1029, true);
            WriteLiteral(@"],
                backgroundColor: [
                    'rgba(255, 99, 132, 0.4)'
                ],
                borderColor: [
                    'rgba(255,99,132,1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    },

                    beginAtZero: true, 
                    min: 0,
                    max: 100
                }],
                xAxes: [{
                    ticks: {
                        beginAtZero: true,
                        min: 0,
                        max: 100
                    }
                }]
            },
            maintainAspectRatio: false
        }
    });

    var ctx = document.getElementById(""CorrectByType"");
            var myChart = new Chart(ctx, {

                type: 'bar',
                data: {

                    labe");
            WriteLiteral("ls: [");
            EndContext();
#line 94 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\Complete.cshtml"
                              foreach (var item in Model.questionTypes)
            {

                        

#line default
#line hidden
            BeginContext(2534, 1, true);
            WriteLiteral("\'");
            EndContext();
            BeginContext(2536, 4, false);
#line 97 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\Complete.cshtml"
                          Write(item);

#line default
#line hidden
            EndContext();
            BeginContext(2540, 2, true);
            WriteLiteral("\',");
            EndContext();
#line 97 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\Complete.cshtml"
                                             
            }

#line default
#line hidden
            BeginContext(2564, 97, true);
            WriteLiteral("],\r\n            datasets: [{\r\n            label: \'% of Answers Correct\',\r\n                data: [");
            EndContext();
            BeginContext(2662, 58, false);
#line 101 "C:\Users\Daniel Ledger 13HJK\Documents\CourseWork\AutoQuestion-Generator-CW\AutoQuestionGenerator\AutoQuestionGenerator\Views\Set\Complete.cshtml"
                  Write(Model.List.Select(x => x.Percentage).ToList().Connect(","));

#line default
#line hidden
            EndContext();
            BeginContext(2720, 1140, true);
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
                    },

                    min: 0,
                    max: 100
                }],
                xAxes: [{
       ");
            WriteLiteral("             min: 0,\r\n                    max: 100\r\n                }]\r\n            }\r\n        }\r\n    });\r\n</script>");
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
