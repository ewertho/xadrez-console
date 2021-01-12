using System;
using tabuleiro;
namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Posicao posicao;

            posicao = new Posicao(3,4);

            System.Console.WriteLine("Posicao: "+posicao);
            Console.ReadLine();
            
        }
    }
}
