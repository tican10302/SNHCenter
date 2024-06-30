using System.Reflection;
using AutoDependencyRegistration;
using AutoMapper;
using DAL.Data;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using REPOSITORY;
using REPOSITORY.Common;

namespace BLL.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServiceExtensions(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        });
        services.AddCors();

        //MAPPER
        var allREPONSITORY = Assembly.GetEntryAssembly()
            .GetReferencedAssemblies()
            .Select(Assembly.Load)
            .Where(x => x.FullName.Contains("REPOSITORY"));
        var mappigConfig = new MapperConfiguration(mc =>
        {
            mc.AddMaps(allREPONSITORY);
            mc.CreateMap<DateOnly?, DateTime?>().ConvertUsing(new DateTimeTypeConverter());
            mc.CreateMap<DateTime?, DateOnly?>().ConvertUsing(new DateOnlyTypeConverter());
        });
        IMapper mapper = mappigConfig.CreateMapper();
        services.AddSingleton(mapper);

        //services.AddAutoMapper(typeof(Startup));

        //FLUENT
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

        // UNITOFWORK
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //ALL SERVICE
        services.AutoRegisterDependencies();

        return services;
    }

    public class DateTimeTypeConverter : ITypeConverter<DateOnly?, DateTime?>
    {
        public DateTime? Convert(DateOnly? source, DateTime? destination, ResolutionContext context)
        {
            return source.HasValue ? source.Value.ToDateTime(TimeOnly.Parse("00:00:00")) : null;
        }
    }

    public class DateOnlyTypeConverter : ITypeConverter<DateTime?, DateOnly?>
    {
        public DateOnly? Convert(DateTime? source, DateOnly? destination, ResolutionContext context)
        {
            return source.HasValue ? DateOnly.FromDateTime(source.Value) : null;
        }
    }
    
}