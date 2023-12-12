
using Domain.Entities;

namespace Domain.Interfaces;

    public interface IPersona : IGenericRepository<Persona>
    {
        Task<IEnumerable<Persona>> GetAllEmployeeFromCompany();
        Task<IEnumerable<Persona>> GetAllEmployeebyCategory(string categoria);
        Task<IEnumerable<Persona>> GetAllPhoneNumEmployeeByCategory(string categoria);
        Task<IEnumerable<Persona>> GetCustomersByCity(string city);
        Task<IEnumerable<Persona>> GetCustomersByDirection(string direction1, string direction2);
        Task<IEnumerable<Persona>> GetCustomersByantiquity(int quantity);
    }
