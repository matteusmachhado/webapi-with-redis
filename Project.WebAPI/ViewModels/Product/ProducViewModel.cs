using Project.Application.Features.Product.Commands.Create;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Project.WebAPI.ViewModels.Product
{
    public class ProducViewModel
    {
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do produto deve ter no máximo 100 caracteres.")]
        [DefaultValue("")]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "A descrição do produto deve ter no máximo 500 caracteres.")]
        [DefaultValue("")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço do produto é obrigatório.")]
        [Range(0.01, 999999.99, ErrorMessage = "O preço deve estar entre 0,01 e 999.999,99.")]
        [DataType(DataType.Currency, ErrorMessage = "O preço deve ser um valor monetário válido.")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "O preço deve ter até duas casas decimais.")]
        public decimal Price { get; set; }

        [JsonIgnore]
        public CreateCommand ToCommand => new CreateCommand(Name, Description, Price);
    }
}
