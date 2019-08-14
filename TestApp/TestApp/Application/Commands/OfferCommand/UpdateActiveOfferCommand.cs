using MediatR;
using System.Runtime.Serialization;

namespace TestApp.Application.Commands
{
    [DataContract]
    public class UpdateActiveOfferCommand : IRequest<bool>
    {
        public string OfferId { get; set; }

        public string EmployerId { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
