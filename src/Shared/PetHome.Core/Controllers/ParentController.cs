﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PetHome.Core.Response.Envelopes;
using PetHome.Core.Response.Validation.Validator;
using PetHome.Domain.Shared.Error;

namespace PetHome.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ParentController : ControllerBase
{
    public override OkObjectResult Ok([ActionResultObjectValue] object? value)
    {
        return base.Ok(ResponseEnvelope.Ok(value));
    }

    public override BadRequestObjectResult BadRequest([ActionResultObjectValue] object? error)
    {
        return base.BadRequest(ResponseEnvelope.Error(error as ErrorList));
    }
}
