﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Kingo.MicroServices
{
    /// <summary>
    /// Represents the result of a <see cref="IExecuteQueryTest{TRequest,TResponse}"/> or <see cref="IExecuteQueryTest{TResponse}"/>.
    /// </summary>
    /// <typeparam name="TResponse">Type of the response returned by the query.</typeparam>
    public interface IExecuteQueryResult<out TResponse> : IMicroProcessorTestResult
    {
        /// <summary>
        /// Asserts that the query returned the expected response.
        /// </summary>
        /// <param name="assertion">Delegate to verify the details of the response.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="assertion"/> is <c>null</c>.
        /// </exception>
        void IsExpectedResponse(Action<TResponse> assertion);        
    }
}
