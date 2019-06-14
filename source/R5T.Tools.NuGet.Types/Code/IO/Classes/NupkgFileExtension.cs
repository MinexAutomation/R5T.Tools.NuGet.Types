using System;

using R5T.NetStandard.IO.Paths;


namespace R5T.Tools.NuGet.IO
{
    public class NupkgFileExtension : FileExtension
    {
        #region Static

        /// <summary>
        /// The <see cref="Constants.NupkgFileExtension"/> value.
        /// </summary>
        public static readonly NupkgFileExtension Instance = new NupkgFileExtension(Constants.NupkgFileExtension);

        #endregion


        public NupkgFileExtension(string value)
            : base(value)
        {
        }
    }
}
