using UnityEngine;
using Cysharp.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace H00N.DataTables
{
    public class DataTableSO<TTable, TRow> : ScriptableObject where TTable : DataTable<TRow> where TRow : DataTableRow
    {
        public int id = 0;

        protected TRow tableRow = null;
        public TRow TableRow { 
            get {
                tableRow ??= GetTableRow();
                return tableRow;
            }
        }

        protected virtual async void OnEnable()
        {
            #if UNITY_EDITOR
            if (EditorApplication.isPlayingOrWillChangePlaymode == false)
                return;
            #endif
        
            await UniTask.WaitUntil(() => DataTableManager.Initialized);
            tableRow = GetTableRow();
        }

        private TRow GetTableRow()
        {
            TTable table = DataTableManager.GetTable<TTable>();
            if (table == null)
                return null;

            return table.GetRow(id);
        }
    }
}