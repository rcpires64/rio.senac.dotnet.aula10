using ApplicationLayer;

namespace DomainLayer.Interfaces.Repository
{
    public interface IProfessorRepository
    {
        Professor Registra(Professor professor);
        IEnumerable<Professor> Lista();
        IEnumerable<Professor> Busca(string nome);
        Professor Atualiza(Professor professor);
        void Apaga(Guid id);
    }
}
