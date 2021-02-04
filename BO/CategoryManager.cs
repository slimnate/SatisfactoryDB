using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryDB.BO
{
    sealed class CategoryManager
    {
        public static Category Root = null;

        public static Category FindOrAddFromPath(string path)
        {
            //check if we have the root set up yet, if not, do that.
            if (Root == null)
            {
                _setRoot(path);
            }

            //split by '/' and process each path segment, adding to the correct child
            string[] split = path.Split('/');
            Category currParent = Root;

            foreach(string categoryName in split)
            {
                if(categoryName == Root.Name) continue; //skip root

                Category child = currParent.GetChild(categoryName);
                if (child == null)
                {
                    //create, add to children.
                    child = new Category(categoryName, currParent);
                    currParent.AddChild(child);
                }

                currParent = child;
            }

            return currParent;
        }

        private static void _setRoot(string path)
        {
            string[] splitPath = path.Split('/');
            Root = new Category(splitPath[0], null);
        }

    }
}
