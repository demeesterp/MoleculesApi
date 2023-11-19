namespace molecules.core.valueobjects.AtomProperty
{
    public record AtomProperties
    {

        public AtomProperties(int id, string symbol, string name, int atomNumber)
        {
            Id = id;
            Symbol = symbol;
            Name = name;
            AtomNumber = atomNumber;
        }

        public int Id { get; }
        public string Symbol { get; } = string.Empty;
        public string Name { get; } = string.Empty;
        public int AtomNumber { get; }
    }
}
