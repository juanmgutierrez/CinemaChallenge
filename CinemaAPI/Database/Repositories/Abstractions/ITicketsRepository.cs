﻿using CinemaAPI.Database.Entities;

namespace CinemaAPI.Database.Repositories.Abstractions;

public interface ITicketsRepository
{
    Task<TicketEntity> ConfirmPaymentAsync(TicketEntity ticket, CancellationToken cancel);
    Task<TicketEntity> CreateAsync(ShowtimeEntity showtime, IEnumerable<SeatEntity> selectedSeats, CancellationToken cancel);
    Task<TicketEntity> GetAsync(Guid id, CancellationToken cancel);
    Task<IEnumerable<TicketEntity>> GetEnrichedAsync(int showtimeId, CancellationToken cancel);
}