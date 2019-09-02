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

    protected string nome;
		public string Nome {
			get => nome;
			set => nome = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "Nome");
		}

		public List<ContingenciaColateral> CCs { get; set; }
		public bool OrdemAleatoria { get; set; }

    public int CriterioNumeroTentativas { get; set; }
    public long CriterioDuracaoSegundos { get; set; }
    public int CriterioAcertosConcecutivos { get; set; }

    public int NumeroTentativas { get; set; }
		public long DuracaoSegundos { get; set; }
		public int AcertosConcecutivos { get; set; }
	}
}
