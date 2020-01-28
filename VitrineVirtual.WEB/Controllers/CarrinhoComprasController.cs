using CorreiosPrecosEPrazo.Correios;
using NLog;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using VitrineVirtual.Data;
using VitrineVirtual.Model;

namespace VitrineVirtual.WEB.Controllers
{
    public class CarrinhoComprasController : Controller
    {
        private VitrineVirtualDBContext db = new VitrineVirtualDBContext();
        Logger log = LogManager.GetCurrentClassLogger();
        Page page = new Page();

        // GET: Carrinho
        public ActionResult Index()
        {

            try
            {
                string idUsuario = Session["ID_Usuario"].ToString();
                var itensCarrinho = db.CarrinhoCompras.Where(x => x.ID_Usuario == idUsuario).ToList();

                if (idUsuario == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                if (itensCarrinho == null) return HttpNotFound();

                if (itensCarrinho != null)
                {
                    ViewData["Qtd_Itens"] = itensCarrinho.Count;
                    decimal? total = 0;
                    total = itensCarrinho.Select(x => x.Preco_Produto).Sum();
                    this.page.Session.Add("Total", total);
                    this.page.Session.Add("Total_Frete", 0);
                    ViewData["Total"] = total;
                    TempData["Total"] = total;

                    return View(itensCarrinho);
                }
                else
                    return View();

            }
            catch (Exception ex)
            {
                var result = ex.InnerException.ToString();
                log.Log(LogLevel.Error)
                  .Exception(ex)
                  .Message("Mensagem de log {0} parametro", 1).Write();
                return RedirectToAction("Index");
            }
        }

        // GET: Carrinho_Compras/Details/5
        public ActionResult Details()
        {
            try
            {
                List<Carrinho_Compras> lstCarrinho = new List<Carrinho_Compras>();
                string idUsuario = Session["ID_Usuario"].ToString();
                var itensCarrinho = db.CarrinhoCompras.Where(x => x.ID_Usuario == idUsuario).ToList();

                if (idUsuario == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (itensCarrinho == null)
                {
                    return HttpNotFound();
                }

                if (itensCarrinho != null)
                {
                    ViewData["Qtd_Itens"] = itensCarrinho.Count;
                    decimal? precoProduto = 0;
                    decimal? total = 0;
                    foreach (var item in itensCarrinho)
                    {
                        Carrinho_Compras entCarrinho = new Carrinho_Compras();
                        entCarrinho.ID_Carrinho_Compras = item.ID_Carrinho_Compras;
                        entCarrinho.ID_Cadastro_Prod_Loja = item.ID_Cadastro_Prod_Loja;
                        entCarrinho.ID_Usuario = idUsuario;
                        entCarrinho.Nome_Produto = item.Nome_Produto;
                        entCarrinho.Modelo_Produto = item.Modelo_Produto;
                        entCarrinho.Preco_Produto = item.Preco_Produto;
                        entCarrinho.Quantidade_Produto = item.Quantidade_Produto;
                        entCarrinho.Descricao_Produto = item.Descricao_Produto;
                        entCarrinho.Caminho_Arquivo = item.Caminho_Arquivo;
                        precoProduto = item.Preco_Produto;
                        total = total + precoProduto;
                        entCarrinho.Total = total;

                        lstCarrinho.Add(entCarrinho);
                    }

                    ViewData["Total"] = total;
                    TempData["Total"] = total;
                    ViewData["CarrinhoCompras"] = lstCarrinho;
                    this.page.Session.Add("Total", total);

                    return View(lstCarrinho);
                }

                else
                    return View();

            }
            catch (Exception ex)
            {
                var result = ex.InnerException.ToString();
                log.Log(LogLevel.Error)
                  .Exception(ex)
                  .Message("Mensagem de log {0} parametro", 1).Write();
                return RedirectToAction("Index");
            }
        }

        // GET: Carrinho_Compras/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carrinho_Compras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Carrinho_Compras,ID_Cadastro_Prod_Loja,ID_Usuario,Nome_Produto,Modelo_Produto,Descricao_Produto,Preco_Produto,Quantidade_Produto")] Carrinho_Compras carrinhoCompras)
        {
            if (ModelState.IsValid)
            {
                int idCadProd = (int)Session["ID_Cadastro_Prod_Loja"];
                var cadProdLoja = db.CadastroProdutoLoja.Find(idCadProd);

                var caminhoArquivo = db.FilePath.FirstOrDefault(x => x.ID_Cadastro_Prod_Loja == idCadProd).Caminho_Arquivo;
                var Caminho_Arquivo = Path.Combine(@"~\images\uploads", caminhoArquivo);

                try
                {
                    carrinhoCompras.ID_Cadastro_Prod_Loja = idCadProd;
                    carrinhoCompras.ID_Usuario = Session["ID_Usuario"].ToString();
                    carrinhoCompras.Nome_Produto = cadProdLoja.Nome_Produto;
                    carrinhoCompras.Modelo_Produto = cadProdLoja.Nome_Produto;
                    carrinhoCompras.Preco_Produto = cadProdLoja.Preco_Produto;
                    carrinhoCompras.Quantidade_Produto = cadProdLoja.Quantidade_Produto;
                    carrinhoCompras.Descricao_Produto = cadProdLoja.Descricao_Produto;
                    carrinhoCompras.Caminho_Arquivo = Caminho_Arquivo;

                    db.CarrinhoCompras.Add(carrinhoCompras);
                    db.SaveChanges();
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "swalSucesso();", true);

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "swalFalhou();", true);
                    var result = ex.InnerException.ToString();
                    log.Log(LogLevel.Error)
                      .Exception(ex)
                      .Message("Mensagem de log {0} parametro", 1).Write();
                    return RedirectToAction("Details");
                }
                return View(carrinhoCompras);
            }
            else
                return RedirectToAction("Details");
        }

        // GET: Carrinho_Compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrinho_Compras carrinho_Compras = db.CarrinhoCompras.Find(id);
            if (carrinho_Compras == null)
            {
                return HttpNotFound();
            }
            return View(carrinho_Compras);
        }

        // POST: Carrinho_Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Carrinho_Compras carrinho_Compras = db.CarrinhoCompras.Find(id);
                db.CarrinhoCompras.Remove(carrinho_Compras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log.Log(LogLevel.Error)
                    .Exception(ex)
                    .Message("Mensagem de log {0} parametro", 1).Write();
                return RedirectToAction("Index");
            }

        }

        public ActionResult GetImagePath(int id)
        {
            Cadastro_Produto_Loja cadastro_Produto_Loja = db.CadastroProdutoLoja.Find(id);
            var caminhoArquivo = db.FilePath.FirstOrDefault(x => x.ID_Cadastro_Prod_Loja == id);

            var Caminho_Arquivo = Path.Combine(@"~\images\uploads", caminhoArquivo.Nome_Arquivo);
            Session["Caminho_Arquivo"] = caminhoArquivo.Nome_Arquivo;

            return View("Edit", cadastro_Produto_Loja);
        }

        [HttpPost]
        public bool AdicionaAoCarrinho()
        {
            if (ModelState.IsValid)
            {
                Carrinho_Compras carrinhoCompras = new Carrinho_Compras();
                int idCadProd = (int)Session["ID_Cadastro_Prod_Loja"];
                var cadProdLoja = db.CadastroProdutoLoja.Find(idCadProd);

                var caminhoArquivo = db.FilePath.FirstOrDefault(x => x.ID_Cadastro_Prod_Loja == idCadProd).Caminho_Arquivo;
                var Caminho_Arquivo = Path.Combine(@"~\images\uploads", caminhoArquivo);

                try
                {
                    carrinhoCompras.ID_Cadastro_Prod_Loja = idCadProd;
                    carrinhoCompras.ID_Usuario = Session["ID_Usuario"].ToString();
                    carrinhoCompras.Nome_Produto = cadProdLoja.Nome_Produto;
                    carrinhoCompras.Modelo_Produto = cadProdLoja.Modelo_Produto;
                    carrinhoCompras.Preco_Produto = cadProdLoja.Preco_Produto;
                    carrinhoCompras.Quantidade_Produto = cadProdLoja.Quantidade_Produto;
                    carrinhoCompras.Descricao_Produto = cadProdLoja.Descricao_Produto;
                    carrinhoCompras.Caminho_Arquivo = Caminho_Arquivo;
                    carrinhoCompras.Total = cadProdLoja.Preco_Produto;
                    carrinhoCompras.Valor_Frete = 1;

                    db.CarrinhoCompras.Add(carrinhoCompras);
                    db.SaveChanges();
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "swalSucesso();", true);

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "swalFalhou();", true);
                    var result = ex.InnerException.ToString();
                    log.Log(LogLevel.Error)
                      .Exception(ex)
                      .Message("Mensagem de log {0} parametro", 1).Write();
                    return false;
                }
                return true;
            }
            else
                return false;
        }

        [HttpPost]
        public ActionResult ExcluirItemCarrinho(int idCarrinho)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var cadProdLoja = db.CarrinhoCompras.Find(idCarrinho);

                    if (cadProdLoja != null)
                    {
                        db.CarrinhoCompras.Remove(cadProdLoja);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                        return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "swalFalhou();", true);
                    var result = ex.InnerException.ToString();
                    log.Log(LogLevel.Error)
                      .Exception(ex)
                      .Message("Mensagem de log {0} parametro", 1).Write();
                    return RedirectToAction("Index");
                }
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CalculaFrete(string cep, string preco, string tipoFrete, string pesoItens)
        {
            string frete = string.Empty;
            string prazo = string.Empty;

            try
            {
                List<Carrinho_Compras> lstCarrinho = new List<Carrinho_Compras>();

                decimal valorItens = Convert.ToDecimal(preco.Replace(".", ","));  //"40010,41106"
                var consultaPreco = Consulta.ConsultarPrecos("", "", tipoFrete, "01021-200", cep, pesoItens, 10, 30, 20, 20, 20, "N", valorItens, "N");
                var consultaPrazo = Consulta.ConsultarPrazo("01021-200", cep, tipoFrete);

                if (consultaPreco != null)
                {
                    decimal valorFrete = Convert.ToDecimal(consultaPreco.servicos.CServico[0].Valor);
                    decimal totalComFrete = Convert.ToDecimal(preco) + valorFrete;

                    decimal codigo = Convert.ToDecimal(consultaPreco.servicos.CServico[0].Codigo);
                    string msgErro = consultaPreco.servicos.CServico[0].MsgErro;

                    decimal pac = Convert.ToDecimal(consultaPreco.servicos.CServico[0].Valor);

                    string dataEntrega = consultaPrazo.servicos.CServico[0].DataMaxEntrega;

                    TempData.Remove("Total_Frete");
                    TempData["Frete"] = pac;
                    TempData["Total_Frete"] = totalComFrete;
                    TempData["Data_Entrega"] = dataEntrega;
                    return Content(totalComFrete.ToString());
                }
                else
                {
                    frete = consultaPreco.servicos.CServico[0].MsgErro;
                    prazo = consultaPrazo.servicos.CServico[0].MsgErro;
                    log.Log(LogLevel.Error).Message("Mensagem de log {0} parametro", frete + " - " + prazo).Write();
                    return null;
                }

            }
            catch (Exception ex)
            {
                log.Log(LogLevel.Error)
                  .Exception(ex)
                  .Message("Mensagem de log {0} parametro", 1).Write();
                return null;
            }

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
