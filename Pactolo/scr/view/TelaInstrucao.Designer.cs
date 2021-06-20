namespace Pactolo.src.view {
    partial class TelaInstrucao {
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
			this.labelInstrucao = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// labelInstrucao
			// 
			this.labelInstrucao.AutoSize = true;
			this.labelInstrucao.Font = new System.Drawing.Font("Microsoft YaHei UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelInstrucao.Location = new System.Drawing.Point(47, 210);
			this.labelInstrucao.Name = "labelInstrucao";
			this.labelInstrucao.Size = new System.Drawing.Size(241, 62);
			this.labelInstrucao.TabIndex = 0;
			this.labelInstrucao.Text = "Instrução";
			this.labelInstrucao.Click += new System.EventHandler(this.LabelInstrucao_Click);
			// 
			// TelaInstrucao
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.labelInstrucao);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "TelaInstrucao";
			this.Load += new System.EventHandler(this.TelaInstrucao_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInstrucao;
    }
}