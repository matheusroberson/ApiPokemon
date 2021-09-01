using System;
using System.ComponentModel.DataAnnotations;

namespace ApiPokemon.Models
{
    public class Mestre
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(2, ErrorMessage = "Esse campo deve conter entre 1 e 2 caracteres")]
        [MinLength(1, ErrorMessage = "Esse campo deve conter entre 1 e 2 caracteres")]
        public string Age { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(80, ErrorMessage = "Esse campo deve conter no máximo 80 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(11, ErrorMessage = "Este campo deve conter 11 caracteres")]
        [MaxLength(11, ErrorMessage = "Este campo deve conter 11 caracteres")]
        private string cpf;
        public string Cpf
        {
            get
            {
                return this.cpf.Substring(1, this.cpf.Length - 1);
            }
            set
            {
                this.cpf = "0" + Convert.ToString(value);
            }
        }
    }
}