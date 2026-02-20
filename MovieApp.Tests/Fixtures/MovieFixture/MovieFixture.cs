using MovieApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Tests.Fixtures.MovieFixture
{
    /// <summary>
    /// This is a factory that can provide one or a list of Movie objects.
    /// </summary>
    internal static class MovieFixture
    {
        public static Movie Create()
        {
            return new Movie()
            {
                Genres = new List<Genre>()
            };
        }

        public static IReadOnlyList<Movie> CreateMultiple(int count)
        {
            return Enumerable.Range(0, count).Select(_ => Create()).ToList();
        }

    }
}
