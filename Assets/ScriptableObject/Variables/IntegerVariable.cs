using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Raskulls.Variables
{

    [CreateAssetMenu(fileName = "NewIntVariable", menuName = "Raskulls/Variables/IntegerVariable")]
    public class IntegerVariable : ScriptableObject
    {
        public int Value;
    }
}
