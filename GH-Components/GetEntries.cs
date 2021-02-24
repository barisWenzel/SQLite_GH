using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Data.SQLite;
using static SQLite_GH.ButtonRead;

namespace SQLite_GH
{
    public class GetEntries : GH_Component
    {

        public GetEntries() : base("GetEntries", "GetEntries", "GetEntries", "SQLiterich", "Read") { }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("CS", "CS", "Connection string", GH_ParamAccess.item);
            pManager.AddTextParameter("Tables", "Tables", "Tables names'", GH_ParamAccess.item);
            pManager.AddTextParameter("Column", "Column", "Column name", GH_ParamAccess.item);
        }


        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Data", "Data", "Data", GH_ParamAccess.list);
        }

        #region Button

        public override void CreateAttributes()
        {
            m_attributes = new CustomAttributes(this);
        }

        public bool Run;

        #endregion

        protected override void SolveInstance(IGH_DataAccess DA)
        {

            string cs = string.Empty;
            if (!DA.GetData(0, ref cs)) return;

            string tname = string.Empty;
            if (!DA.GetData(1, ref tname)) return;

            var columns = string.Empty;
            if (!DA.GetData(2, ref columns)) return;

            if (Run)
                ExpireSolution(true);

            SQLiteConnection connection = new SQLiteConnection(cs);

            List<System.Object> data = Database.ReadValues(connection, columns, tname);
                       DA.SetDataList(0, data);
            Message = "ReadEntries";
        }

        protected override System.Drawing.Bitmap Icon
        {
            get { return null; }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("1cec0af3-22dd-463f-b039-3adbfe1a5f9f"); }
        }
    }
}