using System;
using tabuleiro;
using xadrez;
namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();
                while(!partida.terminada){
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.tab);
                    System.Console.WriteLine();
                    Console.Write("Origem: ");
                    Posicao origem = Tela.lerPosicaoXadrez().ToPosicao();
                    bool[,] posicaoPossiveis = partida.tab.peca(origem).movimentosPossiveis();
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.tab, posicaoPossiveis);
                    System.Console.WriteLine();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.lerPosicaoXadrez().ToPosicao();
                    partida.executaMovimento(origem, destino);
                }
            }
            catch (TabuleiroException e)
            {
                System.Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
