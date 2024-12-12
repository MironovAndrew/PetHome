﻿using CSharpFunctionalExtensions;
using PetHome.Domain.PetManagment.VolunteerEntity;
using PetHome.Domain.Shared.Error;

namespace PetHome.Application.Interfaces.RepositoryInterfaces;

//Инверсия управления
public interface IVolunteerRepository
{
    //Создание волонтёра
    public Task<Guid> Add(Volunteer volunteer, CancellationToken ct);
    //Создание волонтёра
    public Task<Guid> Update(Volunteer volunteer, CancellationToken ct);

    //Найти волонтера по ID
    public Task<Result<Volunteer, Error>> GetById(Guid id, CancellationToken ct);

    //Удаление волонтера
    public Task<Guid> Remove(Volunteer volunteer, CancellationToken ct);

    //Удаление волонтера по id
    public Task<Result<bool, Error>> RemoveById(VolunteerId id, CancellationToken ct);

    //Удалить коллекцию
    public void Remove(IEnumerable<Volunteer> volunteers);

    //Получить список Волонётров у кого параметр isDeleted == true
    public IReadOnlyList<Volunteer> GetDeleted(CancellationToken ct);
}