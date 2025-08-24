namespace SharpNEX.Engine.Platform;

public interface IWindow
{
    string Title { get; set; }
    int Width { get; }
    int Height { get; }
    void Show();
}
