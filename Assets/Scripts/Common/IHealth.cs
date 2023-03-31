using System;

namespace Common
{
    public interface IHealth
    {
        event Action Changed;
        float Value { get; }
        float MaxValue { get; }
        bool IsAlive { get; }
        void TakeDamage(float damage);
    }
}