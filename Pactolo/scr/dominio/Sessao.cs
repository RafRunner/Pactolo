using System;
using Pactolo.scr.enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pactolo.scr.utils;

namespace Pactolo.scr.dominio {
	//Me parece pelo menu que o examinador pode cadastrar uma Contingencia Instritucional aqui tambem, 
	//mas não faz sentido. Validar!
	class Sessao : ElementoDeBanco {

		//Parece que ele coloca nome nas seções mas no layout da tela não teria como
		protected string nome;
		public string Nome {
			get => nome;
			set => nome = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "Nome");
		}

		IList<ContingenciaColateral> CCs { get; set; }

		private string ordemExposicao;
		public string OrdemExposicao {
			get => ordemExposicao;
			set => EOrdemExposicao.ParseAndValidate(value);
		}
		//Estudar se vale a pena criar objetos ao invez dessa declaração direta
		public Boolean CriterioNumeroTentativas { get; set; }
		public int NumeroTentativas { get; set; }
		public Boolean CriterioDuracaoSegundos { get; set; }
		public long DuracaoSegundos { get; set; }
		public Boolean CriterioAcertosConcecutivos { get; set; }
		public int AcertosConcecutivos { get; set; }
	}
}
