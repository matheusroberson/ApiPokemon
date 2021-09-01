using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiPokemon.Models
{
    public class ModelDefault
    {
        public string name { get; set; }
    }
    public class Abilities
    {
        public ModelDefault ability { get; set; }

    }


    public class Stats
    {
        public int base_stat { get; set; }
        public ModelDefault stat { get; set; }

    }

    public class Types
    {
        public ModelDefault type { get; set; }
    }

    public class Sprites
    {
        public string front_default { get; set; }
    }

    public class PokeApi
    {
        public int order { get; set; }
        public string name { get; set; }
        public Sprites sprites { get; set; }
        public int weight { get; set; }
        public int height { get; set; }
        public int id { get; set; }
        public List<Abilities> abilities { get; set; }
        public List<Types> types { get; set; }
        public List<Stats> stats { get; set; }

        public Chain chain { get; set; }
    }

    public class ModelPokeResult
    {
        public string url { get; set; }

    }

    public class PokeResult
    {
        public List<ModelPokeResult> results { get; set; }
    }

    public class PokeEvolutions
    {
        public ModelDefault species { get; set; }
        public List<PokeEvolutions> evolves_to { get; set; }
    }

    public class Chain
    {
        public ModelDefault species { get; set; }

        public List<PokeEvolutions> evolves_to { get; set; }
    }

    public class Evolution
    {
        public string url { get; set; }
    }
    public class Evolve
    {
        public Evolution evolution_chain { get; set; }
    }
}