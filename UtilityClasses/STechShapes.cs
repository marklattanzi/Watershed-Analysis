using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGIS.ShapeFileLib;

namespace warmf
{
    

    class STechShapes
    {
        public void CreateShapeFile(string fileName)
        {

            //ShapeFile sf = new ShapeFile(Global.DIR.SHP + "Untitled_Point.shp");
            //DbfReader dbfReader = new DbfReader(Global.DIR.SHP + "Untitled_Point.shp");

            ////create a new ShapeFileWriter
            //ShapeFileWriter sfw;
            //sfw = ShapeFileWriter.CreateWriter(Global.DIR.SHP, fileName, sf.ShapeType,
            //    dbfReader.DbfRecordHeader.GetFieldDescriptions());
            //try
            //{
            //    // add records to the new shapefile

            //    // Get a ShapeFileEnumerator from the shapefile
            //    // and read each record
            //    ShapeFileEnumerator sfEnum = sf.GetShapeFileEnumerator();
            //    while (sfEnum.MoveNext())
            //    {
            //        // get the raw point data
            //        PointD[] points = sfEnum.Current[0];
            //        //get the DBF record
            //        string[] fields = dbfReader.GetFields(sfEnum.CurrentShapeIndex);
            //        //check whether to add the record to the new shapefile.
            //        //(in this example, field zero contains the road type)
            //        if (string.Compare(fields[0].Trim(), "highway", true) == 0)
            //        {
            //            sfw.AddRecord(points, points.Length, fields);
            //        }
            //    }
            //}
            //finally
            //{
            //    //close the shapefile, shapefilewriter and dbfreader
            //    sfw.Close();
            //    sf.Close();
            //    dbfReader.Close();
            //}
        }
    }
}
