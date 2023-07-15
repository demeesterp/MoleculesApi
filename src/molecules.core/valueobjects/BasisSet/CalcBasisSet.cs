namespace molecules.core.valueobjects.BasisSet
{
    public class CalcBasisSet
    {
        public CalcBasisSetCode Code { get; }

        public string Name { get; }

        public string GmsInput { get; }

        public CalcBasisSet(CalcBasisSetCode code, string name, string gmsInput)
        {
            Code = code;
            Name = name;
            GmsInput = gmsInput;
        }
    }
}
