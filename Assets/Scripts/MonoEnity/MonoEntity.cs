using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoEntity : MonoBehaviour
{
    public GameEntity GameEntity { get; private set; }
    public virtual void Init(GameEntity gameEntity) => GameEntity = gameEntity;
}