#pragma checksum "/home/coder/project/workspace/dotnetapp/Views/Hotel/Create.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "12e7923a8b308ddce9f4d3f8163e8bc9ffd7a322"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Hotel_Create), @"mvc.1.0.view", @"/Views/Hotel/Create.cshtml")]
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
#line 1 "/home/coder/project/workspace/dotnetapp/Views/_ViewImports.cshtml"
using dotnetapp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/coder/project/workspace/dotnetapp/Views/_ViewImports.cshtml"
using dotnetapp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"12e7923a8b308ddce9f4d3f8163e8bc9ffd7a322", @"/Views/Hotel/Create.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5f8d76102390a0daeb769f29824651608905273a", @"/Views/_ViewImports.cshtml")]
    public class Views_Hotel_Create : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Hotel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n<h2>Create New Hotel</h2>\n\n");
#nullable restore
#line 5 "/home/coder/project/workspace/dotnetapp/Views/Hotel/Create.cshtml"
 using (Html.BeginForm())
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"form-group\">\n        ");
#nullable restore
#line 8 "/home/coder/project/workspace/dotnetapp/Views/Hotel/Create.cshtml"
   Write(Html.LabelFor(model => model.Hotel_Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        ");
#nullable restore
#line 9 "/home/coder/project/workspace/dotnetapp/Views/Hotel/Create.cshtml"
   Write(Html.TextBoxFor(model => model.Hotel_Name, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </div>\n    <div class=\"form-group\">\n        ");
#nullable restore
#line 12 "/home/coder/project/workspace/dotnetapp/Views/Hotel/Create.cshtml"
   Write(Html.LabelFor(model => model.City));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        ");
#nullable restore
#line 13 "/home/coder/project/workspace/dotnetapp/Views/Hotel/Create.cshtml"
   Write(Html.TextBoxFor(model => model.City, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </div>\n    <div class=\"form-group\">\n        ");
#nullable restore
#line 16 "/home/coder/project/workspace/dotnetapp/Views/Hotel/Create.cshtml"
   Write(Html.LabelFor(model => model.No_of_Rooms));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        ");
#nullable restore
#line 17 "/home/coder/project/workspace/dotnetapp/Views/Hotel/Create.cshtml"
   Write(Html.TextBoxFor(model => model.No_of_Rooms, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </div> <!-- add a closing tag for the div element -->\n");
            WriteLiteral("    <div class=\"form-group\">\n        ");
#nullable restore
#line 21 "/home/coder/project/workspace/dotnetapp/Views/Hotel/Create.cshtml"
   Write(Html.LabelFor(model => model.Rating));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        ");
#nullable restore
#line 22 "/home/coder/project/workspace/dotnetapp/Views/Hotel/Create.cshtml"
   Write(Html.TextBoxFor(model => model.Rating, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </div>\n    <input type=\"submit\" value=\"Create\" class=\"btn btn-primary\" />\n");
#nullable restore
#line 25 "/home/coder/project/workspace/dotnetapp/Views/Hotel/Create.cshtml"
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Hotel> Html { get; private set; }
    }
}
#pragma warning restore 1591
