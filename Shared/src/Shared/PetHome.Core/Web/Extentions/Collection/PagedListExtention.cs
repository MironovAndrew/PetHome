﻿using Microsoft.EntityFrameworkCore;
using PetHome.Core.Domain.Models;
using System.Linq.Expressions;

namespace PetHome.Core.Web.Extentions.Collection;
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
            .ToListAsync(ct);

        return new PagedList<T>
        {
            Items = items,
            PageNumber = pageNum,
            PageSize = pageSize
        };
    }

    public static PagedList<T> ToPagedList<T>(
        this IEnumerable<T> source,
        int pageNum,
        int pageSize)
    {
        var count = source.Count();
        var items = source
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedList<T>
        {
            Items = items,
            PageNumber = pageNum,
            PageSize = pageSize
        };
    }

    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> source,
        bool condition,
        Expression<Func<T, bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }
}
