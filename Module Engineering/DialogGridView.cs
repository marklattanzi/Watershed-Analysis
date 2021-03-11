using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace warmf
{
    public partial class DialogGridView : Form
    {
        public List<string> selectedValues;

        public DialogGridView()
        {
            InitializeComponent();
        }

        public void Populate(List<string> warmfFields, string [] shapefileFields)
        {
            DataGridViewTextBoxColumn warmfColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "WARMF Coefficient"
            };
            theGridView.Columns.Add(warmfColumn);
            DataGridViewComboBoxColumn shapefileColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "Shapefile Field",
                MaxDropDownItems = shapefileFields.Count()
            };
            shapefileColumn.Items.Add("(not used)");
            for (int i = 0; i < shapefileFields.Count(); i++)
                shapefileColumn.Items.Add(shapefileFields[i]);
            theGridView.Columns.Add(shapefileColumn);

            theGridView.RowCount = warmfFields.Count;
            for (int i = 0; i < warmfFields.Count; i++)
            {
                theGridView.Rows[i].Cells[0].Value = warmfFields[i];
                if (shapefileFields.Contains(warmfFields[i]))
                    theGridView.Rows[i].Cells[1].Value = warmfFields[i];
                else
                    theGridView.Rows[i].Cells[1].Value = "(not used)";
            }
        }

        public void SetGridViewColumnHeaders(string columnHeader1, string columnHeader2)
        {
            if (theGridView.Columns.Count > 0)
                theGridView.Columns[0].HeaderText = columnHeader1;
            if (theGridView.Columns.Count > 1)
                theGridView.Columns[1].HeaderText = columnHeader2;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Get the results
            selectedValues = new List<string>();
            for (int i = 0; i < theGridView.RowCount; i++)
                selectedValues.Add(theGridView.Rows[i].Cells[1].Value.ToString());
        }
    }
}
