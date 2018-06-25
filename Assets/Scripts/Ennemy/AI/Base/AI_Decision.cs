using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI_Decision : ScriptableObject {
    public abstract bool Decide(Enemy enemy);
}
