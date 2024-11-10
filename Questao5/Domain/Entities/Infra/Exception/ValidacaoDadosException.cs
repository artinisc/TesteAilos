namespace Questao5.Domain.Entities
{
    public class ValidacaoDadosException : Exception
    {
        public string Tipo { get; }

        public ValidacaoDadosException(string mensagem, string tipo) : base(mensagem)
        {
            Tipo = tipo;
        }
    }
}
