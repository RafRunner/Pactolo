using Pactolo.scr.utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pactolo.scr.view {
	public partial class TelaMensagem : Form {

		private readonly int height = Screen.PrimaryScreen.Bounds.Height;
		private readonly int width = Screen.PrimaryScreen.Bounds.Width;

		private readonly bool continuarAoClickarMensagem;

		public TelaMensagem(string mensagem, bool mostrarBotao = false, bool continuarAoClickarMensagem = false) {
			InitializeComponent();

			this.continuarAoClickarMensagem = continuarAoClickarMensagem;

			Location = new Point(0, 0);
			Size = new Size(width, height);

			ViewUtils.CorrigeTamanhoPosicaoFonte(lblMensagem);

			lblMensagem.MaximumSize = new Size((int)(width * 0.8), 0);
			lblMensagem.AutoSize = true;
			lblMensagem.Text = mensagem;
			lblMensagem.Location = new Point((width - lblMensagem.Width) / 2, lblMensagem.Location.Y);

			ViewUtils.Justify(lblMensagem);

			if (mostrarBotao) {
				ViewUtils.CorrigeTamanhoPosicaoFonte(btnOk);
				btnOk.Location = new Point {
					X = btnOk.Location.X,
					Y = lblMensagem.Location.Y + lblMensagem.Height + 20
				};
			}
			else {
				btnOk.Visible = false;
			}
		}

		private void btnOk_Click(object sender, EventArgs e) {
			Close();
		}

		private void lblMensagem_Click(object sender, EventArgs e) {
			if (continuarAoClickarMensagem) {
				Close();
			}
		}
	}
}
