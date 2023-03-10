using System.Collections.Generic;
using UnityEngine;

namespace Game.View
{
    public class SkullManager : MonoBehaviour
    {
        [SerializeField] private PreLoader _preLoader;
        [SerializeField] private SkullButton _prefab;
        [SerializeField] private Transform _content;
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
            _hero.Skills.Update -= UpdateSkillsHero;
        }

        public void Init()
        {
            _buttons ??= new List<SkullButton>();

            _hero = _preLoader.GetHero as Data.RootMan;

            _hero.Skills.Update += UpdateSkillsHero;

            for (int i = 0; i < _countButton; i++)
            {
                SkullButton button = Instantiate(_prefab, _content);
                int j = 0;

                foreach (Skills.SetSkills setSkill in _hero.Skills.Get)
                {
                    if (i == j)
                        button.Init(_hero, setSkill.SetSkill.Skill);

                    j++;
                }

                _buttons.Add(button);
                button.Click += SelectSkullCharacter;
            }
        }

        private void UpdateSkillsHero(Skills.SetSkills setSkills)
        {
            int i = 0;
            foreach (SkullButton button in _buttons)
            {
                int j = 0;

                foreach (Skills.SetSkills setSkill in _hero.Skills.Get)
                {
                    if (i == j)
                        button.Init(_hero, setSkills.SetSkill.Skill);
                    j++;
                }
                i++;
            }
        }

        private void SelectSkullCharacter(GEType type, Data.Skill skill)
        {
            Data.RootMan hero = _hero as Data.RootMan;
            hero.Skills.Select(skill.GetType());
        }
    }
}