using Script.Mono.AI;
using UnityEngine;

namespace Script.So.AI
{
    public abstract class Action : ScriptableObject
    {
        public bool acting = false;
        public abstract void Act(AI_Brain brain);
    }
}