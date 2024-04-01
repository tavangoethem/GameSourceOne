using UnityEngine;
using UnityEngine.AI;

namespace AiStates
{
    public class Attack : AIStateBase
    {
        public override AIStateType GetAIStateType { get { return AIStateType.Attack; } }

        public override NavMeshAgent GetNavAgent { get { return _myAgent.GetNavAgent; } }

        public override AIStateType OnStateUpdate()
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 toOther = _myAgent.Player.transform.position - transform.position;
            toOther = toOther.normalized;
            if (Vector3.Distance(_myAgent.Player.transform.position, transform.position) < _myAgent._RangeAttack)
            {
                transform.LookAt(_myAgent.Player.transform);
                if (_myAgent.PlayerLastKnowPosition != null)
                {
                    _myAgent.GetNavAgent.destination = _myAgent.PlayerLastKnowPosition.transform.position;
                }
                RaycastHit hit;
                if (Physics.Raycast(this.transform.position, toOther, out hit, _myAgent._isPlayerInRange))
                {
                    if (hit.transform.GetComponent<MovementController>() != null)
                    {
                        if (_myAgent.PlayerLastKnowPosition != null)
                        {
                            _myAgent.PlayerLastKnowPosition.transform.position = _myAgent.Player.transform.position;
                        }
                        Debug.DrawRay(transform.position, toOther * 1000, Color.white);
                        Debug.Log("Attack");
                    }
                }
            }
            else if (Vector3.Distance(_myAgent.Player.transform.position, transform.position) > _myAgent._RangeAttack)
            {
                if (_myAgent.PlayerLastKnowPosition != null)
                {
                    _myAgent.GetNavAgent.destination = _myAgent.PlayerLastKnowPosition.transform.position;
                }
                if (Vector3.Distance(_myAgent.Player.transform.position, transform.position) > _myAgent._isPlayerInRange)
                {
                    if (_myAgent.PlayerLastKnowPosition != null)
                    {
                        _myAgent.PlayerLastKnowPosition.transform.position = _myAgent.Player.transform.position;
                    }
                    Debug.DrawRay(transform.position, toOther * 1000, Color.white);
                    return AIStateType.Chase;
                }
            }
            return GetAIStateType;
        }
    }
}