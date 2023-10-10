using ApplicationLayer;
using DomainLayer.Interfaces.Repository;
using DomainLayer.Interfaces.Service;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceLayer
{
    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _repository;

        public ProfessorService(IProfessorRepository repository) => _repository = repository;

        /// <summary>
        /// Método responsável por salvar os dados de um professor
        /// </summary>
        /// <param name="professor"></param>
        /// <returns>professor</returns>
        public Professor Registra(Professor professor) => _repository.Registra(professor);

        public IEnumerable<Professor> Lista() => _repository.Lista();

        public IEnumerable<Professor> Busca(string nome) => _repository.Busca(nome);

        Professor IProfessorService.Atualiza(Professor professor) => _repository.Atualiza(professor);

        void IProfessorService.Apaga(Guid id) => _repository.Apaga(id);
    }
}
