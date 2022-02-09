using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace SQLite_GH
{
    public class GetColumns : GH_Component
    {

        public GetColumns() : base("GetColumns", "GetColumns", "GetColumns", "SQLiterich", "Read") {  }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("CS", "CS", "Connection string", GH_ParamAccess.item);
            pManager.AddTextParameter("Tables", "Tables", "Tables names'", GH_ParamAccess.item);

        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Columns", "Columns", "Columns ", GH_ParamAccess.list);
        }


        protected override void SolveInstance(IGH_DataAccess DA)
        {


            string cs = string.Empty;
            if (!DA.GetData(0, ref cs)) return;

            string tname = string.Empty;
            if (!DA.GetData(1, ref tname)) return;

            SQLiteConnection connection = new SQLiteConnection("Data Source=" + cs + ";Version=3;");
            DA.SetDataList(0, Database.GetColumns(tname, connection));

            Message = "GetColumns";
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            { return null; }
        }


        public override Guid ComponentGuid
        {
            get { return new Guid("cd1e027e-7e2f-4059-b8b5-0aee349ce2c6"); }
        }
    }
}