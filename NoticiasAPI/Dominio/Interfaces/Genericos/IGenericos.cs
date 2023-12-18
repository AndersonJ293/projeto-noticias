namespace Dominio.Interfaces.Genericos;

public interface IGenericos<T>
    where T : class
{
    Task Adicionar(T objeto);
    Task Atualizar(T objeto);
    Task Excluir(T objeto);
    Task<T?> BuscarPorId(int id);
    Task<List<T>> Listar();
}
