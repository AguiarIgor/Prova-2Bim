using Microsoft.Data.Sqlite;
using Aula10DB.Database;
using Aula10DB.Repositories;
using Aula10DB.Models;
var databaseConfig = new DatabaseConfig();
var databaseSetup = new DatabaseSetup(databaseConfig);
var clienteRepository = new ClienteRepository(databaseConfig);
var pedidoRepository = new PedidoRepository(databaseConfig);
var itensPedidoRepository = new ItensPedidoRepository(databaseConfig);
var produtoRepository = new ProdutoRepository(databaseConfig);
var vendedorRepository = new VendedorRepository(databaseConfig);

var modelName = args[0];
var modelAction = args[1];
/////////////////////////////////////////
if(modelName == "Cliente")
{
    if(modelAction == "Listar")
    {
        Console.WriteLine("\nListar Cliente");
        Console.WriteLine("Código Cliente   Nome Cliente         Endereço                     Cidade     CEP         UF   IE");
        foreach (var cliente in clienteRepository.Listar())
        {
            Console.WriteLine($"{cliente.ClienteId, -16} {cliente.Nome, -20} {cliente.Endereco, -28} {cliente.Cidade, -11} {cliente.Cep, -11} {cliente.UF, -4} {cliente.IE}");
        }
    }

    else if(modelAction == "Inserir")
    {
        Console.WriteLine("\nInserir Cliente");
        Console.Write("Digite o código do cliente: ");
        int clienteid = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o Nome do cliente: ");
        string nome = Console.ReadLine();
        Console.Write("Digite o Endereço do cliente: ");
        string endereco = Console.ReadLine();
        Console.Write("Digite a Cidade do cliente: ");
        string cidade = Console.ReadLine();
        Console.Write("Digite o CEP do cliente: ");
        string cep = Console.ReadLine();
        Console.Write("Digite a UF do cliente: ");
        string uf = Console.ReadLine();
        Console.Write("Digite a Inscrição Estadual: ");
        string ie = Console.ReadLine();
        var cliente = new Cliente(clienteid, nome, endereco, cidade, cep, uf, ie);
        clienteRepository.Inserir(cliente);
    }

    else if(modelAction == "Apresentar")
    {
        Console.WriteLine("\nApresentar Cliente");
        Console.Write("Digite o código do cliente: ");
        int clienteid = Convert.ToInt32(Console.ReadLine());

        if(clienteRepository.Apresentar(clienteid))
        {
            var cliente = clienteRepository.GetById(clienteid);
            Console.WriteLine($"{cliente.ClienteId}, {cliente.Nome}, {cliente.Endereco}, {cliente.Cidade}, {cliente.Cep}, {cliente.UF}, {cliente.IE}");
        } 
        else 
        {
            Console.WriteLine($"O cliente com Id {clienteid} não existe.");
        }
    }
}
/////////////////////////////////////////
else if(modelName == "Pedido")
{
    if(modelAction == "Listar")
    {
        Console.WriteLine("\nListar Pedido");
        Console.WriteLine("Código Pedido   Prazo Entrega         Data Pedido           Código Cliente   Código Vendedor");
        foreach (var pedido in pedidoRepository.Listar())
        {
            Console.WriteLine($"{pedido.PedidoId, -15} {pedido.PrazoEntrega, -21} {pedido.DataPedido, -21} {pedido.PedidoClienteId, -16} {pedido.PedidoVendedorId}");
        }
    }

    else if(modelAction == "Inserir")
    {
        Console.WriteLine("\nInserir Pedido");
        Console.Write("Qual o código do pedido: ");
        int pedidoId = Convert.ToInt32(Console.ReadLine());
        Console.Write("Qual o prazo de entrega do pedido: ");
        DateTime prazoEntrega = Convert.ToDateTime(Console.ReadLine());
        Console.Write("Qual o código de cliente do pedido: ");
        int pedidoVendedorId = Convert.ToInt32(Console.ReadLine());
        Console.Write("Qual o código do vendedor do pedido: ");
        int pedidoClienteId = Convert.ToInt32(Console.ReadLine());
        var pedido = new Pedido(pedidoId, prazoEntrega, pedidoVendedorId, pedidoClienteId);
        pedidoRepository.Inserir(pedido);
    }

    else if(modelAction == "Apresentar")
    {
        Console.WriteLine("\nApresentar Pedido");
        Console.Write("Digite o código do pedido: ");
        int pedidoid = Convert.ToInt32(Console.ReadLine());

        if(pedidoRepository.Apresentar(pedidoid))
        {
            var pedido = pedidoRepository.GetById(pedidoid);
            Console.WriteLine($"{pedido.PedidoId}, {pedido.PrazoEntrega}, {pedido.DataPedido}, {pedido.PedidoVendedorId}, {pedido.PedidoClienteId}");
        } 
        else 
        {
            Console.WriteLine($"O pedido com Id {pedidoid} não existe.");
        }
    }

    else if(modelAction == "MostrarPedidosCliente")
    {
        Console.WriteLine("\nMostrar Pedidos do Cliente");
        Console.Write("Digite o Código do cliente: ");
        int clienteid = Convert.ToInt32(Console.ReadLine());

        if(pedidoRepository.MostrarPedidosCliente(clienteid))
        {
            var pedidos = new List<Pedido>();
            pedidos = pedidoRepository.GetByClienteId(clienteid);
            
            Console.WriteLine("Código Pedido   Prazo Entrega         Data Pedido           Código Cliente   Código Vendedor");
            foreach (var pedido in pedidos)
            {
                Console.WriteLine($"{pedido.PedidoId, -15} {pedido.PrazoEntrega, -21} {pedido.DataPedido, -21} {pedido.PedidoClienteId, -16} {pedido.PedidoVendedorId}");
            }
        } 
        else 
        {
            Console.WriteLine($"O cliente com Id {clienteid} não existe.");
        }
    }
}
/////////////////////////////////////////
else if(modelName == "ItensPedido")
{
    if(modelAction == "Listar")
    {
        Console.WriteLine("\nListar Itens do Pedido");
        Console.WriteLine("Código Item Pedido   Quantidade   Código Pedido   Código Produto");
        foreach (var itenspedido in itensPedidoRepository.Listar())
        {
            Console.WriteLine($"{itenspedido.ItensPedidoId, -20} {itenspedido.Quantidade, -12} {itenspedido.ItensPedidoCodPedido, -15} {itenspedido.ItensPedidoCodProduto}");
        }
    }

    else if(modelAction == "Inserir")
    {
        Console.WriteLine("\nInserir Itens do Pedido");
        Console.Write("Digite o código do item do pedido: ");
        int itenspedidoid = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o código do pedido do item do pedido: ");
        int itenspedidocodpedido = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o código do produto do item do pedido: ");
        int itenspedidocodproduto = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite a quantidade do item do pedido: ");
        int quantidade = Convert.ToInt32(Console.ReadLine());
        var itenspedido = new ItensPedido(itenspedidoid, itenspedidocodpedido, itenspedidocodproduto, quantidade);
        itensPedidoRepository.Inserir(itenspedido);
    }

    else if(modelAction == "Apresentar")
    {
        Console.WriteLine("\nApresentar Itens do Pedido");
        Console.Write("Digite o código do pedido: ");
        int itenspedidoid = Convert.ToInt32(Console.ReadLine());

        if(itensPedidoRepository.Apresentar(itenspedidoid))
        {
            var itenspedido = itensPedidoRepository.GetById(itenspedidoid);
            Console.WriteLine($"{itenspedido.ItensPedidoId}, {itenspedido.ItensPedidoCodPedido}, {itenspedido.ItensPedidoCodProduto}, {itenspedido.Quantidade}");
        } 
        else 
        {
            Console.WriteLine($"O pedido com Id {itenspedidoid} não existe.");
        }
    }
}
/////////////////////////////////////////
else if(modelName == "Produto")
{
    if(modelAction == "Listar")
    {
        Console.WriteLine("\nListar Produto");
        Console.WriteLine("Código Produto   Descrição    Valor Unitário");
        foreach (var produto in produtoRepository.Listar())
        {
            Console.WriteLine($"{produto.ProdutoId, -16} {produto.Descricao, -12} {produto.ValorUnitario}");
        }
    }

    else if(modelAction == "Inserir")
    {
        Console.WriteLine("\nInserir Produto");
        Console.Write("Digite o Código do produto: ");
        int itenspedidoid = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o Descrição do produto: ");
        string descricao = Console.ReadLine();
        Console.Write("Digite o Valor Unitário do produto: ");
        decimal valorunitario = Convert.ToDecimal(Console.ReadLine());
        var produto = new Produto(itenspedidoid, descricao, valorunitario);
        produtoRepository.Inserir(produto);
    }

    else if(modelAction == "Apresentar")
    {
        Console.WriteLine("\nApresentar Produto");
        Console.Write("Digite o código do produto: ");
        int produtoid = Convert.ToInt32(Console.ReadLine());

        if(produtoRepository.Apresentar(produtoid))
        {
            var produto = produtoRepository.GetById(produtoid);
            Console.WriteLine($"{produto.ProdutoId}, {produto.Descricao}, {produto.ValorUnitario}");
        } 
        else 
        {
            Console.WriteLine($"O produto com Id {produtoid} não existe.");
        }
    }
}
/////////////////////////////////////////
else if(modelName == "Vendedor")
{
    if(modelAction == "Listar")
    {
        Console.WriteLine("\nListar Vendedor");
        Console.WriteLine("Código Vendedor   Nome Vendedor      Salário Fixo   Faixa Comissão");
        foreach (var vendedor in vendedorRepository.Listar())
        {
            Console.WriteLine($"{vendedor.VendedorId, -17} {vendedor.Nome, -18} {vendedor.SalarioFixo, -14} {vendedor.FaixaComissao}");
        }
    }

    else if(modelAction == "Inserir")
    {
        Console.WriteLine("\nInserir Vendedor");
        Console.Write("Digite o Código do vendedor: ");
        int vendedorid = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o Nome do vendedor: ");
        string nome = Console.ReadLine();
        Console.Write("Digite o Salário Fixo do vendedor: ");
        decimal salariofixo = Convert.ToDecimal(Console.ReadLine());
        Console.Write("Digite a Faixa de Comissão do vendedor: ");
        string faixacomissao = Console.ReadLine();
        var vendedor = new Vendedor(vendedorid, nome, salariofixo, faixacomissao);
        vendedorRepository.Inserir(vendedor);
    }

    else if(modelAction == "Apresentar")
    {
        Console.WriteLine("\nApresentar Vendedor");
        Console.Write("Digite o código do vendedor: ");
        int vendedorid = Convert.ToInt32(Console.ReadLine());

        if(vendedorRepository.Apresentar(vendedorid))
        {
            var vendedor = vendedorRepository.GetById(vendedorid);
            Console.WriteLine($"{vendedor.VendedorId}, {vendedor.Nome}, {vendedor.SalarioFixo}, {vendedor.FaixaComissao}");
        } 
        else 
        {
            Console.WriteLine($"O vendedor com Id {vendedorid} não existe.");
        }
    }
}