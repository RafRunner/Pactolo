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
			if (id <= 0) {
				return null;
			}
			using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
				return cnn.Query<Experimentador>("SELECT * FROM Experimentador WHERE Id = @Id", new { Id = id })?.Single();
			}
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
				return cnn.Query<Experimentador>("SELECT * FROM Experimentador WHERE Nome = @Nome AND Email = @Email AND Projeto = @Projeto", experimentador).Single();
			}
		}

		public static Experimentador Salvar(Experimentador experimentador) {
			Experimentador experimentadorExistente = GetByPropriedades(experimentador);
			if (experimentadorExistente != null) {
				throw new Exception("Experimentador já existe na base de dados!");
			}

			experimentadorExistente = GetById(experimentador.Id);

			using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
				long id;

				if (experimentadorExistente == null) {
					id = cnn.Query<long>("INSERT INTO Experimentador (Nome, Email, Projeto) VALUES (@Nome, @Email, @Projeto); SELECT CAST(last_insert_rowid() as int)", experimentador).Single();
				}
				else {
					cnn.Execute("UPDATE Experimentador SET Nome = @Nome, Email = @Email, Projeto = @Projeto WHERE Id = @Id", experimentador);
					id = experimentador.Id;
					experimentador = GetById(id);
				}

				return GetById(id);
			}
		}

		public static void Deletar(Experimentador experimentador) {
			using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
				string command;
				if (experimentador.Id <= 0) {
					command = "DELETE FROM Experimentador WHERE Nome = @Nome AND Email = @Email AND Projeto = @Projeto";
				}
				else {
					command = "DELETE FROM Experimentador WHERE Id = @Id";
				}
				cnn.Execute(command, experimentador);
				experimentador.Id = 0;
			}
		}
	}
}
