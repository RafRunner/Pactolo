using Dapper;
using Pactolo.scr.dominio;
using Pactolo.scr.dominio.DTOs;
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

		public static List<object> GetAll() {
			return AbstractService.GetAll<Experimentador>("Experimentador").Cast<object>().ToList();
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
            IEnumerable<Experimentador> pessoa = AbstractService.GetByObj<Experimentador>("SELECT * FROM Experimentador WHERE Nome = @Nome AND Email = @Email", experimentador);
			return pessoa.Count() > 0 ? pessoa.Single() : null;
		}

        public static void Salvar(Experimentador experimentador) {
            Experimentador experimentadorExistente = GetByPropriedades(experimentador);
            if (experimentador.Id == 0 && experimentadorExistente != null) {
                throw new Exception("Experimentador com mesmo nome e email já existe na base de dados!");
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

        public static void DeletarPorId(long id) {
            AbstractService.DeletarPorId(id, "Experimentador");
        }

        public static List<object> FilterDataTable(DTOFiltro dtoFiltro) {
            List<Experimentador> experimentadores = dtoFiltro.Itens.Cast<Experimentador>().ToList();
            string textoDeBusca = dtoFiltro.TextoDeBusca.ToLower();

            return experimentadores.FindAll(experimentador => {
                if (experimentador.Nome.ToLower().Contains(textoDeBusca) || 
                experimentador.Email.ToLower().Contains(textoDeBusca) ||
                experimentador.Projeto.ToLower().Contains(textoDeBusca)) {
                    return true;
                }
                return false;
            }).Cast<object>().ToList();
        }
    }
}
