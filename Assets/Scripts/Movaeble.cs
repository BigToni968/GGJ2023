using UnityEngine;
using System;

public class Movaeble : MonoBehaviour
{
    [SerializeField] private MonoEntity _monoEntity;
    [SerializeField] private Rigidbody2D _rb;

    private IMove _move;

    private void OnEnable()
    {
        _move ??= _monoEntity.GetComponent<IMove>();
        if (_move == null) return;
        _move.To += Move;
    }

    private void OnDisable()
    {
        if (_move == null) return;
        _move.To -= Move;
    }

    private void Move(Vector2 direction)
    {
        Vector2 velocity = Vector2.zero;
        velocity.Set(direction.x, direction.y);
        _rb.velocity = direction;
    }
}

public interface IMove
{
    public event Action<Vector2> To;
}