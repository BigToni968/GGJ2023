using UnityEngine;

public abstract class GameEntity : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private GEType _GEType;

    public Sprite Sprite => _sprite;
    public GEType GEType => _GEType;

    public MonoEntity Owner { get; set; }

    public virtual void Init() { }
}

public enum GEType
{
    None,
    Matrio,
    Hero,
    Enemy
}