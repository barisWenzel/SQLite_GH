using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace SQLite_GH
{
    public class CreateConnectionString : GH_Component
    {

        public CreateConnectionString() : base("CreateConnectionString", "CreateConnectionString", "CreateConnectionString", "SQLiterich", "Create"){ }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Dir", "Dir", "Path to database", GH_ParamAccess.item);
        }


        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("CS", "CS", "CS", GH_ParamAccess.item);
        }


        protected override void SolveInstance(IGH_DataAccess DA)
        {

            string dir = string.Empty;
            if (!DA.GetData(0, ref dir)) return;

            Database.SetUpConnectionString(dir);
            DA.SetData(0, Database.connection.ConnectionString);
         

            Message = "CreateCS";
        }


        protected override System.Drawing.Bitmap Icon
        {
            get { return null; }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("4f527fb7-b137-44d6-82e9-3965d63d6a1c"); }
        }
    }
}