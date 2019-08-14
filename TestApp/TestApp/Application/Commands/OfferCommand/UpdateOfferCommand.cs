using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TestApp.Application.Commands
{
    [DataContract]
    public class UpdateOfferCommand : IRequest<bool>
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string OfferTypeId { get; set; }
    }
}
