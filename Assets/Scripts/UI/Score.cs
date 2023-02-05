using UnityEngine;
using TMPro;

namespace Game.View
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private PreLoader _preLoader;
        [SerializeField] private TextMeshProUGUI _text;

        private Data.RootMan.Parameters _parameters;

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
            _parameters = (_preLoader.GetHero as Data.RootMan)?.Parammeters;

            if (_parameters != null)
            {
                _parameters.Score.Update += UpdateScore;
                _parameters.Score.Value = +0;
            }
        }

        private void UpdateScore(float val) => _text.SetText($"{_parameters.Score.Value}");

        private void OnDestroy()
        {
            if (_parameters != null)
            {
                _parameters.Score.Update -= UpdateScore;
            }
        }
    }
}