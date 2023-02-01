using RootManData = Game.Data.RootMan;
using UnityEngine;
using System;

public class RootMan : MonoEntity, IMove, IRotate
{
    [SerializeField] private SpriteRenderer _sprite;

    public event Action<Vector2> To;
    public event Action<Vector2> Side;

    private RootManData _data;

    public override void Init(GameEntity gameEntity)
    {
        base.Init(gameEntity);
        _sprite.sprite = gameEntity.Sprite;
        _data = gameEntity as RootManData;
    }

    private void FixedUpdate()
    {
        if (_data.Parammeters.IsAlive)
        {
            Move();

            if (Input.GetMouseButton(0))
            {
                foreach (Skills.SetSkills setSkill in _data.Skills.Get)
                {
                    setSkill.Skill.Execute();
                    return;
                }
            }
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
            Side?.Invoke(direction.x < 0f ? Vector2.left : Vector2.right);
    }
}