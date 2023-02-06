namespace CodeEvents.Api.Data.Repositories
{
    public interface IUnitOfWork
    {
        ICodeEventRepository CodeEventRepository { get; }
        LecturesRepository LecturesRepository { get; }

        Task CompleteAsync();
    }
}