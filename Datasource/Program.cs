// <Snippet1>

using System.Data.SqlClient;
namespace ccrek.sandbox {

	class Program {
		static void Main(string[] args) {
			using SqlConnection conn = SingletonTestDb.GetInstance();

			Console.WriteLine("ServerVersion: {0}", conn.ServerVersion);
			Console.WriteLine("DataSource: {0}", conn.DataSource);
			Console.WriteLine("Database: {0}", conn.Database.ToString());
			Console.WriteLine("State: {0}", conn.State.ToString());


			Console.Read();
		}
	}
}