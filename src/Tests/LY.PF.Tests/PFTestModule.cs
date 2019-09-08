﻿using System;
using Abp.Modules;
using Abp.MultiTenancy;
using Abp.TestBase;
using Abp.Zero.Configuration;
using Castle.MicroKernel.Registration;
using NSubstitute;

namespace LY.PF.Tests
{
    [DependsOn(
        typeof(PFApplicationModule),
        typeof(PFDataModule),
        typeof(AbpTestBaseModule))]
    public class PFTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.Timeout = TimeSpan.FromMinutes(30);

            //Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            RegisterFakeService<IAbpZeroDbMigrator>();
        }

        private void RegisterFakeService<TService>() 
            where TService : class
        {
            var instance = Substitute.For<TService>();
            IocManager.IocContainer.Register(
                Component.For<TService>()
                    .Instance(instance)
                    .LifestyleSingleton()
            );
        }
    }
}
