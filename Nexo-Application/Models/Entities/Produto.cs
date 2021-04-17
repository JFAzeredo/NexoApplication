using System.ComponentModel.DataAnnotations;

namespace NexoApplication.Models.Entities
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        
        [Required]     
        public string Nome { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Por favor, entre com um valor maior do que 0")]
        public double Valor { get; set; }
        
        public bool Disponivel { get; set; }
        
        public int ClienteId { get; set; }
    }
}
