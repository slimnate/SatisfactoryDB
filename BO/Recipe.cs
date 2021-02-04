using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryDB.BO
{
    class Recipe
    {
        public Category Category;

        public string ClassName;
        public string SimpleClassName;
        public string DotName; //form of Recipe_Item.Recipe_Item_C
        public string Name;

        public float Duration;
        public float ManualMultiplier;

        public List<RecipeItem> Ingredients = new List<RecipeItem>();
        public List<RecipeItem> Products = new List<RecipeItem>();

        //TODO: mProducedIn

        public Recipe(string className, string dotName, string name, float duration, float manualMultiplier, Category category)
        {
            //simple name
            this.ClassName = className;
            this.SimpleClassName = className.Replace("Recipe_", "").Replace("_C", "");
            this.DotName = dotName;
            this.Duration = duration;
            this.ManualMultiplier = manualMultiplier;
            this.Category = category;
        }

        public override string ToString()
        {
            return ClassName + " (" + Duration + "s, x" + ManualMultiplier + ")" + " - " + Category;
        }
    }

    class RecipeItem
    {
        public Item Item;
        public int Amount;
    }
}
