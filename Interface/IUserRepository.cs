using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entitities;

namespace API.Interface;

    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllSync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser?> GetUserByIdAsync(int id);
        Task<AppUser?> GetAUserByUsernameAsync(string username);

         Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<MemberDto?> GetMemberAsync(string username);
       

    }
