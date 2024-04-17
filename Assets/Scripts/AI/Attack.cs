using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Weaponry;

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
                //Vector3 tempDir = (_myAgent.Player.transform.position - _myAgent.transform.position).normalized;
                transform.LookAt(new Vector3 (_myAgent.Player.transform.position.x, transform.position.y, _myAgent.Player.transform.position.z));
                _myAgent.GetNavAgent.destination = _myAgent.PlayerLastKnowPosition.transform.position;
                RaycastHit hit;
                if (Physics.Raycast(this.transform.position, toOther, out hit, _myAgent._isPlayerInRange))
                {
                    if (hit.transform.GetComponent<MovementController>() != null)
                    {
                        Debug.DrawRay(transform.position, toOther * 1000, Color.white);
                        if (_myAgent.isShoot == false)
                        {
                            _myAgent.isShoot = true;
                            _myAgent.weapon.GetComponent<IAIWeapons>()?.shootBool(_myAgent.isShoot);
                            _myAgent.weapon.GetComponent<IAIWeapons>()?.AIShoot(_myAgent.Player);
                        }
                        if (Vector3.Distance(_myAgent.Player.transform.position, transform.position) < _myAgent._RangeAttack / 2)
                        {
                            _myAgent.GetNavAgent.destination = this.transform.position;
                            _myAgent.isStopped = true;
                        }
                        else
                            _myAgent.isStopped = false;
                    }
                }
                else
                    _myAgent.isShoot = false;
                _myAgent.weapon.GetComponent<IAIWeapons>()?.shootBool(_myAgent.isShoot);
            }
            else if (Vector3.Distance(_myAgent.Player.transform.position, transform.position) > _myAgent._RangeAttack)
            {
                _myAgent.isStopped = false;
                _myAgent.isShoot = false;
                _myAgent.weapon.GetComponent<IAIWeapons>()?.shootBool(_myAgent.isShoot);
                if (_myAgent.PlayerLastKnowPosition != null)
                {
                    //_myAgent.audioManager.PlaySFX(_myAgent.movingClip);
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