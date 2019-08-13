using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {

	abstract class ElementoDeBanco {

		// Id tem que ter set para, ao deletar um objeto, o métodod possa settar seu Id como zero para que se possa saber que aquele registro não existe mais no banco de dados
		// Além disso ao salvar uma nova pessoa no banco seu id passa de 0 para seu id no banco
		public long Id { get; set; }

		public static T Set<T> (long id, T value) where T : ElementoDeBanco {
			if (value == null) {
				id = 0;
				}
			else {
				id = value.Id;
			}
			return value;
		}
	}
}
