using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.BookingForms.CreateBookingForm;
using WHO.BioHub.DataManagement.Core.UseCases.BookingForms.DeleteBookingForm;
using WHO.BioHub.DataManagement.Core.UseCases.BookingForms.ListBookingForm;
using WHO.BioHub.DataManagement.Core.UseCases.BookingForms.ListBookingForms;
using WHO.BioHub.DataManagement.Core.UseCases.BookingForms.ReadBookingForm;
using WHO.BioHub.DataManagement.Core.UseCases.BookingForms.UpdateBookingForm;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionBookingFormsExtensions
{
    public static IServiceCollection AddCoreBookingForms(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateBookingFormHandler, CreateBookingFormHandler>()
            .AddScoped<ICreateBookingFormMapper, CreateBookingFormMapper>()
            .AddScoped<CreateBookingFormCommandValidator>()

            .AddScoped<IReadBookingFormHandler, ReadBookingFormHandler>()
            .AddScoped<IReadBookingFormMapper, ReadBookingFormMapper>()
            .AddScoped<ReadBookingFormQueryValidator>()

            .AddScoped<IUpdateBookingFormHandler, UpdateBookingFormHandler>()
            .AddScoped<IUpdateBookingFormMapper, UpdateBookingFormMapper>()
            .AddScoped<UpdateBookingFormCommandValidator>()

            .AddScoped<IDeleteBookingFormHandler, DeleteBookingFormHandler>()
            .AddScoped<DeleteBookingFormCommandValidator>()

            .AddScoped<IListBookingFormsHandler, ListBookingFormsHandler>()
            .AddScoped<IListBookingFormsMapper, ListBookingFormsMapper>()
            .AddScoped<ListBookingFormsQueryValidator>()
            ;

        return services;
    }
}