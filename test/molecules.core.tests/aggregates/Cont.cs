using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace molecules.core.tests.aggregates
{

    public abstract class Tracker
    {

        public Dictionary<string, HashSet<int>> hosts;

        public Tracker()
        {
            hosts = new Dictionary<string, HashSet<int>>();
        }

        public abstract string Allocate(string hostType);

        public abstract string Deallocate(string hostName);
    }





}
