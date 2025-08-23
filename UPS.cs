namespace SharpNEX.Engine;

internal class UPS
{
    private int _sum = 0;
    private readonly Queue<int> _ticks = new Queue<int>();

    public void AddTik(int tik)
    {
        _ticks.Enqueue(tik);
        _sum += tik;

        while (_sum >= 1000)
        {
            _sum -= _ticks.Dequeue();
        }
    }

    public int GetUPS()
    {
        return _ticks.Count;
    }
}
