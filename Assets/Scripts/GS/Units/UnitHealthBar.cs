using UnityEngine;
using UnityEngine.UI;

namespace GS.Units
{
    public class UnitHealthBar : MonoBehaviour
    {
        [SerializeField] private Unit unit;

        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.minValue = 0;
            _slider.maxValue = 1;
        }

        private void Update()
        {
            _slider.value = unit.Health / unit.HealthCapacity;
        }
    }
}
