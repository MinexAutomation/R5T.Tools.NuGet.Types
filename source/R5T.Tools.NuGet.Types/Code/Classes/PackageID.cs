using System;

using R5T.NetStandard;


namespace R5T.Tools.NuGet
{
    /// <summary>
    /// By convention, the non-lower-case project-name.
    /// </summary>
    public class PackageID : TypedString
    {
        public PackageID(string value)
            : base(value)
        {
        }
    }
}
