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
    public partial class TelaInstrucao : Form {

        private readonly int height = Screen.PrimaryScreen.Bounds.Height;
        private readonly int width = Screen.PrimaryScreen.Bounds.Width;

        private readonly double heightRatio;
        private readonly double widthRatio;

        public TelaInstrucao(string textoInstrucao) {
            InitializeComponent();

            heightRatio = height / 1080.0;
            widthRatio = width / 1920.0;

            labelInstrucao.Text = textoInstrucao;
            if (textoInstrucao.Length > 800) {
                int multiplicador = Convert.ToInt32(Math.Floor(textoInstrucao.Length / 800.0));
                int tamanhoMultiplicado = Convert.ToInt32(labelInstrucao.Font.Size / (1.25 * multiplicador));
                labelInstrucao.Font = new Font(labelInstrucao.Font.Name, tamanhoMultiplicado);
            }
        }

        private void CorrigeTamanhoEPosicao(Control controle) {
            controle.Height = Convert.ToInt32(controle.Height * heightRatio);
            controle.Width = Convert.ToInt32(controle.Width * widthRatio);
            controle.Location = new Point {
                X = Convert.ToInt32(controle.Location.X * widthRatio),
                Y = Convert.ToInt32(controle.Location.Y * heightRatio)
            };
        }
        private void CorrigeFonte(Label label) {
            label.Font = new Font(label.Font.Name, Convert.ToInt32(label.Font.Size * heightRatio));
        }

        private void TelaInstrucao_Load(object sender, EventArgs e) {
            Location = new Point(0, 0);
            Size = new Size(width, height);

            labelInstrucao.MaximumSize = new Size(width - 50, 0);
            labelInstrucao.AutoSize = true;
            CorrigeTamanhoEPosicao(labelInstrucao);
            CorrigeFonte(labelInstrucao);
        }

        private void LabelInstrucao_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
