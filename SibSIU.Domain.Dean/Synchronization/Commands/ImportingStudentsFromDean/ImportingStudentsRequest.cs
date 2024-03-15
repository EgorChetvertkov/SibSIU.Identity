using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.AcademicGroups;
using SibSIU.Identity.Models.User.Students;
using System.Text;

namespace SibSIU.Domain.Dean.Synchronization.Commands.ImportingStudentsFromDean;
public sealed class ImportingStudentsRequest : IRequest<Result<ComparativeUserBeforeImportList>>, IValidated
{
    public List<AcademicGroupItem> Groups { get; set; }

    public ImportingStudentsRequest(List<AcademicGroupItem> groups)
    {
        Groups = groups;
    }

    public ImportingStudentsRequest() : this([]) { }

    public List<string> GetGroupNames()
    {
        return Groups.Select(g => g.GroupName).ToList();
    }

    public override string ToString()
    {
        if (Groups.Count != 0)
        {
            StringBuilder sb = new();
            sb.Append("Импорт групп:");
            Groups.ForEach((group) => sb.AppendLine(group.GroupName));
            return sb.ToString();
        }

        return "Нет групп для импорта";
    }

    public Error Validate()
    {
        return Groups.Count == 0 ?
            Error.Validation("Не указана ни одна группа") :
            Error.None;
    }
}
