namespace Pactolo.src.view {
	partial class TelaMensagem {
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
			this.lblMensagem = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblMensagem
			// 
			this.lblMensagem.AutoSize = true;
			this.lblMensagem.Font = new System.Drawing.Font("Microsoft YaHei", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMensagem.Location = new System.Drawing.Point(363, 180);
			this.lblMensagem.Name = "lblMensagem";
			this.lblMensagem.Size = new System.Drawing.Size(217, 48);
			this.lblMensagem.TabIndex = 0;
			this.lblMensagem.Text = "Mensagem";
			this.lblMensagem.Click += new System.EventHandler(this.lblMensagem_Click);
			// 
			// btnOk
			// 
			this.btnOk.Font = new System.Drawing.Font("Microsoft YaHei", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOk.Location = new System.Drawing.Point(886, 255);
			this.btnOk.Name = "btnOk";
			this.btnOk.Padding = new System.Windows.Forms.Padding(10);
			this.btnOk.Size = new System.Drawing.Size(150, 79);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "OK";
			this.btnOk.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// TelaMensagem
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(1920, 1061);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.lblMensagem);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "TelaMensagem";
			this.Text = "TelaMensagem";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblMensagem;
		private System.Windows.Forms.Button btnOk;
	}
}