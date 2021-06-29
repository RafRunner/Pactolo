namespace Pactolo.scr.view {
	partial class TelaInformacoes {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.tbInformacoes = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// tbInformacoes
			// 
			this.tbInformacoes.BackColor = System.Drawing.SystemColors.Control;
			this.tbInformacoes.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbInformacoes.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.tbInformacoes.Location = new System.Drawing.Point(13, 13);
			this.tbInformacoes.Name = "tbInformacoes";
			this.tbInformacoes.ReadOnly = true;
			this.tbInformacoes.Size = new System.Drawing.Size(781, 596);
			this.tbInformacoes.TabIndex = 0;
			this.tbInformacoes.Text = "";
			// 
			// TelaInformacoes
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(806, 621);
			this.Controls.Add(this.tbInformacoes);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(5);
			this.Name = "TelaInformacoes";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Informações sobre o software";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox tbInformacoes;
	}
}