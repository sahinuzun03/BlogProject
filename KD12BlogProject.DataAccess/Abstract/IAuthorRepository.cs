using KD12BlogProject.Core.Abstract.DataAccess;
using KD12BlogProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD12BlogProject.DataAccess.Abstract
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
    }
}
