using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.Dean.Synchronization.Commands.SaveStudents.Models;
using SibSIU.Identity.Models.User.Imports;
using SibSIU.Identity.Models.User.Students;

namespace SibSIU.Domain.Dean.Synchronization.Commands.SaveStudents;
public sealed class SaveStudentsRequest : IRequest<Result<CsvLoginData>>
{
    private readonly ComparativeUserBeforeImportList _data;

    public string CacheKey => _data.CacheKey;
    internal List<AddAcademicGroup> MustBeUpdate => GetUpdatedUser();

    private List<AddAcademicGroup> GetUpdatedUser()
    {
        List<AddAcademicGroup> addingGroups = [];
        foreach (var item in _data.DataList.Where(d => d.ExistsSimilar.Any(s => s.MastCombine)))
        {
            var exists = item.ExistsSimilar.Where(s => s.MastCombine).First();
            addingGroups.Add(new(exists.User.Id, item.User.Id, item.User.Students));
        }
        return addingGroups;
    }

    public SaveStudentsRequest(ComparativeUserBeforeImportList data)
    {
        _data = data;
    }

    public SaveStudentsRequest() : this(new()) { }
}
