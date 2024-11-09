using System;
using System.Globalization;

namespace Questao1
{
    public class ContaBancaria 
    {
        public ContaBancaria(int numero, string titular, double depositoInicial)
        {
            Numero = numero;
            Titular = titular;
            Saldo = depositoInicial > 0 ? depositoInicial : 0;
        }

        public ContaBancaria(int numero, string titular)
        {
            Numero = numero;
            Titular = titular;
            Saldo = 0;
        }

        public readonly int Numero;
        public string Titular { get; set; }
        public double Saldo { get; private set; }

        public void Deposito(double quantia)
        {
            if (quantia <= 0)
            {
                return;
            }

            Saldo += quantia;
        }

        public void Saque(double quantia)
        {

            if (quantia <= 0)
            {
                return;
            }

            Saldo -= (quantia + 3.5);
        }

        public override string ToString()
        {
            return $"Conta {Numero}, Titular: {Titular}, Saldo: $ {Saldo.ToString("F2", CultureInfo.InvariantCulture)}";
        }
    }
}
