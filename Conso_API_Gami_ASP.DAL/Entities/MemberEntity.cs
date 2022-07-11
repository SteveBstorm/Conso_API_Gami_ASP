using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Conso_API_Gami_ASP.DAL.Interfaces;

namespace Conso_API_Gami_ASP.DAL.Entities
{
    public class MemberEntity
    {
        public int Id { get; set; }

        public string Pseudo { get; set; }
        public string Email { get; set; }
        public string pwd { get; set; }
        public string pwdRepeat { get; set; }
    }

    public class MemberDetail
    {
        public int Id { get; set; }
        public string Pseudo { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }
        public IEnumerable<GameEntity> FavoriteList{ get; set; }
    }
}
