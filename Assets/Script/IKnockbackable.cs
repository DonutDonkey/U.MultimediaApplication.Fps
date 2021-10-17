using UnityEngine;

namespace Script {
    public interface IKnockbackable {
        void KnockBack(Vector3 other, float force);
    }
}