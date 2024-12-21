using UnityEngine;

namespace ProjectCoin.Farms
{
    public class Farm : MonoBehaviour
    {
        [SerializeField] EggStorage eggStorage = null;
        public EggStorage EggStorage => eggStorage;
        
        [SerializeField] CropStorage cropStorage = null;
        public CropStorage CropStorage => cropStorage;
    }
}
