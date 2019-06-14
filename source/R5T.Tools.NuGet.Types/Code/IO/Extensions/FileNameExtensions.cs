using System;

using R5T.NetStandard.IO.Paths;


namespace R5T.Tools.NuGet.IO
{
    public static class FileNameExtensions
    {
        public static NupkgFileName AsNupkgFileName(this FileName fileName)
        {
            var nupkgFileName = new NupkgFileName(fileName.Value);
            return nupkgFileName;
        }

        public static NuspecFileName AsNuspecFileName(this FileName fileName)
        {
            var nuspecFileName = new NuspecFileName(fileName.Value);
            return nuspecFileName;
        }
    }
}
