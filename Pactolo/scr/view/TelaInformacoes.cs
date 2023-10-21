using System.Windows.Forms;

namespace Pactolo.scr.view {
	public partial class TelaInformacoes : Form {
		public TelaInformacoes() {
			InitializeComponent();

			tbInformacoes.Text = "O Pactolo é um software para estudo da relação entre contingências. A primeira contingência tem como consequência o acesso à tarefa de escolha de acordo com o modelo, a segunda contingência. Originalmente foi desenvolvido para estudo do efeito de estímulos verbais sobre o aprendizado de discriminações condicionais e formação de classes de equivalência, entretanto o software permite variações quanto aos parâmetros.\n" +
"O nome Pactolo vem do mito do Rei Midas, uma alegoria sobre a ganância.Baco, a pedido do próprio Midas, concedera ao rei o dom de transformar tudo que tocava em ouro. Entretanto, Midas percebeu que seu desejo era uma maldição, e acabou por transformar sua própria filha, Phoebe, em uma estátua de ouro.  Midas, em desespero, pede a Baco para se livrar de seu \"dom\". Baco disse que Midas deveria se banhar na fonte do rio Pactolo, para que pudesse se livrar de sua maldição.\n" +
"\nDelineamento: João Lucas Bernardy (Universidade de São Paulo)\n" +
"Automação: Rafael Nunes Santana (Universidade Federal de Goiás) e Emanuel Borges Passinato (Universidade Federal de Goiás)\n" +
"\nCódigo fonte disponível em: https://github.com/RafRunner/Pactolo\n" +
"\nComo citar este software:\n" +
"\nAPA\n" +
"Bernardy, J.L., Santana, R.N., & Passinato, E.B. (2019). Pactolo (Versão 2.2.1). [Software]. São Paulo, SP: Universidade de São Paulo.\n" +
"\nABNT\n" +
"BERNARDY, João Lucas; SANTANA, Rafael Nunes; PASSINATO, Emanuel Borges. Pactolo. Versão 2.2.2. São Paulo.\n" +
"\nContato: bernardy@usp.br";
			tbInformacoes.SelectionBullet = false;
		}
	}
}
