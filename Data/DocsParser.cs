using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SatisfactoryDB.BO;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryDB.Data
{
    class DocsParser
    {
        //path to Docs.json file where all data comes from
        public const string DOC_FILE_PATH = "G:\\Games\\SteamLibrary\\steamapps\\common\\Satisfactory\\CommunityResources\\Docs\\Docs.json";
        
        //base class names for stuff we care about 
        public const string ITEMS = "Class'/Script/FactoryGame.FGItemDescriptor'";
        public const string RESOURCES = "Class'/Script/FactoryGame.FGResourceDescriptor'";
        public const string BIOMASS = "Class'/Script/FactoryGame.FGItemDescriptorBiomass'";

        public const string RECIPES = "Class'/Script/FactoryGame.FGRecipe'";

        //Full parsed JSON data
        public static JArray JsonRoot = null;

        /// <summary>
        /// Parses all the data in the Docs.json file into a SQLite database
        /// </summary>
        public static void Parse()
        {
            Console.WriteLine("Starting parsing of Docs.json...");

            //read in data if not already done.
            InitJsonRoot();

            //Set up database
            

            //init data lists
            List<Item> items = new List<Item>();
            List<Recipe> recipes = new List<Recipe>();

            //parse classes in order
            items = new ItemParser(findBaseClassFor(ITEMS)).Parse(); // ITEMS
            items.AddRange(new BiomassParser(findBaseClassFor(BIOMASS)).Parse()); //BIOMASS
            items.AddRange(new ResourceParser(findBaseClassFor(RESOURCES)).Parse()); //RESOURCES

            recipes = new RecipeParser(findBaseClassFor(RECIPES), items).Parse();

            Console.WriteLine("Completed.");
        }

        /// <summary>
        /// If JsonRoot is null, read in the file contents and parse the data.
        /// </summary>
        private static void InitJsonRoot()
        {
            if(JsonRoot == null)
            {
                //read in text from Docs.json file
                string content = File.ReadAllText(DOC_FILE_PATH);

                //deserialize json array
                JsonRoot = (JArray)JsonConvert.DeserializeObject(content); //deserialize as array
            }
        }

        public static void PrintInfo()
        {
            //Loop through first level list (base classes, each containing a list of subclasses)
            foreach (JObject baseClass in JsonRoot)
            {
                //get base class name. Determines what we're currently parsing (item, recipe, etc.)
                Console.WriteLine("====================================================================");
                Console.WriteLine("Base Class: " + baseClass.Value<string>(""));

                //get subclasses. Each corresponds with an object (item, building, etc.) in game
                JArray subclasses = baseClass.Value<JArray>("Classes");
                printDeets(subclasses); //uncomment to print a pretty list of deets
            }
        }

        private static JObject findBaseClassFor(string nativeClass)
        {
            foreach (JObject obj in JsonRoot)
            {
                string className = obj.Value<string>("NativeClass");
                if(className == nativeClass)
                {
                    return obj;
                }
            }
            throw new Exception("No entry in root for class: " + nativeClass);
        }

        private static void printDeets(JArray subclasses)
        {

            //get subclass names and props
            List<string> subclassNames = getClassNames(subclasses);
            List<string> properties = getShape((JObject)subclasses.First());

            //print out subclass names and property names
            Console.WriteLine(subclassNames.Count + " Classes");
            Console.Write(ListPrint(subclassNames));
            Console.WriteLine("Properties: ");
            Console.Write(ListPrint(properties, "+ "));
        }

        //
        private static List<string> getShape(JObject obj)
        {
            List<string> values = new List<string>();

            foreach(JProperty prop in obj.Properties())
            {
                if (prop.Name.Equals("ClassName"))
                {
                    continue;
                }

                values.Add(prop.Name);
            }

            return values;
        }

        private static List<string> getClassNames(JArray arr)
        {
            List<string> values = new List<string>();

            foreach (JObject obj in arr)
            {
                values.Add(obj.Value<string>("ClassName"));
            }

            return values;
        }

        private static string ListPrint(List<string> list, string bullet="- ")
        {
            string str = "";

            foreach(string s in list)
            {
                str += bullet + s + "\r\n";
            }

            return str;
        }

        //end class
    }

    // end namespace
}
