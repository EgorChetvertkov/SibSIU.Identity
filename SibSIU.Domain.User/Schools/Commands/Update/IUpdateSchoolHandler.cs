﻿using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Schools.Commands._Shared;

namespace SibSIU.Domain.UserManager.Schools.Commands.Update;

public interface IUpdateSchoolHandler : IRequestHandler<CreateOrUpdateSchoolRequest, Result<Message>>
{
}