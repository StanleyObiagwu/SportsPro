using SportsPro.Models;

namespace SportsPro.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly SportsProContext _sportsProContext;
        
        public UnitOfWork(SportsProContext sportsProContext, IGenericRepository<Product> productRepository, IGenericRepository<Incident> incidentRepository, IGenericRepository<Technician> technicianRepository, IGenericRepository<Customer> customerRepository, IGenericRepository<Registration> registrationRepository)
        {
            _sportsProContext = sportsProContext;
            ProductRepository = productRepository;
            IncidentRepository = incidentRepository;
            TechnicianRepository = technicianRepository;
            CustomerRepository = customerRepository;
            RegistrationRepository = registrationRepository;
        }
        
        public IGenericRepository<Product> ProductRepository { get; }
        public IGenericRepository<Incident> IncidentRepository { get; }
        public IGenericRepository<Technician> TechnicianRepository { get; }
        public IGenericRepository<Customer> CustomerRepository { get; }
        
        public IGenericRepository<Registration> RegistrationRepository { get; }

        public void Save()
        {
            _sportsProContext.SaveChanges();
        }
    }
}