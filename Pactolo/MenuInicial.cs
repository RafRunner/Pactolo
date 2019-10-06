using Pactolo.scr.dominio;
using Pactolo.scr.enums;
using Pactolo.scr.services;
using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pactolo {
    public partial class MenuInicial : Form {

        private readonly OpenFileDialog fileDialog = new OpenFileDialog();
        private readonly string imageFilter = "PNG|*.png";
        private readonly string audioFilter = "WAV|*.wav";

        private readonly int height = Screen.PrimaryScreen.Bounds.Height;
        private readonly int width = Screen.PrimaryScreen.Bounds.Width;

        public MenuInicial() {
            InitializeComponent();

            List<ContingenciaInstrucional> CIs = ContingenciaInstrucionalService.GetAll();
            List<ContingenciaColateral> CCs = ContingenciaColateralService.GetAll();
            List<Sessao> sessoes = SessaoService.GetAll();

            comboBoxSexo.Items.AddRange(ESexo.Values());
            comboBoxEscolaridade.Items.AddRange(EEscolaridade.Values());
            comboBoxCI.Items.AddRange(CIs.Cast<object>().ToArray());
            comboBoxCI.DisplayMember = "Nome";
            comboBoxCC.Items.AddRange(CCs.Cast<object>().ToArray());
            comboBoxCC.DisplayMember = "Nome";

            listViewCI.Items.AddRange(CIs.Select(it => {
                var item = new ListViewItem(it.Nome);
                item.SubItems.Add(it.Id.ToString());
                return item;
            }).Cast<ListViewItem>().ToArray());

            listViewCC.Items.AddRange(CCs.Select(it => {
                var item = new ListViewItem(it.Nome);
                item.SubItems.Add(it.Id.ToString());
                return item;
            }).Cast<ListViewItem>().ToArray());

            listViewSessoes.Items.AddRange(sessoes.Select(it => {
                var item = new ListViewItem(it.Nome);
                item.SubItems.Add(it.Id.ToString());
                return item;
            }).Cast<ListViewItem>().ToArray());

            Image pactolo = new Bitmap(Pactolo.Properties.Resources.Pactolo);
            picturePactolo.Image = ImageUtils.Resize(pactolo, picturePactolo.Width, picturePactolo.Height);

            if (this.Width > width) {
                this.Width = width;
            }
            if (this.Height + 70 > height) {
                this.Height = height - 70;
            }
        }

        private static void RemoverDeComboBox<T>(ComboBox combox, long id) where T : ElementoDeBanco {
            object[] itens = new object[combox.Items.Count];
            combox.Items.CopyTo(itens, 0);

            foreach (T item in itens) {
                if (item.Id == id) {
                    combox.Items.Remove(item);
                    break;
                }
            }
        }

        private void CheckBoxTentativasAgrp_CheckedChanged(object sender, EventArgs e) {
            if (checkBoxTentativasAgrp.Checked) {
                checkBoxTentativasRand.Checked = false;
            }
            else {
                checkBoxTentativasRand.Checked = true;
            }
        }

        private void CheckBoxTentativasRand_CheckedChanged(object sender, EventArgs e) {
            if (checkBoxTentativasRand.Checked) {
                checkBoxTentativasAgrp.Checked = false;
            }
            else {
                checkBoxTentativasAgrp.Checked = true;
            }
        }

        private Experimentador CriaExperimentadorPelosCampos() {
            Experimentador experimentador = new Experimentador {
                Nome = textBoxNomeExperimentador.Text,
                Email = textBoxEmailExperimentador.Text,
                Projeto = textBoxProjetoExperimentador.Text
            };
            return experimentador;
        }
        private void CarregarExperimentador(long id) {
            Experimentador experimentador = ExperimentadorService.GetById(id);
            textBoxNomeExperimentador.Text = experimentador.Nome;
            textBoxEmailExperimentador.Text = experimentador.Email;
            textBoxProjetoExperimentador.Text = experimentador.Projeto;
        }

        private void ButtonSalvarExperimentador_Click(object sender, EventArgs e) {
            Experimentador experimentador = CriaExperimentadorPelosCampos();
            ExperimentadorService.Salvar(experimentador);
            MessageBox.Show("Experimentador cadastrado com sucesso!", "Sucesso");
        }

        private void ButtonGerenciarExperimentador_Click(object sender, EventArgs e) {
            new GridCrud(
                ExperimentadorService.GetAll,
                Experimentador.GetOrdemCulunasGrid(),
                ExperimentadorService.FilterDataTable,
                EditarExperimentador.CarregarParaEdicao,
                ExperimentadorService.DeletarPorId,
                CarregarExperimentador);
        }

        private Participante CriarParticipantePelosCampos() {
            Participante participante = new Participante {
                Nome = textBoxNomeParticipante.Text,
                Email = textBoxEmailParticipante.Text,
                Idade = Convert.ToInt32(numericIdade.Value),
                Sexo = (string)comboBoxSexo.SelectedItem,
                Escolaridade = (string)comboBoxEscolaridade.SelectedItem
            };
            return participante;
        }

        private void CarregarParticipante(long id) {
            Participante participante = ParticipanteService.GetById(id);
            textBoxNomeParticipante.Text = participante.Nome;
            textBoxEmailParticipante.Text = participante.Email;
            numericIdade.Value = participante.Idade;
            comboBoxSexo.SelectedItem = participante.Sexo;
            comboBoxEscolaridade.SelectedItem = participante.Escolaridade;
        }

        private void ButtonSalvarParticipante_Click(object sender, EventArgs e) {
            Participante participante = CriarParticipantePelosCampos();
            ParticipanteService.Salvar(participante);
            MessageBox.Show("Particpante cadastrado com sucesso!", "Sucesso");
        }

        private void ButtonGerenciarParticipante_Click(object sender, EventArgs e) {
            new GridCrud(
                ParticipanteService.GetAll,
                Participante.GetOrdemColunasGrid(),
                ParticipanteService.FilterDataTable,
                EditarParticipante.CarregarParaEdicao,
                ParticipanteService.DeletarPorId,
                CarregarParticipante);
        }

        private string SelecionaArquivoComFiltro(string filter = null) {
			string retorno = string.Empty;
            if (filter != null) {
                fileDialog.Filter = filter;
            }
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK) {
                retorno = fileDialog.FileName;
            }
            fileDialog.Filter = string.Empty;
			return retorno;
        }

        private void ButtonCarregarTato_CLick(object sender, EventArgs e) {
			ListViewItem itemTato = new ListViewItem(SelecionaArquivoComFiltro(imageFilter));
			listViewTatos.Items.Add(itemTato);
        }

		private void RemoverTato_Click(object sender, EventArgs e) {
			listViewTatos.Items.Remove(listViewTatos.SelectedItems[0]);
		}

		private ContingenciaInstrucional CriaCIPelosCampos() {
			List<UnidadeDoExperimento> tatos = new List<UnidadeDoExperimento>();
			foreach (ListViewItem item in listViewTatos.Items) {
				UnidadeDoExperimento tato = new UnidadeDoExperimento { NomeImagem = item.Text };
				tatos.Add(tato);
			}

            string nome = textBoxNomeCI.Text;

            ContingenciaInstrucional CI = new ContingenciaInstrucional {
                Nome = nome,
				Tatos = tatos
            };
            return CI;
        }

        private void ButtonCadastrarCI_Click(object sender, EventArgs e) {
            ContingenciaInstrucional CI = CriaCIPelosCampos();
            ContingenciaInstrucionalService.Salvar(CI);
            comboBoxCI.Items.Add(CI);

            ListViewItem itemCI = new ListViewItem(CI.Nome);
            itemCI.SubItems.Add(CI.Id.ToString());
            listViewCI.Items.Add(itemCI);
        }

        private ContingenciaInstrucional GetCISelecionada() {
            if (listViewCI.SelectedItems.Count == 0) {
                return null;
            }

            long id = long.Parse(listViewCI.SelectedItems[0].SubItems[1].Text);
            ContingenciaInstrucional CI = ContingenciaInstrucionalService.GetById(id);
            return CI;
        }

        private void ButtonSelecionarCI_Click(object sender, EventArgs e) {
            ContingenciaInstrucional CI = GetCISelecionada();
            if (CI == null) {
                return;
            }

			textBoxNomeCI.Text = CI.Nome;

            foreach (UnidadeDoExperimento tato in CI.Tatos) {
				ListViewItem itemTato = new ListViewItem(tato.NomeImagem);
				listViewTatos.Items.Add(itemTato);
			}
        }

        private void ButtonApagarCI_Click(object sender, EventArgs e) {
            ContingenciaInstrucional CI = GetCISelecionada();
            if (CI == null) {
                return;
            }

            RemoverDeComboBox<ContingenciaInstrucional>(comboBoxCI, CI.Id);
            ContingenciaInstrucionalService.Deletar(CI);
            listViewCI.Items.Remove(listViewCI.SelectedItems[0]);
        }

        private ContingenciaColateral CriaCCPelosCampos() {
            ContingenciaInstrucional CI = comboBoxCI.SelectedItem as ContingenciaInstrucional;

            UnidadeDoExperimento sModelo = new UnidadeDoExperimento {
                NomeImagem = ImagemService.CopiaImagemParaPasta(textBoxModelo.Text)
            };

            Feedback feedbackSC1 = new Feedback {
                ValorClick = Convert.ToInt32(numericSC1.Value),
                Neutro = checkBoxNeutroSC1.Checked,
                SemCor = checkBoxSemCorSC1.Checked,
                ProbabilidadeComplementar = Convert.ToInt32(numericProbabilidadeSC1.Value)
            };
            FeedbackService.Salvar(feedbackSC1);
            UnidadeDoExperimento SC1 = new UnidadeDoExperimento {
                Feedback = feedbackSC1,
                NomeImagem = ImagemService.CopiaImagemParaPasta(textBoxSC1.Text),
                NomeAudio = AudioService.CopiaAudioParaPasta(textBoxAudioSC1.Text)
            };

            Feedback feedbackSC2 = new Feedback {
                ValorClick = Convert.ToInt32(numericSC2.Value),
                Neutro = checkBoxNeutroSC2.Checked,
                SemCor = checkBoxSemCorSC2.Checked,
                ProbabilidadeComplementar = Convert.ToInt32(numericProbabilidadeSC2.Value)
            };
            FeedbackService.Salvar(feedbackSC2);
            UnidadeDoExperimento SC2 = new UnidadeDoExperimento {
                Feedback = feedbackSC2,
                NomeImagem = ImagemService.CopiaImagemParaPasta(textBoxSC2.Text),
                NomeAudio = AudioService.CopiaAudioParaPasta(textBoxAudioSC2.Text)
            };

            Feedback feedbackSC3 = new Feedback {
                ValorClick = Convert.ToInt32(numericSC3.Value),
                Neutro = checkBoxNeutroSC3.Checked,
                SemCor = checkBoxSemCorSC3.Checked,
                ProbabilidadeComplementar = Convert.ToInt32(numericProbabilidadeSC3.Value)
            };
            FeedbackService.Salvar(feedbackSC3);
            UnidadeDoExperimento SC3 = new UnidadeDoExperimento {
                Feedback = feedbackSC3,
                NomeImagem = ImagemService.CopiaImagemParaPasta(textBoxSC3.Text),
                NomeAudio = AudioService.CopiaAudioParaPasta(textBoxAudioSC3.Text)
            };


            UnidadeDoExperimentoService.Salvar(new List<UnidadeDoExperimento>() { sModelo, SC1, SC2, SC3 });

            ContingenciaColateral CC = new ContingenciaColateral {
                Nome = textBoxNomeCC.Text,
                CI = CI,
                sModelo = sModelo,
                SC1 = SC1,
                SC2 = SC2,
                SC3 = SC3
            };
            return CC;
        }

        private void ButtonCadastrarCC_Click(object sender, EventArgs e) {
            ContingenciaColateral CC = CriaCCPelosCampos();
            ContingenciaColateralService.Salvar(CC);

            ListViewItem itemCC = new ListViewItem(CC.Nome);
            itemCC.SubItems.Add(CC.Id.ToString());
            listViewCC.Items.Add(itemCC);
            comboBoxCC.Items.Add(CC);
        }

        private ContingenciaColateral GetCCSelecionada() {
            if (listViewCC.SelectedItems.Count == 0) {
                return null;
            }

            long id = long.Parse(listViewCC.SelectedItems[0].SubItems[1].Text);
            ContingenciaColateral CC = ContingenciaColateralService.GetById(id);
            return CC;
        }

        private void ButtonSelecionarCC_Click(object sender, EventArgs e) {
            ContingenciaColateral CC = GetCCSelecionada();
            if (CC == null) {
                return;
            }

            textBoxNomeCC.Text = CC.Nome;
            foreach (ContingenciaInstrucional CI in comboBoxCI.Items) {
                if (CI.Id == CC.CI?.Id) {
                    comboBoxCI.SelectedItem = CI;
                    break;
                }
            }

            textBoxModelo.Text = CC.sModelo.NomeImagem;
            textBoxSC1.Text = CC.SC1.NomeImagem;
            textBoxSC2.Text = CC.SC2.NomeImagem;
            textBoxSC3.Text = CC.SC3.NomeImagem;
            textBoxAudioSC1.Text = CC.SC1.NomeAudio;
            textBoxAudioSC2.Text = CC.SC2.NomeAudio;
            textBoxAudioSC3.Text = CC.SC3.NomeAudio;

            checkBoxNeutroSC1.Checked = CC.SC1.Feedback.Neutro;
            checkBoxNeutroSC2.Checked = CC.SC2.Feedback.Neutro;
            checkBoxNeutroSC3.Checked = CC.SC3.Feedback.Neutro;
            checkBoxSemCorSC1.Checked = CC.SC1.Feedback.SemCor;
            checkBoxSemCorSC2.Checked = CC.SC2.Feedback.SemCor;
            checkBoxSemCorSC3.Checked = CC.SC3.Feedback.SemCor;

            numericProbabilidadeSC1.Value = CC.SC1.Feedback.ProbabilidadeComplementar;
            numericProbabilidadeSC2.Value = CC.SC2.Feedback.ProbabilidadeComplementar;
            numericProbabilidadeSC3.Value = CC.SC3.Feedback.ProbabilidadeComplementar;
            numericSC1.Value = CC.SC1.Feedback.ValorClick;
            numericSC2.Value = CC.SC2.Feedback.ValorClick;
            numericSC3.Value = CC.SC3.Feedback.ValorClick;
        }

        private void ButtonApagarCC_Click(object sender, EventArgs e) {
            DialogResult result = MessageBox.Show("Ao deletar essa CC ela será deletada também das sessoes onde estiver cadastrada, deseja continuar?", "Confirmação necessária", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) {
                return;
            }
            ContingenciaColateral CC = GetCCSelecionada();
            if (CC == null) {
                return;
            }

            RemoverDeComboBox<ContingenciaColateral>(comboBoxCC, CC.Id);
            CCPorSessaoService.DeletarAllByCCId(CC.Id);
            ContingenciaColateralService.Deletar(CC);
            listViewCC.Items.Remove(listViewCC.SelectedItems[0]);
        }

        private void ButtonCarregarModelo_Click(object sender, EventArgs e) {
			textBoxModelo.Text = SelecionaArquivoComFiltro(imageFilter);
        }

        private void ButtonCarregarImagemSC1_Click(object sender, EventArgs e) {
			textBoxSC1.Text = SelecionaArquivoComFiltro(imageFilter);
        }

        private void ButtonCarregarImagemSC2_Click(object sender, EventArgs e) {
			textBoxSC2.Text = SelecionaArquivoComFiltro(imageFilter);
        }

        private void ButtonCarregarImagemSC3_Click(object sender, EventArgs e) {
			textBoxSC3.Text = SelecionaArquivoComFiltro(imageFilter);
        }

        private void ButtonCarregarAudioSC1_Click(object sender, EventArgs e) {
			textBoxAudioSC1.Text =  SelecionaArquivoComFiltro(audioFilter);
        }

        private void ButtonCarregarAudioSC2_Click(object sender, EventArgs e) {
			textBoxAudioSC2.Text = SelecionaArquivoComFiltro(audioFilter);
        }

        private void ButtonCarregarAudioSC3_Click(object sender, EventArgs e) {
			textBoxAudioSC3.Text = SelecionaArquivoComFiltro(audioFilter);
        }

        private Sessao CriaSessaoPelosCampos() {
            List<ContingenciaColateral> CCs = new List<ContingenciaColateral>();
            foreach (ListViewItem item in listViewCCSessao.Items) {
                long CCid = long.Parse(item.SubItems[1].Text);
                CCs.Add(ContingenciaColateralService.GetById(CCid));
            }

            Instrucao instrucao = null;
            if (!string.IsNullOrWhiteSpace(textInstrucao.Text)) {
                instrucao = new Instrucao {
                    Texto = textInstrucao.Text
                };
                InstrucaoService.Salvar(instrucao);
            }

            Sessao sessao = new Sessao {
                Nome = textBoxNomeSessao.Text,
                CCs = CCs,
                OrdemAleatoria = checkBoxTentativasRand.Checked,
                CriterioAcertosConcecutivos = Convert.ToInt32(numericAcertosConsec.Value),
                CriterioNumeroTentativas = Convert.ToInt32(numericNTentativas.Value),
                CriterioDuracaoSegundos = Convert.ToInt32(numericDuracao.Value),
                Instrucao = instrucao
            };
            return sessao;
        }

        private void ButtonCadastrarSessao_Click(object sender, EventArgs e) {
            Sessao sessao = CriaSessaoPelosCampos();
            SessaoService.Salvar(sessao);

            ListViewItem itemSessao = new ListViewItem(sessao.Nome);
            itemSessao.SubItems.Add(sessao.Id.ToString());
            listViewSessoes.Items.Add(itemSessao);
        }

        private void ButtonAddCC_Click(object sender, EventArgs e) {
            ContingenciaColateral CC = comboBoxCC.SelectedItem as ContingenciaColateral;
            if (CC == null) {
                return;
            }

            ListViewItem itemCC = new ListViewItem(CC.Nome);
            itemCC.SubItems.Add(CC.Id.ToString());
            listViewCCSessao.Items.Add(itemCC);
        }

        private void ButtonRemoverCC_Click(object sender, EventArgs e) {
            if (listViewCCSessao.SelectedItems.Count == 0) {
                return;
            }
            listViewCCSessao.Items.Remove(listViewCCSessao.SelectedItems[0]);
        }

        private void ButtonSelecioarSessao_Click(object sender, EventArgs e) {
            if (listViewSessoes.SelectedItems.Count == 0) {
                MessageBox.Show("Nenhuma sessão selecionada/cadastrada!", "Advertência");
                return;
            }

            long id = long.Parse(listViewSessoes.SelectedItems[0].SubItems[1].Text);
            Sessao sessao = SessaoService.GetById(id);

            textBoxNomeSessao.Text = sessao.Nome;
            numericAcertosConsec.Value = sessao.CriterioAcertosConcecutivos;
            numericDuracao.Value = sessao.CriterioDuracaoSegundos;
            numericNTentativas.Value = sessao.CriterioNumeroTentativas;
            checkBoxTentativasAgrp.Checked = !sessao.OrdemAleatoria;
            checkBoxTentativasRand.Checked = sessao.OrdemAleatoria;
            if (sessao.Instrucao != null) {
                textInstrucao.Text = sessao.Instrucao.Texto;
            }

            listViewCCSessao.Items.Clear();
            foreach (ContingenciaColateral CC in sessao.CCs) {
                ListViewItem itemCC = new ListViewItem(CC.Nome);
                itemCC.SubItems.Add(CC.Id.ToString());
                listViewCCSessao.Items.Add(itemCC);
            }
        }

        private void ButtonRemoverSessao_Click(object sender, EventArgs e) {
            if (listViewsessoesExecutadas.SelectedItems.Count == 0) {
                MessageBox.Show("Nenhuma sessão selecionada/cadastrada!", "Advertência");
                return;
            }
            listViewsessoesExecutadas.Items.Remove(listViewsessoesExecutadas.SelectedItems[0]);
        }

        private void ButtonExcluirSessao_Click(object sender, EventArgs e) {
            if (listViewSessoes.SelectedItems.Count == 0) {
                MessageBox.Show("Nenhuma sessão selecionada/cadastrada!", "Advertência");
                return;
            }

            long id = long.Parse(listViewSessoes.SelectedItems[0].SubItems[1].Text);
            Sessao sessao = SessaoService.GetById(id);
            SessaoService.Deletar(sessao);

            listViewSessoes.Items.Remove(listViewSessoes.SelectedItems[0]);
            ListViewItem[] sessaoExecutada = listViewsessoesExecutadas.Items.Find(sessao.Nome, false);
            if (sessaoExecutada.Length != 0) {
                listViewsessoesExecutadas.Items.Remove(sessaoExecutada[0]);
            }
        }

        private void ButtonAdicionarSessao_Click(object sender, EventArgs e) {
            if (listViewSessoes.SelectedItems.Count == 0) {
                MessageBox.Show("Nenhuma sessão selecionada/cadastrada!", "Advertência");
                return;
            }

            ListViewItem sessao = listViewSessoes.SelectedItems[0];
            ListViewItem sessaoClone = sessao.Clone() as ListViewItem;
            listViewsessoesExecutadas.Items.Add(sessaoClone);
        }

        private void ButtonIniciar_Click(object sender, EventArgs e) {
            if (listViewsessoesExecutadas.Items.Count == 0) {
                MessageBox.Show("Por favor, selecione pelo menos uma Sessão para executar!", "Advertência");
                return;
            }

            List<Sessao> sessoes = new List<Sessao>();
            foreach (ListViewItem sessao in listViewsessoesExecutadas.Items) {
                sessoes.Add(SessaoService.GetById(long.Parse(sessao.SubItems[1].Text)));
            }
            Experimentador experimentador = CriaExperimentadorPelosCampos();
            Participante participante = CriarParticipantePelosCampos();

            TelaExperimento telaExperimento = new TelaExperimento(sessoes, experimentador, participante);
            telaExperimento.ShowDialog();
        }

        private void CarregarInstrucao(long id) {
            Instrucao instrucao = InstrucaoService.GetById(id);
            textInstrucao.Text = instrucao.Texto;
        }

        private void ButtonGerenciarInstrucoes_Click(object sender, EventArgs e) {
            new GridCrud(
              InstrucaoService.GetAll,
              Instrucao.GetOrdemCulunasGrid(),
              InstrucaoService.FilterDataTable,
              null,
              InstrucaoService.DeletarPorId,
              CarregarInstrucao);
        }

        private void ButtonMaisSobreSoftware_Click(object sender, EventArgs e) {
            MessageBox.Show("O Pactolo é um software para estudo da relação entre contingências. A primeira contingência tem como consequência o acesso à tarefa de escolha de acordo com o modelo, a segunda contingência. Originalmente foi desenvolvido para estudo do efeito de estímulos verbais sobre o aprendizado de discriminações condicionais e formação de classes de equivalência, entretanto o software permite variações quanto aos parâmetros.\n" +
"O nome Pactolo vem do mito do Rei Midas, uma alegoria sobre a ganância.Baco, a pedido do próprio Midas, concedera ao rei o dom de transformar tudo que tocava em ouro. Entretanto, Midas percebeu que seu desejo era uma maldição, e acabou por transformar sua própria filha, Phoebe, em uma estátua de ouro.  Midas, em desespero, pede a Baco para se livrar de seu “dom”. Baco disse que Midas deveria se banhar na fonte do rio Pactolo, para que pudesse se livrar de sua maldição.\n" +
"\nDelineamento: João Lucas Bernardy(Universidade de São Paulo)\n" +
"Automação: Rafael Nunes Santana(Universidade Federal de Goiás) e Emanuel Borges Passinato(Universidade Federal de Goiás)\n" +
"\nComo citar este software:\n" +
"\nAPA\n" +
"Bernardy, J.L., Santana, R.N., &Passinato, E.B. (2019).Pactolo(Versão 1.0). [Software].São Paulo, SP: Universidade de São Paulo\n" +
"\nABNT\n" +
"BERNARDY, João Lucas; SANTANA, Rafael Nunes; PASSINATO, Emanuel Borges. Pactolo.Versão 1.0.São Paulo.\n" +
"\nContato: bernardy @usp.br\n"
		,"Informações");
        }
	}
}
