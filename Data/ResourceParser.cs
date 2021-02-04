using Newtonsoft.Json.Linq;
using SatisfactoryDB.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryDB.Data
{
    class ResourceParser : AbstractDataParser<Resource>
    {
        public ResourceParser(JObject dataObject) : base(dataObject)
        {

        }

        public override List<Resource> Parse()
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
                float collectMultiplier = getCollectMultiplier(obj);

                //create item and add to list
                Resource item = new Resource(className, name, description, stackSize, resourceSink, form, collectMultiplier, energy, radioactiveDecay);
                Data.Add(item);

                Console.WriteLine("BiomassItem created: " + item.Name);
            }

            return Data;
        }

        private float getCollectMultiplier(JObject obj)
        {
            return obj.Value<float>("mCollectSpeedMultiplier");
        }
    }
}
