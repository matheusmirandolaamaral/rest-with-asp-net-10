using RestWithAspNet10.Hypermedia.Abstract;

namespace RestWithAspNet10.Hypermedia.Filters
{
    public class HypermediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = [];
    }
}
