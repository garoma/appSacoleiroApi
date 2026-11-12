
using Microsoft.AspNetCore.Http;

namespace appSacoleiro.API.Models;

public sealed class AtualizarImagemForm
{
    public IFormFile? Imagem { get; set; }
}
