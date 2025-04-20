using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Server.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AddressType
{
    [EnumMember(Value = "billing")] Billing,

    [EnumMember(Value = "shipping")] Shipping
}