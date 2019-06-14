using System;

using R5T.NetStandard.IO.Paths;


namespace R5T.Tools.NuGet.IO
{
    public class NuspecFileExtension : FileExtension
    {
        #region Static

        /// <summary>
        /// The <see cref="Constants.NuspecFileExtension"/> value.
        /// </summary>
        public static readonly NuspecFileExtension Instance = new NuspecFileExtension(Constants.NuspecFileExtension);

        #endregion


        public NuspecFileExtension(string value)
            : base(value)
        {
        }
    }
}
