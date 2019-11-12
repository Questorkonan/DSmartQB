namespace DSmartQB.CORE.DTOs
{
    public class ReturnMessage
    {
        public int Key { get; set; }
        public string Value { get; set; }
        public string ReturnId { get; set; }
    }

    public class Remove
    {
        public string Id { get; set; }
    }
}
