using Dapper;
using Pactolo.scr.dominio;
using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.services {

	class ExperimentadorService : AbstractService {

		public static Experimentador GetById(long id) {
			return AbstractService.GetById<Experimentador>(id, "Experimentador");
		}

		public static List<Experimentador> GetAll() {
			return AbstractService.GetAll<Experimentador>("Experimentador");
		}

		public static List<Experimentador> GetByNome(string nome) {
			nome = StringUtils.Normalize(nome)?.ToLower();

			if (string.IsNullOrEmpty(nome)) {
				return new List<Experimentador>();
			}

			using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
				return cnn.Query<Experimentador>($"SELECT * FROM Experimentador WHERE lower(Nome) LIKE '%{nome}%'").ToList();
			}
		}

		public static Experimentador GetByPropriedades(Experimentador experimentador) {
			using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
				IEnumerable<Experimentador> pessoa = cnn.Query<Experimentador>("SELECT * FROM Experimentador WHERE Nome = @Nome AND Email = @Email AND Projeto = @Projeto", experimentador);
				return pessoa.Count() > 0 ? pessoa.Single() : null;
			}
		}

		public static void Salvar(Experimentador experimentador) {
			Experimentador experimentadorExistente = GetByPropriedades(experimentador);
			if (experimentador.Id == 0 && experimentadorExistente != null) {
				throw new Exception("Experimentador já existe na base de dados!");
			}

			AbstractService.Salvar<Experimentador>(
				experimentador,
				"Experimentador",
				"INSERT INTO Experimentador(Nome, Email, Projeto) VALUES(@Nome, @Email, @Projeto); SELECT CAST(last_insert_rowid() as int)",
				"UPDATE Experimentador SET Nome = @Nome, Email = @Email, Projeto = @Projeto WHERE Id = @Id"
			);
		}

		public static void Deletar(Experimentador experimentador) {
			AbstractService.Deletar(experimentador, "Experimentador");
		}
	}
}
