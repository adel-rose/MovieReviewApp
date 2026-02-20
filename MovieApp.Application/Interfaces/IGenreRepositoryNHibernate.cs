using MovieApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Application.Interfaces
{
    public interface IGenreRepositoryNHibernate
    {
        void Add(Genre genre);
       IList<Genre> GetAll();

    }
}
