using H00N.DataTables;
using ProjectCoin.DataTables;
using UnityEngine;

namespace ProjectCoin.Farms
{
    [CreateAssetMenu(menuName = "SO/Farm/CropsData")]
    public class CropsSO : DataTableSO<CropsTable, CropsTableRow>
    {
        public Sprite[] cropPlantSprites = null;
    }
}