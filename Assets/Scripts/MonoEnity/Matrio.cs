using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class Matrio : MonoEntity
{
    [SerializeField] private SpriteRenderer _sprite;

    public override void Init(GameEntity gameEntity)
    {
        base.Init(gameEntity);
        _sprite.sprite = gameEntity.Sprite;
    }
}