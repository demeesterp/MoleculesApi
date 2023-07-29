using molecules.core.factories.GmsParsers;
using molecules.core.valueobjects.AtomProperty;
using molecules.core.valueobjects.GmsCalc;
using molecules.core.valueobjects.Molecules;
using molecules.shared;

namespace molecules.core.factories
{
    public class GmsMoleculeFactory : IGmsMoleculeFactory
    {
        public bool TryCompleteMolecule(string fileName, List<string> fileLines, Molecule molecule)
        {
            if ( fileName.Contains(GmsCalculationKind.GeometryOptimization.ToString()) ) {
                ParseGeoOpt(fileLines, molecule);
                ParseGeoptDftEnergy(fileLines, molecule);
            }
            else if ( fileName.Contains(GmsCalculationKind.GeoDiskCharge.ToString()) ) {
                ParseCharge(fileLines,molecule);
            }
            else if ( fileName.Contains(GmsCalculationKind.CHelpGCharge.ToString()) ) {
                ParseCharge(fileLines, molecule);
            }
            else if ( fileName.Contains(GmsCalculationKind.FukuiHOMO.ToString()) ) {
                new LewisHOMOPopulationAnalysisParser().Parse(fileLines, molecule);
                molecule.HFEnergyHOMO = new FukuiEnergyLewisHOMOParser().Parse(fileLines);
            }
            else if ( fileName.Contains(GmsCalculationKind.FukuiLUMO.ToString()) ) {
                new LewisLUMOPopulationAnalysisParser().Parse(fileLines, molecule);
                molecule.HFEnergyLUMO = new FukuiEnergyLewisLUMOParser().Parse(fileLines);
            }
            else if ( fileName.Contains(GmsCalculationKind.FukuiNeutral.ToString()) ) {
                new NeutralPopulationAnalysisParser().Parse(fileLines, molecule);
                molecule.HFEnergy = new FukuiEnergyNeutralParser().Parse(fileLines);
            } 
            else {
                return false;
            }
            return true;
        }

        private void ParseGeoOpt(List<string> gmsFile, Molecule molecule)
        {
            string OptimizationResultTag = "***** EQUILIBRIUM GEOMETRY LOCATED *****";
            string line = string.Empty;
            bool start = false;
            int position = 1;
            for (int c = 0; c < gmsFile.Count; ++c)
            {
                line = gmsFile[c];
                if (line.Contains(OptimizationResultTag))
                {
                    start = true;
                    c += 3;
                    continue;
                }

                if (start)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        break;
                    }

                    var newatom = ParseOptAtomPosition(line);
                    newatom.Position = position;
                    newatom.Number = AtomPropertiesTable.GetAtomProperties(newatom.Symbol)?.AtomNumber??0;

                    var existingAtom = molecule.Atoms.Find((i) => i.Symbol == newatom.Symbol
                                                                && i.Position == position);
                    if (existingAtom != null)
                    {
                        existingAtom.PosX = newatom.PosX;
                        existingAtom.PosY = newatom.PosY;
                        existingAtom.PosZ = newatom.PosZ;
                        existingAtom.AtomicWeight = newatom.AtomicWeight;
                    }
                    else
                    {
                        molecule.Atoms.Add(newatom);
                    }

                    ++position;
                }
            }
        }

        public void ParseGeoptDftEnergy(List<string> gmsFile, Molecule molecule)
        {
            string OptimizationResultTag = "***** EQUILIBRIUM GEOMETRY LOCATED *****";
            string EnergyStartTag = "          ENERGY COMPONENTS";
            string EnergyTag = "                       TOTAL ENERGY";

            bool start = false;
            bool overallstart = false;
            for (int c = 0; c < gmsFile.Count; ++c)
            {
                string line = gmsFile[c];
                if (line.Contains(OptimizationResultTag))
                {
                    overallstart = true;
                }

                if (overallstart && line.Contains(EnergyStartTag))
                {
                    start = true;
                }

                if (start && line.Contains(EnergyTag))
                {
                    var data = line.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length > 1)
                    {
                        molecule.DftEnergy = StringConversion.ToDecimal(data[1].Trim());

                        break;
                    }
                }
            }
        }

        public void ParseCharge(List<string> gmsFile, Molecule molecule)
        {
            string StartTag = "          ELECTROSTATIC POTENTIAL";
            string StartChargedTag = " NET CHARGES:";
            string EndChargeTag = " -------------------------------------";
            string StartElpotTag = " NUMBER OF POINTS SELECTED FOR FITTING";
            string GeoDiscTag = "PTSEL=GEODESIC";
            string line = string.Empty;
            bool overallstart = false;
            bool startCharge = false;
            bool startElpot = false;
            bool isGeoDisc = false;
            int currentAtomPos = 1;
            for (int c = 0; c < gmsFile.Count; ++c)
            {
                line = gmsFile[c];

                if (line.Contains(StartTag))
                {
                    overallstart = true;
                }

                if (line.Contains(GeoDiscTag))
                {
                    isGeoDisc = true;
                }

                if (overallstart && line.Contains(StartChargedTag))
                {
                    startCharge = true;
                    c += 3;
                    continue;
                }



                if (overallstart && line.StartsWith(StartElpotTag))
                {
                    startElpot = true;
                    continue;
                }

                if (startElpot)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        startElpot = false;
                        continue;
                    }

                    var data = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length > 6)
                    {
                        ElectronicPotential item = new ElectronicPotential()
                        {
                            PosX = Convert.ToDecimal(data[1]),
                            PosY = Convert.ToDecimal(data[2]),
                            PosZ = Convert.ToDecimal(data[3]),
                            Electronic = Convert.ToDecimal(data[4]),
                            Nuclear = Convert.ToDecimal(data[5]),
                            Total = Convert.ToDecimal(data[6]),
                            Type = isGeoDisc ? ElectronicPotentialType.GeoDisc.ToString() : ElectronicPotentialType.CHelgG.ToString()
                        };
                        molecule.ElPot.Add(item);
                    }
                }

                if (startCharge)
                {
                    if (line.Contains(EndChargeTag))
                    {
                        return;
                    }

                    var data = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length > 2)
                    {
                        string symbol = data[0];
                        decimal charge = StringConversion.ToDecimal(data[1].Trim());
                        var atom = molecule.Atoms.Find(i => i.Position == currentAtomPos && i.Symbol == symbol);
                        if (atom != null)
                        {
                            if (isGeoDisc)
                            {
                                atom.GeoDiscCharge = charge;
                            }
                            else
                            {
                                atom.CHelpGCharge = charge;
                            }

                        }
                        ++currentAtomPos;
                    }
                }
            }
        }

        private static Atom ParseOptAtomPosition(string line)
        {
            Atom retval = new Atom();

            var current = FindNthSegment(line, 1);
            string atomsymbol = line.Substring(current.Item1, current.Item2 - current.Item1);
            current = FindNthSegment(line, 2);
            string charge = line.Substring(current.Item1, current.Item2 - current.Item1);

            string posx = line.Substring(current.Item2, 15);
            string posy = line.Substring(current.Item2 + 15, 15);
            string posz = line.Substring(current.Item2 + 30, 15);

            retval.Symbol = atomsymbol;
            retval.AtomicWeight = (int)StringConversion.ToDecimal(charge);
            retval.PosX = StringConversion.ToDecimal(posx);
            retval.PosY = StringConversion.ToDecimal(posy);
            retval.PosZ = StringConversion.ToDecimal(posz);

            return retval;
        }

        private static int FindFirstSpace(string input, int startPos = 0)
        {
            int retval = startPos;
            for (int c = startPos; c < input.Length; ++c)
            {
                if (input[c] == ' ')
                {
                    retval = c;
                    break;
                }
            }
            return retval;
        }

        private static int FindFirstNoSpace(string input, int startPos = 0)
        {
            int retval = startPos;
            for (int c = startPos; c < input.Length; ++c)
            {
                if (input[c] != ' ')
                {
                    retval = c;
                    break;
                }
            }
            return retval;
        }

        private static Tuple<int, int> FindNthSegment(string input, int n)
        {
            int currentbegin = 0;
            int currentend = 0;
            while (n > 0)
            {
                currentbegin = FindFirstNoSpace(input, currentend);
                currentend = FindFirstSpace(input, currentbegin);
                --n;
            }
            return new Tuple<int, int>(currentbegin, currentend);
        }
    }
}
