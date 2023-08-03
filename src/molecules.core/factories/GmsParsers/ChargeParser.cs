using molecules.core.valueobjects.Molecules;
using molecules.shared;
using System.ComponentModel;

namespace molecules.core.factories.GmsParsers
{
    public static class ChargeParser
    {
        private const string StartTag = "          ELECTROSTATIC POTENTIAL";
        private const string StartChargedTag = " NET CHARGES:";
        private const string EndChargeTag = " -------------------------------------";
        private const string StartElpotTag = " NUMBER OF POINTS SELECTED FOR FITTING";
        private const string GeoDiscTag = "PTSEL=GEODESIC";

        public static void Parse(List<string> fileLines, Molecule molecule )
        {
            bool overallstart = false;
            bool startCharge = false;
            bool startElpot = false;
            bool isGeoDisc = false;
            int currentAtomPos = 1;
            for (int c = 0; c < fileLines.Count; ++c)
            {
                var line = fileLines[c];

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
                            PosX = StringConversion.ToDecimal(data[1]),
                            PosY = StringConversion.ToDecimal(data[2]),
                            PosZ = StringConversion.ToDecimal(data[3]),
                            Electronic = StringConversion.ToDecimal(data[4]),
                            Nuclear = StringConversion.ToDecimal(data[5]),
                            Total = StringConversion.ToDecimal(data[6]),
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


    }
}
