namespace System
{
    /// <summary>
    /// Provides generic utilities.
    /// </summary>
    public static class Utility
    {
        private const string WithHyphenFormat = "D";
        
        private const string NoHyphenFormat = "N";
        
        /// <summary>
        /// Generate GUID string.
        /// </summary>
        /// <param name="withHyphen">If the result string connect with hyphen. </param>
        /// <returns>The GUID string. </returns>
        public static string GenerateGuidString(bool withHyphen = false)
        {
            var guid = Guid.NewGuid();
            return withHyphen ? guid.ToString(WithHyphenFormat) : guid.ToString(NoHyphenFormat);
        }
    }
}