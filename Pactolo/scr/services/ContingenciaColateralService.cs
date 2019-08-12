using Pactolo.scr.dominio;
using System.Collections.Generic;

namespace Pactolo.scr.services {

    class ContingenciaColateralService : AbstractService {
        public static ContingenciaColateral GetById(long id) {
            if (id <= 0) {
                return null;
            }
            ContingenciaColateral contingenciaColateral = AbstractService.GetById<ContingenciaColateral>(id, "ContingenciaColateral");
            ObterObjetosFilhos(contingenciaColateral);
            return contingenciaColateral;
        }

        public static void ObterObjetosFilhos(ContingenciaColateral contingenciaColateral) {
            contingenciaColateral.sModelo = UnidadeDoExperimentoService.GetById(contingenciaColateral.sModeloId);
            contingenciaColateral.SC1 = UnidadeDoExperimentoService.GetById(contingenciaColateral.SC1Id);
            contingenciaColateral.SC2 = UnidadeDoExperimentoService.GetById(contingenciaColateral.SC2Id);
            contingenciaColateral.SC3 = UnidadeDoExperimentoService.GetById(contingenciaColateral.SC3Id);
            contingenciaColateral.CI = ContingenciaInstrucionalService.GetById(contingenciaColateral.CIId);
            return;
        }

        public static List<ContingenciaColateral> GetAll() {
            List<ContingenciaColateral> contingenciasColaterais = AbstractService.GetAll<ContingenciaColateral>("ContingenciaColateral");
            for (int i = 0; i < contingenciasColaterais.Count; i++) {
                ObterObjetosFilhos(contingenciasColaterais[i]);
            }
            return contingenciasColaterais;
        }

        public static void Salvar(ContingenciaColateral contingenciaColateral) {
            AbstractService.Salvar(contingenciaColateral,
                "ContingenciaColateral",
                "INSERT INTO ContingenciaColateral (Nome, sModeloId, SC1Id, SC2Id, SC3Id, CIId, ProbabilidadeComplementar) VALUES (@Nome, @sModelo, @SC1, @SC2, @SC3, @CI, @ProbabilidadeComplementar); SELECT CAST(last_insert_rowid() as int)",
                "UPDATE ContingenciaColateral SET Nome = @Nome, sModeloId = @sModelo, SC1Id = @SC1, SC2Id = @SC2, SC3Id = @SC3, CIId = @CI, ProbabilidadeComplementar = @ProbabilidadeComplementar WHERE Id = @Id");
        }

        public static void Deletar(ContingenciaColateral contingenciaColateral) {
            AbstractService.Deletar(contingenciaColateral, "ContingenciaColateral");
        }
    }
}
