using tabuleiro;
using System.Collections.Generic;
namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab {get; private set;}
        public int turno {get;private set;}
        public Cor jogadorAtual {get;private set;}
        public bool terminada {get;private set;}
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque{get;private set;}
        public PartidaDeXadrez(){
            tab = new Tabuleiro(8,8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPeca();
        }
        public Peca executaMovimento(Posicao origem, Posicao destino){
            Peca p = tab.retirarPeca(origem);
            p.incrementarQtdeMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if(pecaCapturada != null){
                capturadas.Add(pecaCapturada);
            }
            //jogadaEspecial: Roque pequeno
            if(p is Rei && destino.coluna == origem.coluna + 2){
                Posicao origemT = new Posicao(origem.linha,origem.coluna +3);
                Posicao destinoT = new Posicao(origem.linha,origem.coluna +1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQtdeMovimentos();
                tab.colocarPeca(T, destinoT);
            }
            //jogadaEspecial: Roque grande
            if(p is Rei && destino.coluna == origem.coluna - 2){
                Posicao origemT = new Posicao(origem.linha,origem.coluna -4);
                Posicao destinoT = new Posicao(origem.linha,origem.coluna -1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQtdeMovimentos();
                tab.colocarPeca(T, destinoT);
            }
            return pecaCapturada;
        }
        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada){
            Peca p = tab.retirarPeca(destino);
            p.decrementarQtdeMovimentos();
            if(pecaCapturada != null){
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);
            //jogadaEspecial: Roque pequeno
            if(p is Rei && destino.coluna == origem.coluna + 2){
                Posicao origemT = new Posicao(origem.linha,origem.coluna +3);
                Posicao destinoT = new Posicao(origem.linha,origem.coluna +1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQtdeMovimentos();
                tab.colocarPeca(T, origemT);
            }
            //jogadaEspecial: Roque grande
            if(p is Rei && destino.coluna == origem.coluna - 2){
                Posicao origemT = new Posicao(origem.linha,origem.coluna -4);
                Posicao destinoT = new Posicao(origem.linha,origem.coluna -1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQtdeMovimentos();
                tab.colocarPeca(T, origemT);
            }
        }
        public void realizaJogada(Posicao origem, Posicao destino){
            Peca pecaCapturada =  executaMovimento(origem, destino);
            if(estaEmXeque(jogadorAtual)){
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }
            if(estaEmXeque(adversario(jogadorAtual))){
                xeque = true;
            }else{
                xeque = false;
            }
            if(testeXequeMate(adversario(jogadorAtual))){
                terminada = true;
            }else{
                turno++;
                mudaJogador();
            }
        }
        public void validarPosicaoDeOrigem(Posicao pos){
            if(tab.peca(pos)== null){
                throw new TabuleiroException("Não existe peca na posicao de origem escolhida");
            }
            if(jogadorAtual != tab.peca(pos).cor){
                throw new TabuleiroException("A peca de origem escolhida não é sua");
            }
            if(!tab.peca(pos).existeMovimentosPossiveis()){
                throw new TabuleiroException("Não ha movimentos possiveis para a peca de origem escolhida");
            }
        }
        public void validarPosicaoDeDestino(Posicao origem, Posicao destino){
            if(!tab.peca(origem).movimentoPossivel(destino)){
                throw new TabuleiroException("Posicao de destino invalida");
            }
        }
        private void mudaJogador(){
            if(jogadorAtual == Cor.Branca){
                jogadorAtual = Cor.Preta;
            }else{
                jogadorAtual = Cor.Branca;
            }
        }
        public HashSet<Peca> pecasCapturadas(Cor cor ){
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas){
                if(x.cor == cor){
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Peca> pecasEmJogo(Cor cor ){
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in pecas){
                if(x.cor == cor){
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }
        private Cor adversario(Cor cor){
            if(cor == Cor.Branca){
                return Cor.Preta;
            }else{
                return Cor.Branca;
            }
        }
        private Peca rei(Cor cor){
            foreach (Peca x in pecasEmJogo(cor))
            {
                if(x is Rei){
                    return x;
                }
            }
            return null;
        }
        public bool estaEmXeque(Cor cor){
            Peca R = rei(cor);
            if(R == null){
                throw new TabuleiroException("Não tem rei da cor "+cor+" no tabuleiro");
            }
            foreach(Peca x in pecasEmJogo(adversario(cor))){
                bool[,] mat = x.movimentosPossiveis();
                if(mat[R.posicao.linha, R.posicao.coluna]){
                    return true;
                }
            }
            return false;
        }
        public bool testeXequeMate(Cor cor){
            if(!estaEmXeque(cor)){
                return false;
            }
            foreach(Peca x in pecasEmJogo(cor)){
                bool[,] mat = x.movimentosPossiveis();
                for(int i=0; i<tab.linhas; i++){
                    for(int j=0; j<tab.colunas; j++){
                        if(mat[i,j]){
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i,j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if(!testeXeque){
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public void colocaNovaPeca(char coluna, int linha, Peca peca){
            tab.colocarPeca(peca, new PosicaoXadrez(coluna,linha).ToPosicao());
            pecas.Add(peca);
        }
        private void colocarPeca(){
            colocaNovaPeca('a',1, new Torre(tab, Cor.Branca));
            colocaNovaPeca('h',1, new Torre(tab, Cor.Branca));
            colocaNovaPeca('b',1, new Cavalo(tab, Cor.Branca));
            //colocaNovaPeca('g',1, new Cavalo(tab, Cor.Branca));
            colocaNovaPeca('c',1, new Bispo(tab, Cor.Branca));
            //colocaNovaPeca('f',1, new Bispo(tab, Cor.Branca));
            colocaNovaPeca('d',1, new Rainha(tab, Cor.Branca));
            colocaNovaPeca('e',1, new Rei(tab, Cor.Branca, this));
            colocaNovaPeca('a',2, new Peao(tab, Cor.Branca));
            colocaNovaPeca('b',2, new Peao(tab, Cor.Branca));
            colocaNovaPeca('c',2, new Peao(tab, Cor.Branca));
            colocaNovaPeca('d',2, new Peao(tab, Cor.Branca));
            colocaNovaPeca('e',2, new Peao(tab, Cor.Branca));
            colocaNovaPeca('f',2, new Peao(tab, Cor.Branca));
            colocaNovaPeca('g',2, new Peao(tab, Cor.Branca));
            colocaNovaPeca('h',2, new Peao(tab, Cor.Branca));
            //-----------------Pretas------------------------
            colocaNovaPeca('a',8, new Torre(tab, Cor.Preta));
            colocaNovaPeca('h',8, new Torre(tab, Cor.Preta));
            //colocaNovaPeca('b',8, new Cavalo(tab, Cor.Preta));
            colocaNovaPeca('g',8, new Cavalo(tab, Cor.Preta));
            //colocaNovaPeca('c',8, new Bispo(tab, Cor.Preta));
            colocaNovaPeca('f',8, new Bispo(tab, Cor.Preta));
            //colocaNovaPeca('d',8, new Rainha(tab, Cor.Preta));
            colocaNovaPeca('e',8, new Rei(tab, Cor.Preta, this));
            colocaNovaPeca('a',7, new Peao(tab, Cor.Preta));
            colocaNovaPeca('b',7, new Peao(tab, Cor.Preta));
            colocaNovaPeca('c',7, new Peao(tab, Cor.Preta));
            colocaNovaPeca('d',7, new Peao(tab, Cor.Preta));
            colocaNovaPeca('e',7, new Peao(tab, Cor.Preta));
            colocaNovaPeca('f',7, new Peao(tab, Cor.Preta));
            colocaNovaPeca('g',7, new Peao(tab, Cor.Preta));
            colocaNovaPeca('h',7, new Peao(tab, Cor.Preta));
        }
    }
}