using Conso_API_Gami_ASP.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conso_API_Gami_ASP.DAL.Interfaces
{
    public interface IMemberRepository
    {
        string Login(string pseudo, string password);
        MemberDetail GetById(int id, string token);
        bool Insert(MemberEntity entity);
    }
}
