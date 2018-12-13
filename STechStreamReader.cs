using System.IO;

namespace warmf {
    public class STechStreamReader : StreamReader {
        public int LineNum { get; set; }

        public STechStreamReader(string filename) : base(filename) { 
            LineNum = 0; 
        }

        public override string ReadLine() {
            LineNum++;
            return base.ReadLine();
        }
    }
}
