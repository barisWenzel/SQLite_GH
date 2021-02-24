using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using static SQLite_GH.ButtonEntry;
using System.Data.SQLite;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Data;


namespace SQLite_GH
{
    public class CreateEntries : GH_Component
    {

        public CreateEntries() : base("CreateEntries", "CreateEntries", "CreateEntries", "SQLiterich", "Create") { }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("CS", "CS", "Connection string", GH_ParamAccess.item);
            pManager.AddTextParameter("Tables", "Tables", "Tables names'", GH_ParamAccess.item);
            pManager.AddTextParameter("Columns", "Columns", "Columns names", GH_ParamAccess.list);
            pManager.AddTextParameter("Values", "Values", "Values", GH_ParamAccess.tree);
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

        }


        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string cs = string.Empty;
            if (!DA.GetData(0, ref cs)) return;

            string tname = string.Empty;
            if (!DA.GetData(1, ref tname)) return;

            var columns = new List<GH_String>();
            if (!DA.GetDataList(2, columns)) return;

            var values = new GH_Structure<GH_String>();
            if (!DA.GetDataTree(3, out values)) return;


            SQLiteConnection sConn = new SQLiteConnection(cs);
            if (Run)
            {
                Database.CreateEntry(sConn, tname, columns, values);
            }
            Message = "CreateEntries";
        }


        protected override System.Drawing.Bitmap Icon
        {
            get { return null; }
        }


        public override Guid ComponentGuid
        {
            get { return new Guid("bd67a3f1-df36-4d87-9414-f91a64fd663e"); }
        }
    }
}