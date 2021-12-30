using System;
using System.Threading.Tasks;
using GS.StateMachina;
using UnityEngine;
using UnityEngine.Events;

namespace GS.Units
{
    [RequireComponent(typeof(UnitMovement), typeof(StateMachine), typeof(UnitActivity)), SelectionBase]
    public class Unit : MonoBehaviour, IUnitTarget
    {
        [SerializeField] private GameObject selectedGO;
        [SerializeField] private UnitConfig config;

        public UnitsManager Manager => _manager;
        public bool IsSelected { get; private set; }
        public float Health { get; private set; }
        public int HealthCapacity => config.HealthCapacity;
        public UnitConfig Config => config;
        public Vector3 Position => transform.position;
        public bool IsAvailable { get; private set; }
        
        private UnitsManager _manager;
        private UnitMovement _movement;
        private UnitActivity _unitActivity;
        private CapsuleCollider _collider;

        private void Awake()
        {
            _movement = GetComponent<UnitMovement>();
            _unitActivity = GetComponent<UnitActivity>();
            _collider = GetComponent<CapsuleCollider>();
        }

        public void Spawn(Vector3 at, UnitsManager by, int avoidance)
        {
            _manager = by;
            transform.position = at;
            Health = config.HealthCapacity;
            IsAvailable = true;
            _movement.AssignAvoidancePriority(avoidance);
        }
        
        public void MarchTowards(Vector3 dest)
        {
            _unitActivity.RemoveTarget(false);
            _movement.SetDestination(dest, _manager.SelectedUnits.Count);
        }
        
        public void StopMarching()
        {
            _movement.Stop();
        }
        
        public void SetTarget(Unit foe)
        {
            _unitActivity.SetTarget(foe, false);
            _movement.SetDestination(foe.transform.position, _manager.SelectedUnits.Count);
        }

        public void SetSelected(bool value)
        {
            selectedGO.SetActive(value);
            IsSelected = value;
        }

        public PlayerRelationship GetRelationshipTo(PlayerController playerController)
        {
            if (playerController == _manager.PlayerController)
            {
                return PlayerRelationship.Same;
            }

            // @Todo: introduce more cases when Diplomacy is implemented
            return PlayerRelationship.Foe;
        }
        
        public PlayerRelationship GetRelationshipTo(Unit unit)
        {
            return GetRelationshipTo(unit._manager.PlayerController);
        }

        public void TakeDamage(float value)
        {
            Health -= value;

            if (Health <= 0)
            {
                Die();
            }
        }

        private async void Die()
        {
            IsAvailable = false;
            _collider.enabled = false;
            await Task.Delay(TimeSpan.FromSeconds(2));
            _manager.Recycle(this);
        }
    }
}
