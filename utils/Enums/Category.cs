namespace Restaurant
{
    public class Category
    {
        public string Categories(int index)
        {
            if(index is 1)
                return "Main courses";
            else if(index is 2)
                return "Appetizers";
            else if(index is 3)
                return "Beverages";
             else if(index is 4)
                return "Desserts";
            else if(index is 5)
                return "Snacks";
            else
                return "";
        }
    }
}