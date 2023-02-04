using System;

public abstract class Stats
{
    public virtual bool IsAlive { get; private set; } = true;
    public virtual void Init() { }
}

public class SetStat
{
    public event Action<float> Update;
    private float _value;
    public float Value { get => _value; set => Sum(value); }

    public SetStat(float value) => Value = value;

    private void Sum(float value)
    {
        _value += value;
        Update?.Invoke(_value);
    }
}