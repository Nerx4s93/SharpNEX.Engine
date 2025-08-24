namespace SharpNEX.Engine.Platform;

public interface IRenderer
{
    void Init(IntPtr hwnd, int width, int height);

    void BeginFrame();
    void EndFrame();
    void Clear(float r, float g, float b, float a);

    ITexture CreateTexture(string path);
    ITexture CreateTexture(int width, int height, byte[] data);
    void DrawTexture(ITexture texture, float x, float y, float width, float height);
}