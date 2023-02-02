using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Game/Data/" + nameof(RootMan))]
    public class RootMan : GameEntity
    {
        [SerializeField] private Parameters _parameters;
        [SerializeField] private Skills.SetSkills _attack;
        [SerializeField] private Skills _skills;

        public Parameters Parammeters => _parameters;
        public Skills Skills => _skills;

        public Skills.SetSkills Attack => _attack;

        [System.Serializable]
        public class Parameters : Stats
        {
            [SerializeField] private int _score;
            [SerializeField] private float _health;
            [SerializeField] private float _speed;

            public override bool IsAlive => Health.Value > 0f;

            public SetStat Health { get; private set; }
            public SetStat Speed { get; private set; }
            public SetStat Score { get; private set; }

            public override void Init()
            {
                base.Init();
                Health = new SetStat(new Stat(_health));
                Speed = new SetStat(new Stat(_speed));
                Score = new SetStat(new Stat(_score));
            }
        }

        public override void Init()
        {
            base.Init();
            Parammeters.Init();
            _skills.Init(Owner);
            _attack.Init(Owner);
        }
    }
}