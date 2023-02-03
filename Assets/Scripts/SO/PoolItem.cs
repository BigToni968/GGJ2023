using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Game/Data/" + nameof(PoolItem))]
    public class PoolItem : ScriptableObject
    {
        [SerializeField] private MonoEntity monoEntity;
        [SerializeField] private GameEntity gameEntity;

        public GameEntity GetGameEntity => gameEntity;
        public MonoEntity GetMonoEntity => monoEntity;
    }
}