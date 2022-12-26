namespace Mailsysteem_DAL
{
    public abstract class BaseRepository
    {
        protected string ConnectionString { get; }

        public BaseRepository()
        {
            ConnectionString = DatabaseConnection.Connectionstring("MailsysteemEntities");
        }
    }
}