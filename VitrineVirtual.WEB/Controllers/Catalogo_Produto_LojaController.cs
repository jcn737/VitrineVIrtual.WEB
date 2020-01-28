using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using VitrineVirtual.Data;
using VitrineVirtual.Model;

namespace VitrineVirtual.WEB.Controllers
{
    public class Catalogo_Produto_LojaController : Controller
    {
        private VitrineVirtualDBContext db = new VitrineVirtualDBContext();

        // GET: Catalogo_Produto_Loja
        public ActionResult Index()
        {
            // TODO: Pegar usuário cliente campo texto login, checar empresa usuário pertence e gravar cookie
            // Também pode usar redis de banco
            var listaProdutos = db.CadastroProdutoLoja.ToList();
            return View(listaProdutos);
        }

        // GET: Catalogo_Produto_Loja/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cadastro_Produto_Loja catalogo_Produto_Loja = db.CadastroProdutoLoja.Find(id);
            if (catalogo_Produto_Loja == null)
            {
                return HttpNotFound();
            }
            return View(catalogo_Produto_Loja);
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
