using UnityEngine;

namespace GS.Units
{
    [CreateAssetMenu(fileName = "Unit Config", menuName = "Units / Unit Config", order = 0)]
    public class UnitConfig : ScriptableObject
    {
        [SerializeField][Range(0.1f, 10f)] private float movementDuration = 2.3f;
        [SerializeField][Range(1, 5)] private int movementRadius = 3;

        public float MovementDuration => movementDuration;
        public int MovementRadius => movementRadius;
    }
}
