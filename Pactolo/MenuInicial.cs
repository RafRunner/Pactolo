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

        private OpenFileDialog fileDialog = new OpenFileDialog();
        private readonly string imageFilter = "JPEG|*.jpg;*.jpeg";
        private readonly string audioFilter = "MP3|*.mp3";

        public MenuInicial() {
            InitializeComponent();

            List<ContingenciaInstrucional> CIs = ContingenciaInstrucionalService.GetAll();
            List<ContingenciaColateral> CCs = ContingenciaColateralService.GetAll();

            comboBoxSexo.Items.AddRange(ESexo.Values());
            comboBoxEscolaridade.Items.AddRange(EEscolaridade.Values());
            comboBoxCI.Items.AddRange(CIs.Cast<object>().ToArray());
            comboBoxCI.DisplayMember = "Nome";

            listViewCI.Items.AddRange(CIs.Select(it => new ListViewItem(it.Nome, it.Id.ToString())).Cast<ListViewItem>().ToArray());
            listViewCC.Items.AddRange(CCs.Select(it => new ListViewItem(it.Nome, it.Id.ToString())).Cast<ListViewItem>().ToArray());
        }

        private void LimparCamposExperimentador() {
            textBoxNomeExperimentador.Text = "";
            textBoxEmailExperimentador.Text = "";
            textBoxProjetoExperimentador.Text = "";
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

        private void LimparCamposParticipante() {
            textBoxNomeParticipante.Text = "";
            textBoxEmailParticipante.Text = "";
            numericIdade.Value = 0;
            comboBoxSexo.SelectedIndex = -1;
            comboBoxEscolaridade.SelectedIndex = -1;
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

            ImagemService.CopiaImagemParaPasta(caminhoImagemTato1);
            ImagemService.CopiaImagemParaPasta(caminhoImagemAuto);
            ImagemService.CopiaImagemParaPasta(caminhoImagemTato2);

            UnidadeDoExperimento tato1 = new UnidadeDoExperimento { CaminhoImagem = caminhoImagemTato1 };
            UnidadeDoExperimento auto = new UnidadeDoExperimento { CaminhoImagem = caminhoImagemAuto };
            UnidadeDoExperimento tato2 = new UnidadeDoExperimento { CaminhoImagem = caminhoImagemTato2 };

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
            listViewCI.Items.Add(new ListViewItem(CI.Nome, CI.Id.ToString()));
        }

        private void ButtonSelecionrCI_Click(object sender, EventArgs e) {

        }

        private void ButtonApagarCI_Click(object sender, EventArgs e) {

        }

        private ContingenciaColateral CriaCCPelosCampos() {
            ContingenciaInstrucional CI = comboBoxCI.SelectedItem as ContingenciaInstrucional;

            UnidadeDoExperimento sModelo = new UnidadeDoExperimento {
                CaminhoImagem = textBoxModelo.Text
            };

            UnidadeDoExperimento SC1 = new UnidadeDoExperimento {
                Feedback = new Feedback {
                    ValorClick = Convert.ToInt32(numericSC1.Value),
                    Neutro = checkBoxNeutroSC1.Checked,
                    SemCor = checkBoxSemCorSC1.Checked,
                    ProbabilidadeComplementar = Convert.ToInt32(numericProbabilidadeSC1.Value)
                },
                CaminhoImagem = textBoxSC1.Text,
                CaminhoAudio = textBoxAudioSC1.Text
            };

            UnidadeDoExperimento SC2 = new UnidadeDoExperimento {
                Feedback = new Feedback {
                    ValorClick = Convert.ToInt32(numericSC2.Value),
                    Neutro = checkBoxNeutroSC2.Checked,
                    SemCor = checkBoxSemCorSC2.Checked,
                    ProbabilidadeComplementar = Convert.ToInt32(numericProbabilidadeSC2.Value)
                },
                CaminhoImagem = textBoxSC2.Text,
                CaminhoAudio = textBoxAudioSC2.Text
            };

            UnidadeDoExperimento SC3 = new UnidadeDoExperimento {
                Feedback = new Feedback {
                    ValorClick = Convert.ToInt32(numericSC3.Value),
                    Neutro = checkBoxNeutroSC3.Checked,
                    SemCor = checkBoxSemCorSC3.Checked,
                    ProbabilidadeComplementar = Convert.ToInt32(numericProbabilidadeSC3.Value)
                },
                CaminhoImagem = textBoxSC3.Text,
                CaminhoAudio = textBoxAudioSC3.Text
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
            listViewCC.Items.Add(new ListViewItem(CC.Nome, CC.Id.ToString()));
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
    }
}
