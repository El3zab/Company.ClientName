
namespace Company.ClientName.PL.Services
{
    public class TransiantService : ITransiantService
    {
        public TransiantService()
        {
            Guid = Guid.NewGuid();
        }

        public Guid Guid { get; set; }

        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
