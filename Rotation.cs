using SharpNEX.Engine.EngineMath;

namespace SharpNEX.Engine;

[Serializable]
public struct Rotation
{
    public readonly float Angle;

    public Rotation(float angle)
    {
        angle %= 360;
        Angle = angle;
    }

    public float Radians => Trigonometry.AngleDegreesToRadians(Angle);

    #region Операторы

    public static Rotation operator +(Rotation a, Rotation b)
    {
        var result = a.Angle + b.Angle;
        return new Rotation(result);
    }

    public static Rotation operator -(Rotation a, Rotation b)
    {
        var result = a.Angle - b.Angle;
        return new Rotation(result);
    }

    public static Rotation operator +(Rotation a, float b)
    {
        var result = a.Angle + b;
        return new Rotation(result);
    }

    public static Rotation operator -(Rotation a, float b)
    {
        var result = a.Angle - b;
        return new Rotation(result);
    }

    #endregion
}