namespace CodeEvents.Api.Data.Repositories
{
    public class LecturesRepository
    {
        private readonly CodeEventsApiContext db;

        public LecturesRepository(CodeEventsApiContext db)
        {
            this.db = db;
        }
    }
}
