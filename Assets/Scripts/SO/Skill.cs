using UnityEngine;

namespace Game.Data
{
    public abstract class Skill : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _name;
        [SerializeField][TextArea(5, 10)] private string _description;

        public Sprite Sprite => _sprite;
        public string Name => _name;
        public string Description => _description;

        public virtual ISkill Self => BaseSkill;

        public virtual BaseSkill BaseSkill { get; set; }

        public MonoEntity Owner { get; set; }

        public virtual void Init() { }
    }
}