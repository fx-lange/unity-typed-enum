using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace TypedEnum 
{
    [Serializable]
    public class TypedEnumBase : IEquatable<TypedEnumBase>, IComparable<TypedEnumBase>
    {
        [SerializeField] protected string _value;
        [SerializeField] protected int _index;

        protected static Dictionary<Type, List<TypedEnumBase>> _cachedAll = new();

        protected TypedEnumBase(int index, string value)
        {
            _index = index;
            _value = value;
        }

        public override string ToString()
        {
            return _value;
        }

        public static List<TypedEnumBase> ListAll(Type type) 
        {
            if (_cachedAll.ContainsKey(type))
            {
                return _cachedAll[type];
            }

            var all = type.GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.DeclaringType == type)
                .Select(p => (TypedEnumBase)p.GetValue(null))
                .OrderBy(p => p._index)
                .ToList();
            _cachedAll[type] = all;
            return all;
        }
        
        public bool Equals(TypedEnumBase other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _index == other._index;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TypedEnumBase)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_index);
        }

        public static bool operator ==(TypedEnumBase left, TypedEnumBase right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TypedEnumBase left, TypedEnumBase right)
        {
            return !Equals(left, right);
        }

        public int CompareTo(TypedEnumBase other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return _index.CompareTo(other._index);
        }
    }
}