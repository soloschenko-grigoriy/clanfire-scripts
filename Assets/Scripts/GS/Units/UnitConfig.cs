using UnityEngine;

namespace GS
{
    [CreateAssetMenu(fileName = "Unit Config", menuName = "Unit Config", order = 0)]
    public class UnitConfig : ScriptableObject
    {
        [SerializeField] private int healthCapacity = 100;
        [SerializeField] private float attack = 10.0f;
        [SerializeField][Range(1, 10)] private float rechargeTime = 1.0f;
        [SerializeField] [Range(0.5f, 50f)] private float awareness = 10f;
        [SerializeField] [Range(0.1f, 20f)] private float targetReachDistance = 1.5f;
        [SerializeField] [Range(0.1f, 2f)] private float targetGiveUpDistance = 20f;
        
        public int HealthCapacity => healthCapacity;
        public float Attack => attack;
        public float RechargeTime => rechargeTime;
        public float Awareness => awareness;
        public float TargetReachDistance => targetReachDistance;
        public float TargetGiveUpDistance => targetGiveUpDistance;
    }
}
