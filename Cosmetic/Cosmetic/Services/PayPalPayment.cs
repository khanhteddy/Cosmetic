using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BraintreeHttp;
using EMarket.Services.PayPal;
using Microsoft.Extensions.Options;
using PayPal.Core;
using PayPal.v1.Payments;

namespace Services.PayPal
{
    public class PayPalPayment : IPayPalPayment
    {

        private readonly PayPalAuthOptions _option;
        private SandboxEnvironment _sandbox;
        PayPalHttpClient _client;
        public PayPalPayment(IOptions<PayPalAuthOptions> option)
        {

            _option = option.Value;
            _sandbox = new SandboxEnvironment(_option.PayPalClientID, _option.PayPalSecret);
            _client = new PayPalHttpClient(_sandbox);
        }

        public Payment CreatePayment(double total, string returnUrl, string cancelUrl, string intent)
        {

            var payment = new Payment()
            {
                Intent = intent,
                Transactions = GetTransactionsList(total, new List<Item>()),
                RedirectUrls = new RedirectUrls()
                {
                    CancelUrl = cancelUrl,
                    ReturnUrl = returnUrl
                },
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }
            };

            return payment;
        }

        public Payment CreatePayment(double total, string returnUrl, string cancelUrl, string intent, List<Item> items)
        {

            var payment = new Payment()
            {
                Intent = intent,
                Transactions = GetTransactionsList(total, items),
                RedirectUrls = new RedirectUrls()
                {
                    CancelUrl = cancelUrl,
                    ReturnUrl = returnUrl
                },
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }
            };
            return payment;
        }


        private List<Transaction> GetTransactionsList(double total, List<Item> items)
        {
            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                Description = "Transaction description.",
                InvoiceNumber = GetRandomInvoiceNumber(),
                Amount = new Amount()
                {
                    Currency = "USD",
                    Total = total.ToString(),
                    Details = new AmountDetails()
                    {
                        Tax = "0",
                        Shipping = "0",
                        Subtotal = total.ToString()
                    }
                },
                ItemList = new ItemList()
                {
                    Items = items
                },
               // Payee = new Payee
              // {
                  // TODO.. Enter the payee email address here
                //  Email = "Cosmetic@business.com",

                   // TODO.. Enter the merchant id here
                  //  MerchantId = "3VDGNRZN3JTZ8"
                //}
            });
            return transactionList;
        }

        public async Task<string> ExecutePayment(Payment payment)
        {
            PaymentCreateRequest request = new PaymentCreateRequest();
            request.RequestBody(payment);
            try
            {
                HttpResponse response = await _client.Execute(request);
                var statusCode = response.StatusCode;
                Payment result = response.Result<Payment>();
                var links = result.Links.GetEnumerator();
                string paypalRedirectUrl = null;
                while (links.MoveNext())
                {
                    LinkDescriptionObject lnk = links.Current;
                    if (lnk.Rel.ToLower().Trim().Equals("approval_url"))
                    {
                        //saving the payapalredirect URL to which user will be redirected for payment  
                        paypalRedirectUrl = lnk.Href;
                        // saving the paymentID in the key guid
                     
                    }
                }
                return paypalRedirectUrl;
            }
            catch (HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
                return "fail";
            }
        }


        string GetRandomInvoiceNumber()
        {
            return new Random().Next(999999999).ToString();
        }
    }
}