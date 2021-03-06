﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TesteSeusConhecimentos.Domain;
using TesteSeusConhecimentos.Entities;
using TesteSeusConhecimentos.Infra;

namespace TesteSeusConhecimentos.Web.Infocast
{
    public partial class InfoEnterprises : System.Web.UI.Page
    {
        private IEnterpriseRepository _enterpriseRepository;

        private int idEnterprise
        {
            set { ViewState["idEnterprise"] = value; }
            get
            {
                if (ViewState["idEnterprise"] != null)
                    return Convert.ToInt32(ViewState["idEnterprise"]);

                return 0;
            }
        }

        public InfoEnterprises()
        {
            this._enterpriseRepository = new EnterpriseRepository();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetViewStateEnterprise();
                UpdateForm();
            }
        }

        private void SetViewStateEnterprise()
        {
            if (Request.QueryString["id"] != null)
                idEnterprise = Convert.ToInt32(Request.QueryString["id"]);
            else
                idEnterprise = 0;
        }

        private void UpdateForm()
        {
            var enterprise = _enterpriseRepository.GetById(idEnterprise);

            if (enterprise != null)
            {
                formStatus.InnerText = "Editar Empresa";
                txtStreetAdress.Text = enterprise.StreetAdress;
                txtCity.Text = enterprise.City;
                txtState.Text = enterprise.State;
                txtZipCode.Text = enterprise.ZipCode;
                txtCorporateActivity.Text = enterprise.CorporateActivity;
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            var enterprise = new Enterprise()
            {
                IdEnterprise = idEnterprise,
                StreetAdress = txtStreetAdress.Text,
                City = txtCity.Text,
                State = txtState.Text,
                ZipCode = txtZipCode.Text,
                CorporateActivity = txtCorporateActivity.Text
            };
            _enterpriseRepository.Save(enterprise);
            
            Response.Redirect("~/Infocast/Enterprises.aspx");
        }
    }
}