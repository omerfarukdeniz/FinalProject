using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class MemberOperationClaim:IEntity
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int OperationClaimId { get; set; }

    }
}
