using UnityEngine;

namespace Script.So {
    [CreateAssetMenu(fileName = "New Player Container", menuName = "Containers/Player Container")]
    public class PlayerContainer : SingletonScriptableObj<PlayerContainer> {
        [SerializeField] private Runtime_IntValue player_health;
        [SerializeField] private Runtime_IntValue player_armor;
        
        // Just hold runtime ref to weapon SO's LUL

        [SerializeField] private Runtime_IntValue player_ammo_1;
        [SerializeField] private Runtime_IntValue player_ammo_2;
        [SerializeField] private Runtime_IntValue player_ammo_3;

        [SerializeField] private BoolReference ID_WEAPON_2;
        [SerializeField] private BoolReference ID_WEAPON_3;
        [SerializeField] private BoolReference ID_WEAPON_4;

        public BoolReference IDWeapon2 => ID_WEAPON_2;
        public BoolReference IDWeapon3 => ID_WEAPON_3;
        public BoolReference IDWeapon4 => ID_WEAPON_4;

        public void SetDefaultValues() {
            ID_WEAPON_2.Value = false;
            ID_WEAPON_3.Value = false;
            ID_WEAPON_4.Value = false;
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