using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Pactolo.scr.view {
	public partial class GridCrud : Form {

		private List<object> tabelaCompleta;

		private readonly string nomeRegistros;
		private readonly Func<List<object>> funcaoCarregaDados;
		private readonly List<string> ordemColunas;
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
			this.ordemColunas = ordemColunas;
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

			ReloadDataSource(tabelaCompleta);

			dataGrid.CellDoubleClick += new DataGridViewCellEventHandler((sender, e) => {
				funcaoSelecionar.Invoke(long.Parse(dataGrid.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
				Close();
			});

			ShowDialog();
		}

		private void ReloadDataSource(List<object> tabela) {
			dataGrid.DataSource = tabela;

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
		}

		private bool VerifiqueQuantidadeColunasSelecionadasEAvise() {
			if (dataGrid.SelectedRows.Count == 0) {
				MessageBox.Show($"Por favor, selecione pelo menos um(a) {nomeRegistros}!", "Atenção");
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
			ReloadDataSource(tabelaCompleta);
		}

		private void ButtonDeletar_Click(object sender, EventArgs e) {
			if (!VerifiqueQuantidadeColunasSelecionadasEAvise()) {
				return;
			}

			funcaoDeletar.Invoke(ViewUtils.GetIdColunaSelecionada(dataGrid));
			tabelaCompleta = funcaoCarregaDados();
			ReloadDataSource(tabelaCompleta);
			MessageBox.Show($"{nomeRegistros} deletado com sucesso!", "Sucesso");
		}

		private void ButtonSelecionar_Click(object sender, EventArgs e) {
			if (!VerifiqueQuantidadeColunasSelecionadasEAvise()) {
				return;
			}

			funcaoSelecionar.Invoke(ViewUtils.GetIdColunaSelecionada(dataGrid));
			Close();
		}

		private void TextBoxFiltro_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) {
				e.Handled = true;
				e.SuppressKeyPress = true;

				ReloadDataSource(funcaoFiltro.Invoke(tabelaCompleta, textBoxFiltro.Text));
			}
		}

		private void TextBoxFiltro_TextChanged(object sender, EventArgs e) {
			var textoDeBusca = textBoxFiltro.Text;

			if (string.IsNullOrWhiteSpace(textoDeBusca)) {
				ReloadDataSource(tabelaCompleta);
				return;
			}

			if (textoDeBusca.Length < 3) {
				return;
			}

			ReloadDataSource(funcaoFiltro.Invoke(tabelaCompleta, textoDeBusca));
		}
	}
}