using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;

namespace Examen.ApplicationCore.Services
{
    public class LaboratoireService : Service<Laboratoire>, ILaboratoireService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LaboratoireService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Vous pouvez ajouter des méthodes spécifiques aux laboratoires ici si nécessaire
    }
}
