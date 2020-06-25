using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.Dialogue
{
    [System.Serializable]
    public class Condition
    {
        public ICondition[] conditions;

        public Condition(string nodeId)
        {
            if (conditions == null)
            {
                conditions = new ICondition[0];
            }
        }
    }

}