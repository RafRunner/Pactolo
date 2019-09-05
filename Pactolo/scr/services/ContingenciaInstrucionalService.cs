using Dapper;
using Pactolo.scr.dominio;
using Pactolo.scr.utils;
using System.Collections.Generic;
using System.Linq;

namespace Pactolo.scr.services {

    class ContingenciaInstrucionalService : AbstractService{

        public static ContingenciaInstrucional GetById(long id) {
            if (id <= 0) {
                return null;
            }
            ContingenciaInstrucional contingenciaInstrucional = AbstractService.GetById<ContingenciaInstrucional>(id, "ContingenciaInstrucional");
            ObterObjetosFilhos(contingenciaInstrucional);
            return contingenciaInstrucional;
        }

        private static void ObterObjetosFilhos(ContingenciaInstrucional contingenciaInstrucional) {
            contingenciaInstrucional.Tato1 = UnidadeDoExperimentoService.GetById(contingenciaInstrucional.Tato1Id);
            contingenciaInstrucional.Autoclitico = UnidadeDoExperimentoService.GetById(contingenciaInstrucional.AutocliticoId);
            contingenciaInstrucional.Tato2 = UnidadeDoExperimentoService.GetById(contingenciaInstrucional.Tato2Id);
        }

        public static List<ContingenciaInstrucional> GetAll(){
            List<ContingenciaInstrucional> contingenciasInstrucionais = AbstractService.GetAll<ContingenciaInstrucional>("ContingenciaInstrucional");
            for (int i = 0; i< contingenciasInstrucionais.Count; i++) {
                ObterObjetosFilhos(contingenciasInstrucionais[i]);
            }
            return contingenciasInstrucionais;
        }

        public static void Salvar(ContingenciaInstrucional contingenciaInstrucional) {
            AbstractService.Salvar(contingenciaInstrucional,
                "ContingenciaInstrucional",
                "INSERT INTO ContingenciaInstrucional (Nome, Tato1Id, AutocliticoId, Tato2Id) VALUES (@Nome, @Tato1Id, @AutocliticoId, @Tato2Id); SELECT CAST(last_insert_rowid() as int)",
                "UPDATE ContingenciaInstrucional SET Tato1Id = @Tato1Id, AutocliticoId = @AutocliticoId, Tato2Id = @Tato2Id WHERE Id = @Id");
        }

        public static void Deletar(ContingenciaInstrucional contingenciaInstrucional) {
            List<ContingenciaColateral> CCsComEssaCI = ContingenciaColateralService.GetAllByCI(contingenciaInstrucional);
            if (CCsComEssaCI.Count > 0) {
                throw new System.Exception($"Essa CI está cadastrada nas seguintes CCs: {StringUtils.Join(CCsComEssaCI, ", ")}. Delete primeiro essas CCs ou as associe a outra CI");
            }
            AbstractService.Deletar(contingenciaInstrucional, "ContingenciaInstrucional");
            UnidadeDoExperimentoService.Deletar(contingenciaInstrucional.Tato1);
            UnidadeDoExperimentoService.Deletar(contingenciaInstrucional.Tato2);
            UnidadeDoExperimentoService.Deletar(contingenciaInstrucional.Autoclitico);
        }
    }
}
