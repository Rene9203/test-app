using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TestApp.Core.Entities;

namespace TestApp.Application.Commands
{
    [DataContract]
    public class CreateOfferCommand : IRequest<Offer>
    {
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string OfferTypeId { get; set; }
        
        [DataMember]
        public string EmployerId { get; set; }
    }
}
