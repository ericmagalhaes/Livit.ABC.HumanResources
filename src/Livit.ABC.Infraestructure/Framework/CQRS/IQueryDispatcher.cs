using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleInjector;

namespace Livit.ABC.Infraestructure.Framework.CQRS
{
    /// <summary>
    /// Passed around to all allow dispatching a query and to be mocked by unit tests
    /// </summary>
    public interface IQueryDispatcher
    {
        /// <summary>
        /// Dispatches a query and retrieves a query result
        /// </summary>
        /// <typeparam name="TParameter">Query to execute type</typeparam>
        /// <typeparam name="TResult">Query Result to get back type</typeparam>
        /// <param name="query">Query to execute</param>
        /// <returns>Query Result to get back</returns>
        TResult Dispatch<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IQueryResult;
    }
    public interface IQuery
    {

    }
    public interface IQueryResult
    {
    }
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly Container _serviceProvider;

        public QueryDispatcher(Container serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }
            _serviceProvider = serviceProvider;
        }

        public TResult Dispatch<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IQueryResult
        {
            var handler = _serviceProvider.GetInstance<IQueryHandler<TParameter, TResult>>();
            return handler.Retrieve(query);
        }

    }
    /// <summary>
    /// Base interface for query handlers
    /// </summary>
    /// <typeparam name="TParameter">Query type</typeparam>
    /// <typeparam name="TResult">Query Result type</typeparam>
    public interface IQueryHandler<in TParameter, out TResult> where TResult : IQueryResult where TParameter : IQuery
    {
        /// <summary>
        /// Retrieve a query result from a query
        /// </summary>
        /// <param name="query">Query</param>
        /// <returns>Retrieve Query Result</returns>
        TResult Retrieve(TParameter query);
    }
}
