using UnityEngine;

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

        private void Awake() {
            A_self = GO_self.GetComponent<IActor>();
            A_current_poi = GO_default_poi.GetComponent<IActor>();
        }

        private void Update() {
            // ONLY TO SEE IF IT CORRECTLY CAPTURES THE IACTORS
            Debug.Log($"{A_self.GetType()} : " + A_self.IsDead());
            Debug.Log($"{A_current_poi.GetType()}: " + A_current_poi.IsDead());
        }
    }
}