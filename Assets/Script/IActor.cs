using UnityEngine;

namespace Script {
    /// <summary>
    /// General interface used for actors classes. Should contain methods that will help get information required for
    /// BRAIN in AI, Saving.
    /// Methods should be general enough to apply to both ENEMY and BARREL
    /// BARRELS and dynamic / requiring saving props, are actors too
    /// Tka to ja kondziu pisze te dokumentacje, :mental:
    /// </summary>
    public interface IActor {
        public Vector3 GetPosition();
        public Transform GetTransform();
        public bool IsDead();
    }
}