using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CityTraveler.Domain.Enums
{
    public abstract class Enumeration
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Guid ValueId { get; set; }

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() * Name.GetHashCode() * ValueId.GetHashCode();
        }

        public override bool Equals(object obj)
        {

            if (!(obj is Enumeration))
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals((obj as Enumeration).Id);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object obj) => Id.CompareTo(((Enumeration)obj).Id);
    }
}
