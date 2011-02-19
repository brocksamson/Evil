using System;

namespace Evil.Common
{
    public class Position : IEquatable<Position>
    {
        protected Position()
        {
        }

        public Position(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public virtual double Longitude { get; private set; }
        public virtual double Latitude { get; private set; }

        public bool Equals(Position obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.Longitude == Longitude && obj.Latitude == Latitude;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Position)) return false;
            return Equals((Position) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Longitude.GetHashCode()*397) ^ Latitude.GetHashCode();
            }
        }

        public static bool operator ==(Position left, Position right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !Equals(left, right);
        }
    }
}