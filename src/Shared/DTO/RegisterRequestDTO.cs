using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.CESIZen.Shared.DTOs
{
    public class RegisterRequestDTO
    {
        // For the User table
        [Required(ErrorMessage = "Le prénom doit être renseigné.")]
        [MaxLength(50, ErrorMessage = "Le prénom ne doit pas dépasser 50 caractères.")]
        public string Firstname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le nom doit être renseigné.")]
        [MaxLength(50, ErrorMessage = "Le nom ne doit pas dépasser 50 caractères.")]
        public string Lastname { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'email est obligatoire.")]
        [EmailAddress(ErrorMessage = "L'email n'est pas au bon format")]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [MinLength(10, ErrorMessage = "Le mot de passe doit faire au moins 10 caractères.")]

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{10,}$", ErrorMessage = "Le mot de passe doit contenir au moins une majuscule, une minuscule, un chiffre et un caractère spécial.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "La confirmation du mot de passe est obligatoire.")]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public DateTime? Birthday { get; set; }

        // For the Profile table
        [Required(ErrorMessage = "Le pseudo doit être renseigné.")]
        [MinLength(3, ErrorMessage = "Le pseudo doit faire au moins 3 caractères.")]
        [MaxLength(50, ErrorMessage = "Le pseudo ne doit pas dépasser 50 caractères.")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Le pseudonyme ne doit pas comporter d'espaces.")]
        public string Username { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Gender { get; set; }
    }
}