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
	class ParticipanteService : AbstractService {

		public static Participante GetById(long id) {
			return AbstractService.GetById<Participante>(id, "Participante");
		}

		public static List<Participante> GetAll() {
			return AbstractService.GetAll<Participante>("Participante");
		}

		public static List<Participante> GetByNome(string nome) {
			nome = StringUtils.Normalize(nome)?.ToLower();

			if (string.IsNullOrEmpty(nome)) {
				return new List<Participante>();
			}

			using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
				return cnn.Query<Participante>($"SELECT * FROM Participante WHERE lower(Nome) LIKE '%{nome}%'").ToList();
			}
		}

		public static Participante GetByPropriedades(Participante participante) {
			using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
				IEnumerable<Participante> pessoa = cnn.Query<Participante>("SELECT * FROM Participante WHERE Nome = @Nome AND Email = @Email AND Sexo = @Sexo", participante);
				return pessoa.Count() > 0 ? pessoa.Single() : null;
			}
		}

		public static void Salvar(Participante participante) {
			Participante participanteExistente = GetByPropriedades(participante);
			if (participante.Id == 0 && participanteExistente != null) {
				throw new Exception("Participante já existe na base de dados!");
			}

			participanteExistente = GetById(participante.Id);

			using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
				if (participanteExistente == null) {
					long id = cnn.Query<long>("INSERT INTO Participante (Nome, Email, Idade, Escolaridade, Sexo) VALUES (@Nome, @Email, @Idade, @Escolaridade, @Sexo); SELECT CAST(last_insert_rowid() as int)", participante).Single();
					participante.Id = id;
				}
				else {
					cnn.Execute("UPDATE Participante SET Nome = @Nome, Email = @Email, Idade = @Idade, Escolaridade = @Escolaridade, Sexo = @Sexo WHERE Id = @Id", participante);
				}
			}
		}

		public static void Deletar(Participante participante) {
			AbstractService.Deletar(participante, "Participante");
		}
	}
}
