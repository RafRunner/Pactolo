using Pactolo.scr.dominio;
using Pactolo.scr.enums;
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

namespace Pactolo.scr.view {
    public partial class EditarParticipante : Form {

        private readonly Participante participante;

        public EditarParticipante(long id) {
            InitializeComponent();
            comboBoxSexo.Items.AddRange(ESexo.Values());
            comboBoxEscolaridade.Items.AddRange(EEscolaridade.Values());

            participante = ParticipanteService.GetById(id);
            CarregarCampos();
        }

        public static void CarregarParaEdicao(long id) {
            new EditarParticipante(id).ShowDialog();
        }

        private void CarregarCampos() {
            textBoxNomeParticipante.Text = participante.Nome;
            textBoxEmailParticipante.Text = participante.Email;
            numericIdade.Value = participante.Idade;
            comboBoxEscolaridade.SelectedItem = participante.Escolaridade;
            comboBoxSexo.SelectedItem = participante.Sexo;
        }

        private void ButtonSalvarParticipante_Click(object sender, EventArgs e) {
            DialogResult result = MessageBox.Show("Tem certeza que deseja editar o participante?", "Confirmação necessária", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) {
                return;
            }

            participante.Nome = textBoxNomeParticipante.Text;
            participante.Email = textBoxEmailParticipante.Text;
            participante.Idade = Convert.ToInt32(numericIdade.Value);
            participante.Escolaridade = comboBoxEscolaridade.SelectedItem.ToString();
            participante.Sexo = comboBoxSexo.SelectedItem.ToString();

            MessageBox.Show("Participante editado com sucesso!", "Sucesso");
            Close();
        }

        private void ButtonCancelar_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
