﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using project.Lib_Primavera;
using project.Items;

namespace project.Controllers
{
    public class ApiSalesController : ApiController
    {
        //returns 10 top sales of all time
        // GET api/sales/top
        [System.Web.Http.HttpGet]
        public HttpResponseMessage TopSales()
        {
            List<Lib_Primavera.Model.CabecDoc> sales = Lib_Primavera.PriIntegration.getSales();
            List<TopSalesItem> result = new List<TopSalesItem>();

            foreach (Lib_Primavera.Model.CabecDoc sale in sales)
            {
                result.Add(new TopSalesItem
                    {
                        entity = sale.Entidade,
                        numDoc = sale.NumDoc,
                        purchaseValue = sale.TotalMerc + sale.TotalIva,
                        date = sale.Data,
                        numPurchases = Lib_Primavera.PriIntegration.numPurchases(sale.Entidade)
                    });
            }

            result = result.OrderBy(e => e.purchaseValue).Reverse().Take(10).ToList();

            var json = new JavaScriptSerializer().Serialize(result);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        //returns 10 top sales of all time
        // GET api/sales/top/{year}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage TopSalesY(string year)
        {
            List<Lib_Primavera.Model.CabecDoc> sales = Lib_Primavera.PriIntegration.getSalesBy("year", year, null,null);
            List<TopSalesItem> result = new List<TopSalesItem>();

            foreach (Lib_Primavera.Model.CabecDoc sale in sales)
            {
                result.Add(new TopSalesItem
                {
                    entity = sale.Entidade,
                    numDoc = sale.NumDoc,
                    purchaseValue = sale.TotalMerc + sale.TotalIva,
                    date = sale.Data,
                    numPurchases = Lib_Primavera.PriIntegration.numPurchases(sale.Entidade)
                });
            }

            result = result.OrderBy(e => e.purchaseValue).Reverse().Take(10).ToList();

            var json = new JavaScriptSerializer().Serialize(result);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        //returns 10 top sales of all time
        // GET api/sales/top/{year}/{month}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage TopSalesM(string year, string month)
        {
            List<Lib_Primavera.Model.CabecDoc> sales = Lib_Primavera.PriIntegration.getSalesBy("month", year, month, null);
            List<TopSalesItem> result = new List<TopSalesItem>();

            foreach (Lib_Primavera.Model.CabecDoc sale in sales)
            {
                result.Add(new TopSalesItem
                {
                    entity = sale.Entidade,
                    numDoc = sale.NumDoc,
                    purchaseValue = sale.TotalMerc + sale.TotalIva,
                    date = sale.Data,
                    numPurchases = Lib_Primavera.PriIntegration.numPurchases(sale.Entidade)
                });
            }

            result = result.OrderBy(e => e.purchaseValue).Reverse().Take(10).ToList();

            var json = new JavaScriptSerializer().Serialize(result);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        //returns 10 top sales of all time
        // GET api/sales/top/{year}/{month}/{day}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage TopSalesD(string year, string month, string day)
        {
            List<Lib_Primavera.Model.CabecDoc> sales = Lib_Primavera.PriIntegration.getSalesBy("day", year, month, day);
            List<TopSalesItem> result = new List<TopSalesItem>();

            foreach (Lib_Primavera.Model.CabecDoc sale in sales)
            {
                result.Add(new TopSalesItem
                {
                    entity = sale.Entidade,
                    numDoc = sale.NumDoc,
                    purchaseValue = sale.TotalMerc + sale.TotalIva,
                    date = sale.Data,
                    numPurchases = Lib_Primavera.PriIntegration.numPurchases(sale.Entidade)
                });
            }

            result = result.OrderBy(e => e.purchaseValue).Reverse().Take(10).ToList();

            var json = new JavaScriptSerializer().Serialize(result);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        //returns a list with the 10 top products and volume of sales in a year
        // GET api/sales/psb/{year}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage SalesBookingY(string year)
        {
            List<SalesBookingItem> returnList = new List<SalesBookingItem>();
            List<Lib_Primavera.Model.Artigo> allProd = Lib_Primavera.PriIntegration.ListaArtigos();

            foreach (Lib_Primavera.Model.Artigo prod in allProd)
            {
                returnList.Add(new SalesBookingItem
                    {
                        nome = prod.DescArtigo,
                        valorVendas = Lib_Primavera.PriIntegration.getSalesProd("year", prod.CodArtigo, year, null, null)
                    });
            }
            returnList = returnList.OrderBy(e => e.valorVendas).Reverse().Take(10).ToList();

            var json = new JavaScriptSerializer().Serialize(returnList);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        //returns a list with the 10 top products and volume of sales in a certain year and month
        // GET api/sales/psb/{year}/{month}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage SalesBookingM(string year, string month)
        {
            List<SalesBookingItem> returnList = new List<SalesBookingItem>();
            List<Lib_Primavera.Model.Artigo> allProd = Lib_Primavera.PriIntegration.ListaArtigos();

            foreach (Lib_Primavera.Model.Artigo prod in allProd)
            {
                
                returnList.Add(new SalesBookingItem
                {
                    nome = prod.DescArtigo,
                    valorVendas = Lib_Primavera.PriIntegration.getSalesProd("month", prod.CodArtigo, year, month, null)
                });
                
            }
            returnList = returnList.OrderBy(e => e.valorVendas).Reverse().Take(10).ToList();

            var json = new JavaScriptSerializer().Serialize(returnList);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        //returns a list with the 10 top products and volume of sales in a certain year, month and day
        // GET api/sales/psb/{year}/{month}/{day}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage SalesBookingD(string year, string month, string day)
        {
            List<SalesBookingItem> returnList = new List<SalesBookingItem>();
            List<Lib_Primavera.Model.Artigo> allProd = Lib_Primavera.PriIntegration.ListaArtigos();

            foreach (Lib_Primavera.Model.Artigo prod in allProd)
            {
                returnList.Add(new SalesBookingItem
                {
                    nome = prod.DescArtigo,
                    valorVendas = Lib_Primavera.PriIntegration.getSalesProd("day", prod.CodArtigo, year, month, day)
                });
            }
            returnList = returnList.OrderBy(e => e.valorVendas).Reverse().Take(10).ToList();

            var json = new JavaScriptSerializer().Serialize(returnList);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        //returns the regional sales status, which is the percentage of volume of sales each region has in a certain year
        // GET api/sales/rss/{year}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage RegionalSSY(string year)
        {
            List<RegionalSSItem> returnList = new List<RegionalSSItem>();
            List<string> countries = Lib_Primavera.PriIntegration.getAllCountries();

            double totalValue = 0;
            double thisValue = 0;

            foreach (string country in countries)
            {
                thisValue = Lib_Primavera.PriIntegration.getPercentage("year", year, null, null, country);
                totalValue += thisValue;
                returnList.Add(new RegionalSSItem
                {
                    pais = country,
                    percentagem = thisValue,
                    valor = thisValue

                });
            }

            foreach (RegionalSSItem item in returnList)
            {
                if (item.valor == 0)
                {
                    item.percentagem = 0;
                }
                else
                {
                    item.percentagem = 100.0 * item.percentagem / totalValue;
                }
            }

            var json = new JavaScriptSerializer().Serialize(returnList);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        //returns the regional sales status, which is the percentage of volume of sales each region has in a certain year and month
        // GET api/sales/rss/{year}/{month}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage RegionalSSM(string year, string month)
        {
            List<RegionalSSItem> returnList = new List<RegionalSSItem>();
            List<string> countries = Lib_Primavera.PriIntegration.getAllCountries();
            double totalValue = 0;
            double thisValue = 0;

            foreach (string country in countries)
            {
                thisValue = Lib_Primavera.PriIntegration.getPercentage("month", year, month, null, country);
                totalValue += thisValue;
                returnList.Add(new RegionalSSItem
                {
                    pais = country,
                    percentagem = thisValue,
                    valor = thisValue

                });
            }

            foreach (RegionalSSItem item in returnList)
            {
                if (item.valor == 0)
                {
                    item.percentagem = 0;
                }
                else
                {
                    item.percentagem = 100.0 * item.percentagem / totalValue;
                }
            }

            var json = new JavaScriptSerializer().Serialize(returnList);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        //returns the regional sales status, which is the percentage of volume of sales each region has in a certain year, month and day
        // GET api/sales/rss/{year}/{month}/{day}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage RegionalSSD(string year, string month, string day)
        {
            List<RegionalSSItem> returnList = new List<RegionalSSItem>();
            List<string> countries = Lib_Primavera.PriIntegration.getAllCountries();
            double totalValue = 0;
            double thisValue = 0;

            foreach (string country in countries)
            {
                thisValue = Lib_Primavera.PriIntegration.getPercentage("day", year, month, day, country);
                totalValue += thisValue;
                returnList.Add(new RegionalSSItem
                {
                    pais = country,
                    percentagem = thisValue,
                    valor = thisValue

                });
            }
            foreach (RegionalSSItem item in returnList)
            {
                if (item.valor == 0)
                {
                    item.percentagem = 0;
                }
                else
                {
                    item.percentagem = 100.0 * item.percentagem / totalValue;
                }
            }

            var json = new JavaScriptSerializer().Serialize(returnList);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }


    }
}
