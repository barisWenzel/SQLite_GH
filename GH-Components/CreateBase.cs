using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using static SQLite_GH.ButtonMakeDB;

namespace SQLite_GH
{
    public class CreateBase : GH_Component
    {

        public CreateBase() : base("CreateBase", "CreateBase", "CreateBase", "SQLiterich", "Create") { }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("DBName", "DBName", "Database's name", GH_ParamAccess.item);
            pManager.AddTextParameter("Dir", "Dir", "Database dir", GH_ParamAccess.item);
        }

        #region Button

        public override void CreateAttributes()
        {
            m_attributes = new CustomAttributes(this);
        }

        public bool Run;

        #endregion

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("CS", "CS", "CS", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string name = string.Empty;
            if (!DA.GetData(0, ref name)) return;

            string dir = string.Empty;
            if (!DA.GetData(1, ref dir)) return;

            if(Run)
            Database.CreateDatabase(name, dir);

            if (Database.connection!=null)
            DA.SetData(0, Database.connection.ConnectionString);

            Message = "CreateDB";


        }

        protected override System.Drawing.Bitmap Icon
        {
            get { return null; }
        }


        public override Guid ComponentGuid
        {
            get { return new Guid("2991CA20-72E3-4771-8B09-6BA847C090EB"); }
        }
    }
}
