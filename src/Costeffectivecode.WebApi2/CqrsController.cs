﻿using System;
using System.Web.Http;
using System.Web.Http.Results;
using CosteffectiveCode.AutoMapper;
using CosteffectiveCode.Cqrs.Commands;
using CosteffectiveCode.Cqrs.Queries;
using CosteffectiveCode.Ddd.Entities;
using CosteffectiveCode.Ddd.Specifications;

namespace Costeffectivecode.WebApi2
{
    public abstract class CqrsController : ApiController
    {
        public readonly ICommandFactory CommandFactory;

        public readonly IQueryFactory QueryFactory;

        protected CqrsController(ICommandFactory commandFactory, IQueryFactory queryFactory)
        {
            CommandFactory = commandFactory;
            QueryFactory = queryFactory;
        }

        public OkNegotiatedContentResult<TResult> Ok<TSpecification, TResult, TQuery>(
            TSpecification specification)
            where TQuery : IQuery<TSpecification, TResult>
        => Ok(QueryFactory.GetQuery<TSpecification, TResult, TQuery>().Execute(specification));

        public OkNegotiatedContentResult<TResult> Get<TKey, TEntity, TResult>(
            TKey specification)
            where TKey : struct
            where TEntity : class, IEntityBase<TKey>
            where TResult : IEntityBase<TKey>
        => Ok<TKey, TResult, GetQuery<TKey, TEntity, TResult>>(specification);

        public OkNegotiatedContentResult<TDto[]> List<TPagedSpecification, TEntity, TDto, TQuery>(
            TPagedSpecification specification)
            where TPagedSpecification : IPagedSpecification<TDto>
            where TEntity : class, IEntity
            where TDto : IEntity
            where TQuery : PagedEntityToDtoQuery<TPagedSpecification, TEntity, TDto>
        => Ok<TPagedSpecification, TDto[], TQuery>(specification);
    }
}
