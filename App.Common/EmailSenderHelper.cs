using App.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI.WebControls;

namespace App.Common
{
    /// <summary>
    /// This is a class for send a email with information about the ticket
    /// </summary>
    public class EmailSenderHelper
    {
        private static readonly string TICKET_CREATED = "¡Ticket creado con éxito!";
        private static readonly string TICKET_FINISHED = "¡Tu ticket ha sido terminado!";
        private static readonly string TICKET_FINISHED_MESSAGE = "http://localhost:1493/Ticket/Rate/";
        private static readonly string TICKET_CREATED_MESSAGE = "http://localhost:1493/";
        

        #region Attributes

        private SmtpClient _client;
        private MailDefinition _md;
        private ListDictionary _replacements;
        private MailMessage _mm;

        #endregion

        #region Properties

        public SmtpClient Client
        {
            get
            {
                return _client;
            }

            set
            {
                _client = value;
            }
        }

        public MailDefinition Md
        {
            get
            {
                return _md;
            }

            set
            {
                _md = value;
            }
        }

        public ListDictionary Replacements
        {
            get
            {
                return _replacements;
            }

            set
            {
                _replacements = value;
            }
        }

        public MailMessage Mm
        {
            get
            {
                return _mm;
            }

            set
            {
                _mm = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// This is a default constructor of the class
        /// </summary>
        public EmailSenderHelper()
        {
            Client = new SmtpClient();
            Md = new MailDefinition();
            Replacements = new ListDictionary();
            Mm = new MailMessage();
            
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This method is to send an email to user's mailbox
        /// </summary>
        /// <param name="body"></param>
        public bool SendEmail(Ticket ticket, List<RequestType> requestTypes, bool isFinished)
        {
            _client.Port = 587;
            _client.Host = "smtp.gmail.com";
            _client.EnableSsl = true;
            _client.Timeout = 10000;
            _client.UseDefaultCredentials = false;
            _client.Credentials = new NetworkCredential("arkusnexustickets@gmail.com", "Tickets#456");
            _client.DeliveryMethod = SmtpDeliveryMethod.Network;
            
            using (StreamReader reader = File.OpenText(HttpContext.Current.Server.MapPath("~/Content/index.html")))
            {
                
                _md.From = "arkusnexustickets@gmail.com";
                _md.IsBodyHtml = true;
                _md.Subject = "Mind Tickets";
                if (!isFinished)
                {
                    _replacements.Add("{TicketStatus}", TICKET_CREATED);
                }
                else
                {
                    _replacements.Add("{TicketStatus}", TICKET_FINISHED);
                }
                _replacements.Add("{TicketId}", ticket.Id.ToString());
                _replacements.Add("{TicketOwner}", ticket.UserName.ToString());
                _replacements.Add("{TicketRequestType}", requestTypes.Find(x => x.Id == ticket.RequestTypeId).Description.ToString());
                _replacements.Add("{TicketCreatedDate}", ticket.CreatedDate.ToString());
                _replacements.Add("{TicketBody}", ticket.Description);

                if (isFinished)
                {
                    _replacements.Add("{FinishedMessage}", "<a href =" + TICKET_FINISHED_MESSAGE + ticket.Id + " style='color:white;text-decoration:none;font-size:150%;	text-shadow: 2px 2px 5px lightsalmon;' > ¡Ingresa a MIND Tickets para calificarlo!</ a >");
                }
                else
                {
                    _replacements.Add("{FinishedMessage}", "<a href =" + TICKET_CREATED_MESSAGE + " style='color:white;text-decoration:none;' > ¡Ingresa a MIND Tickets para revisar el seguimiento! </a>");
                }
                
                _mm = _md.CreateMailMessage(ticket.UserEmail, _replacements, reader.ReadToEnd(), new System.Web.UI.Control());
                try
                {
                    _client.Send(_mm);
                    return true;
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
        }
        #endregion
    }
}
