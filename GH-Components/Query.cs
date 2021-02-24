using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace SQLite_GH.GH_Components
{
    public class Query : GH_Component
    {

        public Query() : base("Query", "Nickname", "Description", "Category", "Subcategory") { }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("CS", "CS", "connection string", GH_ParamAccess.item);
            pManager.AddTextParameter("Query", "Query", "Query", GH_ParamAccess.item);
        }


        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Data", "Data", "Data", GH_ParamAccess.tree);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {

            string cs = string.Empty;
            if (!DA.GetData(0, ref cs)) return;

            string query = string.Empty;
            if (!DA.GetData(1, ref query)) return;





        }


        protected override System.Drawing.Bitmap Icon
        {
            get {return null;}
        }


        public override Guid ComponentGuid
        {
            get { return new Guid("15c40173-25b3-4d98-92f1-b6bb68113940"); }
        }
    }
}