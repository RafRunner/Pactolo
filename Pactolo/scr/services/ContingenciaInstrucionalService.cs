using Dapper;
using Pactolo.scr.dominio;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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

        public static void ObterObjetosFilhos(ContingenciaInstrucional contingenciaInstrucional) {
            contingenciaInstrucional.Tato1 = UnidadeDoExperimentoService.GetById(contingenciaInstrucional.Tato1Id);
            contingenciaInstrucional.Autoclitico = UnidadeDoExperimentoService.GetById(contingenciaInstrucional.AutocliticoId);
            contingenciaInstrucional.Tato2 = UnidadeDoExperimentoService.GetById(contingenciaInstrucional.Tato2Id);
            return;
        }

        protected static List<ContingenciaInstrucional> GetAll(){
            List<ContingenciaInstrucional> contingenciasInstrucionais = AbstractService.GetAll<ContingenciaInstrucional>("ContingenciaInstrucional");
            for (int i = 0; i< contingenciasInstrucionais.Count; i++) {
                ObterObjetosFilhos(contingenciasInstrucionais[i]);
            }
            return contingenciasInstrucionais;
        }

        public static void Salvar(ContingenciaInstrucional contingenciaInstrucional) {
            AbstractService.Salvar(contingenciaInstrucional,
                "ContingenciaInstrucional",
                "INSERT INTO ContingenciaInstrucional (Tato1Id, AutocliticoId, Tato2Id) VALUES (@Tato1, @Autoclitico, @Tato2); SELECT CAST(last_insert_rowid() as int)",
                "UPDATE ContingenciaInstrucional SET Tato1Id = @Tato1, AutocliticoId = @Autoclitico, Tato2Id = @Tato2 WHERE Id = @Id");
        }

        public static void Deletar(ContingenciaInstrucional contingenciaInstrucional) {
            AbstractService.Deletar(contingenciaInstrucional, "ContingenciaInstrucional");
        }
    }
}
