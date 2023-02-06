namespace CodeEvents.Api.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CodeEventsApiContext db;

        public ICodeEventRepository CodeEventRepository { get; }
        public LecturesRepository LecturesRepository { get; }

        public UnitOfWork(CodeEventsApiContext db)
        {
            this.db = db;
            CodeEventRepository = new CodeEventRepository(db);
            LecturesRepository = new LecturesRepository(db);
        }

        public async Task CompleteAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
