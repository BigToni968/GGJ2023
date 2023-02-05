using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Game/Data/Skills/" + nameof(Liana))]
    public class Liana : Skill
    {
        [SerializeField] private Parameters _parameters;

        public override BaseSkill BaseSkill { get; set; } = new Gameplay.Liana();

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
                Damage = new SetStat(_damage);
                Lenght = new SetStat(_lenght);
            }
        }

        public override void Init()
        {
            base.Init();
            Parammeters.Init();
        }
    }
}