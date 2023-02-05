using UnityEngine.UI;
using UnityEngine;

namespace Game.View
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private PreLoader _preLoader;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private Slider _music;
        [SerializeField] private Slider _sound;
        [SerializeField] private Toggle _fullScreen;

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
            _music.onValueChanged.AddListener(SetMusic);
            _sound.onValueChanged.AddListener(SetSound);
            _fullScreen.onValueChanged.AddListener(SetScreenMode);
        }

        private void OnDestroy()
        {
            _music.onValueChanged.RemoveListener(SetMusic);
            _sound.onValueChanged.RemoveListener(SetSound);
            _fullScreen.onValueChanged.RemoveListener(SetScreenMode);
        }

        private void SetMusic(float value) => _musicSource.volume = _preLoader.Data.Music = value;

        private void SetSound(float value) => _preLoader.Data.Sound = value;

        private void SetScreenMode(bool status)
        {
            Screen.fullScreenMode = status ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        }

    }
}