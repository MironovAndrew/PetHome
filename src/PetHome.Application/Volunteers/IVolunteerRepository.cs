﻿using PetHome.Domain.GeneralValueObjects;
using PetHome.Domain.VolunteerEntity;

namespace PetHome.Application.Volunteers;

//Инверсия управления
public interface IVolunteerRepository
{
    //Создание волонтёра
    public Task<Guid> Add(Volunteer volunteer, CancellationToken ct = default);

    //Найти волонтера по ID
    public Task<Volunteer> GetById(VolunteerId id, CancellationToken ct = default);

    //Удаление волонтера
    public Task<Guid> Remove(Volunteer volunteer, CancellationToken ct = default);

    //Удаление волонтера по id
    public Task<bool> RemoveById(VolunteerId id, CancellationToken ct = default);
}
