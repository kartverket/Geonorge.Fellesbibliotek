using System.Threading.Tasks;

namespace Kartverket.Geonorge.Utilities.Organization
{
    public interface IOrganizationService
    {
        Task<Organization> GetOrganizationByName(string name);
    }
}
