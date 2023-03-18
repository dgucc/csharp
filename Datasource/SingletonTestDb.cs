using System.Configuration;
using System.Data.SqlClient;

namespace ccrek.sandbox {

	public class SingletonTestDb {
		private static SqlConnection _instance = null;
		private string _connectionString = ConfigurationManager.ConnectionStrings["TestDb"].ToString();
		private static readonly object _syncObject = new object();

		private SingletonTestDb() {
			SqlConnection conn = new SqlConnection(_connectionString);
			conn.Open();
			_instance = conn;
		}

		public static SqlConnection GetInstance() {
			if (_instance == null) {
				lock (_syncObject) {
					if (_instance == null) {
						new SingletonTestDb();
					}
				}
			}
			return _instance;
		}
	}

}
