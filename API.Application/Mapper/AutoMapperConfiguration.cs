﻿using API.Application.Mapper.Seguridad;
using API.Data.Entidades.IslaAzul;
using AutoMapper;

namespace API.Application.Mapper
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMappers(this IServiceCollection services, MapperConfigurationExpression cfg)
        {
            IMapper mapper = new MapperConfiguration(cfg).CreateMapper();
            services.AddSingleton(mapper);
        }
        public static MapperConfigurationExpression AddAutoMapperLeadOportunidade(this MapperConfigurationExpression cfg)
        {   
            cfg.AddProfile<ClienteDtoProfile>();
            cfg.AddProfile<HabitacionDtoProfile>();
            cfg.AddProfile<HabitacionAmaDeLlavesDtoProfile>();
            cfg.AddProfile<ReservaDtoProfile>();
            cfg.AddProfile<AmaDeLlavesDtoProfile>();
            cfg.AddProfile<UsuarioDtoProfile>();
            cfg.AddProfile<RolDtoProfile>();
            cfg.AddProfile<PermisoDtoProfile>();

            return cfg;
        }
        public static MapperConfigurationExpression CreateExpression()
        {
            return new MapperConfigurationExpression();
        }
    }
}
