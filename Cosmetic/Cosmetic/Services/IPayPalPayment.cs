using Microsoft.AspNetCore.Mvc;
using PayPal.v1.Payments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMarket.Services.PayPal
{
    public interface IPayPalPayment
    {
        Payment CreatePayment(double amount, string returnUrl, string cancelUrl, string intent);
        Payment CreatePayment(double amount, string returnUrl, string cancelUrl, string intent,List<Item> items);

        Task<string> ExecutePayment(Payment payment);

    }
}
