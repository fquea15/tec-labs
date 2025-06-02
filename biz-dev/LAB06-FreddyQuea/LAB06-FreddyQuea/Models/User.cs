using System.ComponentModel.DataAnnotations;

namespace LAB06_FreddyQuea.Models;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El Username es obligatorio.")]
    [MaxLength(100, ErrorMessage = "El Username no puede superar los 100 caracteres.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "El rol es obligatorio.")]
    [MaxLength(50, ErrorMessage = "El Rol no puede superar los 50 caracteres.")]
    public string Role { get; set; }
}

