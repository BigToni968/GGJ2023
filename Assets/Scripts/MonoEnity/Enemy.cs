using EnemyData = Game.Data.Enemy;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;
using System;

public class Enemy : MonoEntity, IMove, IDamage, IDead, IAnimate
{
    [SerializeField] private SpriteRenderer _sprite;

    public event Action<Vector2> To;
    public event Action Dead;
    public event Action<AnimaionType> Play;
    public event Action<int> PlaySkill;

    private EnemyData _data;
    private ModeEnemy _mode;
    private (IDamage Damage, IDead Dead) _target;

    public MonoEntity Owner => this;

    public bool IsDead => !_data.Parammeters.IsAlive;

    public override void Init(GameEntity gameEntity)
    {
        base.Init(gameEntity);
        _sprite.sprite = gameEntity.Sprite;
        _data = gameEntity as EnemyData;
        _mode = ModeEnemy.Move;
    }
    private void LateUpdate()
    {
        Think();
    }

    public virtual void Think()
    {
        switch (_mode)
        {
            case ModeEnemy.Move:
                Move(Vector2.left);
                Play(AnimaionType.Move);
                if (_target.Damage == null) _mode = ModeEnemy.FindTarget;
                break;

            case ModeEnemy.FindTarget:
                _target.Damage = FindTarget();
                _mode = _target.Damage != null ? ModeEnemy.Attack : ModeEnemy.Move;
                break;

            case ModeEnemy.Attack:
                PlaySkill(1);
                Move(Vector2.zero);
                Attack();
                _mode = ModeEnemy.Battle;
                break;

            case ModeEnemy.Battle:
                if (_target.Damage == null) _mode = ModeEnemy.FindTarget;
                break;
        }
    }

    public virtual async void Attack()
    {

        _target.Dead = _target.Damage.Owner as IDead;

        while (!_target.Dead.IsDead || _data.Parammeters.IsAlive)
        {
            _target.Damage.Get(_data.Parammeters.Damage.Value);
            await Task.Delay(Convert.ToInt32(1000 * _data.Parammeters.AttackFrequency.Value));
        }

        _target = (null, null);
    }

    public virtual IDamage FindTarget()
    {
        Debug.DrawRay(transform.position, Vector2.left * _data.Parammeters.AttackRange.Value, Color.red);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.left, _data.Parammeters.AttackRange.Value);

        if (hits.Length > 1)
            foreach (RaycastHit2D ray in hits)
                if (ray.collider.TryGetComponent(out IDamage enemy))
                    if (enemy.Owner.GameEntity.GEType == GEType.Hero)
                        return enemy;

        return null;
    }

    private void Move(Vector2 direction)
    {
        if (_data == null) return;
        To?.Invoke(direction * _data.Parammeters.Speed.Value * Time.deltaTime);
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

public enum ModeEnemy
{
    Move,
    Attack,
    FindTarget,
    Battle
}