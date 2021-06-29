using Pactolo.scr.dominio;
using Pactolo.scr.services;
using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pactolo.scr.view {
	public partial class TelaExperimento : Form {

		private readonly int height = Screen.PrimaryScreen.Bounds.Height;
		private readonly int width = Screen.PrimaryScreen.Bounds.Width;

		private readonly int imageHeight;
		private readonly int imageWidth;

		private readonly List<Sessao> sessoesExecutadas;

		private Sessao sessaoAtual;
		private ContingenciaColateral CCAtual;
		private List<UnidadeDoExperimento> SCsEmbaralhados = new List<UnidadeDoExperimento>();
		private Timer timerAtual;

		private readonly Random random = new Random();

		private readonly RelatorioSessao relatorioSessao;

		private List<ContingenciaColateral> pacoteCC;

		private int indiceCCAtual = 0;

		private List<PictureBox> tatoToPicture = new List<PictureBox>();
		private List<TaskCompletionSource<bool>> tasksTato;

		private TaskCompletionSource<bool> taskSModelo;
		private TaskCompletionSource<bool> taskCC;

		public TelaExperimento(List<Sessao> sessoesExecutadas, Experimentador experimentador, Participante participante) {
			InitializeComponent();

			this.sessoesExecutadas = sessoesExecutadas;

			relatorioSessao = new RelatorioSessao(sessoesExecutadas.Select(it => it.Id).Cast<long>().ToList(), experimentador, participante);

			imageHeight = Convert.ToInt32(283 * ViewUtils.heightRatio);
			imageWidth = Convert.ToInt32(333 * ViewUtils.widthRatio);

			Location = new Point(0, 0);
			Size = new Size(width, height);

			if (width != 1920 || height != 1080) {
				ViewUtils.CorrigeEscalaTodosOsFilhos(this);
			}

			panelCI.Visible = false;
			EscondeCC();

			ApresentarSessoes();
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

		private void AtualizaPontos(int pontosGanhos) {
			sessaoAtual.NumeroPontos += pontosGanhos;
			if (sessaoAtual.NumeroPontos < 0) {
				sessaoAtual.NumeroPontos = 0;
			}
			labelPontos.Text = "Pontos: " + sessaoAtual.NumeroPontos.ToString();
		}

		private void MostrarTelaFimExperimento() {
			timerAtual.Stop();
			new TelaMensagem("O Experimento foi finalizado ! Por favor, chamar o experimentador.").ShowDialog();
			Close();
		}

		private async void ApresentarSessoes() {
			foreach (Sessao sessao in sessoesExecutadas) {
				await ApresentarSessao(sessao);
				RelatorioSessaoService.GeraRelatorio(relatorioSessao);
			}
			MostrarTelaFimExperimento();
		}

		private void EventoFimTempo(Object myObject, EventArgs myEventArgs) {
			relatorioSessao.AdicionarEvento(new Evento($"Sessão {sessaoAtual.Nome}; Fim do tempo limite, o experimento foi encerrado e mais nenhuma sessão apresentada"));
			RelatorioSessaoService.GeraRelatorio(relatorioSessao);
			MostrarTelaFimExperimento();
		}

		private async Task ApresentarSessao(Sessao sessao) {
			sessaoAtual = sessao;
			List<ContingenciaColateral> CCs = sessaoAtual.CCs;

			if (sessaoAtual.Instrucao != null) {
				new TelaMensagem(sessaoAtual.Instrucao.Texto, false, true).ShowDialog();
			}

			if (sessaoAtual.CriterioDuracaoSegundos > 0) {
				timerAtual = new Timer {
					Interval = Convert.ToInt32(sessaoAtual.CriterioDuracaoSegundos) * 1000
				};
				timerAtual.Tick += new EventHandler(EventoFimTempo);
				timerAtual.Start();
			}

			bool encerrada = false;
			do {
				ContingenciaColateral CC = EscolhaCCASerApresentada();
				if (CC == null) {
					break;
				}
				EscondeCC();
				ContingenciaInstrucional CI = CC.CI;

				if (CI != null) {
					await ApresentarCI(CC);
				}
				else {
					panelCI.Visible = false;
					await ApresentarCC(CC);
				}

				if (sessaoAtual.CriterioNumeroTentativas > 0 && sessaoAtual.NumeroTentativas >= sessaoAtual.CriterioNumeroTentativas) {
					FinalizarCC("Númeo de Tentativas");
					encerrada = true;
				}


				if (sessaoAtual.CriterioAcertosConcecutivos > 0 && sessaoAtual.AcertosConcecutivos >= sessaoAtual.CriterioAcertosConcecutivos) {
					FinalizarCC("Acertos Consecutivos");
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

				if (indiceCCAtual > CCs.Count - 1) {
					return null;
				}

				CCEscolhida = CCs[indiceCCAtual];
			}

			return CCEscolhida;
		}

		private async Task ApresentarCI(ContingenciaColateral CC) {
			ContingenciaInstrucional CI = CC.CI;
			List<UnidadeDoExperimento> tatos = CI.Tatos;
			int quantidadeTatos = tatos.Count;

			tasksTato = new List<TaskCompletionSource<bool>>();
			tatoToPicture = new List<PictureBox>();

			pictureTato1.Visible = false;
			pictureTato2.Visible = false;
			pictureTato3.Visible = false;
			pictureTato4.Visible = false;
			pictureTato5.Visible = false;

			switch (quantidadeTatos) {
				case 1:
					tatoToPicture.Add(pictureTato3);

					pictureTato3.Visible = true;
					break;
				case 2:
					tatoToPicture.Add(pictureTato2);
					tatoToPicture.Add(pictureTato4);

					pictureTato2.Visible = true;
					pictureTato4.Visible = true;
					break;
				case 3:
					tatoToPicture.Add(pictureTato1);
					tatoToPicture.Add(pictureTato3);
					tatoToPicture.Add(pictureTato5);

					pictureTato1.Visible = true;
					pictureTato3.Visible = true;
					pictureTato5.Visible = true;
					break;
				case 4:
					tatoToPicture.Add(pictureTato1);
					tatoToPicture.Add(pictureTato2);
					tatoToPicture.Add(pictureTato4);
					tatoToPicture.Add(pictureTato5);

					pictureTato1.Visible = true;
					pictureTato2.Visible = true;
					pictureTato4.Visible = true;
					pictureTato5.Visible = true;
					break;
				case 5:
					tatoToPicture.Add(pictureTato1);
					tatoToPicture.Add(pictureTato2);
					tatoToPicture.Add(pictureTato3);
					tatoToPicture.Add(pictureTato4);
					tatoToPicture.Add(pictureTato5);

					pictureTato1.Visible = true;
					pictureTato2.Visible = true;
					pictureTato3.Visible = true;
					pictureTato4.Visible = true;
					pictureTato5.Visible = true;
					break;
			}

			panelCI.Visible = true;

			for (int i = 0; i < quantidadeTatos; i++) {
				tasksTato.Add(new TaskCompletionSource<bool>(false));
				tatoToPicture[i].BackColor = Color.Transparent;
				tatoToPicture[i].Image = ImageUtils.Resize(tatos[i].Imagem, imageWidth, imageHeight);
			}

			for (int i = 0; i < quantidadeTatos; i++) {
				await tasksTato[i].Task;
				relatorioSessao.AdicionarEvento(new Evento($"Sessão {sessaoAtual.Nome}; EC {CI.Nome}; Tato da imagem {tatos[i].NomeImagem} Tocado"));
				if (!CI.SemCor) {
					tatoToPicture[i].BackColor = Color.Green;
				}
			}

			for (int i = 0; i < quantidadeTatos; i++) {
				tasksTato[i].TrySetResult(false);
			}

			await ApresentarCC(CC);
		}

		private void EmbaralhaSCs(List<UnidadeDoExperimento> SCs) {
			if (SCsEmbaralhados.Count == 0) {
				SCs = SCs.OrderBy(x => random.Next()).ToList();
			}
			else {
				IEnumerable<string> imagensAnteriores = SCsEmbaralhados.Select(s => s.NomeImagem);

				if (SCs.Any(sc => !imagensAnteriores.Contains(sc.NomeImagem))) {
					SCs = SCs.OrderBy(x => random.Next()).ToList();
				}
				else {
					Dictionary<string, int> ordemAnterior = new Dictionary<string, int>();

					for (int i = 0; i < SCsEmbaralhados.Count; i++) {
						ordemAnterior[SCsEmbaralhados[i].NomeImagem] = i;
					}

					while (SCs.Any(sc => ordemAnterior[sc.NomeImagem] == SCs.IndexOf(sc))) {
						SCs = SCs.OrderBy(x => random.Next()).ToList();
					}
				}
			}

			SCsEmbaralhados = SCs;

			pictureSC1.Image = ImageUtils.Resize(SCsEmbaralhados[0].Imagem, imageWidth, imageHeight);
			pictureSC2.Image = ImageUtils.Resize(SCsEmbaralhados[1].Imagem, imageWidth, imageHeight);
			pictureSC3.Image = ImageUtils.Resize(SCsEmbaralhados[2].Imagem, imageWidth, imageHeight);
		}

		private async Task ApresentarCC(ContingenciaColateral CC) {
			taskCC = new TaskCompletionSource<bool>(false);
			taskSModelo = new TaskCompletionSource<bool>(false);

			CCAtual = CC;
			pictureSModelo.Visible = true;
			pictureSModelo.Image = ImageUtils.Resize(CC.sModelo.Imagem, imageWidth, imageHeight);
			pictureSModelo.BackColor = Color.Transparent;
			List<UnidadeDoExperimento> SCs = new List<UnidadeDoExperimento>() { CC.SC1, CC.SC2, CC.SC3 };

			await taskSModelo.Task;
			UnidadeDoExperimento sModelo = CC.sModelo;

			relatorioSessao.AdicionarEvento(new Evento($"Sessão {sessaoAtual.Nome}; MTS {CC.Nome}; SModelo da imagem {sModelo.NomeImagem} tocado"));

			if (sModelo.Feedback != null && !sModelo.Feedback.SemCor) {
				pictureSModelo.BackColor = Color.Green;
			}
			sModelo.PlayAudio();

			pictureSC3.Visible = true;
			panelPontos.Visible = true;
			pictureSC2.Visible = true;
			pictureSC1.Visible = true;

			EmbaralhaSCs(SCs);

			await taskCC.Task;
		}

		private async void ClickSC(UnidadeDoExperimento SC, Label labelMensagem, PictureBox pictureSC) {
			Feedback feedback = SC.Feedback;

			if (feedback.Neutro || feedback.ValorClick == 0) {
				sessaoAtual.AcertosConcecutivos = 0;
				sessaoAtual.NumeroTentativas++;
				relatorioSessao.AdicionarEvento(new Evento($"Sessão {sessaoAtual.Nome}; MTS {CCAtual.Nome}; SC da imagem {SC.NomeImagem} NEUTRO! tocado", -1));
				await Task.Delay(1000);
			}
			else {
				bool positivo = feedback.ValorClick > 0;
				Color novaCor = positivo ? Color.Green : Color.Red;
				string mensagem = positivo ? "CORRETO!" : "ERRADO!";

				if (feedback.ProbabilidadeComplementar > random.Next(0, 100)) {
					relatorioSessao.AdicionarEvento(new Evento($"Sessão {sessaoAtual.Nome}; MTS {CCAtual.Nome}; SC da imagem {SC.NomeImagem} {mensagem} tocado, valendo {feedback.ValorClick} pontos", positivo ? 1 : 0));
					
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
				}
				else {
					relatorioSessao.AdicionarEvento(new Evento($"Sessão {sessaoAtual.Nome}; MTS {CCAtual.Nome}; SC da imagem {SC.NomeImagem} {mensagem} tocado, porém não houve feedback por probabilidade",-1));
				}

				await Task.Delay(1000);
				labelMensagem.Visible = false;
				pictureSC.BackColor = Color.Transparent;
			}

			taskCC.TrySetResult(true);
		}

		private void FinalizarCC(string motivoFinalização) {
			relatorioSessao.AdicionarEvento(new Evento($"Sessão {sessaoAtual.Nome}; Fim da sessão. Critério: {motivoFinalização}"));
			taskCC.TrySetResult(true);
		}

		private void ComportamentoPicturesCI(PictureBox pictureBox) {
			if (!tatoToPicture.Contains(pictureBox)) {
				return;
			}

			int indiceTato = tatoToPicture.IndexOf(pictureBox);
			if (indiceTato == 0 || tasksTato[indiceTato - 1].Task.IsCompleted) {
				tasksTato[indiceTato].TrySetResult(true);
			}
		}

		private void PictureTato1_Click(object sender, EventArgs e) {
			ComportamentoPicturesCI(pictureTato1);
		}

		private void pictureTato2_Click(object sender, EventArgs e) {
			ComportamentoPicturesCI(pictureTato2);
		}

		private void PictureTato3_Click(object sender, EventArgs e) {
			ComportamentoPicturesCI(pictureTato3);
		}

		private void PictureTato5_Click(object sender, EventArgs e) {
			ComportamentoPicturesCI(pictureTato5);
		}

		private void pictureTato4_Click(object sender, EventArgs e) {
			ComportamentoPicturesCI(pictureTato4);
		}

		private void PictureSModelo_Click(object sender, EventArgs e) {
			taskSModelo.TrySetResult(true);
		}

		private void PictureSC1_Click(object sender, EventArgs e) {
			ClickSC(SCsEmbaralhados[0], labelMensagemSC1, pictureSC1);
		}

		private void PictureSC2_Click(object sender, EventArgs e) {
			ClickSC(SCsEmbaralhados[1], labelMensagemSC2, pictureSC2);
		}

		private void PictureSC3_Click(object sender, EventArgs e) {
			ClickSC(SCsEmbaralhados[2], labelMensagemSC3, pictureSC3);
		}
	}
}
