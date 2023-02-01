using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Game/Data/Skills/" + nameof(Whiplash))]
    public class Whiplash : Skill
    {
        [SerializeField] private Parameters _parameters;

        public override BaseSkill BaseSkill { get; set; } = new Gameplay.Whiplash();

        public Parameters Parammeters => _parameters;

        [System.Serializable]
        public class Parameters : Stats
        {
            [SerializeField] private float _damage;
            [SerializeField] private float _lenght;

            public SetStat Damage { get; private set; }
            public SetStat Lenght { get; private set; }

            public override void Init()
            {
                base.Init();
                Damage = new SetStat(new Stat(_damage));
                Lenght = new SetStat(new Stat(_lenght));
            }
        }

        public override void Init()
        {
            base.Init();
            Parammeters.Init();
        }
    }
}