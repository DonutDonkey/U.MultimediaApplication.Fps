using UnityEngine;

namespace Script.So {
    [CreateAssetMenu(fileName = "New Player Container", menuName = "Containers/Player Container")]
    public class PlayerContainer : SingletonScriptableObj<PlayerContainer>, ISerializationCallbackReceiver {
        [SerializeField] private Runtime_IntValue player_health;
        [SerializeField] private Runtime_IntValue player_armor;
        
        // Just hold runtime ref to weapon SO's LUL

        [SerializeField] private Runtime_IntValue player_ammo_1;
        [SerializeField] private Runtime_IntValue player_ammo_2;
        [SerializeField] private Runtime_IntValue player_ammo_3;
        
        public void SetDefaultValues() {
            //Event?
        }
        //Custom calls for serialization, used for dicitonaries, custom types etc, using so all run values can only be changed at runtime
        public void OnBeforeSerialize() { }
        public void OnAfterDeserialize() { }
    }

    [System.Serializable]
    public class Serialized_Data {
        [SerializeField] private int player_health;
        [SerializeField] private int player_armor;
        
        //Use odin inspector serializer for dictionary?
        // Use list containing id's of ackuired weapons?
        // private Dictionary<weapon_id, bool> player_weapon_list

        // Create Int Value to hold those, and load at runtime
        [SerializeField] private int player_ammo_1;
        [SerializeField] private int player_ammo_2;
        [SerializeField] private int player_ammo_3;
    };
}