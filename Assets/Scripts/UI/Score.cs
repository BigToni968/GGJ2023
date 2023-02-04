using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace Game.View
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private PreLoader _preLoader;
        [SerializeField] private Text _text;

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
                _parameters.Score.Update += (float val) => _text.text = $"Score : {_parameters.Score.Value}";
                _parameters.Score.Value = +0;
            }
        }
    }

}