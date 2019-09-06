using Pactolo.scr.dominio;
using Pactolo.scr.dominio.DTOs;
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
    public partial class GridCrud : Form {

        private readonly Func<List<object>> funcaoCarregaDados;
        private List<object> tabelaCompleta;

        private readonly Func<DTOFiltro, List<object>> funcaoFiltro;
        private readonly Action<long> funcaoEditar;
        private readonly Action<long> funcaoDeletar;
        private readonly Action<long> funcaoSelecionar;

        private static readonly string colunaOculta = "Id";

        public GridCrud(
            Func<List<object>> funcaoCarregaDados, 
            List<string> ordemColunas, 
            Func<DTOFiltro, List<object>> funcaoFiltro, 
            Action<long> funcaoEditar, 
            Action<long> funcaoDeletar,
            Action<long> funcaoSelecionar) {

            InitializeComponent();

            this.tabelaCompleta = funcaoCarregaDados();
            if (tabelaCompleta.Count == 0) {
                MessageBox.Show("Não existe nenhum registro do tipo no momento!", "Advertência");
                Close();
                return;
            }

            this.funcaoCarregaDados = funcaoCarregaDados;
            this.funcaoFiltro = funcaoFiltro;
            this.funcaoEditar = funcaoEditar;
            this.funcaoDeletar = funcaoDeletar;
            this.funcaoSelecionar = funcaoSelecionar;

            if (funcaoEditar == null) {
                buttonEditar.Visible = false;
            }

            dataGrid.DataSource = tabelaCompleta;
            dataGrid.Columns[colunaOculta].Visible = false;
            for (int i = 0; i < ordemColunas.Count; i++) {
                dataGrid.Columns[ordemColunas[i]].DisplayIndex = i;
                dataGrid.Columns[ordemColunas[i]].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            ShowDialog();
        }

        private bool VerifiqueQuantidadeColunasSelecionadasEAvise() {
            if (dataGrid.SelectedRows.Count == 0) {
                MessageBox.Show("Por favor, selecione pelo menos um elemento (selecione toda a linha clicanco na primeira coluna (a vazia)) para editar!", "Atenção");
                return false;
            }
            else {
                return true;
            }
        }

        private void ButtonEditar_Click(object sender, EventArgs e) {
            if (!VerifiqueQuantidadeColunasSelecionadasEAvise()) {
                return;
            }

            funcaoEditar.Invoke(GetIdColunaSelecionada());
            tabelaCompleta = funcaoCarregaDados();
            dataGrid.DataSource = tabelaCompleta;
        }

        private long GetIdColunaSelecionada() {
            return long.Parse(dataGrid.SelectedRows[0].Cells["Id"].Value.ToString());
        }

        private void ButtonDeletar_Click(object sender, EventArgs e) {
            if (!VerifiqueQuantidadeColunasSelecionadasEAvise()) {
                return;
            }

            funcaoDeletar.Invoke(GetIdColunaSelecionada());
            tabelaCompleta = funcaoCarregaDados();
            dataGrid.DataSource = tabelaCompleta;
            MessageBox.Show("Registro deletado com sucesso!", "Sucesso");
        }

        private void ButtonSelecionar_Click(object sender, EventArgs e) {
            if (!VerifiqueQuantidadeColunasSelecionadasEAvise()) {
                return;
            }
            funcaoSelecionar.Invoke(GetIdColunaSelecionada());
            Close();
        }

        private void TextBoxFiltro_TextChanged(object sender, EventArgs e) {
            string textoDeBusca = textBoxFiltro.Text;
            if (string.IsNullOrWhiteSpace(textoDeBusca)) {
                dataGrid.DataSource = tabelaCompleta;
                return;
            }

            if (textoDeBusca.Length < 3) {
                return;
            }

            dataGrid.DataSource = funcaoFiltro.Invoke(new DTOFiltro {
                Itens = tabelaCompleta,
                TextoDeBusca = textoDeBusca
            });
        }
    }
}
