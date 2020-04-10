using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace warmf {
    public struct DIVFileData {
        public string filename;
        public double flowDivertedMultiplier;
        public double flowCap;
        public bool swUseMonthFlows;
        public List<double> monthlyFlow;
    }

    public class HydrologyConstits {
        public string fortranCode;
        public bool swIncludeInOutput;
        public string units;
        public string abbrevName;
        public string fullName;
        public bool swCatchmentInclude;
        public bool swRiverInclude;
        public bool swReservoirInclude;
        public bool swLoadingInclude;
    }

    public class CompositeConstits : HydrologyConstits
    {
        public double electricalCharge;
        public double massEquivalent;
        public double loadingUnitConversion;
        public string loadingUnits;

        public bool swIncludeDissolvedConstits;
        public bool swIncludeAdsorbedConstits;
        public bool swIncludePrecipConstits;

        public List<double> componentTotalMass;
    }

    public class PhysicalConstits : CompositeConstits
    {
        public double airRainMult;
        public double pointSourceMult;
        public double nonpointSourceMult;
        public bool swLoadingTMDL;
        public int dryDepositionForm;
        public bool swChemAdvection;
        public List<double> gasDepositVelocity;
    }

    public class ChemicalConstits : PhysicalConstits
    {
        public double solubWithSulfate;
        public double stoichChemWithSulfate;
        public double stoichSulfateWithChem;
        public double solubWithHydrox;
        public double stoichChemWithHydrox;
        public double stoichHydroxWithChem;
    }

    public struct Reaction {
        public int primReactantNumber;
        public bool swIsAnoxic;
        public double dissolvedOxyLimit;
        public bool swIsUVCatalysis;
        public int numLinkedReactions;
        public double tempCorrectCoeff;
        public string fortranCode;
        public string units;
        public string name;
        public List<double> stoich;  // one per component
    }

    public struct Sediment {
        public double grainSize;
        public double specGravity;
        public double settlingRate;
    }

    public struct Algae {
        public double nitroHalfSat;
        public double phosHalfSat;
        public double silicaHalfSat;
        public double lightSat;
        public double lowTempLimit;
        public double highTempLimit;
        public double optGrowTemp;
    }

    public struct Periphyton {
        public double recycledFraction;
        public double velocityHalfSat;
        public double nitroHalfSat;
        public double phosHalfSat;
        public double lightSat;
        public double scourRegressionCoef;
        public double scourRegressionExp;
        public double ammoniaPref;
        public double spatialLimitHalfSat;
        public double spatialLimitIntercept;
        public double endoRespirationCoef;
        public double endoRespirationExp;
        public double photoRespirationFraction;
    }

    public struct Mineral {
        public string name;
        public double molecularWgt;
        public double phDepend;
        public double weatheringRate;
        public double oxyConsumption;
        public List<double> chemReactionProduct;
    }

    public struct Litter {
        public double coarseLitterFrac;
        public double fineLitterFrac;
        public double humusFrac;
        public double nonStructLeach;
        public double coarseLitterDecay;
        public double fineLitterDecay;
        public double humusDecay;
    }

    public struct Snow {
        public double formTemp;
        public double openAreaSubRate;
        public double forestAreaSubRate;
        public double initialDepth;
        public double openAreaMeltRate;
        public double forestAreaMeltRate;
        public double fieldCapacity;
        public double meltTemp;
        public double rainMeltRate;
        public double thermalConduct;
        public double iceThermalConduct;
        public double soluteIceRetain;
        public double meltLeaching;
        public double nitrificationRate;
    }

    public struct Septic {
        public double failedFlow;
        public List<double> type1;
        public List<double> type2;
        public List<double> type3;
    }

    public struct Landuse {
        public double openWinterFrac;
        public double imperviousFrac;
        public double maxPotentInceptStorage;
        public string name;
        public double rainDetachFactor;
        public double flowDetachFactor;
        public double annualGrowMult;
        public double leafGrowFactor;
        public double productivity;
        public double maintRespRate;
        public double activeRespRate;
        public double dryCollEff;
        public double wetCollEff;
        public double leafWgtArea;
        public double canopyHeight;
        public double stomatalResist;
        public List<double> cropping;
        public List<double> leafAreaIdx;
        public List<double> annualUptake;
        public List<double> litterFallRate;
        public List<double> exudationRate;
        public List<double> leafComp;
        public List<double> trunkComp;
        public int numFertPlans;
        public List<List<List<double>>> fertPlanApplication;
    }

    public struct CatchSeptic {
        public double soilLayer;
        public double population;
        public double standardPct;
        public double advancedPct;
        public double failingPct;
        public double initialBiomass;
        public double thickness;
        public double area;
        public double biomassRespRate;
        public double biomassMortRate;
    }

    public struct CatchSediment {
        public double erosivity;
        public double firstPartSizePct;
        public double secondPartSizePct;
        public double thirdPartSizePct;
    }

    public struct CatchBMP {
        public double streetSweepFreq;
        public double streetSweepEff;
        public double divertedImpervFlow;
        public double detentionPondVol;
        public double maxFertAccumTime;
    }

    public struct CatchMine {
        public double areaFraction;
        public string name;
        public List<double> concentrationLimit;
        public string mineOutFilename;
    }

    public struct CatchReactions
    {
        public List<double> soilReactionRate;
        public List<double> surfaceReactionRate;
        public List<double> canopyReactionRate;
        public List<double> biozoneReactionRate;
    }

    public struct CatchMining {
        public bool swIsLowSoilDeepMineOverburden;
        public int surfaceMineLanduseNum;
        public double depthSpoils;
        public double soilMoisture;
        public double fieldCapacity;
        public double saturationMoisture;
        public double horizHydraulicConduct;
        public double vertHydraulicConduct;
        public double ferroIonOxyidationRate;
        public int numDeepMineDischarges;
        public bool swAreDeepConcentrationsMax;
        public int numSurfaceMineDischarges;
        public bool swAreSurfaceConcentrationsMax;

        public List<CatchMine> deepMines;
        public List<CatchMine> surfaceMines;

        public double coarseLitterWgtFraction;
        public double fineLitterWgtFraction;
        public double humusWgtFraction;

        //public List<double> soilReactionRate;
        //public List<double> surfaceReactionRate;
        //public List<double> canopyReactionRate;
        //public List<double> biozoneReactionRate;

        public int numCEQW2Files;
        public string flowInputFilename;
        public string tempInputFilename;
        public string waterQualInputFilename;
    }

    public struct Soil {
        public double area;
        public double thickness;
        public double moisture;
        public double fieldCapacity;
        public double saturationMoisture;
        public double horizHydraulicConduct;
        public double vertHydraulicConduct;
        public double evapTranspireFract;
        public double waterTemp;

        public double magnesiumXCoeff;
        public double sodiumXCoeff;
        public double potassiumXCoeff;
        public double ammoniaXCoeff;
        public double hydrogenXCoeff;

        public double exchangeCapacity;
        public double maxPhosAdsorption;
        public double density;
        public double tortuosity;
        public int CO2CalcMethod;
        public double CO2ConcenFactor;

        public List<double> weightFract;  // per mineral
        public List<double> solutionConcen;  // for each component
        public List<double> adsorptionIsotherm; // for each component
    }

    public class Node
    {
        public int idNum;
        public string name;
        public string type; //river, reservoir segment, or catchment

        public List<int> upstreamCatchIDs;
        public bool swOutputResults;
        public int numPointSources;
        public List<int> pointSources;
    }

    public class NodeHydro : Node
    {
        public bool swIsSubwaterBoundary;
        public List<int> upstreamRiverIDs;
        public List<int> upstreamReservoirIDs;
        public int numDiversionsTo;
        public List<int> diversionToFilenums;
        public string obsWQFilename;
    }

    public class Catchment : Node
    {
        
        public int METFileNum;
        public double precipMultiplier;
        public double aveTempLapse;
        public double altitudeTempLapse;
        public int airRainChemFileNum;         // from AIRFILES
        public int particleRainChemFileNum;    // from AIRFILES

        public int numSoilLayers;
        public double slope;
        public double width;
        public double aspect;
        public double ManningN;
        public double detentionStorage;

        // landuse
        public List<double> landUsePercent;
        public List<int> fertPlanNum;
        public List<double> landApplicationLoad;
        public List<int> numIrrigationSources;
        public List<List<double>> irrigationSource; 
        public List<double> irrigationSourcePercent;

        public int nluPonds;
        public List<string> pondFilename;
        
        public int numPumpFromSchedules;
        public List<int> pumpFromDivFile;
        public int numPumpToSchedules;
        public List<int> pumpToDivFile;

        public CatchSeptic septic;
        public CatchSediment sediment;
        public CatchBMP bmp;

        public double bufferingPct;
        public double bufferZoneWidth;
        public double bufferZoneSlope;
        public double bufferManningN;
        public double soilLayerSeepage;
        public double overlandFlowSeepage;

        public CatchMining mining;
        public CatchReactions reactions;
        public List<Soil> soils;
    }

    public class River : NodeHydro
    {
        public double depth;
        public double length;
        public double upElevation;
        public double downElevation;
        public double ManningN;
        public double temp;
        public double convectiveHeatFactor;
        public double minFlow;
        
        public bool swViewManagerOutput;

        public double impoundArea;
        public double impoundVol;
        public double freezingTemp;
        public double meltingTemp;
        public double iceCalcAve;

        public List<StageWidth> stageWidthCurve;
        public int numDiversionsFrom;
        public List<int> divFilenumFrom;

        public SimulationOverride overrideSimulation;

        public string hydrologyFilename;

        public double sedDetachVelMult;
        public double sedDetachVelExp;
        public double sedBedDepth;
        public double sedVegFactor;
        public double sedBankStabFactor;
        public double sedDiffusionRate;
        public double sedFirstPartSizePct;
        public double sedSecondPartSizePct;
        public double sedThirdPartSizePct;
        public double reaerationRateMult;
        public double sedOxygenDemand;
        public double precipSettleRate;

        public List<double> waterReactionRate;
        public List<double> bedReactionRate;

        public int numCEQW2Files;
        public string flowInputFilename;
        public string tempInputFilename;
        public string waterQualInputFilename;

        public List<double> componentConcentration;
        public List<double> bedAdsorpConcentration;
        public List<double> waterAdsorpIsotherm;
        public List<double> bedAdsorpIsotherm;
    }

    public class ReservoirSeg : NodeHydro
    {
        public double bottomElevation;
        public int numOutlets;
        public List<ReservoirOutlet> outlets;
        public double precipWgtMult;
        public double tempLapse;
        public double windSpeedMult;
        public double radiationFraction;
        public double radiationFractionReachingDepth;
        public double radiationFractionDepth;
        public double SecchiDiskDepth;
        public double minNegDensity;
        public double minDiffCoeff;
        public double windMixA1Coef;
        public double windMixA2Coef;
        public double windMixMaxDiffCoef;
        public double criticalDensityGradient;
        public double densityGradMaxDiffCoef;
        public double densityGradExp;
        public double inflowEntrain;
        public double sedBottomThickness;
        public double sedDiffusion;
        public double sedOxygenDemand;

        public List<StageArea> bathymetry;

        public List<double> chemConcentrations;
        public List<double> chemConBedSediment;
        public List<DepthTemp> depthTemp;
    }

    public struct StageWidth {
        public double stage;
        public double width;
    }

    public struct SimulationOverride
    {
        public bool swUseObsData;
        public int hydroInterpPeriod;
        public int waterQualityInterpPeriod;
        public int monthAverageMethod;
        public int tdsAdjustmentPriority;
        public int alkAdjustmentPriority;
        public int phAdjustmentPriority;
    }

    public struct StageFlow {
        public double stage;
        public double flow;
    }

    public struct StageArea {
        public double stage;
        public double area;
    }

    public struct ReservoirOutlet {
        public double elevation;
        public double width;
        public int outletType;
        public int numFlowFile;
        public string managedFlowFilename;
    }

    public struct DepthTemp {
        public double depth;
        public double temp;
    }
    
    public struct Reservoir {
		public int idNum;
        public int numSegments;
        public bool swCalcPseudo;
        public int METFilenum;
        public double elevation;
        public int airRainChemFilenum;
        public int coarseAirPartFilenum;
        public string releaseFlowFilename;

        public List<StageFlow> spillway;
        public List<StageArea> bathymetry;

        public bool swAdjustResRelease;
        public string hydrologyFilename;

        public List<double> waterReactionRate;
        public List<double> bedReactionRate;
        public List<double> waterAdsorpIsotherm;
        public List<double> bedAdsorpIsotherm;

        public List<Algae> algae;

        public int numWaterQualParams;
        public List<int> codesWQParams;
        public string waterQualControlFilename;
        public string masterMetFilename;

        public string prescribedOutFlowFilename;

        public int numCEQW2Files;
        public string flowInputFilename;
        public string tempInputFilename;
        public string waterQualInputFilename;

		public int numDerived;
		public List<int> derived;

        public List<ReservoirSeg> reservoirSegs;
    }

    class Coefficients {

        #region Variable Declarations
        public int version;
        public string scenarioDescription;
        public DateTime begDate;
        public DateTime endDate;
        public int numCatchments;
        public int numRivers;
        public int numReservoirSegs;
        public int numReservoirs;
        public int numTimeStepsPerDay;
        public int numSimLoops;
        public int numAutoCalibrationLoops;
        public bool swCalculateLoading;

        public bool swWaterQualSim;
        public bool swLakeSeepageSim;
        public bool swSedimentSim;
        public bool swFertSim;
        public bool swPointSrcSim;
        public bool swInputCoeffChecks;
        public bool swStartupFile;
        public string startupFileName;

        // input files
        public int numMETFiles;
        public List<string> METFilename;

        public int numDIVFiles;
        public List<DIVFileData> DIVData;

        public int numPTSFiles;
        public List<string> PTSFilename;

        public int numAIRFiles;
        public List<string> AIRFilename;

        // output file names
        public string catchmentOutFilename;
        public string riverOutFilename;
        public string reservoirOutFilename;
        public string loadingOutFilename;
        public string warmstartOutFilename;
        public string textOutFilename;

        // system coefficients
        public double tolerancePH;
        public double toleranceCation1;
        public double toleranceCation2;
        public double watershedArea;
        public double watershedElevation;
        public double latitude;
        public double longitude;
        public double evaporationScaling;
        public double evaporationSeasonSkew;
        public double atmosphericTurbidity;
        public double soilThermalConduct;
        public int rainBalancingIons;
        public int airBalancingIons;
        public int numHydrologyParams;
        public int numChemicalParams;
        public int numPhysicalParams;
        public int numCompositeParams;
        public int numComponents;
        public int numReactions;
        public int numSedParticleSizes;
        public int numSedWashLoadParticles;
        public int numAlgae;
        public int numMinerals;

        public List<HydrologyConstits> hydroConstits;
        public List<ChemicalConstits> chemConstits;
        public List<PhysicalConstits> physicalConstits;
        public List<CompositeConstits> compositeConstits;
        public List<HydrologyConstits> AllConstits;
        public List<Reaction> reactions;
        public List<Sediment> sediments;
        public List<Algae> algaes;
        public Periphyton peri;

        public double sedimentShading;
        public double algaeShading;
        public double detritusShading;

        public List<Mineral> minerals;
        public Litter litter;
        public Snow snow;
        public Septic septic;

        // land use data
        public List<double> partDV;     // one per month
        public List<double> coarseDV;   // one per month
        public bool swGasDepositVelocity;
        public List<double> gasDepositVelocity; // one per month  --unused MRL
        public List<double> gasUptakeVelocity;  // one per month
        public double heightWindSpeed;
        public double vonkar;
        public int numLanduses;
        public double standingBiomass;
        public List<Landuse> landuse;

        public List<Catchment> catchments;
        public List<River> rivers;
        public List<Reservoir> reservoirs;

        public bool isTMDLSimulation;
        #endregion

        #region Methods - Reading Coefficients
        
        // test the line code to make sure it's what it expected
        public bool TestLine(string line, int lnum, string abbrev) {
            if (!line.StartsWith(abbrev)) {
				Debug.WriteLine("Expected line " + lnum + " to start with " + abbrev + ". Starts with |" + line.Substring(0, 8) + "|");
                return false;
            }
            //Debug.WriteLine("Processing line " + lnum);
            return true;
        }

        // read a line that is a single ON/OFF switch
        public bool ReadOnOffSwitch(STechStreamReader sr, string lineAbbrev) {
            string line;
            line = sr.ReadLine();
            if (TestLine(line,sr.LineNum,lineAbbrev)) 
                return line.Contains("ON") ? true : false;
            else {  //  bad abbrev when looking for switch so default is OFF
                return false;
            }   
        }

        // reads in single integer
        public int ReadInt(STechStreamReader sr, string lineAbbrev) {
            List<int> data;
            data = ReadIntData(sr, lineAbbrev, 1);
            return data[0];
        }

        // reads in monthly integers - 9 per line
        public List<int> ReadMonthlyIntData(STechStreamReader sr, string lineAbbrev) {
            return ReadIntData(sr, lineAbbrev, 12);
        }

        // reads in integers - 9 per line
        public List<int> ReadIntData(STechStreamReader sr, string lineAbbrev, int num) {
            int intRes;
            string line;
            int linesToRead = (num-1) / 9 + 1;

            List<int> data = new List<int>();
            for (int nlines = 0; nlines < linesToRead; nlines++) {
                line = sr.ReadLine();
                if (TestLine(line, sr.LineNum, lineAbbrev)) {
                    int jj = 0;
					try {
						while (nlines * 9 + jj < num && jj < 9) {
							data.Add(Int32.TryParse(line.Substring(8 * (jj + 1), 8), out intRes) ? intRes : 0);
							jj++;
						}
					}
					catch {
						Debug.WriteLine("MISSING INTEGERS\nExpected " + num + " numbers on line " + sr.LineNum + "\nLine = |" + line + "|");
					}

				} else {
                    data.Add(0);    // what to do if line has error?  MRL
					Debug.WriteLine("Expected line code " + lineAbbrev + ". Saw line code " + line.Substring(0, 8) + " at line " + sr.LineNum);
                }
            }
            return data;
        }

        // reads in single double
        public double ReadDouble(STechStreamReader sr, string lineAbbrev) {
            List<double> data;
            data = ReadDoubleData(sr, lineAbbrev, 1);
            return data[0];
        }

        // reads in monthly doubles - 9 per line
        public List<double> ReadMonthlyDoubleData(STechStreamReader sr, string lineAbbrev) {
            return ReadDoubleData(sr, lineAbbrev, 12);
        }

        // reads in doubles - 9 per line
        public List<double> ReadDoubleData(STechStreamReader sr, string lineAbbrev, int num) {
            double dblRes;
            string line;
            int linesToRead = (num-1) / 9 + 1;

            List<double> data = new List<double>();
            for (int nlines = 0; nlines < linesToRead; nlines++) {
                line = sr.ReadLine();
                if (TestLine(line, sr.LineNum, lineAbbrev)) {
                    int jj = 0;
					try {
						while (nlines * 9 + jj < num && jj < 9) {
							data.Add(Double.TryParse(line.Substring(8 * (jj + 1), 8), out dblRes) ? dblRes : 0);
							jj++;
						}
					} catch {
						Debug.WriteLine("MISSING DOUBLES\nExpected "+num+" numbers on line " + sr.LineNum+"\nLine = |"+line+"|");
					}
				}
				else {
					Debug.WriteLine("Expected line code " + lineAbbrev + ". Saw line code " + line.Substring(0, 8) + " at line " + sr.LineNum);
					data.Add(0);
				}
			}
            return data;
        }

        // read in a line that's a single string
        public string ReadString(STechStreamReader sr, string lineAbbrev) {
            string line = sr.ReadLine();
            if (lineAbbrev != "")
                if (TestLine(line, sr.LineNum, lineAbbrev)) {
					try {
						return line.Substring(8);
					} catch {
						// no string found on line,
						Debug.WriteLine("MISSING STRING\nExpected a string on line " + sr.LineNum + "\nLine = |" + line + "|");
						return "";
					}
                }
                else {
                    return "";  // wrong line code; what should we return?  MRL
                }
            return line;    // return entire line if no line code
        }

		// read an (ignored) line into a string
		public string ReadSpacerLine(STechStreamReader sr, string text) {
			string line;
			line = sr.ReadLine();
			if (!line.Contains(text))
				Debug.WriteLine("SPACER LINE\nExpected: |" + text + "| at line num "+sr.LineNum+"\nLine = |" + line + "|");
			return line;
		}

        // read in and parse the Coefficients file
        public bool ReadCOE(string filename) {
            string line;
            int intRes;
            double dblRes;
            List<int> nums;
            List<double> dnums;

			Logger.Info("Reading coefficients file " + filename);
            STechStreamReader sr = null;
            try {
                sr = new STechStreamReader(filename);
                version = ReadInt(sr, "VERSION");

                // Scenario description
                scenarioDescription = ReadString(sr, "");

                //begdate
                nums = ReadIntData(sr, "BEGDATE", 3);
                begDate = new DateTime(nums[2], nums[1], nums[0]);

                //enddate
                nums = ReadIntData(sr, "ENDDATE", 3);
                endDate = new DateTime(nums[2], nums[1], nums[0]);

                line = ReadSpacerLine(sr,"****");

                // numbers of objects in model
                nums = ReadIntData(sr, "SYSTEM", 8);
                numCatchments = nums[0];
                numRivers = nums[1];
                numReservoirSegs = nums[2];
                numReservoirs = nums[3];
                numTimeStepsPerDay = nums[4];
                numSimLoops = nums[5];
                numAutoCalibrationLoops = nums[6];
                swCalculateLoading = nums[7] == 0 ? false : true;
                swWaterQualSim = ReadOnOffSwitch(sr, "QUAL");
                swLakeSeepageSim = ReadOnOffSwitch(sr, "SEEPS");
                swSedimentSim = ReadOnOffSwitch(sr, "SEDMNT");
                swFertSim = ReadOnOffSwitch(sr, "FERTLZ");
                swPointSrcSim = ReadOnOffSwitch(sr, "POINTS");
                swInputCoeffChecks = ReadOnOffSwitch(sr, "CHECKS");

                line = sr.ReadLine();
                if (TestLine(line, sr.LineNum, "WARMST")) {
                    swStartupFile = line.Substring(8, 8).Contains("1") ? true : false;
                    if (swStartupFile) startupFileName = line.Substring(16);
                }

                //METFILES
                numMETFiles = ReadInt(sr, "METFILE");
                METFilename = new List<string>();
                for (int ii = 0; ii < numMETFiles; ii++)
                    METFilename.Add(ReadString(sr, "METFILE"));

                //DIVFILES
                numDIVFiles = ReadInt(sr, "DIVFILES");

                DIVData = new List<DIVFileData>();
                for (int ii = 0; ii < numDIVFiles; ii++) {
                    line = sr.ReadLine();
                    DIVFileData dfd = new DIVFileData();
                    dfd.flowDivertedMultiplier = Double.TryParse(line.Substring(8, 8), out dblRes) ? dblRes : 0;
                    dfd.flowCap = Double.TryParse(line.Substring(16, 8), out dblRes) ? dblRes : 0;
                    dfd.swUseMonthFlows = !line.Substring(24, 8).Contains("0");
                    // extra column of 8 in these lines (32-39)  MRL
                    dfd.filename = line.Substring(40);
                    dfd.monthlyFlow = ReadMonthlyDoubleData(sr, "DIVFILES");
                    DIVData.Add(dfd);
                }

                // PTSFILES
                numPTSFiles = ReadInt(sr, "PTSFILES");
                PTSFilename = new List<string>();
                for (int ii = 0; ii < numPTSFiles; ii++)
                    PTSFilename.Add(ReadString(sr, "PTSFILES"));

                // AIRFILES
                numAIRFiles = ReadInt(sr, "AIRFILE");   //spec says "AIRFILES" but sample data has "AIRFILE" - MRL
                AIRFilename = new List<string>();
                for (int ii = 0; ii < numAIRFiles; ii++)
                    AIRFilename.Add(ReadString(sr, "AIRFILE"));

                // output filenames
                catchmentOutFilename = ReadString(sr, "FILES");
                riverOutFilename = ReadString(sr, "FILES");
                reservoirOutFilename = ReadString(sr, "FILES");
                loadingOutFilename = ReadString(sr, "FILES");
                warmstartOutFilename = ReadString(sr, "FILES");
                textOutFilename = ReadString(sr, "FILES");

				// System coefficients
				line = ReadSpacerLine(sr, "****");
                dnums = ReadDoubleData(sr, "TOL", 4);
                tolerancePH = dnums[0];
                // nums[1] is unused;
                toleranceCation1 = dnums[2];
                toleranceCation2 = dnums[3];

                dnums = ReadDoubleData(sr, "TAREA", 2);
                watershedArea = dnums[0];
                watershedElevation = dnums[1];

                dnums = ReadDoubleData(sr, "EVPMAX", 5);
                latitude = dnums[0];
                longitude = dnums[1]; ;
                evaporationScaling = dnums[2]; ;
                evaporationSeasonSkew = dnums[3]; ;
                atmosphericTurbidity = dnums[4]; ;
                soilThermalConduct = ReadDouble(sr, "RKCONV");

                nums = ReadIntData(sr, "RINPUT", 2);
                rainBalancingIons = nums[0];
                airBalancingIons = nums[1];

                // Hydrology
                AllConstits = new List<HydrologyConstits>();
                hydroConstits = new List<HydrologyConstits>();
                numHydrologyParams = ReadInt(sr, "CONSTITS");
                for (int ii = 0; ii < numHydrologyParams; ii++) {
                    line = sr.ReadLine();
                    HydrologyConstits hydro = new HydrologyConstits();
                    if (TestLine(line, sr.LineNum, "CONSTIT")) {
                        hydro.fortranCode = line.Substring(8, 8);
                        hydro.swIncludeInOutput = !line.Substring(16, 8).Contains("0");
                        hydro.units = line.Substring(24, 16);
                        hydro.abbrevName = line.Substring(40, 16);
                        hydro.fullName = line.Substring(56);

                        nums = ReadIntData(sr, "CONSTIT", 4);
                        hydro.swCatchmentInclude = nums[0] != 0;
                        hydro.swRiverInclude = nums[1] != 0;
                        hydro.swReservoirInclude = nums[2] != 0;
                        hydro.swLoadingInclude = nums[3] != 0;
                    }
                    hydroConstits.Add(hydro);
                    AllConstits.Add(hydro);
                }

                // Chemical
                chemConstits = new List<ChemicalConstits>();
                numChemicalParams = ReadInt(sr, "CONSTITS");
                for (int ii = 0; ii < numChemicalParams; ii++) {
                    line = sr.ReadLine();
                    ChemicalConstits chem = new ChemicalConstits();
                    if (TestLine(line, sr.LineNum, "CHEMICAL")) {
                        chem.fortranCode = line.Substring(8, 8);
                        chem.swIncludeInOutput = !line.Substring(16, 8).Contains("0");
                        chem.units = line.Substring(24, 16);
                        chem.abbrevName = line.Substring(40, 16);
                        chem.fullName = line.Substring(56);

                        nums = ReadIntData(sr, "CHEMICAL", 4);
                        chem.swCatchmentInclude = nums[0] != 0;
                        chem.swRiverInclude = nums[1] != 0;
                        chem.swReservoirInclude = nums[2] != 0;
                        chem.swLoadingInclude = nums[3] != 0;

                        line = sr.ReadLine();
                        if (TestLine(line, sr.LineNum, "CHEMICAL")) {
                            chem.electricalCharge = Double.TryParse(line.Substring(8, 8), out dblRes) ? dblRes : 0;
                            chem.massEquivalent = Double.TryParse(line.Substring(16, 8), out dblRes) ? dblRes : 0;
                            chem.loadingUnitConversion = Double.TryParse(line.Substring(24, 8), out dblRes) ? dblRes : 0;
                            chem.loadingUnits = line.Substring(32);
                        }

                        dnums = ReadDoubleData(sr, "CHEMICAL", 9);
                        chem.airRainMult = dnums[0];
                        chem.pointSourceMult = dnums[1];
                        chem.nonpointSourceMult = dnums[2];
                        chem.solubWithSulfate = dnums[3];
                        chem.stoichChemWithSulfate = dnums[4];
                        chem.stoichSulfateWithChem = dnums[5];
                        chem.solubWithHydrox = dnums[6];
                        chem.stoichChemWithHydrox = dnums[7];
                        chem.stoichHydroxWithChem = dnums[8];

                        nums = ReadIntData(sr, "CHEMICAL", 3);
                        chem.swLoadingTMDL = nums[0] != 0;
                        chem.dryDepositionForm = nums[1];
                        chem.swChemAdvection = nums[2] != 0;

                        chem.gasDepositVelocity = ReadMonthlyDoubleData(sr, "CHEMICAL");
                    }
                    chemConstits.Add(chem);
                    AllConstits.Add(chem);
                }

                // Physical
                physicalConstits = new List<PhysicalConstits>();
                numPhysicalParams = ReadInt(sr, "CONSTITS");
                for (int ii = 0; ii < numPhysicalParams; ii++) {
                    line = sr.ReadLine();
                    PhysicalConstits physical = new PhysicalConstits();
                    if (TestLine(line, sr.LineNum, "PHYSICAL")) {
                        physical.fortranCode = line.Substring(8, 8);
                        physical.swIncludeInOutput = !line.Substring(16, 8).Contains("0");
                        physical.units = line.Substring(24, 16);
                        physical.abbrevName = line.Substring(40, 16);
                        physical.fullName = line.Substring(56);

                        nums = ReadIntData(sr, "PHYSICAL", 4);
                        physical.swCatchmentInclude = nums[0] != 0;
                        physical.swRiverInclude = nums[1] != 0;
                        physical.swReservoirInclude = nums[2] != 0;
                        physical.swLoadingInclude = nums[3] != 0;

                        line = sr.ReadLine();
                        if (TestLine(line, sr.LineNum, "PHYSICAL")) {
                            physical.electricalCharge = Double.TryParse(line.Substring(8, 8), out dblRes) ? dblRes : 0;
                            physical.massEquivalent = Double.TryParse(line.Substring(16, 8), out dblRes) ? dblRes : 0;
                            physical.loadingUnitConversion = Double.TryParse(line.Substring(24, 8), out dblRes) ? dblRes : 0;
                            physical.loadingUnits = line.Substring(32);
                        }

                        dnums = ReadDoubleData(sr, "PHYSICAL", 3);
                        physical.airRainMult = dnums[0];
                        physical.pointSourceMult = dnums[1];
                        physical.nonpointSourceMult = dnums[2];
                        //There are six unused spots here - SAS

                        nums = ReadIntData(sr, "PHYSICAL", 3);
                        physical.swLoadingTMDL = nums[0] != 0;
                        physical.dryDepositionForm = nums[1];
                        physical.swChemAdvection = nums[2] != 0;

                        physical.gasDepositVelocity = ReadMonthlyDoubleData(sr, "PHYSICAL");
                    }
                    physicalConstits.Add(physical);
                    AllConstits.Add(physical);
                }

                numComponents = numChemicalParams + numPhysicalParams;

                // Composite
                compositeConstits = new List<CompositeConstits>();
                numCompositeParams = ReadInt(sr, "CONSTITS");
                for (int ii = 0; ii < numCompositeParams; ii++) {
                    line = sr.ReadLine();
                    CompositeConstits composite = new CompositeConstits();
                    if (TestLine(line, sr.LineNum, "COMPOSIT")) {
                        composite.fortranCode = line.Substring(8, 8);
                        composite.swIncludeInOutput = !line.Substring(16, 8).Contains("0");
                        composite.units = line.Substring(24, 16);
                        composite.abbrevName = line.Substring(40, 16);
                        composite.fullName = line.Substring(56);

                        nums = ReadIntData(sr, "COMPOSIT", 4);
                        composite.swCatchmentInclude = nums[0] != 0;
                        composite.swRiverInclude = nums[1] != 0;
                        composite.swReservoirInclude = nums[2] != 0;
                        composite.swLoadingInclude = nums[3] != 0;

                        line = sr.ReadLine();
                        if (TestLine(line, sr.LineNum, "COMPOSIT")) {
                            composite.electricalCharge = Double.TryParse(line.Substring(8, 8), out dblRes) ? dblRes : 0;
                            composite.massEquivalent = Double.TryParse(line.Substring(16, 8), out dblRes) ? dblRes : 0;
                            composite.loadingUnitConversion = Double.TryParse(line.Substring(24, 8), out dblRes) ? dblRes : 0;
                            composite.loadingUnits = line.Substring(32);
                        }

                        nums = ReadIntData(sr, "COMPOSIT", 3);
                        composite.swIncludeDissolvedConstits = nums[0] != 0;
                        composite.swIncludeAdsorbedConstits = nums[1] != 0;
                        composite.swIncludePrecipConstits = nums[2] != 0;

                        composite.componentTotalMass = ReadDoubleData(sr, "COMPONEN", numComponents);
                    }
                    compositeConstits.Add(composite);
                    AllConstits.Add(composite);
                }

                // Reactions
                reactions = new List<Reaction>();
                numReactions = ReadInt(sr, "REACTION");
                for (int ii = 0; ii < numReactions; ii++) {
                    line = sr.ReadLine();
                    Reaction reaction = new Reaction();
                    if (TestLine(line, sr.LineNum, "REACTION")) {
                        reaction.primReactantNumber = Int32.TryParse(line.Substring(8, 8), out intRes) ? intRes : 0;
                        reaction.swIsAnoxic = !line.Substring(16, 8).Contains("0");
                        reaction.dissolvedOxyLimit = Double.TryParse(line.Substring(24, 8), out dblRes) ? dblRes : 0;
                        reaction.swIsUVCatalysis = !line.Substring(32, 8).Contains("0");
                        reaction.numLinkedReactions = Int32.TryParse(line.Substring(40, 8), out intRes) ? intRes : 0;
                        reaction.tempCorrectCoeff = Double.TryParse(line.Substring(48, 8), out dblRes) ? dblRes : 0;
                        reaction.fortranCode = line.Substring(56, 8);
                        reaction.units = line.Substring(64, 8);
                        reaction.name = line.Substring(72);

                        reaction.stoich = ReadDoubleData(sr, "STOICH", numComponents);
                    }
                    reactions.Add(reaction);
                }

				// Sediments
				line = ReadSpacerLine(sr, "SEDIMENT");
                sediments = new List<Sediment>();
                nums = ReadIntData(sr, "NPART", 2);
                numSedParticleSizes = nums[0];
                numSedWashLoadParticles = nums[1];
                for (int ii = 0; ii < numSedParticleSizes; ii++) {
                    Sediment sediment = new Sediment();
                    dnums = ReadDoubleData(sr, "SEDIMEN", 3);
                    sediment.grainSize = dnums[0];
                    sediment.specGravity = dnums[1];
                    sediment.settlingRate = dnums[2];
                    sediments.Add(sediment);
                }

				// Algae
				line = ReadSpacerLine(sr, "ALGAE & PERIPHYTON");
                algaes = new List<Algae>();
                numAlgae = 3;
                for (int ii = 0; ii < numAlgae; ii++) {
                    Algae algae = new Algae();
                    dnums = ReadDoubleData(sr, "WQCOEF", 7);
                    algae.nitroHalfSat = dnums[0];
                    algae.phosHalfSat = dnums[1];
                    algae.silicaHalfSat = dnums[2];
                    algae.lightSat = dnums[3];
                    algae.lowTempLimit = dnums[4];
                    algae.highTempLimit = dnums[5];
                    algae.optGrowTemp = dnums[6];
                    algaes.Add(algae);
                }

                // Periphyton
                peri = new Periphyton();
                dnums = ReadDoubleData(sr, "PERIPH", 5);
                peri.recycledFraction = dnums[0];
                peri.velocityHalfSat = dnums[1];
                peri.nitroHalfSat = dnums[2];
                peri.phosHalfSat = dnums[3];
                peri.lightSat = dnums[4];
                dnums = ReadDoubleData(sr, "PERIPH", 8);
                peri.scourRegressionCoef = dnums[0];
                peri.scourRegressionExp = dnums[1];
                peri.ammoniaPref = dnums[2];
                peri.spatialLimitHalfSat = dnums[3];
                peri.spatialLimitIntercept = dnums[4];
                peri.endoRespirationCoef = dnums[5];
                peri.endoRespirationExp = dnums[6];
                peri.photoRespirationFraction = dnums[7];

                // Shading
                dnums = ReadDoubleData(sr, "SHADING", 3);
                sedimentShading = dnums[0];
                algaeShading = dnums[1];
                detritusShading = dnums[2];

				// Minerals
				line = ReadSpacerLine(sr, "MINERALS");
                minerals = new List<Mineral>();
                numMinerals = ReadInt(sr, "NMNRLS");
                for (int ii = 0; ii < numMinerals; ii++) {
                    line = sr.ReadLine();
                    Mineral mineral = new Mineral();
                    if (TestLine(line, sr.LineNum, "MINERL")) {
                        mineral.name = line.Substring(8, 16);
                        mineral.molecularWgt = Double.TryParse(line.Substring(24, 8), out dblRes) ? dblRes : 0;
                        mineral.phDepend = Double.TryParse(line.Substring(32, 8), out dblRes) ? dblRes : 0;
                        mineral.weatheringRate = Double.TryParse(line.Substring(40, 8), out dblRes) ? dblRes : 0;
                        mineral.oxyConsumption = Double.TryParse(line.Substring(48, 8), out dblRes) ? dblRes : 0;
                        mineral.chemReactionProduct = ReadDoubleData(sr, "MNWTH", numChemicalParams);
                    }
                    minerals.Add(mineral); //test for mineral
                }

				// Litter leachable ion params
				line = ReadSpacerLine(sr, "LITTER & HUMUS");
                litter = new Litter();
                dnums = ReadDoubleData(sr, "FRLCH", 4);
                litter.coarseLitterFrac = dnums[0];
                litter.fineLitterFrac = dnums[1];
                litter.humusFrac = dnums[2];
                litter.nonStructLeach = dnums[3];
                dnums = ReadDoubleData(sr, "RATES", 3);
                litter.coarseLitterDecay = dnums[0];
                litter.fineLitterDecay = dnums[1];
                litter.humusDecay = dnums[2];

				// Snow
				line = ReadSpacerLine(sr, "SNOW AND ICE");
                snow = new Snow();
                dnums = ReadDoubleData(sr, "SNOW", 9);
                snow.formTemp = dnums[0];
                snow.openAreaSubRate = dnums[1];
                snow.forestAreaSubRate = dnums[2];
                snow.initialDepth = dnums[3];
                snow.openAreaMeltRate = dnums[4];
                snow.forestAreaMeltRate = dnums[5];
                snow.fieldCapacity = dnums[6];
                snow.meltTemp = dnums[7];
                snow.rainMeltRate = dnums[8];
                dnums = ReadDoubleData(sr, "SNOWLCH", 5);
                snow.thermalConduct = dnums[0];
                snow.iceThermalConduct = dnums[1];
                snow.meltLeaching = dnums[2];
                snow.nitrificationRate = dnums[3];

				// Septic
				line = ReadSpacerLine(sr, "SEPTIC");
                septic = new Septic();
                septic.failedFlow = ReadDouble(sr, "SEPTIC");
                septic.type1 = ReadDoubleData(sr, "SEPTIC", numComponents);
                septic.type2 = ReadDoubleData(sr, "SEPTIC", numComponents);
                septic.type3 = ReadDoubleData(sr, "SEPTIC", numComponents);

				// Land use data
				line = ReadSpacerLine(sr, "CANOPY AND LAND USE");
                // general land use params
                partDV = ReadMonthlyDoubleData(sr, "PARTDV");
                coarseDV = ReadMonthlyDoubleData(sr, "COARSEDV");
                swGasDepositVelocity = ReadOnOffSwitch(sr, "IVDGAS");
                if (swGasDepositVelocity==true)
                {
                    gasDepositVelocity = ReadMonthlyDoubleData(sr, "NOXSOXVD");
                }
                  
                gasUptakeVelocity = ReadMonthlyDoubleData(sr, "NOXSOXVU");

                dnums = ReadDoubleData(sr, "HEIGHT", 2);
                heightWindSpeed = dnums[0];
                vonkar = dnums[1];

				dnums = ReadDoubleData(sr, "NITRIFYR", 2);
				numLanduses = (int)dnums[0];
				standingBiomass = dnums[1];

                // Land use individual data
                landuse = new List<Landuse>();

                for (int ii = 0; ii < numLanduses; ii++) {
					line = ReadSpacerLine(sr, "LAND USE TYPE");
                    line = sr.ReadLine();
                    Landuse lu = new Landuse();
                    if (TestLine(line, sr.LineNum, "INTCEPT")) {
                        lu.openWinterFrac = Double.TryParse(line.Substring(8, 8), out dblRes) ? dblRes : 0;
                        lu.imperviousFrac = Double.TryParse(line.Substring(16, 8), out dblRes) ? dblRes : 0;
                        lu.maxPotentInceptStorage = Double.TryParse(line.Substring(24, 8), out dblRes) ? dblRes : 0;
                        lu.name = line.Substring(32);

                        dnums = ReadDoubleData(sr, "EROSION", 2);
                        lu.rainDetachFactor = dnums[0];
                        lu.flowDetachFactor = dnums[1];
                        dnums = ReadDoubleData(sr, "GROWTH", 8);
                        lu.annualGrowMult = dnums[0];
                        lu.leafGrowFactor = dnums[1];
                        lu.productivity = dnums[2];
                        lu.maintRespRate = dnums[3];
                        lu.activeRespRate = dnums[4];
                        lu.dryCollEff = dnums[5];
                        lu.wetCollEff = dnums[6];
                        lu.leafWgtArea = dnums[7];

                        dnums = ReadDoubleData(sr, "HEIGHT", 2);
                        lu.canopyHeight = dnums[0];
                        lu.stomatalResist = dnums[1];
                        lu.cropping = ReadMonthlyDoubleData(sr, "CROPPING");
                        lu.leafAreaIdx = ReadMonthlyDoubleData(sr, "LAID");
                        lu.annualUptake = ReadMonthlyDoubleData(sr, "UDISTD");
                        lu.litterFallRate = ReadMonthlyDoubleData(sr, "LFD");
                        lu.exudationRate = ReadMonthlyDoubleData(sr, "BETAD");
                        lu.leafComp = ReadDoubleData(sr, "LFCMPD1", numChemicalParams);
                        lu.trunkComp = ReadDoubleData(sr, "TRCMPD", numChemicalParams);

                        lu.numFertPlans = ReadInt(sr, "FERTLZ");
                        lu.fertPlanApplication = new List<List<List<double>>>();
						List <List<double>> fertPlan = new List<List<double>>();
                        for (int jj = 0; jj < lu.numFertPlans; jj++) {
                            for (int kk = 0; kk < 12; kk++) {
							fertPlan.Add(ReadDoubleData(sr, "FERTLZ", numComponents));
                            }
							lu.fertPlanApplication.Add(fertPlan);
                        }
						landuse.Add(lu);
                    }
                }

				// CATCHMENTS
				line = ReadSpacerLine(sr, "CATCHMENT COEFFICIENTS");

                catchments = new List<Catchment>();
                for (int ii = 0; ii < numCatchments; ii++) {
                    line = sr.ReadLine();  // spacer line: CATC####  - can these have 5 digits?  MRL
                    line = sr.ReadLine();
                    Catchment catchData = new Catchment();
                    catchData.idNum = Int32.TryParse(line.Substring(8, 8), out intRes) ? intRes : 0;
                    catchData.METFileNum = Int32.TryParse(line.Substring(16, 8), out intRes) ? intRes : 0;
                    catchData.precipMultiplier = Double.TryParse(line.Substring(24, 8), out dblRes) ? dblRes : 0;
                    catchData.aveTempLapse = Double.TryParse(line.Substring(32, 8), out dblRes) ? dblRes : 0;
                    catchData.altitudeTempLapse = Double.TryParse(line.Substring(40, 8), out dblRes) ? dblRes : 0;
                    catchData.swOutputResults = !line.Substring(48, 8).Contains("0");
                    catchData.airRainChemFileNum = Int32.TryParse(line.Substring(56, 8), out intRes) ? intRes : 0;
                    catchData.particleRainChemFileNum = Int32.TryParse(line.Substring(64, 8), out intRes) ? intRes : 0;
                    catchData.name = line.Substring(72);
                    line = sr.ReadLine();
                    catchData.numSoilLayers = Int32.TryParse(line.Substring(8, 8), out intRes) ? intRes : 0;
                    catchData.slope = Double.TryParse(line.Substring(16, 8), out dblRes) ? dblRes : 0;
                    catchData.width = Double.TryParse(line.Substring(24, 8), out dblRes) ? dblRes : 0;
                    catchData.aspect = Double.TryParse(line.Substring(32, 8), out dblRes) ? dblRes : 0;
                    catchData.ManningN = Double.TryParse(line.Substring(40, 8), out dblRes) ? dblRes : 0;
                    catchData.detentionStorage = Double.TryParse(line.Substring(48, 8), out dblRes) ? dblRes : 0;

                    // upstream catchment numbers
                    catchData.upstreamCatchIDs = ReadIntData(sr, "ICAT", 9);  // will there always be 9 values ? - MRL
                    catchData.landUsePercent = ReadDoubleData(sr, "CATC", numLanduses);

                    // fertilation plan nums per land use
                    catchData.fertPlanNum = ReadIntData(sr, "CATC", numLanduses);

                    // land application load
                    catchData.landApplicationLoad = ReadDoubleData(sr, "STOC", numLanduses);

                    // num of irrigation sources
                    catchData.numIrrigationSources = ReadIntData(sr, "IRRI", numLanduses);

                    // for each land use, get number of irrigation sources and fraction of area
                    // This is not written correctly - need to test with a coe that has irrigation sources - SAS 11/13/19
                    for (int jj=0; jj<numLanduses; jj++) {
                        catchData.irrigationSource = new List<List<double>>();
                        if (catchData.numIrrigationSources[jj] > 0) {
                            catchData.irrigationSource.Add(ReadDoubleData(sr, "IRRL", 2 * catchData.numIrrigationSources[jj])); 
                        }
                    }

                    catchData.nluPonds = ReadInt(sr, "NLUPONDS");
                    if (catchData.nluPonds > 0) catchData.pondFilename = new List<string>();
                    for (int jj = 0; jj < catchData.nluPonds; jj++)
                        catchData.pondFilename.Add(ReadString(sr, "PONDFILE"));

                    // point sources
                    catchData.numPointSources = ReadInt(sr, "PTSOURCE");
                    if (catchData.numPointSources > 0)
                        catchData.pointSources = ReadIntData(sr, "PTSOURCE", catchData.numPointSources);

                    // pumping
                    catchData.numPumpFromSchedules = ReadInt(sr, "PUMPFROM");
                    if (catchData.numPumpFromSchedules > 0)
                        catchData.pumpFromDivFile = ReadIntData(sr, "PUMPFROM", catchData.numPumpFromSchedules);
                    catchData.numPumpToSchedules = ReadInt(sr, "PUMPTO");
                    if (catchData.numPumpToSchedules > 0)
                        catchData.pumpToDivFile = ReadIntData(sr, "PUMPTO", catchData.numPumpToSchedules);

                    // septic
                    dnums = ReadDoubleData(sr, "SEPTIC", 9);
                    catchData.septic = new CatchSeptic();
                    catchData.septic.soilLayer = dnums[0];
                    catchData.septic.population = dnums[1]; 
                    catchData.septic.standardPct = dnums[2]; 
                    catchData.septic.advancedPct = dnums[3]; 
                    catchData.septic.failingPct = dnums[4]; 
                    catchData.septic.initialBiomass = dnums[5]; 
                    catchData.septic.thickness = dnums[6]; 
                    catchData.septic.area = dnums[7]; 
                    catchData.septic.biomassRespRate = dnums[8]; 
                    catchData.septic.biomassMortRate = ReadDouble(sr, "SEPTIC");

                    // sediment
                    dnums = ReadDoubleData(sr, "SEDIMENT", 4);
                    catchData.sediment = new CatchSediment();
                    catchData.sediment.erosivity = dnums[0];
                    catchData.sediment.firstPartSizePct = dnums[1];
                    catchData.sediment.secondPartSizePct = dnums[2];
                    catchData.sediment.thirdPartSizePct = dnums[3];

                    // Best management practices
                    dnums = ReadDoubleData(sr, "BMP", 5);
                    catchData.bmp = new CatchBMP();
                    catchData.bmp.streetSweepFreq = dnums[0];
                    catchData.bmp.streetSweepEff = dnums[1];
                    catchData.bmp.divertedImpervFlow = dnums[2];
                    catchData.bmp.detentionPondVol = dnums[3];
                    catchData.bmp.maxFertAccumTime = dnums[4];

                    // stream bank buffers
                    dnums = ReadDoubleData(sr, "BUFFZONE", 4);
                    catchData.bufferingPct = dnums[0];
                    catchData.bufferZoneWidth = dnums[1];
                    catchData.bufferZoneSlope = dnums[2];
                    catchData.bufferManningN = dnums[3];
                    dnums = ReadDoubleData(sr, "SEEPS", 2);
                    catchData.soilLayerSeepage = dnums[0];
                    catchData.overlandFlowSeepage = dnums[1];

                    // mining
                    line = sr.ReadLine();
                    catchData.mining = new CatchMining();
                    if (TestLine(line, sr.LineNum, "MINING")) {
                        catchData.mining.swIsLowSoilDeepMineOverburden = !line.Substring(8, 8).Contains("0");
                        catchData.mining.surfaceMineLanduseNum = Int32.TryParse(line.Substring(16, 8), out intRes) ? intRes : 0;
                        catchData.mining.depthSpoils = Double.TryParse(line.Substring(24, 8), out dblRes) ? dblRes : 0;
                        catchData.mining.soilMoisture = Double.TryParse(line.Substring(32, 8), out dblRes) ? dblRes : 0;
                        catchData.mining.fieldCapacity = Double.TryParse(line.Substring(40, 8), out dblRes) ? dblRes : 0;
                        catchData.mining.saturationMoisture = Double.TryParse(line.Substring(48, 8), out dblRes) ? dblRes : 0;
                        catchData.mining.horizHydraulicConduct = Double.TryParse(line.Substring(56, 8), out dblRes) ? dblRes : 0;
                        catchData.mining.vertHydraulicConduct = Double.TryParse(line.Substring(64, 8), out dblRes) ? dblRes : 0;
                        //catchData.mining.ferroIonOxyidationRate = Double.TryParse(line.Substring(72, 8), out dblRes) ? dblRes : 0;
                    }
                    
                    // deep mines
                    nums = ReadIntData(sr, "MINEPERM", 2);
                    catchData.mining.numDeepMineDischarges = nums[0];
                    catchData.mining.swAreDeepConcentrationsMax = nums[0] != 0;
                    for (int jj = 0; jj < catchData.mining.numDeepMineDischarges; jj++) {
                        line = sr.ReadLine();
                        CatchMine mine = new CatchMine();
                        if (TestLine(line, sr.LineNum, "MINEPERM")) {
                            mine.areaFraction = Double.TryParse(line.Substring(8, 8), out dblRes) ? dblRes : 0;
                            mine.name = line.Substring(16);
                            mine.concentrationLimit = ReadDoubleData(sr, "MINECONC", numComponents);
                            mine.mineOutFilename = ReadString(sr, "MINEOUT");
                        }
                        catchData.mining.deepMines.Add(mine);
                    }

                    // surface mines
                    nums = ReadIntData(sr, "MINEPERM", 2);
                    catchData.mining.numSurfaceMineDischarges = nums[0];
                    catchData.mining.swAreSurfaceConcentrationsMax = nums[0] != 0;
                    for (int jj = 0; jj < catchData.mining.numSurfaceMineDischarges; jj++) {
                        line = sr.ReadLine();
                        CatchMine mine = new CatchMine();
                        if (TestLine(line, sr.LineNum, "MINEPERM")) {
                            mine.areaFraction = Double.TryParse(line.Substring(8, 8), out dblRes) ? dblRes : 0;
                            mine.name = line.Substring(16);
                            mine.concentrationLimit = ReadDoubleData(sr, "MINECONC", numComponents);
                            mine.mineOutFilename = ReadString(sr, "MINEOUT");
                        }
                        catchData.mining.surfaceMines.Add(mine);
                    }

                    // litter weights
                    dnums = ReadDoubleData(sr, "XLIT", 3);
                    catchData.mining.coarseLitterWgtFraction = dnums[0];
                    catchData.mining.fineLitterWgtFraction = dnums[1];
                    catchData.mining.humusWgtFraction = dnums[2];

                    // reaction rates
                    catchData.reactions.soilReactionRate = ReadDoubleData(sr, "REACSOIL", numReactions);
                    catchData.reactions.surfaceReactionRate = ReadDoubleData(sr, "REACSURF", numReactions);
                    catchData.reactions.canopyReactionRate = ReadDoubleData(sr, "REACCNPY", numReactions);
                    catchData.reactions.biozoneReactionRate = ReadDoubleData(sr, "REACBIOZ", numReactions);

                    //CE-QUAL-W2
                    catchData.mining.numCEQW2Files = ReadInt(sr, "W2FILES");
                    if (catchData.mining.numCEQW2Files == 3) {
                        catchData.mining.flowInputFilename = ReadString(sr, "W2FILES");
                        catchData.mining.tempInputFilename = ReadString(sr, "W2FILES");
                        catchData.mining.waterQualInputFilename = ReadString(sr, "W2FILES");
                    }

                    // soil layers
                    catchData.soils = new List<Soil>();
                    for (int jj = 0; jj < catchData.numSoilLayers; jj++) {
                        Soil soil = new Soil();
                        dnums = ReadDoubleData(sr, "LAYER", 9);
                        soil.area = dnums[0];
                        soil.thickness = dnums[1];
                        soil.moisture = dnums[2];
                        soil.fieldCapacity = dnums[3];
                        soil.saturationMoisture = dnums[4];
                        soil.horizHydraulicConduct = dnums[5];
                        soil.vertHydraulicConduct = dnums[6];
                        soil.evapTranspireFract = dnums[7];
                        soil.waterTemp = dnums[8];

                        dnums = ReadDoubleData(sr, "CKCAMG", 5);
                        soil.magnesiumXCoeff = dnums[0];
                        soil.sodiumXCoeff = dnums[1];
                        soil.potassiumXCoeff = dnums[2];
                        soil.ammoniaXCoeff = dnums[3];
                        soil.hydrogenXCoeff = dnums[4];

                        line = sr.ReadLine();
                        if (TestLine(line, sr.LineNum, "COMP")) {
                            soil.exchangeCapacity = Double.TryParse(line.Substring(8, 8), out dblRes) ? dblRes : 0;
                            soil.maxPhosAdsorption = Double.TryParse(line.Substring(16, 8), out dblRes) ? dblRes : 0;
                            soil.density = Double.TryParse(line.Substring(24, 8), out dblRes) ? dblRes : 0;
                            soil.tortuosity = Double.TryParse(line.Substring(32, 8), out dblRes) ? dblRes : 0;
                            soil.CO2CalcMethod = Int32.TryParse(line.Substring(40, 8), out intRes) ? intRes : 0;
                            soil.CO2ConcenFactor = Double.TryParse(line.Substring(48, 8), out dblRes) ? dblRes : 0;
                        }
                        soil.weightFract = ReadDoubleData(sr, "COMP", numMinerals);
                        soil.solutionConcen = ReadDoubleData(sr, "SOL", numComponents);
                        soil.adsorptionIsotherm = ReadDoubleData(sr, "ADS", numComponents);
                        catchData.soils.Add(soil);
                    }

                    catchments.Add(catchData);
                }

				// RIVERS
				line = ReadSpacerLine(sr, "RIVER COEFFICIENTS");
                rivers = new List<River>();
                for (int ii = 0; ii < numRivers; ii++) {
					line = ReadSpacerLine(sr, "RIVE");	// RIVE####  - can these have 5 digits?  MRL
                    River river = new River();
					dnums = ReadDoubleData(sr, "STRE", 9);
					river.idNum = (int)dnums[0];
					river.depth = dnums[1];
					river.length = dnums[2];
					river.upElevation = dnums[3];
					river.downElevation = dnums[4];
					river.ManningN = dnums[5];
					river.temp = dnums[6];
					river.convectiveHeatFactor = dnums[7];
					river.minFlow = dnums[8];

					line = sr.ReadLine();
					river.swOutputResults = !line.Substring(8, 8).Contains("0");
					river.swIsSubwaterBoundary = !line.Substring(16, 8).Contains("0");
                    river.swViewManagerOutput = !line.Substring(24, 8).Contains("0");
					river.name = line.Substring(32);

					dnums = ReadDoubleData(sr, "IMPO", 5);
					river.impoundArea = dnums[0];
					river.impoundVol = dnums[1];
					river.freezingTemp = dnums[2];
					river.meltingTemp = dnums[3];
					river.iceCalcAve = dnums[4];

					dnums = ReadDoubleData(sr, "STAR", 18);
					river.stageWidthCurve = new List<StageWidth>();
					for (int jj=0; jj<18; jj+=2) {
						StageWidth sw = new StageWidth();
						sw.stage = dnums[jj];
						sw.width = dnums[jj + 1];
						river.stageWidthCurve.Add(sw);
					}

					river.upstreamCatchIDs = ReadIntData(sr, "ICAT", 9);   // how many upstream catchments - 9? - MRL
					river.upstreamRiverIDs = ReadIntData(sr, "IRVT", 9);   // how many upstream rivers - 9? - MRL
					river.upstreamReservoirIDs = ReadIntData(sr, "ILKT", 9);   // how many upstream reservoirs -9? - MRL

					river.numDiversionsFrom = ReadInt(sr, "DIVFROM");
					if (river.numDiversionsFrom > 0)
						river.divFilenumFrom = ReadIntData(sr, "DIVFROM", river.numDiversionsFrom);
					river.numDiversionsTo = ReadInt(sr, "DIVTO");
					if (river.numDiversionsTo > 0)
						river.diversionToFilenums = ReadIntData(sr, "DIVTO", river.numDiversionsTo);
					river.numPointSources = ReadInt(sr, "PTSOURCE");
					if (river.numPointSources > 0)
						river.pointSources = ReadIntData(sr, "PTSOURCE", river.numPointSources);
					nums = ReadIntData(sr, "OBSW", 7);
                    river.overrideSimulation.swUseObsData = nums[0] != 0;
                    river.overrideSimulation.hydroInterpPeriod = nums[1];
                    river.overrideSimulation.waterQualityInterpPeriod = nums[2];
                    river.overrideSimulation.monthAverageMethod = nums[3];
                    river.overrideSimulation.tdsAdjustmentPriority = nums[4];
                    river.overrideSimulation.alkAdjustmentPriority = nums[5];
                    river.overrideSimulation.phAdjustmentPriority = nums[6];
                    river.hydrologyFilename = ReadString(sr, "OBSD");
					dnums = ReadDoubleData(sr, "SEDIMENT", 6);
					river.sedDetachVelMult = dnums[0];
					river.sedDetachVelExp = dnums[1];
					river.sedBedDepth = dnums[2];
					river.sedVegFactor = dnums[3];
					river.sedBankStabFactor = dnums[4];
					river.sedDiffusionRate = dnums[5];
					dnums = ReadDoubleData(sr, "SEDTYPES", 3);
					river.sedFirstPartSizePct = dnums[0];
					river.sedSecondPartSizePct = dnums[1];
					river.sedThirdPartSizePct = dnums[2];
					dnums = ReadDoubleData(sr, "AIRK", 2);  // data file only has 2 values instead of 3 (which two?) - MRL
					river.reaerationRateMult = dnums[0];
					river.sedOxygenDemand = dnums[1];
					river.precipSettleRate = 0;     // not in data files - MRL

					river.waterReactionRate = ReadDoubleData(sr, "REAC-H2O", numReactions);
					river.bedReactionRate = ReadDoubleData(sr, "REAC-BED", numReactions);
					river.obsWQFilename = ReadString(sr, "OBSD");
					river.numCEQW2Files = ReadInt(sr, "W2FILES");
					if (river.numCEQW2Files == 3) {
						river.flowInputFilename = ReadString(sr, "W2FILES");
						river.tempInputFilename = ReadString(sr, "W2FILES");
						river.waterQualInputFilename = ReadString(sr, "W2FILES");
					}

					river.componentConcentration = ReadDoubleData(sr, "STRC", numComponents);
					river.bedAdsorpConcentration = ReadDoubleData(sr, "BEDC", numComponents);
					river.waterAdsorpIsotherm = ReadDoubleData(sr, "STRA", numComponents);
					river.bedAdsorpIsotherm = ReadDoubleData(sr, "BEDA", numComponents);

                    rivers.Add(river);
                }


				// RESERVOIRS
				line = ReadSpacerLine(sr, "LAKE COEFFICIENTS");
				reservoirs = new List<Reservoir>();
				for (int ii = 0; ii < numReservoirs; ii++) {
					line = ReadSpacerLine(sr, "RESE");	// RESE####  - can these have 5 digits?  MRL
					line = sr.ReadLine();
					Reservoir reservoir = new Reservoir();
					reservoir.idNum = ii;
					reservoir.numSegments = Int32.TryParse(line.Substring(8, 8), out intRes) ? intRes : 0;
					reservoir.swCalcPseudo = !line.Substring(16, 8).Contains("0");
					reservoir.METFilenum = Int32.TryParse(line.Substring(24, 8), out intRes) ? intRes : 0;
					reservoir.elevation = Double.TryParse(line.Substring(32, 8), out dblRes) ? dblRes : 0;
					reservoir.airRainChemFilenum = Int32.TryParse(line.Substring(40, 8), out intRes) ? intRes : 0;
					reservoir.coarseAirPartFilenum = Int32.TryParse(line.Substring(48, 8), out intRes) ? intRes : 0;
					reservoir.releaseFlowFilename = line.Substring(56);

					dnums = ReadDoubleData(sr, "STGFLO", 18);
					reservoir.spillway = new List<StageFlow>();
					for (int jj = 0; jj < 18; jj += 2) {
						StageFlow sw = new StageFlow();
						sw.stage = dnums[jj];
						sw.flow = dnums[jj + 1];
						reservoir.spillway.Add(sw);
					}

					dnums = ReadDoubleData(sr, "TOTSTA", 18);
					reservoir.bathymetry = new List<StageArea>();
					for (int jj = 0; jj < 18; jj += 2) {
						StageArea sa = new StageArea();
						sa.stage = dnums[jj];
						sa.area = dnums[jj + 1];
						reservoir.bathymetry.Add(sa);
					}

                    line = sr.ReadLine();
                    reservoir.swAdjustResRelease = !line.Substring(8, 8).Contains("0");
                    reservoir.hydrologyFilename = line.Substring(16);
                    //just uncommented following line..

                    //reservoir.swAdjustResRelease = !ReadString(sr, "OBSDATA").Contains("0");
					reservoir.waterReactionRate = ReadDoubleData(sr, "REAC-H2O", numReactions);
					reservoir.bedReactionRate = ReadDoubleData(sr, "REAC-BED", numReactions);
					reservoir.waterAdsorpIsotherm = ReadDoubleData(sr, "ADSISO", numComponents);
					reservoir.bedAdsorpIsotherm = ReadDoubleData(sr, "ADSISO", numComponents);

					reservoir.algae = new List <Algae > ();
					for (int jj=0; jj<3; jj++) {
						dnums = ReadDoubleData(sr, "ALGAE", 7);
						Algae alg  = new Algae();
						alg.nitroHalfSat = dnums[0];
						alg.phosHalfSat = dnums[1];
						alg.silicaHalfSat = dnums[2];
						alg.lightSat = dnums[3];
						alg.lowTempLimit = dnums[4];
						alg.highTempLimit = dnums[5];
						alg.optGrowTemp = dnums[6];
						reservoir.algae.Add(alg);
					}

					reservoir.numWaterQualParams = ReadInt(sr, "W2PARAM");

					reservoir.codesWQParams = ReadIntData(sr, "W2PARAM", reservoir.numWaterQualParams);

					reservoir.numDerived = ReadInt(sr, "W2DERIVE");
					reservoir.derived = ReadIntData(sr, "W2DERIVE", reservoir.numDerived);

					reservoir.waterQualControlFilename = ReadString(sr, "W2CNTRL");
					reservoir.masterMetFilename = ReadString(sr, "W2MET");
					reservoir.prescribedOutFlowFilename = ReadString(sr, "W2OUTFLO");
					reservoir.numCEQW2Files = ReadInt(sr, "W2FILES");
					if (reservoir.numCEQW2Files == 3) {
						reservoir.flowInputFilename = ReadString(sr, "W2FILES");
						reservoir.tempInputFilename = ReadString(sr, "W2FILES");
						reservoir.waterQualInputFilename = ReadString(sr, "W2FILES");
					}

					reservoir.reservoirSegs = new List<ReservoirSeg>();
					for (int jj=0; jj<reservoir.numSegments; jj++) {
						line = ReadSpacerLine(sr, "RESERVOIR SEGMENT");
						ReservoirSeg seg = new ReservoirSeg();
						line = sr.ReadLine();
						if (TestLine(line, sr.LineNum, "DEPTH")) {
							seg.idNum = Int32.TryParse(line.Substring(8, 8), out intRes) ? intRes : 0;
							seg.bottomElevation = Double.TryParse(line.Substring(16, 8), out dblRes) ? dblRes : 0;
							seg.swOutputResults = !line.Substring(24, 8).Contains("0");
							seg.numOutlets = Int32.TryParse(line.Substring(32, 8), out intRes) ? intRes : 0;
							seg.name = line.Substring(40);
						}
						seg.outlets = new List<ReservoirOutlet>();
						for (int kk=0; kk<seg.numOutlets+1; kk++) {
							line = sr.ReadLine();
							if (TestLine(line, sr.LineNum, "DEPTH")) {
								ReservoirOutlet outlet = new ReservoirOutlet();
								outlet.elevation = Double.TryParse(line.Substring(8, 8), out dblRes) ? dblRes : 0;
								outlet.width = Double.TryParse(line.Substring(16, 8), out dblRes) ? dblRes : 0;
								outlet.outletType = Int32.TryParse(line.Substring(24, 8), out intRes) ? intRes : 0;
								outlet.numFlowFile = Int32.TryParse(line.Substring(32, 8), out intRes) ? intRes : 0;
								outlet.managedFlowFilename = line.Substring(40);
								seg.outlets.Add(outlet);
							}
						}

						seg.numPointSources = ReadInt(sr, "PTSOURCE");
						if (seg.numPointSources > 0)
							seg.pointSources = ReadIntData(sr, "PTSOURCE", seg.numPointSources);
						
						dnums = ReadDoubleData(sr, "METFAC", 6);
						seg.precipWgtMult = dnums[0];
						seg.tempLapse = dnums[1];
						seg.windSpeedMult = dnums[2];
						seg.radiationFractionReachingDepth = dnums[3];
						seg.radiationFractionDepth = dnums[4];
						seg.SecchiDiskDepth = dnums[5];

						dnums = ReadDoubleData(sr, "GMIN", 9);
						seg.minNegDensity = dnums[0];
						seg.minDiffCoeff = dnums[1];
						seg.windMixA1Coef = dnums[2];
						seg.windMixA2Coef = dnums[3];
						seg.windMixMaxDiffCoef = dnums[4];
						seg.criticalDensityGradient = dnums[5];
						seg.densityGradMaxDiffCoef = dnums[6];
						seg.densityGradExp = dnums[7];
						seg.inflowEntrain = dnums[8];

						dnums = ReadDoubleData(sr, "SED", 3);	// need to figure out spec(2 or 3 nums) - MRL
						seg.sedBottomThickness = dnums[0];
						seg.sedDiffusion = dnums[1];
						//seg.sedOxygenDemand = dnums[2];	// only two pieces of data - MRL

						dnums = ReadDoubleData(sr, "STGAREA", 18);
						seg.bathymetry = new List<StageArea>();
						for (int kk = 0; kk < 18; kk += 2) {
							StageArea sa = new StageArea();
							sa.stage = dnums[kk];
							sa.area = dnums[kk + 1];
							seg.bathymetry.Add(sa);
						}

						seg.chemConcentrations = ReadDoubleData(sr, "LAKION", numComponents);
						seg.chemConBedSediment = ReadDoubleData(sr, "BEDION", numComponents);

						dnums = ReadDoubleData(sr, "DEPTHTMP", 18);
						seg.depthTemp = new List<DepthTemp>();
						for (int kk = 0; kk < 18; kk += 2) {
							DepthTemp dt = new DepthTemp();
							dt.depth = dnums[kk];
							dt.temp = dnums[kk + 1];
							seg.depthTemp.Add(dt);
						}

						seg.upstreamCatchIDs = ReadIntData(sr, "ICATOL", 9);
						seg.upstreamRiverIDs = ReadIntData(sr, "IRVTOL", 9);
						seg.upstreamReservoirIDs = ReadIntData(sr, "ILKTOL", 9);
                        
                        nums = ReadIntData(sr, "DIVTO", 9);
                        seg.numDiversionsTo = nums[0];
                        for (int i = 0; i < seg.numDiversionsTo; i++)
                        {
                            seg.diversionToFilenums[i] = nums[i + 1];
                        }
						seg.obsWQFilename = ReadString(sr, "OBSDATA");

                        //last (downstream-most) segment of a reservoir is always a subwatershed boundary
                        //parameter is not written to or read from COE
                        if (jj == reservoir.numSegments - 1)
                            seg.swIsSubwaterBoundary = true;
                        else
                            seg.swIsSubwaterBoundary = false;

                        reservoir.reservoirSegs.Add(seg);
					}
					reservoirs.Add(reservoir);

				}

				line = sr.ReadLine();
				while (line.Contains("CRITERIA")) line = sr.ReadLine();  // remove - MRL

                // criteria
                //This consists of two lines at the end of the COE file
                //The lines are read from / written to the coe file in the readCON() and writeCON() functions

                // parse the TMDL line
                if (TestLine(line, sr.LineNum, "TMDL")) {
					// get TMDL switch.  spec has 6 other numbers...  MRL
					isTMDLSimulation = line.Substring(8, 8).Contains("1") ? true : false;
				}

				sr.Close();
                return true;
            }
            catch (Exception e) {
                Debug.WriteLine("COE exception at line " + sr.LineNum + ": " + e.ToString());
                return false;
            }
        }

        #endregion

        #region Methods - Writing Coefficients

        // writes doubles from a list- 9 per line
        public void WriteDoubleData(STechStreamWriter sw, string lineAbbrev, List<double> dblValues)
        {
            double dblRes;
            int numValues, linesToWrite, i;

            numValues = dblValues.Count;
            dblRes = (double)numValues / 9;
            linesToWrite = Convert.ToInt16(Math.Ceiling(dblRes));

            i = 0;

            for (int j = 0; j < linesToWrite; j++)
            {
                if (j < (linesToWrite - 1)) //all full lines
                {
                    sw.WriteString(lineAbbrev);
                    for (int k = 0; k < 9; k++)
                    {
                        sw.WriteDouble(dblValues[i]);
                        i++;
                    }
                    sw.WriteLine();
                }
                else //last line
                {
                    sw.WriteString(lineAbbrev);
                    while (i < dblValues.Count)
                    {
                        sw.WriteDouble(dblValues[i]);
                        i++;
                    }
                    sw.WriteLine();
                    return;                    
                }
            }
        }

        // writes integers from a list- 9 per line
        public void WriteIntData(STechStreamWriter sw, string lineAbbrev, List<int> intValues)
        {
            double dblRes;
            int numValues, linesToWrite, i;

            numValues = intValues.Count;
            dblRes = (double)numValues / 9;
            linesToWrite = Convert.ToInt16(Math.Ceiling(dblRes));

            i = 0;

            for (int j = 0; j < linesToWrite; j++)
            {
                if (j < (linesToWrite - 1)) //all full lines
                {
                    sw.WriteString(lineAbbrev);
                    for (int k = 0; k < 9; k++)
                    {
                        sw.WriteInt(intValues[i]);
                        i++;
                    }
                    sw.WriteLine();
                }
                else //last line
                {
                    sw.WriteString(lineAbbrev);
                    while (i < intValues.Count)
                    {
                        sw.WriteInt(intValues[i]);
                        i++;
                    }
                    sw.WriteLine();
                    return;
                }
            }
        }

        // write out the coefficients file
        public bool WriteCOE(string filename)
        {
            Logger.Info("Writing coefficients file " + filename);
            STechStreamWriter sw = null;
            try
            {
                sw = new STechStreamWriter(filename);
                
                //version
                sw.WriteString("VERSION");
                sw.WriteInt(version);
                sw.WriteLine();
                
                //scenario description
                sw.WriteString(scenarioDescription);
                sw.WriteLine();
                
                //begin date
                sw.WriteString("BEGDATE");
                sw.WriteInt(begDate.Day);
                sw.WriteInt(begDate.Month);
                sw.WriteInt(begDate.Year);
                sw.WriteLine();

                //end date
                sw.WriteString("ENDDATE");
                sw.WriteInt(endDate.Day);
                sw.WriteInt(endDate.Month);
                sw.WriteInt(endDate.Year);
                sw.WriteLine();

                //spacer line
                sw.WriteLine("********  NCATCH    NSEG NRESSEG NRESERV  PERDAY  NLOOPS AUTOCAL LOADING********");

                // numbers of objects in model
                sw.WriteString("SYSTEM");
                sw.WriteInt(numCatchments);
                sw.WriteInt(numRivers);
                sw.WriteInt(numReservoirSegs);
                sw.WriteInt(numReservoirs);
                sw.WriteInt(numTimeStepsPerDay);
                sw.WriteInt(numSimLoops);
                sw.WriteInt(numAutoCalibrationLoops);
                sw.WriteOnOffas1or0(swCalculateLoading);
                sw.WriteLine();

                //simulation switches
                sw.WriteString("QUAL  =");
                sw.WriteOnOffSwitch(swWaterQualSim);
                sw.WriteLine();
                sw.WriteString("SEEPS =");
                sw.WriteOnOffSwitch(swLakeSeepageSim);
                sw.WriteLine();
                sw.WriteString("SEDMNT=");
                sw.WriteOnOffSwitch(swSedimentSim);
                sw.WriteLine();
                sw.WriteString("FERTLZ=");
                sw.WriteOnOffSwitch(swFertSim);
                sw.WriteLine();
                sw.WriteString("POINTS=");
                sw.WriteOnOffSwitch(swPointSrcSim);
                sw.WriteLine();
                sw.WriteString("CHECKS=");
                sw.WriteOnOffSwitch(swInputCoeffChecks);
                sw.WriteLine();

                //warm start
                sw.WriteString("WARMST");
                if (swStartupFile)
                {
                    sw.WriteInt(1);
                    sw.WriteString(startupFileName);
                }
                else sw.WriteInt(0);
                sw.WriteLine();

                //MET files
                sw.WriteString("METFILE");
                sw.WriteInt(numMETFiles);
                sw.WriteLine();
                for (int i = 0; i < numMETFiles; i++)
                {
                    sw.WriteString("METFILE");
                    sw.WriteString(METFilename[i]);
                    sw.WriteLine();
                }

                //diversion files (.FLO)
                sw.WriteString("DIVFILES");
                sw.WriteInt(numDIVFiles);
                sw.WriteLine();
                for (int i = 0; i < numDIVFiles; i++)
                {
                    sw.WriteString("DIVFILES");
                    sw.WriteDouble(DIVData[i].flowDivertedMultiplier);
                    sw.WriteDouble(DIVData[i].flowCap);
                    sw.WriteOnOffas1or0(DIVData[i].swUseMonthFlows);
                    sw.WriteInt(0); //Value not used - entered 0 as placeholder - SAS
                    sw.WriteString(DIVData[i].filename);
                    sw.WriteLine();
                    WriteDoubleData(sw, "DIVFILES", DIVData[i].monthlyFlow);
                }

                //PTS files
                sw.WriteString("PTSFILES");
                sw.WriteInt(numPTSFiles);
                sw.WriteLine();
                for (int i = 0; i < numPTSFiles; i++)
                {
                    sw.WriteString("PTSFILES");
                    sw.WriteString(PTSFilename[i]);
                    sw.WriteLine();
                }

                //AIR files
                sw.WriteString("AIRFILE");
                sw.WriteInt(numAIRFiles);
                sw.WriteLine();
                for (int i = 0; i < numAIRFiles; i++)
                {
                    sw.WriteString("AIRFILE");
                    sw.WriteString(AIRFilename[i]);
                    sw.WriteLine();
                }

                //run files
                sw.WriteString("FILES");
                sw.WriteString(catchmentOutFilename);
                sw.WriteLine();
                sw.WriteString("FILES");
                sw.WriteString(riverOutFilename);
                sw.WriteLine();
                sw.WriteString("FILES");
                sw.WriteString(reservoirOutFilename);
                sw.WriteLine();
                sw.WriteString("FILES");
                sw.WriteString(loadingOutFilename);
                sw.WriteLine();
                sw.WriteString("FILES");
                sw.WriteString(warmstartOutFilename);
                sw.WriteLine();
                sw.WriteString("FILES");
                sw.WriteString(textOutFilename);
                sw.WriteLine();

                //System coefficients
                sw.WriteLine("******** SYSTEM COEFFICIENTS ******");
                sw.WriteString("TOL");
                sw.WriteDouble(tolerancePH);
                sw.WriteDouble(0.0); //this value isn't currently used - SAS
                sw.WriteDouble(toleranceCation1);
                sw.WriteDouble(toleranceCation2);
                sw.WriteLine();

                sw.WriteString("TAREA");
                sw.WriteDouble(watershedArea);
                sw.WriteDouble(watershedElevation);
                sw.WriteLine();

                sw.WriteString("EVPMAX");
                sw.WriteDouble(latitude);
                sw.WriteDouble(longitude);
                sw.WriteDouble(evaporationScaling);
                sw.WriteDouble(evaporationSeasonSkew);
                sw.WriteDouble(atmosphericTurbidity);
                sw.WriteDouble(0.0); //another place holder for un-used data
                sw.WriteLine();

                sw.WriteString("RKCONV");
                sw.WriteDouble(soilThermalConduct);
                sw.WriteLine();

                sw.WriteString("RINPUT");
                sw.WriteInt(rainBalancingIons);
                sw.WriteInt(airBalancingIons);
                sw.WriteLine();

                //hydrology constituents
                sw.WriteString("CONSTITS");
                sw.WriteInt(numHydrologyParams);
                sw.WriteLine();

                for (int i = 0; i < numHydrologyParams; i++)
                {
                    sw.WriteString("CONSTIT");
                    sw.WriteString(hydroConstits[i].fortranCode);
                    sw.WriteOnOffas1or0(hydroConstits[i].swIncludeInOutput);
                    sw.WriteString16(hydroConstits[i].units);
                    sw.WriteString16(hydroConstits[i].abbrevName);
                    sw.WriteString(hydroConstits[i].fullName);
                    sw.WriteLine();

                    sw.WriteString("CONSTIT");
                    sw.WriteOnOffas1or0(hydroConstits[i].swCatchmentInclude);
                    sw.WriteOnOffas1or0(hydroConstits[i].swRiverInclude);
                    sw.WriteOnOffas1or0(hydroConstits[i].swReservoirInclude);
                    sw.WriteOnOffas1or0(hydroConstits[i].swLoadingInclude);
                    sw.WriteLine();
                }

                //chemical constituents
                sw.WriteString("CONSTITS");
                sw.WriteInt(numChemicalParams);
                sw.WriteLine();

                for (int i = 0; i < numChemicalParams; i++)
                {
                    sw.WriteString("CHEMICAL");
                    sw.WriteString(chemConstits[i].fortranCode);
                    sw.WriteOnOffas1or0(chemConstits[i].swIncludeInOutput);
                    sw.WriteString16(chemConstits[i].units);
                    sw.WriteString16(chemConstits[i].abbrevName);
                    sw.WriteString(chemConstits[i].fullName);
                    sw.WriteLine();

                    sw.WriteString("CHEMICAL");
                    sw.WriteOnOffas1or0(chemConstits[i].swCatchmentInclude);
                    sw.WriteOnOffas1or0(chemConstits[i].swRiverInclude);
                    sw.WriteOnOffas1or0(chemConstits[i].swReservoirInclude);
                    sw.WriteOnOffas1or0(chemConstits[i].swLoadingInclude);
                    sw.WriteLine();

                    sw.WriteString("CHEMICAL");
                    sw.WriteDouble(chemConstits[i].electricalCharge);
                    sw.WriteDouble(chemConstits[i].massEquivalent);
                    sw.WriteDouble(chemConstits[i].loadingUnitConversion);
                    sw.WriteString(chemConstits[i].loadingUnits);
                    sw.WriteLine();

                    sw.WriteString("CHEMICAL");
                    sw.WriteDouble(chemConstits[i].airRainMult);
                    sw.WriteDouble(chemConstits[i].pointSourceMult);
                    sw.WriteDouble(chemConstits[i].nonpointSourceMult);
                    sw.WriteDouble(chemConstits[i].solubWithSulfate);
                    sw.WriteDouble(chemConstits[i].stoichChemWithSulfate);
                    sw.WriteDouble(chemConstits[i].stoichSulfateWithChem);
                    sw.WriteDouble(chemConstits[i].solubWithHydrox);
                    sw.WriteDouble(chemConstits[i].stoichChemWithHydrox);
                    sw.WriteDouble(chemConstits[i].stoichHydroxWithChem);
                    sw.WriteLine();

                    sw.WriteString("CHEMICAL");
                    sw.WriteOnOffas1or0(chemConstits[i].swLoadingTMDL);
                    sw.WriteInt(chemConstits[i].dryDepositionForm);
                    sw.WriteOnOffas1or0(chemConstits[i].swChemAdvection);
                    sw.WriteLine();

                    WriteDoubleData(sw, "CHEMICAL", chemConstits[i].gasDepositVelocity);
                }

                //physical constituents
                sw.WriteString("CONSTITS");
                sw.WriteInt(numPhysicalParams);
                sw.WriteLine();

                for (int i = 0; i < numPhysicalParams; i++)
                {
                    sw.WriteString("PHYSICAL");
                    sw.WriteString(physicalConstits[i].fortranCode);
                    sw.WriteOnOffas1or0(physicalConstits[i].swIncludeInOutput);
                    sw.WriteString16(physicalConstits[i].units);
                    sw.WriteString16(physicalConstits[i].abbrevName);
                    sw.WriteString(physicalConstits[i].fullName);
                    sw.WriteLine();

                    sw.WriteString("PHYSICAL");
                    sw.WriteOnOffas1or0(physicalConstits[i].swCatchmentInclude);
                    sw.WriteOnOffas1or0(physicalConstits[i].swRiverInclude);
                    sw.WriteOnOffas1or0(physicalConstits[i].swReservoirInclude);
                    sw.WriteOnOffas1or0(physicalConstits[i].swLoadingInclude);
                    sw.WriteLine();

                    sw.WriteString("PHYSICAL");
                    sw.WriteDouble(physicalConstits[i].electricalCharge);
                    sw.WriteDouble(physicalConstits[i].massEquivalent);
                    sw.WriteDouble(physicalConstits[i].loadingUnitConversion);
                    sw.WriteString(physicalConstits[i].loadingUnits);
                    sw.WriteLine();

                    sw.WriteString("PHYSICAL");
                    sw.WriteDouble(physicalConstits[i].airRainMult);
                    sw.WriteDouble(physicalConstits[i].pointSourceMult);
                    sw.WriteDouble(physicalConstits[i].nonpointSourceMult);
                    //There are six unused spots here - SAS
                    sw.WriteLine();

                    sw.WriteString("PHYSICAL");
                    sw.WriteOnOffas1or0(physicalConstits[i].swLoadingTMDL);
                    sw.WriteInt(physicalConstits[i].dryDepositionForm);
                    sw.WriteOnOffas1or0(physicalConstits[i].swChemAdvection);
                    sw.WriteLine();

                    WriteDoubleData(sw, "PHYSICAL", physicalConstits[i].gasDepositVelocity);
                }

                //composite constituents
                sw.WriteString("CONSTITS");
                sw.WriteInt(numCompositeParams);
                sw.WriteLine();

                for (int i = 0; i < numCompositeParams; i++)
                {
                    sw.WriteString("COMPOSIT");
                    sw.WriteString(compositeConstits[i].fortranCode);
                    sw.WriteOnOffas1or0(compositeConstits[i].swIncludeInOutput);
                    sw.WriteString16(compositeConstits[i].units);
                    sw.WriteString16(compositeConstits[i].abbrevName);
                    sw.WriteString(compositeConstits[i].fullName);
                    sw.WriteLine();

                    sw.WriteString("COMPOSIT");
                    sw.WriteOnOffas1or0(compositeConstits[i].swCatchmentInclude);
                    sw.WriteOnOffas1or0(compositeConstits[i].swRiverInclude);
                    sw.WriteOnOffas1or0(compositeConstits[i].swReservoirInclude);
                    sw.WriteOnOffas1or0(compositeConstits[i].swLoadingInclude);
                    sw.WriteLine();

                    sw.WriteString("COMPOSIT");
                    sw.WriteDouble(compositeConstits[i].electricalCharge);
                    sw.WriteDouble(compositeConstits[i].massEquivalent);
                    sw.WriteDouble(compositeConstits[i].loadingUnitConversion);
                    sw.WriteString(compositeConstits[i].loadingUnits);
                    sw.WriteLine();

                    sw.WriteString("COMPOSIT");
                    sw.WriteOnOffas1or0(compositeConstits[i].swIncludeDissolvedConstits);
                    sw.WriteOnOffas1or0(compositeConstits[i].swIncludeAdsorbedConstits);
                    sw.WriteOnOffas1or0(compositeConstits[i].swIncludePrecipConstits);
                    sw.WriteLine();

                    WriteDoubleData(sw, "COMPONEN", compositeConstits[i].componentTotalMass);
                }

                // Reactions
                sw.WriteString("REACTION");
                sw.WriteInt(numReactions);
                sw.WriteLine();

                for (int i = 0; i < numReactions; i++)
                {
                    sw.WriteString("REACTION");
                    sw.WriteInt(reactions[i].primReactantNumber);
                    sw.WriteOnOffas1or0(reactions[i].swIsAnoxic);
                    sw.WriteDouble(reactions[i].dissolvedOxyLimit);
                    sw.WriteOnOffas1or0(reactions[i].swIsUVCatalysis);
                    sw.WriteInt(reactions[i].numLinkedReactions);
                    sw.WriteDouble(reactions[i].tempCorrectCoeff);
                    sw.WriteString(reactions[i].fortranCode);
                    sw.WriteString(reactions[i].units);
                    sw.WriteString(reactions[i].name);
                    sw.WriteLine();

                    WriteDoubleData(sw, "STOICH", reactions[i].stoich);
                }

                //Sediments
                sw.WriteLine("******** SEDIMENT ********");
                sw.WriteString("NPART");
                sw.WriteInt(numSedParticleSizes);
                sw.WriteInt(numSedWashLoadParticles);
                sw.WriteLine();

                for (int i = 0; i < numSedParticleSizes; i++)
                {
                    sw.WriteString("SEDIMEN" + Convert.ToString(i + 1));
                    sw.WriteDouble(sediments[i].grainSize);
                    sw.WriteDouble(sediments[i].specGravity);
                    sw.WriteDouble(sediments[i].settlingRate);
                    sw.WriteLine();
                }

                //Algae and Periphyton
                sw.WriteLine("******** ALGAE & PERIPHYTON COEFFICIENTS ********");
                for (int i = 0; i < numAlgae; i++)
                {
                    sw.WriteString("WQCOEF" + Convert.ToString(i + 1));
                    sw.WriteDouble(algaes[i].nitroHalfSat);
                    sw.WriteDouble(algaes[i].phosHalfSat);
                    sw.WriteDouble(algaes[i].silicaHalfSat);
                    sw.WriteDouble(algaes[i].lightSat);
                    sw.WriteDouble(algaes[i].lowTempLimit);
                    sw.WriteDouble(algaes[i].highTempLimit);
                    sw.WriteDouble(algaes[i].optGrowTemp);
                    sw.WriteLine();
                }

                sw.WriteString("PERIPH");
                sw.WriteDouble(peri.recycledFraction);
                sw.WriteDouble(peri.velocityHalfSat);
                sw.WriteDouble(peri.nitroHalfSat);
                sw.WriteDouble(peri.phosHalfSat);
                sw.WriteDouble(peri.lightSat);
                sw.WriteLine();

                sw.WriteString("PERIPH");
                sw.WriteDouble(peri.scourRegressionCoef);
                sw.WriteDouble(peri.scourRegressionExp);
                sw.WriteDouble(peri.ammoniaPref);
                sw.WriteDouble(peri.spatialLimitHalfSat);
                sw.WriteDouble(peri.spatialLimitIntercept);
                sw.WriteDouble(peri.endoRespirationCoef);
                sw.WriteDouble(peri.endoRespirationExp);
                sw.WriteDouble(peri.photoRespirationFraction);
                sw.WriteLine();

                sw.WriteString("SHADING");
                sw.WriteDouble(sedimentShading);
                sw.WriteDouble(algaeShading);
                sw.WriteDouble(detritusShading);
                sw.WriteLine();

                //Minerals
                sw.WriteLine("******** MINERALS ********");
                sw.WriteString("NMNRLS");
                sw.WriteInt(numMinerals);
                sw.WriteLine();
                for (int i = 0; i < numMinerals; i++)
                {
                    sw.WriteString("MINERL" + Convert.ToString(i + 1));
                    sw.WriteString16(minerals[i].name);
                    sw.WriteDouble(minerals[i].molecularWgt);
                    sw.WriteDouble(minerals[i].phDepend);
                    sw.WriteDouble(minerals[i].weatheringRate);
                    sw.WriteDouble(minerals[i].oxyConsumption);
                    sw.WriteLine();

                    WriteDoubleData(sw, "MNWTH" + Convert.ToString(i + 1), minerals[i].chemReactionProduct);
                }

                //Litter and humus
                sw.WriteLine("******** LITTER & HUMUS REACTIONS ********");
                sw.WriteString("FRLCH");
                sw.WriteDouble(litter.coarseLitterFrac);
                sw.WriteDouble(litter.fineLitterFrac);
                sw.WriteDouble(litter.humusFrac);
                sw.WriteDouble(litter.nonStructLeach);
                sw.WriteLine();

                sw.WriteString("RATES");
                sw.WriteDouble(litter.coarseLitterDecay);
                sw.WriteDouble(litter.fineLitterDecay);
                sw.WriteDouble(litter.humusDecay);
                sw.WriteLine();

                //Snow and ice
                sw.WriteLine("******** SNOW AND ICE COEFFICIENTS ********");
                sw.WriteString("SNOW");
                sw.WriteDouble(snow.formTemp);
                sw.WriteDouble(snow.openAreaSubRate);
                sw.WriteDouble(snow.forestAreaSubRate);
                sw.WriteDouble(snow.initialDepth);
                sw.WriteDouble(snow.openAreaMeltRate);
                sw.WriteDouble(snow.forestAreaMeltRate);
                sw.WriteDouble(snow.fieldCapacity);
                sw.WriteDouble(snow.meltTemp);
                sw.WriteDouble(snow.rainMeltRate);
                sw.WriteLine();

                sw.WriteString("SNOWLCH");
                sw.WriteDouble(snow.thermalConduct);
                sw.WriteDouble(snow.iceThermalConduct);
                sw.WriteDouble(snow.meltLeaching);
                sw.WriteDouble(snow.nitrificationRate);
                sw.WriteDouble(0); //There is an unused spot here - SAS
                sw.WriteLine();

                //Septic
                sw.WriteLine("******** SEPTIC ********");
                sw.WriteString("SEPTIC");
                sw.WriteDouble(septic.failedFlow);
                sw.WriteLine();

                WriteDoubleData(sw, "SEPTIC", septic.type1);
                WriteDoubleData(sw, "SEPTIC", septic.type2);
                WriteDoubleData(sw, "SEPTIC", septic.type3);

                //Land use data
                sw.WriteLine("******** CANOPY AND LAND USE ********");
                
                //General landuse parameters
                WriteDoubleData(sw, "PARTDV", partDV);
                WriteDoubleData(sw, "COARSEDV", coarseDV);

                sw.WriteString("IVDGAS");
                sw.WriteOnOffSwitch(swGasDepositVelocity);
                sw.WriteLine();

                if (swGasDepositVelocity==true)
                {
                    WriteDoubleData(sw, "NOXSOXVD", gasDepositVelocity);
                }
                WriteDoubleData(sw, "NOXSOXVU", gasUptakeVelocity);

                sw.WriteString("HEIGHT");
                sw.WriteDouble(heightWindSpeed);
                sw.WriteDouble(vonkar);
                sw.WriteLine();

                sw.WriteString("NITRIFYR");
                sw.WriteInt(numLanduses);
                sw.WriteDouble(standingBiomass);
                sw.WriteLine();

                //land use individual data
                for (int i = 0; i < numLanduses; i++)
                {
                    sw.WriteLine("******** LAND USE TYPE ********");

                    sw.WriteString("INTCEPT");
                    sw.WriteDouble(landuse[i].openWinterFrac);
                    sw.WriteDouble(landuse[i].imperviousFrac);
                    sw.WriteDouble(landuse[i].maxPotentInceptStorage);
                    sw.WriteString(landuse[i].name);
                    sw.WriteLine();

                    sw.WriteString("EROSION");
                    sw.WriteDouble(landuse[i].rainDetachFactor);
                    sw.WriteDouble(landuse[i].flowDetachFactor);
                    sw.WriteLine();

                    sw.WriteString("GROWTH");
                    sw.WriteDouble(landuse[i].annualGrowMult);
                    sw.WriteDouble(landuse[i].leafGrowFactor);
                    sw.WriteDouble(landuse[i].productivity);
                    sw.WriteDouble(landuse[i].maintRespRate);
                    sw.WriteDouble(landuse[i].activeRespRate);
                    sw.WriteDouble(landuse[i].dryCollEff);
                    sw.WriteDouble(landuse[i].wetCollEff);
                    sw.WriteDouble(landuse[i].leafWgtArea);
                    sw.WriteLine();

                    sw.WriteString("HEIGHT");
                    sw.WriteDouble(landuse[i].canopyHeight);
                    sw.WriteDouble(landuse[i].stomatalResist);
                    sw.WriteLine();

                    WriteDoubleData(sw, "CROPPING", landuse[i].cropping);
                    WriteDoubleData(sw, "LAID", landuse[i].leafAreaIdx);
                    WriteDoubleData(sw, "UDISTD", landuse[i].annualUptake);
                    WriteDoubleData(sw, "LFD", landuse[i].litterFallRate);
                    WriteDoubleData(sw, "BETAD", landuse[i].exudationRate);
                    WriteDoubleData(sw, "LFCMPD1", landuse[i].leafComp);
                    WriteDoubleData(sw, "TRCMPD", landuse[i].trunkComp);

                    sw.WriteString("FERTLZ=");
                    sw.WriteInt(landuse[i].numFertPlans);
                    sw.WriteLine();
                    for (int j = 0; j < landuse[i].numFertPlans; j++)
                    {
                        for (int k = 0; k < 12; k++)
                        {
                            WriteDoubleData(sw, "FERTLZ=", landuse[i].fertPlanApplication[j][k]);
                        }
                    }
                }

                //Catchments
                sw.WriteLine("******** CATCHMENT COEFFICIENTS ********");
                for (int i = 0; i < numCatchments; i++)
                {
                    sw.WriteLine("++++++++ CATCHMENT " + catchments[i].idNum.ToString() + " ++++++++");
                    sw.WriteString("CATC" + catchments[i].idNum.ToString());
                    sw.WriteInt(catchments[i].idNum);
                    sw.WriteInt(catchments[i].METFileNum);
                    sw.WriteDouble(catchments[i].precipMultiplier);
                    sw.WriteDouble(catchments[i].aveTempLapse);
                    sw.WriteDouble(catchments[i].altitudeTempLapse);
                    sw.WriteOnOffas1or0(catchments[i].swOutputResults);
                    sw.WriteInt(catchments[i].airRainChemFileNum);
                    sw.WriteInt(catchments[i].particleRainChemFileNum);
                    sw.WriteString(catchments[i].name);
                    sw.WriteLine();

                    sw.WriteString("CATC" + catchments[i].idNum.ToString());
                    sw.WriteInt(catchments[i].numSoilLayers);
                    sw.WriteDouble(catchments[i].slope);
                    sw.WriteDouble(catchments[i].width);
                    sw.WriteDouble(catchments[i].aspect);
                    sw.WriteDouble(catchments[i].ManningN);
                    sw.WriteDouble(catchments[i].detentionStorage);
                    sw.WriteLine();

                    //Upstream catchments
                    WriteIntData(sw, "ICAT" + catchments[i].idNum.ToString(), catchments[i].upstreamCatchIDs);

                    //Land use percentages
                    WriteDoubleData(sw, "CATC" + catchments[i].idNum.ToString(), catchments[i].landUsePercent);

                    //fertilization plan for each land use
                    WriteIntData(sw, "CATC" + catchments[i].idNum.ToString(), catchments[i].fertPlanNum);

                    //land application
                    WriteDoubleData(sw, "STOC" + catchments[i].idNum.ToString(), catchments[i].landApplicationLoad);

                    //number of irrigation sources
                    WriteIntData(sw, "IRRI" + catchments[i].idNum.ToString(), catchments[i].numIrrigationSources);

                    // for each land use, write number of irrigation sources and fraction of area
                    // this code needs to be expanded using a coe that has irrigation sources - SAS 11/13/19
                    for (int j = 0; j < numLanduses; j++)
                    {
                        if (catchments[i].numIrrigationSources[j] > 0)
                        {
                            sw.WriteString("IRRLAN" + j.ToString());
                            for (int k = 0; k < catchments[i].numIrrigationSources[j]; k++)
                            {
                                //sw.WriteInt(catchments[i].irrigationSource[k]);
                                //sw.WriteDouble(catchments[i].irrigationSourcePercent[k]);
                            }
                        }
                    }

                    //Land use ponding - used for AMD applications, so currently no test COE - SAS 11/13/19
                    sw.WriteString("NLUPONDS");
                    sw.WriteInt(catchments[i].nluPonds);
                    sw.WriteLine();
                    if (catchments[i].nluPonds > 0)
                    {
                        for (int j = 0; j < catchments[i].nluPonds; j++)
                        {
                            sw.WriteString("PONDFILE");
                            sw.WriteString(catchments[i].pondFilename[j]);
                            sw.WriteLine();
                        }
                    }

                    // point sources
                    sw.WriteString("PTSOURCE");
                    sw.WriteInt(catchments[i].numPointSources);
                    sw.WriteLine();
                    if (catchments[i].numPointSources > 0)
                    {
                        WriteIntData(sw, "PTSOURCE", catchments[i].pointSources);
                    }

                    // pumping
                    sw.WriteString("PUMPFROM");
                    sw.WriteInt(catchments[i].numPumpFromSchedules);
                    sw.WriteLine();
                    if (catchments[i].numPumpFromSchedules > 0)
                        WriteIntData(sw, "PUMPFROM", catchments[i].pumpFromDivFile);
                    sw.WriteString("PUMPTO");
                    sw.WriteInt(catchments[i].numPumpToSchedules);
                    sw.WriteLine();
                    if (catchments[i].numPumpToSchedules > 0)
                        WriteIntData(sw, "PUMPTO", catchments[i].pumpToDivFile);

                    // septic
                    sw.WriteString("SEPTIC");
                    sw.WriteDouble(catchments[i].septic.soilLayer);
                    sw.WriteDouble(catchments[i].septic.population);
                    sw.WriteDouble(catchments[i].septic.standardPct);
                    sw.WriteDouble(catchments[i].septic.advancedPct);
                    sw.WriteDouble(catchments[i].septic.failingPct);
                    sw.WriteDouble(catchments[i].septic.initialBiomass);
                    sw.WriteDouble(catchments[i].septic.thickness);
                    sw.WriteDouble(catchments[i].septic.area);
                    sw.WriteDouble(catchments[i].septic.biomassRespRate);
                    sw.WriteLine();
                    sw.WriteString("SEPTIC");
                    sw.WriteDouble(catchments[i].septic.biomassMortRate);
                    sw.WriteLine();

                    // sediment
                    sw.WriteString("SEDIMENT");
                    sw.WriteDouble(catchments[i].sediment.erosivity);
                    sw.WriteDouble(catchments[i].sediment.firstPartSizePct);
                    sw.WriteDouble(catchments[i].sediment.secondPartSizePct);
                    sw.WriteDouble(catchments[i].sediment.thirdPartSizePct);
                    sw.WriteLine();

                    // Best managment practices
                    sw.WriteString("BMP");
                    sw.WriteDouble(catchments[i].bmp.streetSweepFreq);
                    sw.WriteDouble(catchments[i].bmp.streetSweepEff);
                    sw.WriteDouble(catchments[i].bmp.divertedImpervFlow);
                    sw.WriteDouble(catchments[i].bmp.detentionPondVol);
                    sw.WriteDouble(catchments[i].bmp.maxFertAccumTime);
                    sw.WriteLine();

                    // stream bank buffers
                    sw.WriteString("BUFFZONE");
                    sw.WriteDouble(catchments[i].bufferingPct);
                    sw.WriteDouble(catchments[i].bufferZoneWidth);
                    sw.WriteDouble(catchments[i].bufferZoneSlope);
                    sw.WriteDouble(catchments[i].bufferManningN);
                    sw.WriteLine();

                    // seeps
                    sw.WriteString("SEEPS");
                    sw.WriteDouble(catchments[i].soilLayerSeepage);
                    sw.WriteDouble(catchments[i].overlandFlowSeepage);
                    sw.WriteLine();

                    // mining
                    sw.WriteString("MINING");
                    sw.WriteOnOffas1or0(catchments[i].mining.swIsLowSoilDeepMineOverburden);
                    sw.WriteInt(catchments[i].mining.surfaceMineLanduseNum);
                    sw.WriteDouble(catchments[i].mining.depthSpoils);
                    sw.WriteDouble(catchments[i].mining.soilMoisture);
                    sw.WriteDouble(catchments[i].mining.fieldCapacity);
                    sw.WriteDouble(catchments[i].mining.saturationMoisture);
                    sw.WriteDouble(catchments[i].mining.horizHydraulicConduct);
                    sw.WriteDouble(catchments[i].mining.vertHydraulicConduct);
                    //sw.WriteDouble(catchments[i].mining.ferroIonOxyidationRate);
                    sw.WriteLine();

                    // deep mines
                    sw.WriteString("MINEPERM");
                    sw.WriteInt(catchments[i].mining.numDeepMineDischarges);
                    sw.WriteOnOffas1or0(catchments[i].mining.swAreDeepConcentrationsMax);
                    sw.WriteLine();
                    for (int j = 0; j < catchments[i].mining.numDeepMineDischarges; j++)
                    {
                        sw.WriteString("MINEPERM");
                        sw.WriteDouble(catchments[i].mining.deepMines[j].areaFraction);
                        sw.WriteString(catchments[i].mining.deepMines[j].name);
                        sw.WriteLine();

                        WriteDoubleData(sw, "MINECONC", catchments[i].mining.deepMines[j].concentrationLimit);

                        sw.WriteString("MINEOUT");
                        sw.WriteString(catchments[i].mining.deepMines[j].mineOutFilename);
                        sw.WriteLine();
                    }

                    // surface mines
                    sw.WriteString("MINEPERM");
                    sw.WriteInt(catchments[i].mining.numSurfaceMineDischarges);
                    sw.WriteOnOffas1or0(catchments[i].mining.swAreSurfaceConcentrationsMax);
                    sw.WriteLine();
                    for (int j = 0; j < catchments[i].mining.numSurfaceMineDischarges; j++)
                    {
                        sw.WriteString("MINEPERM");
                        sw.WriteDouble(catchments[i].mining.surfaceMines[j].areaFraction);
                        sw.WriteString(catchments[i].mining.surfaceMines[j].name);
                        sw.WriteLine();

                        WriteDoubleData(sw, "MINECONC", catchments[i].mining.surfaceMines[j].concentrationLimit);

                        sw.WriteString("MINEOUT");
                        sw.WriteString(catchments[i].mining.surfaceMines[j].mineOutFilename);
                        sw.WriteLine();
                    }

                    // litter weights
                    sw.WriteString("XLIT" + catchments[i].idNum.ToString());
                    sw.WriteDouble(catchments[i].mining.coarseLitterWgtFraction);
                    sw.WriteDouble(catchments[i].mining.fineLitterWgtFraction);
                    sw.WriteDouble(catchments[i].mining.humusWgtFraction);
                    sw.WriteLine();

                    //reaction rates
                    WriteDoubleData(sw, "REACSOIL", catchments[i].reactions.soilReactionRate);
                    WriteDoubleData(sw, "REACSURF", catchments[i].reactions.surfaceReactionRate);
                    WriteDoubleData(sw, "REACCNPY", catchments[i].reactions.canopyReactionRate);
                    WriteDoubleData(sw, "REACBIOZ", catchments[i].reactions.biozoneReactionRate);

                    //CE-QUAL-W2
                    sw.WriteString("W2FILES");
                    sw.WriteInt(catchments[i].mining.numCEQW2Files);
                    sw.WriteLine();
                    if (catchments[i].mining.numCEQW2Files == 3)
                    {
                        sw.WriteString("W2FILES");
                        sw.WriteString(catchments[i].mining.flowInputFilename);
                        sw.WriteLine();
                        sw.WriteString("W2FILES");
                        sw.WriteString(catchments[i].mining.tempInputFilename);
                        sw.WriteLine();
                        sw.WriteString("W2FILES");
                        sw.WriteString(catchments[i].mining.waterQualInputFilename);
                        sw.WriteLine();
                    }

                    // soil layers
                    for (int j = 0; j < catchments[i].numSoilLayers; j++)
                    {
                        sw.WriteString("LAYER" + (j + 1).ToString());
                        sw.WriteDouble(catchments[i].soils[j].area);
                        sw.WriteDouble(catchments[i].soils[j].thickness);
                        sw.WriteDouble(catchments[i].soils[j].moisture);
                        sw.WriteDouble(catchments[i].soils[j].fieldCapacity);
                        sw.WriteDouble(catchments[i].soils[j].saturationMoisture);
                        sw.WriteDouble(catchments[i].soils[j].horizHydraulicConduct);
                        sw.WriteDouble(catchments[i].soils[j].vertHydraulicConduct);
                        sw.WriteDouble(catchments[i].soils[j].evapTranspireFract);
                        sw.WriteDouble(catchments[i].soils[j].waterTemp);
                        sw.WriteLine();

                        sw.WriteString("CKCAMG" + (j + 1).ToString());
                        sw.WriteDouble(catchments[i].soils[j].magnesiumXCoeff);
                        sw.WriteDouble(catchments[i].soils[j].sodiumXCoeff);
                        sw.WriteDouble(catchments[i].soils[j].potassiumXCoeff);
                        sw.WriteDouble(catchments[i].soils[j].ammoniaXCoeff);
                        sw.WriteDouble(catchments[i].soils[j].hydrogenXCoeff);
                        sw.WriteLine();

                        sw.WriteString("COMP" + (j + 1).ToString());
                        sw.WriteDouble(catchments[i].soils[j].exchangeCapacity);
                        sw.WriteDouble(catchments[i].soils[j].maxPhosAdsorption);
                        sw.WriteDouble(catchments[i].soils[j].density);
                        sw.WriteDouble(catchments[i].soils[j].tortuosity);
                        sw.WriteInt(catchments[i].soils[j].CO2CalcMethod);
                        sw.WriteDouble(catchments[i].soils[j].CO2ConcenFactor);
                        sw.WriteLine();

                        WriteDoubleData(sw, "COMP" + (j + 1).ToString(), catchments[i].soils[j].weightFract);
                        WriteDoubleData(sw, "SOL" + (j + 1).ToString(), catchments[i].soils[j].solutionConcen);
                        WriteDoubleData(sw, "ADS" + (j + 1).ToString(), catchments[i].soils[j].adsorptionIsotherm);
                    }
                }

                //Rivers
                sw.WriteLine("******** RIVER COEFFICIENTS ********");
                for (int i = 0; i < numRivers; i++)
                {
                    sw.WriteLine("++++++++ RIVER " + rivers[i].idNum.ToString() + " ++++++++");

                    sw.WriteString("STRE" + rivers[i].idNum.ToString());
                    sw.WriteInt(rivers[i].idNum);
                    sw.WriteDouble(rivers[i].depth);
                    sw.WriteDouble(rivers[i].length);
                    sw.WriteDouble(rivers[i].upElevation);
                    sw.WriteDouble(rivers[i].downElevation);
                    sw.WriteDouble(rivers[i].ManningN);
                    sw.WriteDouble(rivers[i].temp);
                    sw.WriteDouble(rivers[i].convectiveHeatFactor);
                    sw.WriteDouble(rivers[i].minFlow);
                    sw.WriteLine();

                    sw.WriteString("ELEV" + rivers[i].idNum.ToString());
                    sw.WriteOnOffas1or0(rivers[i].swOutputResults);
                    sw.WriteOnOffas1or0(rivers[i].swIsSubwaterBoundary);
                    sw.WriteOnOffas1or0(rivers[i].swViewManagerOutput);
                    sw.WriteString(rivers[i].name);
                    sw.WriteLine();

                    sw.WriteString("IMPO" + rivers[i].idNum.ToString());
                    sw.WriteDouble(rivers[i].impoundArea);
                    sw.WriteDouble(rivers[i].impoundVol);
                    sw.WriteDouble(rivers[i].freezingTemp);
                    sw.WriteDouble(rivers[i].meltingTemp);
                    sw.WriteDouble(rivers[i].iceCalcAve);
                    sw.WriteLine();

                    sw.WriteString("STAR" + rivers[i].idNum.ToString());
                    int k = 0;
                    for (int j = 0; j < 9; j++)
                    {
                        sw.WriteDouble(rivers[i].stageWidthCurve[j].stage);
                        k++;
                        if (k == 9)
                        {
                            sw.WriteLine();
                            sw.WriteString("STAR" + rivers[i].idNum.ToString());
                        }
                        sw.WriteDouble(rivers[i].stageWidthCurve[j].width);
                        k++;
                        if (k == 9)
                        {
                            sw.WriteLine();
                            sw.WriteString("STAR" + rivers[i].idNum.ToString());
                        }
                    }
                    sw.WriteLine();

                    //upstream catchments (9 max)
                    WriteIntData(sw, "ICAT" + rivers[i].idNum.ToString(), rivers[i].upstreamCatchIDs);

                    //upstream rivers (9 max)
                    WriteIntData(sw, "IRVT" + rivers[i].idNum.ToString(), rivers[i].upstreamRiverIDs);

                    //upstream reservoirs (9 max)
                    WriteIntData(sw, "ILKT" + rivers[i].idNum.ToString(), rivers[i].upstreamReservoirIDs);

                    sw.WriteString("DIVFROM");
                    sw.WriteInt(rivers[i].numDiversionsFrom);
                    sw.WriteLine();
                    if (rivers[i].numDiversionsFrom > 0)
                    {
                        WriteIntData(sw, "DIVFROM", rivers[i].divFilenumFrom);
                    }

                    sw.WriteString("DIVTO");
                    sw.WriteInt(rivers[i].numDiversionsTo);
                    sw.WriteLine();
                    if (rivers[i].numDiversionsTo > 0)
                    {
                        WriteIntData(sw, "DIVTO", rivers[i].diversionToFilenums);
                    }

                    sw.WriteString("PTSOURCE");
                    sw.WriteInt(rivers[i].numPointSources);
                    sw.WriteLine();
                    if (rivers[i].numPointSources > 0)
                    {
                        WriteIntData(sw, "PTSOURCE", rivers[i].pointSources);
                    }

                    sw.WriteString("OBSW" + rivers[i].idNum.ToString());
                    sw.WriteOnOffas1or0(rivers[i].overrideSimulation.swUseObsData);
                    sw.WriteInt(rivers[i].overrideSimulation.hydroInterpPeriod);
                    sw.WriteInt(rivers[i].overrideSimulation.waterQualityInterpPeriod);
                    sw.WriteInt(rivers[i].overrideSimulation.monthAverageMethod);
                    sw.WriteInt(rivers[i].overrideSimulation.tdsAdjustmentPriority);
                    sw.WriteInt(rivers[i].overrideSimulation.alkAdjustmentPriority);
                    sw.WriteInt(rivers[i].overrideSimulation.phAdjustmentPriority);
                    sw.WriteLine();

                    sw.WriteString("OBSD" + rivers[i].idNum.ToString());
                    sw.WriteString(rivers[i].hydrologyFilename);
                    sw.WriteLine();

                    sw.WriteString("SEDIMENT");
                    sw.WriteDouble(rivers[i].sedDetachVelMult);
                    sw.WriteDouble(rivers[i].sedDetachVelExp);
                    sw.WriteDouble(rivers[i].sedBedDepth);
                    sw.WriteDouble(rivers[i].sedVegFactor);
                    sw.WriteDouble(rivers[i].sedBankStabFactor);
                    sw.WriteDouble(rivers[i].sedDiffusionRate);
                    sw.WriteLine();

                    sw.WriteString("SEDTYPES");
                    sw.WriteDouble(rivers[i].sedFirstPartSizePct);
                    sw.WriteDouble(rivers[i].sedSecondPartSizePct);
                    sw.WriteDouble(rivers[i].sedThirdPartSizePct);
                    sw.WriteLine();

                    sw.WriteString("AIRK" + rivers[i].idNum.ToString());
                    sw.WriteDouble(rivers[i].reaerationRateMult);
                    sw.WriteDouble(rivers[i].sedOxygenDemand);
                    sw.WriteDouble(rivers[i].precipSettleRate);
                    sw.WriteLine();

                    //reactions
                    WriteDoubleData(sw, "REAC-H2O", rivers[i].waterReactionRate);
                    WriteDoubleData(sw, "REAC-BED", rivers[i].bedReactionRate);

                    //water quality observations
                    sw.WriteString("OBSD" + rivers[i].idNum.ToString());
                    sw.WriteString(rivers[i].obsWQFilename);
                    sw.WriteLine();

                    //CEQUALW2
                    sw.WriteString("W2FILES");
                    sw.WriteInt(rivers[i].numCEQW2Files);
                    sw.WriteLine();
                    if (rivers[i].numCEQW2Files == 3)
                    {
                        sw.WriteString("W2FILES");
                        sw.WriteString(rivers[i].flowInputFilename);
                        sw.WriteLine();
                        sw.WriteString("W2FILES");
                        sw.WriteString(rivers[i].tempInputFilename);
                        sw.WriteLine();
                        sw.WriteString("W2FILES");
                        sw.WriteString(rivers[i].waterQualInputFilename);
                        sw.WriteLine();
                    }

                    //initial water column concentrations
                    WriteDoubleData(sw, "STRC" + rivers[i].idNum.ToString(), rivers[i].componentConcentration);

                    //initial bed adsorbed concentrations
                    WriteDoubleData(sw, "BEDC" + rivers[i].idNum.ToString(), rivers[i].bedAdsorpConcentration);

                    //water column adsorption isotherms
                    WriteDoubleData(sw, "STRA" + rivers[i].idNum.ToString(), rivers[i].waterAdsorpIsotherm);

                    //bed adsorption isotherms
                    WriteDoubleData(sw, "BEDA" + rivers[i].idNum.ToString(), rivers[i].bedAdsorpIsotherm);


                }

                //Reservoirs
                sw.WriteLine("******** LAKE COEFFICIENTS ********");
                for (int i = 0; i < numReservoirs; i++)
                {
                    sw.WriteLine("++++++++ RESERVOIR " + i.ToString() + " ++++++++");

                    sw.WriteString("DEPTH" + (i + 1).ToString());
                    sw.WriteInt(reservoirs[i].idNum);
                    sw.WriteInt(reservoirs[i].numSegments);
                    sw.WriteOnOffas1or0(reservoirs[i].swCalcPseudo);
                    sw.WriteInt(reservoirs[i].METFilenum);
                    sw.WriteDouble(reservoirs[i].elevation);
                    sw.WriteInt(reservoirs[i].airRainChemFilenum);
                    sw.WriteInt(reservoirs[i].coarseAirPartFilenum);
                    sw.WriteString(reservoirs[i].releaseFlowFilename);
                    sw.WriteLine();

                    //Stage-discharge
                    sw.WriteString("STGFLO");
                    int k = 0;
                    for (int j = 0; j < 9; j++)
                    {
                        sw.WriteDouble(reservoirs[i].spillway[j].stage);
                        k++;
                        if (k == 9)
                        {
                            sw.WriteLine();
                            sw.WriteString("STGFLO");
                        }
                        sw.WriteDouble(reservoirs[i].spillway[j].flow);
                        k++;
                        if (k == 9)
                        {
                            sw.WriteLine();
                            sw.WriteString("STGFLO");
                        }
                    }
                    sw.WriteLine();

                    //Stage-area
                    sw.WriteString("TOTSTA");
                    k = 0;
                    for (int j = 0; j < 9; j++)
                    {
                        sw.WriteDouble(reservoirs[i].bathymetry[j].stage);
                        k++;
                        if (k == 9)
                        {
                            sw.WriteLine();
                            sw.WriteString("TOTSTA");
                        }
                        sw.WriteDouble(reservoirs[i].bathymetry[j].area);
                        k++;
                        if (k == 9)
                        {
                            sw.WriteLine();
                            sw.WriteString("TOTSTA");
                        }
                    }
                    sw.WriteLine();

                    //Observed reservoir hydrology
                    sw.WriteString("OBSDATA");
                    sw.WriteOnOffas1or0(reservoirs[i].swAdjustResRelease);
                    sw.WriteString(reservoirs[i].hydrologyFilename);
                    sw.WriteLine();

                    //water column reactions
                    WriteDoubleData(sw, "REAC-H2O", reservoirs[i].waterReactionRate);

                    //bed sediment reactions
                    WriteDoubleData(sw, "REAC-BED", reservoirs[i].bedReactionRate);

                    //water adsorption isotherms
                    WriteDoubleData(sw, "ADSISO", reservoirs[i].waterAdsorpIsotherm);

                    //bed adsorption isotherms
                    WriteDoubleData(sw, "ADSISO", reservoirs[i].bedAdsorpIsotherm);

                    //algae (3 types)
                    for (int J = 0; J < 3; J++)
                    {
                        sw.WriteString("ALGAE");
                        sw.WriteDouble(reservoirs[i].algae[J].nitroHalfSat);
                        sw.WriteDouble(reservoirs[i].algae[J].phosHalfSat);
                        sw.WriteDouble(reservoirs[i].algae[J].silicaHalfSat);
                        sw.WriteDouble(reservoirs[i].algae[J].lightSat);
                        sw.WriteDouble(reservoirs[i].algae[J].lowTempLimit);
                        sw.WriteDouble(reservoirs[i].algae[J].highTempLimit);
                        sw.WriteDouble(reservoirs[i].algae[J].optGrowTemp);
                        sw.WriteLine();
                    }

                    //CE-QUAL-W2 data
                    //water quality parameters used in CE-QUAL-W2 simulations
                    sw.WriteString("W2PARAM");
                    sw.WriteInt(reservoirs[i].numWaterQualParams);
                    sw.WriteLine();
                    WriteIntData(sw, "W2PARAM", reservoirs[i].codesWQParams);

                    //derived (??)
                    sw.WriteString("W2DERIVE");
                    sw.WriteInt(reservoirs[i].numDerived);
                    sw.WriteLine();
                    WriteIntData(sw, "W2DERIVE", reservoirs[i].derived);

                    //CE-QUAL-W2 files
                    sw.WriteString("W2CNTRL");
                    sw.WriteString(reservoirs[i].waterQualControlFilename);
                    sw.WriteLine();
                    sw.WriteString("W2MET");
                    sw.WriteString(reservoirs[i].masterMetFilename);
                    sw.WriteLine();
                    sw.WriteString("W2OUTFLO");
                    sw.WriteString(reservoirs[i].prescribedOutFlowFilename);
                    sw.WriteLine();
                    sw.WriteString("W2FILES");
                    sw.WriteInt(reservoirs[i].numCEQW2Files);
                    sw.WriteLine();
                    if (reservoirs[i].numCEQW2Files == 3)
                    {
                        sw.WriteString("W2FILES");
                        sw.WriteString(reservoirs[i].flowInputFilename);
                        sw.WriteLine();
                        sw.WriteString("W2FILES");
                        sw.WriteString(reservoirs[i].tempInputFilename);
                        sw.WriteLine();
                        sw.WriteString("W2FILES");
                        sw.WriteString(reservoirs[i].waterQualInputFilename);
                        sw.WriteLine();
                    }

                    //Reservoir segments
                    for (int j = 0; j < reservoirs[i].numSegments; j++)
                    {
                        sw.WriteLine("-------- RESERVOIR SEGMENT " + reservoirs[i].reservoirSegs[j].idNum.ToString() + " --------");
                        sw.WriteString("DEPTH");
                        sw.WriteInt(reservoirs[i].reservoirSegs[j].idNum);
                        sw.WriteDouble(reservoirs[i].reservoirSegs[j].bottomElevation);
                        sw.WriteOnOffas1or0(reservoirs[i].reservoirSegs[j].swOutputResults);
                        sw.WriteInt(reservoirs[i].reservoirSegs[j].numOutlets);
                        sw.WriteString(reservoirs[i].reservoirSegs[j].name);
                        sw.WriteLine();

                        //Outlets
                        for (k = 0; k < reservoirs[i].reservoirSegs[j].numOutlets + 1; k++)
                        {
                            sw.WriteString("DEPTH");
                            sw.WriteDouble(reservoirs[i].reservoirSegs[j].outlets[k].elevation);
                            sw.WriteDouble(reservoirs[i].reservoirSegs[j].outlets[k].width);
                            sw.WriteInt(reservoirs[i].reservoirSegs[j].outlets[k].outletType);
                            sw.WriteInt(reservoirs[i].reservoirSegs[j].outlets[k].numFlowFile);
                            sw.WriteString(reservoirs[i].reservoirSegs[j].outlets[k].managedFlowFilename);
                            sw.WriteLine();
                        }

                        //Point sources
                        sw.WriteString("PTSOURCE");
                        sw.WriteInt(reservoirs[i].reservoirSegs[j].numPointSources);
                        sw.WriteLine();
                        if (reservoirs[i].reservoirSegs[j].numPointSources > 0)
                        {
                            WriteIntData(sw, "PTSOURCE", reservoirs[i].reservoirSegs[j].pointSources);
                        }

                        //Met parameters
                        sw.WriteString("METFAC");
                        sw.WriteDouble(reservoirs[i].reservoirSegs[j].precipWgtMult);
                        sw.WriteDouble(reservoirs[i].reservoirSegs[j].tempLapse);
                        sw.WriteDouble(reservoirs[i].reservoirSegs[j].windSpeedMult);
                        sw.WriteDouble(reservoirs[i].reservoirSegs[j].radiationFractionReachingDepth);
                        sw.WriteDouble(reservoirs[i].reservoirSegs[j].radiationFractionDepth);
                        sw.WriteDouble(reservoirs[i].reservoirSegs[j].SecchiDiskDepth);
                        sw.WriteLine();

                        //Mixing parameters
                        sw.WriteString("GMIN");
                        sw.WriteDouble(reservoirs[i].reservoirSegs[j].minNegDensity);
                        sw.WriteDouble(reservoirs[i].reservoirSegs[j].minDiffCoeff);
                        sw.WriteDouble(reservoirs[i].reservoirSegs[j].windMixA1Coef);
                        sw.WriteDouble(reservoirs[i].reservoirSegs[j].windMixA2Coef);
                        sw.WriteDouble(reservoirs[i].reservoirSegs[j].windMixMaxDiffCoef);
                        sw.WriteDouble(reservoirs[i].reservoirSegs[j].criticalDensityGradient);
                        sw.WriteDouble(reservoirs[i].reservoirSegs[j].densityGradMaxDiffCoef);
                        sw.WriteDouble(reservoirs[i].reservoirSegs[j].densityGradExp);
                        sw.WriteDouble(reservoirs[i].reservoirSegs[j].inflowEntrain);
                        sw.WriteLine();

                        //Sediment parameters
                        sw.WriteString("SED");
                        sw.WriteDouble(reservoirs[i].reservoirSegs[j].sedBottomThickness);
                        sw.WriteDouble(reservoirs[i].reservoirSegs[j].sedDiffusion);
                        sw.WriteLine();

                        //Stage-area
                        sw.WriteString("STGAREA");
                        k = 0;
                        for (int l = 0; l < 9; l++)
                        {
                            sw.WriteDouble(reservoirs[i].reservoirSegs[j].bathymetry[l].stage);
                            k++;
                            if (k == 9)
                            {
                                sw.WriteLine();
                                sw.WriteString("STGAREA");
                            }
                            sw.WriteDouble(reservoirs[i].reservoirSegs[j].bathymetry[l].area);
                            k++;
                            if (k == 9)
                            {
                                sw.WriteLine();
                                sw.WriteString("STGAREA");
                            }
                        }
                        sw.WriteLine();

                        //chemical concentrations
                        WriteDoubleData(sw, "LAKION", reservoirs[i].reservoirSegs[j].chemConcentrations);
                        WriteDoubleData(sw, "BEDION", reservoirs[i].reservoirSegs[j].chemConBedSediment);

                        //Depth-temperature relationship
                        sw.WriteString("DEPTHTMP");
                        k = 0;
                        for (int l = 0; l < 9; l++)
                        {
                            sw.WriteDouble(reservoirs[i].reservoirSegs[j].depthTemp[l].depth);
                            k++;
                            if (k == 9)
                            {
                                sw.WriteLine();
                                sw.WriteString("DEPTHTMP");
                            }
                            sw.WriteDouble(reservoirs[i].reservoirSegs[j].depthTemp[l].temp);
                            k++;
                            if (k == 9)
                            {
                                sw.WriteLine();
                                sw.WriteString("DEPTHTMP");
                            }
                        }
                        sw.WriteLine();

                        //Upstream sources - catchments, rivers, reservoir segments
                        WriteIntData(sw, "ICATOL", reservoirs[i].reservoirSegs[j].upstreamCatchIDs);
                        WriteIntData(sw, "IRVTOL", reservoirs[i].reservoirSegs[j].upstreamRiverIDs);
                        WriteIntData(sw, "ICATOL", reservoirs[i].reservoirSegs[j].upstreamReservoirIDs);

                        //Diversions to
                        sw.WriteString("DIVTO");
                        sw.WriteInt(reservoirs[i].reservoirSegs[j].numDiversionsTo);
                        sw.WriteLine();
                        if (reservoirs[i].reservoirSegs[j].numDiversionsTo > 0)
                        {
                            WriteIntData(sw, "DIVTO", reservoirs[i].reservoirSegs[j].diversionToFilenums);
                        }

                        //observed water quality data
                        sw.WriteString("OBSDATA");
                        sw.WriteString(reservoirs[i].reservoirSegs[j].obsWQFilename);
                        sw.WriteLine();
                    }

                    //water quality criteria and TMDL
                    //This consists of three lines at the end of the COE file
                    //The lines are written to the coe file in the writeCON() function
                }

                sw.Close();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("COE write exception at line " + sw.LineNum + ": " + e.ToString());
                return false;
            }
        }

        public void CompileSubwatershed(Node DownstreamNode, ref Coefficients SubwatershedCoefficients)
        {
            // This requires moving Tributary rivers and reservoir segments from NodeHydro to Node, might be more elegant way
            // of doing it
            for (int i = 0; i < DownstreamNode.upstreamCatchIDs.Count; i++)
            {
                int catchIndex = GetCatchmentNumberFromID(DownstreamNode.upstreamCatchIDs[i]);
                if (catchIndex >= 0)
                {
                    // Add everything upstream of the upstream catchment (catchment to catchment connections)
                    CompileSubwatershed(catchments[catchIndex], ref SubwatershedCoefficients);
                    // Add the upstream river - is this a safe way to add the river to the subwatershed coefficients
                    // or do we need to copy the river coefficients first?
                    SubwatershedCoefficients.catchments.Add(catchments[catchIndex]);
                }
            }
            for (int i = 0; i < DownstreamNode.upstreamRiverIDs.Count; i++)
            {
                int riverIndex = GetRiverNumberFromID(DownstreamNode.upstreamRiverIDs[i]);
                if (riverIndex >= 0)
                {
                    // Add everything upstream of the upstream river if it is not a subwatershed boundary
                    if (!rivers[riverIndex].swIsSubwaterBoundary)
                    {
                        CompileSubwatershed(rivers[riverIndex], ref SubwatershedCoefficients);
                    }
                    // Add the upstream river - is this a safe way to add the river to the subwatershed coefficients
                    // or do we need to copy the river coefficients first?
                    SubwatershedCoefficients.rivers.Add(rivers[riverIndex]);
                }
            }
            for (int i = 0; i < DownstreamNode.upstreamReservoirIDs.Count; i++)
            {
                List<int> reservoirAndSegment = GetReservoirAndSegmentFromID(DownstreamNode.upstreamReservoirIDs[i]);
                if (reservoirAndSegment.Count == 2 && reservoirAndSegment[0] >= 0 && reservoirAndSegment[1] >= 0)
                {
                    // Add everything upstream of the upstream reservoir segment if it is not a subwatershed boundary
                    if (reservoirs[reservoirAndSegment[0]].reservoirSegs[reservoirAndSegment[1]].swIsSubwaterBoundary)
                    {
                        CompileSubwatershed(reservoirs[reservoirAndSegment[0]].reservoirSegs[reservoirAndSegment[1]], ref SubwatershedCoefficients);
                    }
                    // Add the upstream reservoir IF this segment is the subwatershed boundary
                    // Again, don't know if this is a safe way to add to subwatershed coefficients or if a copy needs to be made
                    if (reservoirs[reservoirAndSegment[0]].reservoirSegs[reservoirAndSegment[1]].swIsSubwaterBoundary)
                    {
                        SubwatershedCoefficients.reservoirs.Add(reservoirs[reservoirAndSegment[0]]);
                    }
                }
            }
        }

        #endregion

        #region Methods - Working with Entity IDs, Names, WARMF Codes, etc

        public int GetCatchmentNumberFromID(int id)
        {
            for (int i = 0; i < catchments.Count; i++)
            {
                if (catchments[i].idNum == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetRiverNumberFromID(int id)
        {
            for (int i = 0; i < rivers.Count; i++)
            {
                if (rivers[i].idNum == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public List<int> GetReservoirAndSegmentNumberFromID(int id)
        {
            List<int> ReservoirAndSegment = new List<int>();
            for (int i = 0; i < numReservoirs; i++)
            {
                for (int j = 0; j < reservoirs[i].numSegments; j++)
                {
                    if (reservoirs[i].reservoirSegs[j].idNum == id)
                    {
                        ReservoirAndSegment.Add(i);
                        ReservoirAndSegment.Add(j);
                        return ReservoirAndSegment;
                    }
                }
            }
            ReservoirAndSegment.Add(-1);
            ReservoirAndSegment.Add(-1);
            return ReservoirAndSegment;
        }

        public int GetPTSNumberFromName(string name)
        {
            for (int i = 0; i < numPTSFiles; i++)
            {
                if (PTSFilename[i] == name)
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetParameterNumberFromCode(string TheCode)
        // Returns the ordinal number of the constituent from the master list of all constituents 
        // (hydro -> chemical -> physical -> composite)
        {
            int totalNumConstits = numHydrologyParams + numChemicalParams + numPhysicalParams + numCompositeParams;
            for (int ii = 0; ii < totalNumConstits; ii++)
                if (String.Compare(TheCode, AllConstits[ii].fortranCode, new CultureInfo("en-US"), System.Globalization.CompareOptions.IgnoreSymbols) == 0)
                    return ii;
            return -1;
        }

        public string GetParameterCodeFromName(string name)
        {
            int totalNumConstits = numHydrologyParams + numChemicalParams + numPhysicalParams + numCompositeParams;
            for (int ii = 0; ii < totalNumConstits; ii++)
                if (String.Compare(name, AllConstits[ii].fortranCode, new CultureInfo("en-US"), System.Globalization.CompareOptions.IgnoreSymbols) == 0)
                    return AllConstits[ii].fortranCode;
            return "";
        }

        public string GetParameterCodeFromNumber(int num)
        {
            string strResult;
            if (num >= 0 && num < AllConstits.Count)
            {
                strResult = AllConstits[num].fortranCode;
                strResult = strResult.Trim();
                return strResult;
            }
            else
                return "";
        }

        public string GetParameterNameFromCode(string TheCode)
        {
            int parameterNumber = GetParameterNumberFromCode(TheCode);
            if (parameterNumber >= 0)
               return AllConstits[parameterNumber].fullName;

            return TheCode;
        }

        public string GetParameterNameAndUnitsFromCode(string TheCode)
        {
            int parameterNumber = GetParameterNumberFromCode(TheCode);
            if (parameterNumber >= 0)
                return AllConstits[parameterNumber].fullName + ", " + AllConstits[parameterNumber].units;

            return TheCode;
        }

        public string GetParameterNameAndUnitsFromNumber(int num)
        {
            string strResult;
            if (num >= 0 && num < AllConstits.Count)
            {
                strResult = AllConstits[num].fullName + ", " + AllConstits[num].units;
                strResult = strResult.Trim();
                return strResult;
            }  
            else
                return "";
        }

        public List<NodeHydro> GetWatershedPourPoints()
        {
            List<int> UpstreamNodeIDs = new List<int>();
            List<NodeHydro> watershedPourPoints = new List<NodeHydro>();

            for (int i = 0; i < Global.coe.numRivers; i++)
            {
                UpstreamNodeIDs.AddRange(Global.coe.rivers[i].upstreamRiverIDs);
                UpstreamNodeIDs.AddRange(Global.coe.rivers[i].upstreamReservoirIDs);
            }
            for (int i = 0; i < Global.coe.numReservoirs; i++)
            {
                for (int j = 0; j < Global.coe.reservoirs[i].numSegments; j++)
                {
                    UpstreamNodeIDs.AddRange(Global.coe.reservoirs[i].reservoirSegs[j].upstreamRiverIDs);
                    UpstreamNodeIDs.AddRange(Global.coe.reservoirs[i].reservoirSegs[j].upstreamReservoirIDs);
                }
            }
            for (int i = 0; i < Global.coe.numRivers; i++)
            {
                if (! UpstreamNodeIDs.Contains(Global.coe.rivers[i].idNum))
                {
                    watershedPourPoints.Add(Global.coe.rivers[i]);
                }
            }
            for (int i = 0; i < Global.coe.numReservoirs; i++)
            {
                for (int j = 0; j < Global.coe.reservoirs[i].numSegments; j++)
                {
                    if (! UpstreamNodeIDs.Contains(Global.coe.reservoirs[i].reservoirSegs[j].idNum))
                    {
                        watershedPourPoints.Add(Global.coe.reservoirs[i].reservoirSegs[j]);
                    }
                }
            }
            return watershedPourPoints;
        }

        #endregion

        #region Methods - Working with Observed Data (ORC, ORH, OLC, OLH)

        // Compiles a list of all catchment, river, and reservoir observed hydrology files
        public List<string> GetAllObservedHydrologyFiles()
        {
            List<string> obsHydFiles = new List<string>();
            // Catchment prescribed ponding depth files
            for (int ii = 0; ii < catchments.Count; ii++)
                if (catchments[ii].nluPonds > 0)
                    for (int jj = 0; jj < catchments[ii].pondFilename.Count; jj++)
                        obsHydFiles.Add(catchments[ii].pondFilename[jj]);
            // River observed flow files
            for (int ii = 0; ii < rivers.Count; ii++)
                if (!String.IsNullOrWhiteSpace(rivers[ii].hydrologyFilename))
                    obsHydFiles.Add(rivers[ii].hydrologyFilename);
            // Reservoir observed volume / surface elevation files
            for (int ii = 0; ii < reservoirs.Count; ii++)
                if (!String.IsNullOrWhiteSpace(reservoirs[ii].hydrologyFilename))
                    obsHydFiles.Add(reservoirs[ii].hydrologyFilename);

            return obsHydFiles;
        }

        // Compiles a list of all catchment, river, and reservoir observed water quality files
        public List<string> GetAllObservedWaterQualityFiles()
        {
            List<string> obsWQFiles = new List<string>();
            // River observed water quality files
            for (int ii = 0; ii < rivers.Count; ii++)
                if (!String.IsNullOrWhiteSpace(rivers[ii].obsWQFilename))
                    obsWQFiles.Add(rivers[ii].obsWQFilename);
            // Reservoir observed volume / surface elevation files
            for (int ii = 0; ii < reservoirs.Count; ii++)
                for (int jj = 0; jj < reservoirs[ii].reservoirSegs.Count; jj++)
                    if (!String.IsNullOrWhiteSpace(reservoirs[ii].reservoirSegs[jj].obsWQFilename))
                        obsWQFiles.Add(reservoirs[ii].reservoirSegs[jj].obsWQFilename);

            return obsWQFiles;
        }

        #endregion
    }
}
