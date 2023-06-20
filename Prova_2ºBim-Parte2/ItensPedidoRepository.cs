using Aula10DB.Database;
using Aula10DB.Models;
using Microsoft.Data.Sqlite;
namespace Aula10DB.Repositories;
class ItensPedidoRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public ItensPedidoRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public List<ItensPedido> Listar()
    {
        var itenspedidos = new List<ItensPedido>();
        

        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM ItensPedido";

        var reader = command.ExecuteReader();

        while(reader.Read())
        {
            var itenspedidoid = reader.GetInt32(0);
            var itenspedidocodpedido = reader.GetInt32(1);
            var itenspedidocodproduto = reader.GetInt32(2);
            var quantidade = reader.GetInt32(3);
            var itenspedido = ReaderToItensPedido(reader);
            itenspedidos.Add(itenspedido);
        }

        connection.Close();
        
        return itenspedidos;
    }

    public ItensPedido Inserir(ItensPedido itenspedido)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO ItensPedido VALUES($itenspedidoid, $itenspedidocodpedido, $itenspedidocodproduto, $quantidade)";
        command.Parameters.AddWithValue("$itenspedidoid", itenspedido.ItensPedidoId);
        command.Parameters.AddWithValue("$itenspedidocodpedido", itenspedido.ItensPedidoCodPedido);
        command.Parameters.AddWithValue("$itenspedidocodproduto", itenspedido.ItensPedidoCodProduto);
        command.Parameters.AddWithValue("$quantidade", itenspedido.Quantidade);
        
        command.ExecuteNonQuery();
        connection.Close();

        return itenspedido;
    }
    public bool Apresentar(int itenspedidoid)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(coditempedido) FROM ItensPedido WHERE (coditempedido = $itenspedidoid)";
        command.Parameters.AddWithValue("$itenspedidoid", itenspedidoid);

        var reader = command.ExecuteReader();
        reader.Read();
        var result = reader.GetBoolean(0);

        return result;
    }

    public bool MostrarItensPedidoProduto(int produtoid)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(itempedidocodproduto) FROM ItensPedido WHERE (itempedidocodproduto = $produtoid)";
        command.Parameters.AddWithValue("$produtoid", produtoid);

        var reader = command.ExecuteReader();
        reader.Read();
        var result = false;
        
        if(reader.GetInt32(0) > 0){
            result = true;
        }
        else{
            result = false;
        }
        return result;
    }

    public ItensPedido GetById(int itenspedidoid)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM ItensPedido WHERE (coditempedido = $itenspedidoid)";
        command.Parameters.AddWithValue("$itenspedidoid", itenspedidoid);

        var reader = command.ExecuteReader();
        reader.Read();

        var itenspedido = ReaderToItensPedido(reader);

        connection.Close();

        return itenspedido;
    }

    public List<ItensPedido> GetByProdutoId(int produtoid)
    {
        var itenspedidos = new List<ItensPedido>();

        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM ItensPedido WHERE (itempedidocodproduto = $produtoid)";
        command.Parameters.AddWithValue("$produtoid", produtoid);

        var reader = command.ExecuteReader();

        while(reader.Read())
        {
            var itenspedidoid = reader.GetInt32(0);
            var itenspedidocodpedido = reader.GetInt32(1);
            var itenspedidocodproduto = reader.GetInt32(2);
            var quantidade = reader.GetInt32(3);
            var itenspedido = ReaderToItensPedido(reader);
            itenspedidos.Add(itenspedido);
        }

        connection.Close();
        
        return itenspedidos;
    }
private ItensPedido ReaderToItensPedido(SqliteDataReader reader)
    {
        var itenspedido = new ItensPedido(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));

        return itenspedido;
    }
}