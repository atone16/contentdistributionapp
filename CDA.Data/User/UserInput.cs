using CDA.Data.Base;

namespace CDA.Data
{
    public class UserInput : BaseInput
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
