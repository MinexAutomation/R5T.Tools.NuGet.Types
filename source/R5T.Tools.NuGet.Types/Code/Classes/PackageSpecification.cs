using System;

using R5T.NetStandard;


namespace R5T.Tools.NuGet
{
    public class PackageSpecification
    {
        public PackageID ID { get; set; }
        public Version Version { get; set; }


        public PackageSpecification()
        {
        }

        public PackageSpecification(PackageID name, Version version)
        {
            this.ID = name;
            this.Version = version;
        }

        public PackageSpecification(PackageID name)
            : this(name, VersionHelper.None)
        {
        }

        public override string ToString()
        {
            var output =
                this.Version == VersionHelper.None
                ? $"{this.ID}"
                : $"{this.ID} V{this.Version}";

            return output;
        }
    }
}
