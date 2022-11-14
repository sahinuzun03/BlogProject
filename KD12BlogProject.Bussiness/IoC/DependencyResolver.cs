using Autofac;
using AutoMapper;
using KD12BlogProject.Bussiness.AutoMapper;
using KD12BlogProject.Bussiness.Services.AppUserService;
using KD12BlogProject.Bussiness.Services.AuthorService;
using KD12BlogProject.Bussiness.Services.GenreService;
using KD12BlogProject.Bussiness.Services.PostService;
using KD12BlogProject.DataAccess.Abstract;
using KD12BlogProject.DataAccess.EntityFramework.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD12BlogProject.Bussiness.IoC
{
    public class DependencyResolver : Module
    {
        //IoC Nedir : IoC nesnelerin uygulama boyunca ki yaşam döngüsünden sorumludur.Uygulama içerisinde kullanılan objelerin instance'larının yönetilmesini sağlar ve bağımlılığın en aza indirgemeyi amaçlar.

        //

        //AUTOFAC KÜTÜPHANESİ .NET CORE TARAFINDAN GELEN DI'IN YERİNİ ALMAK İÇİN KULLANILMIŞTIR. PEKİ BUNU BİZE .NET CORE'UN DI ' I KARŞILIYORSA NEDEN AUTOFAC'E İHTİYAÇ DUYDUK. BUNUN AÇIKLANMASI İÇİN ASPECT ORİENTED PROGRAMMİNG KONUSUNU İNCELEMENİZ GEREKİYOR.

        // Autofac ile artık builder.Services.AddScoped<GenreRepository,IGenreRepository>() : dememize gerek kalmadı burayı artık DependencResolver yönetiyor.
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GenreRepository>().As<IGenreRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AuthorRepository>().As<IAuthorRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PostRepository>().As<IPostRepository>().InstancePerLifetimeScope();

            builder.RegisterType<GenreService>().As<IGenreService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthorService>().As<IAuthorService>().InstancePerLifetimeScope();
            builder.RegisterType<PostService>().As<IPostService>().InstancePerLifetimeScope();
            builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                //Register Mapper Profile
                //Mapping dosyamızıda buraya ekliyoruz gidip startup'ta eklemek zorunda kalmayalım zaten burasının görevi oraya sağlamak olacak.
                cfg.AddProfile<Mapping>();
            }
            )).AsSelf().SingleInstance();

            
            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();


            base.Load(builder);
        }
    }
}
