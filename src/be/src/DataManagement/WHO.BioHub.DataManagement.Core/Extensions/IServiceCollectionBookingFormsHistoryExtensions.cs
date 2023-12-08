using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.CreateBookingFormHistory;
using WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.DeleteBookingFormHistory;
using WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.ListBookingFormsHistory;
using WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.ReadBookingFormHistory;
using WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.UpdateBookingFormHistory;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionBookingFormsHistoryExtensions
{
    public static IServiceCollection AddCoreBookingFormsHistory(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateBookingFormHistoryHandler, CreateBookingFormHistoryHandler>()
            .AddScoped<ICreateBookingFormHistoryMapper, CreateBookingFormHistoryMapper>()
            .AddScoped<CreateBookingFormHistoryCommandValidator>()

            .AddScoped<IReadBookingFormHistoryHandler, ReadBookingFormHistoryHandler>()
            .AddScoped<ReadBookingFormHistoryQueryValidator>()

            .AddScoped<IUpdateBookingFormHistoryHandler, UpdateBookingFormHistoryHandler>()
            .AddScoped<IUpdateBookingFormHistoryMapper, UpdateBookingFormHistoryMapper>()
            .AddScoped<UpdateBookingFormHistoryCommandValidator>()

            .AddScoped<IDeleteBookingFormHistoryHandler, DeleteBookingFormHistoryHandler>()
            .AddScoped<DeleteBookingFormHistoryCommandValidator>()

            .AddScoped<IListBookingFormsHistoryHandler, ListBookingFormsHistoryHandler>()
            .AddScoped<ListBookingFormsHistoryQueryValidator>()
            ;

        return services;
    }
}