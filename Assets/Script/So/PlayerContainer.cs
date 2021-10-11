using UnityEngine;

namespace Script.So {
    [CreateAssetMenu(fileName = "New Player Container", menuName = "Containers/Player Container")]
    public class PlayerContainer : SingletonScriptableObj<PlayerContainer>, ISerializationCallbackReceiver {
        [SerializeField] private IntValue player_health;
        [SerializeField] private IntValue player_armor;
        
        // Just hold runtime ref to weapon SO's LUL

        [SerializeField] private IntValue player_ammo_1;
        [SerializeField] private IntValue player_ammo_2;
        [SerializeField] private IntValue player_ammo_3;

        //Custom calls for serialization, used for dicitonaries, custom types etc, using so all run values can only be changed at runtime
        public void OnBeforeSerialize() {
            player_health.value = 0;
            player_armor.value = 0;
            player_ammo_1.value = 0;
            player_ammo_2.value = 0;
            player_ammo_3.value = 0;
        }

        public void OnAfterDeserialize() { }

        public void SetDefaultValues() {
            //Event?
        }
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