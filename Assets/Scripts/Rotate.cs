using UnityEngine;
using System;

public class Rotate : MonoBehaviour
{
    [SerializeField] private MonoEntity _mono;

    private IRotate _rotate;
    private Vector2 _side = Vector2.right;
    private Vector2 _scale;

    private void OnEnable()
    {
        _rotate = _mono.GetComponent<IRotate>();

        if (_rotate != null)
            _rotate.Side += Turn;
    }

    private void OnDisable()
    {
        if (_rotate != null)
            _rotate.Side -= Turn;
    }

    private void Turn(Vector2 side)
    {
        if (_side == side) return;
        _side = side;
        _scale = _mono.transform.localScale;
        _scale.x *= -1;
        _mono.transform.localScale = _scale;
    }
}

public interface IRotate
{
    public event Action<Vector2> Side;
}