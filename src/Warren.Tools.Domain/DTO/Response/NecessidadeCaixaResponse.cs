namespace Com.Warren.Services.Response
{
    public class NecessidadeCaixaResponse
    {        
        public decimal ValorCaixaAtual { get; set; }        
        public decimal ValorAPagar { get; set; }
        public double? PorcentagemConsumida { get; set; } 
        public double? PorcentagemCaixa { get; set; } 
        public string MensagemErro { get; set; }

        // Construtor que inicializa os valores
        public NecessidadeCaixaResponse(decimal valorCaixaAtual,
                                         decimal valorAPagar,
                                         double? porcentagemConsumida,
                                         double? porcentagemCaixa)
        {
            ValorCaixaAtual = valorCaixaAtual;
            ValorAPagar = valorAPagar;
            PorcentagemConsumida = porcentagemConsumida;
            PorcentagemCaixa = porcentagemCaixa;
        }        
        public NecessidadeCaixaResponse(string mensagemErro)
        {
            MensagemErro = mensagemErro;
        }
    }
}
