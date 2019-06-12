﻿using System.Collections.Generic;
using System.Reflection;

namespace Kingo.Reflection
{
    internal sealed class MethodAttributeProvider : IMethodAttributeProvider
    {
        private readonly MemberAttributeProvider<MethodInfo> _attributeProvider;

        public MethodAttributeProvider(MethodInfo info)
        {
            _attributeProvider = new MemberAttributeProvider<MethodInfo>(info);
        }

        public MethodInfo Info =>
            _attributeProvider.Member;

        public bool TryGetAttributeOfType<TAttribute>(out TAttribute attribute) where TAttribute : class =>
            _attributeProvider.TryGetAttributeOfType(out attribute);

        public IEnumerable<TAttribute> GetAttributesOfType<TAttribute>() where TAttribute : class =>
            _attributeProvider.GetAttributesOfType<TAttribute>();                        
    }
}
