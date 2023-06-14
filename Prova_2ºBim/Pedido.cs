namespace Aula10DB.Models;

class Pedido
{
    public int PedidoId { get; set; }
    public DateTime PrazoEntrega { get; set; }
    public DateTime DataPedido { get; set; }
    public int PedidoClienteId { get; set; }
    public int PedidoVendedorId { get; set; }

    public Pedido(int pedidoid, DateTime prazoentrega, int pedidoclienteid, int pedidovendedorid)
    {
        PedidoId = pedidoid;
        PrazoEntrega = prazoentrega;
        DataPedido = DateTime.Now;
        PedidoClienteId = pedidoclienteid;
        PedidoVendedorId = pedidovendedorid;
    }
}