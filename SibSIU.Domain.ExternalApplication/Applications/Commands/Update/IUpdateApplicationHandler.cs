﻿using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.ExternalApplication.Applications.Commands._Shared;

namespace SibSIU.Domain.ExternalApplication.Applications.Commands.Update;

public interface IUpdateApplicationHandler : IRequestHandler<CreateOrUpdateApplicationRequest, Result<Message>>
{
}