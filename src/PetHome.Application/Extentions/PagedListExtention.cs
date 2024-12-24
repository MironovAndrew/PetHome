﻿using Microsoft.EntityFrameworkCore;
using PetHome.Application.Models;

namespace PetHome.Application.Extentions;
public static class PagedListExtention
{
    public static async Task<PagedList<T>> ToPagedList<T>(
        this IQueryable<T> source,
        int pageNum,
        int pageSize,
        CancellationToken ct)
    {
        var count = await source.CountAsync();
        var items = await source
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<T>
        {
            Items = items,
            PageNumber = pageNum,
            PageSize = pageSize
        };
    }
}