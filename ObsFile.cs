using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace warmf
{

    class ObservedFile : DataFile
    {
        // methods
        public ObservedFile(string fname)
        {
            filename = fname;
        }

        public override bool ReadHeader(ref STechStreamWriter SR)
        {
            int intRes;
            string line;

            base.ReadHeader(ref SR);

            // Read the number of constituents and the constituent codes
            line = SR.ReadLine();
            NumParameters = Int.TryParse(line.Substring(0, 12), out intRes) ? intRes : 0;
            ParameterNames = new List<string>();
            ParameterCodes = new List<string>();

            for (int ii = 0; ii < NumParameters; ii++)
            {
                string codeString = line.Substring(13 + 8 * ii, 8);
                ParameterCodes.Add(codeString);
                // This code should be put within the coefficients class when possible
                ParameterNames.Add(GetParameterNameFromCode(codeString));
            }
        }

        // This method should be put within the coefficients class when possible
        public string GetParameterNameFromCode(string TheCode)
        {
            int ii;

            for (ii = 0; ii < global.coe.hydroConstits.Count(); ii++)
                if (global.coe.hydroConstits[ii].fortranCode == TheCode)
                    return (global.coe.hydroConstits[ii].fullName + ", " + global.coe.hydroConstits[ii].units);
            for (ii = 0; ii < global.coe.chemicalConstits.Count(); ii++)
                if (global.coe.chemicalConstits[ii].fortranCode == TheCode)
                    return (global.coe.chemicalConstits[ii].fullName + ", " + global.coe.chemicalConstits[ii].units);
            for (ii = 0; ii < global.coe.physicalConstits.Count(); ii++)
                if (global.coe.physicalConstits[ii].fortranCode == TheCode)
                    return (global.coe.physicalConstits[ii].fullName + ", " + global.coe.physicalConstits[ii].units);
            for (ii = 0; ii < global.coe.compositeConstits.Count(); ii++)
                if (global.coe.compositeConstits[ii].fortranCode == TheCode)
                    return (global.coe.compositeConstits[ii].fullName + ", " + global.coe.compositeConstits[ii].units);

            return "";
        }
    }
}
