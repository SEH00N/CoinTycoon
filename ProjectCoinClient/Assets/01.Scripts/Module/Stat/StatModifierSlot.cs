using System;

namespace H00N
{
    [Serializable]
    public struct StatModifierSlot
    {
        public EStatType statType;
        public EStatModifierType modifierType;
        public float value;
    }
}