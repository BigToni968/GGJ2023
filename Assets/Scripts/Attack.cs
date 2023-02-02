using System.Threading.Tasks;
using UnityEngine;
using System;

namespace Game.Gameplay
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private MonoEntity _mono;

        private IBaseAttack _attack;
        private ISkullAttack _skull;
        private IUniqueAttack _unique;

        private SetSkill _activeSkull;
        private SetSkill _activeUnique;

        private void OnEnable()
        {
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
            if (_activeSkull != null || man.Skills.SelectSkill == null) return;

            _activeSkull = man.Skills.SelectSkill;
            SkullExecute();
        }

        private async void SkullExecute()
        {
            _activeSkull.Execute();
            await Task.Delay(Convert.ToInt32(1000 * _activeSkull.Delay));
            _activeSkull = null;
        }


        private void Unique(Skills.SetSkills attack)
        {
            if (_activeUnique != null || attack == null) return;

            _activeUnique = attack.SetSkill;
            UniqueExecute();
        }

        private async void UniqueExecute()
        {
            _activeUnique.Execute();
            await Task.Delay(Convert.ToInt32(1000 * _activeUnique.Delay));
            _activeUnique = null;
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