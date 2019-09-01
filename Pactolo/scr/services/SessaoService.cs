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

        public static void ObterObjetosFilhos(Sessao sessao) {
            sessao.CCs = CCPorSessaoService.GetAllCCBySessaoId(sessao.Id);
        }

        public static List<Sessao> GetAll() {
            List<Sessao> sessoes = AbstractService.GetAll<Sessao>("Sessao");
            foreach (Sessao sessao in sessoes) {
                ObterObjetosFilhos(sessao);
            }
            return sessoes;
        }

        public static void Salvar(Sessao sessao) {
            AbstractService.Salvar(sessao,
                "Sessao",
                "INSERT INTO Sessao (Nome, OrdemAleatoria, CriterioNumeroTentativas, CriterioDuracaoSegundos, CriterioAcertosConcecutivos) VALUES (@Nome, @OrdemAleatoria, @CriterioNumeroTentativas, @CriterioDuracaoSegundos, @CriterioAcertosConcecutivos); SELECT CAST(last_insert_rowid() as int)",
                "UPDATE Sessao SET Nome = @Nome, OrdemAleatoria = @OrdemAleatoria, CriterioNumeroTentativas = @CriterioNumeroTentativas, CriterioDuracaoSegundos = @CriterioDuracaoSegundos, CriterioAcertosConcecutivos = @CriterioAcertosConcecutivos WHERE Id = @Id");
            CCPorSessaoService.SalvarAll(sessao.Id, sessao.CCs);
        }

        public static void Deletar(Sessao sessao) {
            CCPorSessaoService.DeletarAllBySessaoId(sessao.Id);
            AbstractService.Deletar(sessao, "Sessao");
        }
    }
}
