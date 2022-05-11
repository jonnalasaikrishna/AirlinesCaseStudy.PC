using System;
using System.Collections.Generic;

#nullable disable

namespace CommonDAL.Models
{
    public partial class BookingDetail
    {
        public string TicketId { get; set; }
        public string BookingId { get; set; }
        public string FlightNumber { get; set; }
        public DateTime DateOfJourney { get; set; }
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public DateTime BoardingTime { get; set; }
        public string EmailId { get; set; }
        public string UserName { get; set; }
        public string PassportNumber { get; set; }
        public int Age { get; set; }
        public int SeatNumber { get; set; }
        public int Status { get; set; }
        public string Statusstr { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Updatedby { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
