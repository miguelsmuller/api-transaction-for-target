using AutoMapper;
using finance_api.Data.Dtos;
using finance_api.Models;


namespace finance_api.Profiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<CreateTransactionDTO, Transaction>();
        CreateMap<UpdateTransactionDTO, Transaction>();
        CreateMap<Transaction, UpdateTransactionDTO>();
        CreateMap<Transaction, ReadTransactionDTO>();
    }
}

