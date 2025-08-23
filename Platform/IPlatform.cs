namespace SharpNEX.Engine.Platform;

public interface IPlatform
{
    IWindow CreateWindow(string title, int width, int height);
    IRenderer CreateRenderer(IWindow window);
    IInput CreateInput(IWindow window);
}