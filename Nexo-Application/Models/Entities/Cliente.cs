using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NexoApplication.Models.Entities
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        
        [Required]
        public string Nome { get; set; }
        
        [DisplayName("Sobrenome")]
        [Required]
        public string SobreNome { get; set; }
        
        [Required(ErrorMessage = "Um email válido é obrigatório.")]
        [EmailAddress(ErrorMessage = "Endereço de Email Inválido.")]
        public string Email { get; set; }
        
        public DateTime DataCadastro { get; set; }
        
        public bool Ativo { get; set; }
    }
}
