namespace Aula10DB.Models;

class Produto
{
    public int ProdutoId { get; set; }
    public string Descricao { get; set; }
    public decimal ValorUnitario { get; set; }

    public Produto(int produtoid, string descricao, decimal valorunitario)
    {
        ProdutoId = produtoid;
        Descricao = descricao;
        ValorUnitario = valorunitario;
    }
}