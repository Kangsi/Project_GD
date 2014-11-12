using UnityEngine;
using System.Collections;

namespace Manager
{
    public class IDManager : MonoBehaviour
    {
        private static int spellID;
        public static int GetSpellID() { return spellID; }
        public static void AddSpellID() { spellID++; }
    }
}
