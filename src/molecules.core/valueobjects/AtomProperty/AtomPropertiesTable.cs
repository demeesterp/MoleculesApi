using System.Collections.ObjectModel;

namespace molecules.core.valueobjects.AtomProperty
{
    public static class AtomPropertiesTable
    {
        private static readonly ReadOnlyCollection<AtomProperties> _atomProperties =
            new ReadOnlyCollection<AtomProperties>(new AtomProperties[] {
                   new AtomProperties(1,"H","Hydrogen",1),
                   new AtomProperties(2,"He","Helium",2),
                   new AtomProperties(3,"Li", "Lithium", 3),
                   new AtomProperties(4,"Be","Beryllium",4),
                   new AtomProperties(5,"B", "Boron",5),
                   new AtomProperties(6,"C","Carbon",6),
                   new AtomProperties(7,"N", "Nitrogen",7),
                   new AtomProperties(8,"O", "Oxygen",8),
                   new AtomProperties(9,"F", "Fluorine",9),
                   new AtomProperties(10,"Ne","Neon",10),
                   new AtomProperties(11,"Na","Sodium",11),
                   new AtomProperties(12,"Mg","Magnesium",12),
                   new AtomProperties(13,"Al","Aluminium",13),
                   new AtomProperties(14,"Si","Silicon",14),
                   new AtomProperties(15,"P","Phosphorus",15),
                   new AtomProperties(16,"S", "Sulfur",16),
                   new AtomProperties(17,"Cl", "Chlorine", 17),
                   new AtomProperties(18,"Ar", "Argon", 18),
                   new AtomProperties(19,"K", "Potassium",19),
                   new AtomProperties(20,"Ca", "Calcium", 20),
                   new AtomProperties(21,"Fe","Iron",21)
            });

        public static AtomProperties? GetAtomProperties(string symbol)
        {
            return _atomProperties.FirstOrDefault(x => x.Symbol == symbol);
        }
    }
}
