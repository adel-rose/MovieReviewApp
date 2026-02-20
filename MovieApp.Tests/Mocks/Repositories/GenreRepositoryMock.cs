using Moq;
using MovieApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Tests.Mocks.Repositories
{
    internal static class GenreRepositoryMock
    {
        public static Mock<IGenreRepository> GenreReposotoryMocked()
        {
            return new Mock<IGenreRepository>();
        }
    }
}
