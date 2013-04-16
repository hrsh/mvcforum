﻿using System.Collections.Generic;
using System.Linq;
using MVCForum.Data.Context;
using MVCForum.Domain.DomainModel;
using MVCForum.Domain.Interfaces;
using MVCForum.Domain.Interfaces.Repositories;

namespace MVCForum.Data.Repositories
{
    public class BannedEmailRepository : IBannedEmailRepository
    {
        private readonly MVCForumContext _context;
        public BannedEmailRepository(IMVCForumContext context)
        {
            _context = context as MVCForumContext;
        }

        public BannedEmail Add(BannedEmail bannedEmail)
        {
            return _context.BannedEmail.Add(bannedEmail);
        }

        public void Delete(BannedEmail bannedEmail)
        {
            _context.BannedEmail.Remove(bannedEmail);
        }

        public IList<BannedEmail> GetAll()
        {
            return _context.BannedEmail.ToList();
        }

        public PagedList<BannedEmail> GetAllPaged(int pageIndex, int pageSize)
        {
            var total = _context.BannedEmail.Count();

            var results = _context.BannedEmail
                                .OrderByDescending(x => x.Email)
                                .Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            return new PagedList<BannedEmail>(results, pageIndex, pageSize, total);
        }

        public IList<BannedEmail> GetAllWildCards()
        {
            return _context.BannedEmail.Where(x => x.Email.StartsWith("*")).ToList();
        }

        public IList<BannedEmail> GetAllNonWildCards()
        {
            return _context.BannedEmail.Where(x => !x.Email.StartsWith("*")).ToList();
        }
    }
}
