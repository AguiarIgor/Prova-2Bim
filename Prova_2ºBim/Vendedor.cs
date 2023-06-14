namespace Aula10DB.Models;

class Vendedor
{
    public int VendedorId { get; set; }
    public string Nome { get; set; }
    public decimal SalarioFixo { get; set; }
    public string FaixaComissao { get; set; }

    public Vendedor(int vendedorid, string nome, decimal salariofixo, string faixacomissao)
    {
        VendedorId = vendedorid;
        Nome = nome;
        SalarioFixo = salariofixo;
        FaixaComissao = faixacomissao;
    }
}