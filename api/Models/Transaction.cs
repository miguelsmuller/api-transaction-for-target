using System.ComponentModel.DataAnnotations;

namespace finance_api.Models;

public class Transaction
{
    [Key]
    [Required(ErrorMessage = "required ID field")]
    public int ID {get; set;}

    [Required(ErrorMessage = "required Descricao field")]
    [MaxLength(50, ErrorMessage = "Descricao size greater than 50")]
    public string Descricao {get; set;}

    [Required(ErrorMessage = "required Data field")]
    public DateTime Data {get; set;}

    [Required(ErrorMessage = "required Valor field")]
    [Range(0, double.MaxValue, ErrorMessage = "Valor must be a positive number")]
    public decimal Valor {get; set;}

    [Required(ErrorMessage = "required Avulso field")]
    public bool Avulso {get; set;}

    [Required(ErrorMessage = "required Status field")]
    public bool Status {get; set;}
}
