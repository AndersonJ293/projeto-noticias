namespace Entidades.Entidades.ViewModels;

public class NoticiaViewModel
{
    public int Id { get; set; }

    public string? Titulo { get; set; }

    public string? Informacao { get; set; }

    public DateTime DataCadastro { get; set; }

    public string Usuario { get; set; }
}
