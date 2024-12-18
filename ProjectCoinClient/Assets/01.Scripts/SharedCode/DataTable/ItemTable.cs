using System;
using H00N.DataTables;

namespace ProjectCoin.DataTables
{
    [Serializable]
    public class ItemTableRow : DataTableRow
    {
        public string prefabName;
        public string nameLocalKey;
    }

    public class ItemTable : DataTable<ItemTableRow> { }
}