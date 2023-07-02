using System;
using UnityEngine;

namespace Naukri.MeshHelper
{
    public readonly struct Triangle : IEquatable<Triangle>
    {
        public readonly Vector3 a;
        public readonly Vector3 b;
        public readonly Vector3 c;

        public Triangle(Vector3 a, Vector3 b, Vector3 c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public override bool Equals(object obj)
        {
            return obj is Triangle triangle && Equals(triangle);
        }

        public bool Equals(Triangle other)
        {
            return a.Equals(other.a) &&
                   b.Equals(other.b) &&
                   c.Equals(other.c);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(a, b, c);
        }

        public static bool operator ==(Triangle left, Triangle right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Triangle left, Triangle right)
        {
            return !(left == right);
        }
    }
}