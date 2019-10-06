using Dapper;
using Pactolo.scr.dominio;
using Pactolo.scr.utils;
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

		private static void SalvarObjetosFilhos(ContingenciaInstrucional contingenciaInstrucional) {
			for (int i = 0; i < contingenciaInstrucional.Tatos.Count; i++) {
				UnidadeDoExperimento tato = contingenciaInstrucional.Tatos[i];
				UnidadeDoExperimentoService.Salvar(tato);
				ContigenciaInstrucionalToTatoService.Salvar(contingenciaInstrucional, tato, i);
			}
		}

        private static void ObterObjetosFilhos(ContingenciaInstrucional contingenciaInstrucional) {
			contingenciaInstrucional.Tatos = UnidadeDoExperimentoService.GetAllByCI(contingenciaInstrucional);
        }

        public static List<ContingenciaInstrucional> GetAll(){
            List<ContingenciaInstrucional> contingenciasInstrucionais = AbstractService.GetAll<ContingenciaInstrucional>("ContingenciaInstrucional");
            for (int i = 0; i< contingenciasInstrucionais.Count; i++) {
                ObterObjetosFilhos(contingenciasInstrucionais[i]);
            }
            return contingenciasInstrucionais;
        }

		private static void DeletarObjetosFilhos(ContingenciaInstrucional contingenciaInstrucional) {
			ContigenciaInstrucionalToTatoService.DeletarPorCI(contingenciaInstrucional);
			UnidadeDoExperimentoService.DeletarAll(contingenciaInstrucional.Tatos);
		}

        public static void Salvar(ContingenciaInstrucional contingenciaInstrucional) {
			AbstractService.Salvar(contingenciaInstrucional,
				"ContingenciaInstrucional",
				"INSERT INTO ContingenciaInstrucional (Nome) VALUES (@Nome); SELECT CAST(last_insert_rowid() as int)",
				"");
			SalvarObjetosFilhos(contingenciaInstrucional);
		}

        public static void Deletar(ContingenciaInstrucional contingenciaInstrucional) {
            List<ContingenciaColateral> CCsComEssaCI = ContingenciaColateralService.GetAllByCI(contingenciaInstrucional);
            if (CCsComEssaCI.Count > 0) {
                throw new System.Exception($"Essa CI está cadastrada nas seguintes CCs: {ListUtils.Join(CCsComEssaCI.Select(it => it.Nome).Cast<string>().ToList(), ", ")}. Delete primeiro essas CCs ou as associe a outra CI");
            }
			DeletarObjetosFilhos(contingenciaInstrucional);
            AbstractService.Deletar(contingenciaInstrucional, "ContingenciaInstrucional");
        }
    }
}
