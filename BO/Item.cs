using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryDB.BO
{
    public enum MatterState
    {
        NA, Solid, Liquid
    }

    public class Item
    {
        // Dictionary of stack size string to int conversions (key: string, val: int)
        public static Dictionary<string, int> STACK_SIZE = new Dictionary<string, int>
        {
            { "SS_FLUID", 0 },
            { "SS_ONE", 1 },
            { "SS_SMALL", 50 },
            { "SS_MEDIUM", 100 },
            { "SS_BIG", 200 },
            { "SS_HUGE", 500 },
        };

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        //string values
        public string ClassName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //int values
        public int StackSize { get; set; }
        public int ResourceSinkPoints { get; set; }
        public MatterState State { get; set; }

        //float values
        public float EnergyValue { get; set; }
        public float RadioactiveDecay { get; set; }
        public float CollectSpeedMultiplier { get; set; }

        /// <summary>
        /// Creates a new Item with the specified properties.
        /// </summary>
        /// <param name="className">Class name from data</param>
        /// <param name="name">Item name</param>
        /// <param name="description">Item description</param>
        /// <param name="stackSize">Item stack size</param>
        /// <param name="energy">Energy value</param>
        /// <param name="radioactiveDecay">Radioactive decay value</param>
        /// <param name="state">Radioactive decay value (optional, defaults to 0)</param>
        /// <param name="radioactiveDecay">Radioactive decay value (optional, defaults to 0)</param>
        public Item(string className, string name, string description, int stackSize, int resourceSinkPoints,
            float energy, float radioactiveDecay, MatterState state=MatterState.NA, float collectSpeedMult=0.0f)
        {
            this.ClassName = className;
            this.Name = name;
            this.Description = description;
            this.StackSize = stackSize;
            this.ResourceSinkPoints = resourceSinkPoints;
            this.EnergyValue = energy;
            this.RadioactiveDecay = radioactiveDecay;
            this.State = state;
            this.CollectSpeedMultiplier = collectSpeedMult;
        }

        public override string ToString()
        {
            return base.ToString() + " - " + Name + " s: " + this.StackSize + " rs: " + ResourceSinkPoints + " e: " + EnergyValue + " r: " + RadioactiveDecay;
        }

        public static MatterState MatterStateFromString(string s)
        {
            if(s == "RF_LIQUID")
            {
                return MatterState.Liquid;
            }
            return MatterState.Solid;
        }

    }
}
