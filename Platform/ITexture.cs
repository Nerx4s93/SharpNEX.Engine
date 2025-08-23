namespace SharpNEX.Engine.Platform;

public interface ITexture : IDisposable
{
    int Width { get; }
    int Height { get; }
}