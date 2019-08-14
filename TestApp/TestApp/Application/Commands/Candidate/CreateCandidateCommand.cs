using MediatR;
using System.Runtime.Serialization;
using TestApp.Core.Entities;

namespace TestApp.Application.Commands
{
    [DataContract]
    public class CreateCandidateCommand : IRequest<bool>
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }

        public string OfferId { get; set; }
    }
}
