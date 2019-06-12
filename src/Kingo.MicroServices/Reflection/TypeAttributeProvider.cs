﻿using System;
using System.Collections.Generic;

namespace Kingo.Reflection
{
    internal sealed class TypeAttributeProvider : ITypeAttributeProvider
    { 
        private readonly MemberAttributeProvider<Type> _attributeProvider;

        public TypeAttributeProvider(Type type)
        {           
            _attributeProvider = new MemberAttributeProvider<Type>(type);
        }

        public Type Type =>
            _attributeProvider.Member;

        public bool TryGetAttributeOfType<TAttribute>(out TAttribute attribute) where TAttribute : class =>
            _attributeProvider.TryGetAttributeOfType(out attribute);

        public IEnumerable<TAttribute> GetAttributesOfType<TAttribute>() where TAttribute : class =>
            _attributeProvider.GetAttributesOfType<TAttribute>();
    }
}
