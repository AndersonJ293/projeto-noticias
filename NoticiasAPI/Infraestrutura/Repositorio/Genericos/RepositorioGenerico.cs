using System.Runtime.InteropServices;
using Dominio.Interfaces.Genericos;
using Infraestrutura.Configuracoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;

namespace Infraestrutura.Repositorio.Genericos;

public class RepositorioGenerico<T> : IGenericos<T>, IDisposable
    where T : class
{
    private readonly DbContextOptions<Contexto> _optionsBuilder;

    public RepositorioGenerico()
    {
        _optionsBuilder = new DbContextOptions<Contexto>();
    }

    public async Task Adicionar(T objeto)
    {
        using (var data = new Contexto(_optionsBuilder))
        {
            await data.Set<T>().AddAsync(objeto);
            await data.SaveChangesAsync();
        }
    }

    public async Task Atualizar(T objeto)
    {
        using (var data = new Contexto(_optionsBuilder))
        {
            data.Set<T>().Update(objeto);
            await data.SaveChangesAsync();
        }
    }

    public async Task Excluir(T objeto)
    {
        using (var data = new Contexto(_optionsBuilder))
        {
            data.Set<T>().Remove(objeto);
            await data.SaveChangesAsync();
        }
    }

    public async Task<T?> BuscarPorId(int id)
    {
        using (var data = new Contexto(_optionsBuilder))
        {
            return await data.Set<T>().FindAsync(id);
        }
    }

    public async Task<List<T>> Listar()
    {
        using (var data = new Contexto(_optionsBuilder))
        {
            return await data.Set<T>().AsNoTracking().ToListAsync();
        }
    }

    // Flag: Has Dispose already been called?
    bool _disposed = false;

    // Instantiate a SafeHandle instance.
    private readonly SafeHandle _handle = new SafeFileHandle(IntPtr.Zero, true);

    // Public implementation of Dispose pattern callable by consumers.
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _handle.Dispose();
            // Free any other managed objects here.
        }

        _disposed = true;
    }
}
