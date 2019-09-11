using Pactolo.scr.dominio;
using Pactolo.scr.dominio.eventos;
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
        private System.Windows.Forms.Timer timerAtual;

        private readonly Random random = new Random();

        private readonly RelatorioSessao relatorioSessao;

        private readonly int imageHeight;
        private readonly int imageWidth;

        private readonly int height = Screen.PrimaryScreen.Bounds.Height;
        private readonly int width = Screen.PrimaryScreen.Bounds.Width;

        private readonly double heightRatio;
        private readonly double widthRatio;

        private bool CIFinalizado = false;
        private bool SessoesFinalizadas = false;

        private List<ContingenciaColateral> pacoteCC;
        private UnidadeDoExperimento ultimoSC;

        private int indiceCCAtual = 0;

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
            heightRatio = height / 1080.0;
            widthRatio = width / 1920.0;
            imageHeight = Convert.ToInt32(283 * heightRatio);
            imageWidth = Convert.ToInt32(333 * widthRatio);
        }

        private void EscondeCC() {
            panelPontos.Visible = false;
            pictureSModelo.Visible = false;
            pictureSC1.Visible = false;
            pictureSC2.Visible = false;
            pictureSC3.Visible = false;
            labelMensagemSC1.Visible = false;
            labelMensagemSC2.Visible = false;
            labelMensagemSC3.Visible = false;
        }

        private void TelaExperimento_Load(object sender, EventArgs e) {
            Location = new Point(0, 0);
            Size = new Size(width, height);
            if (width != 1920 || height != 1080) {
                ResizeComponents();
            }

            panelCI.Visible = false;
            EscondeCC();

            ApresentarSessoes();
        }

        private void CorrigeTamanhoEPosicao(Control controle) {
            controle.Height = Convert.ToInt32(controle.Height * heightRatio);
            controle.Width = Convert.ToInt32(controle.Width * widthRatio);
            controle.Location = new Point {
                X = Convert.ToInt32(controle.Location.X * widthRatio),
                Y = Convert.ToInt32(controle.Location.Y * heightRatio)
            };
        }

        private void CorrigeFonte(Label label) {
            label.Font = new Font(label.Font.Name, Convert.ToInt32(label.Font.Size * heightRatio));
        }

        private void ResizeComponents() {
            CorrigeTamanhoEPosicao(panelCI);
            CorrigeTamanhoEPosicao(pictureTato1);
            CorrigeTamanhoEPosicao(pictureAuto);
            CorrigeTamanhoEPosicao(pictureTato2);
            CorrigeTamanhoEPosicao(pictureSModelo);
            CorrigeTamanhoEPosicao(pictureSC1);
            CorrigeTamanhoEPosicao(pictureSC2);
            CorrigeTamanhoEPosicao(pictureSC3);
            CorrigeTamanhoEPosicao(panelPontos);
            CorrigeTamanhoEPosicao(labelPontos);
            CorrigeTamanhoEPosicao(labelMensagemSC1);
            CorrigeTamanhoEPosicao(labelMensagemSC2);
            CorrigeTamanhoEPosicao(labelMensagemSC3);
            CorrigeFonte(labelPontos);
            CorrigeFonte(labelMensagemSC1);
            CorrigeFonte(labelMensagemSC2);
            CorrigeFonte(labelMensagemSC3);
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

        private void EventoFimTempo(Object myObject, EventArgs myEventArgs) {
            timerAtual.Stop();
            taskSModelo.TrySetResult(true);
            FinalizarCC("Fim do tempo limite", CCAtual.SC1);
            return;
        }

        private async Task ApresentarSessao(Sessao sessao) {
            sessaoAtual = sessao;
            List<ContingenciaColateral> CCs = sessaoAtual.CCs;

            if (sessaoAtual.Instrucao != null) {
                TelaInstrucao telaInstrucao = new TelaInstrucao(sessaoAtual.Instrucao.Texto);
                telaInstrucao.ShowDialog();
            }

            if (sessaoAtual.CriterioDuracaoSegundos > 0) {
                timerAtual = new System.Windows.Forms.Timer {
                    Interval = Convert.ToInt32(sessaoAtual.CriterioDuracaoSegundos) * 1000
                };
                timerAtual.Tick += new EventHandler(EventoFimTempo);
                timerAtual.Start();
            }

            bool encerrada = false;
            do {
                ContingenciaColateral CC = EscolhaCCASerApresentada();
                if (CCAtual != CC) {
                    EscondeCC();
                    ContingenciaInstrucional CI = CC.CI;
                    
                    if (CI != null) {
                        await ApresentarCI(CC);
                    }
                    else {
                        panelCI.Visible = false;
                        CIFinalizado = true;
                        await ApresentarCC(CC, true);
                    }
                }
                else {
                    await ApresentarCC(CC, false);
                }

                if (sessaoAtual.CriterioNumeroTentativas > 0 && sessaoAtual.NumeroTentativas >= sessaoAtual.CriterioNumeroTentativas) {
                    FinalizarCC("Númeo de Tentativas", ultimoSC);
                    encerrada = true;
                }

                if (sessaoAtual.CriterioAcertosConcecutivos > 0 && sessaoAtual.AcertosConcecutivos >= sessaoAtual.CriterioAcertosConcecutivos) {
                    FinalizarCC("Acertos Consecutivos", ultimoSC);
                    encerrada = true;
                }

            } while (!encerrada);

            indiceCCAtual = 0;
        }

        private ContingenciaColateral EscolhaCCASerApresentada() {
            List<ContingenciaColateral> CCs = sessaoAtual.CCs;
            ContingenciaColateral CCEscolhida;

            if (sessaoAtual.OrdemAleatoria) {
                if (pacoteCC == null || pacoteCC.Count == CCs.Count) {
                    pacoteCC = new List<ContingenciaColateral>();
                }
                do {
                    CCEscolhida = CCs[random.Next(0, CCs.Count)];
                } while (pacoteCC.Contains(CCEscolhida));

                pacoteCC.Add(CCEscolhida);
            }
            else {
                int criterioAcertosEssaCC = Convert.ToInt32(sessaoAtual.CriterioAcertosConcecutivos / CCs.Count);

                if (indiceCCAtual == CCs.Count - 1) {
                    criterioAcertosEssaCC += sessaoAtual.CriterioAcertosConcecutivos % CCs.Count;
                }

                if (CCs[indiceCCAtual].AcertosConcecutivos >= criterioAcertosEssaCC) {
                    indiceCCAtual++;
                }

                CCEscolhida = CCs[indiceCCAtual];
            }

            return CCEscolhida;
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
            relatorioSessao.AdicionarEvento(new EventoCI(sessaoAtual, CI, CI.Tato1));
            await taskAuto.Task;
            pictureAuto.BackColor = Color.Green;
            relatorioSessao.AdicionarEvento(new EventoCI(sessaoAtual, CI, CI.Autoclitico));
            await taskTato2.Task;
            pictureTato2.BackColor = Color.Green;
            relatorioSessao.AdicionarEvento(new EventoCI(sessaoAtual, CI, CI.Tato2));

            taskTato1.TrySetResult(false);
            taskAuto.TrySetResult(false);
            taskTato2.TrySetResult(false);
            CIFinalizado = true;
            await ApresentarCC(CC, true);
        }

        private void EmbaralhaSCs() {
            SCsEmbaralhados = ListUtils.EmbaralhaMudandoPosicao(SCsEmbaralhados);

            pictureSC1.Image = ImageUtils.Resize(SCsEmbaralhados[0].Imagem, imageWidth, imageHeight);
            pictureSC2.Image = ImageUtils.Resize(SCsEmbaralhados[1].Imagem, imageWidth, imageHeight);
            pictureSC3.Image = ImageUtils.Resize(SCsEmbaralhados[2].Imagem, imageWidth, imageHeight);
        }

        private async Task ApresentarCC(ContingenciaColateral CC, bool recomecar) {
            taskCC = new TaskCompletionSource<bool>(false);

            if (recomecar) {
                taskFinalizacao = new TaskCompletionSource<bool>(false);
                taskSModelo = new TaskCompletionSource<bool>(false);

                pictureSC3.Visible = false;
                panelPontos.Visible = false;
                pictureSC2.Visible = false;
                pictureSC1.Visible = false;

                CCAtual = CC;
                SCsEmbaralhados = new List<UnidadeDoExperimento>() { CC.SC1, CC.SC2, CC.SC3 };
                pictureSModelo.Visible = true;
                pictureSModelo.Image = ImageUtils.Resize(CC.sModelo.Imagem, imageWidth, imageHeight);

                await taskSModelo.Task;
                relatorioSessao.AdicionarEvento(new EventoSModelo(sessaoAtual, CC, CC.sModelo.NomeImagem));

                pictureSC3.Visible = true;
                panelPontos.Visible = true;
                pictureSC2.Visible = true;
                pictureSC1.Visible = true;
            }

            EmbaralhaSCs();
            await taskCC.Task;
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

            Feedback feedback = SC.Feedback;

            if (feedback.Neutro || feedback.ValorClick == 0) {
                sessaoAtual.AcertosConcecutivos = 0;
                sessaoAtual.NumeroTentativas++;
                relatorioSessao.AdicionarEvento(new EventoSC(sessaoAtual, CCAtual, GetNomeSc(SC), 0));
                await Task.Delay(1000);
            }
            else {
                if (feedback.ProbabilidadeComplementar > random.Next(0, 100)) {
                    bool positivo = feedback.ValorClick > 0;
                    Color novaCor = positivo ? Color.Green : Color.Red;
                    string mensagem = positivo ? "CORRETO!" : "ERRADO!";

                    sessaoAtual.NumeroTentativas++;

                    if (positivo) {
                        sessaoAtual.AcertosConcecutivos++;
                        CCAtual.AcertosConcecutivos++;
                    }
                    else {
                        sessaoAtual.AcertosConcecutivos = 0;
                        CCAtual.AcertosConcecutivos = 0;
                    }

                    AtualizaPontos(feedback.ValorClick);
                    SC.PlayAudio();
                    labelMensagem.Text = mensagem;
                    labelMensagem.ForeColor = novaCor;
                    labelMensagem.Visible = true;

                    if (!feedback.SemCor) {
                        pictureSC.BackColor = novaCor;
                    }

                    relatorioSessao.AdicionarEvento(new EventoSC(sessaoAtual, CCAtual, GetNomeSc(SC), feedback.ValorClick));
                }
                else {
                    relatorioSessao.AdicionarEvento(new EventoSC(sessaoAtual, CCAtual, GetNomeSc(SC), 0));
                }

                await Task.Delay(1000);
                labelMensagem.Visible = false;
                pictureSC.BackColor = Color.White;
            }

            ultimoSC = SC;
            taskCC.TrySetResult(true);
        }

        private void FinalizarCC(string motivoFinalização, UnidadeDoExperimento SC) {
            EventoSC eventoEncerramento = new EventoSC(sessaoAtual, CCAtual, GetNomeSc(SC), 0);
            eventoEncerramento.MarcarComoEncerramento(motivoFinalização, sessaoAtual.NumeroTentativas.ToString());
            relatorioSessao.AdicionarEvento(eventoEncerramento);
            taskCC.TrySetResult(true);
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
