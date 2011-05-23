using MbUnit.Framework;

namespace Evil.TestHelpers
{
    public class FutureAttribute : IgnoreAttribute
    {
        public FutureAttribute() :base("Future Implemenation")
        {
            
        }
    }
}