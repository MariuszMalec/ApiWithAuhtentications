namespace WepAppAccessToApi.Models
{
    public class MultipleViewModel
    {
        public AuthenticateModel AuthenticateModels { get; set; }
        public IEnumerable<UserFromNbaApii> UserFromNbaApii { get; set; }
    }
}
