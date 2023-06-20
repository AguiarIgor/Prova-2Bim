namespace Aula10DB.Models;

class ItensPedido
{
    public int ItensPedidoId { get; set; }
    public int ItensPedidoCodPedido { get; set; }
    public int ItensPedidoCodProduto { get; set; }
    public int Quantidade { get; set; }

    public ItensPedido(int itenspedidoid, int itenspedidocodpedido, int itenspedidocodproduto, int quantidade)
    {
        ItensPedidoId = itenspedidoid;
        ItensPedidoCodPedido = itenspedidocodpedido;
        ItensPedidoCodProduto = itenspedidocodproduto;
        Quantidade = quantidade;
    }
}