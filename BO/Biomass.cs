using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryDB.BO
{

    class Biomass : Item
    {
        public MatterState Form;

        /// <summary>
        /// Creates a new Biomass with the specified properties.
        /// </summary>
        /// <param name="className">Class name from data</param>
        /// <param name="name">Item name</param>
        /// <param name="description">Item description</param>
        /// <param name="stackSize">Item stack size</param>
        /// <param name="form">State of matter</param>
        /// <param name="energy">Energy value (defaults to <code>0.0f</code>)</param>
        /// <param name="radioactiveDecay">Radioactive decay value (defaults to <code>0.0f</code>)</param>
        public Biomass(string className, string name, string description, int stackSize, int resourceSinkPoints,
                MatterState form, float energy = 0.0f, float radioactiveDecay = 0.0f)
            : base(className, name, description, stackSize, resourceSinkPoints, energy, radioactiveDecay)
        {
            this.Form = form;
        }

        public override string ToString()
        {
            return base.ToString() + " f:" + Form;
        }
    }
}
