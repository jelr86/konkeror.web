using AutoMapper;
using konkeror.app.Services;
using konkeror.app.Services.Interface;
using konkeror.data;
using Ninject;
using Ninject.Modules;
using Ninject.Web.WebApi.Filter;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace konkeror.web.Infrastructure
{
    public class NinjectRegistration : NinjectModule
    {
        public override void Load()
        {
            Bind<IClientRepository>().To<ClientRepository>();
            Bind<DbContext>().To<konkerorEntities>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<KonkerorAutoMapperProfile>());
            Bind<MapperConfiguration>().ToConstant(config).InSingletonScope();

            Bind<IMapper>().ToMethod(ctx =>
                 new Mapper(config, type => ctx.Kernel.Get(type)));

        }
    }
}