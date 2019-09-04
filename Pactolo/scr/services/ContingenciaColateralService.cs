using Pactolo.scr.dominio;
using System.Collections.Generic;

namespace Pactolo.scr.services {

    class ContingenciaColateralService : AbstractService {

        public static ContingenciaColateral GetById(long id) {
            if (id == 0) {
                return null;
            }
            ContingenciaColateral contingenciaColateral = AbstractService.GetById<ContingenciaColateral>(id, "ContingenciaColateral");
            ObterObjetosFilhos(contingenciaColateral);
            return contingenciaColateral;
        }

        private static void ObterObjetosFilhos(ContingenciaColateral contingenciaColateral) {
            contingenciaColateral.sModelo = UnidadeDoExperimentoService.GetById(contingenciaColateral.sModeloId);
            contingenciaColateral.SC1 = UnidadeDoExperimentoService.GetById(contingenciaColateral.SC1Id);
            contingenciaColateral.SC2 = UnidadeDoExperimentoService.GetById(contingenciaColateral.SC2Id);
            contingenciaColateral.SC3 = UnidadeDoExperimentoService.GetById(contingenciaColateral.SC3Id);
            contingenciaColateral.CI = ContingenciaInstrucionalService.GetById(contingenciaColateral.CIId);
        }

        public static List<ContingenciaColateral> GetAll() {
            List<ContingenciaColateral> contingenciasColaterais = AbstractService.GetAll<ContingenciaColateral>("ContingenciaColateral");
            foreach (ContingenciaColateral CC in contingenciasColaterais) {
                ObterObjetosFilhos(CC);
            }
            return contingenciasColaterais;
        }

		// TODO ao salvar o objeto pai, deverá dar cascade nos objetos filhos e salvar eles também? Acho melhor sim
        public static void Salvar(ContingenciaColateral contingenciaColateral) { 
            AbstractService.Salvar(contingenciaColateral,
                "ContingenciaColateral",
                "INSERT INTO ContingenciaColateral (Nome, sModeloId, SC1Id, SC2Id, SC3Id, CIId) VALUES (@Nome, @sModeloId, @SC1Id, @SC2Id, @SC3Id, @CIId); SELECT CAST(last_insert_rowid() as int)",
                "UPDATE ContingenciaColateral SET Nome = @Nome, sModeloId = @sModeloId, SC1Id = @SC1Id, SC2Id = @SC2Id, SC3Id = @SC3Id, CIId = @CIId WHERE Id = @Id");
        }

        public static void Deletar(ContingenciaColateral contingenciaColateral) {
            CCPorSessaoService.DeletarAllByCCId(contingenciaColateral.Id);
            AbstractService.Deletar(contingenciaColateral, "ContingenciaColateral");
            UnidadeDoExperimentoService.Deletar(contingenciaColateral.sModelo);
            UnidadeDoExperimentoService.Deletar(contingenciaColateral.SC1);
            UnidadeDoExperimentoService.Deletar(contingenciaColateral.SC2);
            UnidadeDoExperimentoService.Deletar(contingenciaColateral.SC3);
        }
    }
}
