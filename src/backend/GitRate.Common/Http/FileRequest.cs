using System.IO;

namespace GitRate.Common.Http
{
    /// <summary>
    /// Contains data about file to write to request form
    /// </summary>
    public class FileRequest
    {
        /// <summary>
        /// File name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Name for a content body part name
        /// </summary>
        public string ContentBodyPartName { get; set; } = "file";

        /// <summary>
        /// Type of this file
        /// </summary>
        public string ContentType { get; set; } = HttpConstants.MimeTypes.Octet;

        /// <summary>
        /// File stream
        /// </summary>
        public Stream FileStream { get; set; }
    }
}
