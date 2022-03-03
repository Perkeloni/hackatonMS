﻿using System.Dynamic;

namespace Hackaton.Application.Interfaces.Persistence.Helper
{
    public interface IDataShaper<T>
    {
        IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString);

        ExpandoObject ShapeData(T entity, string fieldsString);
    }
}