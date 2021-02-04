using Newtonsoft.Json.Linq;
using SatisfactoryDB.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryDB.Data
{
    class BiomassParser : AbstractDataParser<Item>
    {
        public BiomassParser(JObject dataObject) : base(dataObject)
        {
            
        }

        /// <summary>
        /// Parse biomass
        /// </summary>
        /// <returns></returns>
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
                MatterState form = getForm(obj);
                float energy = getEnergy(obj);
                float radioactiveDecay = getRadioactiveDecay(obj);

                //create item and add to list
                Item item = new Item(className, name, description, stackSize, resourceSink, energy, radioactiveDecay, form);
                Data.Add(item);

                Console.WriteLine("BiomassItem created: " + item.Name);
            }

            return Data;
        }
    }
}
