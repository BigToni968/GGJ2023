using UnityEngine.UI;
using UnityEngine;
using System;

namespace Game.View
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private PreLoader _preLoader;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private Slider _health;

        Data.RootMan _root;
        private int _count;

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
            _count--;
            SetValue(_count);
        }

        public void SetParams(float count, float max)
        {
            _count = Convert.ToInt32(_health.maxValue = max);
            SetValue(count);
        }

        public void SetValue(float val) => _health.value = val;
    }
}