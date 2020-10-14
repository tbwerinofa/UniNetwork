namespace DataAccess
{
    public class Moderator : AuditBase
    {

        public Moderator()
        {
        }

        public int CalendarId { get; set; }
        public int MemberId { get; set; }

        public virtual Calendar Calendar { get; set; }

        public virtual Member Member { get; set; }
 


    }
}
