using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Game/Data/" + nameof(GameSessionData))]
    public sealed class GameSessionData : ScriptableObject
    {
        [SerializeField] private float _music;
        [SerializeField] private float _sound;

        private float _musicVolume;
        private float _soundVolume;


        public float Music { get => _musicVolume; set => _musicVolume = value; }
        public float Sound { get => _soundVolume; set => _soundVolume = value; }
        public int Score { get; set; }

        public GameEntity Hero { get; set; }

        public bool IsPause { get; set; } = false;

        private void OnEnable()
        {
            Music = _music;
            Sound = _sound;
        }

        private void OnDisable()
        {
            Hero = null;
        }
    }
}