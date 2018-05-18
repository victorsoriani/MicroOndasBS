using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroOndas.Models
{
    public class ProgramasPreDefinidos
    {
        public string nome { get; set; }
        public string instrucaoDeUso { get; set; }
        public char caractere { get; set; }
        public int tempo { get; set; }
        public int potencia { get; set; }

        public ProgramasPreDefinidos(string nome, string instrucaoDeUso, char caractere, int tempo, int potencia)
        {
            this.nome = nome;
            this.caractere = caractere;
            this.instrucaoDeUso = instrucaoDeUso;
            this.caractere = caractere;
            this.tempo = tempo;
            this.potencia = potencia;
        }
    }
}