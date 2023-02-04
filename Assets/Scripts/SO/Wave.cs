using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Game/Data/" + nameof(Wave))]
    public class Wave : ScriptableObject
    {
        [SerializeField] private GameEntity gameEntity;
        [SerializeField] private MonoEntity monoEntity;
        [SerializeField] private GameEntity _bossGameEntity;
        [SerializeField] private MonoEntity _bossMonoEntity;
        [SerializeField] private float _doStartTime;
        [SerializeField] private float _betweenSpawnsTime;
        [SerializeField] private int _numberEnemy;

        public GameEntity GetGameEntity => gameEntity;
        public MonoEntity GetMonoEntity => monoEntity;

        public GameEntity GetBossGameEntity => _bossGameEntity;
        public MonoEntity GetBossMonoEntity => _bossMonoEntity;

        public float DoStartTime => _doStartTime;
        public float BetweenSpawnsTime => _betweenSpawnsTime;

        public int NumberEnemy => _numberEnemy;
    }
}