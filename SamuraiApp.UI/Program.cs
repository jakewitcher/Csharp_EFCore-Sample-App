using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SamuraiApp.UI
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();
        static void Main(string[] args)
        {
            _context.Database.EnsureCreated();

            //InsertNewPkFkGraph();
            //InsertNewOneToOneGraph();
            //AddChildToExistingObject();
            //AddBattles();
            //AddManyToManyWithFks();
            //AddManyToManyWithObjects();
        }

        internal static class DisconnectedMethods
        {
            private static void DisplayState(List<EntityEntry> es, string method)
            {
                Console.WriteLine(method);
                es.ForEach(e => Console.WriteLine(
                    $"{e.Entity.GetType().Name} : {e.State.ToString()}"));
                Console.WriteLine();
            }

            public static void AddGraphAllNew()
            {
                var samuraiGraph = new Samurai { Name = "Julie" };
                samuraiGraph.Quotes.Add(new Quote { Text = "This is new" });
                using (var context = new SamuraiContext())
                {
                    context.Samurais.Add(samuraiGraph);
                    var es = context.ChangeTracker.Entries().ToList();
                    DisplayState(es, "AddGraphAllNew");
                }
            }

            public static void AddGraphWithKeyValues()
            {
                var samuraiGraph = new Samurai { Name = "Julie", Id = 1 };
                samuraiGraph.Quotes.Add(new Quote { Text = "This is not new", Id = 1 });
                using (var context = new SamuraiContext())
                {
                    context.Samurais.Add(samuraiGraph);
                    var es = context.ChangeTracker.Entries().ToList();
                    DisplayState(es, "AddGraphWithKeyValues");
                }
            }

            public static void AttachGraphAllNew()
            {
                var samuraiGraph = new Samurai { Name = "Julie" };
                samuraiGraph.Quotes.Add(new Quote { Text = "This is new" });
                using (var context = new SamuraiContext())
                {
                    context.Samurais.Attach(samuraiGraph);
                    var es = context.ChangeTracker.Entries().ToList();
                    DisplayState(es, "AttachGraphAllNew");
                }
            }

            public static void AttachGraphWithKeys()
            {
                var samuraiGraph = new Samurai { Name = "Julie", Id = 1 };
                samuraiGraph.Quotes.Add(new Quote { Text = "This is not new", Id = 1 });
                using (var context = new SamuraiContext())
                {
                    context.Samurais.Attach(samuraiGraph);
                    var es = context.ChangeTracker.Entries().ToList();
                    DisplayState(es, "AttachGraphWithKeys");
                }
            }

            public static void UpdateGraphAllNew()
            {
                var samuraiGraph = new Samurai { Name = "Julie" };
                samuraiGraph.Quotes.Add(new Quote { Text = "This is new" });
                using (var context = new SamuraiContext())
                {
                    context.Samurais.Update(samuraiGraph);
                    var es = context.ChangeTracker.Entries().ToList();
                    DisplayState(es, "UpdateGraphAllNew");
                }
            }

            public static void UpdateGraphWithKeys()
            {
                var samuraiGraph = new Samurai { Name = "Julie", Id = 1 };
                samuraiGraph.Quotes.Add(new Quote { Text = "This is not new", Id = 1 });
                using (var context = new SamuraiContext())
                {
                    context.Samurais.Update(samuraiGraph);
                    var es = context.ChangeTracker.Entries().ToList();
                    DisplayState(es, "UpdateGraphWithKeys");
                }
            }

            public static void DeleteGraphAllNew()
            {
                var samuraiGraph = new Samurai { Name = "Julie" };
                samuraiGraph.Quotes.Add(new Quote { Text = "This is new" });
                using (var context = new SamuraiContext())
                {
                    context.Samurais.Remove(samuraiGraph);
                    var es = context.ChangeTracker.Entries().ToList();
                    DisplayState(es, "DeleteGraphAllNew");
                }
            }

            public static void DeleteGraphWithKeys()
            {
                var samuraiGraph = new Samurai { Name = "Julie", Id = 1 };
                samuraiGraph.Quotes.Add(new Quote { Text = "This is not new", Id = 1 });
                using (var context = new SamuraiContext())
                {
                    context.Samurais.Remove(samuraiGraph);
                    var es = context.ChangeTracker.Entries().ToList();
                    DisplayState(es, "DeleteGraphWithKeys");
                }
            }

            public static void AddGraphViaEntryAllNew()
            {
                var samuraiGraph = new Samurai { Name = "Julie" };
                samuraiGraph.Quotes.Add(new Quote { Text = "This is new" });
                using (var context = new SamuraiContext())
                {
                    context.Entry(samuraiGraph).State = EntityState.Added;
                    var es = context.ChangeTracker.Entries().ToList();
                    DisplayState(es, "AttachGraphAllNew");
                }
            }

            public static void AddGraphViaEntryWithKeys()
            {
                var samuraiGraph = new Samurai { Name = "Julie", Id = 1 };
                samuraiGraph.Quotes.Add(new Quote { Text = "This is not new", Id = 1 });
                using (var context = new SamuraiContext())
                {
                    context.Entry(samuraiGraph).State = EntityState.Added;
                    var es = context.ChangeTracker.Entries().ToList();
                    DisplayState(es, "AttachGraphWithKeys");
                }
            }
        }

        private static void InsertNewPkFkGraph()
        {
            var samurai = new Samurai
            {
                Name = "Kambei Shimada",
                Quotes = new List<Quote>
                {
                    new Quote {Text = "I've come to save you."}
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void InsertNewOneToOneGraph()
        {
            var samurai = new Samurai { Name = "Shichiroji" };
            samurai.SecretIdentity = new SecretIdentity { RealName = "Julie" };
            _context.Add(samurai);
            _context.SaveChanges();
        }

        private static void AddChildToExistingObject()
        {
            var samurai = _context.Samurais.First();
            samurai.Quotes.Add(new Quote
            {
                Text = "I bet you're happy that I've saved you!"
            });
            _context.SaveChanges();
        }

        private static void AddBattles()
        {
            _context.Battles.AddRange(
                new Battle { Name = "Battle of Shiroyama", StartDate = new DateTime(1877, 9, 24), EndDate = new DateTime(1877, 9, 24) },
                new Battle { Name = "Siege of Osaka", StartDate = new DateTime(1614, 1, 1), EndDate = new DateTime(1615, 12, 31) },
                new Battle { Name = "Boshin War", StartDate = new DateTime(1868, 1, 1), EndDate = new DateTime(1869, 1, 1) }
                );
            _context.SaveChanges();
        }

        private static void AddManyToManyWithFks()
        {
            var sb = new SamuraiBattle { SamuraiId = 23, BattleId = 2 };
            _context.SamuraiBattles.Add(sb);
            _context.SaveChanges();
        }

        private static void AddManyToManyWithObjects()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            var battle = _context.Battles.FirstOrDefault();
            _context.SamuraiBattles.Add(
                new SamuraiBattle { Samurai = samurai, Battle = battle }
            );
            _context.SaveChanges();
        }

        // "Include" must include all of the objects from the set
        private static void EagerLoadWithInclude()
        {
            _context = new SamuraiContext();
            var samuraiWithQuots = _context.Samurais
                .Include(s => s.Quotes).ToList();
        }

        // "ThenInclude" drills down into another layer of related data

        // The Sequence:
        // "Include" grabs the SamuraiBattles associated with the samurais
        // "ThenInclude" goes down into the battles associated with the SamuraiBattles
        private static void EagerLoadManyToManyAkaChildrenGrandchildren()
        {
            _context = new SamuraiContext();
            var samuraiBattlesWith = _context.Samurais
                .Include(s => s.SamuraiBattles)
                .ThenInclude(sb => sb.Battle).ToList();
        }

        private static void EagerLoadWithMultipleBranches()
        {
            _context = new SamuraiContext();
            var samurais = _context.Samurais
                .Include(s => s.SecretIdentity)
                .Include(s => s.Quotes).ToList();
        }

        // constructs an anonymous type. The "new {...}" is an anonymous type, not an actual quote object
        private static void AnonymousTypeViaProjection()
        {
            _context = new SamuraiContext();
            var quotes = _context.Quotes
                .Select(q => new { q.Id, q.Text })
                .ToList();
        }

        private static void AnonymousTypeProjectionWithRelated()
        {
            _context = new SamuraiContext();
            var samurais = _context.Samurais
                .Select(s => new
                {
                    s.Id,
                    s.SecretIdentity.RealName,
                    QuoteCount = s.Quotes.Count
                })
                .ToList();
        }

        // The next two methods are rleated to one another
        // In the first one, you first query a specific samurai and then separately query the quotes related to the samurai
        // The entity context recongizes the relationship and associates them (so that now the quotes "exist" on the samurai object retrieved from memory)
        private static void RelatedObjectsFixUp()
        {
            _context = new SamuraiContext();
            var samurai = _context.Samurais.Find(22);
            var quotes = _context.Quotes.Where(q => q.SamuraiId == 22).ToList();
        }

        // In this one, an anonymous object is created with the Samurai and the samurai quotes
        // So technically both tables of related data have been queried
        // However, if you try to access the quotes from the samurai object, they won't be there
        // because in this context Entity does not recognize the relationship and does not "Fix Up" the related objects
        private static void EagerLoadViaProjectionNotQuite()
        {
            _context = new SamuraiContext();
            var samurais = _context.Samurais
                .Select(s => new { Samurai = s, Quotes = s.Quotes })
                .ToList();
        }

        private static void ExplicitLoad()
        {
            _context = new SamuraiContext();
            var samurai = _context.Samurais.FirstOrDefault();
            _context.Entry(samurai).Collection(s => s.Quotes).Load();
            _context.Entry(samurai).Reference(s => s.SecretIdentity).Load();
        }

        private static void ExplicitLoadWithChildFilter()
        {
            _context = new SamuraiContext();
            var samurai = _context.Samurais.FirstOrDefault();

            _context.Entry(samurai)
                .Collection(s => s.Quotes)
                .Query()
                .Where(q => q.Text.Contains("happy"))
                .Load();
        }

        private static void UsingRelatedDataForFiltersAndMore()
        {
            _context = new SamuraiContext();
            var samurais = _context.Samurais
                .Where(s => s.Quotes.Any(q => q.Text.Contains("happy")))
                .ToList();
        }
    }
}
