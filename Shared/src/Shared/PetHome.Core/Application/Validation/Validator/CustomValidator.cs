﻿using CSharpFunctionalExtensions;
using FluentValidation;
using PetHome.SharedKernel.Responses.ErrorManagement;

namespace PetHome.Core.Application.Validation.Validator;
public static class CustomValidator
{
    public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>
        (this IRuleBuilder<T, TElement> ruleBuilder, Func<TElement, Result<TValueObject, Error>> factoryMethod)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            Result<TValueObject, Error> result = factoryMethod(value);

            if (result.IsSuccess)
                return;

            context.AddFailure(result.Error.InvalidField);
        });
    }

}
public static class RuleBuilderExtention
{
    public static IRuleBuilder<T, TProperty> WithError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule,
        Error error)
    {
        return rule.WithMessage(error.Message);
    }
}
