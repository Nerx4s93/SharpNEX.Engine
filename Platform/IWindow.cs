namespace SharpNEX.Engine.Platform;

public interface IWindow
{
    IntPtr Hwnd { get; }
    string Title { get; set; }
    int Width { get; }
    int Height { get; }
    void Show();
}
