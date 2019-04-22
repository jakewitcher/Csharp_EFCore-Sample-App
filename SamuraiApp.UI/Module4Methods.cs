using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SamuraiApp.UI
{
    class Module4Methods
    {
        private static SamuraiContext _context = new SamuraiContext();
        static void ModuleMethods(string[] args)
        {
            //InsertSamurai();
            //InsertMultipleSamurais();
            //SimpleSamuraiQuery();
            //MoreQueries();
            //RetrieveAndUpdateSamurai();
            //RetrieveAndUpdateMultipleSamurais();
            //QueryAndUpdateSamuraiDisconnected();
            //QueryAndUpdateDisconnectedBattle();
            //DeleteMany();
        }

        private static void DeleteMany()
        {
            var samurais = _context.Samurais.Where(s => s.Name.Contains("San"));
            _context.Samurais.RemoveRange(samurais);
            // alternate: _context.RemoveRange(samurais);
            _context.SaveChanges();
        }

        private static void DeleteWhileTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Kambei Shimada");
            _context.Samurais.Remove(samurai);
            // alternates:
            //_context.Remove(samurai);
            //_context.Entry(samurai).State = EntityState.Deleted;
            //_context.Samurais.Remove(_context.Samurais.Find(1));
            _context.SaveChanges();
        }

        private static void QueryAndUpdateDisconnectedBattle()
        {
            var battle = _context.Battles.FirstOrDefault();
            battle.EndDate = new DateTime(1754, 12, 31);
            using (var contextNewAppInstance = new SamuraiContext())
            {
                contextNewAppInstance.Battles.Update(battle);
                contextNewAppInstance.SaveChanges();
            }
        }

        private static void InsertBattle()
        {
            var battle = new Battle();
            battle.Name = "Some Battle";
            battle.StartDate = new DateTime(1754, 6, 15);
            battle.EndDate = new DateTime(1754, 12, 30);

            _context.Battles.Add(battle);
            _context.SaveChanges();
        }

        private static void QueryAndUpdateSamuraiDisconnected()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Kikuchiyo");
            samurai.Name += "San";
            using (var contextNewAppInstance = new SamuraiContext())
            {
                contextNewAppInstance.Samurais.Update(samurai);
                contextNewAppInstance.SaveChanges();
            }
        }

        private static void RetrieveAndUpdateMultipleSamurais()
        {
            var samurais = _context.Samurais.ToList();
            samurais.ForEach(s => s.Name += "San");
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();
        }

        private static void MoreQueries()
        {
            var samurais = _context.Samurais.Where(s => s.Name == "Sampson").ToList();
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
            var name = "Julie";
            Samurai julieSamurais = _context.Samurais.FirstOrDefault(s => s.Name == name);

            // The method "Find" will see if the info is already saved locally before going to the database to get it (I think that's correct)
            var samuraiByKey = _context.Samurais.Find(2);
        }

        private static void SimpleSamuraiQuery()
        {
            var samurais = _context.Samurais.ToList();
            // gathers results into a variable first (rather than foreach (var s in context.Samurai))
            // which will query for each iteration and slow things down
            var query = _context.Samurais;
            foreach (var samurai in query)
            {
                Console.WriteLine(samurai.Name);
            }
        }

        private static void InsertMultipleSamurais()
        {
            //var samurai = new Samurai { Name = "Julie" };
            //var samuraiSammy = new Samurai { Name = "Sampson" };
            // "AddRange" allows for multiple arguments or a list
            //_context.Samurais.AddRange(new List<Samurai> { samurai, samuraiSammy });
            _context.AddRange(
                new Samurai { Name = "Kambei Shimada" },
                new Samurai { Name = "Schichiroji" },
                new Samurai { Name = "Katsushiro Okamoto" },
                new Samurai { Name = "Heihachi Hayashida" },
                new Samurai { Name = "Kyuzo" },
                new Samurai { Name = "Gorobei Katayama" }
                );
            _context.SaveChanges();

        }

        private static void InsertSamurai()
        {
            var samurai = new Samurai { Name = "Sampson" };

            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
    }
}

