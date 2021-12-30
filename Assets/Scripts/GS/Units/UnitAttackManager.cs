using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GS.Units
{
    public class UnitAttackManager : MonoBehaviour
    {
        public bool IsRecharging { get; private set; }

        private Unit _unit;
        private float _elapsedSinceLastAttack;

        private void Awake()
        {
            _unit = GetComponent<Unit>();
            IsRecharging = false;
        }

        private void Update()
        {
            if (IsRecharging)
            {
                KeepRecharging();
            }
        }

        public void Attack(IUnitTarget target)
        {
            if (IsRecharging)
            {
                return;
            }

            // transform.LookAt(target.Position);

            // wait for animation to complete 
            // await Task.Delay(TimeSpan.FromSeconds(0.75f));
            DeliverAttack(target);
        }

        private void DeliverAttack(IUnitTarget target)
        {
            if (IsRecharging || target == null)
            {
                return;
            }

            transform.LookAt(target.Position);
            target.TakeDamage(_unit.Config.Attack);
            IsRecharging = true;
        }

        // Maybe this should be called from act?
        private void KeepRecharging()
        {
            _elapsedSinceLastAttack += Time.deltaTime;
            if (_elapsedSinceLastAttack < _unit.Config.RechargeTime)
            {
                return;
            }

            IsRecharging = false;
            _elapsedSinceLastAttack = 0;
        }
    }
}
