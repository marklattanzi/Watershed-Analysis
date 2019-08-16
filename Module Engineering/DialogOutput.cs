using System;
using System.IO;
using System.Windows.Forms;


namespace warmf
{
    public partial class DialogOutput : Form
    {
        public static Output catchmentOutput = new Output();
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

                //Read the .CAT file (for selected catchment) into memory
                catchmentOutput.ReadCAT(Global.DIR.CAT + "Catawba_SC_June2018.CAT", catchment.idNum);
                
                //Populate listbox containing output parameters
                lbOutputParameters.DataSource = catchmentOutput.constituentNameUnits;
                
                //Show Observations (Hide for catchments)
                chkShowObservations.Hide();
            }
            
        }

        
    }
}
