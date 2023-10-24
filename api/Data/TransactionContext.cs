using finance_api.Models;
using Microsoft.EntityFrameworkCore;

namespace finance_api.Data;

public class TransactionContext : DbContext
{
    public TransactionContext(DbContextOptions<TransactionContext> opts) : base(opts){

    }

    public DbSet<Transaction> Transactions {get; set;}
}
