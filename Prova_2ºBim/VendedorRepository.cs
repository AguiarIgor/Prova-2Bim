using Aula10DB.Database;
using Aula10DB.Models;
using Microsoft.Data.Sqlite;
namespace Aula10DB.Repositories;
class VendedorRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public VendedorRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public List<Vendedor> Listar()
    {
        var vendedores = new List<Vendedor>();
        

        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Vendedores";

        var reader = command.ExecuteReader();

        while(reader.Read())
        {
            var vendedorid = reader.GetInt32(0);
            var nome = reader.GetString(1);
            var salariofixo = reader.GetDecimal(2);
            var faixacomissao = reader.GetString(3);
            var vendedor = ReaderToVendedor(reader);
            vendedores.Add(vendedor);
        }

        connection.Close();
        
        return vendedores;
    }

    public Vendedor Inserir(Vendedor vendedor)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Vendedores VALUES($vendedorid, $nome, $salariofixo, $faixacomissao)";
        command.Parameters.AddWithValue("$vendedorid", vendedor.VendedorId);
        command.Parameters.AddWithValue("$nome", vendedor.Nome);
        command.Parameters.AddWithValue("$salariofixo", vendedor.SalarioFixo);
        command.Parameters.AddWithValue("$faixacomissao", vendedor.FaixaComissao);
        
        command.ExecuteNonQuery();
        connection.Close();

        return vendedor;
    }
    public bool Apresentar(int vendedorid)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(codvendedor) FROM Vendedores WHERE (codvendedor = $vendedorid)";
        command.Parameters.AddWithValue("$vendedorid", vendedorid);

        var reader = command.ExecuteReader();
        reader.Read();
        var result = reader.GetBoolean(0);

        return result;
    }

    public Vendedor GetById(int vendedorid)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Vendedores WHERE (codvendedor = $vendedorid)";
        command.Parameters.AddWithValue("$vendedorid", vendedorid);

        var reader = command.ExecuteReader();
        reader.Read();

        var vendedor = ReaderToVendedor(reader);

        connection.Close();

        return vendedor;
    }
private Vendedor ReaderToVendedor(SqliteDataReader reader)
    {
        var vendedor = new Vendedor(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2), reader.GetString(3));

        return vendedor;
    }
}