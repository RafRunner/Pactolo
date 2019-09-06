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
            if (this.Height + 50 > height) {
                this.Height = height - 50;
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

        private void SelecionaArquivoComFiltro(TextBox textBox, string filter = null) {
            if (filter != null) {
                fileDialog.Filter = filter;
            }
            if (fileDialog.ShowDialog() == DialogResult.OK) {
                textBox.Text = fileDialog.FileName;
            }
            fileDialog.Filter = "";
        }

        private void ButtonCarregarTato1_Click(object sender, EventArgs e) {
            SelecionaArquivoComFiltro(textBoxTato1CI, imageFilter);
        }

        private void ButtonCarregarAuto_Click(object sender, EventArgs e) {
            SelecionaArquivoComFiltro(textBoxAutocliticoCI, imageFilter);
        }

        private void ButtonCarregarTato2_Click(object sender, EventArgs e) {
            SelecionaArquivoComFiltro(textBoxTato2CI, imageFilter);
        }

        private ContingenciaInstrucional CriaCIPelosCampos() {
            string caminhoImagemTato1 = textBoxTato1CI.Text;
            string caminhoImagemAuto = textBoxAutocliticoCI.Text;
            string caminhoImagemTato2 = textBoxTato2CI.Text;

            string nome = textBoxNomeCI.Text;

            UnidadeDoExperimento tato1 = new UnidadeDoExperimento { NomeImagem = ImagemService.CopiaImagemParaPasta(caminhoImagemTato1) };
            UnidadeDoExperimento auto = new UnidadeDoExperimento { NomeImagem = ImagemService.CopiaImagemParaPasta(caminhoImagemAuto) };
            UnidadeDoExperimento tato2 = new UnidadeDoExperimento { NomeImagem = ImagemService.CopiaImagemParaPasta(caminhoImagemTato2) };

            UnidadeDoExperimentoService.Salvar(new List<UnidadeDoExperimento>() { tato1, auto, tato2 });

            ContingenciaInstrucional CI = new ContingenciaInstrucional {
                Nome = nome,
                Tato1 = tato1,
                Autoclitico = auto,
                Tato2 = tato2
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

        private void ButtonSelecionrCI_Click(object sender, EventArgs e) {
            ContingenciaInstrucional CI = GetCISelecionada();
            if (CI == null) {
                return;
            }

            textBoxNomeCI.Text = CI.Nome;
            textBoxTato1CI.Text = CI.Tato1.NomeImagem;
            textBoxAutocliticoCI.Text = CI.Autoclitico.NomeImagem;
            textBoxTato2CI.Text = CI.Tato2.NomeImagem;
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
            ContingenciaColateral CC = GetCCSelecionada();
            if (CC == null) {
                return;
            }

            RemoverDeComboBox<ContingenciaColateral>(comboBoxCC, CC.Id);
            ContingenciaColateralService.Deletar(CC);
            listViewCC.Items.Remove(listViewCC.SelectedItems[0]);
        }

        private void ButtonCarregarModelo_Click(object sender, EventArgs e) {
            SelecionaArquivoComFiltro(textBoxModelo, imageFilter);
        }

        private void ButtonCarregarImagemSC1_Click(object sender, EventArgs e) {
            SelecionaArquivoComFiltro(textBoxSC1, imageFilter);
        }

        private void ButtonCarregarImagemSC2_Click(object sender, EventArgs e) {
            SelecionaArquivoComFiltro(textBoxSC2, imageFilter);
        }

        private void ButtonCarregarImagemSC3_Click(object sender, EventArgs e) {
            SelecionaArquivoComFiltro(textBoxSC3, imageFilter);
        }

        private void ButtonCarregarAudioSC1_Click(object sender, EventArgs e) {
            SelecionaArquivoComFiltro(textBoxAudioSC1, audioFilter);
        }

        private void ButtonCarregarAudioSC2_Click(object sender, EventArgs e) {
            SelecionaArquivoComFiltro(textBoxAudioSC2, audioFilter);
        }

        private void ButtonCarregarAudioSC3_Click(object sender, EventArgs e) {
            SelecionaArquivoComFiltro(textBoxAudioSC3, audioFilter);
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
                MessageBox.Show("Nenhuma sessão selecionada!", "Advertência");
                return;
            }
            listViewsessoesExecutadas.Items.Remove(listViewsessoesExecutadas.SelectedItems[0]);
        }

        private void ButtonExcluirSessao_Click(object sender, EventArgs e) {
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
            MessageBox.Show("Desenvolvido por:\nRafal Nunes Santana e Emanuel Borges Passinato", "Informações");
        }
    }
}
