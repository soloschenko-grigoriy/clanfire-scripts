using UnityEngine;

namespace GS.Structures
{
    [CreateAssetMenu(fileName = "Structure", menuName = "Structure Config", order = 0)]
    public class StructureSO : ScriptableObject
    {
        [SerializeField] private string title;
        [SerializeField][Range(5, 100f)] private float constructionTime;

        public float ConstructionTime => constructionTime;
        public string Title => title;
    }
}
