using Script.Mono.AI;
using UnityEngine;

namespace Script.So.AI
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide (AI_Brain brain);
    }
}