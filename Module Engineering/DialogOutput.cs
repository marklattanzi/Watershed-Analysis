using System;
using System.IO;
using System.Windows.Forms;

namespace warmf
{
    public partial class DialogOutput : Form
    {
        FormMain parent;

        public DialogOutput(FormMain par)
        {
            InitializeComponent();
            this.parent = par;
        }

        public void Populate(string featureType, int cnum)
        {
            

            if (featureType == "River")
            {
                Text = featureType + " " + Global.coe.rivers[cnum].idNum + " Output";
            }
            else if (featureType == "Catchment")
            {
                Catchment catchment = Global.coe.catchments[cnum];
                Text = featureType + " " + catchment.idNum + " Output";
                
                //Output Types
                cbOutputType.Items.Add("Surface Water");
                cbOutputType.Items.Add("Combined Output");
                for (int i = 0; i < catchment.numSoilLayers; i++)
                {  
                    cbOutputType.Items.Add("Soil Layer " + (i+1));
                }
                cbOutputType.SelectedIndex = 1;

                //Output Parameters
                int iCount = 0;
                string strNameUnits;
                for (int i = 0; i < Global.coe.numHydrologyParams; i++)
                {
                    if (Global.coe.hydroConstits[i].swCatchmentInclude==true)
                    {
                        strNameUnits = Global.coe.hydroConstits[i].fullName + ", " + Global.coe.hydroConstits[i].units;
                        lbOutputParameters.Items.Add(strNameUnits);
                    }
                    iCount = iCount + 1;
                }
                for (int i = 0; i < Global.coe.numChemicalParams; i++)
                {
                    if (Global.coe.chemConstits[i].swCatchmentInclude==true)
                    {
                        strNameUnits = Global.coe.chemConstits[i].fullName + ", " + Global.coe.chemConstits[i].units;
                        lbOutputParameters.Items.Add(strNameUnits);
                    }
                    iCount = iCount + 1;
                }
                for (int i = 0; i < Global.coe.numPhysicalParams; i++)
                {
                    if (Global.coe.physicalConstits[i].swCatchmentInclude==true)
                    {
                        strNameUnits = Global.coe.physicalConstits[i].fullName + ", " + Global.coe.physicalConstits[i].units;
                        lbOutputParameters.Items.Add(strNameUnits);
                    }
                    iCount = iCount + 1;
                }
                for (int i = 0; i < Global.coe.numCompositeParams; i++)
                {
                    if (Global.coe.compositeConstits[i].swCatchmentInclude==true)
                    {
                        strNameUnits = Global.coe.compositeConstits[i].fullName + ", " + Global.coe.compositeConstits[i].units;
                        lbOutputParameters.Items.Add(strNameUnits);
                    }
                    iCount = iCount + 1;
                }
                //Show Observations (Hide for catchments)
                chkShowObservations.Hide();
            }
            
        }

        
    }
}
