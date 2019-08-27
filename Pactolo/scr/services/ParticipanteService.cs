using Dapper;
using Pactolo.scr.dominio;
using Pactolo.scr.dominio.DTOs;
using Pactolo.scr.utils;
using System;
using System.Collections;
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

		public static List<object> GetAll() {
			return AbstractService.GetAll<Participante>("Participante").Cast<object>().ToList();
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

		private static Participante GetByPropriedades(Participante participante) {
            IEnumerable<Participante> pessoa = AbstractService.GetByObj<Participante>("SELECT * FROM Participante WHERE Nome = @Nome AND Email = @Email", participante);
			return pessoa.Count() > 0 ? pessoa.Single() : null;
		}

		public static void Salvar(Participante participante) {
			AbstractService.Salvar<Participante>(
				participante,
				"Participante",
				"INSERT INTO Participante (Nome, Email, Idade, Escolaridade, Sexo) VALUES (@Nome, @Email, @Idade, @Escolaridade, @Sexo); SELECT CAST(last_insert_rowid() as int)",
				"UPDATE Participante SET Nome = @Nome, Email = @Email, Idade = @Idade, Escolaridade = @Escolaridade, Sexo = @Sexo WHERE Id = @Id"
			);
		}

		public static void Deletar(Participante participante) {
			AbstractService.Deletar(participante, "Participante");
        }

        public static void DeletarPorId(long id) {
            AbstractService.DeletarPorId(id, "Participante");
        }

        public static List<object> FilterDataTable(DTOFiltro dtoFiltro) {
            List<Participante> experimentadores = dtoFiltro.Itens.Cast<Participante>().ToList();
            string textoDeBusca = dtoFiltro.TextoDeBusca;

            return experimentadores.FindAll(experimentador => {
                if (experimentador.Nome.Contains(textoDeBusca) ||
                experimentador.Email.Contains(textoDeBusca)) {
                    return true;
                }
                return false;
            }).Cast<object>().ToList();
        }
    }
}
