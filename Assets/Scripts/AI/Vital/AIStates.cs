using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Weaponry;

namespace AiStates
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIStates : MonoBehaviour
    {
        [SerializeField] private AIStateType _curStateType;
        [SerializeField] private AIStateBase _curState;

        public AIStateBase CurState { get { return _curState; } }

        private Dictionary<AIStateType, AIStateBase> _states = new Dictionary<AIStateType, AIStateBase>();

        private NavMeshAgent _navAgent;

        public NavMeshAgent GetNavAgent { get { return _navAgent; } }
        [SerializeField] public Vector3 StartPosition;

        [Header("Field of View")]
        [Tooltip("Range of 0 to 1 float for the FOV I recommend starting at 0.7.")]
        [SerializeField][Range(0, 1)] public float FOVForEnemy = 0.7f;

        [Tooltip("")][SerializeField] public float _RangeAttack = 10f;
        [Tooltip("")][SerializeField] public float _isPlayerInRange = 5f;
        [SerializeField] public GameObject PlayerLastKnowPosition;

        public Vector3 forward;
        public Vector3 toOther;

        public bool CanSeePlayer = false;

        public MonoBehaviour weapon;

        public bool isShoot = false;

        [Tooltip("Set to the player.")][SerializeField] public GameObject Player;

        public void Start()
        {
            Player = PlayerCharacter.Instance.gameObject;

            StartPosition = this.transform.position;
            _navAgent = GetComponent<NavMeshAgent>();

            AIStateBase[] foundStates = GetComponents<AIStateBase>();

            for (int i = 0; i < foundStates.Length; i++)
            {
                if (_states.ContainsKey(foundStates[i].GetAIStateType) == false)
                {
                    _states.Add(foundStates[i].GetAIStateType, foundStates[i]);
                    foundStates[i].InitState(this);
                }
            }
            ChangeState(_curStateType);
        }
        private void Update()
        {
            if (_curState != null)
            {
                ChangeState(_curState.OnStateUpdate());
            }
            if (PlayerLastKnowPosition == null)
            {
                PlayerLastKnowPosition = GameObject.Find("PlayerShadow");
                if (PlayerLastKnowPosition == null)
                {
                    Debug.LogError("Add Prefab PlayerShadow to the scene otherwise the AI wont Function");
                }
            }
        }
        private void ChangeState(AIStateType newState)
        {
            if (_states.ContainsKey(newState) == false)
            {
                return;
            }
            if (_curState == null || _curState.GetAIStateType != newState)
            {
                if (_curState != null)
                {
                    _curState.OnStateExit();
                }

                _curStateType = newState;

                _curState = _states[newState];
                _curState.OnStateEnter();
            }
        }
    }
}
