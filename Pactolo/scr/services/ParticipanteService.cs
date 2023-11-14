using Dapper;
using Pactolo.scr.dominio;
using Pactolo.scr.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Pactolo.scr.services {

	class ParticipanteService : AbstractService {

		public static Participante GetById(long id) {
			return GetById<Participante>(id, "Participante");
		}

		public static List<object> GetAll() {
			return GetAll<Participante>("Participante").Cast<object>().ToList();
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
            IEnumerable<Participante> pessoa = GetByObj<Participante>("SELECT * FROM Participante WHERE Nome = @Nome AND Email = @Email", participante);
			return pessoa.Count() > 0 ? pessoa.Single() : null;
		}

		public static void Salvar(Participante participante) {
            Participante participanteExistente = GetByPropriedades(participante);
            if (participante.Id == 0 && participanteExistente != null) {
                throw new Exception("Participante já existe na base de dados!");
            }

            Salvar(
				participante,
				"Participante",
				"INSERT INTO Participante (Nome, Email, Idade, Escolaridade, Sexo) VALUES (@Nome, @Email, @Idade, @Escolaridade, @Sexo); SELECT CAST(last_insert_rowid() as int)",
				"UPDATE Participante SET Nome = @Nome, Email = @Email, Idade = @Idade, Escolaridade = @Escolaridade, Sexo = @Sexo WHERE Id = @Id"
			);
		}

		public static void Deletar(Participante participante) {
			Deletar(participante, "Participante");
        }

        public static void DeletarPorId(long id) {
            DeletarPorId(id, "Participante");
        }

        public static List<object> FilterDataTable(List<object> objetos, string textoDeBusca) {
            List<Participante> experimentadores = objetos.Cast<Participante>().ToList();

            return experimentadores.FindAll(experimentador => {
                if (experimentador.Nome.ToLower().Contains(textoDeBusca) || experimentador.Email.ToLower().Contains(textoDeBusca)) {
                    return true;
                }
                return false;
            }).Cast<object>().ToList();
        }
    }
}
