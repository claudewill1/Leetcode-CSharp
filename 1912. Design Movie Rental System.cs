///
/* You have a movie renting company consisting of n shops. You want to implement a renting system that supports searching for, booking, and returning movies. The system should also support generating a report of the currently rented movies.

Each movie is given as a 2D integer array entries where entries[i] = [shopi, moviei, pricei] indicates that there is a copy of movie moviei at shop shopi with a rental price of pricei. Each shop carries at most one copy of a movie moviei.

The system should support the following functions:

Search: Finds the cheapest 5 shops that have an unrented copy of a given movie. The shops should be sorted by price in ascending order, and in case of a tie, the one with the smaller shopi should appear first. If there are less than 5 matching shops, then all of them should be returned. If no shop has an unrented copy, then an empty list should be returned.
Rent: Rents an unrented copy of a given movie from a given shop.
Drop: Drops off a previously rented copy of a given movie at a given shop.
Report: Returns the cheapest 5 rented movies (possibly of the same movie ID) as a 2D list res where res[j] = [shopj, moviej] describes that the jth cheapest rented movie moviej was rented from the shop shopj. The movies in res should be sorted by price in ascending order, and in case of a tie, the one with the smaller shopj should appear first, and if there is still tie, the one with the smaller moviej should appear first. If there are fewer than 5 rented movies, then all of them should be returned. If no movies are currently being rented, then an empty list should be returned.
Implement the MovieRentingSystem class:

MovieRentingSystem(int n, int[][] entries) Initializes the MovieRentingSystem object with n shops and the movies in entries.
List<Integer> search(int movie) Returns a list of shops that have an unrented copy of the given movie as described above.
void rent(int shop, int movie) Rents the given movie from the given shop.
void drop(int shop, int movie) Drops off a previously rented movie at the given shop.
List<List<Integer>> report() Returns a list of cheapest rented movies as described above.
Note: The test cases will be generated such that rent will only be called if the shop has an unrented copy of the movie, and drop will only be called if the shop had previously rented out the movie.

 

Example 1:

Input
["MovieRentingSystem", "search", "rent", "rent", "report", "drop", "search"]
[[3, [[0, 1, 5], [0, 2, 6], [0, 3, 7], [1, 1, 4], [1, 2, 7], [2, 1, 5]]], [1], [0, 1], [1, 2], [], [1, 2], [2]]
Output
[null, [1, 0, 2], null, null, [[0, 1], [1, 2]], null, [0, 1]]

Explanation
MovieRentingSystem movieRentingSystem = new MovieRentingSystem(3, [[0, 1, 5], [0, 2, 6], [0, 3, 7], [1, 1, 4], [1, 2, 7], [2, 1, 5]]);
movieRentingSystem.search(1);  // return [1, 0, 2], Movies of ID 1 are unrented at shops 1, 0, and 2. Shop 1 is cheapest; shop 0 and 2 are the same price, so order by shop number.
movieRentingSystem.rent(0, 1); // Rent movie 1 from shop 0. Unrented movies at shop 0 are now [2,3].
movieRentingSystem.rent(1, 2); // Rent movie 2 from shop 1. Unrented movies at shop 1 are now [1].
movieRentingSystem.report();   // return [[0, 1], [1, 2]]. Movie 1 from shop 0 is cheapest, followed by movie 2 from shop 1.
movieRentingSystem.drop(1, 2); // Drop off movie 2 at shop 1. Unrented movies at shop 1 are now [1,2].
movieRentingSystem.search(2);  // return [0, 1]. Movies of ID 2 are unrented at shops 0 and 1. Shop 0 is cheapest, followed by shop 1.
 

Constraints:

1 <= n <= 3 * 105
1 <= entries.length <= 105
0 <= shopi < n
1 <= moviei, pricei <= 104
Each shop carries at most one copy of a movie moviei.
At most 105 calls in total will be made to search, rent, drop and report.
 
*/
using System;
using System.Collections.Generic;

public class MovieRentingSystem {
    private readonly Dictionary<ValueTuple<int, int>, int> priceMap;
    private readonly Dictionary<int, SortedSet<(int price, int shop)>> availableMovies;
    private readonly SortedSet<(int price, int shop, int movie)> rentedMovies;
    public MovieRentingSystem(int n, int[][] entries) {
        priceMap = new Dictionary<ValueTuple<int, int>, int>();
        availableMovies = new Dictionary<int, SortedSet<ValueTuple<int, int>>>();
        // Customer Comparer: rented movies sorted by price -> shop -> movie
        rentedMovies = new SortedSet<(int, int, int)>(Comparer<(int, int, int)>.Create((a, b) => {
            if (a.Item1 != b.Item1) return a.Item1.CompareTo(b.Item1); // sort by price
            if (a.Item2 != b.Item2) return a.Item2.CompareTo(b.Item2); // then shop
            return a.Item3.CompareTo(b.Item3); // then movie
        }));

        // Build initial data structures for entries
        foreach (var entry in entries){
            int shop = entry[0], movie = entry[1], price = entry[2];
            priceMap[(shop, movie)] = price;

            // Ensure movie has its own available set
            if(!availableMovies.ContainsKey(movie)){
                // Sort available shops by price -> shop
                availableMovies[movie] = new SortedSet<(int,int)>(Comparer<(int,int)>.Create((a, b) => {
                    if(a.Item1 != b.Item1) return a.Item1.CompareTo(b.Item1);
                    return a.Item2.CompareTo(b.Item2);
                }));
            }
            // Add movie copy to available pool
            availableMovies[movie].Add((price, shop));
        }
    }
    // Search: return up to 5 cheapest shops for an unrented copy of 'movie'.
    public IList<int> Search(int movie) {
        var result = new List<int>();
        if(!availableMovies.ContainsKey(movie))
            return result;

        int count = 0;
        foreach (var entry in availableMovies[movie])
        {
            result.Add(entry.Item2); // shop
            count++;
            if (count == 5) break;
        }
        return result;
    }
    // Rent: remove from available pool, add to rented pool.
    public void Rent(int shop, int movie) {
        int price = priceMap[(shop,movie)];
        availableMovies[movie].Remove((price,shop));
        rentedMovies.Add((price, shop, movie));
    }
    // Drop: remove from rented pool, add back to available pool.
    public void Drop(int shop, int movie) {
        int price = priceMap[(shop, movie)];
        rentedMovies.Remove((price,shop,movie));
        availableMovies[movie].Add((price, shop));
    }
    // Report: return up to 5 cheapest rented movies globally.
    public IList<IList<int>> Report() {
        return rentedMovies.Take(5)
            .Select(x => (IList<int>)new List<int> { x.Item2, x.Item3})
            .ToList();
    }
}

/**
 * Your MovieRentingSystem object will be instantiated and called as such:
 * MovieRentingSystem obj = new MovieRentingSystem(n, entries);
 * IList<int> param_1 = obj.Search(movie);
 * obj.Rent(shop,movie);
 * obj.Drop(shop,movie);
 * IList<IList<int>> param_4 = obj.Report();
 */