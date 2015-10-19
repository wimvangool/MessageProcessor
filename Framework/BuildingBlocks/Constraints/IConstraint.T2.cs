﻿using System;

namespace Kingo.BuildingBlocks.Constraints
{
    /// <summary>
    /// When implemented by a class, represents a constraint that transforms an input value to an output value.
    /// </summary>
    /// <typeparam name="TValueIn">Type in the input value.</typeparam>
    /// <typeparam name="TValueOut">Type of the output value.</typeparam>
    public interface IConstraint<TValueIn, TValueOut> : IConstraint<TValueIn>
    {
        /// <summary>
        /// Creates and returns a logical AND constraint for this and the specified <paramref name="constraint"/>.
        /// </summary>
        /// <param name="constraint">Another constraint.</param>
        /// <returns>A logical AND constraint.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="constraint"/> is <c>null</c>.
        /// </exception>
        IConstraint<TValueIn, TResult> And<TResult>(IConstraint<TValueOut, TResult> constraint);  

        /// <summary>
        /// Determines whether or not the specified <paramref name="valueIn"/> satisfies this constraint.
        /// </summary>
        /// <param name="valueIn">The value to check.</param>
        /// <param name="valueOut">
        /// If this method returns <c>true</c>, will be assigned the output value of this constraint;
        /// otherwise it will be assigned the default value.
        /// </param>
        /// <returns><c>true</c> if the value satisfies this constraint; otherwise <c>false</c>.</returns>
        bool IsSatisfiedBy(TValueIn valueIn, out TValueOut valueOut);

        /// <summary>
        /// Determines whether or not the specified <paramref name="valueIn"/> satisfies this constraint.
        /// </summary>
        /// <param name="valueIn">The value to check.</param>
        /// <param name="errorMessage">
        /// If this method returns <c>true</c>, this parameter will be set to the error of the constraint that failed;
        /// otherwise, it will be <c>null</c>.
        /// </param>
        /// <param name="valueOut">
        /// If this method returns <c>false</c>, will be assigned the output value of this constraint;
        /// otherwise it will be assigned the default value.
        /// </param>
        /// <returns><c>true</c> if the value satisfies this constraint; otherwise <c>false</c>.</returns>
        bool IsNotSatisfiedBy(TValueIn valueIn, out IErrorMessage errorMessage, out TValueOut valueOut);
    }
}
