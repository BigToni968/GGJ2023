using UnityEngine;


public class Matrio : MonoEntity, IDamage
{
    [SerializeField] private SpriteRenderer _sprite;

    private Game.Data.Matrio _data;

    public MonoEntity Owner => this;

    public override void Init(GameEntity gameEntity)
    {
        base.Init(gameEntity);
        _sprite.sprite = gameEntity.Sprite;
        _data = gameEntity as Game.Data.Matrio;
    }

    public int Get(float Damage)
    {
        _data.Parammeters.Health.Add(new Stat(-Damage));
        return 0;
    }
}