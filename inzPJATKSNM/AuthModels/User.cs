using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inzPJATKSNM.AuthModels
{
    public class User
    {
        public int userId { get; set; }
        public String login { get; set; }
        public String haslo { get; set; }
        public String imie { get;set; }
        public String nazwisko { get; set; }
        public String token { get; set; }
        public String nazwaRoli { get; set; }
        public Rola rola { get; set; }
        public List<Uprawnienia> uprawnieniaUsera { get; set; }

    }
}