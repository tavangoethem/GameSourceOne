using UnityEngine;
using UnityEngine.AI;
using Weaponry;

namespace AiStates
{
    public class Chase : AIStateBase
    {
        public override AIStateType GetAIStateType { get { return AIStateType.Chase; } }

        public override NavMeshAgent GetNavAgent { get { return _myAgent.GetNavAgent; } }

        public override AIStateType OnStateUpdate()
        {
            if (_myAgent.weapon.GetComponent<IAIWeapons>().IsShoot == true)
            {
                _myAgent.isShoot = false;
                _myAgent.weapon.GetComponent<IAIWeapons>()?.shootBool(_myAgent.isShoot);
            }
            if (_myAgent.PlayerLastKnowPosition != null)
            {
                _myAgent.GetNavAgent.destination = _myAgent.PlayerLastKnowPosition.transform.position;
            }
            if (_myAgent.Player == null)
            {
                return AIStateType.StartingState;
            }
            Vector3 forward = _myAgent.Player.transform.TransformDirection(Vector3.forward);
            Vector3 toOther = _myAgent.Player.transform.position - transform.position;
            toOther = toOther.normalized;
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, toOther, out hit, _myAgent._isPlayerInRange))
            {
                if (hit.transform.GetComponent<MovementController>() != null)
                {
                    _myAgent.CanSeePlayer = true;
                    Collider[] colls = Physics.OverlapSphere(transform.position, 10);
                    foreach (Collider coll in colls)
                    {
                        if (coll.gameObject.GetComponent<AIStates>() && coll.gameObject.GetComponent<AIStates>().CanSeePlayer == false)
                        {
                            coll.gameObject.GetComponent<AIStates>().CanSeePlayer = true;
                            if (_myAgent.Player != null)
                                coll.gameObject.GetComponent<AIStates>().Player = _myAgent.Player;
                        }
                    }
                    if (_myAgent.PlayerLastKnowPosition != null)
                    {
                        _myAgent.PlayerLastKnowPosition.transform.position = _myAgent.Player.transform.position;
                        _myAgent.GetNavAgent.destination = _myAgent.PlayerLastKnowPosition.transform.position;
                    }   
                    Debug.Log("Update Position");
                    if (Vector3.Distance(_myAgent.Player.transform.position, transform.position) < _myAgent._RangeAttack)
                    {
                        Debug.Log("Go To Attack1");
                        return AIStateType.Attack;
                    }
                }
                else if (true)
                {
                    _myAgent.CanSeePlayer = false;
                    if (_myAgent.PlayerLastKnowPosition != null && _myAgent.Player != null)
                    {
                        _myAgent.PlayerLastKnowPosition.transform.rotation = _myAgent.Player.transform.rotation;
                        _myAgent.PlayerLastKnowPosition.SetActive(true);
                    }
                }
            }
            if (Vector3.Distance(_myAgent.PlayerLastKnowPosition.transform.position, _myAgent.gameObject.transform.position) - _myAgent.PlayerLastKnowPosition.transform.position.y < 2)
            {
                return AIStateType.StartingState;
            }
            else if (_myAgent.CanSeePlayer == false)
            {
                if (_myAgent.PlayerLastKnowPosition != null && _myAgent.Player != null)
                {
                    _myAgent.PlayerLastKnowPosition.transform.rotation = _myAgent.Player.transform.rotation;
                    _myAgent.PlayerLastKnowPosition.SetActive(true);
                }
            }
            return GetAIStateType;
        }
    }
}