﻿using Desafio_Balta_IBGE.Domain.Atributes;
using Desafio_Balta_IBGE.Shared.Exceptions;
using Desafio_Balta_IBGE.Shared.Extensions;
using Desafio_Balta_IBGE.Shared.Result;
using Desafio_Balta_IBGE.Shared.ValueObjects;

namespace Desafio_Balta_IBGE.Domain.ValueObjects
{
    public sealed class Password : ValueObject
    {
        public Password()
        {

        }
        public Password(string hash)
        {
            Hash = hash.Trim().Encrypt();
            //this.CheckPropertiesIsNull();
        }

        [IfNull(ErrorMessage = "Senha inválida.")]
        public string? Hash { get; private set; }
        public string? Code { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public DateTime? ActivateDate { get; private set; } = null;
        public bool Active => ActivateDate != null && ExpireDate == null;

        public void GenerateCode()
        {
            Code = Guid.NewGuid().ToString("N")[..8].ToUpper();
            ExpireDate = DateTime.Now.AddMinutes(5);
            ActivateDate = null;
        }

        public bool Verify(string password)
            => Criptography.CompareHash(password, Hash);

        public VerifyCodeResult VerifyCode(string code)
        {
            if (Code is null && ActivateDate != null)
                return new VerifyCodeResult(IsCodeValid: false, Message: "Este código já foi verificado.");

            if (Active)
                return new VerifyCodeResult(IsCodeValid: false, Message: "Este código já foi utilizado.");

            if (code.Trim() != Code?.Trim())
                return new VerifyCodeResult(IsCodeValid: false, Message: "Código informado não confere.");

            if (ExpireDate < DateTime.Now)
                return new VerifyCodeResult(IsCodeValid: false, Message: "Este código já expirou.");

            InvalidParametersException.ThrowIfNull(code, "Código informado é inválido.");

            return new VerifyCodeResult(IsCodeValid: true, Message: string.Empty);
        }

        public void UpdatePassword(string password)
        {
            InvalidParametersException.ThrowIfNull(password, "Senha inválida.");
            Hash = password.Trim().Encrypt();
        }
    }
}
