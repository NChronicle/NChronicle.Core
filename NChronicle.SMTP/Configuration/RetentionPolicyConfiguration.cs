using System;

namespace NChronicle.File.Configuration {

    /// <summary>
    /// Container for <see cref="RetentionPolicy"/> configuration.
    /// </summary>
    public class RetentionPolicyConfiguration {

        internal TimeSpan? AgeLimit = new TimeSpan(1, 0, 0, 0);

        internal long ByteLimit = 104857600; // 100 MB
        internal long RetentionLimit = 20;

        /// <summary>
        /// Set the age limit for the output file before it will 
        /// be archived. The default age limit is 1 day. 
        /// </summary>
        /// <param name="timeSpan">The maximum age for the output file as a <see cref="TimeSpan"/>.</param>
        public void WithAgeLimit (TimeSpan timeSpan) => this.AgeLimit = timeSpan;

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
            this.ByteLimit = bytes;
        }

        /// <summary>
        /// Remove the file size limit for the output file so as not
        /// to archive it - regardless of it's file size - unless it
        /// extends over the set age limit.
        /// </summary>
        public void WithNoFileSizeLimit () {
            this.ByteLimit = 0;
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

    }

}