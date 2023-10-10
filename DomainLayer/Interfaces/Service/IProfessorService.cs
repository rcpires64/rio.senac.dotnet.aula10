using ApplicationLayer;

namespace DomainLayer.Interfaces.Service
{
    public interface IProfessorService
    {
        Professor Registra(Professor professor);
        IEnumerable<Professor> Lista();
        IEnumerable<Professor> Busca(string nome);
        Professor Atualiza(Professor professor);
        void Apaga(Guid id);
    }
}
