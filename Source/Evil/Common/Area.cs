namespace Evil.Common
{
    public class Area : Entity
    {
        public virtual string Name { get; set; }
        public virtual Position UpperLeft { get; set; }
        public virtual Position LowerRight { get; set; }
    }
}