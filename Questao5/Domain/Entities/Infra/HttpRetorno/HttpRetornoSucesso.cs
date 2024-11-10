namespace Questao5.Domain.Entities
{
    public class HttpRetornoSucesso
    {
        public HttpRetornoSucesso(string mensagem, object? conteudo)
        {
            Message = mensagem;
            Content = conteudo;
        }

        public string Message { get; set; }
        public object? Content { get; set; }
    }
}
