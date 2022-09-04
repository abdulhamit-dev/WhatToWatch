using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Entities.Conrete;

namespace WhatToWatch.DataAccess.Concrete.DataMapping
{
    public class MovieNoteAndVoteMap : IEntityTypeConfiguration<MovieNoteAndVote>
    {
        public void Configure(EntityTypeBuilder<MovieNoteAndVote> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Vote).IsRequired();
        }
    }
}
