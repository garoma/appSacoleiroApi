using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace appSacoleiro.API.Models;

public sealed class CriarProdutoForm
{
    [Required]
    [StringLength(150)]
    public string Nome { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Descricao { get; set; }

    [Range(0.01, 9999999999)]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(100)]
    public string Categoria { get; set; } = string.Empty;

    public IFormFile? Imagem1 { get; set; }

    public IFormFile? Imagem2 { get; set; }

    public IFormFile? Video { get; set; }
}