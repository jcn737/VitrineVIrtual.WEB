using NLog;
using NLog.Fluent;
using PayPal;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI;
using VitrineVirtual.WEB.PayPal;


namespace VitrineVirtual.WEB.Controllers
{
    public class PaypalController : Controller
    {
        Logger log = LogManager.GetCurrentClassLogger();
        Page page = new Page();
        private Payment payment;
        // GET: Paypal
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles = "Administrator,Logista,Revendedor")]
        public ActionResult PaymentWithCreditCard()
        {
            //create and item for which you are taking payment
            //if you need to add more items in the list
            //Then you will need to create multiple item objects or use some loop to instantiate object
            Item item = new Item();
            item.name = "Demo Item";
            item.currency = "BRL";
            item.price = "5";
            item.quantity = "1";
            item.sku = "sku";

            //Now make a List of Item and add the above item to it
            //you can create as many items as you want and add to this list
            List<Item> itms = new List<Item>();
            itms.Add(item);
            ItemList itemList = new ItemList();
            itemList.items = itms;

            //Address for the payment
            Address billingAddress = new Address();
            billingAddress.city = "Campinas";
            billingAddress.country_code = "BR";
            billingAddress.line1 = "23rd street kew gardens";
            billingAddress.postal_code = "43210";
            billingAddress.state = "NY";


            //Now Create an object of credit card and add above details to it
            //Please replace your credit card details over here which you got from paypal

            CreditCard crdtCard = new CreditCard();
            crdtCard.billing_address = billingAddress;
            crdtCard.cvv2 = "874";  //card cvv2 number
            crdtCard.expire_month = 1; //card expire date
            crdtCard.expire_year = 2020; //card expire year
            crdtCard.first_name = "Aman";
            crdtCard.last_name = "Thakur";
            crdtCard.number = "1234567890123456"; //enter your credit card number here
            crdtCard.type = "visa"; //credit card type here paypal allows 4 types

            // Specify details of your payment amount.
            Details details = new Details();
            details.shipping = "1";
            details.subtotal = "5";
            details.tax = "1";

            // Specify your total payment amount and assign the details object
            Amount amnt = new Amount();
            amnt.currency = "USD";
            // Total = shipping tax + subtotal.
            amnt.total = "7";
            amnt.details = details;

            // Now make a transaction object and assign the Amount object
            Transaction tran = new Transaction();
            tran.amount = amnt;
            tran.description = "Description about the payment amount.";
            tran.item_list = itemList;
            tran.invoice_number = "your invoice number which you are generating";

            // Now, we have to make a list of transaction and add the transactions object
            // to this list. You can create one or more object as per your requirements

            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(tran);

            // Now we need to specify the FundingInstrument of the Payer
            // for credit card payments, set the CreditCard which we made above

            FundingInstrument fundInstrument = new FundingInstrument();
            fundInstrument.credit_card = crdtCard;

            // The Payment creation API requires a list of FundingIntrument

            List<FundingInstrument> fundingInstrumentList = new List<FundingInstrument>();
            fundingInstrumentList.Add(fundInstrument);

            // Now create Payer object and assign the fundinginstrument list to the object
            Payer payr = new Payer();
            payr.funding_instruments = fundingInstrumentList;
            payr.payment_method = "credit_card";

            // finally create the payment object and assign the payer object & transaction list to it
            Payment pymnt = new Payment();
            pymnt.intent = "sale";
            pymnt.payer = payr;
            pymnt.transactions = transactions;

            try
            {
                //getting context from the paypal
                //basically we are sending the clientID and clientSecret key in this function
                //to the get the context from the paypal API to make the payment
                //for which we have created the object above.

                //Basically, apiContext object has a accesstoken which is sent by the paypal
                //to authenticate the payment to facilitator account.
                //An access token could be an alphanumeric string

                APIContext apiContext = PaypalConfiguration.GetAPIContext();

                //Create is a Payment class function which actually sends the payment details
                //to the paypal API for the payment. The function is passed with the ApiContext
                //which we received above.

                Payment createdPayment = pymnt.Create(apiContext);

                //if the createdPayment.state is "approved" it means the payment was successful else not

                if (createdPayment.state.ToLower() != "approved")
                {
                    return View("FailureView");
                }
            }
            catch (PayPalException ex)
            {
                log.Log(LogLevel.Error)
                  .Exception(ex)
                  .Message("Mensagem de log {0} parametro", 1).Write();
                return View("FailureView");
            }

            return View("SuccessView");
        }

        //[Authorize(Roles = "Administrator,Logista,Revendedor")]
        [HttpPost]
        public bool PaymentWithPaypal()
        {
            //getting the apiContext
            APIContext apiContext = PaypalConfiguration.GetAPIContext();


            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal
                //Payer Id will be returned when payment proceeds or click to pay
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class

                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                                "/Paypal/PaymentWithPayPal?";

                    //here we are generating guid for storing the paymentID received in session
                    //which will be used in the payment execution

                    var guid = Convert.ToString((new Random()).Next(100000));

                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected for paypal account payment

                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);

                    //get links returned from paypal in response to Create function call

                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);

                    //return Redirect(paypalRedirectUrl);
                    return true;
                }
                else
                {

                    // This function exectues after receving all parameters for the payment

                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                    //If executed payment failed then we will show payment failure message to user
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        //return View("FailureView");
                        return true;
                    }
                }
            }
            catch (PayPalException ex)
            {
                log.Log(LogLevel.Error)
                  .Exception(ex)
                  .Message("Mensagem de log {0} parametro", 1).Write();
                //return View("FailureView");
                return false;
            }

            //on successful payment, show success page to user.
            //return View("SuccessView");
            return true;
        }

        //[Authorize(Roles = "Administrator,Logista,Revendedor")]
        [HttpPost]
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            try
            {
                var paymentExecution = new PaymentExecution() { payer_id = payerId };
                this.payment = new Payment() { id = paymentId };
                return this.payment.Execute(apiContext, paymentExecution);
            }
            catch (PayPalException ex)
            {
                log.Log(LogLevel.Error)
                  .Exception(ex)
                  .Message("Mensagem de log {0} parametro", 1).Write();
                return null;
            }
            
        }

        //[Authorize(Roles = "Administrator,Logista,Revendedor")]
        [HttpPost]
        public Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            try
            {
                var idCadastroProdLoja = Session["ID_Cadastro_Prod_Loja"].ToString();
                var nomeProduto = Session["Nome_Produto"].ToString();
                var precoProduto = Session["Preco_Produto"].ToString().Replace(",",".");
                var precoDesconto = Session["Preco_Desconto"] != null ? Session["Preco_Desconto"].ToString().Replace(",", ".") : "0,00";
                var quantidadeProduto = Session["Quantidade_Produto"].ToString();
                var modeloProduto = Session["Modelo_Produto"].ToString();

                //create itemlist and add item objects to it
                var itemList = new ItemList() { items = new List<Item>() };

                //Adding Item Details like name, currency, price etc
                itemList.items.Add(new Item()
                {
                    name = nomeProduto,
                    currency = "BRL",
                    price = precoProduto,
                    quantity = quantidadeProduto,
                    sku = "sku"
                });

                var payer = new Payer() { payment_method = "paypal" };

                // Configure Redirect Urls here with RedirectUrls object
                var redirUrls = new RedirectUrls()
                {
                    cancel_url = redirectUrl + "&Cancel=true",
                    return_url = redirectUrl
                };

                // Adding Tax, shipping and Subtotal details
                var details = new Details()
                {
                    tax = "1.50",
                    shipping = "1.80",
                    subtotal = "4.78",
                    shipping_discount = "1"
                };

                //Final amount with details
                var amount = new Amount()
                {
                    currency = "BRL",
                    total = precoProduto, // Total must be equal to sum of tax, shipping and subtotal.
                    details = details
                };

                var transactionList = new List<Transaction>();
                // Adding description about the transaction
                transactionList.Add(new Transaction()
                {
                    description = "Transaction description",
                    invoice_number = Convert.ToString((new Random()).Next(100000)),
                    amount = amount,
                    item_list = itemList
                });


                this.payment = new Payment()
                {
                    intent = "sale",
                    payer = payer,
                    transactions = transactionList,
                    redirect_urls = redirUrls
                };

                // Create a payment using a APIContext
                var payment = this.payment.Create(apiContext);

                return payment;
            }
            catch (PayPalException ex)
            {
                log.Log(LogLevel.Error)
                  .Exception(ex)
                  .Message("Mensagem de log {0} parametro", ex.Message).Write();
                //ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "swalFalhou();", true);
                ScriptManager.RegisterStartupScript(page, page.GetType(), "showPopUpConfirm", "showPopUpConfirm('Sucesso!', 'Itens de importação inseridos com sucesso no catálogo de produtos.','success');", true);

                return null;

            }

        }
    }
}