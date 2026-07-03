using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Kafka.Consumer.Console
{
    internal class PessoaModel
    {
        [JsonPropertyName("Nome")]
        public string? Nome {  get; set; }
        [JsonPropertyName("Idade")]
        public int Idade {  get; set; }

        public override string ToString()
        {
            return $"Seu nome é {this.Nome} e sua idade é {this.Idade.ToString()}";
        }
    }
}
