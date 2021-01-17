using System.Collections.Generic;
using System;
using tabuleiro;
using xadrez;
namespace xadrez_console
{
    class Tela
    {
        public static void imprimirPartida(PartidaDeXadrez partida){
            imprimirTabuleiro(partida.tab);
            System.Console.WriteLine();
            imprimirPecasCapturadas(partida);
            System.Console.WriteLine();
            System.Console.WriteLine("Turno: "+partida.turno);
            System.Console.WriteLine("Aguardando Jogada: "+partida.jogadorAtual);
            if(partida.xeque){
                System.Console.WriteLine("Xeque!");
            }
        }
        public static void imprimirPecasCapturadas(PartidaDeXadrez partida){
            System.Console.WriteLine("Pecas capturadas: ");
            System.Console.Write("Brancas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Branca));
            System.Console.WriteLine();
            System.Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            imprimirConjunto(partida.pecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            System.Console.WriteLine();
        }
        public static void imprimirConjunto(HashSet<Peca> conjunto){
            System.Console.Write("[");
            foreach(Peca x in conjunto){
                System.Console.Write(x +" ");
            }
            System.Console.Write("]");
        }
        public static void imprimirTabuleiro(Tabuleiro tab){
            for(int i=0; i<tab.linhas; i++){
                System.Console.Write(8 - i + " ");
                for(int j=0; j<tab.colunas; j++){
                    imprimirPeca(tab.peca(i, j));
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine("  a b c d e f g h");
        }
        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicaoPossiveis){
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;
            for(int i=0; i<tab.linhas; i++){
                System.Console.Write(8 - i + " ");
                for(int j=0; j<tab.colunas; j++){
                    if(posicaoPossiveis[i,j]){
                        Console.BackgroundColor = fundoAlterado;
                    }else{
                        Console.BackgroundColor = fundoOriginal;
                    }
                    imprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }
        public static PosicaoXadrez lerPosicaoXadrez(){
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1]+"");
            return new PosicaoXadrez(coluna, linha);
        }
        public static void imprimirPeca(Peca peca){
            if(peca == null){
                System.Console.Write("- ");
            }else{
                if(peca.cor == Cor.Branca){
                    System.Console.Write(peca);
                }else{
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    System.Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                System.Console.Write(" ");
            }
        }
    }
}