using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.User.Students;

namespace SibSIU.Domain.Dean.Synchronization.Commands.ImportingStudentsFromDean;
public interface IImportingStudentsHandler : IRequestHandler<ImportingStudentsRequest, Result<ComparativeUserBeforeImportList>>
{
}
