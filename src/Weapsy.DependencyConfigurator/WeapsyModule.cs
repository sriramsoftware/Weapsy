﻿using Autofac;
using FluentValidation;
using Weapsy.Core.DependencyResolver;
using Weapsy.Core.Dispatcher;
using Weapsy.Core.Domain;
using Weapsy.Core.Caching;
using Weapsy.Domain.Data.Repositories;
using Weapsy.Domain.Model.Sites.Handlers;
using Weapsy.Domain.Model.Sites.Rules;
using Weapsy.Domain.Model.Sites.Validators;
using Weapsy.Reporting.Data.EventHandlers;
using Weapsy.Domain.Services.Installation;
using Weapsy.Domain.Model.Languages;
using Weapsy.Reporting.Menus;
using Weapsy.Reporting.Languages;
using Weapsy.Reporting.Pages;
using Weapsy.Reporting.Sites;
using Weapsy.Domain.EventStore.SqlServer;
using Weapsy.Reporting.Data.Languages;
using Weapsy.Reporting.Data.Menus;
using Weapsy.Reporting.Data.Pages;
using Weapsy.Reporting.Data.Sites;
using System.Reflection;
using Weapsy.Reporting.Data.ModuleTypes;
using Weapsy.Reporting.ModuleTypes;
using Weapsy.Reporting.Data.Apps;
using Weapsy.Reporting.Apps;
using Weapsy.Domain.Data;
using Weapsy.Reporting.Data.Modules;
using Weapsy.Reporting.Modules;
using Weapsy.Reporting.Themes;
using Weapsy.Reporting.Data.Themes;

namespace Weapsy.DependencyConfigurator
{
    public class WeapsyModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WeapsyDbContext>().As<WeapsyDbContext>();
            builder.RegisterType<EventStoreDbContext>().As<EventStoreDbContext>();

            builder.RegisterType<AutofacResolver>().As<IResolver>();
            builder.RegisterType<CommandSender>().As<ICommandSender>();
            builder.RegisterType<EventPublisher>().As<IEventPublisher>();
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>();
            builder.RegisterType<SqlServerEventStore>().As<IEventStore>();

            builder.RegisterAssemblyTypes(typeof(CreateSiteHandler).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(ICommandHandler<>));
            builder.RegisterAssemblyTypes(typeof(CreateSiteValidator).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IValidator<>));
            builder.RegisterAssemblyTypes(typeof(SiteRules).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRules<>));
            builder.RegisterAssemblyTypes(typeof(SiteRepository).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRepository<>));
            builder.RegisterAssemblyTypes(typeof(SiteEventsHandler).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IEventHandler<>));

            builder.RegisterType<LanguageSortOrderGenerator>().As<ILanguageSortOrderGenerator>();

            builder.RegisterType<AppInstallationService>().As<IAppInstallationService>();
            builder.RegisterType<SiteInstallationService>().As<ISiteInstallationService>();

            builder.RegisterType<AppFacade>().As<IAppFacade>();
            builder.RegisterType<LanguageFacade>().As<ILanguageFacade>();
            builder.RegisterType<MenuFacade>().As<IMenuFacade>();
            builder.RegisterType<ModuleFacade>().As<IModuleFacade>();
            builder.RegisterType<ModuleTypeFacade>().As<IModuleTypeFacade>();
            builder.RegisterType<PageFacade>().As<IPageFacade>();
            builder.RegisterType<SiteFacade>().As<ISiteFacade>();
            builder.RegisterType<ThemeFacade>().As<IThemeFacade>();

            //Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            //builder.RegisterAssemblyTypes(assemblies).As(typeof(IStartupTask));            
            //builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(IQueryHandler<,>));                        
        }
    }
}