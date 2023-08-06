using molecules.core.valueobjects.Molecules;
using molecules.shared;

namespace molecules.core.factories.GmsParsers
{

    public enum PopulationAnalysisType
    {
        neutral,
        lewisLUMO,
        lewisHOMO
    }

    internal abstract class PopulationAnalysisParser
    {
        #region private properties

        private bool Start
        {
            get;
            set;
        }

        #endregion


        #region abstract tag methods

        protected abstract string GetGeometryResultTag();

        protected abstract string GetStartTag();

        protected abstract string GetStartTagAOPopulations();

        protected abstract string GetStartTagOverlapPopulations();

        protected abstract string GetStartTagPopulations();

        protected abstract string GetStartTagBondOrder();

        protected abstract PopulationAnalysisType GetPopulationStatus();

        #endregion

        public bool Parse(List<string> input, Molecule molecule)
        {
            bool retval = false;
            bool overallstart = false;
            for (int c = 0; c < input.Count; ++c)
            {
                var line = input[c];
                if (line.Contains(GetGeometryResultTag()))
                {
                    overallstart = true;
                }

                if (overallstart && line.Contains(GetStartTag()))
                {
                    Start = true;
                }

                if (Start)
                {
                    ParseAOPopulations(input.GetRange(c, input.Count - c), molecule);
                    ParseOverlapPopulations(input.GetRange(c, input.Count - c), molecule);
                    ParseBondOrder(input.GetRange(c, input.Count - c), molecule);
                    ParsePopulations(input.GetRange(c, input.Count - c), molecule);
                    retval = true;
                    break;
                }
            }

            return retval;

        }


        #region private helpers

        private void ParseAOPopulations(List<string> input, Molecule molecule)
        {
            if (Start)
            {
                bool startAOPop = false;
                string line = string.Empty;
                for (int c = 0; c < input.Count; ++c)
                {
                    line = input[c];
                    if (line.Contains(GetStartTagAOPopulations()))
                    {
                        startAOPop = true;
                        continue;
                    }

                    if (startAOPop)
                    {
                        var items = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                        if (items.Length == 6)
                        {
                            int atomPosition = int.Parse(items[2]);
                            string atomSymbol = items[1].Trim();
                            int orbitalpos = int.Parse(items[0]);
                            string orbitalSymbol = items[3];

                            Atom? atom = molecule.Atoms.Find(i => i.Position == atomPosition && i.Symbol == atomSymbol);
                            if (atom != null)
                            {
                                decimal mulliken = StringConversion.ToDecimal(items[4].Trim());
                                decimal lowdin = StringConversion.ToDecimal(items[5].Trim());
                                AtomOrbital? orbital = atom.Orbitals.Find(i => i.Position == orbitalpos && i.Symbol == orbitalSymbol);
                                if (orbital != null)
                                {
                                    switch (GetPopulationStatus())
                                    {
                                        case PopulationAnalysisType.neutral:
                                            {
                                                orbital.MullikenPopulation = mulliken;
                                                orbital.LowdinPopulation = lowdin;
                                                break;
                                            }
                                        case PopulationAnalysisType.lewisLUMO:
                                            {
                                                orbital.MullikenPopulationPlus1 = mulliken;
                                                orbital.LowdinPopulationPlus1 = lowdin;
                                                break;
                                            }
                                        case PopulationAnalysisType.lewisHOMO:
                                            {
                                                orbital.MullikenPopulationMinus1 = mulliken;
                                                orbital.LowdinPopulationMinus1 = lowdin;
                                                break;
                                            }
                                    }
                                }
                                else
                                {
                                    switch (GetPopulationStatus())
                                    {
                                        case PopulationAnalysisType.neutral:
                                            {
                                                atom.Orbitals.Add(new AtomOrbital()
                                                {
                                                    Symbol = orbitalSymbol,
                                                    Position = orbitalpos,
                                                    LowdinPopulation = lowdin,
                                                    MullikenPopulation = mulliken
                                                });
                                                break;
                                            }
                                        case PopulationAnalysisType.lewisLUMO:
                                            {
                                                atom.Orbitals.Add(new AtomOrbital()
                                                {
                                                    Symbol = orbitalSymbol,
                                                    Position = orbitalpos,
                                                    LowdinPopulationPlus1 = lowdin,
                                                    MullikenPopulationPlus1 = mulliken
                                                });
                                                break;
                                            }
                                        case PopulationAnalysisType.lewisHOMO:
                                            {
                                                atom.Orbitals.Add(new AtomOrbital()
                                                {
                                                    Symbol = orbitalSymbol,
                                                    Position = orbitalpos,
                                                    LowdinPopulationMinus1 = lowdin,
                                                    MullikenPopulationMinus1 = mulliken
                                                });
                                                break;
                                            }
                                    }
                                }
                            }
                        }


                        if (string.IsNullOrWhiteSpace(line))
                        {
                            break;
                        }
                    }

                }
            }

        }


        private void ParseOverlapPopulations(List<string> input, Molecule molecule)
        {
            if (Start)
            {
                bool startAOPop = false;
                string line = string.Empty;
                Bond[,] bondmatrix = new Bond[molecule.Atoms.Count, molecule.Atoms.Count];
                int blockCount = 0;
                bool startBlockcounting = false;
                for (int c = 0; c < input.Count; ++c)
                {
                    line = input[c];
                    if (line.Contains(GetStartTagOverlapPopulations()))
                    {
                        startAOPop = true;
                        c += 1;
                        continue;
                    }
                    if (startAOPop)
                    {
                        if (line.Contains(GetStartTagPopulations()))
                        {
                            break;
                        }
                        var data = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        if (data.Length > 1)
                        {
                            if (data[1].Contains('.'))
                            {
                                startBlockcounting = true;
                                for (int pos = 1; pos < data.Length; ++pos)
                                {

                                    switch (GetPopulationStatus())
                                    {
                                        case PopulationAnalysisType.neutral:
                                            {
                                                bondmatrix[int.Parse(data[0]) - 1, blockCount * 5 + pos - 1] = new Bond()
                                                {
                                                    Atom1Position = int.Parse(data[0]),
                                                    Atom2Position = blockCount * 5 + pos,
                                                    OverlapPopulation = StringConversion.ToDecimal(data[pos].Trim())
                                                };
                                                break;
                                            }
                                        case PopulationAnalysisType.lewisLUMO:
                                            {
                                                bondmatrix[int.Parse(data[0]) - 1, blockCount * 5 + pos - 1] = new Bond()
                                                {
                                                    Atom1Position = int.Parse(data[0]),
                                                    Atom2Position = blockCount * 5 + pos,
                                                    OverlapPopulationPlus1 = StringConversion.ToDecimal(data[pos].Trim())
                                                };


                                                break;
                                            }
                                        case PopulationAnalysisType.lewisHOMO:
                                            {
                                                bondmatrix[int.Parse(data[0]) - 1, blockCount * 5 + pos - 1] = new Bond()
                                                {
                                                    Atom1Position = int.Parse(data[0]),
                                                    Atom2Position = blockCount * 5 + pos,
                                                    OverlapPopulationMinus1 = StringConversion.ToDecimal(data[pos].Trim())
                                                };
                                                break;
                                            }
                                    }
                                }
                            }
                        }
                        if (startBlockcounting && string.IsNullOrWhiteSpace(line))
                        {
                            ++blockCount;
                            startBlockcounting = false;
                        }
                    }
                }

                foreach (var bond in bondmatrix)
                {
                    if (bond != null && bond.Atom1Position != bond.Atom2Position)
                    {

                        var found = molecule.Bonds.Find(i => i.Atom1Position == bond.Atom1Position && i.Atom2Position == bond.Atom2Position
                                                             ||
                                                             i.Atom1Position == bond.Atom2Position && i.Atom2Position == bond.Atom1Position);

                        if (found != null)
                        {
                            if (GetPopulationStatus() == PopulationAnalysisType.neutral)
                            {
                                found.OverlapPopulation = bond.OverlapPopulation;
                            }
                            if (GetPopulationStatus() == PopulationAnalysisType.lewisLUMO)
                            {
                                found.OverlapPopulationPlus1 = bond.OverlapPopulationPlus1;
                            }
                            if (GetPopulationStatus() == PopulationAnalysisType.lewisHOMO)
                            {
                                found.OverlapPopulationMinus1 = bond.OverlapPopulationMinus1;
                            }
                        }
                        else
                        {
                            molecule.Bonds.Add(bond);
                        }
                    }
                }

                foreach (var bond in molecule.Bonds)
                {
                    var atom = molecule.Atoms.Find(i => i.Position == bond.Atom1Position);
                    if (atom != null)
                    {
                        var atombond = atom.Bonds.Find(i => i.Atom1Position == bond.Atom1Position && i.Atom2Position == bond.Atom2Position);
                        if (atombond != null)
                        {
                            atom.Bonds.Remove(atombond);
                        }
                        atom.Bonds.Add(bond);
                    }
                }
            }
        }


        private void ParseBondOrder(List<string> input, Molecule molecule)
        {
            if (Start)
            {
                bool startPop = false;
                string line = string.Empty;
                for (int c = 0; c < input.Count; ++c)
                {
                    line = input[c];
                    if (line.Contains(GetStartTagBondOrder()))
                    {
                        startPop = true;
                        c += 4;
                        continue;
                    }


                    if (startPop)
                    {

                        if (string.IsNullOrWhiteSpace(line))
                        {
                            break;
                        }

                        var result = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                        for (int outerc = 0; outerc < result.Length / 4; ++outerc)
                        {
                            int atompos1 = int.Parse(result[outerc * 4]);
                            int atompos2 = int.Parse(result[1 + outerc * 4]);
                            decimal dist = StringConversion.ToDecimal(result[2 + outerc * 4].Trim());
                            decimal bondorder = StringConversion.ToDecimal(result[3 + outerc * 4].Trim());

                            switch (GetPopulationStatus())
                            {
                                case PopulationAnalysisType.neutral:
                                    {
                                        var b = molecule.Bonds.Find(i => i.Atom1Position == atompos1 && i.Atom2Position == atompos2 || i.Atom1Position == atompos2 && i.Atom2Position == atompos1);
                                        if (b != null)
                                        {
                                            b.BondOrder = bondorder;
                                            b.Distance = dist;
                                        }
                                        break;
                                    }
                                case PopulationAnalysisType.lewisLUMO:
                                    {
                                        var b = molecule.Bonds.Find(i => i.Atom1Position == atompos1 && i.Atom2Position == atompos2 || i.Atom1Position == atompos2 && i.Atom2Position == atompos1);
                                        if (b != null)
                                        {
                                            b.BondOrderPlus1 = bondorder;
                                            b.Distance = dist;
                                        }
                                        break;
                                    }
                                case PopulationAnalysisType.lewisHOMO:
                                    {
                                        var b = molecule.Bonds.Find(i => i.Atom1Position == atompos1 && i.Atom2Position == atompos2 || i.Atom1Position == atompos2 && i.Atom2Position == atompos1);
                                        if (b != null)
                                        {
                                            b.BondOrderMinus1 = bondorder;
                                            b.Distance = dist;
                                        }
                                        break;
                                    }
                            }
                        }
                    }
                }
            }
        }


        private void ParsePopulations(List<string> input, Molecule molecule)
        {
            if (Start)
            {
                bool startPop = false;
                string line = string.Empty;
                for (int c = 0; c < input.Count; ++c)
                {
                    line = input[c];
                    if (line.Contains(GetStartTagPopulations()))
                    {
                        startPop = true;
                        continue;
                    }

                    if (startPop)
                    {
                        var result = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        if (result.Length > 5)
                        {
                            int position = int.Parse(result[0].Trim());
                            string symbol = result[1].Trim();
                            decimal mullpop = StringConversion.ToDecimal(result[2].Trim());
                            decimal lowdinpop = StringConversion.ToDecimal(result[4].Trim());


                            switch (GetPopulationStatus())
                            {
                                case PopulationAnalysisType.neutral:
                                    {
                                        var atom = molecule.Atoms.Find(i => i.Position == position && i.Symbol == symbol);
                                        if (atom != null)
                                        {
                                            atom.MullikenPopulation = mullpop;
                                            atom.LowdinPopulation = lowdinpop;
                                        }
                                        break;
                                    }
                                case PopulationAnalysisType.lewisLUMO:
                                    {
                                        var atom = molecule.Atoms.Find(i => i.Position == position && i.Symbol == symbol);
                                        if (atom != null)
                                        {
                                            atom.MullikenPopulationPlus1 = mullpop;
                                            atom.LowdinPopulationPlus1 = lowdinpop;
                                        }
                                        break;
                                    }
                                case PopulationAnalysisType.lewisHOMO:
                                    {
                                        var atom = molecule.Atoms.Find(i => i.Position == position && i.Symbol == symbol);
                                        if (atom != null)
                                        {
                                            atom.MullikenPopulationMinus1 = mullpop;
                                            atom.LowdinPopulationMinus1 = lowdinpop;
                                        }
                                        break;
                                    }
                            }
                        }
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            break;
                        }
                    }

                }

            }

        }


        #endregion
    }
}
