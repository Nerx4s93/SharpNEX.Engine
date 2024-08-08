﻿using System;

namespace SharpNEX.Engine
{
    public struct Rotation
    {
        public Rotation(float Angle)
        {
            while (Angle >= 360)
            {
                Angle -= 360;
            }

            this.Angle = Angle;
        }

        public float Angle;

        public static Rotation operator +(Rotation a, Rotation b)
        {
            float result = (float)Math.Round(a.Angle + b.Angle, 2);
            return new Rotation(result);
        }

        public static Rotation operator -(Rotation a, Rotation b)
        {
            float result = (float)Math.Round(a.Angle - b.Angle, 2);
            return new Rotation(result);
        }
        }
    }
}
