using UnityEngine.UI;
using UnityEngine;

namespace Game.View
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private PreLoader _preLoader;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private Slider _health;

        Data.RootMan _root;
        private int count;

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

        private void OnDestroy()
        {
            if (_root != null)
                _root.Parammeters.Health.Update -= SetValue;

            if (_spawner != null)
                (_spawner as IDead).Dead -= WaveProggres;
        }

        public void Init()
        {
            if (_spawner != null)
            {
                Init(_spawner);
                return;
            }

            _root = _preLoader.GetHero as Data.RootMan;
            _root.Parammeters.Health.Update += SetValue;
            SetParams(_root.Parammeters.Health.Value, _root.Parammeters.Health.Value);
        }

        public void Init(Spawner spawner)
        {
            (spawner as IDead).Dead += WaveProggres;
            SetParams(spawner.Max, spawner.Max);
        }

        public void WaveProggres()
        {
            count--;
            SetValue(count);
        }

        public void SetParams(float count, float max)
        {
            count = _health.maxValue = max;
            SetValue(count);
        }

        public void SetValue(float val) => _health.value = val;
    }
}