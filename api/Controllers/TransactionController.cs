using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using finance_api.Models;
using finance_api.Data;
using finance_api.Data.Dtos;

namespace finance_api.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private TransactionContext _ctx;
    private IMapper _mapper;

    public TransactionController(TransactionContext context, IMapper mapper){
        _ctx = context;
        _mapper = mapper;
    }


    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AddTransaction(
        [FromBody] CreateTransactionDTO transactionDTO
    ){
        Transaction transaction = _mapper.Map<Transaction>(transactionDTO);

        _ctx.Transactions.Add(transaction);
        _ctx.SaveChanges();

        return CreatedAtAction(
            nameof(GetTransactionByID), 
            new {id = transaction.ID}, 
            transaction
        );
    }


    /// <summary>
    /// Obtém uma lista de transações do banco de dados.
    /// </summary>
    /// <param name="skip">Número de registros a serem ignorados</param>
    /// <param name="take">Número máximo de registros a serem retornados</param>
    /// <returns>Uma lista de transações</returns>
    [HttpGet]
    public IEnumerable<ReadTransactionDTO> GetTransactions(
        [FromQueryAttribute] int skip = 0, 
        [FromQueryAttribute] int take = 10
    ){
        return _mapper.Map<List<ReadTransactionDTO>>(
            _ctx.Transactions.Skip(skip).Take(take)
        );
    }


    /// <summary>
    /// Obtém os detalhes de uma transação específica com base no ID fornecido.
    /// </summary>
    /// <param name="ID">ID da transação desejada</param>
    /// <returns>Detalhes da transação correspondente</returns>
    [HttpGet("{ID}")]
    public IActionResult GetTransactionByID(int ID){
        var transaction = _ctx.Transactions.FirstOrDefault(
            transaction => transaction.ID == ID
        );

        if (transaction== null) return NotFound();

        var transactionDTO = _mapper.Map<ReadTransactionDTO>(transaction);

        return Ok(transactionDTO);
    }


    /// <summary>
    /// Atualiza os detalhes de uma transação específica com base no ID fornecido.
    /// </summary>
    /// <param name="ID">ID da transação a ser atualizada</param>
    /// <param name="transactionDTO">Objeto com os campos a serem atualizados</param>
    /// <returns>IActionResult</returns>
    [HttpPut("{ID}")]
    public IActionResult UpdateTransaction(
        int ID,
        [FromBody] UpdateTransactionDTO transactionDTO
    ){
        var transaction = _ctx.Transactions.FirstOrDefault(
            transaction => transaction.ID == ID
        );

        if (transaction == null) return NotFound();

        _mapper.Map(transactionDTO, transaction);

        _ctx.SaveChanges();

        return NoContent(); 
    }

    /// <summary>
    /// Atualiza parcialmente os detalhes de uma transação com base no ID fornecido, aplicando um patch JSON.
    /// </summary>
    /// <param name="ID">ID da transação a ser atualizada parcialmente</param>
    /// <param name="patch">Patch JSON com as alterações desejadas</param>
    /// <returns>IActionResult</returns>
    [HttpPatch("{ID}")]
    public IActionResult UpdatePartialTransaction(
        int ID,
        JsonPatchDocument<UpdateTransactionDTO> patch
    ){
        var transaction = _ctx.Transactions.FirstOrDefault(
            transaction => transaction.ID == ID
        );

        if (transaction == null) return NotFound();

        var transactionToUpdate = _mapper.Map<UpdateTransactionDTO>(transaction);

        patch.ApplyTo(transactionToUpdate, ModelState);

        if (!TryValidateModel(transactionToUpdate)){
            return ValidationProblem(ModelState);
        }

        _mapper.Map(transactionToUpdate, transaction);

        _ctx.SaveChanges();

        return NoContent(); 
    }


    /// <summary>
    /// Remove uma transação com base no ID fornecido.
    /// </summary>
    /// <param name="ID">ID da transação a ser removida</param>
    /// <returns>IActionResult</returns>
    [HttpDelete("{ID}")]
    public IActionResult DeleteTransaction(int ID){
        var transaction = _ctx.Transactions.FirstOrDefault(
            transaction => transaction.ID == ID
        );

        if (transaction == null) return NotFound();

        _ctx.Remove(transaction);
        _ctx.SaveChanges();

        return NoContent(); 
    }
}
