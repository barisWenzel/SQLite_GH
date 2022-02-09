using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Data.SQLite;
using static SQLite_GH.ButtonGetTables;

namespace SQLite_GH
{
    public class GetTables : GH_Component
    {

        public GetTables() : base("GetTables", "GetTables", "GetTables", "SQLiterich", "Read") { }
        #region button
        public override void CreateAttributes()
        {
            m_attributes = new CustomAttributes(this);
        }

        public bool Run;

        #endregion

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("CS", "CS", "connection string", GH_ParamAccess.item);
        }


        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Tables", "Tables", "Tables ", GH_ParamAccess.list);
        }


        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string cs = string.Empty;
            if (!DA.GetData(0, ref cs)) return;

            SQLiteConnection connection = new SQLiteConnection("Data Source=" + cs + ";Version=3;");
            if (Run)
                ExpireSolution(true);
            DA.SetDataList(0, Database.GetTableNames(connection));

            Message = "GetTables";
        }


        protected override System.Drawing.Bitmap Icon
        {
            get{return null;}
        }


        public override Guid ComponentGuid
        {
            get { return new Guid("c1c5c5f9-3a42-4a95-8d60-c30071d8decb"); }
        }
    }
}