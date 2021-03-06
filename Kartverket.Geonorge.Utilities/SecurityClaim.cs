namespace Kartverket.Geonorge.Utilities
{
    public class SecurityClaim
    {
        public class Role
        {
            public const string MetadataAdmin = "nd.metadata_admin";
            public const string MetadataEditor = "nd.metadata_editor";
        }

        public static string GetUsername()
        {
            return GetSecurityClaim("username");
        }

        public static string GetSecurityClaim(string type)
        {
            string result = null;
            foreach (var claim in System.Security.Claims.ClaimsPrincipal.Current.Claims)
            {
                if (claim.Type == type && !string.IsNullOrWhiteSpace(claim.Value))
                {
                    result = claim.Value;
                    break;
                }
            }

            // bad hack, must fix BAAT
            if (!string.IsNullOrWhiteSpace(result) && type.Equals("organization") && result.Equals("Statens kartverk"))
            {
                result = "Kartverket";
            }

            return result;
        }
    }
}
