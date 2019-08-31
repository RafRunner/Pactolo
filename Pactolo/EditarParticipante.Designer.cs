using Pactolo.scr.enums;

namespace Pactolo {
    partial class EditarParticipante {
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBoxSexo = new System.Windows.Forms.ComboBox();
            this.numericIdade = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxEscolaridade = new System.Windows.Forms.ComboBox();
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.buttonSalvarParticipante = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxEmailParticipante = new System.Windows.Forms.TextBox();
            this.textBoxNomeParticipante = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericIdade)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.comboBoxSexo);
            this.panel2.Controls.Add(this.numericIdade);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.comboBoxEscolaridade);
            this.panel2.Controls.Add(this.buttonCancelar);
            this.panel2.Controls.Add(this.buttonSalvarParticipante);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.textBoxEmailParticipante);
            this.panel2.Controls.Add(this.textBoxNomeParticipante);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Location = new System.Drawing.Point(12, 13);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(6);
            this.panel2.Size = new System.Drawing.Size(547, 231);
            this.panel2.TabIndex = 10;
            // 
            // comboBoxSexo
            // 
            this.comboBoxSexo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSexo.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSexo.FormattingEnabled = true;
            this.comboBoxSexo.Location = new System.Drawing.Point(374, 132);
            this.comboBoxSexo.Name = "comboBoxSexo";
            this.comboBoxSexo.Size = new System.Drawing.Size(161, 29);
            this.comboBoxSexo.TabIndex = 16;
            // 
            // numericIdade
            // 
            this.numericIdade.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericIdade.Location = new System.Drawing.Point(13, 132);
            this.numericIdade.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numericIdade.Name = "numericIdade";
            this.numericIdade.Size = new System.Drawing.Size(88, 29);
            this.numericIdade.TabIndex = 15;
            this.numericIdade.UseWaitCursor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(371, 112);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 16);
            this.label10.TabIndex = 14;
            this.label10.Text = "Sexo:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(104, 113);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 16);
            this.label9.TabIndex = 13;
            this.label9.Text = "Escolaridade:";
            // 
            // comboBoxEscolaridade
            // 
            this.comboBoxEscolaridade.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxEscolaridade.FormattingEnabled = true;
            this.comboBoxEscolaridade.Location = new System.Drawing.Point(107, 132);
            this.comboBoxEscolaridade.Name = "comboBoxEscolaridade";
            this.comboBoxEscolaridade.Size = new System.Drawing.Size(261, 29);
            this.comboBoxEscolaridade.TabIndex = 12;
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancelar.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancelar.Location = new System.Drawing.Point(427, 182);
            this.buttonCancelar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(109, 36);
            this.buttonCancelar.TabIndex = 7;
            this.buttonCancelar.Text = "Cancelar";
            this.buttonCancelar.UseVisualStyleBackColor = true;
            this.buttonCancelar.Click += new System.EventHandler(this.ButtonCancelar_Click);
            // 
            // buttonSalvarParticipante
            // 
            this.buttonSalvarParticipante.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSalvarParticipante.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSalvarParticipante.Location = new System.Drawing.Point(13, 182);
            this.buttonSalvarParticipante.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonSalvarParticipante.Name = "buttonSalvarParticipante";
            this.buttonSalvarParticipante.Size = new System.Drawing.Size(87, 36);
            this.buttonSalvarParticipante.TabIndex = 6;
            this.buttonSalvarParticipante.Text = "Salvar";
            this.buttonSalvarParticipante.UseVisualStyleBackColor = true;
            this.buttonSalvarParticipante.Click += new System.EventHandler(this.ButtonSalvarParticipante_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Nome:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(14, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "Idade:";
            // 
            // textBoxEmailParticipante
            // 
            this.textBoxEmailParticipante.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEmailParticipante.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxEmailParticipante.HideSelection = false;
            this.textBoxEmailParticipante.Location = new System.Drawing.Point(13, 79);
            this.textBoxEmailParticipante.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxEmailParticipante.MaxLength = 100;
            this.textBoxEmailParticipante.Name = "textBoxEmailParticipante";
            this.textBoxEmailParticipante.Size = new System.Drawing.Size(522, 29);
            this.textBoxEmailParticipante.TabIndex = 5;
            // 
            // textBoxNomeParticipante
            // 
            this.textBoxNomeParticipante.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNomeParticipante.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNomeParticipante.HideSelection = false;
            this.textBoxNomeParticipante.Location = new System.Drawing.Point(13, 26);
            this.textBoxNomeParticipante.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxNomeParticipante.MaxLength = 100;
            this.textBoxNomeParticipante.Name = "textBoxNomeParticipante";
            this.textBoxNomeParticipante.Size = new System.Drawing.Size(522, 29);
            this.textBoxNomeParticipante.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(14, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 16);
            this.label7.TabIndex = 2;
            this.label7.Text = "Email:";
            // 
            // EditarParticipante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 254);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EditarParticipante";
            this.Text = "Editar Participante";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericIdade)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown numericIdade;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxEscolaridade;
        private System.Windows.Forms.Button buttonCancelar;
        private System.Windows.Forms.Button buttonSalvarParticipante;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxEmailParticipante;
        private System.Windows.Forms.TextBox textBoxNomeParticipante;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxSexo;
    }
}