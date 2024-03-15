using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.User.Imports;

namespace SibSIU.Domain.Dean.Synchronization.Commands.SaveStudents;
public interface ISaveStudentsHandler : IRequestHandler<SaveStudentsRequest, Result<CsvLoginData>>
{
}
