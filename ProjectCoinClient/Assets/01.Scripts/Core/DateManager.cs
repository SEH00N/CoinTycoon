using System;
using UnityEngine;

namespace ProjectCoin
{
    public class DateManager : MonoBehaviour
    {
        private static DateManager instance = null;
        public static DateManager Instance => instance;

        [SerializeField] float tickDuration = 7f;
        [SerializeField] int dayCycleTickCount = 43;

        public event Action OnTickCycleEvent = null;
        public event Action OnDateCycleEvent = null;

        private float timer = 0f;

        private int dayCounter = 0;
        private int tickCounter = 0;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if(timer <= 0f)
            {
                timer = tickDuration;
                HandleTick();

                if(tickCounter > dayCycleTickCount)
                {
                    tickCounter = 0;
                    HandleDateChanged();
                }
            }
        }

        private void HandleDateChanged()
        {
            Debug.Log("Date Cycle");

            dayCounter++;
            OnDateCycleEvent?.Invoke();
        }

        private void HandleTick()
        {
            Debug.Log("Tick Cycle");

            tickCounter++;
            OnTickCycleEvent?.Invoke();
        }
    }
}