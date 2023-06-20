using Aula10DB.Database;
using Aula10DB.Models;
using Microsoft.Data.Sqlite;
namespace Aula10DB.Repositories;
class ProdutoRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public ProdutoRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public List<Produto> Listar()
    {
        var produtos = new List<Produto>();
        

        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Produtos";

        var reader = command.ExecuteReader();

        while(reader.Read())
        {
            var produtoid = reader.GetInt32(0);
            var descricao = reader.GetString(1);
            var valorunitario = reader.GetDecimal(2);
            var produto = ReaderToProduto(reader);
            produtos.Add(produto);
        }

        connection.Close();
        
        return produtos;
    }

    public Produto Inserir(Produto produto)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Produtos VALUES($produtoid, $descricao, $valorunitario)";
        command.Parameters.AddWithValue("$produtoid", produto.ProdutoId);
        command.Parameters.AddWithValue("$descricao", produto.Descricao);
        command.Parameters.AddWithValue("$valorunitario", produto.ValorUnitario);

        command.ExecuteNonQuery();
        connection.Close();

        return produto;
    }

    public bool Apresentar(int produtoid)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(codproduto) FROM Produtos WHERE (codproduto = $produtoid)";
        command.Parameters.AddWithValue("$produtoid", produtoid);

        var reader = command.ExecuteReader();
        reader.Read();
        var result = reader.GetBoolean(0);

        return result;
    }

    public Produto GetById(int produtoid)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Produtos WHERE (codproduto = $produtoid)";
        command.Parameters.AddWithValue("$produtoid", produtoid);

        var reader = command.ExecuteReader();
        reader.Read();

        var produto = ReaderToProduto(reader);

        connection.Close(); 

        return produto;
    }

private Produto ReaderToProduto(SqliteDataReader reader)
    {
        var produto = new Produto(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2));
        return produto;
    }
}