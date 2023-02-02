using System.Collections.Generic;
using UnityEngine;

namespace Game.View
{
    public class SkullManager : MonoBehaviour
    {
        [SerializeField] private PreLoader _preLoader;
        [SerializeField] private SkullButton _prefab;
        [SerializeField] private int _countButton;

        private Data.RootMan _hero;
        private List<SkullButton> _buttons;

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
            foreach (SkullButton button in _buttons)
                button.Click -= SelectSkullCharacter;

            _buttons.Clear();
        }

        public void Init()
        {
            _buttons ??= new List<SkullButton>();

            foreach (GameEntity entity in _preLoader.GetGameEntity)
                if (entity.GEType == GEType.Hero) _hero = entity as Data.RootMan;

            for (int i = 0; i < _countButton; i++)
            {
                SkullButton button = Instantiate(_prefab, transform);
                int j = 0;

                foreach (Skills.SetSkills setSkill in _hero.Skills.Get)
                {
                    if (i == j)
                    {
                        _buttons.Add(button);
                        button.Init(_hero, setSkill.SetSkill.Skill);
                    }
                    j++;
                }

                button.Click += SelectSkullCharacter;
            }
        }

        private void SelectSkullCharacter(GEType type, Data.Skill skill)
        {
            foreach (GameEntity entity in _preLoader.GetGameEntity)
                if (entity.GEType == GEType.Hero)
                {
                    Data.RootMan hero = entity as Data.RootMan;
                    hero.Skills.Select(skill.GetType());
                }
        }
    }
}