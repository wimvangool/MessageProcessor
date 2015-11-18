﻿using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Kingo.BuildingBlocks.Constraints.Decoders
{
    internal sealed class NullAppender<T, TValue> : MethodCallDecorator<T, TValue>
    {
        internal override IEnumerable<string> MethodCalls()
        {
            return Enumerable.Empty<string>();
        }

        public override MethodCallExpression Decorate(MethodCallExpression expression)
        {
            return expression;
        }

        public override MethodCallDecorator<T, TValue> Append(MethodCallDecorator<T, TValue> appender)
        {
            return appender;
        }        
    }
}
