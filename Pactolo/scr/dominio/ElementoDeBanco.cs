using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.dominio {

	public abstract class ElementoDeBanco {

		// Id tem que ter set para, ao deletar um objeto, o métodod possa settar seu Id como zero para que se possa saber que aquele registro não existe mais no banco de dados
		// Além disso ao salvar uma nova pessoa no banco seu id passa de 0 para seu id no banco
		public long Id { get; set; }

		public long GetId(ElementoDeBanco value) {
			if (value == null) {
				return 0;
			}
			else {
				return value.Id;
			}
		}
	}
}
