using System;
using System.Linq;
using System.Xml.Linq;

namespace WesterosLinQ
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument doc = XDocument.Load("war-of-westeros.xml");


            //Console.WriteLine(doc);
            doc.Descendants("name")
               .Select(x => x.Value)
               .ToConsole("ALL");


            //Q1: How many houses participated?

            var q1 = doc.Descendants("house")
                        .Select(x => x.Value)
                        .Distinct();
            Console.WriteLine("Count q1: " + q1.Count());
            q1.ToConsole("Q1");


            //Q2: List the battles with the "ambush" type

            string ambush = "ambush";
            var q2 = from battleNode in doc.Descendants("battle")
                     where battleNode.Element("type")?.Value == ambush
                     select battleNode.Element("name")?.Value;
            q2.ToConsole("Q2");


            //Q3: How many battles were won by the defenders and had a major capture?

            var q3 = from battleNode in doc.Descendants("battle")
                     where battleNode.Element("outcome").Value == "defender" &&
                     int.Parse(battleNode.Element("majorcapture").Value) > 0
                     select battleNode.Element("name")?.Value;
            Console.WriteLine("Count Q3: " + q3.Count());
            q3.ToConsole("Q3");


            //Q4: How many battles resulted in the Stark house's victory?

            var q4 = from battleNode in doc.Descendants("battle")
                     let whoWon = battleNode.Element("outcome").Value
                     let winnerHouses = battleNode.Element(whoWon).Elements("house")
                     .Select(x => x.Value)
                     where winnerHouses.Contains("Stark")
                     select new
                     {
                         BattleName = battleNode.Element("name").Value,
                         outcome = whoWon,
                         Houses = string.Join("; ",winnerHouses)
                     };
            Console.WriteLine("Q4: " + q4.Count());
            q4.ToConsole("Q4");


            //Q5: Which battles had more than 2 participating houses?

            var q5 = from battleNode in doc.Descendants("battle")
                     let attackers = battleNode.Element("attacker").Elements("house").Count()
                     let defenders = battleNode.Element("defender").Elements("house").Count()
                     let sumOfHouses1 = attackers + defenders
                     let sumOfHouses2 = battleNode.Descendants("house")
                                                  .Select(x => x.Value)
                                                  .Distinct()
                                                  .Count()
                     where sumOfHouses2 > 2
                     orderby sumOfHouses2 descending
                     select new
                     {
                         BattleName = battleNode.Element("name").Value,
                         NumHouses = sumOfHouses2,
                         Region = battleNode.Element("region").Value
                     };
            q5.ToConsole("Q5");


            //Q6: Which are the 3 most violent regions?

            var q6 = from battleNode in doc.Descendants("battle")
                     group battleNode by battleNode.Element("region").Value into grp
                     let battleCount = grp.Count()
                     orderby battleCount descending
                     select new
                     {
                         Region = grp.Key,
                         Count = battleCount
                     };
            //It wont look for ties
            q6.Take(3).ToConsole("Q6");
            
            //It displays ties even if it results in more than the desired number of outcomes.
            var top3Counts = q6.Select(x => x.Count).Distinct().Take(3);
            q6.Where(grp => top3Counts.Contains(grp.Count)).ToConsole("Q6b");


            //Q7: Which is the most violent region? 


            //It wont look for ties
            Console.WriteLine("Q7: " + q6.FirstOrDefault());

            //It displays ties even if it results in more than the desired number of outcomes.
            var mostViolent = q6.Select(x => x.Count).Distinct().Take(1);
            q6.Where(grp => mostViolent.Contains(grp.Count)).ToConsole("Q7b");


            //Q8: In the 3 most violent region, which battles had more than 2 participating houses?

            var q8 = from battle in q5
                     join region in q6.Take(3) on battle.Region equals region.Region
                     select new
                     {
                         battle,
                         region
                     };
            q8.ToConsole("Q8");


            //Q9: List the houses ordered descending by the number of battles won

            var explaination =
                from houseNode in doc.Descendants("house")    //this x.Parent.Parent looks bad, dont use it
                where houseNode.Parent.Name == houseNode.Parent.Parent.Element("outcome").Value
                                    //also this x.Name usage is not advised, because it
                                    //can lead to problems
                select new { };


            var q9 = from battleNode in doc.Descendants("battle")
                     let whoWon = battleNode.Element("outcome").Value
                     let winnerHouses = battleNode.Element(whoWon).Elements("house").Select(x => x.Value)
                     from house in winnerHouses
                     group house by house into grp
                     let winCount = grp.Count()
                     orderby winCount descending
                     select new
                     {
                         House = grp.Key,
                         winCount
                     };
            q9.ToConsole("Q9");


        Console.ReadLine();

        }
    }
}