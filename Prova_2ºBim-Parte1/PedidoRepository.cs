using Aula10DB.Database;
using Aula10DB.Models;
using Microsoft.Data.Sqlite;
namespace Aula10DB.Repositories;
class PedidoRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public PedidoRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public List<Pedido> Listar()
    {
        var pedidos = new List<Pedido>();
        

        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Pedidos";

        var reader = command.ExecuteReader();

        while(reader.Read())
        {
            var pedidoid = reader.GetInt32(0);
            var prazoentrega = reader.GetDateTime(1);
            var datapedido = reader.GetDateTime(2);
            var pedidoclienteid = reader.GetInt32(3);
            var pedidovendedorid = reader.GetInt32(4);
            var pedido = ReaderToPedido(reader);
            pedidos.Add(pedido);
        }

        connection.Close();
        
        return pedidos;
    }

    public Pedido Inserir(Pedido pedido)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Pedidos VALUES($pedidoid, $prazoentrega, $datapedido, $pedidocodvendedor, $pedidocodcliente)";
        command.Parameters.AddWithValue("$pedidoid", pedido.PedidoId);
        command.Parameters.AddWithValue("$prazoentrega", pedido.PrazoEntrega);
        command.Parameters.AddWithValue("$datapedido", pedido.DataPedido);
        command.Parameters.AddWithValue("$pedidocodvendedor", pedido.PedidoVendedorId);
        command.Parameters.AddWithValue("$pedidocodcliente", pedido.PedidoClienteId);

        command.ExecuteNonQuery();
        connection.Close();

        return pedido;
    }

    public bool Apresentar(int pedidoid)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(codpedido) FROM Pedidos WHERE (codpedido = $pedidoid)";
        command.Parameters.AddWithValue("$pedidoid", pedidoid);

        var reader = command.ExecuteReader();
        reader.Read();
        var result = reader.GetBoolean(0);

        return result;
    }

    public Pedido GetById(int pedidoid)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Pedidos WHERE (codpedido = $pedidoid)";
        command.Parameters.AddWithValue("$pedidoid", pedidoid);

        var reader = command.ExecuteReader();
        reader.Read();

        var pedido = ReaderToPedido(reader);

        connection.Close(); 

        return pedido;
    }

private Pedido ReaderToPedido(SqliteDataReader reader)
    {
        var pedido = new Pedido(reader.GetInt32(0), reader.GetDateTime(1), reader.GetInt32(3), reader.GetInt32(4));
        pedido.DataPedido = reader.GetDateTime(2);
        return pedido;
    }
}
