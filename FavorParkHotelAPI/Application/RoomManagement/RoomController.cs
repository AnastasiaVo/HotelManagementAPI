﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FavorParkHotelAPI.Application.RoomManagement.Dto;
using FavorParkHotelAPI.Application.RoomManagement.Services;
using FavorParkHotelAPI.Application.RoomManagement.Query;

namespace FavorParkHotelAPI.Application.RoomManagement
{
    [ApiController]
    [Route("room")]
    [Authorize] // Add authorization if required
    public class RoomController : BaseController
    {
        private readonly IMediator _mediator;

        public RoomController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateRoom([FromBody] ApplyRoomDto roomDto)
        {
            var response = await _mediator.Send(new CreateRoomService(roomDto));
            return Result(response);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateRoom(int roomId, [FromBody] ApplyRoomDto roomDto)
        {
            var response = await _mediator.Send(new UpdateRoomService(roomId, roomDto));
            return Result(response);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteRoom([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteRoomService(id));
            return Result(response);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllRooms()
        {
            var response = await _mediator.Send(new GetAllHotelRoomsService());
            return Result(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRoomById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetHotelRoomByIdService(id));
            return Result(response);
        }

        [HttpGet]
        [Route("{id}/capacity")]
        public async Task<IActionResult> GetRoomCapacityById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetRoomCapacityByIdService(id));
            return Result(response);
        }

        [HttpGet]
        [Route("checkReservation")]
        public async Task<IActionResult> CheckRoomReservation(int roomId, DateTime startDate, DateTime endDate)
        {
            var response = await _mediator.Send(new CheckRoomReservationService(roomId, startDate, endDate));
            return Result(response);
        }

        [HttpGet("free-rooms/{capacity}")]
        public async Task<IActionResult> GetFreeHotelRoomsByCapacityAsync(int capacity)
        {
            var query = new GetFreeHotelRoomsByCapacityService(capacity);
            var response = await Mediator.Send(query);

            return Result(response);
        }

    }
}

