using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaHogar.Util
{
    public readonly struct UFloat
    {
        private readonly float _value;

        public UFloat(float value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "El valor no puede ser negativo.");
            }
            _value = value;
        }

        public readonly float Value => _value;

        public static implicit operator float(UFloat nonNegativeFloat) => nonNegativeFloat._value;
        public static implicit operator UFloat(float value) => new(value);

        public static bool operator <(UFloat a, UFloat b)
        {
            return a._value < b._value;
        }

        public static bool operator >(UFloat a, UFloat b)
        {
            return a._value > b._value;
        }

        public static bool operator ==(UFloat a, UFloat b)
        {
            return a._value == b._value;
        }

        public static bool operator !=(UFloat a, UFloat b)
        {
            return a._value != b._value;
        }

        public static bool operator <=(UFloat a, UFloat b)
        {
            return a._value <= b._value;
        }

        public static bool operator >=(UFloat a, UFloat b)
        {
            return a._value >= b._value;
        }

        public override bool Equals(object? a)
        {
            return !(a is UFloat) ? false : this == (UFloat)a;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}