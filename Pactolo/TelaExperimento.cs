using Pactolo.scr.dominio;
using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pactolo {
    public partial class TelaExperimento : Form {

        private readonly List<Sessao> sessoesExecutadas;
        private readonly Experimentador experimentador;
        private readonly Participante participante;

        private readonly RelatorioSessao relatorioSessao;

        private readonly int imageHeight = 283;
        private readonly int imageWidth = 333;

        private readonly int height = Screen.PrimaryScreen.Bounds.Height;
        private readonly int width = Screen.PrimaryScreen.Bounds.Width;

        private bool CIFinalizado = false;
        private string ultimoBotaoTocado;

        //private readonly TaskCompletionSource<bool> taskSessao = new TaskCompletionSource<bool>();
        //private readonly TaskCompletionSource<bool> taskCI = new TaskCompletionSource<bool>();
        //private readonly TaskCompletionSource<bool> taskCC = new TaskCompletionSource<bool>();
        //private readonly TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
        //private readonly TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
        //private readonly TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

        public TelaExperimento(List<Sessao> sessoesExecutadas, Experimentador experimentador, Participante participante) {
            InitializeComponent();

            this.sessoesExecutadas = sessoesExecutadas;
            this.experimentador = experimentador;
            this.participante = participante;

            relatorioSessao = new RelatorioSessao(sessoesExecutadas.Select(it => it.Id) as List<long>, experimentador, participante);
        }

        private void TelaExperimento_Load(object sender, EventArgs e) {
            Location = new Point(0, 0);
            Size = new Size(width, height);
            ApresentarSessoes();
        }

        private async void ApresentarSessoes() {
            foreach (Sessao sessao in sessoesExecutadas) {
                await ApresentarSessao(sessao);
            }
        }

        private async Task ApresentarSessao(Sessao sessao) {
            List<ContingenciaColateral> CCs = sessao.CCs;

            foreach (ContingenciaColateral CC in CCs) {
                ContingenciaInstrucional CI = CC.CI;
                if (CI != null) {
                    await ApresentarCI(CI);
                }
                else {
                    panelCI.Visible = false;
                    await ApresentarCC(CC);
                }
            }
        }

        private async Task ApresentarCC(ContingenciaColateral CC) {
            pictureSModelo.Image = ImageUtils.Resize(CC.sModelo.Imagem, imageWidth, imageHeight);
            pictureSC1.Image = ImageUtils.Resize(CC.SC1.Imagem, imageWidth, imageHeight);
            pictureSC2.Image = ImageUtils.Resize(CC.SC2.Imagem, imageWidth, imageHeight);
            pictureSC3.Image = ImageUtils.Resize(CC.SC3.Imagem, imageWidth, imageHeight);
        }

        private async Task ApresentarCI(ContingenciaInstrucional CI) {
            pictureTato1.Image = ImageUtils.Resize(CI.Tato1.Imagem, imageWidth, imageHeight);
            pictureAuto.Image = ImageUtils.Resize(CI.Autoclitico.Imagem, imageWidth, imageHeight);
            pictureTato2.Image = ImageUtils.Resize(CI.Tato2.Imagem, imageWidth, imageHeight);


        }
    }
}
