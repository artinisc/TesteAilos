namespace Questao5.Domain.Entities
{
    public class HttpRetornoFalha
    {
        public HttpRetornoFalha(string mensagem, string tipo)
        {
            Message = mensagem;
            Tipo = tipo;
        }

        public string Message { get; set; }
        public string Tipo { get; set; }
    }
}
