using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Grasshopper.Kernel;
using Grasshopper;
using Grasshopper.Kernel.Data;

namespace SQLite_GH.GH_Components
{
    public class Query : GH_Component
    {

        public Query() : base("Query", "Nickname", "Description", "Category", "Subcategory") { }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("db_Path", "db_Path", "db_Path", GH_ParamAccess.item);
            pManager.AddGenericParameter("Query", "Query", "Query", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Data", "Data", "Data", GH_ParamAccess.tree);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            //Retrive the inputs
            string cs = string.Empty;
            if (!DA.GetData(0, ref cs)) return;

            string query = string.Empty;
            if (!DA.GetData(1, ref query)) return;

            SQLiteConnection connection = new SQLiteConnection("Data Source=" + cs + ";Version=3;");

            DataTree<System.Object> data = new DataTree<System.Object>();

            connection.Open();
            using (SQLiteCommand fmd = connection.CreateCommand())
            {
                fmd.CommandText = query;
                fmd.CommandType = CommandType.Text;
                SQLiteDataReader r = fmd.ExecuteReader();

                while (r.Read())
                {
                    for (int i = 0; i < r.FieldCount; i++)
                    {
                        data.Add(r.GetValue(i), new GH_Path(i));
                    }

                }
            }
            DA.SetDataTree(0, data);
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