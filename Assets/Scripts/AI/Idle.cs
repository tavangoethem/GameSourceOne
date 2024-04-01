using UnityEngine;
using UnityEngine.AI;

namespace AiStates
{
    public class Idle : AIStateBase
    {
        public override AIStateType GetAIStateType { get { return AIStateType.StartingState; } }

        public override NavMeshAgent GetNavAgent { get { return _myAgent.GetNavAgent; } }

        public override AIStateType OnStateUpdate()
        {
            if (_myAgent.CanSeePlayer == true)
            {
                if (_myAgent.PlayerLastKnowPosition != null)
                {
                    _myAgent.PlayerLastKnowPosition.transform.position = _myAgent.Player.transform.position;
                }
                if (_myAgent.Player != null)
                    _myAgent.GetNavAgent.destination = _myAgent.Player.transform.position;
                return AIStateType.Chase;
            }
            if (_myAgent.Player != null)
            {
                Vector3 forward = transform.TransformDirection(Vector3.forward);
                Vector3 toOther = _myAgent.Player.transform.position - transform.position;
                toOther = toOther.normalized;
                RaycastHit hit;
                if (Vector3.Distance(_myAgent.Player.transform.position, transform.position) < _myAgent._isPlayerInRange)
                {
                    if (Physics.Raycast(this.transform.position, toOther, out hit, _myAgent._isPlayerInRange / 2))
                    {
                        if (hit.transform.GetComponent<MovementController>() != null)
                        {
                            Debug.DrawRay(transform.position, _myAgent.toOther * 1000, Color.red);
                            if (_myAgent.PlayerLastKnowPosition != null)
                            {
                                _myAgent.PlayerLastKnowPosition.transform.position = _myAgent.Player.transform.position;
                            }
                            _myAgent.GetNavAgent.destination = _myAgent.Player.transform.position;

                            return AIStateType.Chase;
                        }
                    }
                    else if (Vector3.Dot(_myAgent.forward, _myAgent.toOther) > _myAgent.FOVForEnemy)
                    {
                        if (Physics.Raycast(this.transform.position, _myAgent.toOther, out hit, _myAgent._isPlayerInRange))
                        {
                            if (hit.transform.GetComponent<MovementController>() != null)
                            {
                                Debug.DrawRay(transform.position, _myAgent.toOther * 1000, Color.white);
                                return AIStateType.Chase;
                            }
                        }
                    }
                }
            }
            return GetAIStateType;
        }
    }
}