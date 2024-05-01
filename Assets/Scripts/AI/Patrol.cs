using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace AiStates
{
    public class Patrol : AIStateBase
    {

        public override AIStateType GetAIStateType { get { return AIStateType.StartingState; } }
        public override NavMeshAgent GetNavAgent { get { return _myAgent.GetNavAgent; } }

        private bool _NextCheckPoint = true;
        public Transform[] _Checkpoints;
        [SerializeField] private int _CheckpointDest = 0;

        public override AIStateType OnStateUpdate()
        {
            //_myAgent?.audioManager?.PlaySFX(_myAgent.movingClip);
            if (_myAgent.CanSeePlayer == true)
            {
                _myAgent.PlayerLastKnowPosition.transform.position = _myAgent.Player.transform.position;
                if (_myAgent.Player != null)
                    _myAgent.GetNavAgent.destination = _myAgent.Player.transform.position;
                return AIStateType.Chase;
            }
            if (_myAgent.Player != null)
            {
                _myAgent.GetNavAgent.destination = _Checkpoints[_CheckpointDest].position;
                if (Vector3.Distance(_myAgent.Player.transform.position, transform.position) < _myAgent._isPlayerInRange / 2)
                {
                    Vector3 forward = transform.TransformDirection(Vector3.forward);
                    Vector3 toOther = _myAgent.Player.transform.position - transform.position;
                    toOther = toOther.normalized;
                    RaycastHit hit;
                    if (Physics.Raycast(this.transform.position, toOther, out hit, _myAgent._isPlayerInRange))
                    {
                        if (hit.transform.GetComponent<MovementController>() != null)
                        {
                            Debug.DrawRay(transform.position, toOther * 1000, Color.red);
                            if (_myAgent.PlayerLastKnowPosition != null)
                            {
                                _myAgent.PlayerLastKnowPosition.transform.position = _myAgent.Player.transform.position;
                                _myAgent.GetNavAgent.destination = _myAgent.PlayerLastKnowPosition.transform.position;
                            }
                            //Debug.Log("Go To Chase1");
                            return AIStateType.Chase;
                        }
                    }
                }
                else if (_NextCheckPoint == true)
                {
                    // Set the agent to go to the currently selected destination.
                    _myAgent.GetNavAgent.destination = _Checkpoints[_CheckpointDest].position;
                    _CheckpointDest = _CheckpointDest + 1;
                    // Choose the next point in the array as the destination,
                    // cycling to the start if necessary.
                    if (_CheckpointDest == _Checkpoints.Length)
                    {
                        _CheckpointDest = 0;
                    }
                    _NextCheckPoint = false;
                }
                else if (Vector3.Distance(_myAgent.transform.position, _myAgent.GetNavAgent.destination) < 1)
                {
                    _myAgent.GetNavAgent.destination = this.transform.position;
                    _NextCheckPoint = true;
                }
                else if (_myAgent.Player)
                {
                    if (Vector3.Dot(_myAgent.forward, _myAgent.toOther) > _myAgent.FOVForEnemy)
                    {
                        //print("Can see");
                        if (Vector3.Distance(_myAgent.Player.transform.position, transform.position) < _myAgent._isPlayerInRange)
                        {

                            RaycastHit hit;
                            if (Physics.Raycast(this.transform.position, _myAgent.toOther, out hit, _myAgent._isPlayerInRange))
                            {
                                if (hit.transform.GetComponent<MovementController>() != null)
                                {
                                    //Debug.DrawRay(transform.position, _myAgent.toOther * 1000, Color.white);
                                    //Debug.Log("Go To Chase2");
                                    return AIStateType.Chase;
                                }
                            }
                        }
                    }
                }
            }
            return GetAIStateType;
        }
    }
}