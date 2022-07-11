using Conso_API_Gami_ASP.DAL.Entities;
using Conso_API_Gami_ASP.DAL.Interfaces;


namespace Demo_ASP_MVC_Modele.WebApp.Models.Mappers
{
    public static class MemberMapper
    {
        public static MemberEntity ToBll(this MemberRegForm form)
        {
            return new MemberEntity
            {
                Email = form.Email,
                pwd = form.Pwd,
                pwdRepeat = form.PwdRepeat,
                Pseudo = form.Pseudo
            };
        }

        public static Member ToWeb(this MemberDetail m)
        {
            return new Member
            {
                Id = m.Id,
                Email = m.Email,
                Pseudo = m.Pseudo,
                Token = m.Token
            };
        }

        
    }
}
