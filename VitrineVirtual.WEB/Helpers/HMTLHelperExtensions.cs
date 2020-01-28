using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using VitrineVirtual.Model;

namespace VitrineVirtual.WEB
{
    public static class HMTLHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {

            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        public static string PageClass(this HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

        public static IHtmlString Image(this HtmlHelper helper, byte[] image, string imgclass,
                                     object htmlAttributes = null)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("class", imgclass);
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            var imageString = image != null ? Convert.ToBase64String(image) : string.Empty;
            var img = string.Format("data:image/jpg;base64,{0}", imageString);
            builder.MergeAttribute("src", img);

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }


        public static MvcHtmlString PesquisaCNPJ(this HtmlHelper<CUA_Empresas> helper, CUA_Empresas empresa, object atributos)
        {
            if (!string.IsNullOrEmpty(empresa.CNPJ))
            {
                var dadosEmpresa = new TagBuilder("input");
                dadosEmpresa.AddCssClass("form-control");
                dadosEmpresa.MergeAttribute("type", "text");
                dadosEmpresa.MergeAttribute("id", "CNPJConsulta");
                dadosEmpresa.MergeAttribute("name", "CNPJConsulta");
                dadosEmpresa.MergeAttribute("value", empresa.CNPJ);
                return MvcHtmlString.Create(string.Format(dadosEmpresa.InnerHtml));
            }
            return null;
        }

        //This overload accepts single expression as parameter.
        public static MvcHtmlString Custom_TextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            return Custom_TextBoxFor(helper, expression, null);
        }

        public static MvcHtmlString TextBoxQtd(this HtmlHelper helper, string qtdItem, object value, object htmlAttributes)
        {
            int quantidade = Convert.ToInt32(qtdItem);

            if (quantidade > 0)
            {
                var qtdProd = new TagBuilder("input");
                qtdProd.AddCssClass("form-control");
                qtdProd.MergeAttribute("type", "number");
                qtdProd.MergeAttribute("id", "qtdItem");
                qtdProd.MergeAttribute("name", "qtdItem");

                var preco = Convert.ToDecimal(value);
                if (quantidade == 0) { quantidade = 1; }
                decimal precoFinal = preco * quantidade;
                qtdProd.MergeAttribute("value", precoFinal.ToString());
                return MvcHtmlString.Create(string.Format(qtdProd.InnerHtml));
            }
            return null;
        }

        //This overload accepts expression and htmlAttributes object as parameter.
        public static MvcHtmlString Custom_TextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            //Fetching the metadata related to expression. This includes name of the property, model value of the property as well.
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            //Fetching the property name.
            string propertyName = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            //Creating a input tag using TagBuilder class.
            TagBuilder textBox = new TagBuilder("input");
            //Setting the tags property type to text to render textbox.
            textBox.Attributes.Add("type", "text");
            //Setting the name and id attribute.
            textBox.Attributes.Add("name", propertyName);
            textBox.Attributes.Add("id", propertyName);

            //Setting the value attribute of textbox with model value if present.
            if (metadata.Model != null)
            {
                textBox.Attributes.Add("value", metadata.Model.ToString());
            }
            textBox.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(textBox.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString FileFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return helper.FileFor(expression, null);
        }

        public static MvcHtmlString FileFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var builder = new TagBuilder("input");

            var id = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
            builder.GenerateId(id);
            builder.MergeAttribute("name", id);
            builder.MergeAttribute("type", "file");

            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            // Render tag
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }

    public static partial class PublicExtensions
    {
        public static string ForcedString(this object NullableObject, string DefaultValue = "")
        {
            return (NullableObject == null ? DefaultValue : NullableObject.ToString().Trim());
        }

    }
}
