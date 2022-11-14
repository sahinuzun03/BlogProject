using KD12BlogProject.Core.Abstract.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KD12BlogProject.Core.Abstract.DataAccess
{
    public interface IBaseRepository<T> where T : IBaseEntity
    {
        Task Create(T entity);
        Task Delete(T entity);
        Task Update(T entity);

        Task<T> GetDefault(Expression<Func<T, bool>> expression);

        Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression);

        Task<bool> Any(Expression<Func<T, bool>> exception);


        Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
          Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);


        Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> select,
            Expression<Func<T, bool>> where,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);


        //Func<T(tanımlanacakTip),T(geridönecekTip)>()
        //--Aşağıda Örnek Bir Func yapısının tanımı vardır.İleride Service'lerimizi yaparken detaylarını burada inceleyeceğiz.//
        //static void Main(string[] args)
        //{
        //    Func<int, int> Toplam = ToplamMetodu;
        //    Console.Write(Toplam(5));
        //    Console.Read();
        //}

        //static int ToplamMetodu(int sayi)
        //{
        //    int Toplam = 0;
        //    for (int i = 0; i <= sayi; i++)
        //    {
        //        Toplam += i;
        //    }
        //    return Toplam;
        //}
    }
}
