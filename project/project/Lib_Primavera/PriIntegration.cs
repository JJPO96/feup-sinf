﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.ErpBS800;
using Interop.StdPlatBS800;
using Interop.StdBE800;
using Interop.GcpBE800;
using ADODB;
using Interop.IGcpBS800;
//using Interop.StdBESql800;
//using Interop.StdBSSql800;

using project.Items;

namespace project.Lib_Primavera
{
    public class PriIntegration
    {
        # region Cliente

        public static List<Model.Cliente> ListaClientes()
        {
            StdBELista objList;

            List<Model.Cliente> listClientes = new List<Model.Cliente>();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {

                //objList = PriEngine.Engine.Comercial.Clientes.LstClientes();

                objList = PriEngine.Engine.Consulta("SELECT Cliente, Nome, Moeda, NumContrib as NumContribuinte, Fac_Mor AS campo_exemplo FROM  CLIENTES");


                while (!objList.NoFim())
                {
                    listClientes.Add(new Model.Cliente
                    {
                        CodCliente = objList.Valor("Cliente"),
                        NomeCliente = objList.Valor("Nome"),
                        Moeda = objList.Valor("Moeda"),
                        NumContribuinte = objList.Valor("NumContribuinte"),
                        Morada = objList.Valor("campo_exemplo")
                    });
                    objList.Seguinte();

                }

                return listClientes;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.Cliente GetCliente(string codCliente)
        {
            GcpBECliente objCli = new GcpBECliente();

            Model.Cliente myCli = new Model.Cliente();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {

                if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == true)
                {
                    objCli = PriEngine.Engine.Comercial.Clientes.Edita(codCliente);
                    myCli.CodCliente = objCli.get_Cliente();
                    myCli.NomeCliente = objCli.get_Nome();
                    myCli.Moeda = objCli.get_Moeda();
                    myCli.NumContribuinte = objCli.get_NumContribuinte();
                    myCli.Morada = objCli.get_Morada();
                    return myCli;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.RespostaErro UpdCliente(Lib_Primavera.Model.Cliente cliente)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            GcpBECliente objCli = new GcpBECliente();

            try
            {

                if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
                {

                    if (PriEngine.Engine.Comercial.Clientes.Existe(cliente.CodCliente) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {

                        objCli = PriEngine.Engine.Comercial.Clientes.Edita(cliente.CodCliente);
                        objCli.set_EmModoEdicao(true);

                        objCli.set_Nome(cliente.NomeCliente);
                        objCli.set_NumContribuinte(cliente.NumContribuinte);
                        objCli.set_Moeda(cliente.Moeda);
                        objCli.set_Morada(cliente.Morada);

                        PriEngine.Engine.Comercial.Clientes.Actualiza(objCli);

                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;

                }

            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }

        public static Lib_Primavera.Model.RespostaErro DelCliente(string codCliente)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBECliente objCli = new GcpBECliente();


            try
            {

                if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
                {
                    if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {

                        PriEngine.Engine.Comercial.Clientes.Remove(codCliente);
                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }

                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }

        public static Lib_Primavera.Model.RespostaErro InsereClienteObj(Model.Cliente cli)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            GcpBECliente myCli = new GcpBECliente();

            try
            {
                if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
                {

                    myCli.set_Cliente(cli.CodCliente);
                    myCli.set_Nome(cli.NomeCliente);
                    myCli.set_NumContribuinte(cli.NumContribuinte);
                    myCli.set_Moeda(cli.Moeda);
                    myCli.set_Morada(cli.Morada);

                    PriEngine.Engine.Comercial.Clientes.Actualiza(myCli);

                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }


        }

        public static int numPurchases(string entity)
        {
            int numPurchases = 0;

            bool companyInitialized = PriEngine.InitializeCompany(
                project.Properties.Settings.Default.Company.Trim(),
                project.Properties.Settings.Default.User.Trim(),
                project.Properties.Settings.Default.Password.Trim());

            if (companyInitialized)
            {
                numPurchases = PriEngine.Engine.Consulta("SELECT count(*) as numPurchases FROM CabecDoc WHERE Entidade = " + entity).Valor("numPurchases");
                return numPurchases;

            }
            return -1;
        }

        #endregion Cliente;   // -----------------------------  END   CLIENTE    -----------------------

        #region Artigo

        public static Lib_Primavera.Model.Artigo GetArtigo(string codArtigo)
        {

            GcpBEArtigo objArtigo = new GcpBEArtigo();
            Model.Artigo myArt = new Model.Artigo();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {

                if (PriEngine.Engine.Comercial.Artigos.Existe(codArtigo) == false)
                {
                    return null;
                }
                else
                {
                    objArtigo = PriEngine.Engine.Comercial.Artigos.Edita(codArtigo);
                    myArt.CodArtigo = objArtigo.get_Artigo();
                    myArt.DescArtigo = objArtigo.get_Descricao();

                    return myArt;
                }

            }
            else
            {
                return null;
            }

        }

        public static List<Model.Artigo> ListaArtigos()
        {

            StdBELista objList;

            Model.Artigo art = new Model.Artigo();
            List<Model.Artigo> listArts = new List<Model.Artigo>();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Comercial.Artigos.LstArtigos();

                while (!objList.NoFim())
                {
                    art = new Model.Artigo();
                    art.CodArtigo = objList.Valor("artigo");
                    art.DescArtigo = objList.Valor("descricao");

                    listArts.Add(art);
                    objList.Seguinte();
                }

                return listArts;

            }
            else
            {
                return null;

            }

        }

        #endregion Artigo

        #region Fornecedor

        public static List<Model.Fornecedor> ListaFornecedores()
        {
            StdBELista objList;

            List<Model.Fornecedor> listFornecedores = new List<Model.Fornecedor>();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta("SELECT Fornecedor, Nome, Morada, NumContrib, Tel FROM FORNECEDORES");


                while (!objList.NoFim())
                {
                    listFornecedores.Add(new Model.Fornecedor
                    {
                        CodFornecedor = objList.Valor("Fornecedor"),
                        NomeFornecedor = objList.Valor("Nome"),
                        Morada = objList.Valor("Morada"),
                        NumContribuinte = objList.Valor("NumContrib"),
                        Telefone = objList.Valor("Tel")
                    });
                    objList.Seguinte();

                }

                return listFornecedores;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.Fornecedor GetFornecedor(string codFornecedor)
        {
            GcpBEFornecedor objFor = new GcpBEFornecedor();

            Model.Fornecedor myFor = new Model.Fornecedor();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {

                if (PriEngine.Engine.Comercial.Clientes.Existe(codFornecedor) == true)
                {
                    objFor = PriEngine.Engine.Comercial.Fornecedores.Edita(codFornecedor);
                    myFor.CodFornecedor = objFor.get_Fornecedor();
                    myFor.NomeFornecedor = objFor.get_Nome();
                    myFor.NumContribuinte = objFor.get_NumContribuinte();
                    myFor.Morada = objFor.get_Morada();
                    return myFor;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }

        #endregion fornecedor

        #region DocCompra

        public static List<Model.DocCompra> VGR_List()
        {

            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocCompra dc = new Model.DocCompra();
            List<Model.DocCompra> listdc = new List<Model.DocCompra>();
            Model.LinhaDocCompra lindc = new Model.LinhaDocCompra();
            List<Model.LinhaDocCompra> listlindc = new List<Model.LinhaDocCompra>();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, NumDocExterno, Entidade, DataDoc, NumDoc, TotalMerc, Serie From CabecCompras where TipoDoc='VGR'");
                while (!objListCab.NoFim())
                {
                    dc = new Model.DocCompra();
                    dc.id = objListCab.Valor("id");
                    dc.NumDocExterno = objListCab.Valor("NumDocExterno");
                    dc.Entidade = objListCab.Valor("Entidade");
                    dc.NumDoc = objListCab.Valor("NumDoc");
                    dc.Data = objListCab.Valor("DataDoc");
                    dc.TotalMerc = objListCab.Valor("TotalMerc");
                    dc.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecCompras, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido, Armazem, Lote from LinhasCompras where IdCabecCompras='" + dc.id + "' order By NumLinha");
                    listlindc = new List<Model.LinhaDocCompra>();

                    while (!objListLin.NoFim())
                    {
                        lindc = new Model.LinhaDocCompra();
                        lindc.IdCabecDoc = objListLin.Valor("idCabecCompras");
                        lindc.CodArtigo = objListLin.Valor("Artigo");
                        lindc.DescArtigo = objListLin.Valor("Descricao");
                        lindc.Quantidade = objListLin.Valor("Quantidade");
                        lindc.Unidade = objListLin.Valor("Unidade");
                        lindc.Desconto = objListLin.Valor("Desconto1");
                        lindc.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindc.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindc.TotalLiquido = objListLin.Valor("PrecoLiquido");
                        lindc.Armazem = objListLin.Valor("Armazem");
                        lindc.Lote = objListLin.Valor("Lote");

                        listlindc.Add(lindc);
                        objListLin.Seguinte();
                    }

                    dc.LinhasDoc = listlindc;

                    listdc.Add(dc);
                    objListCab.Seguinte();
                }
            }
            return listdc;
        }

        public static Model.RespostaErro VGR_New(Model.DocCompra dc)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            GcpBEDocumentoCompra myGR = new GcpBEDocumentoCompra();
            GcpBELinhaDocumentoCompra myLin = new GcpBELinhaDocumentoCompra();
            GcpBELinhasDocumentoCompra myLinhas = new GcpBELinhasDocumentoCompra();

            PreencheRelacaoCompras rl = new PreencheRelacaoCompras();
            List<Model.LinhaDocCompra> lstlindv = new List<Model.LinhaDocCompra>();

            try
            {
                if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myGR.set_Entidade(dc.Entidade);
                    myGR.set_NumDocExterno(dc.NumDocExterno);
                    myGR.set_Serie(dc.Serie);
                    myGR.set_Tipodoc("VGR");
                    myGR.set_TipoEntidade("F");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dc.LinhasDoc;
                    PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(myGR, rl);
                    foreach (Model.LinhaDocCompra lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Compras.AdicionaLinha(myGR, lin.CodArtigo, lin.Quantidade, lin.Armazem, "", lin.PrecoUnitario, lin.Desconto);
                    }


                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Compras.Actualiza(myGR, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }

        public static double getPurchasesTotal()
        {
            double total = 0;

            bool companyInitialized = PriEngine.InitializeCompany(
                project.Properties.Settings.Default.Company.Trim(),
                project.Properties.Settings.Default.User.Trim(),
                project.Properties.Settings.Default.Password.Trim());

            if (companyInitialized)
            {
                StdBELista objList = PriEngine.Engine.Consulta(
                    "SELECT CabecCompras.TotalMerc, CabecCompras.TotalIva FROM CabecCompras");

                while (!objList.NoFim())
                {
                    total -= objList.Valor("TotalMerc");
                    total -= objList.Valor("TotalIVA");

                    objList.Seguinte();
                }
            }

            return total;
        }

        #endregion DocCompra

        #region DocsVenda

        /*public static Model.RespostaErro Encomendas_New(Model.DocVenda dv)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBEDocumentoVenda myEnc = new GcpBEDocumentoVenda();

            GcpBELinhaDocumentoVenda myLin = new GcpBELinhaDocumentoVenda();

            GcpBELinhasDocumentoVenda myLinhas = new GcpBELinhasDocumentoVenda();

            PreencheRelacaoVendas rl = new PreencheRelacaoVendas();
            List<Model.LinhaDocVenda> lstlindv = new List<Model.LinhaDocVenda>();

            try
            {
                if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myEnc.set_Entidade(dv.Entidade);
                    myEnc.set_Serie(dv.Serie);
                    myEnc.set_Tipodoc("ECL");
                    myEnc.set_TipoEntidade("C");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dv.LinhasDoc;
                    PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(myEnc, rl);
                    foreach (Model.LinhaDocVenda lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Vendas.AdicionaLinha(myEnc, lin.CodArtigo, lin.Quantidade, "", "", lin.PrecoUnitario, lin.Desconto);
                    }


                    // PriEngine.Engine.Comercial.Compras.TransformaDocumento(

                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Vendas.Actualiza(myEnc, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }*/

        /*public static List<Model.DocVenda> Encomendas_List()
        {

            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL'");
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVenda();
                    dv.id = objListCab.Valor("id");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.Data = objListCab.Valor("Data");
                    dv.TotalMerc = objListCab.Valor("TotalMerc");
                    dv.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVenda>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVenda();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        lindv.CodArtigo = objListLin.Valor("Artigo");
                        lindv.DescArtigo = objListLin.Valor("Descricao");
                        lindv.Quantidade = objListLin.Valor("Quantidade");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        lindv.Desconto = objListLin.Valor("Desconto1");
                        lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }
            return listdv;
        }*/

        /*public static Model.DocVenda Encomenda_Get(string numdoc)
        {


            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {


                string st = "SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL' and NumDoc='" + numdoc + "'";
                objListCab = PriEngine.Engine.Consulta(st);
                dv = new Model.DocVenda();
                dv.id = objListCab.Valor("id");
                dv.Entidade = objListCab.Valor("Entidade");
                dv.NumDoc = objListCab.Valor("NumDoc");
                dv.Data = objListCab.Valor("Data");
                dv.TotalMerc = objListCab.Valor("TotalMerc");
                dv.Serie = objListCab.Valor("Serie");
                objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                listlindv = new List<Model.LinhaDocVenda>();

                while (!objListLin.NoFim())
                {
                    lindv = new Model.LinhaDocVenda();
                    lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                    lindv.CodArtigo = objListLin.Valor("Artigo");
                    lindv.DescArtigo = objListLin.Valor("Descricao");
                    lindv.Quantidade = objListLin.Valor("Quantidade");
                    lindv.Unidade = objListLin.Valor("Unidade");
                    lindv.Desconto = objListLin.Valor("Desconto1");
                    lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                    lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                    lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");
                    listlindv.Add(lindv);
                    objListLin.Seguinte();
                }

                dv.LinhasDoc = listlindv;
                return dv;
            }
            return null;
        }*/

        public static List<Model.CabecDoc> getSales()
        {
            StdBELista objList;

            List<Model.CabecDoc> sales = new List<Model.CabecDoc>();

            bool companyInitialized = PriEngine.InitializeCompany(
                project.Properties.Settings.Default.Company.Trim(),
                project.Properties.Settings.Default.User.Trim(),
                project.Properties.Settings.Default.Password.Trim());

            if (companyInitialized)
            {
                objList = PriEngine.Engine.Consulta("SELECT CabecDoc.Data, CabecDoc.Entidade, CabecDoc.Nome, CabecDoc.NumDoc, CabecDoc.NumContribuinte, CabecDoc.TotalMerc, CabecDoc.TotalIva FROM CabecDoc");
                while (!objList.NoFim())
                {
                    sales.Add(new Model.CabecDoc
                    {
                        Id = null,
                        Entidade = objList.Valor("Entidade"),
                        Nome = objList.Valor("Nome"),
                        NumDoc = objList.Valor("NumDoc"),
                        NumContribuinte = objList.Valor("NumContribuinte"),
                        TotalMerc = objList.Valor("TotalMerc"),
                        TotalIva = objList.Valor("TotalIva"),
                        LinhasDoc = null,
                        Data = objList.Valor("Data"),
                        Serie = null
                    });

                    objList.Seguinte();
                }

                return sales;
            }
            else
                return null;
        }

        public static List<Model.LinhaDocVenda> getProductSales()
        {
            StdBELista objList;
            Model.LinhaDocVenda lindv;

            List<Model.LinhaDocVenda> sales = new List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc");
                sales = new List<Model.LinhaDocVenda>();

                while (!objList.NoFim())
                {
                    lindv = new Model.LinhaDocVenda();
                    lindv.IdCabecDoc = objList.Valor("idCabecDoc");
                    lindv.CodArtigo = objList.Valor("Artigo");
                    lindv.DescArtigo = objList.Valor("Descricao");
                    lindv.Quantidade = objList.Valor("Quantidade");
                    lindv.Unidade = objList.Valor("Unidade");
                    lindv.Desconto = objList.Valor("Desconto1");
                    lindv.PrecoUnitario = objList.Valor("PrecUnit");
                    lindv.TotalILiquido = objList.Valor("TotalILiquido");
                    lindv.TotalLiquido = objList.Valor("PrecoLiquido");

                    sales.Add(lindv);
                    objList.Seguinte();
                }

                return sales;
            }
            else
                return null;
        }

        public static double getSalesTotal()
        {
            double total = 0;

            bool companyInitialized = PriEngine.InitializeCompany(
                project.Properties.Settings.Default.Company.Trim(),
                project.Properties.Settings.Default.User.Trim(),
                project.Properties.Settings.Default.Password.Trim());

            if (companyInitialized)
            {
                StdBELista objList = PriEngine.Engine.Consulta(
                    "SELECT CabecDoc.TotalMerc, CabecDoc.TotalIva FROM CabecDoc");

                while (!objList.NoFim())
                {
                    total += objList.Valor("TotalMerc");
                    total += objList.Valor("TotalIVA");

                    objList.Seguinte();
                }
            }

            return total;
        }

        public static List<TopSalesCountry> getTop10SalesCountries()
        {
            List<TopSalesCountry> result = new List<TopSalesCountry>();

            bool companyInitialized = PriEngine.InitializeCompany(
                project.Properties.Settings.Default.Company.Trim(),
                project.Properties.Settings.Default.User.Trim(),
                project.Properties.Settings.Default.Password.Trim());

            if (companyInitialized)
            {
                Dictionary<string, double> countrySalesMap = new Dictionary<string, double>();

                StdBELista objList = PriEngine.Engine.Consulta(
                    "SELECT Clientes.Pais, CabecDoc.TotalMerc, CabecDoc.TotalIva FROM Clientes, CabecDoc WHERE CabecDoc.Entidade = Clientes.Cliente");

                // build country-sales dictionary
                while (!objList.NoFim())
                {
                    string country = objList.Valor("Pais");
                    double amount = objList.Valor("TotalMerc") + objList.Valor("TotalIVA");

                    if (countrySalesMap.ContainsKey(country))
                        countrySalesMap[country] += amount;
                    else
                        countrySalesMap[country] = amount;

                    objList.Seguinte();
                }

                // this is the total sales of the top 10 sales countries
                double total = 0;

                // pick the top 10
                for (int i = 0; i < 10 && countrySalesMap.Count > 0; i++)
                {
                    var countryMaxSales = countrySalesMap.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

                    result.Add(new TopSalesCountry
                    {
                        country = countryMaxSales,
                        amount = countrySalesMap[countryMaxSales],
                        percentage = 0
                    });

                    total += countrySalesMap[countryMaxSales];

                    countrySalesMap.Remove(countryMaxSales);
                }

                // fill in the percentage for each entry
                foreach (TopSalesCountry entry in result)
                {
                    entry.percentage = 100.0 * entry.amount / total;
                }
            }

            return result;
        }

        /*public static List<Model.Product> mostSoldProductsByClient(string client_id)
        {
            StdBELista objList;

            List<Model.Product> listProducts = new List<Model.Product>();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta("SELECT LinhasDoc.Artigo, LinhasDoc.Descricao, LinhasDoc.PrecoLiquido, LinhasDoc.Quantidade FROM LinhasDoc, CabecDoc WHERE LinhasDoc.IdCabecDoc == CabecDoc.Id AND CabecDoc.Entidade = " + client_id);
                while (!objList.NoFim())
                {
                    listProducts.Add(new Model.Product
                    {
                        CodArtigo = objList.Valor("Artigo"),
                        DescArtigo = objList.Valor("Descricao")
                    });
                    objList.Seguinte();

                }

                return listProducts;
            }
            else
                return null;
        }*/

        public static List<Model.Teste> getIDDocProduct()
        {
            StdBELista objList;

            List<Model.Teste> listProducts = new List<Model.Teste>();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta("SELECT IdCabecDoc, Descricao FROM LinhasDoc");

                while (!objList.NoFim())
                {
                    listProducts.Add(new Model.Teste
                    {
                        teste_str = objList.Valor("IdCabecDoc"),
                        teste_str2 = objList.Valor("Descricao")
                    });
                    objList.Seguinte();

                }

                return listProducts;
            }
            else
                return null;
        }

        public static List<Model.Teste> getIDDoc()
        {
            StdBELista objList;

            List<Model.Teste> listProducts = new List<Model.Teste>();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta("SELECT Id, NumContribuinte FROM CabecDoc");

                while (!objList.NoFim())
                {
                    listProducts.Add(new Model.Teste
                    {
                        teste_str = objList.Valor("Id"),
                        teste_str2 = objList.Valor("NumContribuinte")
                    });
                    objList.Seguinte();
                }

                return listProducts;
            }
            else return null;
        }

        #endregion DocsVenda
    }
}