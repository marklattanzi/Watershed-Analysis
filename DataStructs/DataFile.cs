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
        public bool RegularInterval;
        public static int DATAFILLTYPENONE = -1;
        public static int DATAFILLTYPESHIFT = 0;
        public static int DATAFILLTYPEMULTIPLY = 1;
        public static int EXTRAPOLATEINTERVALHOURLY = 0;
        public static int EXTRAPOLATEINTERVALDAILY = 1;
        public static int EXTRAPOLATEINTERVALWEEKLY = 2;
        public static int EXTRAPOLATEINTERVALMONTHLY = 3;
        public static int EXTRAPOLATEINTERVALYEARLY = 4;
        public static int EXTRAPOLATEMETHODMISSING = 0;
        public static int EXTRAPOLATEMETHODZERO = 1;
        public static int EXTRAPOLATEMETHODAVERAGE = 2;
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

        // Determines if the latitude and longitude are possible
        public bool CoordinatesInRange()
        {
            if (latitude < -90 || latitude > 90)
                return false;
            if (longitude < -180 || longitude > 180)
                return false;

            return true;
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
                    Debug.WriteLine("Error opening StreamstmReader for data file " + filename);
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

                sr.Close();
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

        // Returns the index of the parameter whose Fortran code matches what was passed in
        public int FindParameterCode(string TheCode)
        {
            string codeWithoutSpaces = TheCode.Trim();
            for (int i = 0; i < ParameterCodes.Count; i++)
                if (ParameterCodes[i].IndexOf(TheCode) == 0)
                    return i;
            return -1;
        }

        public virtual bool IsRegularInterval()
        {
            return true;
        }

        // Returns the average time interval of the data in days
        public double GetDataInterval()
        {
            if (TheData.Count < 2)
                return 0;

            // Choose data points to compare
            int firstPoint = 0;
            int lastPoint = TheData.Count - 1;

            if (!IsRegularInterval())
                firstPoint = lastPoint - 1;

            // Difference in days and fraction of days
            double difference = (TheData[lastPoint].Date - TheData[firstPoint].Date).TotalDays;

            return difference / (double)(lastPoint - firstPoint);
        }

        // Gets default data interval code from the actual data interval
        public int GetDefaultDataInterval()
        {
            // Get the actual data interval
            double interval = GetDataInterval();

            // Annual if no interval
            if (interval <= 0)
                return EXTRAPOLATEINTERVALYEARLY;

            // Hourly by default
            if (interval < 0.5)
                return EXTRAPOLATEINTERVALHOURLY;
            // Daily by default
            if (interval < 3.5)
                return EXTRAPOLATEINTERVALDAILY;
            // Weekly by default
            if (interval < 15)
                return EXTRAPOLATEINTERVALWEEKLY;
            // Monthly by default
            if (interval < 180)
                return EXTRAPOLATEINTERVALMONTHLY;

            // If nothing else, default data interval is annual
            return EXTRAPOLATEINTERVALYEARLY;
        }

        // Returns the value used when simulating
        public double GetEffectiveValue(int TheDataLine, int TheGroup, int TheParameter)
        {
            if (TheDataLine < 0 || TheDataLine >= TheData.Count)
                return -999;
            if (TheGroup < 0 || TheGroup >= NumGroups)
                return -999;
            if (TheParameter < 0 || TheParameter >= NumParameters)
                return -999;

            if (TheGroup == 1)
                return TheData[TheDataLine].SecondaryValues[TheParameter];
            return TheData[TheDataLine].Values[TheParameter];
        }

        // Gets average values for each parameter for every day of the year
        public bool GetAverageValues(ref DataFile Averages)
        {
            int i, j, k;
            DataFile numValues = new DataFile();

            // Initialize records to be used to calculate typical values for
            // each day of the year
            Averages.NumGroups = NumGroups;
            Averages.NumParameters = NumParameters;
            numValues.NumGroups = NumGroups;
            numValues.NumParameters = NumParameters;
            for (k = 0; k < Averages.TheData.Count; k++)
            {
                DataLine averageDataLine = new DataLine();
                DataLine numValuesDataLine = new DataLine();
                for (j = 0; j < NumParameters; j++)
                {
                    Averages.TheData[k].Values[j] = 0;
                    Averages.TheData[k].SecondaryValues[j] = 0;
                    numValues.TheData[k].Values[j] = 0;
                    numValues.TheData[k].SecondaryValues[j] = 0;
                }
            }

            // Sum up all the values to create an average year
            int julian;
            for (i = 0; i < TheData.Count; i++)
            {
                DateTime interDate = TheData[i].Date;
                // Fill in for days between this data point and the next
                do
                {
                    julian = interDate.DayOfYear - 1;
                    for (k = 0; k < NumParameters; k++)
                    {
                        // Value used for simulation may be from earlier day in file
                        double effectiveValue = GetEffectiveValue(i, 0, k);
                        if (effectiveValue > -999)
                        {
                            Averages.TheData[julian].Values[k] +=
                                 effectiveValue;
                            numValues.TheData[julian].Values[k] += 1;
                        }
                        if (NumGroups == 2)
                        {
                            effectiveValue = GetEffectiveValue(i, 1, k);
                            if (effectiveValue > -999)
                            {
                                Averages.TheData[julian].SecondaryValues[k] +=
                                     effectiveValue;
                                numValues.TheData[julian].SecondaryValues[k] += 1;
                            }
                        }
                    }
                    interDate.AddDays(1);
                } while (i < TheData.Count - 1 && interDate < TheData[i + 1].Date);
            }

            // Average the summed values
            for (k = 0; k < Averages.TheData.Count; k++)
            {
                for (j = 0; j < Averages.TheData[k].Values.Count; j++)
                {
                    // Calculate average for primary and secondary values
                    // numValues will be zero for secondary values if there aren't any
                    if (numValues.TheData[k].Values[j] > 1)
                        Averages.TheData[k].Values[j] /=
                           numValues.TheData[k].Values[j];
                    if (numValues.TheData[k].SecondaryValues[j] > 1)
                        Averages.TheData[k].SecondaryValues[j] /=
                           numValues.TheData[k].SecondaryValues[j];

                    // Smooth out average with leap years, affecting the last julian day
                    if (k == 365)
                    {
                        Averages.TheData[365].Values[j] =
                           (Averages.TheData[364].Values[j] +
                           Averages.TheData[0].Values[j]) / 2;
                        Averages.TheData[365].SecondaryValues[j] =
                           (Averages.TheData[364].SecondaryValues[j] +
                           Averages.TheData[0].SecondaryValues[j]) / 2;
                    }
                }
            }

            return true;
        }

        // Extrapolates data to a specified date
        public bool ExtrapolateData(DateTime TheDate, int Interval, int Method)
        {
            int i, j, k;

            if (TheData.Count < 1)
                return false;

            // Calculate the number of data lines to add
            // Set increments for each of the interval possibilities
            int hour = 0;
            int day = 0;
            int month = 0;
            int year = 0;
            // Hourly
            if (Interval == EXTRAPOLATEINTERVALHOURLY)
                hour = 1;
            // Daily
            if (Interval == EXTRAPOLATEINTERVALDAILY)
                day = 1;
            // Weekly
            if (Interval == EXTRAPOLATEINTERVALWEEKLY)
                day = 7;
            // Monthly
            if (Interval == EXTRAPOLATEINTERVALMONTHLY)
                month = 1;
            // Yearly
            if (Interval == EXTRAPOLATEINTERVALYEARLY)
                year = 1;
            int numNewLines = 0;

            // Extrapolate either forward or backward
            int direction = 0;
            int startTest;
            // Forward
            if (TheData[TheData.Count - 1].Date <= TheDate)
            {
                direction = 1;
                startTest = TheData.Count - 1;
            }
            // Backward
            else if (TheData[0].Date > TheDate)
            {
                direction = -1;
                startTest = 0;
            }
            // Can't extrapolate at all
            else
                return false;

            // Set up typical average values for each day of the year if that method is selected
            DataFile typical = new DataFile();
            if (Method == EXTRAPOLATEMETHODAVERAGE)
                GetAverageValues(ref typical);

            DateTime testDate = TheData[startTest].Date;
            while ((testDate <= TheDate && direction == 1) || (testDate > TheDate && direction == -1))
            {
                numNewLines++;
                testDate.AddHours(hour * direction);
                testDate.AddDays(day * direction);
                testDate.AddMonths(month * direction);
                testDate.AddYears(year * direction);
            }

            // Expand the data to include all the new lines
            int originalStartYear = TheData[0].Date.Year;
            int originalEndYear = TheData[TheData.Count - 1].Date.Year;
            int lineCount = 0;
            if (direction == 1)
            {
                lineCount = TheData.Count;
                for (i = 0; i < numNewLines; i++)
                {
                    DataLine newDataLine = new DataLine();
                    TheData.Add(newDataLine);
                }
            }
            else if (direction == -1)
            {
                lineCount = numNewLines - 1;
                for (i = 0; i < numNewLines; i++)
                {
                    DataLine newDataLine = new DataLine();
                    TheData.Insert(0, newDataLine);
                }
            }

            for (; lineCount < TheData.Count && lineCount >= 0; lineCount += direction)
            {
                TheData[lineCount].Date = TheData[lineCount - 1 * direction].Date;
                TheData[lineCount].Date.AddYears(year * direction);
                TheData[lineCount].Date.AddMonths(month * direction);
                TheData[lineCount].Date.AddDays(day * direction);
                TheData[lineCount].Date.AddHours(hour * direction);

                // Set the new data as directed
                // Missing
                if (Method == EXTRAPOLATEMETHODMISSING)
                {
                    for (j = 0; j < TheData[lineCount].Values.Count; j++)
                        TheData[lineCount].Values[j] = -999;
                    if (NumGroups == 2)
                        for (j = 0; j < TheData[lineCount].SecondaryValues.Count; j++)
                            TheData[lineCount].SecondaryValues[j] = -999;

                    TheData[lineCount].Source = "";
                }
                // Zero
                else if (Method == EXTRAPOLATEMETHODZERO)
                {
                    for (j = 0; j < TheData[lineCount].Values.Count; j++)
                        TheData[lineCount].Values[j] = 0;
                    if (NumGroups == 2)
                        for (j = 0; j < TheData[lineCount].SecondaryValues.Count; j++)
                            TheData[lineCount].SecondaryValues[j] = 0;

                    TheData[lineCount].Source = "zero";
                }
                // Typical values
                else if (Method == EXTRAPOLATEMETHODAVERAGE)
                {
                    int julian = TheData[lineCount].Date.DayOfYear - 1;
                    // Days to include in each average: 1 for hourly or daily, 7 for weekly
                    int daysInAverage = Math.Max(1, day);
                    if (Interval == EXTRAPOLATEINTERVALMONTHLY)
                        daysInAverage = 30;
                    if (Interval == EXTRAPOLATEINTERVALYEARLY)
                        daysInAverage = 365;
                    // Fill in with typical values calculated earlier
                    for (j = 0; j < TheData[lineCount].Values.Count; j++)
                    {
                        double theAverage = 0;
                        for (k = 0; k < daysInAverage; k++)
                            theAverage += typical.TheData[(julian + k) % 366].Values[j];

                        // Get the average over the data interval
                        TheData[lineCount].Values[j] =
                             theAverage /= (double)daysInAverage;
                    }
                    if (NumGroups == 2)
                        for (j = 0; j < TheData[lineCount].SecondaryValues.Count; j++)
                        {
                            double theAverage = 0;
                            for (k = 0; k < daysInAverage; k++)
                                theAverage += typical.TheData[(julian + k) % 366].SecondaryValues[j];

                            // Get the average over the data interval
                            TheData[lineCount].SecondaryValues[j] =
                                 theAverage /= (double)daysInAverage;
                        }

                    string sourceString = "average of " + Convert.ToString(originalStartYear) + "-" + Convert.ToString(originalEndYear);
                    TheData[lineCount].Source = sourceString;
                }
            }

            return true;
        }

        // Truncates data before or after a certain date
        public bool TruncateData(DateTime TheDate, bool After)
        {
            for (int i = TheData.Count - 1; i >= 0; i--)
            {
                if (TheData[i].Date > TheDate && After)
                    TheData.RemoveAt(i);
                else if (TheData[i].Date < TheDate && !After)
                    TheData.RemoveAt(i);
            }

            return true;
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

        // Replaces data in this file with data in another structure
        public virtual bool ReplaceData(List <double> replaceVariable, DataFile other, int otherVariable, bool fillBetween, double flowConvert)
        {
            int i;
            int lineNumber = 0;

            if (otherVariable > other.NumParameters)
                return false;
            if (other.TheData.Count <= 0)
                return false;

            // Fill in space between this dataset and the other, as applicable, with blank lines
            if (IsRegularInterval())
            {
                // Contingency 1: "Other" last data point before first point of this data
                if (other.TheData[other.TheData.Count - 1].Date < TheData[0].Date)
                    ExtrapolateData(other.TheData[other.TheData.Count - 1].Date, GetDefaultDataInterval(), EXTRAPOLATEMETHODMISSING);
                // Contingency 2: "Other" first data point after last point of this data
                if (other.TheData[0].Date > TheData[TheData.Count - 1].Date)
                    ExtrapolateData(other.TheData[0].Date, GetDefaultDataInterval(), EXTRAPOLATEMETHODMISSING);
            }

            return true;

            /*
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

             */
        }
    }
}
