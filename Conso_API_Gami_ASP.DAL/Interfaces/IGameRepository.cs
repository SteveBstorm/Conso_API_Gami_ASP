using Conso_API_Gami_ASP.DAL.Entities;

namespace Conso_API_Gami_ASP.DAL.Interfaces
{
    public interface IGameRepository
    {
        bool AddFavoriteGame(int memberId, int gameId, string token);
        bool Delete(int id, string token);
        IEnumerable<GameEntity> GetAll();
        GameEntity GetById(int id);
        bool Insert(GameEntity entity, string token);
        bool Update(GameEntity entity, string token);
    }
}