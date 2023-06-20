namespace Aula10DB.Models;

class Cliente
{
    public int ClienteId { get; set; }
    public string Nome { get; set; }
    public string Endereco { get; set; }
    public string Cidade { get; set; }
    public string Cep { get; set; }
    public string UF { get; set; }
    public string IE { get; set; }

    public Cliente(int clienteid, string nome, string endereco, string cidade, string cep, string uf, string ie)
    {
        ClienteId = clienteid;
        Nome = nome;
        Endereco = endereco;
        Cidade = cidade;
        Cep = cep;
        UF = uf;
        IE = ie;
    }
}