using Pactolo.scr.dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pactolo.scr.services {
    public partial class EditarExperimentador : Form {

        private readonly Experimentador experimentador;

        public EditarExperimentador(long id) {
            InitializeComponent();
            this.experimentador = ExperimentadorService.GetById(id);
            CarregarCampos();
        }

        public static void CarregarParaEdicao(long id) {
            new EditarExperimentador(id).ShowDialog();
        }

        private void CarregarCampos() {
            textBoxNomeExperimentador.Text = experimentador.Nome;
            textBoxEmailExperimentador.Text = experimentador.Email;
            textBoxProjetoExperimentador.Text = experimentador.Projeto;
        }

        private void ButtonSalvarExperimentador_Click(object sender, EventArgs e) {
            DialogResult result = MessageBox.Show("Tem certeza que deseja editar o experimentador?", "Confirmação necessária", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) {
                return;
            }

            experimentador.Nome = textBoxNomeExperimentador.Text;
            experimentador.Email = textBoxEmailExperimentador.Text;
            experimentador.Projeto = textBoxProjetoExperimentador.Text;
            ExperimentadorService.Salvar(experimentador);

            MessageBox.Show("Experimentador editado com sucesso!", "Sucesso");
            Close();
        }

        private void ButtonCancelar_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
