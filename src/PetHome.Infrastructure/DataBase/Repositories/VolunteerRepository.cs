﻿using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetHome.Application.Features.Volunteers;
using PetHome.Domain.PetManagment.VolunteerEntity;

namespace PetHome.Infrastructure.DataBase.Repositories;
public class VolunteerRepository : IVolunteerRepository
{
    private readonly ApplicationDBContext _dBContext;

    public VolunteerRepository(ApplicationDBContext dBContext)
    {
        _dBContext = dBContext;
    }

    //Создание волонтёра
    public async Task<Guid> Add(Volunteer volunteer, CancellationToken ct = default)
    {
        await _dBContext.Volunteers.AddAsync(volunteer, ct);
        await _dBContext.SaveChangesAsync(ct);
        return volunteer.Id;
    }

    //Изменение волонтёра
    public async Task<Guid> Update(Volunteer volunteer, CancellationToken ct = default)
    {
        _dBContext.Volunteers.Attach(volunteer); 
        await _dBContext.SaveChangesAsync(ct);
        return volunteer.Id;
    }

    //Найти волонтера по ID
    public async Task<Volunteer> GetById(Guid id, CancellationToken ct = default)
    {
        var volunteer = await _dBContext.Volunteers
            .Where(v => v.Id == id)
            .Include(x => x.Pets)
            .FirstOrDefaultAsync(ct);

        return volunteer;
    }

    //Удаление волонтера
    public async Task<Guid> Remove(Volunteer volunteer, CancellationToken ct = default)
    {
        _dBContext.Remove(volunteer);
        await _dBContext.SaveChangesAsync(ct);

        return volunteer.Id;
    }

    //Удаление волонтера по id
    public async Task<bool> RemoveById(Guid id, CancellationToken ct = default)
    {
        Volunteer volunteer = GetById(id).Result;

        if (volunteer == null)
            return false;

        await Remove(volunteer, ct);
        return true;
    }
}
