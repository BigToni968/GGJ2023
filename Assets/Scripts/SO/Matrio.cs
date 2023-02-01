using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Game/Data/" + nameof(Matrio))]
    public class Matrio : GameEntity
    {
        [SerializeField] private Parameters _parameters;

        public Parameters Parammeters => _parameters;

        [System.Serializable]
        public class Parameters : Stats
        {
            [SerializeField] private float _health;

            public SetStat Health { get; private set; }

            public override void Init()
            {
                base.Init();
                Health = new SetStat(new Stat(_health));
            }
        }

        public override void Init()
        {
            base.Init();
            Parammeters.Init();
        }
    }
}