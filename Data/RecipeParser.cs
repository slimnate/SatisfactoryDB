using Newtonsoft.Json.Linq;
using SatisfactoryDB.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace SatisfactoryDB.Data
{
    class RecipeParser : AbstractDataParser<Recipe>
    {
        private List<Item> Items;

        /// <summary>
        /// Creates a new RecipeParser
        /// </summary>
        /// <param name="baseClass">base class from the parent object</param>
        /// <param name="data">JArray containing the subclasses of the base class</param>
        public RecipeParser(JObject dataObject, List<Item> items) : base(dataObject)
        {
            this.Items = items;
        }

        /// <summary>
        /// Parses the data supplied on instantiation and turns it into some objects we can use.
        /// </summary>
        /// <returns></returns>
        public override List<Recipe> Parse()
        {
            List<string> sourceList = new List<string>();
            List<string> recipes = new List<string>();

            //loop through data
            foreach (JObject obj in this.RawData)
            {
                string className = getClassName(obj);
                string fullName = getFullName(obj);
                string name = getName(obj);
                string ingredientString = obj.Value<string>("mIngredients");
                string productString = obj.Value<string>("mProduct");
                string producedInString = obj.Value<string>("mProducedIn");
                string related = obj.Value<string>("mRelevantEvents");

                float duration = getDuration(obj);
                float manualMultiplier = getManualMultiplier(obj);

                //category and dot name
                string[] catAndName = categoryAndClassFromFullName(fullName);
                string categoryPath = catAndName[0]; 
                string dotName = catAndName[1];
                Category category = CategoryManager.FindOrAddFromPath(categoryPath);

                //Create recipe
                Recipe current = new Recipe(className, dotName, name, duration, manualMultiplier, category);

                Match sourceMatches = Regex.Match(producedInString, @"\((.*)\)");
                string[] sources = sourceMatches.Groups[1].Value.Split(',');

                foreach(string source in sources)
                {
                    if(source == "")
                        continue;

                    if (!sourceList.Contains(source))
                    {
                        sourceList.Add(source);
                    }
                }

                Data.Add(current);
            }
            //end loop

            return Data;
        }

        private string getFullName(JObject obj)
        {
            return obj.Value<string>("FullName");
        }

        private float getDuration(JObject obj)
        {
            return obj.Value<float>("mManufactoringDuration");
        }

        private float getManualMultiplier(JObject obj)
        {
            return obj.Value<float>("mManualManufacturingMultiplier");
        }

        private string[] categoryAndClassFromFullName(string fullName)
        {
            Match m = Regex.Match(fullName, @"\/(.*)\/(.*)$");
            return new string[] { m.Groups[1].Value, m.Groups[2].Value }; // 1 and 2 because firt match is the full string
        }
    }
}
