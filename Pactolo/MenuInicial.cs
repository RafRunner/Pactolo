using Pactolo.scr.dominio;
using Pactolo.scr.services;
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
    public partial class MenuInicial : Form {

        private Experimentador experimentadorSendoEditado;
        private Participante participanteSendoEditado;

        public MenuInicial() {
            InitializeComponent();
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

        private void ButtonSalvarExperimentador_Click(object sender, EventArgs e) {
            Experimentador experimentador;

            if (experimentadorSendoEditado != null) {
                experimentador = experimentadorSendoEditado;
                experimentadorSendoEditado = null;

                DialogResult result = MessageBox.Show("Tem certeza que deseja editar o experimentador?", "Confirmação necessária", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) {
                    LimparCamposExperimentador();
                    return;
                }
            }
            else {
                experimentador = CriaExperimentadorPelosCampos();
            }

            ExperimentadorService.Salvar(experimentador);
            MessageBox.Show("Experimentador salvo com sucesso!", "Sucesso");
        }

        private void EditarExperimentador(long id) {
            Experimentador experimentador = ExperimentadorService.GetById(id);
            textBoxNomeExperimentador.Text = experimentador.Nome;
            textBoxEmailExperimentador.Text = experimentador.Email;
            textBoxProjetoExperimentador.Text = experimentador.Projeto;

            experimentadorSendoEditado = experimentador;
        }

        private void ButtonGerenciarExperimentador_Click(object sender, EventArgs e) {
            List<object> experimentadores = ExperimentadorService.GetAll().Cast<object>().ToList();
            if (experimentadores.Count == 0) {
                MessageBox.Show("Não existem expeimentadores cadastrados no momento!", "Advertência");
                return;
            }

            GridCrud gridExperimentadores = new GridCrud(
                experimentadores,
                Experimentador.GetOrdemCulunasGrid(),
                ExperimentadorService.FilterDataTable,
                EditarExperimentador,
                ExperimentadorService.DeletarPorId);
            gridExperimentadores.ShowDialog();
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

        private void EditarParticipante(long id) {
            Participante participante = ParticipanteService.GetById(id);
            textBoxNomeParticipante.Text = participante.Nome;
            textBoxEmailParticipante.Text = participante.Email;
            numericIdade.Value = participante.Idade;
            comboBoxSexo.SelectedItem = participante.Sexo;
            comboBoxEscolaridade.SelectedItem = participante.Escolaridade;

            participanteSendoEditado = participante;
        }

        private void ButtonSalvarParticipante_Click(object sender, EventArgs e) {
            Participante participante;

            if (participanteSendoEditado != null) {
                participante = participanteSendoEditado;
                participanteSendoEditado = null;

                DialogResult result = MessageBox.Show("Tem certeza que deseja editar o participante?", "Confirmação necessária", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) {
                    LimparCamposParticipante();
                    return;
                }
            }
            else {
                participante = CriarParticipantePelosCampos();
            }

            ParticipanteService.Salvar(participante);
            MessageBox.Show("Particpante salvo com sucesso!", "Sucesso");
        }

        private void ButtonGerenciarParticipante_Click(object sender, EventArgs e) {
            List<object> participantes = ParticipanteService.GetAll().Cast<object>().ToList();
            if (participantes.Count == 0) {
                MessageBox.Show("Não existem participante cadastrados no momento!", "Advertência");
                return;
            }

            GridCrud gridParticipantes = new GridCrud(
                ParticipanteService.GetAll().Cast<object>().ToList(),
                Participante.GetOrdemColunasGrid(),
                ParticipanteService.FilterDataTable,
                EditarParticipante,
                ParticipanteService.DeletarPorId);
            gridParticipantes.ShowDialog();
        }
    }
}
