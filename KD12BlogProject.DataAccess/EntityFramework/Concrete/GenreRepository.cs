﻿using KD12BlogProject.DataAccess.Abstract;
using KD12BlogProject.DataAccess.EntityFramework.Context;
using KD12BlogProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD12BlogProject.DataAccess.EntityFramework.Concrete
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(KD12BlogDbContext kD12BlogDbContext) : base(kD12BlogDbContext)
        {
        }
    }
}
