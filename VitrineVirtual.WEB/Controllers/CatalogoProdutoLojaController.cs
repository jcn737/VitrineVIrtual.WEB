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
using PayPal.Api;
using VitrineVirtual.WEB.PayPal;

namespace VitrineVirtual.WEB.Controllers
{
    public class CatalogoProdutoLojaController : Controller
    {
        private VitrineVirtualDBContext db = new VitrineVirtualDBContext();

        //[Authorize(Roles = "Administrator,Logista")]
        public ActionResult Index()
        {
            // TODO: Pegar usuário cliente campo texto login, checar empresa usuário pertence e gravar cookie
            // Também pode usar redis de banco
            var listaProdutos = db.CadastroProdutoLoja.ToList();
            return View(listaProdutos);
        }

        //[Authorize(Roles = "Administrator,Logista")]
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

        //[AllowAnonymous]
        //public ActionResult ModaFeminina(string categoria)
        //{
        //    List<Cadastro_Produto_Loja> catalogo_Produto_Loja = new List<Cadastro_Produto_Loja>();
        //    var grupoCategoria = db.CadastroProdutoLoja.GroupBy(p => p.Categoria_Produto);

        //    foreach (var categorias in grupoCategoria)
        //    {
        //        var produtosListagem = categorias.Where(x => x.Categoria_Produto == categoria);
        //        foreach (var produtos in produtosListagem)
        //        {
        //            catalogo_Produto_Loja.Add(produtos);
        //        }
        //    }

        //    return View(catalogo_Produto_Loja);
        //}

        [AllowAnonymous]
        public ActionResult ModaMasculina(string categoria)
        {
            List<Cadastro_Produto_Loja> catalogo_Produto_Loja = new List<Cadastro_Produto_Loja>();
            var grupoCategoria = db.CadastroProdutoLoja.GroupBy(p => p.Categoria_Produto);

            foreach (var categorias in grupoCategoria)
            {
                var produtosListagem = categorias.Where(x => x.Categoria_Produto == categoria);
                foreach (var produtos in produtosListagem)
                {
                    catalogo_Produto_Loja.Add(produtos);
                }
            }

            return View(catalogo_Produto_Loja);
        }

        [AllowAnonymous]
        public ActionResult ModaInfantoJuvenil(string categoria)
        {
            List<Cadastro_Produto_Loja> catalogo_Produto_Loja = new List<Cadastro_Produto_Loja>();
            var grupoCategoria = db.CadastroProdutoLoja.GroupBy(p => p.Categoria_Produto);

            foreach (var categorias in grupoCategoria)
            {
                var produtosListagem = categorias.Where(x => x.Categoria_Produto == categoria);
                foreach (var produtos in produtosListagem)
                {
                    catalogo_Produto_Loja.Add(produtos);
                }
            }

            return View(catalogo_Produto_Loja);
        }

        [AllowAnonymous]
        public ActionResult ModaBaby(string categoria)
        {
            List<Cadastro_Produto_Loja> catalogo_Produto_Loja = new List<Cadastro_Produto_Loja>();
            var grupoCategoria = db.CadastroProdutoLoja.GroupBy(p => p.Categoria_Produto);

            foreach (var categorias in grupoCategoria)
            {
                var produtosListagem = categorias.Where(x => x.Categoria_Produto == categoria);
                foreach (var produtos in produtosListagem)
                {
                    catalogo_Produto_Loja.Add(produtos);
                }
            }

            return View(catalogo_Produto_Loja);
        }

        [AllowAnonymous]
        public ActionResult ModaPraia(string categoria)
        {
            List<Cadastro_Produto_Loja> catalogo_Produto_Loja = new List<Cadastro_Produto_Loja>();
            var grupoCategoria = db.CadastroProdutoLoja.GroupBy(p => p.Categoria_Produto);

            foreach (var categorias in grupoCategoria)
            {
                var produtosListagem = categorias.Where(x => x.Categoria_Produto == categoria);
                foreach (var produtos in produtosListagem)
                {
                    catalogo_Produto_Loja.Add(produtos);
                }
            }

            return View(catalogo_Produto_Loja);
        }

        [AllowAnonymous]
        public ActionResult ModaFitness(string categoria)
        {
            List<Cadastro_Produto_Loja> catalogo_Produto_Loja = new List<Cadastro_Produto_Loja>();
            var grupoCategoria = db.CadastroProdutoLoja.GroupBy(p => p.Categoria_Produto);

            foreach (var categorias in grupoCategoria)
            {
                var produtosListagem = categorias.Where(x => x.Categoria_Produto == categoria);
                foreach (var produtos in produtosListagem)
                {
                    catalogo_Produto_Loja.Add(produtos);
                }
            }

            return View(catalogo_Produto_Loja);
        }

        [AllowAnonymous]
        public ActionResult ModaIntima(string categoria)
        {
            List<Cadastro_Produto_Loja> catalogo_Produto_Loja = new List<Cadastro_Produto_Loja>();
            var grupoCategoria = db.CadastroProdutoLoja.GroupBy(p => p.Categoria_Produto);

            foreach (var categorias in grupoCategoria)
            {
                var produtosListagem = categorias.Where(x => x.Categoria_Produto == categoria);
                foreach (var produtos in produtosListagem)
                {
                    catalogo_Produto_Loja.Add(produtos);
                }
            }

            return View(catalogo_Produto_Loja);
        }

        [AllowAnonymous]
        public ActionResult SapatoFeminino(string categoria)
        {
            List<Cadastro_Produto_Loja> catalogo_Produto_Loja = new List<Cadastro_Produto_Loja>();
            var grupoCategoria = db.CadastroProdutoLoja.GroupBy(p => p.Categoria_Produto);

            foreach (var categorias in grupoCategoria)
            {
                var produtosListagem = categorias.Where(x => x.Categoria_Produto == categoria);
                foreach (var produtos in produtosListagem)
                {
                    catalogo_Produto_Loja.Add(produtos);
                }
            }

            return View(catalogo_Produto_Loja);
        }

        [AllowAnonymous]
        public ActionResult SapatoMasculino(string categoria)
        {
            List<Cadastro_Produto_Loja> catalogo_Produto_Loja = new List<Cadastro_Produto_Loja>();
            var grupoCategoria = db.CadastroProdutoLoja.GroupBy(p => p.Categoria_Produto);

            foreach (var categorias in grupoCategoria)
            {
                var produtosListagem = categorias.Where(x => x.Categoria_Produto == categoria);
                foreach (var produtos in produtosListagem)
                {
                    catalogo_Produto_Loja.Add(produtos);
                }
            }

            return View(catalogo_Produto_Loja);
        }

        [AllowAnonymous]
        public ActionResult SapatoInfatil(string categoria)
        {
            List<Cadastro_Produto_Loja> catalogo_Produto_Loja = new List<Cadastro_Produto_Loja>();
            var grupoCategoria = db.CadastroProdutoLoja.GroupBy(p => p.Categoria_Produto);

            foreach (var categorias in grupoCategoria)
            {
                var produtosListagem = categorias.Where(x => x.Categoria_Produto == categoria);
                foreach (var produtos in produtosListagem)
                {
                    catalogo_Produto_Loja.Add(produtos);
                }
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
