using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using CodeGenerate.Util;
using CodeGenerate.Model;

namespace CodeGenerate.Control
{
    class WordControl
    {
        public static void Generate(DataGridView dgvTables, bool isPascal, EnumDbType dbType)
        {
            var listSql = new List<string>();
            List<Table> list = TableControl.GetCheckedTableName(dgvTables, isPascal);

            if (list.Count == 0)
            {
                MessageBox.Show(ConstantUtil.CheckedNoTable);
                return;
            }

            var ds = new DataSet();
           
            foreach (Table item in list)
            {
                DataTable dtColumns = ColumnControl.GetTableColumnsByTableName(item.TableName, dbType);
                dtColumns.TableName = item.TabPascalName + "$" + item.Comments;
                bool b = ColumnControl.GetColRelaPascalName(dtColumns);
                if (isPascal && !b)
                {
                    MessageBox.Show(ConstantUtil.NoRelaPascalCol);
                    return;
                }
                ds.Tables.Add(dtColumns);
            }

            CreateWordUtil.Create(ds, dbType);
        }
    }
}
