using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pactolo.scr.services {

	abstract class AbstractService {

		protected static string GetConnectionString(string id = "Default") {
			return ConfigurationManager.ConnectionStrings[id].ConnectionString;
		}
	}
}
