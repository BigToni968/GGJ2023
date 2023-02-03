using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Game/Data/" + nameof(Sources))]
    public class Sources : ScriptableObject
    {
        [SerializeField] private GameSkills _gameSkills;
        [SerializeField] private EntityData _entityData;

        public GameSkills GetGameSkills => _gameSkills;
        public EntityData GetEntityData => _entityData;

        [System.Serializable]
        public class GameSkills
        {
            [SerializeField] private Skills.SetSkills[] _skills;

            public IEnumerable GetSkills => _skills;
            public int Count => _skills.Length;

            public Skills.SetSkills Find(Skill skill)
            {
                foreach (Skills.SetSkills skills in _skills)
                {
                    Skills.SetSkills setSkill = skills.Clone() as Skills.SetSkills;
                    setSkill.Init(null);

                    if (skills.SetSkill.Skill == skill)
                        return skills;
                }
                return null;
            }
        }

        [System.Serializable]
        public class EntityData
        {
            [SerializeField] private GameEntity[] _gameEntity;

            public IEnumerable GetEntity => _gameEntity;
            public int Count => _gameEntity.Length;
        }
    }
}