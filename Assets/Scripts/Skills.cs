using System.Collections;
using UnityEngine;
using Game.Data;
using System;

[Serializable]
public class Skills
{
    [SerializeField] private SetSkills[] _skills;

    public int Count => _skills.Length;
    public IEnumerable Get => _skills;

    public SetSkill SelectSkill { get; private set; }

    public void Init(MonoEntity owner)
    {
        foreach (SetSkills setSkill in Get)
            setSkill.Init(owner);
    }

    public SetSkill Select(int number)
    {
        if (_skills.Length >= number) return null;

        SelectSkill = _skills[number].SetSkill;

        return SelectSkill;
    }

    public SetSkill Select(Type type)
    {
        foreach (SetSkills setSkills in _skills)
            if (setSkills.SetSkill.Skill.GetType() == type)
                SelectSkill = setSkills.SetSkill;

        return SelectSkill;
    }

    [Serializable]
    public class SetSkills
    {
        [SerializeField] private Skill _skill;
        [SerializeField] private int _count;
        [SerializeField] private int _max;
        [SerializeField] private float _delay;

        public SetSkill SetSkill { get; private set; }

        public void Init(MonoEntity owner)
        {
            _skill.Owner = owner;
            SetSkill = new SetSkill(_skill, _count, _max);
        }
    }
}

public class SetSkill
{
    public event Action<int> Count;

    private int _value;
    public int Value { get => _value; set => ReValue(value); }
    public int Max { get; private set; }
    public float Delay { get; private set; }
    public Skill Skill => _skill;

    private Skill _skill;

    private void ReValue(int value)
    {
        _value = Mathf.Clamp(_value + value, 0, Max);
        Count?.Invoke(value);
    }

    public SetSkill(Skill skill, int value) : this(skill, value, value) { }

    public SetSkill(Skill skill, int value, int max) : this(skill, value, max, 1f) { }

    public SetSkill(Skill skill, int value, int max, float delay)
    {
        _skill = skill;
        Max = max;
        Value = value;
        Delay = delay;
    }

    public void Execute()
    {
        if (Value == 0) return;
        Value = -1;

        ShellSkill shell = new GameObject().AddComponent<ShellSkill>();
        shell.name = _skill.Name;

        Skill skill = GameObject.Instantiate(_skill);
        skill.BaseSkill.Sprite = skill.Sprite;
        skill.BaseSkill.Owner = _skill.Owner;
        skill.BaseSkill.Body = shell.gameObject;
        skill.BaseSkill.Stats = FindStats(_skill);
        skill.BaseSkill.Stats.Init();
        skill.Init();

        shell.gameObject.transform.parent = _skill.Owner.transform;
        shell.gameObject.transform.localScale = Vector3.one;
        shell.gameObject.transform.position = _skill.Owner.transform.position;


        shell.Init(skill.Self);
    }

    private Stats FindStats(Skill skill)
    {
        return skill switch
        {
            Whiplash whiplash => whiplash.Parammeters,
            StarFly starFly => starFly.Parammeters,
            _ => null
        };
    }
}


public interface ISkill
{
    public event Action InitCallback;
    public event Action UpdateCallback;
    public event Action DestroyCallbeck;

    public void Init();
    public void Destroy();
    public void Update();
}

public abstract class BaseSkill : ISkill
{
    public event Action InitCallback;
    public event Action UpdateCallback;
    public event Action DestroyCallbeck;

    public GameObject Body { get; set; }
    public Stats Stats { get; set; }
    public MonoEntity Owner { get; set; }
    public Sprite Sprite { get; set; }

    public virtual void Init() => InitCallback?.Invoke();
    public virtual void Destroy() => DestroyCallbeck?.Invoke();
    public virtual void Update() => UpdateCallback?.Invoke();
}

public class ShellSkill : MonoBehaviour
{
    public ISkill Skill => _skill;

    private ISkill _skill;
    public void Init(ISkill skill)
    {
        _skill = skill;
        _skill.Init();
        _skill.DestroyCallbeck += Destroy;
    }

    private void Destroy()
    {
        _skill.DestroyCallbeck -= Destroy;
        Destroy(gameObject);
    }
    private void Update() => _skill.Update();
}