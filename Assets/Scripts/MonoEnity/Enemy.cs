using EnemyData = Game.Data.Enemy;
using UnityEngine;
using System;
using System.Collections.Generic;

public class Enemy : MonoEntity, IMove, IDamage, IDead
{
    [SerializeField] private SpriteRenderer _sprite;

    public event Action<Vector2> To;
    public event Action Dead;

    private EnemyData _data;

    public MonoEntity Owner => this;

    public bool IsDead => !_data.Parammeters.IsAlive;
    private float timer = 0;

    public override void Init(GameEntity gameEntity)
    {
        base.Init(gameEntity);
        _sprite.sprite = gameEntity.Sprite;
        _data = gameEntity as EnemyData;
    }

    private void Update()
    {
        Think();
    }
    virtual public void Think()
    {
        Debug.DrawRay(transform.position, Vector2.left * _data.Parammeters.AttackRange.Value, Color.red);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.left, _data.Parammeters.AttackRange.Value);
        List<GameObject> targets = new List<GameObject>();
        foreach(var hit in hits)
        {
            if (hit.collider.CompareTag("Obstacle"))
                targets.Add(hit.collider.gameObject);
        }
        if (targets.Count == 0)
        {
            timer = _data.Parammeters.AttackFrequency.Value;
            Move();
        }
        else
        {
            timer -= Time.deltaTime;
            Move(0);
            if(timer <= 0)
            {
                foreach (var item in targets)
                {
                    //item.GetComponent<...>().Get(_data.Parammeters.Damage);
                }
            }
        }
    }
    private void Move(float mult = 1)
    {
        if (_data == null) return;
        To?.Invoke(Vector2.left * _data.Parammeters.Speed.Value * mult);
    }

    public int Get(float Damage)
    {
        _data.Parammeters.Health.Value = -Damage;

        if (!_data.Parammeters.IsAlive)
        {
            Dead?.Invoke();
            return Convert.ToInt32(_data.Parammeters.PriceDead.Value);
        }
        return 0;
    }
}
