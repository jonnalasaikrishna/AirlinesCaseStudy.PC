using CommonDAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace AirlineBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlineBookingController : ControllerBase
    {
        FlightBookingDBContext _flightBookingDBContext;
        public AirlineBookingController(FlightBookingDBContext flightBookingDBContext)
        {
            _flightBookingDBContext = flightBookingDBContext;
        }

        // To get Booking details

        [Authorize]
        [HttpGet("GetBookingDetails")]

        public IActionResult GetBookingData()
        {
            try
            {
                var BookingData = _flightBookingDBContext.BookingDetails.ToList();
                if (BookingData.Count == 0)
                {
                    return NotFound("No Booking Data is exist");
                }
                return Ok(BookingData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("InsertBookindDetails")]
        public IActionResult InsertUser(BookingDetail BookingData)
        {
            try
            {
                _flightBookingDBContext.BookingDetails.Add(BookingData);
                _flightBookingDBContext.SaveChanges();

                return Ok("User Details was added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Inser Booking details
        //[HttpPost]
        //[Route("insert-booking-details")]
        //public IActionResult InsertUserDetails(List<BookingDetail> bookings)
        //{
        //    try
        //    {
        //        string BookingId = GenerateBookingID();
        //        foreach (BookingDetail booking in bookings)
        //        {
        //            string TicketId = GenerateticketID();
        //            booking.TicketId = string.Empty;
        //            booking.TicketId = TicketId;
        //            booking.BookingId = string.Empty;
        //            booking.BookingId = BookingId;
        //            using (var scope = new TransactionScope())
        //            {
        //                _flightBookingDBContext.Add(booking);
        //                scope.Complete();

        //            }
        //        }
        //        return Ok();
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

        ///// <summary>
        ///// Generate UniQue TiketID
        ///// </summary>
        ///// <returns></returns>
        //public string GenerateticketID()
        //{
        //    int count = _flightBookingDBContext.BookingDetails.ToList().Count();
        //    string strSecretCode = string.Empty;
        //    string strguid = string.Empty;
        //    string strYearCode = string.Empty;
        //    string TicketID = string.Empty;
        //    try
        //    {
        //        System.Guid guid = System.Guid.NewGuid();
        //        strguid = guid.ToString();
        //        strSecretCode = strguid.Substring(strguid.LastIndexOf("-") + 1);
        //        strSecretCode = strSecretCode.ToUpper().Replace('O', 'W').Replace('0', '4');
        //        strSecretCode = strSecretCode.Substring(0, 6);

        //        TicketID = "TIC" + strSecretCode.ToUpper() + count;

        //        return TicketID;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //        return TicketID;
        //    }

        //}
        ///// <summary> bgfmnvdddddddddddd
        ///// Genarate unique Booking iD
        ///// </summary>
        ///// <returns></returns>
        //public string GenerateBookingID()
        //{
        //    int count = _flightBookingDBContext.BookingDetails.ToList().Count();
        //    string strSecretCode = string.Empty;
        //    string strguid = string.Empty;
        //    string strYearCode = string.Empty;
        //    string TicketID = string.Empty;
        //    try
        //    {
        //        System.Guid guid = System.Guid.NewGuid();
        //        strguid = guid.ToString();
        //        strSecretCode = strguid.Substring(strguid.LastIndexOf("-") + 1);
        //        strSecretCode = strSecretCode.ToUpper().Replace('O', 'W').Replace('0', '4');
        //        strSecretCode = strSecretCode.Substring(0, 6);

        //        TicketID = "BOK" + strSecretCode.ToUpper() + count;

        //        return TicketID;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //        return TicketID;
        //    }

        //}

        [HttpGet]
        [Route("Email-ticket")]
        public IActionResult GetTicket(string EmailID)
        {
            try
            {
                IEnumerable<BookingDetail> bookings = _flightBookingDBContext.BookingDetails.ToList()
                                                .Where(o => o.EmailId.ToUpper() == EmailID.ToUpper());
                return new OkObjectResult(bookings);
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Cancel Ticket
        /// </summary>
        /// <param name="TicketID"></param>
        /// <returns></returns>
      
        [Authorize]
        [HttpPut]
        [Route("cancel-ticket/{TicketID}")]
        public IActionResult CancelTicket(string TicketID)
        {
            try
            {
                IEnumerable<BookingDetail> bookings = _flightBookingDBContext.BookingDetails.ToList().Where(o => o.TicketId == TicketID).Take(1);
                foreach (BookingDetail booking in bookings)
                {
                    booking.Status = 0;
                    booking.Statusstr = "Canceled";
                    using (var scope = new TransactionScope())
                    {
                        _flightBookingDBContext.Update(booking);
                        
                        _flightBookingDBContext.SaveChanges();

                        scope.Complete();
                    }
                }
                return new OkObjectResult(bookings);
            }
            catch
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// PNR STATUS Check
        /// </summary>
        /// <param name="TicketID"></param>
        /// <returns></returns>
        /// 
        [Authorize]
        [HttpGet]
        [Route("pnr-ticket/{TicketID}")]
        public IActionResult GetpnrTicket(string TicketID)
        {
            try
            {
                IEnumerable<BookingDetail> bookings = _flightBookingDBContext.BookingDetails.ToList()
                                                .Where(o => o.TicketId.ToUpper() == TicketID.ToUpper());
                return new OkObjectResult(bookings);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
