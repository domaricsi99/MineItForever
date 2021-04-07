namespace GameRepository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// base repo interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
        where T : class
    {
        /// <summary>
        /// add.
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// delete.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// update.
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// get one.
        /// </summary>
        /// <param name="shortName"></param>
        /// <returns>asd.</returns>
        T GetOne(string shortName);

        /// <summary>
        /// get all.
        /// </summary>
        /// <returns>asd.</returns>
        IQueryable<T> GetAll();
    }
}
