using System;
using System.Collections.Generic;
using UnityEngine;

namespace H00N
{
    [CreateAssetMenu(menuName = "SO/Stat")]
    public class StatSO : ScriptableObject
    {
        [Serializable]
        public class StatSlot
        {
            public EStatType statType;
            public Stat stat;
        }

        public List<StatSlot> stats = new List<StatSlot>();
        private Dictionary<EStatType, Stat> statDictionary;
        public Stat this[EStatType indexer]
        {
            get
            {
                if (statDictionary.ContainsKey(indexer) == false)
                {
                    Debug.LogWarning("Stat of Given Type is Doesn't Existed");
                    return null;
                }

                return statDictionary[indexer];
            }
        }

        public event Action OnStatChangedEvent = null;

        private void OnEnable()
        {
            statDictionary = new Dictionary<EStatType, Stat>();
            stats.ForEach(i =>
            {
                if (statDictionary.ContainsKey(i.statType))
                {
                    Debug.LogWarning("Stat of Current Type is Already Existed");
                    return;
                }
                i.stat.Init();
                statDictionary.Add(i.statType, i.stat);
            });
            OnStatChangedEvent?.Invoke();
        }

        public void AddModifier(StatModifierSlot modifierSlot) =>
            AddModifier(modifierSlot.statType, modifierSlot.modifierType, modifierSlot.value);

        public void RemoveModifier(StatModifierSlot modifierSlot) =>
            RemoveModifier(modifierSlot.statType, modifierSlot.modifierType, modifierSlot.value);

        public void AddModifier(EStatType statType, EStatModifierType modifierType, float value)
        {
            this[statType]?.AddModifier(modifierType, value);
            OnStatChangedEvent?.Invoke();
        }

        public void RemoveModifier(EStatType statType, EStatModifierType modifierType, float value)
        {
            this[statType]?.RemoveModifier(modifierType, value);
            OnStatChangedEvent?.Invoke();
        }
    }
}