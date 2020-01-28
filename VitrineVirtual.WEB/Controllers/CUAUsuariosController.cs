using Microsoft.AspNet.Identity.EntityFramework;
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
    //[Authorize(Roles = "Administrator,Logista,Revendedor")]
    public class CUAUsuariosController : Controller
    {
        private VitrineVirtualDBContext db = new VitrineVirtualDBContext();
        Logger log = LogManager.GetCurrentClassLogger();
        Page page = new Page();


        // GET: CUA_Usuarios
        public ActionResult Index()
        {
            return View(db.Usuarios.ToList());
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
                var produtoSelecionado = db.Usuarios.Where(a => a.Login_Usuario.Contains(pesquisa)).OrderBy(a => a.Login_Usuario).ToList();
                return View(produtoSelecionado);
            }
            catch (Exception ex)
            {
                log.Log(LogLevel.Error)
                  .Exception(ex)
                  .Message("Mensagem de log {0} parametro", 1).Write();
                return RedirectToAction("Index");
            }

        }

        /// <summary>
        /// Método de retorno dos dados pelos correios
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="cep"></param>
        /// <returns></returns>
        public ActionResult retornaDadosPorCNPJ(string cnpj)
        {
            try
            {
                CUA_Usuarios usuarios = new CUA_Usuarios();
                var resultCNPJ = EmpresaDados(cnpj);


                if (cnpj != null)
                {
                    usuarios.Bairro = resultCNPJ.bairro;
                    usuarios.Cep = resultCNPJ.cep.Replace(".", "").Replace("-", "").Replace("/", "");
                    usuarios.Municipio = resultCNPJ.municipio;
                    usuarios.Complemento = resultCNPJ.complemento;
                    usuarios.UF = resultCNPJ.uf;
                    usuarios.Endereco = resultCNPJ.logradouro;
                    usuarios.Numero_Endereco = resultCNPJ.numero;
                    usuarios.Situacao_Cadastral = resultCNPJ.situacao;
                    usuarios.Nome_Completo = resultCNPJ.nome;
                    usuarios.Telefone = resultCNPJ.telefone.Replace("(", "").Replace(")", "").Replace("-", "");
                    usuarios.CNPJ = resultCNPJ.cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

                    return View("Create", usuarios);
                }
                
                else
                {
                    return View(usuarios);
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

        public ActionResult PesquisaUsuario(string email)
        {
            var cadastraUsuario = db.Usuarios.Where(x => x.Email == email).ToList();
            CUA_Usuarios usuarios = new CUA_Usuarios();
            usuarios.Email = cadastraUsuario[0].Email;
            usuarios.Login_Usuario = cadastraUsuario[0].Login_Usuario;
            usuarios.id = cadastraUsuario[0].id;

            return View("Create", usuarios);
        }

        // GET: CUA_Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUA_Usuarios cUA_Usuarios = db.Usuarios.Find(id);
            if (cUA_Usuarios == null)
            {
                return HttpNotFound();
            }
            return View(cUA_Usuarios);
        }

        // GET: CUA_Usuarios/Create
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                log.Log(LogLevel.Error)
                  .Exception(ex)
                  .Message("Mensagem de log {0} parametro", 1).Write();
                return View();
            }
        }

        // POST: CUA_Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome_Completo,Login_Usuario,Email,Telefone,Departamento,Cargo,Data_Nascimento,Cpf,Rg,Cep,Endereco,Numero_Endereco,Complemento,Bairro,Municipio,Status_Usuario,Data_Criacao,Data_Ultimo_Logon,CNPJ,Tipo_Usuario")] CUA_Usuarios CUA_Usuarios)
        {
            try
            {
                CUA_Usuarios.Data_Criacao = DateTime.Now;
                CUA_Usuarios.Data_Ultimo_Logon = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Usuarios.Add(CUA_Usuarios);
                    db.SaveChanges();
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "showPopUpConfirm", "showPopUpConfirm('Alerta!','Deseja realmente cadastrar esse usuário?','warning');", true);

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                log.Log(LogLevel.Error)
                  .Exception(ex)
                  .Message("Mensagem de log {0} parametro", 1).Write();
                return View();
            }

            return View(CUA_Usuarios);
        }

        // GET: CUA_Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUA_Usuarios cUA_Usuarios = db.Usuarios.Find(id);
            if (cUA_Usuarios == null)
            {
                return HttpNotFound();
            }
            return View(cUA_Usuarios);
        }

        // POST: CUA_Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nome_Completo,Login_Usuario,Email,Telefone,Departamento,Cargo,Data_Nascimento,Cpf,Rg,Cep,Endereco,Numero_Endereco,Complemento,Bairro,Municipio,Status_Usuario,Data_Criacao,Data_Ultimo_Logon,CNPJ,Tipo_Usuario")] CUA_Usuarios cUA_Usuarios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cUA_Usuarios).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(cUA_Usuarios);
            }
            catch (Exception ex)
            {
                log.Log(LogLevel.Error)
                  .Exception(ex)
                  .Message("Mensagem de log {0} parametro", 1).Write();
                return View();
            }
            
        }

        // GET: CUA_Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUA_Usuarios cUA_Usuarios = db.Usuarios.Find(id);
            if (cUA_Usuarios == null)
            {
                return HttpNotFound();
            }
            return View(cUA_Usuarios);
        }

        // POST: CUA_Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                CUA_Usuarios cUA_Usuarios = db.Usuarios.Find(id);
                db.Usuarios.Remove(cUA_Usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log.Log(LogLevel.Error)
                  .Exception(ex)
                  .Message("Mensagem de log {0} parametro", 1).Write();
                return View();
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

    }
}
