﻿using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HueFestivalTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ManagerOrStaff")]
    public class CheckinsController : ControllerBase
    {
        private readonly ICheckinRepository _checkinRepository;
        private readonly ITicketRepository _ticketRepository;

        public CheckinsController(ICheckinRepository checkinRepository,
            ITicketRepository ticketRepository)
        {
            _checkinRepository = checkinRepository;
            _ticketRepository = ticketRepository;
        }

        // GET: api/Checkins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Checkin>>> GetCheckins()
        {
            return await _checkinRepository.GetAllCheckinAsync();
        }

        // GET: api/Checkins/Paging
        [HttpGet("Paging")]
        public async Task<ActionResult<IEnumerable<Checkin>>> GetCheckinPaging(int pageNumber, int pageSize)
        {
            var result = await _checkinRepository.GetCheckinPagingAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // GET: api/Checkins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Checkin>> GetCheckin(Guid id)
        {
            var checkin = await _checkinRepository.GetCheckinByIdAsync(id);

            if (checkin == null)
            {
                return Ok(new
                {
                    Message = "This Checkin doesn't exist"
                });
            }

            return checkin;
        }

        // POST: api/Checkins
        [HttpPost]
        public async Task<ActionResult<Checkin>> PostCheckin([FromForm] CheckinDTO checkin)
        {
            bool status;
            string message;
            Guid idAccount = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            if (checkin.QRCodeContent == string.Empty)
            {
                return Ok(new
                {
                    Message = "Please enter QRCode Content"
                });
            }
            string[] qrcodeContent = checkin.QRCodeContent!.Split("|");
            var checkTicket = await _ticketRepository.GetTicketByTicketNumberAsync(qrcodeContent[0]);

            if (checkTicket == null)
            {
                status = false;
                message = "Invalid Ticket";
            }
            else
            {
                if (CheckTicket(checkTicket, checkin.QRCodeContent) == false)
                {
                    status = false;
                    message = "Invalid Ticket";
                }
                else
                {
                    status = true;
                    message = "Valid Ticket";
                }
            }
            if (status == true && await _checkinRepository.GetCheckinAsync(checkin.QRCodeContent) != null)
            {
                return Ok(new
                {
                    Message = "This Ticket has been checked in"
                });
            }

            var result = await _checkinRepository.InsertCheckinAsync(checkin, idAccount, status);

            return Ok(new
            {
                Message = "Insert Success " + message,
                result
            });
        }


        // DELETE: api/Checkins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheckin(Guid id)
        {
            var checkin = await _checkinRepository.GetCheckinByIdAsync(id);
            if (checkin == null)
            {
                return Ok(new
                {
                    Message = "This checkin doesn't exist"
                });
            }

            await _checkinRepository.DeleteCheckinAsync(checkin);

            return Ok(new
            {
                Message = "Delete Success"
            });
        }


        private bool CheckTicket(Ticket ticket, string qrCodeContent)
        {
            string[] qrcodeContent = qrCodeContent!.Split("|");

            if (qrcodeContent[1] != ticket.EventLocation!.Event!.Name)
            {
                return false;
            }
            if (qrcodeContent[2] != ticket.EventLocation.Time.ToString("HH:mm"))
            {
                return false;
            }
            if (qrcodeContent[3] != ticket.EventLocation.DateStart.ToString("dd/MM/yyyy"))
            {
                return false;
            }
            if (qrcodeContent[4] != ticket.EventLocation!.Location!.Title)
            {
                return false;
            }
            if (qrcodeContent[5] != ticket.Price.ToString())
            {
                return false;
            }
            if (qrcodeContent[6] != ticket.TypeTicket!.Name)
            {
                return false;
            }
            return true;
        }
    }
}
