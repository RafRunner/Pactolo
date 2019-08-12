using Pactolo.scr.dominio;
using System.Collections.Generic;

namespace Pactolo.scr.services {
    class SessaoService : AbstractService{

        public static Sessao GetById(long id) {
            if (id == 0) {
                return null;
            }
            Sessao sessao = AbstractService.GetById<Sessao>(id, "Sessao");
            ObterObjetosFilhos(sessao);
            return sessao;
        }
        // ATENCAOOOO!!! ESSE METODO NÃO ESTA PRONTO É NECESSARIO FAZER UMA RELAÇÃO ENTRE SESSAO E CC
        public static void ObterObjetosFilhos(Sessao sessao) {
            sessao.CCs = ContingenciaColateralService.GetAll();
            return;
        }

        protected static List<Sessao> GetAll() {
            List<Sessao> sessoes = AbstractService.GetAll<Sessao>("Sessao");
            for (int i = 0; i < sessoes.Count; i++) {
                ObterObjetosFilhos(sessoes[i]);
            }
            return sessoes;
        }

        // ATENCAOOOO!!! ESSE METODO NÃO ESTA PRONTO É NECESSARIO FAZER UMA RELAÇÃO ENTRE SESSAO E CC
        public static void Salvar(Sessao sessao) {
            AbstractService.Salvar(sessao,
                "ContingenciaColateral",
                "INSERT INTO ContingenciaColateral (Nome, OrdemExposicao, CriterioNumeroTentativas, NumeroTentativas, CriterioDuracaoSegundos, DuracaoSegundos, CriterioAcertosConcecutivos, AcertosConcecutivos) VALUES (@Nome, @OrdemExposicao, @CriterioNumeroTentativas, @NumeroTentativas, @CriterioDuracaoSegundos, @DuracaoSegundos, @CriterioAcertosConcecutivos, @AcertosConcecutivos); SELECT CAST(last_insert_rowid() as int)",
                "UPDATE ContingenciaColateral SET Nome = @Nome, OrdemExposicao = @OrdemExposicao, CriterioNumeroTentativas = @CriterioNumeroTentativas, NumeroTentativas = @NumeroTentativas, CriterioDuracaoSegundos = @CriterioDuracaoSegundos, DuracaoSegundos = @DuracaoSegundos, CriterioAcertosConcecutivos = @CriterioAcertosConcecutivos, AcertosConcecutivos = @AcertosConcecutivos WHERE Id = @Id");
        }

        public static void Deletar(Sessao sessao) {
            AbstractService.Deletar(sessao, "Sessao");
        }
    }
}
