using UnityEngine.UI;
using UnityEngine;

namespace Game.View
{
    public class EndLevel : MonoBehaviour
    {
        [SerializeField] private PreLoader _preLoader;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private MessEndGame _mess;
        [SerializeField] private Slider _healthEnemy;

        private void OnEnable()
        {
            if (_preLoader != null)
                _preLoader.Init += Init;
        }

        private void OnDisable()
        {
            if (_preLoader != null)
                _preLoader.Init -= Init;
        }

        private void Init()
        {
            (_preLoader.GetHero.Owner as IDead).Dead += DeadHero;
            (_spawner as IDead).Dead += DeadEnemy;
        }

        private void DeadHero()
        {
            (_preLoader.GetHero.Owner as IDead).Dead -= DeadHero;
            (_spawner as IDead).Dead -= DeadEnemy;

            Result(GEType.Hero);
        }
        private void DeadEnemy()
        {
            if (_healthEnemy.value <= 0)
            {
                (_preLoader.GetHero.Owner as IDead).Dead -= DeadHero;
                (_spawner as IDead).Dead -= DeadEnemy;

                Result(GEType.Enemy);
            }
        }
        private void Result(GEType type)
        {
            if (type == GEType.Enemy)
                _mess.Call("Win!", null);
        }
    }
}