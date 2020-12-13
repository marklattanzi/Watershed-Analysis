using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace warmf
{
    public partial class DialogEditLayers : Form
    {
        public List<FormMain.LayerInfo> changedLayers;

        public DialogEditLayers(ref List<FormMain.LayerInfo> layers)
        {
            InitializeComponent();

            changedLayers = new List<FormMain.LayerInfo>();
            for (int i = 0; i < layers.Count; i++)
            {
                lvLayerList.Items.Add(layers[i].Name);
                changedLayers.Add(layers[i]);
            }
        }

        private void btnAddLayer_Click(object sender, EventArgs e)
        {
            // Get the name of the file
            OpenFileDialog openDialog = new OpenFileDialog
            {
                InitialDirectory = Global.DIR.SHP,
                FileName = "",
                DefaultExt = ".wpf",
                Filter = "Shapefiles (.shp)|*.shp|Raster files (.tif)|*.tif",
                FilterIndex = 2,
                Title = "Select Spatial Data File"
            };
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                // Types of layers
                List<string> layerTypes = new List<string>();
                layerTypes.Add("Catchment");
                layerTypes.Add("River");
                layerTypes.Add("Reservoir");
                layerTypes.Add("Data");
                layerTypes.Add("Digital Elevation Model");
                layerTypes.Add("Land Use / Land Cover");
                layerTypes.Add("Septic Systems");
                layerTypes.Add("Soils");
                layerTypes.Add("Reference");
                // Get the name of the layer and type of layer
                string newFileName = Path.GetFileName(openDialog.FileName);
                DialogAddLayer addLayerDialog = new DialogAddLayer();
                addLayerDialog.Text = newFileName;
                for (int i = 0; i < layerTypes.Count; i++)
                    addLayerDialog.cbAddLayerType.Items.Add(layerTypes[i]);
                if (addLayerDialog.ShowDialog() == DialogResult.OK)
                {
                    FormMain.LayerInfo newLayer = new FormMain.LayerInfo();
                    newLayer.FileName = Path.GetFileName(openDialog.FileName);
                    newLayer.Name = addLayerDialog.tbNewLayerName.Text;
                    newLayer.Type = addLayerDialog.cbAddLayerType.SelectedIndex + 1;
                    changedLayers.Add(newLayer);
                    lvLayerList.Items.Add(newLayer.Name);
                }
            }

        }
    }
}
