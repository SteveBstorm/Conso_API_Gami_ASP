using Conso_API_Gami_ASP.DAL.Entities;
using Conso_API_Gami_ASP.DAL.Interfaces;
using Conso_API_Gami_ASP.DAL.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conso_API_Gami_ASP.DAL.Repositories
{

    public class MemberRepository : ApiRequester, IMemberRepository
    {
        public MemberRepository() : base("https://localhost:7186/api/")
        {
        }

        public MemberDetail GetById(int id, string token)
        {
            return Get<MemberDetail>("member/" + id, token);
        }

        public bool Insert(MemberEntity entity)
        {
            return Post("member", entity);
        }

        public string Login(string pseudo, string password)
        {
            string tosend = JsonConvert.SerializeObject(new { pseudo = pseudo, pwd = password });
            HttpContent content = new StringContent(tosend, Encoding.UTF8, "application/json");

            using(HttpResponseMessage response = client.PostAsync("member/login", content).Result)
            {
                if (!(response.IsSuccessStatusCode)) throw new HttpRequestException();
                string json  = response.Content.ReadAsStringAsync().Result;
                return json;
            }
        }
       
    }
}
