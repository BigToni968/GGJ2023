using UnityEngine;

namespace Game.Gameplay
{
    public class Whiplash : BaseSkill
    {

        private Data.Whiplash.Parameters _parameters;
        private LineRenderer _line;
        private Vector3 _pos;
        private IRotate _rotate;
        private Vector2 _side = Vector2.right;

        public override void Init()
        {
            base.Init();
            _parameters = Stats as Data.Whiplash.Parameters;
            _line = Body.AddComponent<LineRenderer>();
            _line.startWidth = _line.endWidth = 0.2f;
            _rotate = Owner.GetComponent<IRotate>();
            _rotate.Side += Turn;
        }

        private void Turn(Vector2 side) => _side = side;

        public override void Destroy()
        {
            base.Destroy();
            _rotate.Side -= Turn;
        }

        public override void Update()
        {
            base.Update();
            if (_parameters == null) return;

            if (_line.GetPosition(1).x < _parameters.Lenght.Value)
            {
                if (_side == Vector2.right)
                {
                    _line.SetPosition(0, Owner.transform.position);
                    _pos = Owner.transform.position;
                    _pos.x = _line.GetPosition(1).x;

                    _pos.x += Time.deltaTime;
                }
                else if (_side == Vector2.left)
                {
                    _line.SetPosition(0, Owner.transform.position);
                    _pos = Owner.transform.position * -1;
                    _pos.x = _line.GetPosition(1).x;

                    _pos.x -= Time.deltaTime;
                }

                _line.SetPosition(1, _pos);
            }
            else Destroy();
        }
    }
}