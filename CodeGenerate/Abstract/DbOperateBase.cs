using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeGenerate.Abstract
{
    public abstract class DbOperateBase
    {
        #region Fields
        protected StringBuilder SbTemp;
        protected string TableName;
        protected string PreNs;
        protected string PreModel;
        protected CheckedListBox CheckedListBox1;
        #endregion

        #region Methods
        public virtual StringBuilder Generate()
        {
            return new StringBuilder();
        }

        protected virtual void concatNamespace()
        {
            ;
        }

        protected virtual void concatGet()
        {
            ;
        }

        protected virtual void concatInsert()
        {
            ;
        }

        protected virtual void concatUpdate()
        {
            ;
        }

        protected virtual void concatDelete()
        {
            ;
        }
        #endregion
    }
}
