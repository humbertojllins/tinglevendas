using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using TingleVendas.Models;

namespace TingleVendas.Controllers
{
    [Authorize]
    public class UpFileController : Controller
    {
        private readonly TingleVendasContext _context;
        string datahoraImportacao;
        string nomeArquivoImportacao;
        List<Click> listaC;
        List<Oi360> listaoi360;
        int i = 0;

        //Variaveis para Amazon s3
        //private const string bucketName = "tingledoc"; //"*** provide bucket name ***";
        //private const string keyName = "mdvdoc_"; // "*** provide a name for the uploaded object ***";
        //private const string filePath = "*** provide the full path name of the file to upload ***";
        // Specify your bucket region (an example region is shown).
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1;
        //private static IAmazonS3 s3Client;
        //Variaveis para Amazon s3

        public UpFileController(TingleVendasContext context)
        {
            _context = context;

            //s3Client = new AmazonS3Client(bucketRegion);
            //UploadFileAsync().Wait();
        }
        /// <summary>
        /// Seta os dados de sessao do usuario para ser utilizada na pagina _layout
        /// </summary>
        private void setaDadosSessao()
        {
            var usuario = HttpContext.Session.GetObjectFromJson<Usuario>("sessionUsuario");

            if (usuario != null)
            {
                //ViewData["nome"] = usuario.Nome + " " + usuario.Sobrenome;
                ViewData["nome"] = usuario.Nome + " " + usuario.Sobrenome;
                ViewData["email"] = usuario.Email;
                ////ViewData["foto"] = usuario.Foto;
                ViewData["url"] = "Importação de Dados";
                //this.ControllerContext.RouteData.Values["action"].ToString();
                int totalMenus = _context.Menu.Count();
                //Esconde os menus
                for (int i = 1; i <= totalMenus; i++)
                {
                    ViewData[i.ToString()] = "none";
                }

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    ViewData["ListaMenu"] = new SelectList(_context.GrupoMenu.Where(gm => gm.IdGrupo == usuario.IdGrupo), "Idmenu", "Idmenu");
                    //Mostra os menus para o perfil do usuário
                    foreach (var item in ((SelectList)ViewData["ListaMenu"]))
                    {
                        ViewData[item.Value] = "Normal";
                    }
                }
            }
        }

        public IActionResult Index(string arquivo)
        {
            setaDadosSessao(arquivo);
            setaDadosSessao();
            return View();
        }

        void setaDadosSessao (string arquivo)
        {
            ViewData["oi360"] = false;
            ViewData["bov"] = false;
            ViewData["bovcanc"] = false;
            ViewData["click"] = false;
            ViewData["clickfechadas"] = false;
            switch (arquivo)
            {
                case "oi360":
                    ViewData["oi360"] = true;
                    break;
                case "bov":
                    ViewData["bov"] = true;
                    break;
                case "bovcanc":
                    ViewData["bovcanc"] = true;
                    break;
                case "click":
                    ViewData["click"] = true;
                    break;
                case "clickfechadas":
                    ViewData["clickfechadas"] = true;
                    break;
            }
        }


        // GET: LogImportacao
        public async Task<IActionResult> LogImp()
        {
            setaDadosSessao();
            return RedirectToAction("Index", "LogImportacao");
        }

        bool validaNomeArquivo(string opcao, string nomeArquivo)
        {
            int tamanhoPadrao = opcao.Length;
            int tamanhoArquiv = nomeArquivo.Length;
            if (tamanhoArquiv > tamanhoPadrao && nomeArquivo.Substring(0, tamanhoPadrao).ToLower().Equals(opcao.ToLower()))
            {
                return true;
            }
            return false;
        }

        [HttpPost("FileUpload")]
        //public async Task<IActionResult> Index(List<IFormFile> files, string arquivo)
        public async Task<IActionResult> Index(List<IFormFile> files, string arquivo)
        {
            //ActionResult ret;
            string msg = "";
            setaDadosSessao();
            long size = files.Sum(f => f.Length);

            switch (arquivo)
            {
                case "oi360":
                    if (validaNomeArquivo("Planilhaoi360_", files[0].FileName))
                    {
                        //msg = upOi360(files);
                    }
                    else
                    {
                        //msg = "Nome de arquivo inválido, ex: Planilhaoi360_Jan2020";
                        //return Json(new { status = "false", message = msg });
                    }
                    break;
                case "bov":
                    if (validaNomeArquivo("Planilhabovinst_", files[0].FileName)) { 
                        msg = upBovIntalacao(files);
                    }
                    else
                    {
                        msg = "Nome de arquivo inválido, ex: Planilhabovinst_Jan2020";
                        return Json(new { status = "false", message = msg });
                    }
                    break;
                case "bovcanc":
                    if (validaNomeArquivo("Planilhabovcanc_", files[0].FileName))
                    {
                        msg = upBovCancelamento(files);
                    }
                    else
                    {
                        msg = "Nome de arquivo inválido, ex: Planilhabovcanc_Jan2020";
                        return Json(new { status = "false", message = msg });
                    }
                    break;
                case "click":
                    if (validaNomeArquivo("PlanilhaClick_", files[0].FileName))
                    {
                        msg = upClick(files);
                    }
                    else
                    {
                        msg = "Nome de arquivo inválido, ex: PlanilhaClick_Jan2020";
                        return Json(new { status = "false", message = msg });
                    }
                    break;
                case "clickfechadas":
                    if (validaNomeArquivo("PlanilhaClickFechadas_", files[0].FileName))
                    {
                        msg = upClickFechadas(files);
                    }
                    else
                    {
                        msg = "Nome de arquivo inválido, ex: PlanilhaClickFechadas_Jan2020";
                        return Json(new { status = "false", message = msg });
                    }
                    break;
                
                case "docvenda":
                    msg = upPdfDocument(files);
                    break;
            }

            if (msg.Substring(0, 4) == "ERRO")
            {
                return Json(new { status = "false", message = msg });
            }
            return Json(new { status = "true", message = msg });
            //return Json();
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        #region OI360


        public string upOi360(List<IFormFile> files)
        {
            try
            {
                //Seta a data e hora da importacao da planilha
                datahoraImportacao = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
                Oi360 l = new Oi360();
                Startup.Progress = 0;
                int totalLinhas = 0; 
            //int totalReadBytes = 0;

                var filePaths = new List<string>();
                foreach (var formFile in files)
                {
                    //if (formFile.Count() > 0)
                    //{
                        //Seta o nome do arquivo que será importado
                        nomeArquivoImportacao = formFile.FileName;
                        // full path to file in temp location
                        var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
                        //FileInfo excel = new FileInfo(filePath);

                        //var filePath = Path.GetFullPath(formFile.FileName);
                        //var filePath = RandomString(4) + "_"+ formFile.FileName;
                        filePaths.Add(filePath);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            //formFile.CopyToAsync(stream).ConfigureAwait(false);
                            formFile.CopyToAsync(stream);
                            System.Threading.Thread.Sleep(5000);
                        //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        ExcelPackage package = new ExcelPackage(stream);
                            int qtdPlanilhas = package.Workbook.Worksheets.Count();
                            if (qtdPlanilhas > 0)
                            {
                                try
                                {
                                    listaoi360 = new List<Oi360>();
                                    ExcelWorksheet linha;
                                    totalLinhas = (int)package.Workbook.Worksheets[0].Dimension?.Rows;
                                    for (int i = 2; i <= totalLinhas; i++)
                                    {
                                        
                                        //Verificar se ja foi inserido, se sim, verificar se tem alguma alteracao
                                        linha = package.Workbook.Worksheets[0];
                                        l.NumeroDoPedido = Convert.ToString(linha.Cells[i, 1].Value);
                                        l.DataEmQueOPedidoFoiRealizado = ConverterData(Convert.ToString(linha.Cells[i, 2].Value), true);
                                        //l.HoraEmQueOPedidoFoiRealizado = linha.Cells[i, 3].Value.ToString();
                                        //l.DataEmQueOPedidoFoiTratado = ConverterData(linha.Cells[i, 4].Value.ToString());
                                        //l.HoraEmQueOPedidoFoiTratado = linha.Cells[i, 5].Value.ToString();
                                        l.NomeCliente = Convert.ToString(linha.Cells[i, 5].Value);
                                        l.Genero = Convert.ToString(linha.Cells[i, 6].Value);
                                        l.DataDeNascimento = ConverterData(Convert.ToString(linha.Cells[i, 7].Value), true);
                                        l.Cpf = Convert.ToString(linha.Cells[i, 8].Value);
                                        l.Rg = Convert.ToString(linha.Cells[i, 9].Value);
                                        l.RgOExpedidor = Convert.ToString(linha.Cells[i, 10].Value);
                                        l.RgDataExpedicao = ConverterData(Convert.ToString(linha.Cells[i, 11].Value), true);
                                        l.NomeCompletoDaMae = Convert.ToString(linha.Cells[i, 12].Value);
                                        l.Nacionalidade = Convert.ToString(linha.Cells[i, 13].Value);
                                        l.ContatoPrincipal = Convert.ToString(linha.Cells[i, 14].Value);
                                        l.ContatoPrincipalWhatsapp = Convert.ToString(linha.Cells[i, 15].Value);
                                        l.ContatoSecundario = Convert.ToString(linha.Cells[i, 16].Value);
                                        l.ContatoSecundarioWhatsapp = Convert.ToString(linha.Cells[i, 17].Value);
                                        l.EMail = Convert.ToString(linha.Cells[i, 24].Value);
                                        l.MatriculaVendedor = Convert.ToString(linha.Cells[i, 25].Value);
                                        l.NomeVendedor = Convert.ToString(linha.Cells[i, 26].Value);
                                        l.TvPlanoTv = Convert.ToString(linha.Cells[i, 36].Value);
                                        //TODO: VALIDAR FUNCAO
                                        l.TvPontosAdicionais = ConverterInteiro(Convert.ToString(linha.Cells[i, 37].Value));
                                        l.ComboContratado = Convert.ToString(linha.Cells[i, 43].Value);
                                        l.PagamentoFormaDePagamento = Convert.ToString(linha.Cells[i, 44].Value);
                                        l.PagamentoVencimento = ConverterInteiro(Convert.ToString(linha.Cells[i, 45].Value));
                                        l.PagamentoContaOnline = Convert.ToString(linha.Cells[i, 46].Value);
                                        l.PagamentoBanco = Convert.ToString(linha.Cells[i, 47].Value);
                                        l.PagamentoAgencia = Convert.ToString(linha.Cells[i, 48].Value);
                                        l.PagamentoConta = Convert.ToString(linha.Cells[i, 49].Value);
                                        l.PagamentoDigito = ConverterInteiro(Convert.ToString(linha.Cells[i, 50].Value));
                                        l.StatusPrimario = Convert.ToString(linha.Cells[i, 51].Value);
                                        l.ObservacaoVendedor = Convert.ToString(linha.Cells[i, 53].Value);
                                        //TOdo:Trocar para string
                                        l.InstalacaoCep = Convert.ToString(linha.Cells[i, 60].Value);
                                        l.InstalacaoLogradouro = Convert.ToString(linha.Cells[i, 61].Value);
                                        l.InstalacaoNumero = Convert.ToString(linha.Cells[i, 62].Value);
                                        l.InstalacaoBairro = Convert.ToString(linha.Cells[i, 63].Value);
                                        l.InstalacaoReferencia = Convert.ToString(linha.Cells[i, 66].Value);
                                        l.InstalacaoCdoeCdoi = Convert.ToString(linha.Cells[i, 67].Value);
                                        l.InstalacaoComplemento1Tipo = Convert.ToString(linha.Cells[i, 68].Value);
                                        l.InstalacaoComplemento1 = Convert.ToString(linha.Cells[i, 69].Value);
                                        l.InstalacaoComplemento2Tipo = Convert.ToString(linha.Cells[i, 70].Value);
                                        l.InstalacaoComplemento2 = Convert.ToString(linha.Cells[i, 71].Value);
                                        l.CobrancaCep = Convert.ToString(linha.Cells[i, 74].Value);
                                        l.CobrancaLogradouro = Convert.ToString(linha.Cells[i, 75].Value);
                                        l.CobrancaNumero = Convert.ToString(linha.Cells[i, 76].Value);
                                        l.CobrancaBairro = Convert.ToString(linha.Cells[i, 77].Value);
                                        l.CobrancaComplemento1Tipo = Convert.ToString(linha.Cells[i, 80].Value);
                                        l.CobrancaComplemento1 = Convert.ToString(linha.Cells[i, 81].Value);
                                        l.CobrancaComplemento2Tipo = Convert.ToString(linha.Cells[i, 82].Value);
                                        l.CobrancaComplemento2 = Convert.ToString(linha.Cells[i, 83].Value);
                                        //l.VendaMesAnterior = ValidaSeReinput(l.NomeCliente, Convert.ToDateTime(l.DataEmQueOPedidoFoiRealizado));

                                        listaoi360.Add(l);
                                        l = new Oi360();

                                            //if (ValidaSeJaInseriu(l.NumeroDoPedido,l.NomeCliente, Convert.ToDateTime(l.DataEmQueOPedidoFoiRealizado)))
                                            //    {
                                            //        try
                                            //        {
                                                
                                            //            _context.Oi360.Add(l);
                                            //            _context.SaveChanges();
                                                

                                            //    }
                                            //        catch (Exception ex)
                                            //        {
                                            //            _context.Oi360.Local.Clear();
                                            //            //Gravar log
                                            //            InserirLog("OI360", datahoraImportacao, "Erro", l.NumeroDoPedido, l.NomeCliente, i, ex.InnerException.Message);
                                            //        }
                                            //    }
                                            //    Startup.Progress = (int)(((float)i / (float)totalLinhas) * 100);
                                        
                                    }



                                List<Oi360> listaOrdenada = listaoi360.OrderByDescending(ord => ord.DataEmQueOPedidoFoiRealizado).ToList();
                                foreach (var item in listaOrdenada)
                                {
                                    i += 1;
                                    string rein ="";
                                    if (ValidaSeJaInseriu(item.NumeroDoPedido, item.NomeCliente, Convert.ToDateTime(item.DataEmQueOPedidoFoiRealizado),out rein))
                                    {
                                        try
                                        {  
                                            item.VendaMesAnterior = rein;
                                            _context.Oi360.Add(item);
                                            _context.SaveChanges();
                                        }
                                        catch (Exception ex)
                                        {
                                            //_context.Oi360.Local.Clear();
                                            //Gravar log
                                            InserirLog("OI360", datahoraImportacao, "Erro", item.NumeroDoPedido, item.NomeCliente, i, ex.InnerException.Message);
                                        }
                                    }
                                    Startup.Progress = (int)(((float)i / (float)totalLinhas) * 100);

                                    //_context.Oi360.Add(item);
                                    //_context.SaveChanges();
                                }

                            }
                            catch (Exception ex)
                                {
                                    InserirLog("OI360", datahoraImportacao, "Erro", Convert.ToString(l.NumeroDoPedido), Convert.ToString(l.NomeCliente), 0, (ex.InnerException.Message == null ? ex.Message : ex.InnerException.Message));
                                }
                            }
                            else
                            {
                                return new string("(P)O arquivo não foi carregado, favor repetir a operação!");
                            }

                    }
                    //}
                    //else
                    //{
                    //    return new string("O arquivo não foi carregado, favor repetir a operação!");
                    //}
                }

                InserirLog("OI360", datahoraImportacao, "Resumo", nomeArquivoImportacao, "**********", totalLinhas-1, _context.LogImportacao.Where(l => l.Planilha == "OI360" && l.DataHora == datahoraImportacao).Count().ToString());
                //ViewData["qtdLinhas"] = totalLinhas;
                //ViewData["erros"] = _context.LogImportacao.Where(l => l.Chave1 == "OI360" && l.DataHora == datahoraImportacao).Count();
                return new string("Importação concluída!");
            }
            catch (Exception ex)
            {
                return new string(ex.Message);
            }

        }

        public string ValidaSeReinput(string nomeCliente, DateTime dtVenda)
        {
            int i= _context.Oi360.Count(c => c.NomeCliente == nomeCliente && c.DataEmQueOPedidoFoiRealizado<dtVenda);
            return (i==0? "" : "Reinput");
        }

        public bool ValidaSeJaInseriu(string numPedido, string nomeCliente, DateTime dtVenda, out string reinput)
        {
            DateTime dtInicial, dtFinal;
            dtInicial = new DateTime(dtVenda.Year, dtVenda.Month, 1);
            dtFinal = dtInicial.AddMonths(1);

            //Validar duplicidade pelo CPF e nao pelo numero do pedido
            //Oi360 valida = _context.Oi360.FirstOrDefault(np => np.NumeroDoPedido == numPedido);
            Oi360 valida = _context.Oi360.FirstOrDefault(np => np.NumeroDoPedido == numPedido);

            if (valida != null)
            {
                //Número do pedido já inserido
                reinput = "";
                return false;
            }
            else
            {
                valida = _context.Oi360.FirstOrDefault(np => np.NomeCliente == nomeCliente);
                if (valida != null)
                { 
                    if (valida.DataEmQueOPedidoFoiRealizado > dtInicial)
                    {
                        reinput = "";
                        return false;
                    }
                    else
                    {
                        reinput = "Reinput";
                        return true;
                    }
                }
            }
            reinput = "";
            return true;
        }
        #endregion

        #region FunçõesGerais
        public DateTime ConverterData(object data, bool combarra)
        {
            string dtTexto;
            DateTime dt = new DateTime(9999,12,31);
            if (data != null && data.ToString() != "" && data.ToString() != "-")
            {
                /*if (data.ToString().Length == 10)
                {
                    if (combarra == true)
                    {
                        dtTexto = data.ToString().Substring(6, 4) + "/" + data.ToString().Substring(3, 2) + "/" + data.ToString().Substring(0, 2);
                    }
                    else
                    {
                        dtTexto = data.ToString().Substring(0, 4) + "/" + data.ToString().Substring(4, 2) + "/" + data.ToString().Substring(6, 2);
                    }
                    DateTime.TryParse(dtTexto, out dt);
                }
                else
                {*/
                    if (combarra == true)
                    {
                        dtTexto = data.ToString().Substring(6, 4) + "/" + data.ToString().Substring(3, 2) + "/" + data.ToString().Substring(0, 2);
                    }
                    else
                    {
                        dtTexto = data.ToString().Substring(0, 4) + "/" + data.ToString().Substring(4, 2) + "/" + data.ToString().Substring(6, 2);
                    }
                    DateTime.TryParse(dtTexto, out dt);
                //}
            }
            return dt;
        }


        public DateTime ConverterDataHora(object data, bool combarra)
        {
            string dtTexto;
            DateTime dt = new DateTime(9999, 12, 31);
            if (data != null && data.ToString() != "" && data.ToString() != "-")
            {
                if (combarra == true)
                {
                    dtTexto = data.ToString().Substring(6, 4) + "/" + data.ToString().Substring(3, 2) + "/" + data.ToString().Substring(0, 2) + " " +
                              data.ToString().Substring(11,2) + ":" + data.ToString().Substring(14, 2);
                }
                else
                {
                    dtTexto = data.ToString().Substring(0, 4) + "/" + data.ToString().Substring(4, 2) + "/" + data.ToString().Substring(6, 2);
                }
                DateTime.TryParse(dtTexto, out dt);
                //}
            }
            return dt;
        }
        public int ConverterInteiro(string valor)
            {
                int vlrint = 0;
                int.TryParse(valor, out vlrint);

                return vlrint;
            }

        public void InserirLog(string pplanilha, string pdatahora,string pchave1, string pchave2,string pchave3, int plinha, string perro)
        {
            try
            {
                LogImportacao x = new LogImportacao();
                x.Planilha = pplanilha;
                x.DataHora = pdatahora;
                x.Chave1 = pchave1;
                x.Chave2 = pchave2;
                x.Chave3 = pchave3;
                x.Linha = plinha;
                x.Erro = perro;

                //using (var errorContext = new TingleVendasContext())
                //{
                    _context.LogImportacao.Add(x);
                    _context.SaveChanges();
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion



        [HttpPost]
        public ActionResult Progress()
        {
            return this.Content(Startup.Progress.ToString());
        }

        #region UpFileToS3
        /// <summary>
        /// Amazon S3
        /// </summary>
        /// <returns></returns>
        //private static async Task UploadFileAsync()
        public string upPdfDocument(List<IFormFile> files)
        {

            AWSCredentials credentials = new BasicAWSCredentials("AKIAII6CBAYNVBS2TUFQ", "bjSIPkp6r7Sfhz+pyg8xHnHgw5b43WF7Sngx/V7t");
            AmazonS3Config config = new AmazonS3Config();
            config.ServiceURL = "s3.amazonaws.com";
            config.RegionEndpoint = bucketRegion;

            AmazonS3Client client = new AmazonS3Client(credentials, config);

            var baseUrl = "https://" + "tingledoc" + ".s3.amazonaws.com/" + "mdv/";
            try
            {
                Startup.Progress = 50;
                long size = files.Sum(f => f.Length);

                //client.DeleteObjectAsync(new Amazon.S3.Model.DeleteObjectRequest() { BucketName = "tingledoc/mdv", Key = "Guia de uso (3).pdf" });


                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        var filename = formFile.FileName;
                        //var temp_caminho = Path.GetTempFileName();
                        //temp_caminho = temp_caminho.Replace(".tmp", "/") + filename;
                        using (var fs = System.IO.File.Create(filename))
                        {
                            formFile.CopyTo(fs);
                            fs.Flush();
                        }//these code snippets saves the uploaded files to the project directory

                        //uploadToS3(filename);//this is the method to upload saved file to S3

                        //TransferUtility fileTransferUtility = new TransferUtility(new AmazonS3Client(bucketRegion));
                        TransferUtility fileTransferUtility = new TransferUtility(client);
                        
                        string bucketName = "tingledoc/mdv";
                        baseUrl += filename;
                        fileTransferUtility.Upload(filename, bucketName);
                        Startup.Progress = 100;
                        System.IO.File.Delete(filename);
                    }
                }
            }
            catch (AmazonS3Exception e)
            {
                //Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
                return new string("ERRO S3:" + e.Message + "|" + e.InnerException);
                //return Json(new { status = false, message = e.Message + "|" + e.InnerException });
            }
            catch (Exception e)
            {
                //return Json(new { status = false, message = e.Message + "|" + e.InnerException });
                return new string("ERRO :" + e.Message + "|" + e.InnerException);
            }
            return new string(baseUrl);
            //return Json(new { status = false, message = baseUrl });
            //return Json(new { status = true, message = "Login sucesso!" });
        }

        public void uploadToS3(string filePath)
        {
            try
            {
                TransferUtility fileTransferUtility = new
                    TransferUtility(new AmazonS3Client(bucketRegion));

                string bucketName="tingledoc/mdv";


                //if (_bucketSubdirectory == "" || _bucketSubdirectory == null)
                //{
                //    bucketName = _bucketName; //no subdirectory just bucket name  
                //}
                //else
                //{   // subdirectory and bucket name  
                //    bucketName = _bucketName + @"/" + _bucketSubdirectory;
                //}


                // 1. Upload a file, file name is used as the object key name.
                fileTransferUtility.Upload(filePath, bucketName);
                Console.WriteLine("Upload 1 completed");


            }
            catch (AmazonS3Exception s3Exception)
            {
                Console.WriteLine(s3Exception.Message,
                                  s3Exception.InnerException);
            }
        }
        #endregion
        /// Amazon S3
        ///

        #region BOV
        public string upBovIntalacao(List<IFormFile> files)
        {
            try
            {
                //Seta a data e hora da importacao da planilha
                datahoraImportacao = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
                Bov l = new Bov();
                Startup.Progress = 0;
                int totalLinhas = 0;
                //int totalReadBytes = 0;

                var filePaths = new List<string>();
                foreach (var formFile in files)
                {
                    //if (files.Count() > 0)
                    //{
                        //Seta o nome do arquivo que será importado
                        nomeArquivoImportacao = formFile.FileName;
                        // full path to file in temp location
                        var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.

                        //var filePath = Path.GetFullPath(formFile.FileName);

                        //var filePath = RandomString(4) + "_"+ formFile.FileName;
                        filePaths.Add(filePath);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            //formFile.CopyToAsync(stream).ConfigureAwait(false);
                            formFile.CopyToAsync(stream);
                            //Aguarda 5 segundos ate o arquivo temporário ser criado
                            System.Threading.Thread.Sleep(5000);
                            using var package = new ExcelPackage(stream);
                            int qtdPlanilhas = package.Workbook.Worksheets.Count();
                            if (qtdPlanilhas > 0)
                            {
                                try
                                {
                                    ExcelWorksheet linha;
                                    totalLinhas = (int)package.Workbook.Worksheets[0].Dimension?.Rows;
                                    for (int i = 2; i <= totalLinhas; i++)
                                    {

                                        //Verificar se ja foi inserido, se sim, verificar se tem alguma alteracao
                                        linha = package.Workbook.Worksheets[0];
                                        l.GrupoUnidade = Convert.ToString(linha.Cells[i, 2].Value);
                                        l.NumeroPedido = Convert.ToString(linha.Cells[i, 3].Value);
                                        l.Produto = Convert.ToString(linha.Cells[i, 4].Value);
                                        l.DataPedido = ConverterData(Convert.ToString(linha.Cells[i, 6].Value), false);
                                        l.BovStatus = Convert.ToString(linha.Cells[i, 7].Value);
                                        l.DataStatus = ConverterData(Convert.ToString(linha.Cells[i, 8].Value), false);
                                        l.NumIdentidade = Convert.ToString(linha.Cells[i, 10].Value);
                                        l.NomeCliente = Convert.ToString(linha.Cells[i, 11].Value);
                                        l.SegmentoMercado = Convert.ToString(linha.Cells[i, 12].Value);
                                        l.TelefoneContato = Convert.ToString(linha.Cells[i, 13].Value);
                                        l.MetodoPagamento = Convert.ToString(linha.Cells[i, 14].Value);
                                        l.DiaVencimentoFatura = ConverterInteiro(Convert.ToString(linha.Cells[i, 15].Value));
                                        l.DataCorte = ConverterData(Convert.ToString(linha.Cells[i, 16].Value), false);
                                        l.TipoLogradouroInstalacao = Convert.ToString(linha.Cells[i, 17].Value);
                                        l.NomeLogradouroInstalacao = Convert.ToString(linha.Cells[i, 18].Value);
                                        l.NumeroInstalacao = Convert.ToString(linha.Cells[i, 19].Value);
                                        l.TipoCompInstalacao = Convert.ToString(linha.Cells[i, 20].Value);
                                        l.NumComp1Instalacao = Convert.ToString(linha.Cells[i, 21].Value);
                                        l.NumComp2Instalacao = Convert.ToString(linha.Cells[i, 22].Value);
                                        l.BairroInstalacao = Convert.ToString(linha.Cells[i, 24].Value);
                                        l.MunicipioInstalacao = Convert.ToString(linha.Cells[i, 25].Value);
                                        l.EstadoInstalacao = Convert.ToString(linha.Cells[i, 26].Value);
                                        l.CepInstalacao = Convert.ToString(linha.Cells[i, 27].Value);
                                        l.NumLocalidade = ConverterInteiro(Convert.ToString(linha.Cells[i, 28].Value));
                                        l.CodigoEstacao = Convert.ToString(linha.Cells[i, 29].Value);
                                        l.IdUnicoAcesso = Convert.ToString(linha.Cells[i, 30].Value);
                                        l.NumeroContrato = Convert.ToString(linha.Cells[i, 32].Value);
                                        l.AcessoGpon = Convert.ToString(linha.Cells[i, 33].Value);
                                        l.DddFixo = ConverterInteiro(Convert.ToString(linha.Cells[i, 34].Value));
                                        l.NumeroFixo = ConverterInteiro(Convert.ToString(linha.Cells[i, 35].Value));
                                        l.QtdPontosAdicionaisTv = Convert.ToString(linha.Cells[i, 38].Value);
                                        l.LinhaProduto = Convert.ToString(linha.Cells[i, 40].Value);
                                        l.Plano = Convert.ToString(linha.Cells[i, 41].Value);
                                        l.NomeOferta = Convert.ToString(linha.Cells[i, 44].Value);
                                        l.CodigoSap = Convert.ToString(linha.Cells[i, 49].Value);
                                        l.NumeroDocumento = Convert.ToString(linha.Cells[i, 55].Value);
                                        l.TipoPedido = Convert.ToString(linha.Cells[i, 57].Value);
                                        l.ClasseProduto = ConverterInteiro(Convert.ToString(linha.Cells[i, 58].Value));
                                        l.IdBundle = ConverterInteiro(Convert.ToString(linha.Cells[i, 62].Value));
                                        l.IndCombo = Convert.ToString(linha.Cells[i, 66].Value);
                                        l.SitBundle = Convert.ToString(linha.Cells[i, 72].Value);


                                        if (ValidaSeJaInseriuBOV(l.NumeroPedido, l.Produto))
                                        {
                                            try
                                            {
                                                _context.Bov.Add(l);
                                                _context.SaveChanges();
                                                l = new Bov();

                                            }
                                            catch (Exception ex)
                                            {
                                                _context.Bov.Local.Clear();
                                                //Gravar log
                                                InserirLog("BOV - INSTALAÇÃO", datahoraImportacao, "Erro", l.NumeroPedido, l.NomeCliente, i, ex.InnerException.Message);

                                            }
                                        }
                                        Startup.Progress = (int)(((float)i / (float)totalLinhas) * 100);
                                        //await Task.Delay(10); // It is only to make the process slower

                                    }
                                }
                                catch (Exception ex)
                                {
                                    InserirLog("BOV - INSTALAÇÃO", datahoraImportacao,"Erro", Convert.ToString(l.NumeroPedido), Convert.ToString(l.NomeCliente), 0, (ex.InnerException.Message==null?ex.Message: ex.InnerException.Message));
                                }
                            }
                            else
                            {
                                return new string("(P)O arquivo não foi carregado, favor repetir a operação!");
                            }
                        }
                    //}
                    //else
                    //{
                    //    return new string("O arquivo não foi carregado, favor repetir a operação!");
                    //}
                }
                InserirLog("BOV - INSTALAÇÃO", datahoraImportacao, "Resumo", nomeArquivoImportacao, "**********", totalLinhas-1, _context.LogImportacao.Where(l => l.Planilha == "BOV - INSTALAÇÃO" && l.DataHora == datahoraImportacao).Count().ToString());
                return new string("Importação concluída!");
            }
            catch (Exception ex)
            {
                return new string(ex.Message);
            }
        }

        public string upBovCancelamento(List<IFormFile> files)
        {
            try
            {
                //Seta a data e hora da importacao da planilha
                datahoraImportacao = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
                Bov l = new Bov();
                Startup.Progress = 0;
                int totalLinhas = 0;
                //int totalReadBytes = 0;

                var filePaths = new List<string>();
                foreach (var formFile in files)
                {
                    //if (files.Count() > 0)
                    //{
                        //Seta o nome do arquivo que serimportado
                        nomeArquivoImportacao = formFile.FileName;
                        // full path
                        var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
                        //var filePath = Path.GetFullPath(formFile.FileName);

                        //var filePath = RandomString(4) + "_"+ formFile.FileName;
                        filePaths.Add(filePath);
                        try
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                //formFile.CopyToAsync(stream).ConfigureAwait(false);
                                formFile.CopyToAsync(stream);
                                //Aguarda 5 segundos ate o arquivo temporário ser criado
                                System.Threading.Thread.Sleep(5000);

                                ExcelPackage package = new ExcelPackage(stream);
                                int qtdPlanilhas = package.Workbook.Worksheets.Count();
                                if (qtdPlanilhas > 0)
                                {
                                    try
                                    {
                                        ExcelWorksheet linha;
                                        totalLinhas = (int)package.Workbook.Worksheets[0].Dimension?.Rows;
                                        for (int i = 2; i <= totalLinhas; i++)
                                        {

                                            //Verificar se ja foi inserido, se sim, verificar se tem alguma alteracao
                                            linha = package.Workbook.Worksheets[0];
                                            l.GrupoUnidade = Convert.ToString(linha.Cells[i, 11].Value);
                                            l.NumeroPedido = Convert.ToString(linha.Cells[i, 2].Value);
                                            l.Produto = Convert.ToString(linha.Cells[i, 3].Value);
                                            l.DataPedido = ConverterData(Convert.ToString(linha.Cells[i, 4].Value), false);
                                            l.BovStatus = Convert.ToString(linha.Cells[i, 5].Value);
                                            l.DataStatus = ConverterData(Convert.ToString(linha.Cells[i, 6].Value), false);
                                            l.MotivoSituacaoPedido = Convert.ToString(linha.Cells[i, 7].Value);
                                            l.NumIdentidade = Convert.ToString(linha.Cells[i, 9].Value);
                                            l.NomeCliente = Convert.ToString(linha.Cells[i, 10].Value);
                                            //l.SegmentoMercado = Convert.ToString(linha.Cells[i, 12].Value);
                                            l.TelefoneContato = Convert.ToString(linha.Cells[i, 12].Value);
                                            //l.MetodoPagamento = Convert.ToString(linha.Cells[i, 14].Value);
                                            //l.DiaVencimentoFatura = ConverterInteiro(Convert.ToString(linha.Cells[i, 15].Value));
                                            //l.DataCorte = ConverterData(Convert.ToString(linha.Cells[i, 16].Value), false);
                                            l.TipoLogradouroInstalacao = Convert.ToString(linha.Cells[i, 13].Value);
                                            l.NomeLogradouroInstalacao = Convert.ToString(linha.Cells[i, 14].Value);
                                            l.NumeroInstalacao = Convert.ToString(linha.Cells[i, 15].Value);
                                            l.TipoCompInstalacao = Convert.ToString(linha.Cells[i, 16].Value);
                                            l.NumComp1Instalacao = Convert.ToString(linha.Cells[i, 17].Value);
                                            l.NumComp2Instalacao = Convert.ToString(linha.Cells[i, 18].Value);
                                            l.BairroInstalacao = Convert.ToString(linha.Cells[i, 20].Value);
                                            l.MunicipioInstalacao = Convert.ToString(linha.Cells[i, 21].Value);
                                            l.EstadoInstalacao = Convert.ToString(linha.Cells[i, 22].Value);
                                            l.CepInstalacao = Convert.ToString(linha.Cells[i, 23].Value);
                                            l.NumLocalidade = ConverterInteiro(Convert.ToString(linha.Cells[i, 24].Value));
                                            //l.CodigoEstacao = Convert.ToString(linha.Cells[i, 25].Value);
                                            //l.IdUnicoAcesso = Convert.ToString(linha.Cells[i, 30].Value);
                                            //l.NumeroContrato = Convert.ToString(linha.Cells[i, 32].Value);
                                            //l.AcessoGpon = Convert.ToString(linha.Cells[i, 33].Value);
                                            //l.DddFixo = ConverterInteiro(Convert.ToString(linha.Cells[i, 34].Value));
                                            //l.NumeroFixo = ConverterInteiro(Convert.ToString(linha.Cells[i, 35].Value));
                                            //l.QtdPontosAdicionaisTv = Convert.ToString(linha.Cells[i, 38].Value);
                                            l.LinhaProduto = Convert.ToString(linha.Cells[i, 36].Value);
                                            l.Plano = Convert.ToString(linha.Cells[i, 37].Value);
                                            l.NomeOferta = Convert.ToString(linha.Cells[i, 41].Value);
                                            l.CodigoSap = Convert.ToString(linha.Cells[i, 42].Value);
                                            l.NumeroDocumento = Convert.ToString(linha.Cells[i, 67].Value);
                                            l.TipoPedido = Convert.ToString(linha.Cells[i, 69].Value);
                                            //l.ClasseProduto = Convert.ToString(linha.Cells[i, 69].Value);
                                            l.IdBundle = ConverterInteiro(Convert.ToString(linha.Cells[i, 62].Value));
                                            //l.IndCombo = Convert.ToString(linha.Cells[i, 66].Value);
                                            //l.SitBundle = Convert.ToString(linha.C().s[i, 72].Value);


                                            if (ValidaSeJaInseriuBOV(l.NumeroPedido, l.Produto))
                                            {
                                                try
                                                {
                                                    _context.Bov.Add(l);
                                                    _context.SaveChanges();
                                                    l = new Bov();

                                                }
                                                catch (Exception ex)
                                                {
                                                    _context.Bov.Local.Clear();
                                                    //Gravar log
                                                    InserirLog("BOV - CANCELAMENTOS", datahoraImportacao,"Erro",l.NumeroPedido, l.NomeCliente, i, ex.InnerException.Message);
                                                }
                                            }
                                            Startup.Progress = (int)(((float)i / (float)totalLinhas) * 100);
                                            //await Task.Delay(10); // It is only to make the process slower

                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        InserirLog("BOV - CANCELAMENTOS", datahoraImportacao,"Erro", Convert.ToString(l.NumeroPedido), Convert.ToString(l.NomeCliente), 0, (ex.InnerException.Message == null ? ex.Message : ex.InnerException.Message));
                                    }
                                }
                                else
                                {
                                    return new string("(P)O arquivo não foi carregado, favor repetir a operação!" + package.Workbook.Worksheets.First().Name);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            return new string(ex.Message);
                        }
                    //}
                    //else
                    //{
                    //    return new string("O arquivo não foi carregado, favor repetir a operação!");
                    //}
                }
                InserirLog("BOV - CANCELAMENTOS", datahoraImportacao, "Resumo", nomeArquivoImportacao, "**********", totalLinhas-1, _context.LogImportacao.Where(l => l.Planilha == "BOV - CANCELAMENTOS" && l.DataHora == datahoraImportacao).Count().ToString());
                return new string("Importação concluída!");
            }
            catch (Exception ex)
            {
                return new string(ex.Message);
            }

        }

        public bool ValidaSeJaInseriuBOV(string numPedido, string produto)
        {
            //Validar duplicidade pelo CPF e nao pelo numero do pedido
            Bov valida = _context.Bov.FirstOrDefault(np => np.NumeroPedido == numPedido && np.Produto==produto);

            if (valida != null)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Click
        public string upClick(List<IFormFile> files)
        {
            try
            {
                int j = 0;
                listaC = new List<Click>();
                //Seta a data e hora da importacao da planilha
                datahoraImportacao = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
                Click l = new Click();
                Startup.Progress = 0;
                int totalLinhas = 0;
                //int totalReadBytes = 0;

                var filePaths = new List<string>();
                foreach (var formFile in files)
                {
                    //if (formFile.Count() > 0)
                    //{
                    //Seta o nome do arquivo que será importado
                    nomeArquivoImportacao = formFile.FileName;
                    // full path to file in temp location
                    var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
                                                           //FileInfo excel = new FileInfo(filePath);

                    //var filePath = Path.GetFullPath(formFile.FileName);
                    //var filePath = RandomString(4) + "_"+ formFile.FileName;
                    filePaths.Add(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        //formFile.CopyToAsync(stream).ConfigureAwait(false);
                        formFile.CopyToAsync(stream);
                        System.Threading.Thread.Sleep(5000);
                        //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        ExcelPackage package = new ExcelPackage(stream);
                        int qtdPlanilhas = package.Workbook.Worksheets.Count();
                        if (qtdPlanilhas > 0)
                        {
                            try
                            {
                                ExcelWorksheet linha;
                                totalLinhas = (int)package.Workbook.Worksheets[0].Dimension?.Rows;
                                for (int i = 2; i <= totalLinhas; i++)
                                {
                                    try
                                    {
                                    //Verificar se ja foi inserido, se sim, verificar se tem alguma alteracao
                                    linha = package.Workbook.Worksheets[0];
                                    l.DataCarga = ConverterData(Convert.ToString(linha.Cells[i, 1].Value),true);
                                    l.Atividade = Convert.ToString(linha.Cells[i, 2].Value);
                                    //l.GrupoUnidade = Convert.ToString(linha.Cells[i, 3].Value);
                                    l.Inicioagendamento = ConverterDataHora(Convert.ToString(linha.Cells[i, 4].Value),true);
                                    l.Fimagendamento = ConverterDataHora(Convert.ToString(linha.Cells[i, 5].Value), true);
                                    //l.Filial = Convert.ToString(linha.Cells[i, 6].Value);
                                    //l.Ddd = Convert.ToString(linha.Cells[i, 7].Value);
                                    //l.FilialIi = Convert.ToString(linha.Cells[i, 8].Value);
                                    //l.Gc = Convert.ToString(linha.Cells[i, 9].Value);
                                    //l.Gv = Convert.ToString(linha.Cells[i, 10].Value);
                                    //l.GrupoCanal = Convert.ToString(linha.Cells[i, 11].Value);
                                    //l.CodSap = ConverterInteiro(Convert.ToString(linha.Cells[i, 12].Value));
                                    //l.NomePdv = Convert.ToString(linha.Cells[i, 13].Value);
                                    l.NumOs = Convert.ToString(linha.Cells[i, 14].Value);
                                    //l.IndVendaConjunta = Convert.ToString(linha.Cells[i, 15].Value);
                                    //l.DataFechamento = Convert.ToString(linha.Cells[i, 16].Value);
                                    //l.DataFimPriAgendamento = Convert.ToString(linha.Cells[i, 17].Value);
                                    //l.DataFimUltAgendamento = Convert.ToString(linha.Cells[i, 18].Value);
                                    //l.QtdReagendamentoClick = Convert.ToString(linha.Cells[i, 19].Value);
                                    //l.Agendado = Convert.ToString(linha.Cells[i, 20].Value);
                                    //l.Codencerramento = Convert.ToString(linha.Cells[i, 21].Value);
                                    l.Estado = Convert.ToString(linha.Cells[i, 22].Value);
                                    l.Safra = Convert.ToString(linha.Cells[i, 23].Value);
                                    l.NomeMunicipio = Convert.ToString(linha.Cells[i, 24].Value);
                                    l.Gram = Convert.ToString(linha.Cells[i, 25].Value);
                                    l.Gra = Convert.ToString(linha.Cells[i, 26].Value);
                                    l.Matriculatecnico = Convert.ToString(linha.Cells[i, 27].Value);
                                    l.Tecnico = Convert.ToString(linha.Cells[i, 28].Value);
                                    //l.MatriculaVendedor = Convert.ToString(linha.Cells[i, 29].Value);
                                    l.Nrba = Convert.ToString(linha.Cells[i, 30].Value);
                                    //l.GcCarteira = Convert.ToString(linha.Cells[i, 31].Value);
                                    l.Contato1 = Convert.ToString(linha.Cells[i, 32].Value);
                                    l.Contato2 = Convert.ToString(linha.Cells[i, 33].Value);
                                    l.Contato3 = Convert.ToString(linha.Cells[i, 34].Value);
                                    l.Contatoefetivo = Convert.ToString(linha.Cells[i, 35].Value);
                                    l.NomeCliente = Convert.ToString(linha.Cells[i, 36].Value);
                                    l.CpfCliente = Convert.ToString(linha.Cells[i, 37].Value);
                                    //l.Coordenador = Convert.ToString(linha.Cells[i, 38].Value);
                                    //l.TelCoord = Convert.ToString(linha.Cells[i, 39].Value);
                                    //l.Prontoparaexecucao = Convert.ToString(linha.Cells[i, 40].Value);
                                    l.TipoLogr = Convert.ToString(linha.Cells[i, 41].Value);
                                    l.NomeLogr = Convert.ToString(linha.Cells[i, 42].Value);
                                    l.NumLoc = Convert.ToString(linha.Cells[i, 43].Value);
                                    l.TipoCompl = Convert.ToString(linha.Cells[i, 44].Value);
                                    l.Compl = Convert.ToString(linha.Cells[i, 45].Value).Replace("┤","");
                                    l.Bairro = Convert.ToString(linha.Cells[i, 46].Value);
                                    l.Cep = Convert.ToString(linha.Cells[i, 47].Value);
                                    l.NumLoc = Convert.ToString(linha.Cells[i, 48].Value);

                                    }
                                    catch (Exception ex)
                                    {
                                        InserirLog("CLICKABERTA", datahoraImportacao, "Erro", Convert.ToString(l.NomeCliente), Convert.ToString(l.NomeCliente), 0, (ex.InnerException.Message == null ? ex.Message : ex.InnerException.Message));
                                    }
                                    //if (ValidaSeJaInseriu(l.NumeroDoPedido))
                                    //{
                                    try
                                        {
                                            _context.Click.Add(l);
                                            _context.SaveChanges();
                                            l = new Click();

                                        }
                                        catch (Exception ex)
                                        {
                                            //_context.Oi360.Local.Clear();
                                            //Gravar log
                                            InserirLog("CLICKABERTAS", datahoraImportacao, "Erro", Convert.ToString(l.NomeCliente), l.NomeCliente, i, ex.InnerException.Message);
                                        }
                                    //}
                                    Startup.Progress = (int)(((float)i / (float)totalLinhas) * 100);
                                    //await Task.Delay(10); // It is only to make the process slower
                                }
                            }
                            catch (Exception ex)
                            {
                                InserirLog("CLICKABERTAS", datahoraImportacao, "Erro", Convert.ToString(l.Id), Convert.ToString(l.NomeCliente), 0, (ex.InnerException.Message == null ? ex.Message : ex.InnerException.Message));
                            }
                        }
                        else
                        {
                            return new string("(P)O arquivo não foi carregado, favor repetir a operação!");
                        }

                    }
                }

                InserirLog("CLICKABERTAS", datahoraImportacao, "Resumo", nomeArquivoImportacao, "**********", totalLinhas - 1, _context.LogImportacao.Where(l => l.Planilha == "CLICK" && l.DataHora == datahoraImportacao).Count().ToString());
                return new string("Importação concluída!");
            }
            catch (Exception ex)
            {
                return new string(ex.Message);
            }

        }

        public string upClickFechadas(List<IFormFile> files)
        {
            try
            {
                int j = 0;
                listaC = new List<Click>();
                //Seta a data e hora da importacao da planilha
                datahoraImportacao = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
                Click l = new Click();
                Startup.Progress = 0;
                int totalLinhas = 0;
                //int totalReadBytes = 0;

                var filePaths = new List<string>();
                foreach (var formFile in files)
                {
                    //if (formFile.Count() > 0)
                    //{
                    //Seta o nome do arquivo que será importado
                    nomeArquivoImportacao = formFile.FileName;
                    // full path to file in temp location
                    var filePath = Path.GetTempFileName(); 
                    
                    filePaths.Add(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        //formFile.CopyToAsync(stream).ConfigureAwait(false);
                        formFile.CopyToAsync(stream);
                        System.Threading.Thread.Sleep(5000);
                        //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        ExcelPackage package = new ExcelPackage(stream);
                        int qtdPlanilhas = package.Workbook.Worksheets.Count();
                        if (qtdPlanilhas > 0)
                        {
                            try
                            {
                                ExcelWorksheet linha;
                                totalLinhas = (int)package.Workbook.Worksheets[0].Dimension?.Rows;
                                for (int i = 2; i <= totalLinhas; i++)
                                {
                                    try
                                    {
                                        //Verificar se ja foi inserido, se sim, verificar se tem alguma alteracao
                                        linha = package.Workbook.Worksheets[0];
                                        l.DataCarga = ConverterData(Convert.ToString(linha.Cells[i, 1].Value), true);
                                        l.Atividade = Convert.ToString(linha.Cells[i, 2].Value);
                                        //l.GrupoUnidade = Convert.ToString(linha.Cells[i, 3].Value);
                                        //l.Inicioagendamento = ConverterDataHora(Convert.ToString(linha.Cells[i, 4].Value), true);
                                        //l.Fimagendamento = ConverterDataHora(Convert.ToString(linha.Cells[i, 5].Value), true);
                                        //l.Filial = Convert.ToString(linha.Cells[i, 6].Value);
                                        //l.Ddd = Convert.ToString(linha.Cells[i, 7].Value);
                                        //l.FilialIi = Convert.ToString(linha.Cells[i, 8].Value);
                                        //l.Gc = Convert.ToString(linha.Cells[i, 9].Value);
                                        //l.Gv = Convert.ToString(linha.Cells[i, 10].Value);
                                        //l.GrupoCanal = Convert.ToString(linha.Cells[i, 11].Value);
                                        //l.CodSap = ConverterInteiro(Convert.ToString(linha.Cells[i, 12].Value));
                                        //l.NomePdv = Convert.ToString(linha.Cells[i, 13].Value);
                                        l.NumOs = Convert.ToString(linha.Cells[i, 12].Value);
                                        //l.IndVendaConjunta = Convert.ToString(linha.Cells[i, 15].Value);
                                        //l.DataFechamento = Convert.ToString(linha.Cells[i, 16].Value);
                                        //l.DataFimPriAgendamento = Convert.ToString(linha.Cells[i, 17].Value);
                                        //l.DataFimUltAgendamento = Convert.ToString(linha.Cells[i, 18].Value);
                                        l.DescStatus = Convert.ToString(linha.Cells[i, 20].Value);
                                        l.StatusFechada = Convert.ToString(linha.Cells[i, 21].Value);
                                        //l.Estado = Convert.ToString(linha.Cells[i, 20].Value);
                                        //l.Agendado = Convert.ToString(linha.Cells[i, 20].Value);
                                        //l.Codencerramento = Convert.ToString(linha.Cells[i, 21].Value);
                                        //l.Estado = Convert.ToString(linha.Cells[i, 20].Value);
                                        l.Safra = Convert.ToString(linha.Cells[i, 24].Value);
                                        l.NomeMunicipio = Convert.ToString(linha.Cells[i, 25].Value);
                                        l.Gram = Convert.ToString(linha.Cells[i, 26].Value);
                                        l.Gra = Convert.ToString(linha.Cells[i, 27].Value);
                                        l.NomeCliente = Convert.ToString(linha.Cells[i, 28].Value);
                                        l.CpfCliente = Convert.ToString(linha.Cells[i, 29].Value);

                                        l.Matriculatecnico = Convert.ToString(linha.Cells[i, 30].Value);
                                        l.Tecnico = Convert.ToString(linha.Cells[i, 31].Value);
                                        //l.MatriculaVendedor = Convert.ToString(linha.Cells[i, 29].Value);
                                        l.Nrba = Convert.ToString(linha.Cells[i, 34].Value);
                                        //l.GcCarteira = Convert.ToString(linha.Cells[i, 31].Value);
                                        l.Contato1 = Convert.ToString(linha.Cells[i, 36].Value);
                                        l.Contato2 = Convert.ToString(linha.Cells[i, 37].Value);
                                        l.Contato3 = Convert.ToString(linha.Cells[i, 38].Value);

                                        //l.Contatoefetivo = Convert.ToString(linha.Cells[i, 35].Value);
                                        
                                        //l.Coordenador = Convert.ToString(linha.Cells[i, 38].Value);
                                        //l.TelCoord = Convert.ToString(linha.Cells[i, 39].Value);
                                        //l.Prontoparaexecucao = Convert.ToString(linha.Cells[i, 40].Value);
                                        //l.TipoLogr = Convert.ToString(linha.Cells[i, 41].Value);
                                        //l.NomeLogr = Convert.ToString(linha.Cells[i, 42].Value);
                                        //l.NumLoc = Convert.ToString(linha.Cells[i, 43].Value);
                                        //l.TipoCompl = Convert.ToString(linha.Cells[i, 44].Value);
                                        //l.Compl = Convert.ToString(linha.Cells[i, 45].Value).Replace("┤", "");
                                        //l.Bairro = Convert.ToString(linha.Cells[i, 46].Value);
                                        //l.Cep = Convert.ToString(linha.Cells[i, 47].Value);
                                        //l.NumLoc = Convert.ToString(linha.Cells[i, 48].Value);

                                    }
                                    catch (Exception ex)
                                    {
                                        InserirLog("CLICKFECHADAS", datahoraImportacao, "Erro", Convert.ToString(l.NomeCliente), Convert.ToString(l.NomeCliente), 0, (ex.InnerException.Message == null ? ex.Message : ex.InnerException.Message));
                                    }
                                    //if (ValidaSeJaInseriu(l.NumeroDoPedido))
                                    //{
                                    try
                                    {
                                        _context.Click.Add(l);
                                        _context.SaveChanges();
                                        l = new Click();

                                    }
                                    catch (Exception ex)
                                    {
                                        //_context.Oi360.Local.Clear();
                                        //Gravar log
                                        InserirLog("CLICKFECHADAS", datahoraImportacao, "Erro", Convert.ToString(l.NomeCliente), l.NomeCliente, i, ex.InnerException.Message);
                                    }
                                    //}
                                    Startup.Progress = (int)(((float)i / (float)totalLinhas) * 100);
                                    //await Task.Delay(10); // It is only to make the process slower
                                }
                            }
                            catch (Exception ex)
                            {
                                InserirLog("CLICKFECHADAS", datahoraImportacao, "Erro", Convert.ToString(l.NomeCliente), Convert.ToString(l.NomeCliente), 0, (ex.InnerException.Message == null ? ex.Message : ex.InnerException.Message));
                            }
                        }
                        else
                        {
                            return new string("(P)O arquivo não foi carregado, favor repetir a operação!");
                        }

                    }
                }

                InserirLog("CLICKFECHADAS", datahoraImportacao, "Resumo", nomeArquivoImportacao, "**********", totalLinhas - 1, _context.LogImportacao.Where(l => l.Planilha == "CLICK" && l.DataHora == datahoraImportacao).Count().ToString());
                return new string("Importação concluída!");
            }
            catch (Exception ex)
            {
                return new string(ex.Message);
            }

        }
        #endregion


    }
} 
