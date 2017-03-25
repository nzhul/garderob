using App.Data.Repositories;
using App.Models;
using App.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data
{
    public interface IUoWData
    {
        IRepository<ApplicationUser> Users { get; }
        IRepository<Page> Pages { get; }

        int SaveChanges();
    }
}
