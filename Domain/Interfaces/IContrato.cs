
using Domain.Entities;

namespace Domain.Interfaces;

    public interface IContrato : IGenericRepository<Contrato>
    {
        Task<IEnumerable<Contrato>> GetContractByStatus();
    }
