using NLog;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using VitrineVirtual.Data;
using VitrineVirtual.Model;


namespace VitrineVirtual.WEB.Controllers
{
    //[Authorize(Roles = "Administrator,Logista")]
    public class CadastroProdutoLojaController : Controller
    {
        private VitrineVirtualDBContext db = new VitrineVirtualDBContext();
        Logger log = LogManager.GetCurrentClassLogger();
        Page page = new Page();

        public ActionResult Index()
        {
            // TODO: Pegar usuário cliente campo texto login, checar empresa usuário pertence e gravar cookie
            // Também pode usar redis de banco
            var listaProdutos = db.CadastroProdutoLoja;
            //ViewBag.Empresas = new SelectList(db.Empresas.ToList(), "ID_Empresa", "Nome_Fantasia");
            return View(listaProdutos);
        }

        /// <summary>
        /// Método resposável de filtrar por produto.
        /// </summary>
        /// <param name="pesquisa"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FiltrarPorNome(string pesquisa)
        {
            try
            {
                var produtoSelecionado = db.CadastroProdutoLoja.Where(a => a.Nome_Produto.Contains(pesquisa)).OrderBy(a => a.Nome_Produto).ToList();
                return View(produtoSelecionado);
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "swalFalhou();", true);
                Log.Info(ex.Message);
                return RedirectToAction("Index");
            }

        }

        // GET: CADASTRO_PRODUTO_LOJA/Details/5
        public ActionResult Details(int? id)
        {
            Cadastro_Produto_Loja cadastroProdutoLoja;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                cadastroProdutoLoja = db.CadastroProdutoLoja.Find(id);

                this.page.Session.Add("ID_Cadastro_Prod_Loja", cadastroProdutoLoja.ID_Cadastro_Prod_Loja);
                this.page.Session.Add("Nome_Produto", cadastroProdutoLoja.Nome_Produto);
                this.page.Session.Add("Preco_Produto", cadastroProdutoLoja.Preco_Produto);
                this.page.Session.Add("Preco_Desconto", cadastroProdutoLoja.Preco_Desconto);
                this.page.Session.Add("Quantidade_Produto", cadastroProdutoLoja.Quantidade_Produto);
                this.page.Session.Add("Modelo_Produto", cadastroProdutoLoja.Modelo_Produto);

                this.page.Session.Add("Item_Compra", cadastroProdutoLoja);


                var usuario = Session["ID_Usuario"].ToString();

                var dadosUsuario = db.Usuarios.Where(x => x.id == usuario).ToList();
                //var dadosUsuario = db.Usuarios.SingleOrDefault(x => x.id == usuario);

                if (dadosUsuario.Count != 0)
                {
                    this.page.Session.Add("Nome_Completo", dadosUsuario[0].Nome_Completo);
                    this.page.Session.Add("Endereco", dadosUsuario[0].Endereco);
                    this.page.Session.Add("Numero_Endereco", dadosUsuario[0].Numero_Endereco);
                    this.page.Session.Add("Municipio", dadosUsuario[0].Municipio);
                    this.page.Session.Add("Cep", dadosUsuario[0].Cep);
                    this.page.Session.Add("Telefone", dadosUsuario[0].Telefone);
                    this.page.Session.Add("UF", dadosUsuario[0].UF);

                }

            }
            catch (Exception ex)
            {

                log.Log(LogLevel.Error)
                  .Exception(ex)
                  .Message("Mensagem de log {0} parametro", 1).Write();
                return View();
            }

            if (cadastroProdutoLoja == null)
            {
                return HttpNotFound();
            }
            return View(cadastroProdutoLoja);

        }

        // GET: CADASTRO_PRODUTO_LOJA/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: CADASTRO_PRODUTO_LOJA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Cadastro_Prod_Loja,Nome_Produto,Modelo_Produto,Descricao_Produto,Preco_Produto,Quantidade_Produto,Status_Produto,Categoria_Produto")]Cadastro_Produto_Loja cadastro_Produto_Loja, HttpPostedFileBase upload1, HttpPostedFileBase upload2, HttpPostedFileBase upload3, HttpPostedFileBase upload4)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (upload1 != null && upload1.ContentLength > 0)
                    {
                        var foto = new FilePath
                        {
                            Nome_Arquivo = Guid.NewGuid().ToString() + Path.GetExtension(upload1.FileName),
                            Tipo_Arquivo = Path.GetExtension(upload1.FileName)
                        };
                        foto.Caminho_Arquivo = Path.Combine(Server.MapPath("~/images/uploads"), foto.Nome_Arquivo);
                        upload1.SaveAs(foto.Caminho_Arquivo);
                        cadastro_Produto_Loja.FilePath = new List<FilePath>();
                        cadastro_Produto_Loja.FilePath.Add(foto);
                    }

                    if (upload2 != null && upload2.ContentLength > 0)
                    {
                        var foto = new FilePath
                        {
                            Nome_Arquivo_2 = Guid.NewGuid().ToString() + Path.GetExtension(upload2.FileName),
                            Tipo_Arquivo = Path.GetExtension(upload2.FileName)
                        };
                        foto.Caminho_Arquivo = Path.Combine(Server.MapPath("~/images/uploads"), foto.Nome_Arquivo_2);
                        upload2.SaveAs(foto.Caminho_Arquivo);
                        cadastro_Produto_Loja.FilePath = new List<FilePath>();
                        cadastro_Produto_Loja.FilePath.Add(foto);
                    }

                    if (upload3 != null && upload3.ContentLength > 0)
                    {
                        var foto = new FilePath
                        {
                            Nome_Arquivo_3 = Guid.NewGuid().ToString() + Path.GetExtension(upload3.FileName),
                            Tipo_Arquivo = Path.GetExtension(upload3.FileName)
                        };
                        foto.Caminho_Arquivo_3 = Path.Combine(Server.MapPath("~/images/uploads"), foto.Nome_Arquivo_3);
                        upload3.SaveAs(foto.Caminho_Arquivo_3);
                        cadastro_Produto_Loja.FilePath = new List<FilePath>();
                        cadastro_Produto_Loja.FilePath.Add(foto);
                    }

                    if (upload4 != null && upload4.ContentLength > 0)
                    {
                        var foto = new FilePath
                        {
                            Nome_Arquivo_4 = Guid.NewGuid().ToString() + Path.GetExtension(upload4.FileName),
                            Tipo_Arquivo = Path.GetExtension(upload4.FileName)
                        };
                        foto.Caminho_Arquivo_4 = Path.Combine(Server.MapPath("~/images/uploads"), foto.Nome_Arquivo_4);
                        upload4.SaveAs(foto.Caminho_Arquivo_4);
                        cadastro_Produto_Loja.FilePath = new List<FilePath>();
                        cadastro_Produto_Loja.FilePath.Add(foto);
                    }


                    db.CadastroProdutoLoja.Add(cadastro_Produto_Loja);
                    db.SaveChanges();

                    var path = Path.Combine(Server.MapPath("~/images/temp"));
                    FileInfo file = new FileInfo(path);
                    if (file.Exists)
                        file.Delete();

                    ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "swalSucesso();", true);
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "swalFalhou();", true);
                    log.Log(LogLevel.Error)
                      .Exception(ex)
                      .Message("Mensagem de log {0} parametro", 1).Write();
                    return RedirectToAction("Index");
                }
            }

            return View(cadastro_Produto_Loja);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cadastro_Produto_Loja cadastro_Produto_Loja = db.CadastroProdutoLoja.Find(id);
            if (cadastro_Produto_Loja == null)
            {
                return HttpNotFound();
            }
            return View(cadastro_Produto_Loja);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int? id, HttpPostedFileBase upload1, HttpPostedFileBase upload2, HttpPostedFileBase upload3, HttpPostedFileBase upload4)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cadProdLojaToUpdate = db.CadastroProdutoLoja.Find(id);
            if (TryUpdateModel(cadProdLojaToUpdate, "",
               new string[] { "ID_Cadastro_Prod_Loja,Nome_Produto,Modelo_Produto,Descricao_Produto,Preco_Produto,Quantidade_Produto,Status_Produto,Categoria_Produto" }))

            {
                try
                {
                    if (upload1 != null && upload1.ContentLength > 0)
                    {
                        var foto = new FilePath
                        {
                            Nome_Arquivo = Guid.NewGuid().ToString() + Path.GetExtension(upload1.FileName),
                            Tipo_Arquivo = Path.GetExtension(upload1.FileName)
                        };
                        foto.Caminho_Arquivo = Path.Combine(Server.MapPath("~/images/uploads"), foto.Nome_Arquivo);
                        cadProdLojaToUpdate.FilePath = new List<FilePath> { foto };
                        upload1.SaveAs(foto.Caminho_Arquivo);
                    }

                    if (upload2 != null && upload2.ContentLength > 0)
                    {
                        var foto = new FilePath
                        {
                            Nome_Arquivo_2 = Guid.NewGuid().ToString() + Path.GetExtension(upload2.FileName),
                            Tipo_Arquivo = Path.GetExtension(upload2.FileName)
                        };
                        foto.Caminho_Arquivo_2 = Path.Combine(Server.MapPath("~/images/uploads"), foto.Nome_Arquivo_2);
                        cadProdLojaToUpdate.FilePath = new List<FilePath> { foto };
                        upload2.SaveAs(foto.Caminho_Arquivo_2);
                    }

                    if (upload3 != null && upload3.ContentLength > 0)
                    {
                        var foto = new FilePath
                        {
                            Nome_Arquivo_3 = Guid.NewGuid().ToString() + Path.GetExtension(upload3.FileName),
                            Tipo_Arquivo = Path.GetExtension(upload3.FileName)
                        };
                        foto.Caminho_Arquivo_3 = Path.Combine(Server.MapPath("~/images/uploads"), foto.Nome_Arquivo_3);
                        cadProdLojaToUpdate.FilePath = new List<FilePath> { foto };
                        upload3.SaveAs(foto.Caminho_Arquivo_3);
                    }

                    if (upload4 != null && upload4.ContentLength > 0)
                    {
                        var foto = new FilePath
                        {
                            Nome_Arquivo_4 = Guid.NewGuid().ToString() + Path.GetExtension(upload4.FileName),
                            Tipo_Arquivo = Path.GetExtension(upload4.FileName)
                        };
                        foto.Caminho_Arquivo_4 = Path.Combine(Server.MapPath("~/images/uploads"), foto.Nome_Arquivo_4);
                        cadProdLojaToUpdate.FilePath = new List<FilePath> { foto };
                        upload4.SaveAs(foto.Caminho_Arquivo_4);
                    }

                    db.Entry(cadProdLojaToUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "swalSucesso();", true);
                    Log.Info("Registro alterado com sucesso");
                    return RedirectToAction("Edit");

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "swalFalhou();", true);
                    log.Log(LogLevel.Error).Exception(ex).Message("Mensagem de log {0} parametro", 1).Write();
                    return RedirectToAction("Index");
                }
            }

            return View(cadProdLojaToUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Cadastro_Produto_Loja cadastro_Produto_Loja = db.CadastroProdutoLoja.Find(id);
                var caminhoArquivo = db.FilePath.Where(x => x.ID_Cadastro_Prod_Loja == id).ToList();
                if (caminhoArquivo.Count > 0)
                {
                    for (int i = 0; i < caminhoArquivo.Count; i++)
                    {
                        if (caminhoArquivo[i].Caminho_Arquivo != null)
                            System.IO.File.Delete(caminhoArquivo[i].Caminho_Arquivo);

                        db.FilePath.Remove(caminhoArquivo[i]);
                    }
                }

                db.CadastroProdutoLoja.Remove(cadastro_Produto_Loja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log.Log(LogLevel.Error)
                  .Exception(ex)
                  .Message("Mensagem de log {0} parametro", 1).Write();
                return View("Index");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteFoto(int id, string nomeFoto)
        {
            try
            {
                Cadastro_Produto_Loja cadastro_Produto_Loja = db.CadastroProdutoLoja.Find(id);
                var caminhoArquivo = db.FilePath.Where(x => x.ID_Cadastro_Prod_Loja == id).Where(x => x.Nome_Arquivo == nomeFoto).ToList();
                if (caminhoArquivo.Count > 0)
                {
                    if (caminhoArquivo[0].Caminho_Arquivo != null)
                        System.IO.File.Delete(caminhoArquivo[0].Caminho_Arquivo);

                    db.FilePath.Remove(caminhoArquivo[0]);
                }

                //db.CadastroProdutoLoja.Remove(cadastro_Produto_Loja);
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            catch (Exception ex)
            {
                log.Log(LogLevel.Error)
                  .Exception(ex)
                  .Message("Mensagem de log {0} parametro", 1).Write();
                return View("Index");
            }
        }

        #region Trata Imagens

        public ActionResult GetImage(int id)
        {
            Cadastro_Produto_Loja cadastro_Produto_Loja = db.CadastroProdutoLoja.Find(id);
            var caminhoArquivo = db.FilePath.Where(x => x.ID_Cadastro_Prod_Loja == id).ToList();
            FilePath foto;
            Utils utils = new Utils();
            try
            {
                for (int i = 0; i < caminhoArquivo.Count; i++)
                {

                    foto = new FilePath
                    {
                        ID_Cadastro_Prod_Loja = caminhoArquivo[i].ID_Cadastro_Prod_Loja,
                        Nome_Arquivo = caminhoArquivo[i].Nome_Arquivo,
                        Tipo_Arquivo = caminhoArquivo[i].Tipo_Arquivo,
                        ID_Caminho = Convert.ToInt32(caminhoArquivo[0].ID_Caminho),
                        Caminho_Arquivo = Path.Combine(@"~\images\uploads", caminhoArquivo[i].Nome_Arquivo)
                    };
                    string caminho = "caminho_" + (i + 1);
                    string arqFoto = "arqFoto_" + (i + 1);
                    Session[caminho] = foto;
                    Session[arqFoto] = caminhoArquivo[i].Nome_Arquivo;
                    ViewData[caminho] = caminhoArquivo[i].Nome_Arquivo;

                    ViewBag.arqFoto = TempData[arqFoto];

                    //To maintain the state of TempData  
                    TempData.Keep();

                    cadastro_Produto_Loja.FilePath.Add(foto);
                }

                Session["Foto"] = cadastro_Produto_Loja;

                return View("Edit", cadastro_Produto_Loja);
            }
            catch (Exception ex)
            {

                log.Log(LogLevel.Error)
                  .Exception(ex)
                  .Message("Mensagem de log {0} parametro", 1).Write();
                return View();
            }


        }

        [HttpPost]
        public bool InserirFotoTempDir(string diretorio)
        {
            string fName = string.Empty; ;
            string uploadpath = string.Empty;
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        var path = Path.Combine(Server.MapPath("~/images/temp/" + diretorio));
                        string pathString = System.IO.Path.Combine(path.ToString());
                        var fileName1 = Path.GetFileName(file.FileName);
                        uploadpath = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(uploadpath);
                    }
                }
                return true; ;
            }
            catch (Exception ex)
            {
                log.Log(LogLevel.Error)
                      .Exception(ex)
                      .Message("Mensagem de log {0} parametro", 1).Write();
                return false;
            }
        }

        #endregion

        [HttpPost]
        public ActionResult AtualizaPreco(int id, string quantidade, string preco)
        {
            try
            {
                var cadProdLojaToUpdate = db.CadastroProdutoLoja.Find(id);

                int qtdItem = Convert.ToInt32(quantidade);
                decimal precoProd = Convert.ToDecimal(preco.Replace(".",","));
                if (precoProd > 0)
                {
                    if (qtdItem == 0) { qtdItem = 1; }
                    decimal precoFinal = precoProd * qtdItem;
                    cadProdLojaToUpdate.Preco_Produto = precoFinal;
                    //db.Entry(cadProdLojaToUpdate).State = EntityState.Modified;
                    //db.SaveChanges();
                    TempData.Add("Atualiza_Valor", precoFinal);
                    return Content(precoFinal.ToString());
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                log.Log(LogLevel.Warn)
                  .Exception(ex)
                  .Message("Mensagem de log {0} parametro", 1).Write();
                return null;
            }
        }

        public static List<SelectListItem> GetDropDown()
        {
            List<SelectListItem> lstEmpresas = new List<SelectListItem>();
            VitrineVirtualDBContext dbContext = new VitrineVirtualDBContext();

            var empresas = dbContext.Empresas.ToList();
            foreach (var empresa in empresas)
            {
                lstEmpresas.Add(new SelectListItem() { Text = empresa.Nome_Fantasia, Value = empresa.ID_Empresa.ToString() });
            }
            return lstEmpresas;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Controllers Menu Lateral

        public ActionResult CatalogoItens(string categoria, string nome)
        {
            try
            {

                List<Cadastro_Produto_Loja> lstModaFeminina = new List<Cadastro_Produto_Loja>();
                var grupoCategoria = db.Empresas
                    .Join(db.CadastroProdutoLoja, cad => cad.ID_Empresa, emp => emp.ID_Empresa,
                    (cad, emp) => new { Cadastro_Produto_Loja = cad, CUA_Empresas = emp })
                    .Where(cad => cad.CUA_Empresas.Categoria_Produto == categoria)
                    .Where(emp => emp.Cadastro_Produto_Loja.Nome_Fantasia == nome)
                    .Select(emp => emp.Cadastro_Produto_Loja.Nome_Fantasia).FirstOrDefault();

                return Content(grupoCategoria);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return View();
            }

        }

        #endregion
    }
}
