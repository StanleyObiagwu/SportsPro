using SportsPro.Models;

namespace SportsPro.Data
{
    public interface IUnitOfWork
    {
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<Incident> IncidentRepository { get; }
        IGenericRepository<Technician> TechnicianRepository { get; }
        IGenericRepository<Customer> CustomerRepository { get; }
        
        IGenericRepository<Registration> RegistrationRepository { get; }
        void Save();
    }
}