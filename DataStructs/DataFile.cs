using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warmf
{
    // The DataLine class stores all the information on one line of a time series data file
    public class DataLine
    {
        public DateTime Date { get; set; }
        public List<double> Values;
        public List<double> SecondaryValues;
        public string Source;

        public bool ParseDate(string TheString)
        {

            int day = Int32.TryParse(TheString.Substring(0, 2), out int intRes) ? intRes : 0;
            int month = Int32.TryParse(TheString.Substring(2, 2), out intRes) ? intRes : 0;
            int year = Int32.TryParse(TheString.Substring(4, 4), out intRes) ? intRes : 0;
            int hour = Int32.TryParse(TheString.Substring(9, 2), out intRes) ? intRes : 0;
            int minute = Int32.TryParse(TheString.Substring(11, 2), out intRes) ? intRes : 0;
            Date = new DateTime(year, month, day, hour, minute, 0);

            return true;
        }

        public bool ParseString(string TheString, int NumValues)
        {
            //int intRes, day, month, year, hour, minute;

            ParseDate(TheString);
            /*            day = Int32.TryParse(TheString.Substring(0, 2), out intRes) ? intRes : 0;
                        month = Int32.TryParse(TheString.Substring(2, 2), out intRes) ? intRes : 0;
                        year = Int32.TryParse(TheString.Substring(4, 4), out intRes) ? intRes : 0;
                        hour = Int32.TryParse(TheString.Substring(9, 2), out intRes) ? intRes : 0;
                        minute = Int32.TryParse(TheString.Substring(11, 2), out intRes) ? intRes : 0;
                        Date = new DateTime(year, month, day, hour, minute, 0);*/

            Values = new List<double>();
            for (int i = 0; i < NumValues; i++)
            {
                Values.Add(Double.TryParse(TheString.Substring(13 + 8 * i, 8), out double dblRes) ? dblRes : 0);
            }
            Source = TheString.Substring(13 + 8 * NumValues);

            return true;
        }

        public bool WriteDataLine(ref STechStreamWriter SW)
        {
            // Write the date and time using a 24-hour clock
            SW.Write("{0}", Date.ToString("ddMMyyyy HHmm"));
            for (int ii = 0; ii < Values.Count(); ii++)
            {
                SW.WriteDouble(Values[ii]);
            }
            SW.WriteLine(Source);

            return true;
        }
    }

    // The DataFile class stores the entire contents of a time series data file
    // Derived classes are used for different types of data files
    public class DataFile
    {
        public int NumLines;
        public int NumParameters;
        public int NumGroups;
        public bool FlexibleColumns;
        public bool Sortable;
        public bool Fillable;
        public List<string> ParameterNames;
        public List<string> ParameterCodes;

        public string filename { get; set; }
        public string shortName { get; set; }
        public int version { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

        public List<DataLine> TheData;

        public DataFile()
        {
            NumLines = 0;
            NumParameters = 0;
            NumGroups = 1;
            FlexibleColumns = false;
            Sortable = false;
            Fillable = false;
            ParameterNames = new List<string>();
            ParameterCodes = new List<string>();
            TheData = new List<DataLine>();
        }

        public struct GraphLabels
        {
            public string key;
            public string xaxis;
            public string yaxis;
        }

        public bool ReadVersionLatLongName(ref STechStreamReader SR)
        {
            string line;

            line = SR.ReadLine();
            if (line.StartsWith("VERSION"))
                version = Int32.TryParse(line.Substring(8, 8), out int intRes) ? intRes : 0;
            else
            {
                Debug.WriteLine("Error in data file.  Version number is missing. Continuing.");
                version = -1;

                return false;
            }

            line = SR.ReadLine();
            latitude = Double.TryParse(line.Substring(9, 10), out double dblRes) ? dblRes : 0;
            longitude = Double.TryParse(line.Substring(30, 10), out dblRes) ? dblRes : 0;
            shortName = line.Substring(40);

            return true;
        }

        public virtual bool ReadHeader(ref STechStreamReader SR)
        {
            ReadVersionLatLongName(ref SR);

            return ReadParameters(ref SR);
        }

        // Reads the number and codes of parameters
        public virtual bool ReadParameters(ref STechStreamReader SR)
        {
            try
            { 
                string line = SR.ReadLine();
                Int32.TryParse(line.Substring(5, 8), out NumParameters);
                for (int ii = 0; ii < NumParameters; ii++)
                {
                    ParameterCodes.Add(line.Substring(13 + 8 * ii, 8));
                    ParameterNames.Add(Global.coe.GetParameterNameAndUnitsFromCode(ParameterCodes[ii]));
                }
            }
            catch (Exception e)
            {
                if (SR != null)
                    Debug.WriteLine("Error in data file.  Badly formatted data at line = " + SR.LineNum);
                else
                    Debug.WriteLine("Error opening StreamReader for data file " + filename);
                return false;
            }

            return true;
        }

        // Reads the lines of data
        public virtual bool ReadData(ref STechStreamReader SR)
        {
            DataLine thisDataLine;
            string line = SR.ReadLine();
            while (line != null)
            {
                thisDataLine = new DataLine();
                thisDataLine.ParseString(line, NumParameters);
                TheData.Add(thisDataLine);
                line = SR.ReadLine();
            }

            return true;
        }

        public virtual bool ReadFile()
        {
            STechStreamReader sr = null;

            try
            {
                //string line;               
                //DataLine thisDataLine;
                sr = new STechStreamReader(filename);

                ReadHeader(ref sr);
                ReadData(ref sr);
            }
            catch (Exception e)
            {
                if (sr != null)
                    Debug.WriteLine("Error in data file.  Badly formatted data at line = " + sr.LineNum);
                else
                    Debug.WriteLine("Error opening StreamReader for data file " + filename);
                return false;
            }
            return true;

        }

        public bool WriteVersionLatLongName(ref STechStreamWriter SW)
        {
            try
            {
                SW.WriteLine("VERSION {0, 8}", version);
                SW.WriteLine("Latitude:{0, 10:F4} Longitude:{1,10:F4}{2}", latitude, longitude, shortName);
            }
            catch (Exception e)
            {
                if (SW != null)
                    Debug.WriteLine("Error writing MET file {0} at line {1} ", filename, SW.LineNum);
                else
                    Debug.WriteLine("Error writing MET file {0}.  Problem with file creation.", filename);
                return true;
            }

            return true;
        }

        public virtual bool WriteParameters(ref STechStreamWriter SW)
        {
            SW.Write("     ");
            SW.WriteInt(NumParameters);
            for (int ii = 0; ii < NumParameters; ii++)
                SW.Write(ParameterCodes[ii]);
            SW.Write("\n");

            return true;
        }

        public virtual bool WriteHeader(ref STechStreamWriter SW)
        {
            WriteVersionLatLongName(ref SW);
            return WriteParameters(ref SW);
        }

        // Writes all the lines of data
        public virtual bool WriteData(ref STechStreamWriter SW)
        {
            for (int ii = 0; ii < TheData.Count(); ii++)
            {
                TheData[ii].WriteDataLine(ref SW);
            }

            return true;
        }

        // Writes the entire file
        public bool WriteFile()
        {
            STechStreamWriter sw = null;
            try
            {
                sw = new STechStreamWriter(filename, false);
                WriteHeader(ref sw);
                WriteData(ref sw);
                sw.Close();
                return true;
            }
            catch (Exception e)
            {
                if (sw != null)
                    Debug.WriteLine("Error writing data file {0} at line {1} ", filename, sw.LineNum);
                else
                    Debug.WriteLine("Error writing data file {0}.  Problem with file creation.", filename);
                return true;
            }

        }

        // Sorts the data by date - algorithm from Microsoft Fortran Powerstation
        // Uses Quick_Sort from FORTRAN Powerstation 4.0 "Fortran for Engineers"
        public bool SortByDate(int Left, int Right)
        {
            if (!Sortable)
                return false;

            int left1, right1;
            DataLine temp;

            if (Left < Right)
            {
                left1 = Left;
                right1 = Right;

                do
                {
                    // Shift left1 to the right
                    while (left1 < Right && !(TheData[left1].Date > TheData[Left].Date))
                        left1++;

                    // Shift right1 to the left
                    while (Left < right1 && !(TheData[right1].Date < TheData[Left].Date))
                        right1--;

                    // Swap data lines
                    if (left1 < right1)
                    {
                        temp = TheData[left1];
                        TheData[left1] = TheData[right1];
                        TheData[right1] = temp;
                    }
                } while (left1 < right1);

                temp = TheData[Left];
                TheData[Left] = TheData[right1];
                TheData[right1] = temp;
                // Sort left subset
                SortByDate(Left, right1 - 1);
                // Sort right subset
                SortByDate(right1 + 1, Right);
            }
            
            return true;
        }

        // Returns the index of the parameter whose Fortran code matches what was passed in
        public int FindParameterCode(string TheCode)
        {
            string codeWithoutSpaces = TheCode.Trim();
            for (int i = 0; i < ParameterCodes.Count; i++)
                if (ParameterCodes[i].IndexOf(TheCode) == 0)
                    return i;
            return -1;
        }

        // Replaces one data line with another
        public void ReplaceData(List <double> ReplaceVariable, DataFile Other, int OtherVariable, bool FillBetween, double Conversion)
        {

        }

        /*
// Replaces a variable on a specific line of data with a variable from elsewhere
bool TimeSeriesData::ReplaceDataLine(int ReplaceLine, stDoubleArray &ReplaceVariable, stDataLine &Other, int OtherVariable, double)
{
	int i, j;
	int replaceNumber;
   for (replaceNumber = 0; replaceNumber < ReplaceVariable.Number; replaceNumber++)
   	if (ReplaceVariable.Values[replaceNumber] > 0.)
      	break;

	// Flag for data source
   if (replaceNumber == Data[ReplaceLine].Types[0].Number)
   {
      Data[ReplaceLine].SetDataSource(Other.DataSource);
      return true;
   }

	if (ReplaceLine < 0 || ReplaceLine >= NumData)
   	return false;
   if (replaceNumber < 0 || replaceNumber > Data[ReplaceLine].Types[0].Number)
   	return false;
   if (OtherVariable >= Other.Types[0].Number)
   	return false;

   // Copy the data to be changed
	for (i = 0; i < NumTypes; i++)
   {
   	if (Other.Types[i].Values[OtherVariable] > -999.)
      {
		   // Add up current values on this data line
   	   double currentTotal = 0.;
   		for (j = 0; j < Data[ReplaceLine].Types[i].Number; j++)
      		currentTotal += Data[ReplaceLine].Types[i].Values[j] * ReplaceVariable.Values[j];

         if (currentTotal > 0.)
         	for (j = 0; j < Data[ReplaceLine].Types[i].Number; j++)
            {
            	if (ReplaceVariable.Values[i] > 0.)
               	Data[ReplaceLine].Types[i].Values[j] *= Other.Types[i].Values[OtherVariable] / currentTotal;
            }
         else
      		Data[ReplaceLine].Types[i].Values[replaceNumber] = Other.Types[i].Values[OtherVariable];
      }
   }

	return true;
}

// Returns a default data line for extrapolation and importing
stDataLine TimeSeriesData::GetDefaultDataLine(stDataLine &OtherDataLine)
{
	int theDataLine = 0;
   for (int i = 0; i < NumData; i++)
   {
   	if (Data[i].IsAfter(OtherDataLine))
      	break;

      if (Data[i].Types[0].Values[0] > -999.)
      	theDataLine = i;
   }

   return Data[theDataLine];
}

// Replaces data with data from another file
bool TimeSeriesData::ReplaceData(stDoubleArray &ReplaceVariable, TimeSeriesData &Other, int OtherVariable, bool FillBetween, double FlowConvert)
{
   int i;
   int lineNumber = 0;

   if (OtherVariable > Other.NumVariables)
   	return false;
   if (Other.NumData <= 0)
   	return false;

   // Fill in space between this dataset and the other, as applicable, with blank lines
   if (IsRegularInterval())
   {
		// Contingency 1: "Other" last data point before first point of this data
		if (Other.Data[Other.NumData - 1].IsBefore(Data[0]))
      	ExtrapolateData(Other.Data[Other.NumData - 1].Date, GetDataInterval(), EXTRAPOLATEMETHODMISSING);
		// Contingency 2: "Other" first data point after last point of this data
		if (Other.Data[0].IsAfter(Data[NumData - 1]))
      	ExtrapolateData(Other.Data[0].Date, GetDataInterval(), EXTRAPOLATEMETHODMISSING);
   }

   // Get a default data line for after the end of the "Other" time series
	stDataLine defaultDataLine = GetDefaultDataLine(Other.Data[Other.NumData - 1]);

	for (i = 0; i < Other.NumData; i++)
   {
      // Get the line number with the same date or just earlier as this data line in "Other"
   	if (!AdvanceToDateAndTime(Other.Data[i], lineNumber))
      {
       	// Contingency 1: "Other's" data point is before all data
      	if (Other.Data[i].IsBefore(Data[0]))
      		lineNumber = 0;
       	// Contingency 2: "Other's" data point is after all data
      	if (Data[NumData - 1].IsBefore(Other.Data[i]))
      		lineNumber = NumData - 1;
      }

      // Only add a new line if the dates don't match
      if (!Data[lineNumber].IsSameTime(Other.Data[i]))
      {
	      // Add a new line after "lineNumber" if date is before
   	   if (Data[lineNumber].IsBefore(Other.Data[i]))
      		lineNumber++;

         AddDataLine(lineNumber, 1, true);

         // Set the date of the new line to match the Other line
         Data[lineNumber].Date = Other.Data[i].Date;
         Data[lineNumber].Time = Other.Data[i].Time;
      }

		// Replace all data before the next data line in the "Other" series
      do
      {
	      // Actually replace the data with the data from "Other"
      	if (OtherVariable < Other.NumVariables)
         	ReplaceDataLine(lineNumber, ReplaceVariable, Other.Data[i], OtherVariable, FlowConvert);
         else
         	Data[lineNumber].SetDataSource(Other.Data[i].DataSource);
         lineNumber++;
      } while (FillBetween && lineNumber < NumData && i < Other.NumData - 1 &&
      			Data[lineNumber].IsBefore(Other.Data[i + 1]));
   }

   // Put default data back in place
   // Set the date to after the other data
   defaultDataLine.Date = Other.Data[Other.NumData - 1].Date;
   defaultDataLine.Time = Other.Data[Other.NumData - 1].Time;
   if (Other.NumData > 1 && Other.Data[Other.NumData - 1].Date.nDay ==
   	 Other.Data[Other.NumData - 2].Date.nDay)
   {
   	// Increment default data by a month
      defaultDataLine.Date += 31;
      defaultDataLine.Date.nDay = Other.Data[Other.NumData - 1].Date.nDay;
   }
   else
   	defaultDataLine.Date++;

	lineNumber = 0;
   if (AdvanceToDateAndTime(defaultDataLine, lineNumber))
   {
      if (!Data[lineNumber].IsSameTime(defaultDataLine))
      {
      	lineNumber++;
      	AddDataLine(lineNumber);
         Data[lineNumber] = defaultDataLine;
      }
      else
      	// Replace data if first value is missing
      	if (Data[lineNumber].Types[0].Values[0] == -999.)
      		Data[lineNumber] = defaultDataLine;
   }

   return true;
}
         */
    }
}
