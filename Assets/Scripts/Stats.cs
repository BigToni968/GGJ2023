using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stats
{
    [SerializeField] private float _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _range;

    public SetStat Health { get; private set; }
    public SetStat Speed { get; private set; }
    public SetStat Damage { get; private set; }
    public SetStat Range { get; private set; }

    public void Init()
    {
        Health = new SetStat(new Stat(_health));
        Speed = new SetStat(new Stat(_speed));
        Damage = new SetStat(new Stat(_damage));
        Range = new SetStat(new Stat(_range));
    }

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
        Value = value;
        Max = max;
    }

    private void ReValue(float value)
    {
        _value = Mathf.Clamp(value, 0, Max);
        Update?.Invoke();
    }
}