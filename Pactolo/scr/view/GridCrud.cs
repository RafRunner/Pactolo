using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Pactolo.scr.view {
	public partial class GridCrud : Form {

		private List<object> tabelaCompleta;

		private readonly string nomeRegistros;
		private readonly Func<List<object>> funcaoCarregaDados;
		private readonly Func<List<object>, string, List<object>> funcaoFiltro;
		private readonly Action<long> funcaoEditar;
		private readonly Action<long> funcaoDeletar;
		private readonly Action<long> funcaoSelecionar;

		public GridCrud(
			string nomeRegistros,
			Func<List<object>> funcaoCarregaDados,
			List<string> ordemColunas,
			Func<List<object>, string, List<object>> funcaoFiltro,
			Action<long> funcaoEditar,
			Action<long> funcaoDeletar,
			Action<long> funcaoSelecionar) {

			InitializeComponent();

			this.nomeRegistros = nomeRegistros;
			this.tabelaCompleta = funcaoCarregaDados();

			if (tabelaCompleta.Count == 0) {
				MessageBox.Show($"Não existe nenhum registro de {nomeRegistros} no momento!", "Advertência");
				Close();
				return;
			}

			this.funcaoCarregaDados = funcaoCarregaDados;
			this.funcaoFiltro = funcaoFiltro;
			this.funcaoEditar = funcaoEditar;
			this.funcaoDeletar = funcaoDeletar;
			this.funcaoSelecionar = funcaoSelecionar;

			Text = "Gerenciador de " + nomeRegistros;

			if (funcaoEditar == null) {
				buttonEditar.Visible = false;
			}

			dataGrid.DataSource = tabelaCompleta;

			var tamanhoHeaders = Math.Max(200, (dataGrid.Width - 40) / ordemColunas.Count);

			var colunaEnum = dataGrid.Columns.GetEnumerator();
			while (colunaEnum.MoveNext()) {
				var coluna = (DataGridViewColumn)colunaEnum.Current;
				var indexColuna = ordemColunas.IndexOf(coluna.HeaderText);

				if (indexColuna == -1) {
					coluna.Visible = false;
				}
				else {
					coluna.DisplayIndex = indexColuna;
					coluna.Width = tamanhoHeaders;
				}
			}

			ShowDialog();
		}

		private bool VerifiqueQuantidadeColunasSelecionadasEAvise() {
			if (dataGrid.SelectedRows.Count == 0) {
				MessageBox.Show($"Por favor, selecione pelo menos um(a) {nomeRegistros}. Selecione toda a linha clicanco na primeira coluna (a vazia)!", "Atenção");
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

			funcaoEditar.Invoke(ViewUtils.GetIdColunaSelecionada(dataGrid));
			tabelaCompleta = funcaoCarregaDados();
			dataGrid.DataSource = tabelaCompleta;
		}

		private void ButtonDeletar_Click(object sender, EventArgs e) {
			if (!VerifiqueQuantidadeColunasSelecionadasEAvise()) {
				return;
			}

			funcaoDeletar.Invoke(ViewUtils.GetIdColunaSelecionada(dataGrid));
			tabelaCompleta = funcaoCarregaDados();
			dataGrid.DataSource = tabelaCompleta;
			MessageBox.Show($"{nomeRegistros} deletado com sucesso!", "Sucesso");
		}

		private void ButtonSelecionar_Click(object sender, EventArgs e) {
			if (!VerifiqueQuantidadeColunasSelecionadasEAvise()) {
				return;
			}

			funcaoSelecionar.Invoke(ViewUtils.GetIdColunaSelecionada(dataGrid));
			Close();
		}

		private void textBoxFiltro_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) {
				e.Handled = true;
				e.SuppressKeyPress = true;

				var textoDeBusca = textBoxFiltro.Text;
				if (string.IsNullOrWhiteSpace(textoDeBusca)) {
					dataGrid.DataSource = tabelaCompleta;
					return;
				}

				dataGrid.DataSource = funcaoFiltro.Invoke(tabelaCompleta, textoDeBusca);
			}
		}

		private void TextBoxFiltro_TextChanged(object sender, EventArgs e) {
			var textoDeBusca = textBoxFiltro.Text;

			if (string.IsNullOrWhiteSpace(textoDeBusca)) {
				dataGrid.DataSource = tabelaCompleta;
				return;
			}

			if (textoDeBusca.Length < 3) {
				return;
			}

			dataGrid.DataSource = funcaoFiltro.Invoke(tabelaCompleta, textoDeBusca);
		}
	}
}