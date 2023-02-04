using EnemyData = Game.Data.Enemy;
using UnityEngine;
using System;

public class Enemy : MonoEntity, IMove, IDamage, IDead
{
    [SerializeField] private SpriteRenderer _sprite;

    public event Action<Vector2> To;
    public event Action Dead;

    private EnemyData _data;

    public MonoEntity Owner => this;

    public bool IsDead => !_data.Parammeters.IsAlive;

    public override void Init(GameEntity gameEntity)
    {
        base.Init(gameEntity);
        _sprite.sprite = gameEntity.Sprite;
        _data = gameEntity as EnemyData;
    }

    private void Update()
    {

    }

    private void Move()
    {
        if (_data == null) return;
        To?.Invoke(Vector2.left * _data.Parammeters.Speed.Value * Time.deltaTime);

    }

    public int Get(float Damage)
    {
        _data.Parammeters.Health.Add(new Stat(-Damage));

        if (!_data.Parammeters.IsAlive)
        {
            Dead?.Invoke();
            return Convert.ToInt32(_data.Parammeters.PriceDead.Value);
        }
        return 0;
    }
}
