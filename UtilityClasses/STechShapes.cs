using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGIS.ShapeFileLib;
using System.Drawing;

namespace warmf
{
    public class STechShapes
    {
        public static bool CreateShapeFile(string fileType)
        //fileType is: MET, PTS, FLO, AIR, ORH, ORC
        //if ORH is passed, OLH files are also included
        //if ORC is passed, OLC files are also included
        {
            double[] coords = new double[2];
            string[] fields = new string[2];

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

            //readers and writers
            ShapeFile sf = new ShapeFile(Global.DIR.SHP + "Untitled_Point.shp");
            DbfReader dbfReader = new DbfReader(Global.DIR.SHP + "Untitled_Point.shp");
            ShapeFileWriter sfw = ShapeFileWriter.CreateWriter(Global.DIR.SHP, fileType, sf.ShapeType,
                dbfReader.DbfRecordHeader.GetFieldDescriptions());
            
            try
            {
                if (fileType == "MET")
                {
                    for (int i = 0; i < Global.coe.numMETFiles; i++)
                    {
                        METFile dataFile = new METFile(Global.DIR.MET + Global.coe.METFilename[i]);
                        STechStreamReader sr = new STechStreamReader(Global.DIR.MET + Global.coe.METFilename[i]);
                        dataFile.ReadVersionLatLongName(ref sr);
                        coords[0] = dataFile.longitude;
                        coords[1] = dataFile.latitude;
                        fields[0] = dataFile.shortName;
                        fields[1] = Global.coe.METFilename[i];
                        sfw.AddRecord(coords, 1, fields);
                        dataFile = null;
                        sr = null;
                        Array.Clear(coords, 0, coords.Length);
                        Array.Clear(fields, 0, fields.Length);
                    }
                }
                if (fileType == "PTS")
                {
                    for (int i = 0; i < Global.coe.numPTSFiles; i++)
                    {
                        PTSFile dataFile = new PTSFile(Global.DIR.PTS + Global.coe.PTSFilename[i]);
                        STechStreamReader sr = new STechStreamReader(Global.DIR.PTS + Global.coe.PTSFilename[i]);
                        dataFile.ReadVersionLatLongName(ref sr);
                        coords[0] = dataFile.longitude;
                        coords[1] = dataFile.latitude;
                        fields[0] = dataFile.shortName;
                        fields[1] = Global.coe.PTSFilename[i];
                        sfw.AddRecord(coords, 1, fields);
                        dataFile = null;
                        sr = null;
                        Array.Clear(coords, 0, coords.Length);
                        Array.Clear(fields, 0, fields.Length);
                    }
                }
                if (fileType == "FLO")
                {
                    for (int i = 0; i < Global.coe.numAIRFiles; i++)
                    {
                        FLOFile dataFile = new FLOFile(Global.DIR.FLO + Global.coe.DIVData[i].filename);
                        STechStreamReader sr = new STechStreamReader(Global.DIR.FLO + Global.coe.DIVData[i].filename);
                        dataFile.ReadVersionLatLongName(ref sr);
                        coords[0] = dataFile.longitude;
                        coords[1] = dataFile.latitude;
                        fields[0] = dataFile.shortName;
                        fields[1] = Global.coe.DIVData[i].filename;
                        sfw.AddRecord(coords, 1, fields);
                        dataFile = null;
                        sr = null;
                        Array.Clear(coords, 0, coords.Length);
                        Array.Clear(fields, 0, fields.Length);
                    }
                }
                if (fileType == "AIR")
                {
                    for (int i = 0; i < Global.coe.numAIRFiles; i++)
                    {
                        AIRFile dataFile = new AIRFile(Global.DIR.AIR + Global.coe.AIRFilename[i]);
                        STechStreamReader sr = new STechStreamReader(Global.DIR.AIR + Global.coe.AIRFilename[i]);
                        dataFile.ReadVersionLatLongName(ref sr);
                        coords[0] = dataFile.longitude;
                        coords[1] = dataFile.latitude;
                        fields[0] = dataFile.shortName;
                        fields[1] = Global.coe.AIRFilename[i];
                        sfw.AddRecord(coords, 1, fields);
                        dataFile = null;
                        sr = null;
                        Array.Clear(coords, 0, coords.Length);
                        Array.Clear(fields, 0, fields.Length);
                    }
                }
                if (fileType == "ORH") //also includes OLH
                {
                    for (int i = 0; i < Global.coe.numRivers; i++)
                    {
                        if (Global.coe.rivers[i].hydrologyFilename != "")
                        {
                            ObservedFile dataFile = new ObservedFile(Global.DIR.ORH + Global.coe.rivers[i].hydrologyFilename);
                            STechStreamReader sr = new STechStreamReader(Global.DIR.ORH + Global.coe.rivers[i].hydrologyFilename);
                            dataFile.ReadVersionLatLongName(ref sr);
                            coords[0] = dataFile.longitude;
                            coords[1] = dataFile.latitude;
                            fields[0] = dataFile.shortName;
                            fields[1] = Global.coe.rivers[i].hydrologyFilename;
                            sfw.AddRecord(coords, 1, fields);
                            dataFile = null;
                            sr = null;
                            Array.Clear(coords, 0, coords.Length);
                            Array.Clear(fields, 0, fields.Length);
                        }
                    }
                    for (int i = 0; i < Global.coe.numReservoirs; i++) //OLH defined at reservoir level (not reservoir segment)
                    {
                        if (Global.coe.reservoirs[i].hydrologyFilename != "")
                        {
                            ObservedFile dataFile = new ObservedFile(Global.DIR.OLH + Global.coe.reservoirs[i].hydrologyFilename);
                            STechStreamReader sr = new STechStreamReader(Global.DIR.OLH + Global.coe.reservoirs[i].hydrologyFilename);
                            dataFile.ReadVersionLatLongName(ref sr);
                            coords[0] = dataFile.longitude;
                            coords[1] = dataFile.latitude;
                            fields[0] = dataFile.shortName;
                            fields[1] = Global.coe.reservoirs[i].hydrologyFilename;
                            sfw.AddRecord(coords, 1, fields);
                            dataFile = null;
                            sr = null;
                            Array.Clear(coords, 0, coords.Length);
                            Array.Clear(fields, 0, fields.Length);
                        }
                    }
                }
                if (fileType == "ORC") //also includes OLC
                {
                    for (int i = 0; i < Global.coe.numRivers; i++)
                    {
                        if (Global.coe.rivers[i].obsWQFilename != "")
                        {
                            ObservedFile dataFile = new ObservedFile(Global.DIR.ORC + Global.coe.rivers[i].obsWQFilename);
                            STechStreamReader sr = new STechStreamReader(Global.DIR.ORC + Global.coe.rivers[i].obsWQFilename);
                            dataFile.ReadVersionLatLongName(ref sr);
                            coords[0] = dataFile.longitude;
                            coords[1] = dataFile.latitude;
                            fields[0] = dataFile.shortName;
                            fields[1] = Global.coe.rivers[i].obsWQFilename;
                            sfw.AddRecord(coords, 1, fields);
                            dataFile = null;
                            sr = null;
                            Array.Clear(coords, 0, coords.Length);
                            Array.Clear(fields, 0, fields.Length);
                        }
                    }
                    for (int i = 0; i < Global.coe.numReservoirs; i++)
                    {
                        for (int j = 0; j < Global.coe.reservoirs[i].numSegments; j++)
                        {
                            if (Global.coe.reservoirs[i].reservoirSegs[j].obsWQFilename != "")
                            {
                                ObservedFile dataFile = new ObservedFile(Global.DIR.OLC + Global.coe.reservoirs[i].reservoirSegs[j].obsWQFilename);
                                STechStreamReader sr = new STechStreamReader(Global.DIR.OLC + Global.coe.reservoirs[i].reservoirSegs[j].obsWQFilename);
                                dataFile.ReadVersionLatLongName(ref sr);
                                coords[0] = dataFile.longitude;
                                coords[1] = dataFile.latitude;
                                fields[0] = dataFile.shortName;
                                fields[1] = Global.coe.reservoirs[i].reservoirSegs[j].obsWQFilename;
                                sfw.AddRecord(coords, 1, fields);
                                dataFile = null;
                                sr = null;
                                Array.Clear(coords, 0, coords.Length);
                                Array.Clear(fields, 0, fields.Length);
                            }
                        }
                    }
                }

                //close the shapefile, shapefilewriter and dbfreader
                sfw.Close();
                sf.Close();
                dbfReader.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Exception/Error", MessageBoxButtons.OK);
                sfw.Close();
                sf.Close();
                dbfReader.Close();
                return false;
                throw;
            }
        }

        public void DrawMarker(EGIS.Controls.SFMap frmMap, Graphics g, double longitude, double latitude)
        {
            //not used at the moment, but retained in case we need it later
            Point pt = frmMap.GisPointToPixelCoord(longitude, latitude);
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
