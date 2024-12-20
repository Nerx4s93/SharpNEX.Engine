﻿using System;

namespace SharpNEX.Engine
{
    [Serializable]
    public struct Vector
    {
        public Vector(float X, float Y)
        {
            this.X = X; this.Y = Y;
        }

        public float X;

        public float Y;

        #region Операторы

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }

        public static Vector operator -(Vector a)
        {
            return new Vector(-a.X, -a.Y);
        }

        public static Vector operator *(Vector a, float b)
        {
            return new Vector(a.X * b, a.Y * b);
        }

        public static Vector operator /(Vector a, float b)
        {
            return new Vector(a.X / b, a.Y / b);
        }

        #endregion

        public override string ToString()
        {
            return $"{{ X: {X}; Y: {Y} }}";
        }

        public float GetLenght()
        {
            return (float)Math.Sqrt(X * X + Y * Y);
        }

        public Vector Normalize()
        {
            float length = GetLenght();
            return new Vector(X / length, Y / length);
        }

        public static readonly Vector Zero = new Vector(0, 0);
    }
}
