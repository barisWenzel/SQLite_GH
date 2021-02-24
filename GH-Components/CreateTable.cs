using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System.Data.SQLite;
using static SQLite_GH.ButtonTable;

namespace SQLite_GH
{
    public class CreateTable : GH_Component
    {
        public CreateTable() : base("CreateTable", "CreateTable", "CreateTable", "SQLiterich", "Create") { }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("CS", "CS", "Connection string", GH_ParamAccess.item);
            pManager.AddTextParameter("Tables", "Tables", "Tables names'", GH_ParamAccess.item);
            pManager.AddTextParameter("Columns", "Columns", "Columns names'", GH_ParamAccess.list);
            pManager.AddTextParameter("Datatype", "Datatype", "Datatype name", GH_ParamAccess.list);
            pManager.AddBooleanParameter("Override", "Override", "Override", GH_ParamAccess.item, false);
        }



        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
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

            var columns = new List<GH_String>();
            if (!DA.GetDataList(2, columns)) return;


            var datatypes = new List<string>();
            if (!DA.GetDataList(3, datatypes)) return;

            bool ov = false;
            if (!DA.GetData(4, ref ov)) return;


            SQLiteConnection sConn = new SQLiteConnection(cs);
            if(Run)
            Database.CreateTable( sConn ,tname,columns,datatypes,ov);

            Message = "CreateTable";

        }

        protected override System.Drawing.Bitmap Icon
        {
            get { return null; }
        }


        public override Guid ComponentGuid
        {
            get { return new Guid("B212E3EB-D847-4D1F-9361-22CBA7E03941"); }
        }
    }
}