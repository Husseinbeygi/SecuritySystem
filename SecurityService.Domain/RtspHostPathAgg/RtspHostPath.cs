using _0_Framework.Domain;

namespace SecuritySystem.Domain.RtspHostPathAgg
{
    public class RtspHostPath : EntityBase
    {
        public RtspHostPath(string address)
        {
            Address=address;
        }

        public string Address { get; set; }

    }
}
