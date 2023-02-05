using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;

namespace Game.Gameplay
{
    public class Attack : MonoBehaviour, IAnimate
    {
        [SerializeField] private MonoEntity _mono;
        [SerializeField] private int _countActiveSkill;

        private IBaseAttack _attack;
        private ISkullAttack _skull;
        private IUniqueAttack _unique;

        private List<Data.Skill> _listActiveSkill;
        private SetSkill _activeUnique;

        public event Action<AnimaionType> Play;
        public event Action<int> PlaySkill;

        private void OnEnable()
        {
            _listActiveSkill ??= new List<Data.Skill>(_countActiveSkill);

            _attack ??= _mono.GetComponent<IBaseAttack>();
            _skull ??= _mono.GetComponent<ISkullAttack>();
            _unique ??= _mono.GetComponent<IUniqueAttack>();

            if (_attack != null)
                _attack.Fire += BaseAttack;

            if (_skull != null)
                _skull.Skull += Skull;

            if (_unique != null)
                _unique.Unique += Unique;
        }

        private void OnDisable()
        {
            if (_attack != null)
                _attack.Fire -= BaseAttack;

            if (_skull != null)
                _skull.Skull -= Skull;

            if (_unique != null)
                _unique.Unique -= Unique;
        }

        private void BaseAttack() { }

        private void Skull(Data.RootMan man)
        {
            if (_listActiveSkill.Count == _countActiveSkill || man.Skills.SelectSkill == null) return;
            if (_listActiveSkill.Contains(man.Skills.SelectSkill.Skill)) return;

            SkullExecute(man.Skills.SelectSkill);
        }

        private async void SkullExecute(SetSkill setSkill)
        {
            _listActiveSkill.Add(setSkill.Skill);
            setSkill.Execute();
            int delay = Convert.ToInt32(1000 * setSkill.Delay);
            await Task.Delay(delay);
            _listActiveSkill.Remove(setSkill.Skill);
        }


        private void Unique(Skills.SetSkills attack)
        {
            if (_activeUnique != null || attack == null) return;

            _activeUnique = attack.SetSkill;
            UniqueExecute();
        }

        private async void UniqueExecute()
        {
            Play?.Invoke(AnimaionType.Attack);
            _activeUnique.Execute();
            await Task.Delay(Convert.ToInt32(1000 * _activeUnique.Delay));
            _activeUnique = null;
            PlaySkill?.Invoke(0);
        }
    }
}

public interface IAttack : IBaseAttack, ISkullAttack { }

public interface IBaseAttack
{
    public event Action Fire;
}

public interface IUniqueAttack
{
    public event Action<Skills.SetSkills> Unique;
}

public interface ISkullAttack
{
    public event Action<Game.Data.RootMan> Skull;
}