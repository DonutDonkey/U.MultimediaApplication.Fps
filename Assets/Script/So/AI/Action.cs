using System.Collections;
using System.Collections.Generic;
using Script.Mono.AI;
using UnityEngine;

namespace Script.So.AI
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(AI_Brain brain);
    }
}