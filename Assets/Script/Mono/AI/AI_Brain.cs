using System;
using Script.So.AI;
using UnityEngine;
using UnityEngine.AI;

namespace Script.Mono.AI {
    //Mono attach object for each Actor that requires artifical thinking, if needed extend for instance to 
    // Flying Enemy brain etc
    // IS a connector between SO delegates, and the scene. 
    public class AI_Brain : MonoBehaviour, IBrain {
        // Set 'incentives' - refference player, and other different enemy tag when damaged etc
        // Set initial stae
        // Run Brain
        
        [Header("Object refferences")]
        [SerializeField] private GameObject GO_self;
        [SerializeField] private GameObject GO_default_poi;
        
        private IActor A_self;
        private IActor A_current_poi;
        
        [SerializeField] private GameObject [] POI_idle_walk_points;
        [SerializeField] private GameObject [] POI_actors;

        public State currentState;
        public State remainState;
        
        [HideInInspector] public float stateTimeElapsed;
        public NavMeshAgent agent;
        public int nextWaypoint;
        
        public IActor ASelf
        {
            get => A_self;
            set => A_self = value;
        }
        public IActor ACurrentPoi
        {
            get => A_current_poi;
            set => A_current_poi = value;
        }

        public GameObject[] PoiIdleWalkPoints
        {
            get => POI_idle_walk_points;
            set => POI_idle_walk_points = value;
        }

        private void Awake()
        {
            A_self = GO_self.GetComponent<IActor>();
            agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            A_current_poi = GO_default_poi.GetComponent<IActor>();
        }

        private void Update() {
            currentState.UpdateState(this);
            
            Debug.Log($"Current state: {currentState.name}");
            
            // ONLY TO SEE IF IT CORRECTLY CAPTURES THE IACTORS
            //Debug.Log($"{A_self.GetType()} : " + A_self.GetTransform().position);
            //Debug.Log($"{A_current_poi.GetType()}: " + A_current_poi.IsDead());
        }
        public void TransitionToState(State nextState)
        {
            //if (nextState != remainState) 
            //{
                currentState = nextState;
                OnExitState ();
            //}
        }
        public bool CheckIfCountDownElapsed(float duration)
        {
            stateTimeElapsed += Time.deltaTime;
            return (stateTimeElapsed >= duration);
        }

        private void OnExitState()
        {
            stateTimeElapsed = 0;
        }

        public GameObject GetSelfGameObject()
        {
            return GO_self;
        }
    }
}