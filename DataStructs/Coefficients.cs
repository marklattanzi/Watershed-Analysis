using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace warmf {
    public struct DIVFileData {
        public string filename;
        public double flowDivertedMultiplier;
        public double flowCap;
        public bool swUseMonthFlows;
        public List<double> monthlyFlow;
    }

    public struct HydrologyConstits {
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

    public struct ChemicalConstits {
        public string fortranCode;
        public bool swIncludeInOutput;
        public string units;
        public string abbrevName;
        public string fullName;
        public bool swCatchmentInclude;
        public bool swRiverInclude;
        public bool swReservoirInclude;
        public bool swLoadingInclude;
        public double electricalCharge;
        public double massEquivalent;
        public double loadingUnitConversion;
        public string loadingUnits;
        public double airRainMult;
        public double pointSourceMult;
        public double nonpointSourceMult;
        public double solubWithSulfate;
        public double stoichChemWithSulfate;
        public double stoichSulfateWithChem;
        public double solubWithHydrox;
        public double stoichChemWithHydrox;
        public double stoichHydroxWithChem;
        public bool swLoadingTMDL;
        public int dryDepositionForm;
        public bool swChemAdvection;
        public List<double> gasDepositVelocity;
    }

    public struct PhysicalConstits {
        public string fortranCode;
        public bool swIncludeInOutput;
        public string units;
        public string abbrevName;
        public string fullName;
        public bool swCatchmentInclude;
        public bool swRiverInclude;
        public bool swReservoirInclude;
        public bool swLoadingInclude;
        public double electricalCharge;
        public double massEquivalent;
        public double loadingUnitConversion;
        public string loadingUnits;
        public double airRainMult;
        public double pointSourceMult;
        public double nonpointSourceMult;
        public bool swLoadingTMDL;
        public int dryDepositionForm;
        public bool swChemAdvection;
        public List<double> gasDepositVelocity;
    }

    public struct CompositeConstits {
        public string fortranCode;
        public bool swIncludeInOutput;
        public string units;
        public string abbrevName;
        public string fullName;
        public bool swCatchmentInclude;
        public bool swRiverInclude;
        public bool swReservoirInclude;
        public bool swLoadingInclude;
        public double electricalCharge;
        public double massEquivalent;
        public double loadingUnitConversion;
        public string loadingUnits;

        public bool swIncludeDissolvedConstits;
        public bool swIncludeAdsorbedConstits;
        public bool swIncludePrecipConstits;

        public List<double> componentTotalMass;
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
        public double courseLitterFrac;
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

        public List<double> soilReactionRate;
        public List<double> surfaceReactionRate;
        public List<double> canopyReactionRate;
        public List<double> biozoneReactionRate;

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

    public struct Catchment {
        public int idNum;
        public int METFileNum;
        public double precipMultiplier;
        public double aveTempLapse;
        public double altitudeTempLapse;
        public bool swOutputToFile;
        public int airRainChemFileNum;         // from AIRFILES
        public int particleRainChemFileNum;    // from AIRFILES
        public string name;

        public int numSoilLayers;
        public double slope;
        public double width;
        public double aspect;
        public double ManningN;
        public double detentionStorage;

        public List<int> upstreamCatchmentNum;

        // landuse
        public List<double> landUsePercent;
        public List<int> fertPlanNum;
        public List<double> landApplicationLoad;
        public List<int> numIrrigationSources;
        public List<List<double>> irrigationSource;
        public List<double> irrigationSourcePercent;

        public int nluPonds;
        public List<string> pondFilename;
        public int numPointSources;
        public List<int> pointSources;
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
        public List<Soil> soils;
    }

    public struct StageWidth {
        public double stage;
        public double width;
    }

    public struct River {
        public int idNum;
        public double depth;
        public double length;
        public double upElevation;
        public double downElevation;
        public double ManningN;
        public double temp;
        public double convectiveHeatFactor;
        public double minFlow;

        public bool swIncludeOutput;
        public bool swIsSubwaterBoundary;
        public string name;

        public double impoundArea;
        public double impoundVol;
        public double freezingTemp;
        public double meltingTemp;
        public double iceCalcAve;

        public List<StageWidth> segment;
        public List<int> upstreamCatch;
        public List<int> upstreamRiver;
        public List<int> upstreamReservoir;
        public int numDiversionsFrom;
        public List<int> divFilenumFrom;
        public int numDiversionsTo;
        public List<int> divFilenumTo;
        public int numPointSrcs;
        public List<int> pointSrcFilenum;

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
        public string waterQualFilename;

        public int numCEQW2Files;
        public string flowInputFilename;
        public string tempInputFilename;
        public string waterQualInputFilename;

        public List<double> componentConcentration;
        public List<double> bedAdsorpConcentration;
        public List<double> waterAdsorpIsotherm;
        public List<double> bedAdsorpIsotherm;
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

    public struct ReservoirSeg {
        public int idNum;
        public double bottomElevation;
        public bool swOutputResults;
        public int numOutlets;
        public string name;
        public List<ReservoirOutlet> outlets;
        public int numPointSrcs;
        public List<int> pointSrcFilenums;
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
        public List<int> upstreamCatchIDs;
        public List<int> upstreamRiverIDs;
        public List<int> upstreamLakeIDs;
        public List<int> diversionToFilenums;
        public string obsWQFilename;
    }

    public struct Reservoir {
		public int idNum;
        public int numSegments;
        public bool swCalcPseudo;
        public int METFilenum;
        public double elevation;
        public int airRainChemFilenum;
        public int courseAirRainChemFilenum;
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
        public string warmfstartOutFilename;
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
        public List<double> courseDV;   // one per month
        public bool swGasDepositVelocity;
        //public List<double> gasDepositVelocity; // one per month  --unused MRL
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

        // ********************************************************************************************
        // Coefficients METHODS

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
        public bool ReadFile(string filename) {
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
                warmfstartOutFilename = ReadString(sr, "FILES");
                textOutFilename = ReadString(sr, "FILES");

				// System coefficients
				line = ReadSpacerLine(sr, "****");
                dnums = ReadDoubleData(sr, "TOL", 4);
                tolerancePH = nums[0];
                // nums[1] is unused;
                toleranceCation1 = nums[2];
                toleranceCation2 = nums[3];

                dnums = ReadDoubleData(sr, "TAREA", 2);
                watershedArea = nums[0];
                watershedElevation = nums[1];

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

                        nums = ReadIntData(sr, "PHYSICAL", 3);
                        physical.swLoadingTMDL = nums[0] != 0;
                        physical.dryDepositionForm = nums[1];
                        physical.swChemAdvection = nums[2] != 0;

                        physical.gasDepositVelocity = ReadMonthlyDoubleData(sr, "PHYSICAL");
                    }
                    physicalConstits.Add(physical);
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
                litter.courseLitterFrac = dnums[0];
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
                courseDV = ReadMonthlyDoubleData(sr, "COARSEDV");
                swGasDepositVelocity = ReadOnOffSwitch(sr, "IVDGAS");
//                gasDepositVelocity = readMonthlyDoubleData(sr, "NOXSOXVD");  // missing from Catawba file - MRL
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
                    catchData.swOutputToFile = !line.Substring(48, 8).Contains("0");
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
                    catchData.upstreamCatchmentNum = ReadIntData(sr, "ICAT", 9);  // will there always be 9 values ? - MRL
                    catchData.landUsePercent = ReadDoubleData(sr, "CATC", numLanduses);

                    // fertilation plan nums per land use
                    catchData.fertPlanNum = ReadIntData(sr, "CATC", numLanduses);

                    // land application load
                    catchData.landApplicationLoad = ReadDoubleData(sr, "STOC", numLanduses);

                    // num of irrigation sources
                    catchData.numIrrigationSources = ReadIntData(sr, "IRRI", numLanduses);

                    // for each land use, get number of irrigation sources and fraction of area
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

                    // Best practices
                    dnums = ReadDoubleData(sr, "BMP", 5);
                    catchData.bmp = new CatchBMP();
                    catchData.bmp.streetSweepFreq = dnums[0];
                    catchData.bmp.streetSweepEff = dnums[1];
                    catchData.bmp.divertedImpervFlow = dnums[2];
                    catchData.bmp.detentionPondVol = dnums[3];
                    catchData.bmp.maxFertAccumTime = dnums[4];

                    // buffering/seeps
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
                        catchData.mining.depthSpoils = Double.TryParse(line.Substring(16, 8), out dblRes) ? dblRes : 0;
                        catchData.mining.soilMoisture = Double.TryParse(line.Substring(24, 8), out dblRes) ? dblRes : 0;
                        catchData.mining.fieldCapacity = Double.TryParse(line.Substring(32, 8), out dblRes) ? dblRes : 0;
                        catchData.mining.saturationMoisture = Double.TryParse(line.Substring(40, 8), out dblRes) ? dblRes : 0;
                        catchData.mining.horizHydraulicConduct = Double.TryParse(line.Substring(48, 8), out dblRes) ? dblRes : 0;
                        catchData.mining.vertHydraulicConduct = Double.TryParse(line.Substring(56, 8), out dblRes) ? dblRes : 0;
                        catchData.mining.ferroIonOxyidationRate = Double.TryParse(line.Substring(64, 8), out dblRes) ? dblRes : 0;
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
                    catchData.mining.fineLitterWgtFraction = dnums[0];
                    catchData.mining.humusWgtFraction = dnums[0];

                    // reaction rates
                    catchData.mining.soilReactionRate = ReadDoubleData(sr, "REACSOIL", numReactions);
                    catchData.mining.surfaceReactionRate = ReadDoubleData(sr, "REACSURF", numReactions);
                    catchData.mining.canopyReactionRate = ReadDoubleData(sr, "REACCNPY", numReactions);
                    catchData.mining.biozoneReactionRate = ReadDoubleData(sr, "REACBIOZ", numReactions);
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
                            soil.CO2ConcenFactor = Int32.TryParse(line.Substring(48, 8), out intRes) ? intRes : 0;
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
					river.swIncludeOutput = !line.Substring(8, 8).Contains("0");
					river.swIsSubwaterBoundary = !line.Substring(16, 8).Contains("0");
					river.name = line.Substring(24);

					dnums = ReadDoubleData(sr, "IMPO", 5);
					river.impoundArea = dnums[0];
					river.impoundVol = dnums[1];
					river.freezingTemp = dnums[2];
					river.meltingTemp = dnums[3];
					river.iceCalcAve = dnums[4];

					dnums = ReadDoubleData(sr, "STAR", 18);
					river.segment = new List<StageWidth>();
					for (int jj=0; jj<18; jj+=2) {
						StageWidth sw = new StageWidth();
						sw.stage = dnums[jj];
						sw.width = dnums[jj + 1];
						river.segment.Add(sw);
					}

					river.upstreamCatch = ReadIntData(sr, "ICAT", 9);   // how many upstream catchments - 9? - MRL
					river.upstreamRiver = ReadIntData(sr, "IRVT", 9);   // how many upstream rivers - 9? - MRL
					river.upstreamReservoir = ReadIntData(sr, "ILKT", 9);   // how many upstream reservoirs -9? - MRL

					river.numDiversionsFrom = ReadInt(sr, "DIVFROM");
					if (river.numDiversionsFrom > 0)
						river.divFilenumFrom = ReadIntData(sr, "DIVFROM", river.numDiversionsFrom);
					river.numDiversionsTo = ReadInt(sr, "DIVTO");
					if (river.numDiversionsTo > 0)
						river.divFilenumTo = ReadIntData(sr, "DIVTO", river.numDiversionsTo);
					river.numPointSrcs = ReadInt(sr, "PTSOURCE");
					if (river.numPointSrcs > 0)
						river.pointSrcFilenum = ReadIntData(sr, "PTSOURCE", river.numPointSrcs);
					nums = ReadIntData(sr, "OBSW", 7);  // not in spec...  MRL
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
					river.waterQualFilename = ReadString(sr, "OBSD");
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
					reservoir.courseAirRainChemFilenum = Int32.TryParse(line.Substring(48, 8), out intRes) ? intRes : 0;
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

						seg.numPointSrcs = ReadInt(sr, "PTSOURCE");
						if (seg.numPointSrcs > 0)
							seg.pointSrcFilenums = ReadIntData(sr, "PTSOURCE", seg.numPointSrcs);
						
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
						seg.upstreamCatchIDs = ReadIntData(sr, "ILKTOL", 9);
						seg.diversionToFilenums = ReadIntData(sr, "DIVTO", 1);	// how many nums?  need to fix readData routines - MRL
						seg.obsWQFilename = ReadString(sr, "OBSDATA");
						reservoir.reservoirSegs.Add(seg);
					}
					reservoirs.Add(reservoir);

				}

				line = sr.ReadLine();
				while (line.Contains("CRITERIA")) line = sr.ReadLine();  // remove - MRL

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

		public bool WriteFile() {
			return true;
		}
    }
}
