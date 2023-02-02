using UnityEngine;
using System;

public class HitCollider : MonoBehaviour, IColliderSignal
{
    public event Action<Collision2D, ColliderSignal> Collision;
    public event Action<Collider2D, ColliderSignal> Trigger;

    private void OnCollisionEnter2D(Collision2D collision) => Collision?.Invoke(collision, ColliderSignal.EnterCollision);

    private void OnCollisionExit2D(Collision2D collision) => Collision?.Invoke(collision, ColliderSignal.ExitCollision);

    private void OnTriggerEnter2D(Collider2D collision) => Trigger?.Invoke(collision, ColliderSignal.EnterTrigger);

    private void OnTriggerExit2D(Collider2D collision) => Trigger?.Invoke(collision, ColliderSignal.ExitTrigger);
}

public enum ColliderSignal
{
    Null,
    EnterCollision,
    ExitCollision,
    EnterTrigger,
    ExitTrigger
}

public interface IColliderSignal
{
    public event Action<Collision2D, ColliderSignal> Collision;
    public event Action<Collider2D, ColliderSignal> Trigger;
}