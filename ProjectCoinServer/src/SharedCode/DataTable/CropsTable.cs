using System;
using H00N.DataTables;

namespace ProjectCoin.DataTables
{
    [Serializable]
    public class CropsTableRow : DataTableRow
    {
        public int growthStep;
        public int growthRate;
        public int productCropID;
        public string nameLocalKey;
    }

    public class CropsTable : DataTable<CropsTableRow> { }
}