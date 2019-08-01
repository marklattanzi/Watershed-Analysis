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

        public override bool ReadHeader(ref STechStreamReader SR)
        {
            base.ReadHeader(ref SR);

            return ReadParameters(ref SR);
        }

    }
}
