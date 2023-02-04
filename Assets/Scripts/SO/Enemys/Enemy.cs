using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Game/Data/" + nameof(Enemy))]
    public class Enemy : GameEntity
    {
        [SerializeField] private Parameters _parameters;
        [SerializeField] private Skills _skills;

        public Parameters Parammeters => _parameters;
        public Skills Skills => _skills;

        [System.Serializable]
        public class Parameters : Stats
        {
            [SerializeField] private float _health;
            [SerializeField] private float _speed;
            [SerializeField] private int _priceDead;
            public override bool IsAlive => Health.Value > 0f;

            public SetStat Health { get; private set; }
            public SetStat Speed { get; private set; }
            public SetStat PriceDead { get; private set; }

            public override void Init()
            {
                base.Init();
                Health = new SetStat(_health);
                Speed = new SetStat(_speed);
                PriceDead = new SetStat(_priceDead);
            }
        }

        public override void Init()
        {
            base.Init();
            Parammeters.Init();
            _skills.Init(Owner);
        }
    }
}
