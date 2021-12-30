using UnityEngine;

namespace GS.Units
{
    public interface IUnitTarget
    {
        public Vector3 Position { get; }
        public bool IsAvailable { get; }
        
        public void TakeDamage(float damage);
    }
}
