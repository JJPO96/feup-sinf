﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using project.Lib_Primavera.Model;

namespace project.Controllers
{
    public class FinancialController : ApiController
    {

        // GET api/financial
        [System.Web.Http.HttpGet]
        public string Index()
        {
            return "test only financial";
        }

        // GET api/financial/global
        [System.Web.Http.HttpGet]
        public string Global()
        {
            return "Tamos tesos. No money. Ja tas Cândido";
        }

        // GET api/financial/purchases
        [System.Web.Http.HttpGet]
        public double Purchases()
        {
            return Lib_Primavera.PriIntegration.getPurchasesTotal();
        }

    }
}