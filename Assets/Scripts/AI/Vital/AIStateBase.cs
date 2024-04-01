using UnityEngine;
using UnityEngine.AI;

namespace AiStates
{
    public abstract class AIStateBase : MonoBehaviour
    {
        protected AIStates _myAgent;
        public abstract AIStateType GetAIStateType { get; }
        public abstract NavMeshAgent GetNavAgent { get; }

        public void InitState(AIStates agent)
        {
            _myAgent = agent;
        }   

        public virtual void OnStateEnter()
        {

        }
        public virtual void OnStateExit()
        {

        }
        public abstract AIStateType OnStateUpdate();
    }

    public enum AIStateType
    {
        StartingState,
        Chase,
        Attack,
        SecondAttackState
    }
}