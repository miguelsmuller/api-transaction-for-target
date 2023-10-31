using System.ComponentModel.DataAnnotations;

namespace finance_api.Data.Dtos;

public class ReadTransactionDTO
{
    public int ID {get; set;}

    public string Descricao {get; set;}

    public DateTime Data {get; set;}

    public decimal Valor {get; set;}

    public bool Avulso {get; set;}

    public bool Status {get; set;}

    public DateTime ViewingTime {get;set;} = DateTime.Now;
}

