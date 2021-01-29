using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGIS.ShapeFileLib;
using System.Drawing;
using DotSpatial.Controls;
using DotSpatial.Data;

namespace warmf
{
    public class STechShapes
    {
        public static bool CreateShapeFile(string fileType, string directory, List<string> fileNames)
        //fileType is: MET, PTS, FLO, AIR, ORH, ORC
        //if ORH is passed, OLH files are also included
        //if ORC is passed, OLC files are also included
        {
            //delete previous version
            if (File.Exists(Global.DIR.SHP + fileType + ".shp"))
            {
                File.Delete(Global.DIR.SHP + fileType + ".shp");
            }
            if (File.Exists(Global.DIR.SHP + fileType + ".shx"))
            {
                File.Delete(Global.DIR.SHP + fileType + ".shx");
            }
            if (File.Exists(Global.DIR.SHP + fileType + ".dbf"))
            {
                File.Delete(Global.DIR.SHP + fileType + ".dbf");
            }
            if (File.Exists(Global.DIR.SHP + fileType + ".cpg"))
            {
                File.Delete(Global.DIR.SHP + fileType + ".cpg");
            }

            try
            {
                // Create fields for the name and file name
                System.Data.DataColumn nameColumn = new System.Data.DataColumn(FormMain.FIELDNAMENAME);
                System.Data.DataColumn fileNameColumn = new System.Data.DataColumn(FormMain.FIELDNAMEFILENAME);

                // Add the fields to a feature set
                FeatureSet theFeatureSet = new FeatureSet(FeatureType.Point);
                theFeatureSet.DataTable.Columns.Add(nameColumn);
                theFeatureSet.DataTable.Columns.Add(fileNameColumn);

                for (int i = 0; i < fileNames.Count; i++)
                {
                    DataFile dataFile = new DataFile();
                    STechStreamReader sr = new STechStreamReader(directory + fileNames[i]);
                    dataFile.ReadVersionLatLongName(ref sr);
                    sr.Close();
                    if (dataFile.CoordinatesInRange())
                    {
                        // Add the field values for each file
                        GeoAPI.Geometries.Coordinate thePoint = new GeoAPI.Geometries.Coordinate(dataFile.longitude, dataFile.latitude);
                        theFeatureSet.Features.Add(thePoint);
                        theFeatureSet.Features[theFeatureSet.Features.Count - 1].DataRow[0] = dataFile.shortName;
                        theFeatureSet.Features[theFeatureSet.Features.Count - 1].DataRow[1] = fileNames[i];
                    }
                }

                // Write the shapefile
                theFeatureSet.SaveAs(Global.DIR.SHP + fileType + ".shp", true);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Exception/Error", MessageBoxButtons.OK);
                return false;
                throw;
            }
        }

        public void DrawMarker(EGIS.Controls.SFMap frmMap, Graphics g, double longitude, double latitude)
        {
            //not used at the moment, but retained in case we need it later
            System.Drawing.Point pt = frmMap.GisPointToPixelCoord(longitude, latitude);
            int width = 10;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.DrawLine(Pens.Red, pt.X, pt.Y - width, pt.X, pt.Y + width);
            g.DrawLine(Pens.Red, pt.X - width, pt.Y, pt.X + width, pt.Y);
            pt.Offset(-width / 2, -width / 2);
            g.FillEllipse(Brushes.Yellow, pt.X, pt.Y, width, width);
            g.DrawEllipse(Pens.Red, pt.X, pt.Y, width, width);
        }
    }
}
