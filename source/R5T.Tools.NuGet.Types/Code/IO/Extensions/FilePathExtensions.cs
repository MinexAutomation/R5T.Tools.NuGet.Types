using System;

using R5T.NetStandard.IO.Paths;


namespace R5T.Tools.NuGet.IO
{
    public static class FilePathExtensions
    {
        public static NupkgFilePath AsNupkgFilePath(this FilePath filePath)
        {
            var nupkgFilePath = new NupkgFilePath(filePath.Value);
            return nupkgFilePath;
        }

        public static NuspecFilePath AsNuspecFilePath(this FilePath filePath)
        {
            var nuspecFilePath = new NuspecFilePath(filePath.Value);
            return nuspecFilePath;
        }
    }
}
