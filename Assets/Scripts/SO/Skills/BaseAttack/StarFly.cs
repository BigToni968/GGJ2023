using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Game/Data/Skills/Attack/" + nameof(StarFly))]
    public class StarFly : Skill
    {
        [SerializeField] private Parameters _parameters;

        public override BaseSkill BaseSkill { get; set; } = new Gameplay.StarFly();

        public Parameters Parammeters => _parameters;

        [System.Serializable]
        public class Parameters : Stats
        {
            [SerializeField] private float _damage;
            [SerializeField] private float _speed;
            [SerializeField] private float _timeLife;

            public SetStat Damage { get; private set; }
            public SetStat Speed { get; private set; }
            public SetStat TimeLife { get; private set; }

            public override void Init()
            {
                base.Init();
                Damage = new SetStat(new Stat(_damage));
                Speed = new SetStat(new Stat(_speed));
                TimeLife = new SetStat(new Stat(_timeLife));
            }
        }

        public override void Init()
        {
            base.Init();
            Parammeters.Init();
        }
    }
}