﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MongoDB.Bson;
using SimpleHabr.DTOs;
using SimpleHabr.Models;
using SimpleHabr.Services;

namespace SimpleHabr.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        private readonly IUnitOfWork _uow;

        public AutoMapperProfiles()
        {
        }

        public AutoMapperProfiles(IUnitOfWork uow)
        {
            _uow = uow;
            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.Id, opt =>
                {
                    opt.MapFrom(src => src.Id.ToString());
                }).ForMember(dest => dest.Comments, opt =>
               {
                   opt.MapFrom(src => src.Comments == null ? new List<string>() : src.Comments.Select(i => i.ToString()));

               }).ForMember(dest => dest.UserId, opt =>
                {
                    opt.MapFrom(src => src.UserId.ToString());
                }).ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<PostDto, Post>()
                   .ForMember(dest => dest.Id, opt =>
                   {
                       opt.MapFrom(src => new ObjectId(src.Id));
                   }).ForMember(dest => dest.Comments, opt =>
                   {
                       opt.MapFrom(src => src.Comments == null ? new List<ObjectId>() : src.Comments.Select(i => new ObjectId(i)));

                   }).ForMember(dest => dest.UserId, opt =>
                   {
                       opt.MapFrom(src => new ObjectId(src.UserId));
                   }).ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));



            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.Id, opt =>
                {
                    opt.MapFrom(src => src.Id.ToString());
                }).ForMember(dest => dest.PostId, opt =>
                  {
                      opt.MapFrom(src => src.PostId.ToString());
                  }).ForMember(dest => dest.UserId, opt =>
                  {
                      opt.MapFrom(src => src.UserId.ToString());
                  }).ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CommentDto, Comment>()
               .ForMember(dest => dest.Id, opt =>
               {
                   opt.MapFrom(src => new ObjectId(src.Id));

               }).ForMember(dest => dest.PostId, opt =>
               {
                   opt.MapFrom(src => new ObjectId(src.PostId));
               }).ForMember(dest => dest.UserId, opt =>
               {
                   opt.MapFrom(src => new ObjectId(src.UserId));
               }).ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            /*  CreateMap<Post, PostDetailDto>()
                 .ForMember(dest => dest.Id, opt =>
                 {
                     opt.MapFrom(src => src.Id.ToString());
                 }).ForMember(dest => dest.Comments, opt =>
                 {
                     opt.MapFrom(src => src.Comments == null ? new List<CommentDto>() : mapper.Map<IEnumerable<CommentDto>>(_uow.Comments.GetAll().Where(c => c.PostId == src.Id)));

                 }).ForMember(dest => dest.UserId, opt =>
                 {
                     opt.MapFrom(src => src.UserId.ToString());
                 }).ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));*/

            /* CreateMap<PostDetailDto, Post>()
    .ForMember(dest => dest.Id, opt =>
    {
        opt.MapFrom(src => new ObjectId( src.Id));
    }).ForMember(dest => dest.Comments, opt =>
    {
        opt.MapFrom(src => src.Comments == null ? new List<ObjectId>() : mapper.Map<IEnumerable<CommentDto>>(_uow.Comments.GetAll().Where(c => c.PostId == new ObjectId( src.Id))).Select(i=>new ObjectId(i.Id)).ToList());

    }).ForMember(dest => dest.UserId, opt =>
    {
        opt.MapFrom(src => new ObjectId(src.UserId));
    }).ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));*/
        }
    }
}
