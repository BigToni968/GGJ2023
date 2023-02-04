using RootManData = Game.Data.RootMan;
using UnityEngine;
using System;

public class RootMan : MonoEntity, IMove, IRotate, ISkullAttack, IUniqueAttack, IDamage
{
    [SerializeField] private SpriteRenderer _sprite;

    public event Action<Vector2> To;
    public event Action<Vector2> Side;
    public event Action<RootManData> Skull;
    public event Action<Skills.SetSkills> Unique;

    public Vector2 Direction { get; private set; } = Vector2.right;

    public MonoEntity Owner => this;

    private RootManData _data;

    public override void Init(GameEntity gameEntity)
    {
        base.Init(gameEntity);
        _sprite.sprite = gameEntity.Sprite;
        _data = gameEntity as RootManData;
    }

    private void Update()
    {
        if (_data == null) return;

        if (_data.Parammeters.IsAlive)
        {
            Move();

            if (Input.GetButtonDown("Fire1")) Unique?.Invoke(_data.Attack);
            if (Input.GetButtonDown("Fire2")) Skull?.Invoke(_data);
        }
    }

    private void Move()
    {
        Vector2 direction = Vector2.zero;
        if (_data == null) return;
        direction.x = Input.GetAxis("Horizontal") * _data.Parammeters.Speed.Value;
        direction.y = transform.position.y;
        To?.Invoke(direction);

        if (direction.x != 0f)
        {
            Direction = direction.x < 0f ? Vector2.left : Vector2.right;
            Side?.Invoke(Direction);
        }
    }

    public int Get(float Damage)
    {
        _data.Parammeters.Health.Add(new Stat(-Damage));
        return 0;
    }
}