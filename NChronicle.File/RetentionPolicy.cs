using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NChronicle.File.Configuration;
using NChronicle.File.Delegates;
using NChronicle.File.Interfaces;

namespace NChronicle.File {

    /// <summary>
    /// The default <see cref="IRetentionPolicy"/> archiving iteratively on file size and age. 
    /// </summary>
    /// <remarks>
    /// <para>
    /// This retention policy is based on file size and file age. Depending 
    /// on the configuration, when the output file grows to the set size limit, 
    /// or the time since it was created is greater than the set age limit, it 
    /// will be archived and named with time the output file was created. 
    /// </para>
    /// <para>
    /// If the file name for the archive is already taken it will be appended by
    /// a subsequently growing number. 
    /// </para>
    /// <para>
    /// It is also possible to set a retention limit, this limit defines how 
    /// many of the newest archived logs are kept. 
    /// </para>
    /// <para>
    /// The default configuration is a 100MB file size limit, 1 day file age limit, 
    /// and a retention limit of 20.</para>
    /// </remarks>
    /// <example>
    /// Starting an application at 12:35 on December 12th 2016 with
    /// a file size limit of 100MB, a file age limit of 30 minutes and 
    /// an output path of <c>"chronicle.log"</c> will result in the output 
    /// file being archived with the name of <c>"chronicle.2016.12.01.12.35.log"</c> 
    /// at 13:05 or when 100MB of records have been written to it; which ever comes first.
    /// </example>
    public class RetentionPolicy : IRetentionPolicy {

        private readonly RetentionPolicyConfiguration _configuration;

        /// <summary>
        /// Create a new <see cref="RetentionPolicy"/> instance with the default configuration.
        /// </summary>
        public RetentionPolicy () {
            this._configuration = new RetentionPolicyConfiguration();
        }

        /// <summary>
        /// Check if the output file at the given <paramref name="path"/> is still within the configured
        /// limits or is to be archived. It is ready if the file - with the pending bytes to be written - 
        /// is over the configured file size limit or the file is older than the configured file age limit. 
        /// </summary>
        /// <param name="path">The path to the output file.</param>
        /// <param name="pendingBytes">Any pending bytes that are to be written to the output file.</param>
        /// <returns>A <see cref="bool"/> indicating if the output file is to be archived.</returns>
        public bool CheckPolicy (string path, byte[] pendingBytes) {
            var fileInfo = new FileInfo(path);
            if (this._configuration.ByteLimit != 0 && fileInfo.Length + pendingBytes.Length > this._configuration.ByteLimit) {
                return true;
            }
            return this._configuration.AgeLimit.HasValue && fileInfo.CreationTimeUtc < DateTime.UtcNow - this._configuration.AgeLimit.Value;
        }

        /// <summary>
        /// Archive the output file at the given <paramref name="path"/>, naming with time the output file 
        /// was created. 
        /// </summary>
        /// <param name="path">The path to the output file.</param>
        public void InvokePolicy (string path) {
            System.IO.File.Move(path, this.GetFileName(path));
            this.PurgeFiles(path);
        }

        /// <summary>
        /// Configure this <see cref="RetentionPolicy"/> with the specified options.
        /// </summary>
        /// <param name="configurationDelegate">A function to set <see cref="RetentionPolicy"/> configuration.</param>
        public void Configure (RetentionPolicyConfigurationDelegate configurationDelegate) {
            configurationDelegate(this._configuration);
        }

        private string GetFileName (string path) {
            var now = new FileInfo(path).CreationTime;
            var extension = string.Empty;
            if (!string.IsNullOrEmpty(extension = Path.GetExtension(path))) {
                path = $"{Path.GetDirectoryName(path)}\\{Path.GetFileNameWithoutExtension(path)}";
            }
            var newPath = $"{path}.{now.Year}.{now.Month}.{now.Day}.{now.Hour}.{now.Minute}";
            if (!System.IO.File.Exists($"{newPath}{extension}")) {
                return $"{newPath}{extension}";
            }
            var i = 0;
            string fileName;
            do {
                i++;
                fileName = $"{newPath}.{i}{extension}";
            }
            while (System.IO.File.Exists(fileName));
            return fileName;
        }

        private void PurgeFiles (string path) {
            if (this._configuration.RetentionLimit <= 0) return;
            var fileName = this.EscapeRegex(Path.GetFileNameWithoutExtension(path));
            var fileExt = this.EscapeRegex(Path.GetExtension(path));
            var regex = new Regex
                ($"{fileName}\\.\\d\\d\\d\\d\\.\\d\\d?\\.\\d\\d?\\.\\d\\d?\\.\\d\\d?(\\.\\d+)?{fileExt}$");
            var files = Directory.EnumerateFiles(Path.GetDirectoryName(path)).Where(p => regex.IsMatch(p));
            var toDelete = files.Count() - this._configuration.RetentionLimit;
            if (toDelete > 0) {
                var filesOrdered = files.OrderBy(f => new FileInfo(f).CreationTimeUtc).ToArray();
                for (var i = 0; i < toDelete; i++) {
                    System.IO.File.Delete(filesOrdered[i]);
                }
            }
        }

        private string EscapeRegex (string str) {
            if (str == null) return null;
            if (str == string.Empty) return string.Empty;
            return
                str.Replace("\\", "\\\\")
                   .Replace(".", "\\.")
                   .Replace("$", "\\$")
                   .Replace("^", "\\^")
                   .Replace("[", "\\[")
                   .Replace("]", "\\]")
                   .Replace("(", "\\(")
                   .Replace(")", "\\)")
                   .Replace("{", "\\{")
                   .Replace("}", "\\}")
                   .Replace("*", "\\*")
                   .Replace("+", "\\+")
                   .Replace("?", "\\?")
                   .Replace("#", "\\#");
        }

    }

}