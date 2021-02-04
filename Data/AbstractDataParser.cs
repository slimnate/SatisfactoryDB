using Newtonsoft.Json.Linq;
using SatisfactoryDB.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryDB.Data
{
    public abstract class AbstractDataParser<T>
    {
        public string NativeClass;

        public JArray RawData;

        public List<T> Data;

        public AbstractDataParser(JObject dataObject)
        {
            this.NativeClass = dataObject.Value<string>("NativeClass");
            this.RawData = dataObject.Value<JArray>("Classes");
            this.Data = new List<T>();
        }


        public abstract List<T> Parse();

        // SHARED PROPERTY PARSING FROM OBJECT - prevent duplication in separate parsers.
        protected string getClassName(JObject obj)
        {
            return obj.Value<string>("ClassName");
        }

        protected string getName(JObject obj)
        {
            return obj.Value<string>("mDisplayName");
        }

        protected string getDescription(JObject obj)
        {
            return obj.Value<string>("mDescription");
        }

        protected int getStackSize(JObject obj)
        {
            string ss = obj.Value<string>("mStackSize");
            return Item.STACK_SIZE[ss];
        }

        protected int getResourceSinkPoints(JObject obj)
        {
            return obj.Value<int>("mResourceSinkPoints");
        }

        protected float getEnergy(JObject obj)
        {
            return obj.Value<float>("mEnergyValue");
        }

        protected float getRadioactiveDecay(JObject obj)
        {
            return obj.Value<float>("mRadioactiveDecay");
        }

        protected MatterState getForm(JObject obj)
        {
            return Item.MatterStateFromString(obj.Value<string>("mForm"));
        }
    }
}
