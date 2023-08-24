// See https://aka.ms/new-console-template for more information
using Microsoft.Data.SqlClient;

using SqlExample;

var connStr = "server=localhost\\sqlexpress;" +
                "database=SalesDb;" +
                "trusted_connection=true;" +
                "trustServerCertificate=true;";

var conn = new SqlConnection(connStr);

conn.Open();

if(conn.State != System.Data.ConnectionState.Open) {
    throw new Exception("Connection didn't open");
}

Console.WriteLine("Success!");

// put our sql code here

var sql = "SELECT * from Customers Order by Name;";
var cmd = new SqlCommand(sql, conn);
var reader = cmd.ExecuteReader();
var customers = new List<Customer>();

while(reader.Read()) {
    var cust = new Customer();
    cust.Id = Convert.ToInt32(reader["Id"]);
    cust.Name = Convert.ToString(reader["Name"]);
    cust.City = Convert.ToString(reader["City"]);
    cust.State = Convert.ToString(reader["State"]);
    cust.Sales = Convert.ToDecimal(reader["Sales"]);
    cust.Active = Convert.ToBoolean(reader["Active"]);
    customers.Add(cust);
}

reader.Close();
conn.Close();

var x = 0;