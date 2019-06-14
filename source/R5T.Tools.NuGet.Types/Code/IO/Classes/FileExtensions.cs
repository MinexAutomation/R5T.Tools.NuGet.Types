using System;


namespace R5T.Tools.NuGet.IO
{
    public static class FileExtensions
    {
        /// <summary>
        /// The NuGet package file-extension.
        /// </summary>
        public static readonly NupkgFileExtension Nupkg = NupkgFileExtension.Instance;
        /// <summary>
        /// The NuGet specification file-extension.
        /// </summary>
        public static readonly NuspecFileExtension Nuspec = NuspecFileExtension.Instance;
    }
}
