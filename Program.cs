using System;
using System.Collections.Generic;
using System.Linq;

namespace NinetiesTV
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Show> shows = DataLoader.GetShows();

            Print("All Names", Names(shows));
            Print("Alphabetical Names", NamesAlphabetically(shows));
            Print("Ordered by Popularity", ShowsByPopularity(shows));
            Print("Shows with an '&'", ShowsWithAmpersand(shows));
            Print("Latest year a show aired", MostRecentYear(shows));
            Print("Average Rating", AverageRating(shows));
            Print("Shows only aired in the 90s", OnlyInNineties(shows));
            Print("Top Three Shows", TopThreeByRating(shows));
            Print("Shows starting with 'The'", TheShows(shows));
            Print("All But the Worst", AllButWorst(shows));
            Print("Shows with Few Episodes", FewEpisodes(shows));
            Print("Shows Sorted By Duration", ShowsByDuration(shows));
            Print("Comedies Sorted By Rating", ComediesByRating(shows));
            Print("More Than One Genre, Sorted by Start", WithMultipleGenresByStartYear(shows));
            Print("Most Episodes", MostEpisodes(shows));
            Print("Ended after 2000", EndedFirstAfterTheMillennium(shows));
            Print("Best Drama", BestDrama(shows));
            Print("All But Best Drama", AllButBestDrama(shows));
            Print("Good Crime Shows", GoodCrimeShows(shows));
            Print("Long-running, Top-rated", FirstLongRunningTopRated(shows));
            Print("Most Words in Title", WordieastName(shows));
            Print("All Names", AllNamesWithCommas(shows));
            Print("All Names with And", AllNamesWithCommasPlsAnd(shows));
        }

        /**************************************************************************************************
         The Exercises

         Above each method listed below, you'll find a comment that describes what the method should do.
         Your task is to write the appropriate LINQ code to make each method return the correct result.

        **************************************************************************************************/

        // 1. Return a list of each of show names.
        static List<string> Names(List<Show> shows)
        {
            return shows.Select(s => s.Name).ToList(); // Looks like this one's already done!
        }

        // 2. Return a list of show names ordered alphabetically.
        static List<string> NamesAlphabetically(List<Show> shows)
        {
            return shows.Select(s => s.Name).OrderBy(s => s).ToList();
        }

        // 3. Return a list of shows ordered by their IMDB Rating with the highest rated show first.
        static List<Show> ShowsByPopularity(List<Show> shows)
        {
            return shows.OrderByDescending(s => s.ImdbRating).ToList();
        }

        // 4. Return a list of shows whose title contains an & character.
        static List<Show> ShowsWithAmpersand(List<Show> shows)
        {
            return shows.Where(s => s.Name.Contains("&") == true).Select(s => s).ToList();
        }

        // 5. Return the most recent year that any of the shows aired.
        static int MostRecentYear(List<Show> shows)
        {
            return shows.Max(s => s.EndYear);
        }

        // 6. Return the average IMDB rating for all the shows.
        static double AverageRating(List<Show> shows)
        {
            return shows.Average(s => s.ImdbRating);
        }

        // 7. Return the shows that started and ended in the 90s.
        // these like to be lower cased here instead of like Sql
        static List<Show> OnlyInNineties(List<Show> shows)
        {
            /*// IEnumerate the list/can also be written as var
            List<Show> OnlyNinety = (
                // look at the shows
                from s in shows
                // while there look at the start and end, if the fit the params
                where s.StartYear >= 1990 && s.EndYear < 2000
                // select it
                select s
                // then return it as a list
                ).ToList();
            // then return to us the query
            return OnlyNinety*/
            return shows.Where(s => s.StartYear >= 1990 && s.EndYear < 2000).ToList();

        }

        // 8. Return the top three highest rated shows.
        static List<Show> TopThreeByRating(List<Show> shows)
        {
            return shows.OrderByDescending(s => s.ImdbRating).Take(3).ToList();
        }

        // 9. Return the shows whose name starts with the word "The".
        static List<Show> TheShows(List<Show> shows)
        {
            return shows.Where(s => s.Name.StartsWith("The")).Select(s => s).ToList();
        }

        // 10. Return all shows except for the lowest rated show.
        static List<Show> AllButWorst(List<Show> shows)
        {
            return shows.OrderBy(s => s.ImdbRating).Skip(1).ToList();
        }

        // 11. Return the names of the shows that had fewer than 100 episodes.
        static List<string> FewEpisodes(List<Show> shows)
        {
           /* // Ienumerate the list
            List<string> episodeCount = (
                // go to the shows
                from s in shows
                    // check out specifically the number of epsiodes 
                where s.EpisodeCount < 100
                // if they have fewer than 100
                select s.Name
                // put them in a list
            ).ToList();
            // return that queried list
            spisodeCount*/
            return shows.Where(s => s.EpisodeCount < 100).Select(s => s.Name).ToList();
        }

        // 12. Return all shows ordered by the number of years on air.
        //     Assume the number of years between the start and end years is the number of years the show was on.
        static List<Show> ShowsByDuration(List<Show> shows)
        {
            /* changing it up and using var
            var showRun = (
                // Look at teh collection of shows
                from s in shows
                // take the shows start year and end year, find the difference
                orderby (s.EndYear - s.StartYear) descending
                // order it by that diff in descending order 
                select s
            ).ToList();
            // List it and return to me
            return showRun*/
            return shows.OrderBy(s => s.EndYear - s.StartYear).ToList();
        }

        // 13. Return the names of the comedy shows sorted by IMDB rating.
        static List<string> ComediesByRating(List<Show> shows)
        {
            /* Var instead of List<>
            var comedy = (
                from s in shows
                    // Look at shows, then look at the genre collection. 
                    // find the show that contains the genre of comedy.
                where s.Genres.Contains("Comedy")
                // order the comedies by rating
                orderby s.ImdbRating descending
                // return the name of the show, instead of the whole show
                select s.Name
            ).ToList();
            return comedy*/
            return shows.OrderByDescending(s => s.ImdbRating).Where(s => s.Genres.Contains("Comedy")).Select(s => s.Name).ToList();
        }

        // 14. Return the shows with more than one genre ordered by their starting year.
        static List<Show> WithMultipleGenresByStartYear(List<Show> shows)
        {
            /*var multiGenre = (
                from s in shows
                // Count the number of genres in the shows
                // If there is more than one hold on to it
                where s.Genres.Count > 1
                // put that in a new collection but by start year
                orderby s.StartYear descending
                // return the whole show object
                select s
            ).ToList();
            // List it and show me!
            return multiGenre*/
            return shows.OrderBy(s => s.StartYear).Where(s => s.Genres.Count() > 1).ToList();
        }

        // 15. Return the show with the most episodes.
        static Show MostEpisodes(List<Show> shows)
        {
            // hold onto the Max method with this. 
            int manyEpsiodes = shows.Max(s => s.EpisodeCount);
            // now that we have the epsiode with the max number
            // we would like to return the first instance from collection
            return shows.FirstOrDefault(s => s.EpisodeCount == manyEpsiodes);
        }

        // 16. Order the shows by their ending year then return the first 
        //     show that ended on or after the year 2000.
        static Show EndedFirstAfterTheMillennium(List<Show> shows)
        {
            // Like above we need to hold on to the Min year a show ended
            int latestYear = shows.Where(s => s.EndYear > 2000).Min(s => s.EndYear);
            // We would like the first instce of a show that ened close to 2000
            return shows.Where(s => s.EndYear > 2000).OrderBy(s => s.EndYear).FirstOrDefault(s => s.EndYear == latestYear);
        }

        // 17. Order the shows by rating (highest first) 
        //     and return the first show with genre of drama.
        static Show BestDrama(List<Show> shows)
        {
            return shows.OrderByDescending(s => s.ImdbRating).FirstOrDefault(s => s.Genres.Contains("Drama"));
        }

        // 18. Return all dramas except for the highest rated.
        static List<Show> AllButBestDrama(List<Show> shows)
        {
            var bestDrama = (
                from s in shows
                // SO we are looking into the shows, but more precisely we want the ones with Drama
                where s.Genres.Contains("Drama")
                // So once we have only the ones with drama order them in descending order
                orderby s.ImdbRating descending
                // select those shows and collect
                select s
                // so now return to us all the best drama but the "bestest"
            ).Skip(1).ToList();
            // Return that collection
            return bestDrama;
        }

        // 19. Return the number of crime shows with an IMDB rating greater than 7.0.
        static int GoodCrimeShows(List<Show> shows)
        {
            // this time we want an integer not a collection of shows
            int crimeShows = (
                from s in shows
                // Like joining tables you we want the intersection of crim shows and ratings of higher than 7
                where s.Genres.Contains("Crime") && s.ImdbRating > 7
                // collect them
                select s
                // count the objects in that list 
            ).Count();
            // tell us the number
            return crimeShows;
        }

        // 20. Return the first show that ran for more than 10 years 
        //     with an IMDB rating of less than 8.0 ordered alphabetically.
        static Show FirstLongRunningTopRated(List<Show> shows)
        {
            var longestRunning =
                // method that iterate through the collection and take the difference in the span of the show
                // while you iterate over that list find the one with an imdbrating of less than 8.
                // where those intersect return that collection, but ordered. 
                shows.Where(s => (s.EndYear - s.StartYear) > 10 && s.ImdbRating < 8).OrderBy(s => s.Name).ToList();
                // although we have a list we just want the one in first first[0] index spot.
                return longestRunning[0];
        }

        // 21. Return the show with the most words in the name.
        static Show WordieastName(List<Show> shows)
        {
            // Since we need the longest name we will use integers
            // method function that gathers the condition of counting characters
            int longTitle = shows.Max(s => s.Name.Count(Char.IsWhiteSpace));
            // return the first instance of the above 
            return shows.FirstOrDefault(s => s.Name.Count(Char.IsWhiteSpace) == longTitle);
        }

        // 22. Return the names of all shows as a single string seperated by a comma and a space.
        static string AllNamesWithCommas(List<Show> shows)
        {
            // We have to convert the list collection into one lomg string
            // Select = JS Map()
            string titles = String.Join(", ", shows.Select(s => s.Name).ToList());
            return titles;
        }

        // 23. Do the same as above, but put the word "and" between the second-to-last and last show name.
        static string AllNamesWithCommasPlsAnd(List<Show> shows)
        {
            // recreate the above, but make it gramatically correct
            // but we want to "Take()" the last one and push an 'and' between
            string titles = String.Join(", ", shows.Select(s => s.Name).Take(shows.Count - 1).ToArray());
            List<string> concat = shows.Select(s => s.Name).Skip(shows.Count - 1).ToList();
            return titles + ", and " + concat[0];
        }


        /**************************************************************************************************
         CHALLENGES

         These challenges are very difficult and may require you to research LINQ methods that we haven't
         talked about. Such as:

            GroupBy()
            SelectMany()
            Count()

        **************************************************************************************************/

        // 1. Return the genres of the shows that started in the 80s.
        // 2. Print a unique list of geners.
        // 3. Print the years 1987 - 2018 along with the number of shows that started in each year (note many years will have zero shows)
        // 4. Assume each episode of a comedy is 22 minutes long and each episode of a show that isn't a comedy is 42 minutes. How long would it take to watch every episode of each show?
        // 5. Assume each show ran each year between its start and end years (which isn't true), which year had the highest average IMDB rating.



        /**************************************************************************************************
         There is no code to write or change below this line, but you might want to read it.
        **************************************************************************************************/

        static void Print(string title, List<Show> shows)
        {
            PrintHeaderText(title);
            foreach (Show show in shows)
            {
                Console.WriteLine(show);
            }

            Console.WriteLine();
        }

        static void Print(string title, List<string> strings)
        {
            PrintHeaderText(title);
            foreach (string str in strings)
            {
                Console.WriteLine(str);
            }

            Console.WriteLine();
        }

        static void Print(string title, Show show)
        {
            PrintHeaderText(title);
            Console.WriteLine(show);
            Console.WriteLine();
        }

        static void Print(string title, string str)
        {
            PrintHeaderText(title);
            Console.WriteLine(str);
            Console.WriteLine();
        }

        static void Print(string title, int number)
        {
            PrintHeaderText(title);
            Console.WriteLine(number);
            Console.WriteLine();
        }

        static void Print(string title, double number)
        {
            PrintHeaderText(title);
            Console.WriteLine(number);
            Console.WriteLine();
        }

        static void PrintHeaderText(string title)
        {
            Console.WriteLine("============================================");
            Console.WriteLine(title);
            Console.WriteLine("--------------------------------------------");
        }
    }
}