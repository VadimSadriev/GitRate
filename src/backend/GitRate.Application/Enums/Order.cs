using System.Runtime.Serialization;

namespace GitRate.Application.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    public enum Order
    {
        /// <summary>
        /// Asc ordering
        /// </summary>
        [EnumMember(Value = "asc")]
        Asc = 0,

        /// <summary>
        /// Descend ordering
        /// </summary>
        [EnumMember(Value = "desc")]
        Desc = 1
    }
}
