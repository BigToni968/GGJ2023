using UnityEngine;
using System;

public class Dead : MonoBehaviour
{
    [SerializeField] private MonoEntity _mono;

    private IDead _dead;

    private void OnEnable()
    {
        _dead = _mono.GetComponent<IDead>();

        if (_dead != null)
            _dead.Dead += OnDead;
    }

    private void OnDisable()
    {
        if (_dead != null)
            _dead.Dead -= OnDead;
    }

    private void OnDead()
    {
        Destroy(gameObject);
    }
}

public interface IDead
{
    public event Action Dead;
    public bool IsDead { get; }
}