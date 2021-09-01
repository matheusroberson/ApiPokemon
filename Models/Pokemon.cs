using System;
using System.ComponentModel.DataAnnotations;

namespace ApiPokemon.Models
{
    public class Pokemon
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Mestre inválido")]
        public int IdMestre { get; set; }
        public Mestre Mestre { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, 1018, ErrorMessage = "Pokemon inválido")]
        public int IdPokemon { get; set; }

    }
}