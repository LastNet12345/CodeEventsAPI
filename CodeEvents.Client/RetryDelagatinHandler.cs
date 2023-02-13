namespace CodeEvents.Client
{
    public class RetryDelagatinHandler : DelegatingHandler
    {
        private const int nrOfTimes = 2;
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = default!;

            for (int i = 0; i < nrOfTimes; i++)
            {
                  response = await base.SendAsync(request, cancellationToken);

                  if(response.IsSuccessStatusCode) return response;
            }

            return response;
           
        }
    }
}
