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

var cust15 = GetByPK(15);

conn.Close();

Customer? GetByPK(int id) {

    //var sql = $"SELECT * from Customers Where id = {id};";
    var sql = "SELECT * from Customers Where id = @Id;";
    var cmd = new SqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@Id", id);
    var reader = cmd.ExecuteReader();
    if(!reader.HasRows) {
        return null;
    }
    reader.Read();
    var cust = new Customer();
    cust.Id = Convert.ToInt32(reader["Id"]);
    cust.Name = Convert.ToString(reader["Name"]);
    cust.City = Convert.ToString(reader["City"]);
    cust.State = Convert.ToString(reader["State"]);
    cust.Sales = Convert.ToDecimal(reader["Sales"]);
    cust.Active = Convert.ToBoolean(reader["Active"]);

    reader.Close();
    return cust;

}