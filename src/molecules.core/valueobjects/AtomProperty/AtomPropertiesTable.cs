namespace molecules.core.valueobjects.AtomProperty
{
    public static class AtomPropertiesTable
    {
        public static List<AtomProperties> atomProperties = new List<AtomProperties>()
        {
            new AtomProperties()
            {
                Id = 1,
                Symbol = "H",
                Name = "Hydrogen",
                AtomNumber = 1
            },
            new AtomProperties()
            {
                Id = 2,
                Symbol = "He",
                Name = "Helium",
                AtomNumber = 2
            },
            new AtomProperties()
            {
                Id = 3,
                Symbol = "Li",
                Name = "Lithium",
                AtomNumber = 3
            },
            new AtomProperties()
            {
                Id = 4,
                Symbol = "Be",
                Name = "Beryllium",
                AtomNumber = 4
            },
            new AtomProperties()
            {
                Id = 5,
                Symbol = "B",
                Name = "Boron",
                AtomNumber = 5
            },
            new AtomProperties()
            {
                Id = 6,
                Symbol = "C",
                Name = "Carbon",
                AtomNumber = 6
            },
            new AtomProperties()
            {
                Id = 7,
                Symbol = "N",
                Name = "Nitrogen",
                AtomNumber = 7
            },
            new AtomProperties()
            {
                Id = 8,
                Symbol = "O",
                Name = "Oxygen",
                AtomNumber = 8
            },
            new AtomProperties()
            {
                Id = 9,
                Symbol = "F",
                Name = "Fluorine",
                AtomNumber = 9
            },
            new AtomProperties()
            {
                Id = 10,
                Symbol = "Ne",
                Name = "Neon",
                AtomNumber = 10
            },
            new AtomProperties()
            {
                Id = 11,
                Symbol = "Na",
                Name = "Sodium",
                AtomNumber = 11
            },
            new AtomProperties()
            {
                Id = 12,
                Symbol = "Mg",
                Name = "Magnesium",
                AtomNumber = 12
            },
            new AtomProperties()
            {
                Id = 13,
                Symbol = "Al",
                Name = "Aluminium",
                AtomNumber = 13
            },
            new AtomProperties()
            {
                Id = 14,
                Symbol = "Si",
                Name = "Silicon",
                AtomNumber = 14
            },
            new AtomProperties()
            {
                Id = 15,
                Symbol = "P",
                Name = "Phosphorus",
                AtomNumber = 15
            },
            new AtomProperties()
            {
                Id = 16,
                Symbol = "S",
                Name = "Sulfur",
                AtomNumber = 16
            },
            new AtomProperties()
            {
                Id = 17,
                Symbol = "Cl",
                Name = "Chlorine",
                AtomNumber = 17
            },
            new AtomProperties()
            {
                Id = 18,
                Symbol = "Ar",
                Name = "Argon",
                AtomNumber = 18
            },
            new AtomProperties()
            {
                Id = 19,
                Symbol = "K",
                Name = "Potassium",
                AtomNumber = 19
            },
            new AtomProperties()
            {
                Id = 20,
                Symbol = "Ca",
                Name = "Calcium",
                AtomNumber = 20
            },
            new AtomProperties()
            {
                Id = 21,
                Symbol = "Fe",
                Name = "Iron",
                AtomNumber = 21
            }
        };



        public static AtomProperties? GetAtomProperties(string symbol)
        {
            return atomProperties.FirstOrDefault(x => x.Symbol == symbol);
        }
    }
}
