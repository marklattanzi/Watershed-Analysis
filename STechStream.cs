using System.IO;

// File stream reader and writer that keep track of line nums
namespace warmf {
    public class STechStreamReader : StreamReader {
        public int LineNum { get; private set; }

		public STechStreamReader(string filename) : base(filename) { LineNum = 0; }

        public override string ReadLine() {
            LineNum++;
            return base.ReadLine();
        }
    }

	public class STechStreamWriter : StreamWriter {
		public int LineNum { get; private set; }

		public STechStreamWriter(string filename) : base(filename) { LineNum = 0; }
		public STechStreamWriter(string filename, bool append) : base(filename, append) { }

		public override void WriteLine() {
			LineNum++;
			base.WriteLine();
		}
	}
}
