using System.Diagnostics;
using Enigmatry.Blueprint.Model.Auditing;

namespace Enigmatry.Blueprint.Model.Identity
{
    public class UserUpdatedDomainEvent : AuditableDomainEvent
    {
        public UserUpdatedDomainEvent(string userName) : base("UserUpdated")
        {
            UserName = userName;
        }

        public string UserName { get; }

        public override object AuditPayload => new {UserName};

        public void TestDuplication()
        {
            for (int i = 0; i < 100; i++)
            {
                Debug.WriteLine("duplication here1");
                
                Debug.WriteLine("duplication here2");
                
                Debug.WriteLine("duplication here3");
               
            }
        }
    }
}