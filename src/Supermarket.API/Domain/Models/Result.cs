using System;
using System.Runtime.Serialization;
using Supermarket.API.Models;

namespace Supermarket.API.Domain.Models
{
    [Serializable]
    [DataContract]
    public class Result : ResultBase
    {
       public string token { get; set; }
    }
}