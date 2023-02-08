namespace CodeEvents.Api.Core.Repositories
{
    public interface IUnitOfWork
    {
        ICodeEventRepository CodeEventRepository { get; }
        ILecturesRepository LecturesRepository { get; }

        Task CompleteAsync();
    }
}