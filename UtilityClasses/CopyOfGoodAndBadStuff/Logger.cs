using System;
using System.Collections.Generic;
using System.IO;

namespace warmf {

	static class Logger {

		private enum LogLevel { TRACE, INFO, DEBUG, WARNING, ERROR, FATAL }
		private static List<string> levels = new List<string> { "TRACE", "INFO", "DEBUG", "WARNING", "ERROR", "FATAL" };

		private static string datetimeFormat;
		public static bool useTime { private get; set; }
		private static string logFilename;

		// Initiate an instance of Logger class constructor.
		// If log file does not exist, it will be created automatically.
		static Logger() {
			datetimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
			useTime = false;
			logFilename = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "gui.log";

			// Log file header line
			string logHeader = logFilename + " is created.";
			if (!System.IO.File.Exists(logFilename)) {
				WriteLine(System.DateTime.Now.ToString(datetimeFormat) + " " + logHeader, false);
			}
		}

		// Log a DEBUG message
		static public void Debug(string text) { WriteFormattedLog(LogLevel.DEBUG, text); }

		// Log an ERROR message
		static public void Error(string text) { WriteFormattedLog(LogLevel.ERROR, text); }

		// Log a FATAL ERROR message
		static public void Fatal(string text) { WriteFormattedLog(LogLevel.FATAL, text); }

		// Log an INFO message
		static public void Info(string text) { WriteFormattedLog(LogLevel.INFO, text); }

		// Log a TRACE message
		static public void Trace(string text) { WriteFormattedLog(LogLevel.TRACE, text); }

		// Log a WARNING message
		static public void Warning(string text) { WriteFormattedLog(LogLevel.WARNING, text); }

		static private void WriteLine(string text, bool append = true) {
			try {
				StreamWriter writer = new StreamWriter(logFilename, append, System.Text.Encoding.UTF8);
				if (!string.IsNullOrEmpty(text)) {
					writer.WriteLine(text);
				}
				writer.Close();
			}
			catch {
				throw;
			}
		}

		static private void WriteFormattedLog(LogLevel level, string text) {
			string pretext = "";
			if (useTime)
				pretext = System.DateTime.Now.ToString(datetimeFormat);
			pretext += " [" + levels[(int)level] + "]:";
			WriteLine(pretext + text);
		}
	}
}
