using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RestWithAspNet10.Hypermedia.Abstract;

namespace RestWithAspNet10.Hypermedia.Filters
{
    public class HypermediaFilter(HypermediaFilterOptions hypermediaFilterOptions) : ResultFilterAttribute
    {
        private readonly HypermediaFilterOptions _hypermediaFilterOptions = hypermediaFilterOptions;

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            TryEnricheResult(context);
            base.OnResultExecuting(context);
        }

        private void TryEnricheResult(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult objectResult)
            {
                var enricher = _hypermediaFilterOptions.ContentResponseEnricherList.FirstOrDefault(option => option.CanEnrich(context));
                    enricher?.Enrich(context).Wait();
            }
        }
    }
}
