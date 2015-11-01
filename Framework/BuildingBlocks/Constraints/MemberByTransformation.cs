﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Kingo.BuildingBlocks.Constraints
{
    internal sealed class MemberByTransformation : Member
    {
        private readonly string[] _parentNames;
        private readonly string _name;
        private readonly Identifier[] _fieldsOrProperties;
        private readonly Type _type;

        internal MemberByTransformation(string[] parentNames, string name, Type type)         
        {
            _parentNames = parentNames;            
            _name = name;
            _fieldsOrProperties = Identifier.EmptyArray;
            _type = type;
        }

        private MemberByTransformation(string[] parentNames, string name, Type type, Identifier[] fieldsOrProperties)
        {
            _parentNames = parentNames;
            _name = name;
            _fieldsOrProperties = fieldsOrProperties;
            _type = type;
        }

        protected override string[] ParentNames
        {
            get { return _parentNames; }
        }

        protected override Identifier[] FieldsOrProperties
        {
            get { return _fieldsOrProperties; }
        }

        public override string Name
        {
            get { return _name; }
        }

        public override Type Type
        {
            get { return _type; }
        }

        internal MemberByTransformation Transform(Type newType, Func<string, string> nameSelector)
        {
            if (newType == _type && nameSelector == null)
            {
                return this;
            }
            return new MemberByTransformation(_parentNames, Rename(nameSelector), newType);
        }

        internal MemberByTransformation Transform(Type newType, Identifier fieldOrProperty)
        {
            return new MemberByTransformation(_parentNames, _name, newType, _fieldsOrProperties.Add(fieldOrProperty));
        }

        private string Rename(Func<string, string> nameSelector)
        {
            return nameSelector == null ? _name : nameSelector.Invoke(_name);
        }

        internal IMember WithValue(object value)
        {
            return new MemberWithValue(this, value);
        }

        internal void WriteErrorMessageTo(IErrorMessageReader reader, IErrorMessage errorMessage)
        {
            foreach (var memberName in FromParentNameToCurrentName())
            {
                reader.Add(errorMessage, memberName);              
            }
        }

        private IEnumerable<string> FromParentNameToCurrentName()
        {
            return FromParentNameToCurrentName(_parentNames.Add(_name));
        }

        private static IEnumerable<string> FromParentNameToCurrentName(string[] names)
        {
            return GetNameCombinations(names).Select(Join);
        }        

        private static IEnumerable<string[]> GetNameCombinations(string[] names)
        {
            for (int index = 0; index < names.Length; index++)
            {
                yield return GetNameCombination(names, index + 1);
            }
        }

        private static string[] GetNameCombination(string[] names, int length)
        {
            var nameCombination = new string[length];

            for (int index = 0; index < length; index++)
            {
                nameCombination[index] = names[index];
            }
            return nameCombination;
        }
    }
}