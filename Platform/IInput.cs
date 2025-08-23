namespace SharpNEX.Engine.Platform;

public interface IInput
{
    // клавиатура
    void Update();
    bool IsKeyPressed(Keys key);
    bool IsKeyDown(Keys key);
    bool IsKeyUp(Keys key);

    // мышь
    (int X, int Y) GetMousePosition();
    bool IsMouseButtonPressed(int button);
    int GetMouseWheelDelta();

    // джойстик
    float GetAxis(string axisName);
}