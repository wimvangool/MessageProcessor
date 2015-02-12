﻿namespace System.ComponentModel.Server.Modules
{
    /// <summary>
    /// This type is used to support implicit type conversion from a <see cref="Func{T, S}" /> to a
    /// <see cref="IQueryPipelineFactory{TMessageIn, TMessageOut}" />.
    /// </summary>
    /// <typeparam name="TMessageIn">Type of the messages that go into the pipeline.</typeparam>
    /// <typeparam name="TMessageOut">Type of the messages that come out of the pipeline.</typeparam>
    public sealed class QueryPipelineFactoryDecorator<TMessageIn, TMessageOut> : IQueryPipelineFactory<TMessageIn, TMessageOut> where TMessageIn : class        
    {
        private readonly Func<IQuery<TMessageIn, TMessageOut>, IQuery<TMessageIn, TMessageOut>> _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryPipelineFactoryDecorator{TMessageIn, TMessageOut}" /> class.
        /// </summary>
        /// <param name="factory">The factory to invoke.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="factory"/> is <c>null</c>.
        /// </exception>
        public QueryPipelineFactoryDecorator(Func<IQuery<TMessageIn, TMessageOut>, IQuery<TMessageIn, TMessageOut>> factory)
        {            
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }
            _factory = factory;
        }

        IQuery<TMessageIn, TMessageOut> IQueryPipelineFactory<TMessageIn, TMessageOut>.CreateQueryPipeline(IQuery<TMessageIn, TMessageOut> query)
        {
            return _factory.Invoke(query);
        }

        /// <summary>
        /// Implicitly converts <paramref name="factory"/> to an instance of <see cref="QueryPipelineFactoryDecorator{TMessageIn, TMessageOut}" />.
        /// </summary>
        /// <param name="factory">The value to convert.</param>
        /// <returns>
        /// <c>null</c> if <paramref name="factory"/> is <c>null</c>;
        /// otherwise, a new instance of <see cref="QueryPipelineFactoryDecorator{TMessageIn, TMessageOut}" />.
        /// </returns>
        public static implicit operator QueryPipelineFactoryDecorator<TMessageIn, TMessageOut>(Func<IQuery<TMessageIn, TMessageOut>, IQuery<TMessageIn, TMessageOut>> factory)
        {
            return factory == null ? null : new QueryPipelineFactoryDecorator<TMessageIn, TMessageOut>(factory);
        }
    }
}