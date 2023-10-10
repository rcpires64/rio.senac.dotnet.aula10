using ApplicationLayer;
using DomainLayer.ViewModels;

namespace DomainLayer.Interfaces.Repository
{
    public interface IAlunoRepository
    {
        Task Registra(AlunoCadastroViewModel viewModel);
        Task<IEnumerable<Aluno>> Lista();
        Task<IEnumerable<Aluno>> Busca(string nome);
        Task Atualiza(Aluno aluno);
        Task Apaga(Guid id);
        Task RegistraNotas(AlunoNotasViewModel viewModel);
        Task<AlunoNotasViewModel> BuscaNotas(string matricula);
        Task<dynamic> BuscaAlunoNotas(string matricula);
    }
}
