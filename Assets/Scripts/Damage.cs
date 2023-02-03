public interface IDamage
{
    public MonoEntity Owner { get; }
    public int Get(float Damage);
}