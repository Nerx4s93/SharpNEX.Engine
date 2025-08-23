namespace SharpNEX.Engine.Platform.Windows;

public class WindowsForm : IPlatform
{
    public IWindow CreateWindow(string title, int width, int height) 
        => new WinWindow(title, width, height);

    public IRenderer CreateRenderer(IWindow window)
    {
        throw new NotImplementedException();
    }

    public IInput CreateInput()
    {
        throw new NotImplementedException();
    }
}