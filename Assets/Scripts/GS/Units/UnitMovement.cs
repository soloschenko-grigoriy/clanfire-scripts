using System.Linq;
using Castle.Core.Internal;
using GS.Units.Helpers;
using UnityEngine;
using UnityEngine.AI;

namespace GS.Units
{
    [RequireComponent(typeof(Unit))]
    public class UnitMovement : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Unit _unit;
        private const int LayerMask = 1 << 7;
        private const int MAXColliders = 100;

        public bool DestinationIsSet { get; private set; }
        
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _unit = GetComponent<Unit>();
        }

        private void Update()
        {
            if (_agent.isStopped)
            {
                return;
            }

            if (_agent.pathPending)
            {
                return;
            }

            if (_agent.remainingDistance >= _agent.stoppingDistance)
            {
                return;
            }

            if (_agent.hasPath && _agent.velocity.sqrMagnitude != 0f)
            {
                return;
            }

            transform.LookAt(_agent.destination);
            Stop();
        }

        public void AssignAvoidancePriority(int num)
        {
            _agent.avoidancePriority = num;
        }
        
        public void SetDestination(Vector3 dest, int numberOfAgents)
        {
            var hitColliders = new Collider[MAXColliders];
            Physics.OverlapSphereNonAlloc(transform.position, _unit.Config.Awareness, hitColliders, LayerMask);
            var numberOfAgentsNearby = hitColliders.FindAll(col => col != null).Length;
            
            _agent.SetDestination(dest);
            // _agent.stoppingDistance = StoppingDistance.FindByKey(numberOfAgentsNearby);
            DestinationIsSet = true;
        }

        public void Go()
        {
            _agent.isStopped = false;
        }

        public void Stop()
        {
            transform.LookAt(_agent.destination);
            _agent.isStopped = true;
            DestinationIsSet = false;
        }
        
#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Debug.DrawLine(transform.position, _agent.destination, Color.magenta);
        }
#endif
    }
}
