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

        private List<object> tabelaCompleta;

        private Func<DTOFiltro, List<object>> funcaoFiltro;
        private Action<long> funcaoEditar;
        private Action<long> funcaoDeletar;

        private static readonly string colunaOculta = "Id";

        public GridCrud(List<object> dados, List<string> ordemColunas, Func<DTOFiltro, List<object>> funcaoFiltro, Action<long> funcaoEditar, Action<long> funcaoDeletar) {
            InitializeComponent();

            this.tabelaCompleta = dados;
            this.funcaoFiltro = funcaoFiltro;
            this.funcaoEditar = funcaoEditar;
            this.funcaoDeletar = funcaoDeletar;

            dataGrid.DataSource = dados;
            dataGrid.Columns[colunaOculta].Visible = false;
            for (int i = 0; i < ordemColunas.Count; i++) {
                dataGrid.Columns[ordemColunas[i]].DisplayIndex = i;
            }
        }

        private void ButtonFiltrar_Click(object sender, EventArgs e) {
            dataGrid.DataSource = funcaoFiltro.Invoke(new DTOFiltro {
                Itens = tabelaCompleta,
                TextoDeBusca = textBoxFiltro.Text
            });
        }

        private void ButtonLimpar_Click(object sender, EventArgs e) {
            dataGrid.DataSource = tabelaCompleta;
        }

        private void ButtonEditar_Click(object sender, EventArgs e) {
            if (dataGrid.SelectedRows.Count == 0) {
                MessageBox.Show("Por favor, selecione pelo menos um elemento (selecione toda a linha clicanco na primeira coluna (a vazia)) para editar!", "Atenção");
                return;
            }

            funcaoEditar.Invoke(long.Parse(dataGrid.SelectedRows[0].Cells["Id"].Value.ToString()));
            Close();
        }

        private void ButtonDeletar_Click(object sender, EventArgs e) {
            if (dataGrid.SelectedRows.Count == 0) {
                MessageBox.Show("Por favor, selecione pelo menos um elemento (selecione toda a linha clicanco na primeira coluna (a vazia)) para deletar!", "Atenção");
                return;
            }

            funcaoDeletar.Invoke(long.Parse(dataGrid.SelectedRows[0].Cells["Id"].Value.ToString()));
            MessageBox.Show("Registro deletado com sucesso!", "Sucesso");
            if (dataGrid.Rows.Count == 1) {
                Close();
            }
            dataGrid.Rows.Remove(dataGrid.SelectedRows[0]);
        }
    }
}
