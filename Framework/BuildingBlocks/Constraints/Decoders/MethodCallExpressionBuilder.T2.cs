﻿using System;
using System.Linq.Expressions;

namespace Kingo.BuildingBlocks.Constraints.Decoders
{
    internal sealed class MethodCallExpressionBuilder<T, TValue>
    {
        private readonly MethodCallExpressionFactory<T, TValue> _methodCallExpressionFactory;

        internal MethodCallExpressionBuilder(MemberExpressionDecoder<T, TValue> interpreter, LambdaExpression leftExpression, MethodCallDecorator<T, TValue> methodCallDecorator)
        {
            _methodCallExpressionFactory = new MethodCallExpressionFactory<T, TValue>(interpreter, leftExpression, methodCallDecorator);
        }

        internal Expression<Func<IMemberConstraintBuilder<T, TValue>>> BuildMethodCallExpression()
        {
            return Expression.Lambda<Func<IMemberConstraintBuilder<T, TValue>>>(_methodCallExpressionFactory.CreateMethodCallExpression());
        }                    
    }
}
