using Conso_API_Gami_ASP.DAL.Interfaces;

namespace Conso_API_Gami_ASP.DAL.Entities
{
    public class GameEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NbPlayerMin { get; set; }
        public int NbPlayerMax { get; set; }
        public int? Age { get; set; }
        public bool IsCoop { get; set; }
    }
}
