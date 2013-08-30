using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace GEPEX
{
    public partial class botao : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }
        public void InitializeComponent()
        {
            this.imgNovo.Command += new System.Web.UI.WebControls.CommandEventHandler(this.imgNovo_Click);
            this.imgSalvar.Command += new System.Web.UI.WebControls.CommandEventHandler(this.imgSalvar_Click);
            this.imgPesquisar.Command += new System.Web.UI.WebControls.CommandEventHandler(this.imgPesquisar_Click);
            this.imgLimpar.Command += new System.Web.UI.WebControls.CommandEventHandler(this.imgLimpar_Click);
            this.imgImprimir.Command += new System.Web.UI.WebControls.CommandEventHandler(this.imgImprimir_Click);
            this.imgVoltar.Command += new System.Web.UI.WebControls.CommandEventHandler(this.imgVoltar_Click);
            this.imgFichaAluno.Command += new System.Web.UI.WebControls.CommandEventHandler(this.imgFichaAluno_Click);
            this.imgPlanejamentoClinivo.Command += new System.Web.UI.WebControls.CommandEventHandler(this.imgPlanejamentoClinivo_Click);
            this.imgPlanejamentoPedagogico.Command += new System.Web.UI.WebControls.CommandEventHandler(this.imgPlanejamentoPedagogico_Click);
            this.imgListaEspera.Command += new System.Web.UI.WebControls.CommandEventHandler(this.imgListaEspera_Click);
            this.imgFinalizarAnoLetivo.Command += new System.Web.UI.WebControls.CommandEventHandler(this.imgFinalizarAnoLetivo_Click);
        }
        public delegate void EventHandler(object sender, System.Web.UI.WebControls.CommandEventArgs e);
        public event EventHandler imgNovoOnClick;
        public event EventHandler imgSalvarOnClick;
        public event EventHandler imgLimparOnClick;
        public event EventHandler imgVoltarOnClick;
        public event EventHandler imgPesquisarOnClick;
        public event EventHandler imgImprimirOnClick;
        public event EventHandler imgPlanejamentoClinicoOnClick;
        public event EventHandler imgFichaAlunoOnClick;
        public event EventHandler imgPlanejamentoPedagogicoOnClick;
        public event EventHandler imgListaEsperaOnClick;
        public event EventHandler imgFinalizarAnoLetivoOnClick;

        private void imgNovo_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            this.imgNovoOnClick(sender, e);
        }

        private void imgSalvar_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            this.imgSalvarOnClick(sender, e);
        }
        private void imgLimpar_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            this.imgLimparOnClick(sender, e);
        }
        private void imgVoltar_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            this.imgVoltarOnClick(sender, e);
        }
        private void imgPesquisar_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            this.imgPesquisarOnClick(sender, e);
        }
        private void imgImprimir_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            this.imgImprimirOnClick(sender, e);
        }
        private void imgFichaAluno_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            this.imgFichaAlunoOnClick(sender, e);
        }
        private void imgPlanejamentoClinivo_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            this.imgPlanejamentoClinicoOnClick(sender, e);
        }
        private void imgPlanejamentoPedagogico_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            this.imgPlanejamentoPedagogicoOnClick(sender, e);
        }
        private void imgListaEspera_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            this.imgListaEsperaOnClick(sender, e);
        }
        private void imgFinalizarAnoLetivo_Click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            this.imgFinalizarAnoLetivoOnClick(sender, e);
        }
        public void Desabilitar(bool novo)
        {
            if (novo)
            {
                imgNovo.Visible = false;                
            }
        }
        public void Desabilitar(bool novo, bool pesquisar)
        {
            if (novo)
            {
               imgNovo.Visible = false;
            }
            if (pesquisar)
            {
                imgPesquisar.Visible = false;
            }
        }
        public void Desabilitar(bool novo, bool pesquisar, bool salvar)
        {
            if (novo)
                imgNovo.Visible = false;
            if (pesquisar)
                imgPesquisar.Visible = false;
            if (salvar)
                imgSalvar.Visible = false;
        }
        public void Desabilitar(bool novo, bool pesquisar, bool salvar, bool limpar)
        {
            if (novo)
                imgNovo.Visible = false;
            if (pesquisar)
                imgPesquisar.Visible = false;
            if (salvar)
                imgSalvar.Visible = false;
            if (limpar)
                imgLimpar.Visible = false;
        }
        public void Desabilitar(bool novo, bool pesquisar, bool salvar, bool limpar, bool imprimir)
        {
            if (novo)
                imgNovo.Visible = false;
            if (pesquisar)
                imgPesquisar.Visible = false;
            if (salvar)
                imgSalvar.Visible = false;
            if (limpar)
                imgLimpar.Visible = false;
            if (imprimir)
                imgImprimir.Visible = false;
        }
        public void Desabilitar(bool novo, bool pesquisar, bool salvar, bool limpar, bool imprimir, bool fichaAluno)
        {
            if (novo)
                imgNovo.Visible = false;
            if (pesquisar)
                imgPesquisar.Visible = false;
            if (salvar)
                imgSalvar.Visible = false;
            if (limpar)
                imgLimpar.Visible = false;
            if (imprimir)
                imgImprimir.Visible = false;
            if (fichaAluno)
                imgFichaAluno.Visible = false;
        }
        public void Desabilitar(bool novo, bool pesquisar, bool salvar, bool limpar, bool imprimir, bool fichaAluno, bool planejamentoClinico)
        {
            if (novo)
                imgNovo.Visible = false;
            if (pesquisar)
                imgPesquisar.Visible = false;
            if (salvar)
                imgSalvar.Visible = false;
            if (limpar)
                imgLimpar.Visible = false;
            if (imprimir)
                imgImprimir.Visible = false;
            if (fichaAluno)
                imgFichaAluno.Visible = false;
            if (planejamentoClinico)
                imgPlanejamentoClinivo.Visible = false;
        }
        public void Desabilitar(bool novo, bool pesquisar, bool salvar, bool limpar, bool imprimir, bool fichaAluno, bool planejamentoClinico, bool planejamentoPedagogico)
        {
            if (novo)
                imgNovo.Visible = false;
            if (pesquisar)
                imgPesquisar.Visible = false;
            if (salvar)
                imgSalvar.Visible = false;
            if (limpar)
                imgLimpar.Visible = false;
            if (imprimir)
                imgImprimir.Visible = false;
            if (fichaAluno)
                imgFichaAluno.Visible = false;
            if (planejamentoClinico)
                imgPlanejamentoClinivo.Visible = false;
            if (planejamentoPedagogico)
                imgPlanejamentoPedagogico.Visible = false;
        }
        public void Desabilitar(bool novo, bool pesquisar, bool salvar, bool limpar, bool imprimir, bool fichaAluno, bool planejamentoClinico, bool planejamentoPedagogico, bool voltar)
        {
            if (novo)
                imgNovo.Visible = false;
            if (pesquisar)
                imgPesquisar.Visible = false;
            if (salvar)
                imgSalvar.Visible = false;
            if (limpar)
                imgLimpar.Visible = false;
            if (imprimir)
                imgImprimir.Visible = false;
            if (fichaAluno)
                imgFichaAluno.Visible = false;
            if (planejamentoClinico)
                imgPlanejamentoClinivo.Visible = false;
            if (planejamentoPedagogico)
                imgPlanejamentoPedagogico.Visible = false;
            if (voltar)
                imgVoltar.Visible = false;
        }
        public void Desabilitar(bool novo, bool pesquisar, bool salvar, bool limpar, bool imprimir, bool fichaAluno, bool planejamentoClinico, bool planejamentoPedagogico, bool voltar, bool listaEspera)
        {
            if (novo)
                imgNovo.Visible = false;
            if (pesquisar)
                imgPesquisar.Visible = false;
            if (salvar)
                imgSalvar.Visible = false;
            if (limpar)
                imgLimpar.Visible = false;
            if (imprimir)
                imgImprimir.Visible = false;
            if (fichaAluno)
                imgFichaAluno.Visible = false;
            if (planejamentoClinico)
                imgPlanejamentoClinivo.Visible = false;
            if (planejamentoPedagogico)
                imgPlanejamentoPedagogico.Visible = false;
            if (voltar)
                imgVoltar.Visible = false;
            if (!listaEspera)
                imgListaEspera.Visible = true;
        }
        public void Desabilitar(bool novo, bool pesquisar, bool salvar, bool limpar, bool imprimir, bool fichaAluno, 
                                bool planejamentoClinico, bool planejamentoPedagogico, bool voltar, bool listaEspera, bool finalizarAnoLetivo)
        {
            if (novo)
                imgNovo.Visible = false;
            if (pesquisar)
                imgPesquisar.Visible = false;
            if (salvar)
                imgSalvar.Visible = false;
            if (limpar)
                imgLimpar.Visible = false;
            if (imprimir)
                imgImprimir.Visible = false;
            if (fichaAluno)
                imgFichaAluno.Visible = false;
            if (planejamentoClinico)
                imgPlanejamentoClinivo.Visible = false;
            if (planejamentoPedagogico)
                imgPlanejamentoPedagogico.Visible = false;
            if (voltar)
                imgVoltar.Visible = false;
            if (!listaEspera)
                imgListaEspera.Visible = true;
            if (!finalizarAnoLetivo)
                imgFinalizarAnoLetivo.Visible = true;
        }

    }
}