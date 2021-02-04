using Newtonsoft.Json.Linq;
using SatisfactoryDB.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryDB.Data
{
    class ItemParser : AbstractDataParser<Item>
    {
        /// <summary>
        /// Create ItemParser
        /// </summary>
        /// <param name="baseClass">base class name</param>
        /// <param name="data">JArray of data</param>
        public ItemParser(JObject dataObject) : base(dataObject)
        {

        }

        public override List<Item> Parse()
        {
            //loop through data
            foreach (JObject obj in this.RawData)
            {
                //parse fields
                string className = getClassName(obj);
                string name = getName(obj);
                string description = getDescription(obj);
                int stackSize = getStackSize(obj);
                int resourceSink = getResourceSinkPoints(obj);
                float energy = getEnergy(obj);
                float radioactiveDecay = getRadioactiveDecay(obj);

                //create item and add to list
                Item item = new Item(className, name, description, stackSize, resourceSink, energy, radioactiveDecay);
                Data.Add(item);

                Console.WriteLine("Item created: " + item.Name);
            }

            return Data;
        }
    }
}
