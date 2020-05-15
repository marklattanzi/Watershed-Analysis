using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace warmf
{
    public partial class DialogScenarioManager : Form
    {
        static int MAXOPENSCENARIOS = 4;

        public DialogScenarioManager(List <FormMain.ScenarioInfo> TheScenarios)
        {
            InitializeComponent();

            // Add scenario names to the lists and copy button
            for (int i = 0; i < TheScenarios.Count; i++)
            {
                // Strip out the directories and extension
                string scenarioName = Path.GetFileNameWithoutExtension(TheScenarios[i].Name);
                // All scenarios are added to the project scenarios list
                ProjectScenariosList.Items.Add(scenarioName);
                // Open scenarios are added to the open scenarios list
                if (TheScenarios[i].IsOpen > 0)
                    OpenScenariosList.Items.Add(scenarioName);
                // Modify the text of the Copy Active Scenario button to include the active scenario name
                if (TheScenarios[i].IsActive > 0)
                {
                    string copyString = CopyActiveScenario.Text;
                    copyString = copyString + scenarioName;
                    CopyActiveScenario.Text = copyString;
                }
            }

            // Initial enabling / disabling of buttons
            EnableProjectScenarioButtons();
            EnableClose();
        }

        // Retrieves the scenario information from the dialog
        // Note this does not include whether or not a scenario is active
        public void GetScenarioInfo(ref List<FormMain.ScenarioInfo> Scenarios)
        {
            Scenarios.Clear();
            for (int i = 0; i < ProjectScenariosList.Items.Count; i++)
            {
                FormMain.ScenarioInfo thisScenario = new FormMain.ScenarioInfo();
                thisScenario.Name = ProjectScenariosList.Items[i].ToString() + ".coe";
                thisScenario.IsActive = 0;
                if (OpenScenariosList.Items.IndexOf(ProjectScenariosList.Items[i].ToString()) >= 0)
                    thisScenario.IsOpen = 1;
                else
                    thisScenario.IsOpen = 0;
                Scenarios.Add(thisScenario);
            }
        }

        private void EnableProjectScenarioButtons()
        {
            // Move Up, and Move Down buttons are enabled if anything is selected 
            // in the project scenarios list and there is more than 1 item in the list
            bool enableUpDown = (ProjectScenariosList.SelectedIndex >= 0 && ProjectScenariosList.Items.Count > 1);
            ScenarioMoveUp.Enabled = enableUpDown;
            ScenarioMoveDown.Enabled = enableUpDown;

            // Remove button is enabled if Up/Down are enabled AND the selected scenario isn't the only open scenario
            bool enableRemove = enableUpDown; 
            if (enableRemove)
            {
                enableRemove = (OpenScenariosList.Items.IndexOf(ProjectScenariosList.SelectedItem) < 0 ||
                    OpenScenariosList.Items.Count > 1);
            }
            RemoveScenario.Enabled = enableRemove;

            // Enable the Open button if a project scenario is selected, it is not in the open scenarios list, 
            // and there are fewer than MAXOPENSCENARIOS in the open scenarios list
            EnableOpen();
        }

        // Enables or disables the Open button
        private void EnableOpen()
        {
            // Disabled unless criteria are met
            bool enable = false;
            // There needs to be a scenario selected in the project scenarios list and 
            // the number of open scenarios is less than the maximum allowed
            if (ProjectScenariosList.SelectedIndex >= 0 && OpenScenariosList.Items.Count < MAXOPENSCENARIOS)
            {
                // Check to see if the selected scenario is already in the list
                if (OpenScenariosList.Items.IndexOf(ProjectScenariosList.SelectedItem.ToString()) < 0)
                    enable = true;
            }

            ScenarioOpen.Enabled = enable;
        }

        // Enables or disables the Close button
        private void EnableClose()
        {
            ScenarioClose.Enabled = (OpenScenariosList.SelectedIndex >= 0 && OpenScenariosList.Items.Count > 1);
        }

        private void ScenarioManager_Load(object sender, EventArgs e)
        {

        }

        private void ProjectScenariosList_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableProjectScenarioButtons();
        }

        private void OpenScenariosList_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableOpen();
            EnableClose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        private void CopyActiveScenario_Click(object sender, EventArgs e)
        {
            // Remember the active scenario name
            string activeScenario = Path.GetFileNameWithoutExtension(Global.coe.catchmentOutFilename);
            string newScenario = "";
            if (FormMain.ScenarioSaveAs(ref newScenario))
            {
                // Put back the output file names
                Global.coe.SetOutputFileNames(activeScenario);

                // Add the new scenario to the project scenarios list
                AddToProjectScenarioList(newScenario);

                // Reset the button enabling
                EnableProjectScenarioButtons();
            }

        }

        public void AddToProjectScenarioList(string NewFile)
        {
            // Strip out the directory and extension
            string scenarioName = Path.GetFileNameWithoutExtension(NewFile);

            // Find out if the file is already in the list of project scenarios
            int selectedIndex = ProjectScenariosList.Items.IndexOf(scenarioName);
            if (selectedIndex >= 0)
            {
                // Select the "added" scenario
                ProjectScenariosList.SelectedIndex = selectedIndex;
            }
            else
            {
                // If the added scenario is not already in the list, add it and select it
                ProjectScenariosList.Items.Add(scenarioName);
                ProjectScenariosList.SelectedIndex = ProjectScenariosList.Items.Count - 1;

                // Reset the button enabling
                EnableProjectScenarioButtons();
            }
        }

        private void AddScenario_Click(object sender, EventArgs e)
        {
            // Get the new coefficient file
            string newScenarioName = "";
            if (FormMain.OpenExistingCOEFile(ref newScenarioName))
                // Add the new scenario to the project scenarios list
                AddToProjectScenarioList(newScenarioName);
        }

        private void RemoveScenario_Click(object sender, EventArgs e)
        {
            // Get the selected string in the project scenarios
            string selectedString = ProjectScenariosList.SelectedItem.ToString();
            int openScenariosIndex = OpenScenariosList.Items.IndexOf(selectedString);

            // Make sure this scenario is not the only open scenario
            if (openScenariosIndex < 0 || OpenScenariosList.Items.Count > 1)
            {
                // Get the selected index in the project scenarios
                int projectScenariosIndex = ProjectScenariosList.SelectedIndex;
                // Remove the scenario from the project scenarios
                if (projectScenariosIndex >= 0)
                    ProjectScenariosList.Items.RemoveAt(projectScenariosIndex);
                // Remove the scenario from the open scenarios
                if (openScenariosIndex >= 0)
                    OpenScenariosList.Items.RemoveAt(openScenariosIndex);

                // Reset the button enabling
                EnableProjectScenarioButtons();
            }

        }

        // Determines the index where a scenario belongs in the open scenarios list
        // based on its position in the project scenarios list
        private int GetOpenScenariosIndex(int ProjectScenariosIndex)
        {
            for (int i = 0; i < OpenScenariosList.Items.Count; i++)
            {
                // Get the string at this position in the open scenarios list
                string scenarioString = OpenScenariosList.Items[i].ToString();
                // Get the position of the string in the list of project scenarios
                int scenarioIndex = ProjectScenariosList.Items.IndexOf(scenarioString);
                // Determine if the scenario being tested is below the one passed into this function
                if (scenarioIndex >= ProjectScenariosIndex)
                    return i;
            }

            // All open scenarios are above passed in scenario in project scenarios list
            return OpenScenariosList.Items.Count;
        }

        private void MoveScenario(int Direction)
        {
            int selectedIndex = ProjectScenariosList.SelectedIndex;
            if (selectedIndex >= 0)
            {
                int newIndex = selectedIndex + Direction;
                // Moved above the top of the list - go to the bottom
                if (newIndex < 0)
                    newIndex = ProjectScenariosList.Items.Count - 1;
                // Moved below the bottom of the list - go to the top
                if (newIndex >= ProjectScenariosList.Items.Count)
                    newIndex = 0;

                // Save the scenario name so we can reinsert it in the list
                string selectedString = ProjectScenariosList.SelectedItem.ToString();

                // Remove the scenario from the old location and put it at the new
                ProjectScenariosList.Items.RemoveAt(selectedIndex);
                ProjectScenariosList.Items.Insert(newIndex, selectedString);

                // Select the scenario in its new location
                ProjectScenariosList.SelectedIndex = newIndex;

                // As applicable, remove the scenario from the open scenarios list and then replace it at the new location
                int openScenariosIndex = OpenScenariosList.Items.IndexOf(selectedString);
                if (openScenariosIndex >= 0 && OpenScenariosList.Items.Count > 1)
                {
                    // Remove the scenario from the open scenarios list
                    OpenScenariosList.Items.RemoveAt(openScenariosIndex);
                    // Find where the scenario should be reinserted into the list
                    int newOpenScenariosIndex = GetOpenScenariosIndex(newIndex);
                    if (newOpenScenariosIndex >= 0)
                    {
                        OpenScenariosList.Items.Insert(newOpenScenariosIndex, selectedString);
                    }
                }
            }
        }

        private void ScenarioMoveUp_Click(object sender, EventArgs e)
        {
            // Move the scenario up in the list
            MoveScenario(-1);
        }

        private void ScenarioMoveDown_Click(object sender, EventArgs e)
        {
            // Move the scenario down in the list
            MoveScenario(1);
        }

        private void ScenarioOpen_Click(object sender, EventArgs e)
        {
            if (OpenScenariosList.Items.Count < MAXOPENSCENARIOS)
            {
                int selectedIndex = ProjectScenariosList.SelectedIndex;
                if (selectedIndex >= 0)
                {
                    // Get the selected string from the project scenarios list
                    string selectedString = ProjectScenariosList.SelectedItem.ToString();

                    // Check to see if the scenario is already in the list of open scenarios
                    int openScenariosIndex = OpenScenariosList.Items.IndexOf(selectedString);
                    if (openScenariosIndex < 0)
                    {
                        // Get the position in the open scenarios list where this scenario belongs
                        // based on the order of the project scenarios
                        openScenariosIndex = GetOpenScenariosIndex(selectedIndex);
                        if (openScenariosIndex >= 0)
                            OpenScenariosList.Items.Insert(openScenariosIndex, selectedString);

                        EnableOpen();
                    }

                }
            }
        }

        private void ScenarioClose_Click(object sender, EventArgs e)
        {
            // Remove the selected scenario from the list of open scenarios
            if (OpenScenariosList.SelectedIndex >= 0 && OpenScenariosList.Items.Count > 1)
                OpenScenariosList.Items.RemoveAt(OpenScenariosList.SelectedIndex);

            EnableProjectScenarioButtons();
            EnableClose();
        }
    }
}
