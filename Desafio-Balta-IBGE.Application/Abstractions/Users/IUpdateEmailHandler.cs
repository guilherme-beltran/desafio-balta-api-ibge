﻿using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;

namespace Desafio_Balta_IBGE.Application.Abstractions.Users
{
    public interface IUpdateEmailHandler
    {
        Task<IResponse> Handle(UpdateEmailUserRequest request, CancellationToken cancellationToken);
    }
}
