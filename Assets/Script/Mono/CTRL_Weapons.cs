using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script.Mono {
    public class CTRL_Weapons : MonoBehaviour {
        [SerializeField] private List<GameObject> weapons_reffs;
        
        private Dictionary<string, GameObject> weapons;

        private void Awake() {
            weapons = new Dictionary<string, GameObject>();
            
            foreach (var wr in weapons_reffs)
                weapons[wr.name] = wr;
        }

        public void WeaponSwitch(string in_wep_id) {
            var w = weapons[in_wep_id];
            if (w.activeSelf) return;

            StartCoroutine(Switch(weapons_reffs.FirstOrDefault(w => w.activeSelf), w));
        }

        IEnumerator Switch(GameObject curr_wep, GameObject nxt_wep) {
            yield return new WaitUntil(() => !curr_wep.activeSelf);
            
            // curr_wep.SetActive(false);
            nxt_wep.SetActive(true);
        }
    }
}