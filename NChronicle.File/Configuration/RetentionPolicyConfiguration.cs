using System;
using System.Xml;
using System.Xml.Schema;

namespace NChronicle.File.Configuration {

    /// <summary>
    /// Container for <see cref="RetentionPolicy"/> configuration.
    /// </summary>
    public class RetentionPolicyConfiguration {

        internal TimeSpan? AgeLimit = new TimeSpan(1, 0, 0, 0);

        internal long FileSizeLimit = 104857600; // 100 MB
        internal long RetentionLimit = 20;

        /// <summary>
        /// Set the age limit for the output file before it will 
        /// be archived. The default age limit is 1 day. 
        /// </summary>
        /// <param name="timeSpan">The maximum age for the output file as a <see cref="TimeSpan"/>.</param>
        public void WithAgeLimit (TimeSpan timeSpan) {
            if (timeSpan < TimeSpan.FromMinutes(1)) 
                throw new ArgumentException($"Specified {nameof(timeSpan)} is less than the minimum of 1 minute.");
            this.AgeLimit = timeSpan;
        }

        /// <summary>
        /// Remove the age limit for the output file so as not to
        /// archive it - regardless of it's age - unless it extends 
        /// over the set file size limit. 
        /// </summary>
        public void WithNoAgeLimit () => this.AgeLimit = null;

        /// <summary>
        /// Set the file size limit for the output file before it
        /// will be archived. The file size limit must be above 50KB. 
        /// The default file size limit is 100MB;.
        /// </summary>
        /// <param name="bytes">The maximum file size for the output file in Kilobytes.</param>
        public void WithFileSizeLimitInKilobytes (long bytes) => this.WithFileSizeLimitInBytes(bytes*1024);

        /// <summary>
        /// Set the file size limit for the output file before it
        /// will be archived. The file size limit must be above 50KB. 
        /// The default file size limit is 100MB;.
        /// </summary>
        /// <param name="bytes">The maximum file size for the output file in Bytes.</param>
        public void WithFileSizeLimitInBytes (long bytes) {
            if (bytes < 50*1024) throw new ArgumentException("File size limit must be 50 or more kilobytes.");
            this.FileSizeLimit = bytes;
        }

        /// <summary>
        /// Remove the file size limit for the output file so as not
        /// to archive it - regardless of it's file size - unless it
        /// extends over the set age limit.
        /// </summary>
        public void WithNoFileSizeLimit () {
            this.FileSizeLimit = 0;
        }

        /// <summary>
        /// Set the file size limit for the output file before it
        /// will be archived. The file size limit must be above 50KB. 
        /// The default file size limit is 100MB;.
        /// </summary>
        /// <param name="bytes">The maximum file size for the output file in Megabytes.</param>
        public void WithFileSizeLimitInMegabytes (long bytes) => this.WithFileSizeLimitInBytes(bytes*1024*1024);

        /// <summary>
        /// Remove the retention limit so as not to delete
        /// any archived log regardless of the quantity. 
        /// </summary>
        public void WithNoRetentionLimit () => this.RetentionLimit = 0;

        /// <summary>
        /// Set a retention <paramref name="limit"/>, defining how many of 
        /// the newest archived logs are kept, the elder archived logs are deleted. 
        /// the default retention limit is 20.
        /// </summary>
        /// <param name="limit">The maximum number of archived log files to keep.</param>
        public void WithRetentionLimit (long limit) => this.RetentionLimit = limit;


        /// <summary>
        /// Required for XML serialization, this method offers no functionality.
        /// </summary>
        /// <returns>A null <see cref="XmlSchema"/>.</returns>
        public XmlSchema GetSchema() => null;

        /// <summary>
        /// Populate configuration from XML via the specified <see cref="XmlReader" />.
        /// </summary>
        /// <param name="reader"><see cref="XmlReader" /> stream from the configuration file.</param>
        /// <seealso cref="Core.NChronicle.ConfigureFrom(string, bool, int)"/>
        public void ReadXml(XmlReader reader) {
            while (reader.Read()) {
                if (reader.NodeType == XmlNodeType.Element) {
                    switch (reader.Name) {
                        case nameof(this.AgeLimit):
                            if (reader.IsEmptyElement) break;
                            var agelimitStr = reader.ReadElementContentAsString();
                            if (string.IsNullOrWhiteSpace(agelimitStr))
                                throw new XmlException($"Unexpected library configuration for {nameof(FileChronicleLibrary)}, empty {nameof(this.AgeLimit)}.");
                            TimeSpan ageLimit;
                            if (!TimeSpan.TryParse(agelimitStr, out ageLimit))
                                throw new XmlException($"Unexpected library configuration for {nameof(FileChronicleLibrary)}, value '{agelimitStr}' for {nameof(this.AgeLimit)} is not a valid {nameof(TimeSpan)}.");
                            if (ageLimit <= TimeSpan.Zero)
                                this.WithNoAgeLimit();
                            else 
                                this.WithAgeLimit(ageLimit);
                            break;
                        case nameof(this.FileSizeLimit):
                            if (reader.IsEmptyElement) break;
                            var fileSizeLimitStr = reader.ReadElementContentAsString();
                            if (string.IsNullOrWhiteSpace(fileSizeLimitStr))
                                throw new XmlException($"Unexpected library configuration for {nameof(FileChronicleLibrary)}, empty {nameof(this.FileSizeLimit)}.");
                            Int64 fileSizeLimit;
                            if (!Int64.TryParse(fileSizeLimitStr, out fileSizeLimit))
                                throw new XmlException($"Unexpected library configuration for {nameof(FileChronicleLibrary)}, value '{fileSizeLimitStr}' for {nameof(this.FileSizeLimit)} is not a valid {nameof(Int64)}.");
                            if (fileSizeLimit <= 0) 
                                this.WithNoFileSizeLimit();
                            else
                                this.WithFileSizeLimitInBytes(fileSizeLimit);
                            break;
                        case nameof(this.RetentionLimit):
                            if (reader.IsEmptyElement) break;
                            var retentionLimitStr = reader.ReadElementContentAsString();
                            if (string.IsNullOrWhiteSpace(retentionLimitStr))
                                throw new XmlException($"Unexpected library configuration for {nameof(FileChronicleLibrary)}, empty {nameof(this.FileSizeLimit)}.");
                            Int64 retentionLimit;
                            if (!Int64.TryParse(retentionLimitStr, out retentionLimit))
                                throw new XmlException($"Unexpected library configuration for {nameof(FileChronicleLibrary)}, value '{retentionLimitStr}' for {nameof(this.RetentionLimit)} is not a valid {nameof(Int64)}.");
                            if (retentionLimit <= 0)
                                this.WithNoRetentionLimit();
                            else
                                this.WithRetentionLimit(retentionLimit);
                            break;
                        default:
                            reader.Skip();
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Write configuration to XML via the specified <see cref="XmlWriter" />.
        /// </summary>
        /// <param name="writer"><see cref="XmlWriter" /> stream to the configuration file.</param>
        /// <seealso cref="Core.NChronicle.SaveConfigurationTo(string)"/>
        public void WriteXml(XmlWriter writer) {
            if (this.AgeLimit.HasValue) {
                writer.WriteElementString(nameof(this.AgeLimit), this.AgeLimit.Value.ToString());
            }
            writer.WriteElementString(nameof(this.FileSizeLimit), this.FileSizeLimit.ToString());
            writer.WriteElementString(nameof(this.RetentionLimit), this.RetentionLimit.ToString());
        }

    }

}