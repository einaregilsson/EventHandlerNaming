using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EinarEgilsson.EventHandlerNaming
{
    public class Transform
    {
        public static readonly Transform NoChange = new Transform(0, "No change", s=>s);
        public static readonly Transform LowerCase = new Transform(1, "lowercase", s=>s.ToLower());
        public static readonly Transform UpperCase = new Transform(2, "UPPERCASE", s=>s.ToUpper());
        public static readonly Transform CamelCase = new Transform(3, "camelCase", s => Char.ToLower(s[0]) + s.Substring(1));
        public static readonly Transform PascalCase = new Transform(4, "PascalCase", s => Char.ToUpper(s[0]) + s.Substring(1));

        public static readonly List<Transform> Values = new List<Transform>
        {
            NoChange, LowerCase, UpperCase, CamelCase, PascalCase                                                            
        };

        private Transform(int value, string displayName, Func<string,string> transform)
        {
            Value = value;
            DisplayName = displayName;
            _transform = transform;
        }

        private readonly Func<string, string> _transform;

        public override string ToString()
        {
            return DisplayName;
        }

        public string Execute(string name)
        {
            return _transform(Regex.Replace(name, "^_*", ""));
        }

        public int Value { get; private set; }
        public string DisplayName { get; private set; }

        public static implicit operator Transform(int value)
        {
            return Values.FirstOrDefault(transform => transform.Value == value);
        }

        public static implicit operator int(Transform value)
        {
            return value.Value;
        }

    }
}
 