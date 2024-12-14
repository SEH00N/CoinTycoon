using System;
using H00N.DataTables;

namespace ProjectCoin.DataTables
{
    [Serializable]
    public class CropTableRow : DataTableRow
    {
        public int growthStep;
        public int growthRate;
        public int productCropID;
        public string nameLocalKey;
    }

    public class CropTable : DataTable<CropTableRow> { }
}