using Conso_API_Gami_ASP.DAL.Entities;
using Conso_API_Gami_ASP.DAL.Interfaces;
using Conso_API_Gami_ASP.DAL.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conso_API_Gami_ASP.DAL.Repositories
{
    public class GameRepository : ApiRequester, IGameRepository
    {
        public GameRepository() : base("https://localhost:7186/api/")
        {
        }

        public bool AddFavoriteGame(int memberId, int gameId, string token)
        {
            return Post($"game/favorite/{gameId}/member/{memberId}", new { }, token);

        }

        public bool Delete(int id, string token)
        {
            return Delete("game/" + id, token);
        }

        public IEnumerable<GameEntity> GetAll()
        {
            return Get<IEnumerable<GameEntity>>("game");
        }

        public GameEntity GetById(int id)
        {
            return Get<GameEntity>("game/" + id);
        }



        public bool Insert(GameEntity entity, string token)
        {
            return Post("game", entity, token);
        }

        public bool Update(GameEntity entity, string token)
        {
            return Put("game", entity, token);
        }
    }
}
