using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroOndas.Models
{
    public class MicroOndas
    {
        public int tempo;
        public int potencia;

        public MicroOndas(int tempo, int potencia)
        {
            this.tempo = tempo;
            if (potencia <= 0)
            {
                potencia = 10;
            }
            else
            {
                this.potencia = potencia;
            }
        }

        public void setTempo(string tempo)
        {
            if (tempo == "")
                throw new Exception("Informe um tempo válido, entre 1 segundo até 120");

            try
            {
                this.tempo = int.Parse(tempo);
                if (this.tempo < 1 || this.tempo > 120)
                {
                    throw new Exception("O tempo deve ser númerico, entre 1 a 120.");
                }
            }
            catch (FormatException fe)
            {
                throw new Exception("O tempo deve ser númerico, entre 1 a 120." + fe.Message);
            }
            catch (OverflowException ofl)
            {
                throw new Exception("O tempo deve ser inteiro." + ofl.Message);
            }

        }

        public int getTempo()
        {
            return this.tempo;
        }

        public void setPotencia(string potencia)
        {
            if(potencia=="")
                throw new Exception("Informe uma potencia válida, entre 1 segundo até 10");

            try
            {
                this.potencia = int.Parse(potencia);
                if(this.potencia<1 || this.potencia > 10)
                {
                    throw new Exception("A potência deve ser númerico, entre 1 a 10.");
                }
            }
            catch (FormatException fe)
            {
                throw new Exception("A potência deve ser númerico, entre 1 a 10." + fe.Message);
            }
            catch (OverflowException ofl)
            {
                throw new Exception("A potência deve ser inteiro." + ofl.Message);
            }
        }

        public int getPotencia()
        {
            return this.potencia;
        }
    }
}