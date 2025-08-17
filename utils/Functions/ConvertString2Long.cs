namespace Restaurant.utils
{
    public class ConvertString2Long
    {
        public long ConvertId2Long(string Id)
        {
            long UserId=0;
            for(int i=Id.Length-1 ; i>=0 ; i--)
            {
                long d=1;
                for(int j=0 ; j<Id.Length-i-1 ; j++)
                {
                    d*=10;
                }
                UserId += (Id[i] - '0')*d;
            }
            return UserId;
        }
    }
}