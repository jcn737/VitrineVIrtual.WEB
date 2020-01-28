using Newtonsoft.Json;
using NLog;
using NLog.Fluent;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using System.Web.UI;
using VitrineVirtual.Data;
using VitrineVirtual.Model;

namespace VitrineVirtual.WEB.Controllers
{
    [AllowAnonymous]
    [Authorize(Roles = "Administrator")]
    public class CUAEmpresasController : Controller
    {
        private VitrineVirtualDBContext db = new VitrineVirtualDBContext();
        Logger log = LogManager.GetCurrentClassLogger();
        Page page = new Page();


        // GET: CUA_Empresas
        public ActionResult Index()
        {
            var listaEmpresas = db.Empresas;
            return View(listaEmpresas);
        }

        /// <summary>
        /// Método de retorno dos dados pela receita federal e correios
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public ActionResult RetornaDadosPorCNPJ(string cnpj)
        {
            try
            {
                CUA_Empresas empDados = new CUA_Empresas();
                var resultCNPJ = EmpresaDados(cnpj);

                empDados.Bairro = resultCNPJ.bairro;
                empDados.CEP = resultCNPJ.cep.Replace(".", "").Replace("-", "").Replace("/", "");
                empDados.Cidade = resultCNPJ.municipio;
                empDados.Complemento = resultCNPJ.complemento;
                empDados.Estado = resultCNPJ.uf;
                empDados.Endereco = resultCNPJ.logradouro;
                empDados.Numero = resultCNPJ.numero;
                empDados.Matriz_Filial = resultCNPJ.tipo;
                empDados.Natureza_Juridica = resultCNPJ.natureza_juridica;
                empDados.Nome_Fantasia = resultCNPJ.fantasia;
                empDados.CNPJ = resultCNPJ.cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
                empDados.Razao_Social = resultCNPJ.nome;
                empDados.Situacao_Cadastral = resultCNPJ.situacao;
                empDados.Email = resultCNPJ.email;
                empDados.Telefone = resultCNPJ.telefone.Replace(".", "").Replace("-", "").Replace("/", "");
                empDados.Abertura_Empresa = Convert.ToDateTime(resultCNPJ.abertura);

                if (resultCNPJ.status == "OK")
                    return View("Create", empDados);

                else if(resultCNPJ.status == "ERROR")
                {
                    ViewBag.Error = "CNPJ inválido";
                    return View("Create");
                }
                else
                    return View("Create");
            }
            catch (Exception ex)
            {
                log.Log(LogLevel.Error)
                  .Exception(ex)
                  .Message("Mensagem de log {0} parametro", 1).Write();
                return null;
            }
        }

        /// <summary>
        /// Método que realiza consulta na API ReceitaWS e retorna os dados da empresa relativa ao CNPJ consultado.
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        protected ReceitaWS EmpresaDados(string cnpj)
        {
            ReceitaWS jsonResult = new ReceitaWS();
            string responseJson;
            string receitaWS = "https://www.receitaws.com.br/v1/cnpj/";
            string Api_Consulta = receitaWS + cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    using (var request = new HttpRequestMessage(HttpMethod.Get, Api_Consulta))
                    {
                        using (var response = client.SendAsync(request).Result)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                responseJson = response.Content.ReadAsStringAsync().Result;
                                jsonResult = JsonConvert.DeserializeObject<ReceitaWS>(responseJson);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Log(LogLevel.Error)
                  .Exception(ex)
                  .Message("Mensagem de log {0} parametro", 1).Write();

                return null;
            }
            return jsonResult;
        }

        /// <summary>
        /// Método resposável de filtrar por empresas.
        /// </summary>
        /// <param name="pesquisa"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FiltrarPorNome(string pesquisa)
        {
            try
            {
                var produtoSelecionado = db.Empresas.Where(a => a.Nome_Fantasia.Contains(pesquisa)).OrderBy(a => a.Nome_Fantasia).ToList();
                return View(produtoSelecionado);
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

        // GET: CUA_Empresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUA_Empresas Empresas = db.Empresas.Find(id);
            if (Empresas == null)
            {
                return HttpNotFound();
            }
            return View(Empresas);
        }

        // GET: CUA_Empresas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CUA_Empresas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome_Fantasia,Status_Empresa,Data_Criacao,Razao_Social,CEP,Endereco,Numero,Complemento,Bairro,Cidade,Estado,Email,CNPJ,Matriz_Filial,Situacao_Cadastral,Natureza_Juridica,Abertura_Empresa")] CUA_Empresas Empresas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Empresas.Add(Empresas);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(Empresas);
            }
            catch (Exception ex)
            {
                log.Log(LogLevel.Error)
                  .Exception(ex)
                  .Message("Mensagem de log {0} parametro", 1).Write();
                return null;
            }

        }

        // GET: CUA_Empresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUA_Empresas cUA_Empresas = db.Empresas.Find(id);
            if (cUA_Empresas == null)
            {
                return HttpNotFound();
            }
            return View(cUA_Empresas);
        }

        // POST: CUA_Empresas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Empresa,Nome_Empresa,Status_Empresa,Data_Criacao,Data_Alteracao,Nome_Fantasia,Nome_Tecnico,CNPJ")] CUA_Empresas cUA_Empresas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cUA_Empresas).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(cUA_Empresas);
            }
            catch (Exception ex)
            {
                log.Log(LogLevel.Error)
                  .Exception(ex)
                  .Message("Mensagem de log {0} parametro", 1).Write();
                return RedirectToAction("Index");
            }
            
        }

        // GET: CUA_Empresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUA_Empresas cUA_Empresas = db.Empresas.Find(id);
            if (cUA_Empresas == null)
            {
                return HttpNotFound();
            }
            return View(cUA_Empresas);
        }

        // POST: CUA_Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CUA_Empresas cUA_Empresas = db.Empresas.Find(id);
            db.Empresas.Remove(cUA_Empresas);
            db.SaveChanges();
            return RedirectToAction("Index");
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
