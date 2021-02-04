using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryDB.BO
{
    class Resource : Item
    {
        public MatterState Form;
        public float CollectSpeedMultiplier;

        /// <summary>
        /// Creates a new Resource with the specified properties.
        /// </summary>
        /// <param name="className">Class name from data</param>
        /// <param name="name">Item name</param>
        /// <param name="description">Item description</param>
        /// <param name="stackSize">Item stack size</param>
        /// <param name="form">State of matter</param>
        /// <param name="energy">Energy value (defaults to <code>0.0f</code>)</param>
        /// <param name="radioactiveDecay">Radioactive decay value (defaults to <code>0.0f</code>)</param>
        public Resource(string className, string name, string description, int stackSize, int resourceSinkPoints,
                MatterState form, float collectMultiplier, float energy = 0.0f, float radioactiveDecay = 0.0f)
            : base(className, name, description, stackSize, resourceSinkPoints, energy, radioactiveDecay)
        {
            this.Form = form;
            this.CollectSpeedMultiplier = collectMultiplier;
        }

        public override string ToString()
        {
            return base.ToString() + " f:" + Form;
        }
    }
}
