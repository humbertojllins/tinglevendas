#pragma checksum "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8c37a9f98af67e0a1252720db0716cbacb91e907"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_RelatorioVendas_Index), @"mvc.1.0.view", @"/Views/RelatorioVendas/Index.cshtml")]
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
#line 1 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/_ViewImports.cshtml"
using TingleVendas;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/_ViewImports.cshtml"
using TingleVendas.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8c37a9f98af67e0a1252720db0716cbacb91e907", @"/Views/RelatorioVendas/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"96892a0fa333857afc3f91b0b994c359718c2752", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_RelatorioVendas_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<TingleVendas.Models.RelatorioVendas>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("cboMes"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "pMes", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("cboSupervisor"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "pSupervisor", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("target", new global::Microsoft.AspNetCore.Html.HtmlString("_blank"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Oi360", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onload", new global::Microsoft.AspNetCore.Html.HtmlString("grid();"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 4 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<!--<style>
    div.dataTables_wrapper {
        width: 800px;
        margin: 0 auto;
    }
</style>-->
<!--<div class=""text-center"">
    <h1 class=""display-4"">Olá</h1>
    <p>Bem-vindo ao sistema de gestão de vendas da MDV</p>

</div>-->

    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8c37a9f98af67e0a1252720db0716cbacb91e9077472", async() => {
                WriteLiteral("\r\n        <section class=\"content\">\r\n            <div class=\"container-fluid\">\r\n                <!-- Small boxes (Stat box) -->\r\n                <!--<form asp-action=\"Index\" asp-route-pAno=\"cboAno.Value\" asp-route-pMes=\"cboMes.Value\">-->\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8c37a9f98af67e0a1252720db0716cbacb91e9077999", async() => {
                    WriteLiteral(@"
                    <div class=""row"">
                        <div class=""col-md-3"">
                            <div class=""form-group"">
                                <label>Selecione o ano</label>
                                <input type=""number"" id=""cboAno"" name=""pAno"" class=""form-control"" style=""width:200px""");
                    BeginWriteAttribute("value", " value=\"", 990, "\"", 1022, 1);
#nullable restore
#line 29 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
WriteAttributeValue("", 998, ViewData["anoCorrente"], 998, 24, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(@" min=""2020"" step=""1"">
                            </div>
                        </div>
                        <div class=""col-md-3"">
                            <div class=""form-group"">
                                <label>Selecione o mês</label>
                                ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8c37a9f98af67e0a1252720db0716cbacb91e9079306", async() => {
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Name = (string)__tagHelperAttribute_1.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
#nullable restore
#line 35 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = ViewBag.ListaMeses;

#line default
#line hidden
#nullable disable
                    __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral(@"
                            </div>
                        </div>
                        <div class=""col-md-3"">
                            <div class=""form-group"">
                                <label>Selecione o supervisor</label>
                                ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8c37a9f98af67e0a1252720db0716cbacb91e90711470", async() => {
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Name = (string)__tagHelperAttribute_4.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
#nullable restore
#line 41 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = ViewBag.ListaSupervisor;

#line default
#line hidden
#nullable disable
                    __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral(@"
                            </div>
                        </div>

                        <div class=""col-md-2"">
                            <div class=""form-group"">
                                <label style=""visibility:hidden"">Botão</label>
                                <input type=""submit"" id=""GFG_Button"" style=""width:150px"" value=""Pesquisar"" class=""btn btn-primary btn-block"">
                            </div>
                        </div>
                    </div>

                ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                <div class=""row"">
                </div>
            </div>
        </section>
        <section class=""content"">
            <div class=""container-fluid"">
                <!-- Small boxes (Stat box) -->
                <div class=""row"">
                    <div class=""col-lg-3 col-6"">
                        <!-- small box -->
                        <div class=""small-box bg-info"">
                            <div class=""inner"">
                                <h3>");
#nullable restore
#line 66 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
                               Write(ViewData["QtdVendasReal"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h3>

                                <p>Vendas realizadas</p>
                            </div>
                            <div class=""icon"">
                                <i class=""nav-icon fas fa-hand-holding-usd""></i>

                            </div>
                            <a href=""#"" onclick=""searchAdv('');"" class=""small-box-footer""><i class=""fas fa-arrow-circle-right""></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class=""col-lg-3 col-6"">
                        <!-- small box -->
                        <div class=""small-box bg-success"">
                            <div class=""inner"">
                                <h3>
                                    ");
#nullable restore
#line 83 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
                               Write(ViewData["QtdVendasInstalados"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                    \r\n                                    <label style=\"font-size:medium\">(");
#nullable restore
#line 85 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
                                                                Write(ViewData["PercVendasInstalados"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"%)</label>    
                                    
                                </h3>

                                <p>Instalados</p>

                            </div>
                            
                                <div class=""icon"">
                                    <i class=""nav-icon fas fa-user-check""></i>
                                </div>
                                <a href=""#"" onclick=""searchAdv('Concluído');"" class=""small-box-footer"">Listar  <i class=""fas fa-arrow-circle-right""></i></a>
                            </div>
                        </div>
                    <!-- ./col -->
                    <div class=""col-lg-2 col-6"">
                        <!-- small box -->
                        <div class=""small-box bg-warning"">
                            <div class=""inner"">
                                <h3>");
#nullable restore
#line 104 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
                               Write(ViewData["QtdVendasPendentes"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"<sup style=""font-size: 20px""></sup></h3>

                                <p>Pendências</p>
                            </div>
                            <div class=""icon"">
                                <i class=""nav-icon fas fa-info-circle""></i>
                            </div>
                            <a href=""#"" onclick=""searchAdv('Pendente');"" class=""small-box-footer"">Listar  <i class=""fas fa-arrow-circle-right""></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class=""col-lg-2 col-6"">
                        <!-- small box -->
                        <div class=""small-box bg-danger"">
                            <div class=""inner"">
                                <h3>");
#nullable restore
#line 119 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
                               Write(ViewData["QtdVendasCanceladas"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h3>

                                <p>Cancelados</p>
                            </div>
                            <div class=""icon"">
                                <i class=""nav-icon fas fa-times-circle""></i>
                            </div>
                            <a href=""#"" onclick=""searchAdv('Cancelado');"" class=""small-box-footer"">Listar  <i class=""fas fa-arrow-circle-right""></i></a>
                        </div>
                    </div>
                    <div class=""col-lg-2 col-6"">
                        <!-- small box -->
                        <div class=""small-box bg-danger"">
                            <div class=""inner"">
                                <h3>");
#nullable restore
#line 133 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
                               Write(ViewData["QtdVendasPerdas"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h3>

                                <p>Perdas</p>
                            </div>
                            <div class=""icon"">
                                <i class=""nav-icon fas fa-trash-alt""></i>
                            </div>
                            <a href=""#"" onclick=""searchAdv('Perdas');"" class=""small-box-footer"">Listar  <i class=""fas fa-arrow-circle-right""></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                </div>
                <!-- /.row -->
                <!-- Main row -->
                <div class=""row"">

                </div>
                <!-- /.row (main row) -->
            </div><!-- /.container-fluid -->
        </section>


        <!--<table id=""dtprincipal"" class=""table table-hover table-bordered table-striped"">-->
        <table id=""dtprincipal"" class=""display nowrap"" style=""width:100%"">
            <thead>
                <tr>
                    <th>
                    ");
                WriteLiteral(@"    Numero Pedido
                    </th>
                    <th>
                        Nome
                    </th>
                    <th>
                        CPF
                    </th>
                    <th>
                        Status
                    </th>
                    <th>
                        Tipo de pedido
                    </th>
                    <th>
                        Bairro
                    </th>
                    <th>
                        Data Pedido
                    </th>
                    <th>
                        Supervisor
                    </th>
                    <th>
                        Vendedor
                    </th>
                    <th>
                        Reinput
                    </th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 192 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8c37a9f98af67e0a1252720db0716cbacb91e90723002", async() => {
                    WriteLiteral("\r\n                            ");
#nullable restore
#line 197 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.NumPedido));

#line default
#line hidden
#nullable disable
                    WriteLiteral("\r\n                        ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_7.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_8.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#nullable restore
#line 196 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
                                                                                         WriteLiteral(item.NumPedido);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 201 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
                   Write(Html.DisplayFor(modelItem => item.NomeCliente));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 204 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Cpf));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 207 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
                   Write(Html.DisplayFor(modelItem => item.BovStatus));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 210 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
                   Write(Html.DisplayFor(modelItem => item.TipoPedido));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 213 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Bairro));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 216 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
                   Write(Convert.ToDateTime(item.DataPedido).ToString("dd/MM/yyyy"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 219 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
                   Write(Html.DisplayFor(modelItem => item.NomeSupervisor));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 222 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
                   Write(Html.DisplayFor(modelItem => item.NomeVendedor));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 225 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Reinput));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </td>\r\n\r\n                </tr>\r\n");
#nullable restore
#line 229 "/Users/humbertolins/Documents/Projetos/tinglevendas/TingleVendas/Views/RelatorioVendas/Index.cshtml"
                }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"            </tbody>
        </table>

        <!-- page script -->

        <script>function grid() {
                $('#dtprincipal').DataTable({
                    ""scrollX"": true,
                    dom: 'lBfrtip',
                    buttons: [
                        'copyHtml5',
                        'excelHtml5',
                        'csvHtml5',
                        'pdfHtml5'
                    ],
                    ""language"": {
                        ""lengthMenu"": ""Mostrar _MENU_&nbsp; &nbsp; &nbsp;"",
                        ""zeroRecords"": ""Nenhum registro"",
                        ""info"": ""Página _PAGE_ de _PAGES_"",
                        ""infoEmpty"": ""Nenhum registro"",
                        ""infoFiltered"": ""(filtrado de _MAX_ total registros)"",
                        ""search"": ""Procurar"",
                        ""loadingRecords"": ""Carregando..."",
                        ""processing"": ""Processando..."",
                        ""paginate"": {
               ");
                WriteLiteral(@"             ""first"": ""Primeiro"",
                            ""last"": ""Último"",
                            ""next"": ""Próximo"",
                            ""previous"": ""Anterior""
                        }
                    }


                });
            }

            function searchAdv(busca) {

                var table = $('#dtprincipal').DataTable();
                table.search(busca).draw();

            }</script>
    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<TingleVendas.Models.RelatorioVendas>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
