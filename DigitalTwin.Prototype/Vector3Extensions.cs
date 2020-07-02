using System;
using System.Numerics;

namespace DigitalTwin.Prototype
{
    public static class Vector3Extensions
    {
        public static Vector3 Normalize(this Vector3 vector)
        {
            var magnitude = Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2) + Math.Pow(vector.Z, 2));
            return new Vector3(
                Convert.ToSingle(vector.X / magnitude),
                Convert.ToSingle(vector.Y / magnitude),
                Convert.ToSingle(vector.Z / magnitude));
        }

        public static Vector3 Floor(this Vector3 vector)
        {
            return new Vector3(
                Convert.ToSingle(Math.Floor(vector.X)),
                Convert.ToSingle(Math.Floor(vector.Y)),
                Convert.ToSingle(Math.Floor(vector.Z)));
        }
    }
}