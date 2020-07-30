using System;
using System.Runtime.Serialization;
using Supermarket.API.Models;

namespace Supermarket.API.Domain.Models
{
    [Serializable]
    [DataContract]
    public class ResultBase 
    {
        [DataMember(Order = 1)]
        public string Status { get; set; }
        [DataMember(Order = 2)]
        public DateTime StartTime { get; set; }
        [DataMember(Order = 3)]
        public DateTime EndTime { get; set; }
    }
}