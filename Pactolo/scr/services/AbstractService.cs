using Dapper;
using Pactolo.scr.dominio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.services {

	abstract class AbstractService {

		protected static string GetConnectionString(string id = "Default") {
			return ConfigurationManager.ConnectionStrings[id].ConnectionString;
		}

		protected static T GetById<T>(long id, string nomeTabela) where T : ElementoDeBanco {
			if (id == 0) {
				return default(T);
			}
			using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
				IEnumerable<T> data = cnn.Query<T>($"SELECT * FROM {nomeTabela} WHERE Id = @Id", new { Id = id });
				return data.Count() > 0 ? data.Single() : default(T);
			}
		}

		protected static List<T> GetAll<T>(string nomeTabela) where T : ElementoDeBanco {
			using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
				return cnn.Query<T>($"SELECT * FROM {nomeTabela}").ToList<T>();
			}
		}

		protected static void Deletar(ElementoDeBanco objeto, string nomeTabela) {
			if (objeto == null) {
				return;
			}

			using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
				cnn.Execute($"DELETE FROM {nomeTabela} WHERE Id = @Id", objeto);
				objeto.Id = 0;
			}
		}
	}
}
