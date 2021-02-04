using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryDB.BO
{
    class Category
    {
        public string Name;
        public Category Parent;
        public List<Category> Children;
        public List<Item> Items;

        /// <summary>
        /// Create new category with name and parent.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        public Category(string name, Category parent)
        {
            this.Name = name;
            this.Parent = parent;
            this.Children = new List<Category>();
            this.Items = new List<Item>();
        }

        /// <summary>
        /// Find child category by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Category GetChild(string name)
        {
            Category child = null;
            foreach(Category c in Children)
            {
                if(c.Name == name)
                {
                    return c;
                }
            }
            return child;
        }

        public void AddChild(Category c)
        {
            Children.Add(c);
        }

        /// <summary>
        /// Associate an item with this category
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public override string ToString()
        {
            return Name + " ( " + Children.Count + " )";
        }
    }
}
