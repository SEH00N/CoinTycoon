using System;
using UnityEngine;

namespace H00N
{
    [Serializable]
    public class Stat
    {
        [SerializeField] float baseValue = 10f;

        private float currentValue = 10f;
        public float CurrentValue => currentValue;

        private StatModifiers modifiers = new StatModifiers();

        public event Action<float> OnValueChangedEvent = null;

        public void Init()
        {
            modifiers.Init();
            currentValue = baseValue;
        }

        private void CalculateValue()
        {
            currentValue = baseValue;
            modifiers.CalculateValue(ref currentValue);
            OnValueChangedEvent?.Invoke(currentValue);
        }

        public void AddModifier(EStatModifierType modifierType, float value)
        {
            modifiers[modifierType].Add(value);
            CalculateValue();
        }

        public void RemoveModifier(EStatModifierType modifierType, float value)
        {
            modifiers[modifierType].Remove(value);
            CalculateValue();
        }

        public static implicit operator float(Stat left) => left.CurrentValue;
        public static implicit operator int(Stat left) => (int)left.CurrentValue;
    }
}