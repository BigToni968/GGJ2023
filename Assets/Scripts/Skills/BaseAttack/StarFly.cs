using System.Threading.Tasks;
using UnityEngine;
using System;

namespace Game.Gameplay
{
    public class StarFly : BaseSkill
    {
        private Data.StarFly.Parameters _parameters;
        private Vector2 _direcion;
        private IColliderSignal _signal;

        public override void Init()
        {
            base.Init();
            Body.transform.parent = null;
            _parameters = Stats as Data.StarFly.Parameters;
            //_direcion = Owner.GetComponent<IRotate>().Direction;
            //_direcion = Vector2.right * _direcion * _parameters.Speed.Value;
            _direcion = Vector2.right * _parameters.Speed.Value;

            Body.AddComponent<SpriteRenderer>().sprite = Sprite;
            _signal = Body.AddComponent<HitCollider>() as IColliderSignal;
            _signal.Trigger += OnTriggerEnter;
            BoxCollider2D box = Body.AddComponent<BoxCollider2D>();

            box.autoTiling = true;
            box.isTrigger = true;
            Body.transform.localScale = Body.transform.localScale * 10;
            Timer();
        }

        private void OnTriggerEnter(Collider2D collider, ColliderSignal type)
        {
            //Debug.Log(collider.name);
            if (collider.TryGetComponent<IDamage>(out IDamage target))
            {
                //if (target.Owner == Owner) Debug.Log("Это я.");
            }
        }

        private async void Timer()
        {
            await Task.Delay(Convert.ToInt32(1000 * _parameters.Lifetime.Value));
            if (Body != null)
                Destroy();
        }

        public override void Destroy()
        {
            base.Destroy();
            _signal.Trigger -= OnTriggerEnter;
        }

        public override void Update()
        {
            base.Update();
            Body.transform.Translate(_direcion * Time.deltaTime);
        }
    }
}