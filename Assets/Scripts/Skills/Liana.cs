using UnityEngine;

namespace Game.Gameplay
{
    public class Liana : BaseSkill
    {

        private Data.Liana.Parameters _parameters;
        private Data.RootMan _owner;
        private Animated _animated;

        public override void Init()
        {
            base.Init();
            _parameters = Stats as Data.Liana.Parameters;
            _animated = Owner.GetComponent<Animated>();
            _owner = Owner.GameEntity as Data.RootMan;
            Play();
        }

        private void Play()
        {
            _animated.PlaySkill(3);
            Debug.DrawRay(Body.transform.position, Vector2.right * _parameters.Lenght.Value, Color.red);
            RaycastHit2D[] hits = Physics2D.RaycastAll(Body.transform.position, Vector2.right * _parameters.Lenght.Value);

            foreach (RaycastHit2D ray in hits)
                if (ray.collider.TryGetComponent(out IDamage enemy))
                    if (enemy.Owner.GameEntity.GEType == GEType.Enemy)
                        _owner.Parammeters.Score.Value = enemy.Get(_parameters.Damage.Value);
            _animated.PlaySkill(0);
        }

        private void Attak()
        {

        }
    }
}