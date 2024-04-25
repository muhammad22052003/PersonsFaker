namespace Task_5.Packages
{
    public class RegionsPackage
    {
        /// See the available regions at the link https://github.com/bchavez/Bogus/
        public void AddRegion(string localeCode, string countryName)
        {
            if (Regions.ContainsKey(countryName) || Regions.ContainsValue(localeCode))
            {
                return;
            }
            Regions.Add(countryName, localeCode);
        }
        public bool CheckRegion(string regionName)
        {
            bool result = Regions.ContainsKey(regionName);

            return result;
        }

        // Add available regions
        public Dictionary<string, string> Regions { get; private set; } = new Dictionary<string, string>();
    }
}
