using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab {get; private set;}
        public int turno {get;protected set;}
        public Cor jogadorAtual {get;protected set;}
        public bool terminada {get;private set;}
        public PartidaDeXadrez(){
            tab = new Tabuleiro(8,8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            colocarPeca();
        }
        public void executaMovimento(Posicao origem, Posicao destino){
            Peca p = tab.retirarPeca(origem);
            
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
        }
        public void realizaJogada(Posicao origem, Posicao destino){
            executaMovimento(origem, destino);
            turno++;
            mudaJogador();
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
            if(!tab.peca(origem).podeMoverPara(destino)){
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
        private void colocarPeca(){
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c',1).ToPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c',4).ToPosicao());
        }
    }
}