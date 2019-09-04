using Pactolo.scr.dominio;
using Pactolo.scr.services;
using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pactolo {
    public partial class TelaExperimento : Form {

        private readonly List<Sessao> sessoesExecutadas;

        private Sessao sessaoAtual;
        private ContingenciaColateral CCAtual;
        private List<UnidadeDoExperimento> SCsEmbaralhados;

        private SoundPlayer soundPlayer;

        private readonly Random random = new Random();

        private readonly RelatorioSessao relatorioSessao;

        private readonly int imageHeight = 283;
        private readonly int imageWidth = 333;

        private readonly int height = Screen.PrimaryScreen.Bounds.Height;
        private readonly int width = Screen.PrimaryScreen.Bounds.Width;

        private bool CIFinalizado = false;
        private bool SessoesFinalizadas = false;

        private TaskCompletionSource<bool> taskTato1;
        private TaskCompletionSource<bool> taskAuto;
        private TaskCompletionSource<bool> taskTato2;
        private TaskCompletionSource<bool> taskSModelo;
        private TaskCompletionSource<bool> taskFinalizacao;
        private TaskCompletionSource<bool> taskCC;

        public TelaExperimento(List<Sessao> sessoesExecutadas, Experimentador experimentador, Participante participante) {
            InitializeComponent();

            this.sessoesExecutadas = sessoesExecutadas;

            relatorioSessao = new RelatorioSessao(sessoesExecutadas.Select(it => it.Id).Cast<long>().ToList(), experimentador, participante);
        }

        private void TelaExperimento_Load(object sender, EventArgs e) {
            Location = new Point(0, 0);
            Size = new Size(width, height);

            panelCI.Visible = false;
            panelPontos.Visible = false;
            pictureSModelo.Visible = false;
            pictureSC1.Visible = false;
            pictureSC2.Visible = false;
            pictureSC3.Visible = false;
            labelMensagemSC1.Visible = false;
            labelMensagemSC2.Visible = false;
            labelMensagemSC3.Visible = false;

            ApresentarSessoes();
        }

        private void AtualizaPontos(int pontosGanhos) {
            sessaoAtual.NumeroPontos += pontosGanhos;
            if (sessaoAtual.NumeroPontos < 0) {
                sessaoAtual.NumeroPontos = 0;
            }
            labelPontos.Text = "Pontos: " + sessaoAtual.NumeroPontos.ToString();
        }

        private async void ApresentarSessoes() {
            foreach (Sessao sessao in sessoesExecutadas) {
                await ApresentarSessao(sessao);
                RelatorioSessaoService.GeraRelatorio(relatorioSessao);
            }
            SessoesFinalizadas = true;
            MessageBox.Show("Finalizado o Experimento! Por favor, chamar o experimentador", "Finalizado");
            await taskFinalizacao.Task;
        }

        private async Task ApresentarSessao(Sessao sessao) {
            sessaoAtual = sessao;
            List<ContingenciaColateral> CCs = sessao.CCs;

            foreach (ContingenciaColateral CC in CCs) {
                ContingenciaInstrucional CI = CC.CI;
                if (CI != null) {
                    await ApresentarCI(CC);
                }
                else {
                    panelCI.Visible = false;
                    await ApresentarCC(CC);
                }
            }
        }

        private void EmbaralhaSCs() {
            UnidadeDoExperimento[] copia = new UnidadeDoExperimento[3];
            SCsEmbaralhados.CopyTo(copia);

            if (random.Next(0, 2) == 0) {
                SCsEmbaralhados[0] = copia[2];
                SCsEmbaralhados[1] = copia[0];
                SCsEmbaralhados[2] = copia[1];
            }
            else {
                SCsEmbaralhados[0] = copia[1];
                SCsEmbaralhados[1] = copia[2];
                SCsEmbaralhados[2] = copia[0];
            }

            pictureSC1.Image = ImageUtils.Resize(SCsEmbaralhados[0].Imagem, imageWidth, imageHeight);
            pictureSC2.Image = ImageUtils.Resize(SCsEmbaralhados[1].Imagem, imageWidth, imageHeight);
            pictureSC3.Image = ImageUtils.Resize(SCsEmbaralhados[2].Imagem, imageWidth, imageHeight);
        }

        private async Task ApresentarCC(ContingenciaColateral CC) {
            taskSModelo = new TaskCompletionSource<bool>(false);
            taskFinalizacao = new TaskCompletionSource<bool>(false);
            taskCC = new TaskCompletionSource<bool>(false);

            CCAtual = CC;
            SCsEmbaralhados = new List<UnidadeDoExperimento>() { CC.SC1, CC.SC2, CC.SC3 };
            pictureSModelo.Visible = true;
            pictureSModelo.Image = ImageUtils.Resize(CC.sModelo.Imagem, imageWidth, imageHeight);

            await taskSModelo.Task;

            pictureSC3.Visible = true;
            panelPontos.Visible = true;
            pictureSC2.Visible = true;
            pictureSC1.Visible = true;

            EmbaralhaSCs();
            await taskCC.Task;
        }

        private async Task ApresentarCI(ContingenciaColateral CC) {
            CIFinalizado = false;
            taskTato1 = new TaskCompletionSource<bool>(false);
            taskAuto = new TaskCompletionSource<bool>(false);
            taskTato2 = new TaskCompletionSource<bool>(false);

            panelCI.Visible = true;
            pictureTato1.BackColor = Color.White;
            pictureAuto.BackColor = Color.White;
            pictureTato2.BackColor = Color.White;

            ContingenciaInstrucional CI = CC.CI;
            pictureTato1.Image = ImageUtils.Resize(CI.Tato1.Imagem, imageWidth, imageHeight);
            pictureAuto.Image = ImageUtils.Resize(CI.Autoclitico.Imagem, imageWidth, imageHeight);
            pictureTato2.Image = ImageUtils.Resize(CI.Tato2.Imagem, imageWidth, imageHeight);

            await taskTato1.Task;
            pictureTato1.BackColor = Color.Green;
            await taskAuto.Task;
            pictureAuto.BackColor = Color.Green;
            await taskTato2.Task;
            pictureTato2.BackColor = Color.Green;
            taskTato1.TrySetResult(false);
            taskAuto.TrySetResult(false);
            taskTato2.TrySetResult(false);
            CIFinalizado = true;
            await ApresentarCC(CC);
        }

        private void PictureTato1_Click(object sender, EventArgs e) {
            taskTato1.TrySetResult(true);
        }

        private void PictureAuto_Click(object sender, EventArgs e) {
            if (taskTato1.Task.IsCompleted) {
                taskAuto.TrySetResult(true);
            }
        }

        private void PictureTato2_Click(object sender, EventArgs e) {
            if (taskTato1.Task.IsCompleted && taskAuto.Task.IsCompleted) {
                taskTato2.TrySetResult(true);
            }
        }

        private void PictureSModelo_Click(object sender, EventArgs e) {
            if (CIFinalizado) {
                taskSModelo.TrySetResult(true);
            }
        }

        private string GetNomeSc(UnidadeDoExperimento SC) {
            if (SC.Id == CCAtual.SC1Id) {
                return "SC1";
            }
            if (SC.Id == CCAtual.SC2Id) {
                return "SC2";
            }
            if (SC.Id == CCAtual.SC3Id) {
                return "SC3";
            }
            return "Ocorreu uma inconsistência!";
        }

        private async void ClickBotao(UnidadeDoExperimento SC, Label labelMensagem, PictureBox pictureSC) {
            if (SessoesFinalizadas) {
                return;
            }

            sessaoAtual.NumeroTentativas++;
            Feedback feedback = SC.Feedback;

            if (feedback.Neutro || feedback.ValorClick == 0) {
                relatorioSessao.AdicionarEvento(new Evento(sessaoAtual, CCAtual, GetNomeSc(SC), 0));
                await Task.Delay(1000);
            }
            else {
                if (!string.IsNullOrEmpty(SC.NomeAudio)) {
                    soundPlayer = new SoundPlayer(@AudioService.GetFullPath(SC.NomeAudio));
                    soundPlayer.Play();
                }

                bool positivo = feedback.ValorClick > 0;
                Color novaCor = positivo ? Color.Green : Color.Red;
                string mensagem = positivo ? "CORRETO!" : "ERRADO!";

                if (positivo) {
                    sessaoAtual.AcertosConcecutivos++;
                }
                else {
                    sessaoAtual.AcertosConcecutivos = 0;
                }

                if (feedback.ProbabilidadeComplementar > random.Next(0, 101)) {
                    AtualizaPontos(feedback.ValorClick);
                    relatorioSessao.AdicionarEvento(new Evento(sessaoAtual, CCAtual, GetNomeSc(SC), feedback.ValorClick));
                }
                else {
                    relatorioSessao.AdicionarEvento(new Evento(sessaoAtual, CCAtual, GetNomeSc(SC), 0));
                }

                labelMensagem.Text = mensagem;
                labelMensagem.ForeColor = novaCor;
                labelMensagem.Visible = true;

                if (!feedback.SemCor) {
                    pictureSC.BackColor = novaCor;
                }

                labelMensagem.Visible = false;
                pictureSC.BackColor = Color.White;
            }


            if (sessaoAtual.NumeroTentativas >= sessaoAtual.CriterioNumeroTentativas) {
                Evento eventoEncerramento = new Evento(sessaoAtual, CCAtual, GetNomeSc(SC), 0);
                eventoEncerramento.MarcarComoEncerramento("Númeo de Tentativas", sessaoAtual.NumeroTentativas.ToString());
                relatorioSessao.AdicionarEvento(eventoEncerramento);
                taskCC.TrySetResult(true);
                return;
            }

            if (sessaoAtual.AcertosConcecutivos >= sessaoAtual.CriterioAcertosConcecutivos) {
                Evento eventoEncerramento = new Evento(sessaoAtual, CCAtual, GetNomeSc(SC), 0);
                eventoEncerramento.MarcarComoEncerramento("Acertos Consecutivos", sessaoAtual.AcertosConcecutivos.ToString());
                relatorioSessao.AdicionarEvento(eventoEncerramento);
                taskCC.TrySetResult(true);
                return;
            }

            await Task.Delay(1000);
            EmbaralhaSCs();
        }

        private void PictureSC1_Click(object sender, EventArgs e) {
            ClickBotao(SCsEmbaralhados[0], labelMensagemSC1, pictureSC1);
        }

        private void PictureSC2_Click(object sender, EventArgs e) {
            ClickBotao(SCsEmbaralhados[1], labelMensagemSC2, pictureSC2);
        }

        private void PictureSC3_Click(object sender, EventArgs e) {
            ClickBotao(SCsEmbaralhados[2], labelMensagemSC3, pictureSC3);
        }
    }
}
