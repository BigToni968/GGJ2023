using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Stats
{
    public virtual bool IsAlive { get; private set; } = true;
    public virtual void Init() { }
}

public class SetStat
{
    public event Action<float> Update;
    public float Value { get; private set; }

    private List<Stat> _statList = new List<Stat>();

    private void Recalculate()
    {
        Value = 0;

        foreach (Stat stat in _statList)
            Value += stat.Value;

        Update?.Invoke(Value);
    }

    public SetStat(Stat stat) => Add(stat);

    public Stat Add(Stat stat)
    {
        if (_statList.Contains(stat)) return null;

        _statList.Add(stat);
        stat.Update += Recalculate;
        Recalculate();

        return stat;
    }

    public void Remove(Stat stat)
    {
        if (!_statList.Contains(stat)) return;

        _statList.Remove(stat);
        stat.Update -= Recalculate;
        Recalculate();
    }
}

public class Stat
{
    public event Action Update;
    public float Value { get => _value; set => ReValue(value); }
    public float Max { get; set; }

    private float _value;

    public Stat(float value) : this(value, value) { }

    public Stat(float value, float max)
    {
        Max = max;
        Value = value;
    }

    private void ReValue(float value)
    {
        _value = Mathf.Clamp(value, 0, Max);
        Update?.Invoke();
    }
}