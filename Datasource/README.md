# Datasource

1. References  
TestDataSource.csproj :   

```xml
 <ItemGroup>
    <PackageReference Include="System.Configuration.ConfigurationManager" Version="7.0.0" />
    <PackageReference Include="System.Data.SqlClient" Version="4.8.5" />
  </ItemGroup>
```

2. ConnectionString   
ConnectionString in App.config file :  
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<connectionStrings>
		<add name="TestDb" connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DataSourceDb;Integrated Security=True" providerName="System.Data.SqlClient" />
	</connectionStrings>
</configuration>
```

3. Singleton  
SingletonTestDb.cs :  
```csharp
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
					new SingletonTestDb();
				}
			}
			return _instance;
		}
	}
}
```

4. Program.cs :   
```csharp
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
```

5. Output  
>ServerVersion: 15.00.4153
DataSource: (localdb)\MSSQLLocalDB
Database: DataSourceDb
State: Open
