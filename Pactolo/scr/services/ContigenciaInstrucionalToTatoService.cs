using Dapper;
using Pactolo.scr.dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.services {
	class ContigenciaInstrucionalToTatoService : AbstractService {

		public static void Salvar(ContingenciaInstrucional contingenciaInstrucional, UnidadeDoExperimento tato, int i) {
			using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
				string sqlInsert = $"INSERT INTO ContigenciaInstrucionalToTato (IdCI, IdUnidadeExperimento, Ordem) VALUES ({contingenciaInstrucional.Id}, {tato.Id}, {i})";
				cnn.Execute(sqlInsert);
			}
		}

		public static void DeletarPorCI(ContingenciaInstrucional contingenciaInstrucional) {
			using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
				string sqlInsert = $"DELETE FROM ContigenciaInstrucionalToTato WHERE IdCI = {contingenciaInstrucional.Id}";
				cnn.Execute(sqlInsert);
			}
		}

		public static List<ContigenciaInstrucionalToTato> GetByCIAndTato(ContingenciaInstrucional contingenciaInstrucional, UnidadeDoExperimento tato) {
			return AbstractService.GetByObj<ContigenciaInstrucionalToTato>(
				"SELECT CiTT.* FROM UnidadeDoExperimento ue JOIN ContigenciaInstrucionalToTato CiTT ON ue.Id = CiTT.IdUnidadeExperimento WHERE CiTT.IdCI = @IdCI AND CiTT.IdUnidadeExperimento = @IdUE ORDER BY CiTT.Ordem",
				new { IdCi = contingenciaInstrucional.Id, IdUE = tato.Id });
		}
	}
}
